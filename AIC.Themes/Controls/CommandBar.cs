using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace AIC.Themes.Controls
{
    public class CommandBar:ItemsControl
    {
        static CommandBar()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CommandBar), new FrameworkPropertyMetadata(typeof(CommandBar)));
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
        }

        //#region Dependency Property
        //#region public string Text
        ///// <summary>
        ///// 
        ///// </summary>
        //public string Text
        //{
        //    get { return GetValue(TextProperty) as string; }
        //    set { SetValue(TextProperty, value); }
        //}

        ///// <summary>
        ///// Identifies the Text dependency property.
        ///// </summary>
        //public static readonly DependencyProperty TextProperty =
        //    DependencyProperty.Register(
        //        "Text",
        //        typeof(string),
        //        typeof(DemoCategory),
        //        new PropertyMetadata(null, OnTextChanged));

        //private static void OnTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        //{
        //    DemoCategory source = d as DemoCategory;
        //    string value = (string)e.NewValue;
        //    if (source != null)
        //    {
        //        if (source.Title == "" || source.Title == null)
        //        {
        //            source.TitleText = value;
        //        }
        //    }
        //}
        //#endregion public string Text
        //#endregion
    }
}
