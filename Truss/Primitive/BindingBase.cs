﻿using System;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using Kent.Boogaart.HelperTrinity;
using Kent.Boogaart.HelperTrinity.Extensions;

namespace Kent.Boogaart.Truss.Primitive
{
	/// <summary>
	/// A base class for all binding types provided with Truss.
	/// </summary>
	/// <remarks>
	/// <para>
	/// This class provides behavior common to all Truss bindings.
	/// </para>
	/// <para>
	/// All bindings must have a single target object. That object is made accessible via the <see cref="TargetObject"/> property. As
	/// with most properties on this class, attempting to set the <see cref="TargetObject"/> after the binding has been activated will
	/// throw an exception.
	/// </para>
	/// <para>
	/// All bindings also have a mode which is exposed by the <see cref="Mode"/> property. The mode determines which direction or
	/// directions changes are pushed. By default, the mode is set to <see cref="BindingMode.TwoWay"/>, which means that changes are
	/// pushed in both directions. Changes can be pushed in a single direction by setting the <see cref="Mode"/> to either
	/// <see cref="BindingMode.OneWayToTarget"/> or <see cref="BindingMode.OneWayToSource"/>.
	/// </para>
	/// <para>
	/// The <see cref="Container"/> property provides access to the <see cref="IBindingContainer"/> in which the binding is activated.
	/// For many bindings, this will be the <see cref="BindingManager"/> to which they were added. However, bindings added to a
	/// <see cref="MultiSourceBinding"/>'s <see cref="MultiSourceBinding.Sources"/> collection will have the
	/// <see cref="MultiSourceBinding"/> as their container.
	/// </para>
	/// </remarks>
	public abstract class BindingBase
	{
        private static readonly ExceptionHelper _exceptionHelper = new ExceptionHelper(typeof(BindingBase));
        private IBindingContainer _container;
		private readonly WeakReference _targetObject;
		private PropertyExpression _targetPropertyExpression;
		private BindingMode _mode;
		private int _activated;

		/// <summary>
		/// Constructs an instance of <c>BindingBase</c>.
		/// </summary>
		protected BindingBase()
		{
			_targetObject = new WeakReference(null);
		}

		/// <summary>
		/// Gets the <see cref="BindingManager"/> that this binding is currently activated in, or <see langword="null"/> if it is not
		/// activated.
		/// </summary>
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public IBindingContainer Container
		{
			get { return _container; }
		}

		/// <summary>
		/// Gets or sets the target object for this binding.
		/// </summary>
		public object TargetObject
		{
			get { return _targetObject.Target; }
			set
			{
				VerifyNotActivated();
				_targetObject.Target = value;
			}
		}

		/// <summary>
		/// Gets or sets the <see cref="PropertyExpression"/> that resolves to the bound property in the <see cref="TargetObject"/>.
		/// </summary>
		/// <remarks>
		/// <para>
		/// When a binding is activated, a target property expression must be determined. This will allow the target property to be
		/// monitored and updated. If this property has been set explicitly to a non-<see langword="null"/> value, the binding will use it
		/// once activated instead of determining one.
		/// </para>
		/// </remarks>
		/// <value>
		/// The <see cref="PropertyExpression"/>, or <see langword="null"/> if it has not yet been set or determined.
		/// </value>
		public PropertyExpression TargetPropertyExpression
		{
			get { return _targetPropertyExpression; }
			private set { _targetPropertyExpression = value; }
		}

		/// <summary>
		/// Gets or sets the mode in which this binding operates.
		/// </summary>
		public BindingMode Mode
		{
			get { return _mode; }
			set
			{
				VerifyNotActivated();
				_mode = value;
			}
		}

		/// <summary>
		/// Attempts to activate this <c>BindingBase</c> in the specified <see cref="IBindingContainer"/>.
		/// </summary>
		/// <param name="bindingContainer">
		/// The <see cref="IBindingContainer"/> within which this binding will be activated.
		/// </param>
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public void Activate(IBindingContainer bindingContainer)
		{
			Activate(bindingContainer, null);
		}

		/// <summary>
		/// Attempts to activate this <c>BindingBase</c> in the specified <see cref="IBindingContainer"/> and with a custom target
		/// <see cref="PropertyExpression"/>.
		/// </summary>
		/// <param name="bindingContainer">
		/// The <see cref="IBindingContainer"/> within which this binding will be activated.
		/// </param>
		/// <param name="targetPropertyExpression">
		/// The <see cref="PropertyExpression"/> to use for this binding's target property expression.
		/// </param>
		public void Activate(IBindingContainer bindingContainer, PropertyExpression targetPropertyExpression)
		{
			bindingContainer.AssertNotNull("bindingContainer");
			_exceptionHelper.ResolveAndThrowIf(!bindingContainer.Bindings.Contains(this), "BindingMustBeActivatedInOwningContainer");

			if (Interlocked.CompareExchange(ref _activated, 1, 0) != 0)
			{
				throw _exceptionHelper.Resolve("AlreadyActivated");
			}

			_container = bindingContainer;
			_targetPropertyExpression = targetPropertyExpression;

			if (TargetPropertyExpression == null)
			{
				//no target property expression was provided when activating, so ask the base class to create one
				TargetPropertyExpression = AttemptCreateTargetPropertyExpression();
			}

			if (TargetPropertyExpression != null && (Mode == BindingMode.TwoWay || Mode == BindingMode.OneWayToSource))
			{
				//listen for changes in the target
				TargetPropertyExpression.ValueChanged += _targetPropertyExpression_ValueChanged;
			}

			OnActivated();
		}

		/// <summary>
		/// Called whenever the value of the target property expression on this <c>BindingBase</c> is changed.
		/// </summary>
		protected virtual void OnTargetPropertyExpressionValueChanged()
		{
		}

		/// <summary>
		/// Attempts to create the target <see cref="PropertyExpression"/> for this binding.
		/// </summary>
		/// <returns>
		/// A <see cref="PropertyExpression"/>, or <see langword="null"/> if no expression could be created.
		/// </returns>
		protected abstract PropertyExpression AttemptCreateTargetPropertyExpression();

		/// <summary>
		/// Deactivates this <c>BindingBase</c> so that it can be activated again.
		/// </summary>
		internal void Deactivate()
		{
			if (Interlocked.Exchange(ref _activated, 0) == 1)
			{
				_container = null;

				if (TargetPropertyExpression != null)
				{
					TargetPropertyExpression.ValueChanged -= _targetPropertyExpression_ValueChanged;
					TargetPropertyExpression = null;
				}

				OnDeactivated();
			}
		}

		//TODO: these VerifyNotActivated methods aren't thread-safe from the caller's perspective
		//Consider: thread A calls VerifyNotInUse which returns without problem, then thread B uses the same object, then thread A continues
		//on thinking the object is not in use

		/// <summary>
		/// Verifies that this <c>BindingBase</c> is activated, and throws an exception with a default message if it is not.
		/// </summary>
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public void VerifyActivated()
		{
			VerifyActivated("BindingBase is not yet activated.");
		}

		/// <summary>
		/// Verifies that this <c>BindingBase</c> is activated, and throws an exception with the specified message if it is not.
		/// </summary>
		/// <param name="message">
		/// The message to use for the exception if the <c>BindingBase</c> is not activated.
		/// </param>
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public void VerifyActivated(string message)
		{
            _exceptionHelper.ResolveAndThrowIf(_activated == 0, "ActivatedOrNotActivated", message);
		}

		/// <summary>
		/// Verifies that this <c>BindingBase</c> is not activated, and throws an exception with a default message if it is.
		/// </summary>
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public void VerifyNotActivated()
		{
			VerifyNotActivated("BindingBase is currently activated and cannot be modified.");
		}

		/// <summary>
		/// Verifies that this <c>BindingBase</c> is not activated, and throws an exception with the specified message if it is.
		/// </summary>
		/// <param name="message">
		/// The message to use for the exception if the <c>BindingBase</c> is activated.
		/// </param>
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public void VerifyNotActivated(string message)
		{
            _exceptionHelper.ResolveAndThrowIf(_activated == 1, "ActivatedOrNotActivated", message);
		}

		/// <summary>
		/// Called when this <c>BindingBase</c> is activated.
		/// </summary>
		protected virtual void OnActivated()
		{
		}

		/// <summary>
		/// Called when this <c>BindingBase</c> is deactivated.
		/// </summary>
		protected virtual void OnDeactivated()
		{
		}

		private void _targetPropertyExpression_ValueChanged(object sender, EventArgs e)
		{
			OnTargetPropertyExpressionValueChanged();
		}
	}
}