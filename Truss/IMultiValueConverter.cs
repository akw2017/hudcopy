using System;
using Kent.Boogaart.Truss.Primitive;

namespace Kent.Boogaart.Truss
{
	/// <summary>
	/// Provides a way to convert values for a <see cref="MultiSourceBinding"/>.
	/// </summary>
	/// <remarks>
	/// <para>
	/// A <see cref="MultiSourceBinding"/> requires a means to convert multiple values into one, and vice-versa. An implementation of
	/// this interface must be assigned to the <see cref="MultiSourceBinding.Converter"/> parameter before the binding can be activated.
	/// </para>
	/// </remarks>
	/// <include file='Documentation/Examples.xml' path='Examples/Example[@Name="IMultiValueConverter.Simple"]/*'/>
	/// <include file='Documentation/Examples.xml' path='Examples/Example[@Name="IMultiValueConverter.Complex"]/*'/>
	public interface IMultiValueConverter
	{
		/// <summary>
		/// Converts multiple source values into a single target value.
		/// </summary>
		/// <remarks>
		/// This method will only be called if the mode of the <see cref="MultiSourceBinding"/> is either <see cref="BindingMode.TwoWay"/>
		/// or <see cref="BindingMode.OneWayToTarget"/>.
		/// </remarks>
		/// <param name="values">
		/// The values from all sources of the <see cref="MultiSourceBinding"/>.
		/// </param>
		/// <param name="type">
		/// The type of the target property.
		/// </param>
		/// <param name="parameter">
		/// The converter parameter.
		/// </param>
		/// <returns>
		/// The single target value.
		/// </returns>
		object ConvertSourcesToTarget(object[] values, Type type, object parameter);

		/// <summary>
		/// Converts a single target value into multiple source values.
		/// </summary>
		/// <remarks>
		/// This method will only be called if the mode of the <see cref="MultiSourceBinding"/> is either <see cref="BindingMode.TwoWay"/>
		/// or <see cref="BindingMode.OneWayToSource"/>.
		/// </remarks>
		/// <param name="value">
		/// The single target value.
		/// </param>
		/// <param name="types">
		/// The types of the source properties.
		/// </param>
		/// <param name="parameter">
		/// The converter parameter.
		/// </param>
		/// <returns>
		/// The multiple source values, which must have the same length as <paramref name="types"/>.
		/// </returns>
		object[] ConvertTargetToSources(object value, Type[] types, object parameter);
	}
}