using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AIC.PDA.Controls
{
    /// <summary>
    /// Follow steps 1a or 1b and then 2 to use this custom control in a XAML file.
    ///
    /// Step 1a) Using this custom control in a XAML file that exists in the current project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:HUD"
    ///
    ///
    /// Step 1b) Using this custom control in a XAML file that exists in a different project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:HUD;assembly=HUD"
    ///
    /// You will also need to add a project reference from the project where the XAML file lives
    /// to this project and Rebuild to avoid compilation errors:
    ///
    ///     Right click on the target project in the Solution Explorer and
    ///     "Add Reference"->"Projects"->[Browse to and select this project]
    ///
    ///
    /// Step 2)
    /// Go ahead and use your control in the XAML file.
    ///
    ///     <MyNamespace:SensorAdapterControl/>
    ///
    /// </summary>
    [TemplatePart(Name = "RootGrid", Type = typeof(Grid))]
    [TemplatePart(Name = "CyclePath", Type = typeof(Path))]
    [TemplatePart(Name = "OutterEllipse", Type = typeof(Ellipse))]
    [TemplatePart(Name = "InnerEllipse", Type = typeof(Ellipse))]
    public class SensorAdapterControl : Control
    {
        private Path cyclePath;
        private Ellipse outterEllipse;
        private Ellipse innerEllipse;
        private Grid rootGrid;

        static SensorAdapterControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(SensorAdapterControl), new FrameworkPropertyMetadata(typeof(SensorAdapterControl)));
        }

        protected override Size MeasureOverride(Size availableSize)
        {
            if (!double.IsPositiveInfinity(availableSize.Height)&& !double.IsPositiveInfinity(availableSize.Width))
            {
               
                var minSize = Math.Min(availableSize.Width, availableSize.Height);

                rootGrid.Width = minSize;
                rootGrid.Height = minSize;
                outterEllipse.Width = minSize;
                outterEllipse.Height = minSize;
                innerEllipse.Width = minSize * 0.6;
                innerEllipse.Height = minSize * 0.6;
                cyclePath.Width = minSize * 0.8;
                cyclePath.Height = minSize * 0.8;
                return availableSize;
            }
            return new Size(0, 0);
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            cyclePath = GetTemplateChild("CyclePath") as Path;
            outterEllipse = GetTemplateChild("OutterEllipse") as Ellipse;
            innerEllipse = GetTemplateChild("InnerEllipse") as Ellipse;
            rootGrid = GetTemplateChild("RootGrid") as Grid;
        }
    }
}
