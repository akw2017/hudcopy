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
using System.Windows.Media.Animation;
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
    ///     <MyNamespace:SampleFreControl/>
    ///
    /// </summary>
    [TemplatePart(Name = "sinPath", Type = typeof(Path))]
    [TemplatePart(Name = "root", Type = typeof(Grid))]
    public class SampleFreControl : Control
    {
        private Path sinPath;
        private Grid root;
        private  Storyboard stb = new Storyboard();

        static SampleFreControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(SampleFreControl), new FrameworkPropertyMetadata(typeof(SampleFreControl)));
        }


        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            sinPath = GetTemplateChild("sinPath") as Path;
            root = GetTemplateChild("root") as Grid;
        }

        protected override Size MeasureOverride(Size constraint)
        {
            Size size;
            if (root.ActualWidth==0||root.ActualHeight==0)
            {
                size = constraint;
            }
            else
            {
                size = new Size(root.ActualWidth, root.ActualHeight);
            }
            var left = -size.Width + 1;
            sinPath.Width = size.Width * 2;
            sinPath.Margin = new Thickness(left, 0, 0, 0);

            stb.Stop();
            stb.Children.Clear();

            DoubleAnimation animation = new DoubleAnimation(0, -sinPath.Margin.Left, TimeSpan.FromMilliseconds(2000));
            Storyboard.SetTarget(animation, sinPath);
            Storyboard.SetTargetProperty(animation, new PropertyPath("(UIElement.RenderTransform).(TransformGroup.Children)[3].(TranslateTransform.X)"));
            stb.Children.Add(animation);

            stb.RepeatBehavior = RepeatBehavior.Forever;
            stb.Begin();
  
            return base.MeasureOverride(constraint);
        }
    }
}
