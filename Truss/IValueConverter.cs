using System;
using Kent.Boogaart.Truss.Primitive;

namespace Kent.Boogaart.Truss
{
	/// <summary>
	/// Provides a way to convert values for a <see cref="SingleSourceBinding"/>.
	/// </summary>
	/// <remarks>
	/// <para>
	/// Converters on a <see cref="SingleSourceBinding"/> are not required. However, if a default conversion between target and source
	/// does not exist, or if the conversion should be customized in some manner, implementations of this interface allow the conversion
	/// behavior to be tailored.
	/// </para>
	/// </remarks>
	/// <include file='Documentation/Examples.xml' path='Examples/Example[@Name="IValueConverter.Simple"]/*'/>
	/// <include file='Documentation/Examples.xml' path='Examples/Example[@Name="IValueConverter.Complex"]/*'/>
	public interface IValueConverter
	{
		/// <summary>
		/// Converts the source value to a target value.
		/// </summary>
		/// <remarks>
		/// This method will only be called if the mode of the <see cref="SingleSourceBinding"/> is either <see cref="BindingMode.TwoWay"/>
		/// or <see cref="BindingMode.OneWayToTarget"/>.
		/// </remarks>
		/// <param name="value">
		/// The source value to convert.
		/// </param>
		/// <param name="type">
		/// The type of the target property.
		/// </param>
		/// <param name="parameter">
		/// The converter parameter.
		/// </param>
		/// <returns>
		/// The target value.
		/// </returns>
		object ConvertSourceToTarget(object value, Type type, object parameter);

		/// <summary>
		/// Converts the target value to a source value.
		/// </summary>
		/// <remarks>
		/// This method will only be called if the mode of the <see cref="SingleSourceBinding"/> is either <see cref="BindingMode.TwoWay"/>
		/// or <see cref="BindingMode.OneWayToSource"/>.
		/// </remarks>
		/// <param name="value">
		/// The target value to convert.
		/// </param>
		/// <param name="type">
		/// The type of the source property.
		/// </param>
		/// <param name="parameter">
		/// The converter parameter.
		/// </param>
		/// <returns>
		/// The source value.
		/// </returns>
		object ConvertTargetToSource(object value, Type type, object parameter);
	}
}