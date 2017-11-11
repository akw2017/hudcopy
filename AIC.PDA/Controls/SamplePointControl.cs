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
    [TemplatePart(Name = "sinPath", Type = typeof(Sine))]
    [TemplatePart(Name = "root", Type = typeof(Grid))]
    [TemplatePart(Name = "___Ellipse4_", Type = typeof(Ellipse))]
    public class SamplePointControl : Control
    {
        private Sine sinPath;
        private Grid root;
        private Ellipse ___Ellipse4_;
        private Storyboard stb = new Storyboard();
        static SamplePointControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(SamplePointControl), new FrameworkPropertyMetadata(typeof(SamplePointControl)));
        }

        public SamplePointControl()
        {
            Loaded += SamplePointControl_Loaded;
        }

        private void SamplePointControl_Loaded(object sender, RoutedEventArgs e)
        {
            if(sinPath!=null)
            {
                Animate();
            } 
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            sinPath = GetTemplateChild("sinPath") as Sine;
            root = GetTemplateChild("root") as Grid;
            ___Ellipse4_ = GetTemplateChild("___Ellipse4_") as Ellipse;
        }

        protected override Size MeasureOverride(Size constraint)
        {
            if(IsLoaded)
            {
                Animate();
            } 
            return base.MeasureOverride(constraint);
        }

        private void Animate()
        {
            stb.Stop();
            stb.Children.Clear();

            var geo = PathGeometry.CreateFromGeometry(sinPath.AnimationPath);
            DoubleAnimationUsingPath animationX = new DoubleAnimationUsingPath();
            animationX.Source = PathAnimationSource.X;
            animationX.PathGeometry = geo;
            animationX.Duration = TimeSpan.FromMilliseconds(2000);
            Storyboard.SetTarget(animationX, ___Ellipse4_);
            Storyboard.SetTargetProperty(animationX, new PropertyPath("(UIElement.RenderTransform).(TransformGroup.Children)[3].(TranslateTransform.X)"));
            stb.Children.Add(animationX);

            DoubleAnimationUsingPath animationY = new DoubleAnimationUsingPath();
            animationY.Source = PathAnimationSource.Y;
            animationY.PathGeometry = geo;
            animationY.Duration = TimeSpan.FromMilliseconds(2000);
            Storyboard.SetTarget(animationY, ___Ellipse4_);
            Storyboard.SetTargetProperty(animationY, new PropertyPath("(UIElement.RenderTransform).(TransformGroup.Children)[3].(TranslateTransform.Y)"));
            stb.Children.Add(animationY);

            //PointAnimationUsingPath animation = new PointAnimationUsingPath();
            //animation.PathGeometry = PathGeometry.CreateFromGeometry(sinPath.RenderedGeometry);
            //animation.Duration = TimeSpan.FromMilliseconds(1000);
            //Storyboard.SetTarget(animation, myAnimatedEllipseGeometry);
            //Storyboard.SetTargetProperty(animation, new PropertyPath(EllipseGeometry.CenterProperty));
            //stb.Children.Add(animation);

            stb.RepeatBehavior = RepeatBehavior.Forever;
            stb.Begin();
        }
    }
}
