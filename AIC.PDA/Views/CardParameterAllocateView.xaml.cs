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
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AIC.PDA.Views
{
    /// <summary>
    /// Interaction logic for CardParameterAllocateView.xaml
    /// </summary>
    public partial class CardParameterAllocateView : UserControl
    {
        public CardParameterAllocateView()
        {
            InitializeComponent();
        }

        private void backGrid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (allocateCardontent.Content == null) return;
            var stb = new Storyboard();

            frontVisual.Visual = BorderIn; 
            backVisual.Visual = backBorderIn;
            contentBehavior.IsEnable = false;

            var xScale = rootGrid.ActualWidth / contentGrid.Width;
            var yScale = (rootGrid.ActualHeight / contentGrid.ActualHeight) * 0.8;
            DoubleAnimation animationx = new DoubleAnimation(0, rootGrid.ActualWidth / 2 - (contentGrid.ActualWidth* yScale) / 2, TimeSpan.FromSeconds(0.5));
            Storyboard.SetTarget(animationx, contentGrid);
            Storyboard.SetTargetProperty(animationx, new PropertyPath("(UIElement.RenderTransform).(TransformGroup.Children)[3].(TranslateTransform.X)"));
            stb.Children.Add(animationx);

            DoubleAnimation animationy = new DoubleAnimation(0, (contentGrid.ActualHeight - rootGrid.ActualHeight - 120) * 0.8 / 2, TimeSpan.FromSeconds(0.5));
            Storyboard.SetTarget(animationy, contentGrid);
            Storyboard.SetTargetProperty(animationy, new PropertyPath("(UIElement.RenderTransform).(TransformGroup.Children)[3].(TranslateTransform.Y)"));
            stb.Children.Add(animationy);

            DoubleAnimation scaleX = new DoubleAnimation(0, yScale, TimeSpan.FromSeconds(0.5));
            Storyboard.SetTarget(scaleX, contentGrid);
            Storyboard.SetTargetProperty(scaleX, new PropertyPath("(UIElement.RenderTransform).(TransformGroup.Children)[0].(ScaleTransform.ScaleX)"));
            stb.Children.Add(scaleX);

            DoubleAnimation scaleY = new DoubleAnimation(0, yScale, TimeSpan.FromSeconds(0.5));
            Storyboard.SetTarget(scaleY, contentGrid);
            Storyboard.SetTargetProperty(scaleY, new PropertyPath("(UIElement.RenderTransform).(TransformGroup.Children)[0].(ScaleTransform.ScaleY)"));
            stb.Children.Add(scaleY);

            ObjectAnimationUsingKeyFrames animation = new ObjectAnimationUsingKeyFrames();
            Storyboard.SetTarget(animation, vp3D);
            Storyboard.SetTargetProperty(animation, new PropertyPath("Visibility"));
            animation.KeyFrames.Add(new DiscreteObjectKeyFrame(Visibility.Visible, TimeSpan.FromSeconds(0.55)));
            animation.KeyFrames.Add(new DiscreteObjectKeyFrame(Visibility.Hidden, TimeSpan.FromSeconds(1.65)));
            stb.Children.Add(animation);

            DoubleAnimation opacity1 = new DoubleAnimation(0, TimeSpan.FromMilliseconds(100));
            opacity1.BeginTime = TimeSpan.FromSeconds(0.6);
            Storyboard.SetTarget(opacity1, BorderOut);
            Storyboard.SetTargetProperty(opacity1, new PropertyPath("Opacity"));
            stb.Children.Add(opacity1);

            ObjectAnimationUsingKeyFrames animation2 = new ObjectAnimationUsingKeyFrames();
            Storyboard.SetTarget(animation2, backGrid);
            Storyboard.SetTargetProperty(animation2, new PropertyPath("Visibility"));
            animation2.KeyFrames.Add(new DiscreteObjectKeyFrame(Visibility.Visible, TimeSpan.FromSeconds(1.1)));
            stb.Children.Add(animation2);

            DoubleAnimation opacity3 = new DoubleAnimation(1, TimeSpan.FromMilliseconds(100));
            opacity3.BeginTime = TimeSpan.FromSeconds(1.55);
            Storyboard.SetTarget(opacity3, backBorderOut);
            Storyboard.SetTargetProperty(opacity3, new PropertyPath("Opacity"));
            stb.Children.Add(opacity3);

            EventHandler handler = null;
            handler = delegate (object s, EventArgs re)
             {
                 stb.Completed -= handler;
                 contentBehavior.IsEnable = true;
             };

            stb.Completed += handler;

            stb.Begin();

            Point3DAnimation point3D = new Point3DAnimation(new Point3D(0, 0, 0.5), new Point3D(0, 0, 1.1), TimeSpan.FromSeconds(0.5));
            point3D.BeginTime = TimeSpan.FromSeconds(0.55);
            point3D.AutoReverse = true;
            point3D.DecelerationRatio = 0.3;
            camera.BeginAnimation(PerspectiveCamera.PositionProperty, point3D);

            DoubleAnimation rotateAnimation = new DoubleAnimation(0,180, TimeSpan.FromSeconds(1));
            rotateAnimation.BeginTime = TimeSpan.FromSeconds(0.55);
            rotateAnimation.AccelerationRatio = 0.3;
            rotateAnimation.DecelerationRatio = 0.3;
            rotate.BeginAnimation(AxisAngleRotation3D.AngleProperty, rotateAnimation);               
        }

        private void Stb_Completed(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
           

            var stb = new Storyboard();
            stb.SpeedRatio = 1.2;
            frontVisual.Visual = backBorderIn;
            backVisual.Visual = BorderIn;
            contentBehavior.IsEnable = false;

            Point3DAnimation point3D = new Point3DAnimation(new Point3D(0, 0, 0.5), new Point3D(0, 0, 1.1), TimeSpan.FromSeconds(0.5));
            //point3D.AutoReverse = true;
            point3D.DecelerationRatio = 0.3;
            camera.BeginAnimation(PerspectiveCamera.PositionProperty, point3D);

            DoubleAnimation rotateAnimation = new DoubleAnimation(0,-180, TimeSpan.FromSeconds(1));
            rotateAnimation.AccelerationRatio = 0.3;
            rotateAnimation.DecelerationRatio = 0.3;
            rotate.BeginAnimation(AxisAngleRotation3D.AngleProperty, rotateAnimation);

            ObjectAnimationUsingKeyFrames animation = new ObjectAnimationUsingKeyFrames();
            Storyboard.SetTarget(animation, vp3D);
            Storyboard.SetTargetProperty(animation, new PropertyPath("Visibility"));
            animation.KeyFrames.Add(new DiscreteObjectKeyFrame(Visibility.Visible, TimeSpan.FromSeconds(0)));
            animation.KeyFrames.Add(new DiscreteObjectKeyFrame(Visibility.Hidden, TimeSpan.FromSeconds(1.2)));
            stb.Children.Add(animation);

            ObjectAnimationUsingKeyFrames animation2 = new ObjectAnimationUsingKeyFrames();
            Storyboard.SetTarget(animation2, backGrid);
            Storyboard.SetTargetProperty(animation2, new PropertyPath("Visibility"));
            animation2.KeyFrames.Add(new DiscreteObjectKeyFrame(Visibility.Collapsed, TimeSpan.FromSeconds(1.15)));
            stb.Children.Add(animation2);

            DoubleAnimation opacity3 = new DoubleAnimation(0, TimeSpan.FromMilliseconds(100));
            opacity3.BeginTime = TimeSpan.FromSeconds(0.05);
            Storyboard.SetTarget(opacity3, backBorderOut);
            Storyboard.SetTargetProperty(opacity3, new PropertyPath("Opacity"));
            stb.Children.Add(opacity3);

            DoubleAnimation opacity1 = new DoubleAnimation(1, TimeSpan.FromMilliseconds(100));
            opacity1.BeginTime = TimeSpan.FromSeconds(1.05);
            Storyboard.SetTarget(opacity1, BorderOut);
            Storyboard.SetTargetProperty(opacity1, new PropertyPath("Opacity"));
            stb.Children.Add(opacity1);


            DoubleAnimation animationx = new DoubleAnimation(0, TimeSpan.FromSeconds(0.5));
            animationx.BeginTime = TimeSpan.FromSeconds(1);
            Storyboard.SetTarget(animationx, contentGrid);
            Storyboard.SetTargetProperty(animationx, new PropertyPath("(UIElement.RenderTransform).(TransformGroup.Children)[3].(TranslateTransform.X)"));
            stb.Children.Add(animationx);

            DoubleAnimation animationy = new DoubleAnimation(0, TimeSpan.FromSeconds(0.5));
            animationy.BeginTime = TimeSpan.FromSeconds(1); 
            Storyboard.SetTarget(animationy, contentGrid);
            Storyboard.SetTargetProperty(animationy, new PropertyPath("(UIElement.RenderTransform).(TransformGroup.Children)[3].(TranslateTransform.Y)"));
            stb.Children.Add(animationy);

            DoubleAnimation scaleX = new DoubleAnimation(1, TimeSpan.FromSeconds(0.5));
            scaleX.BeginTime = TimeSpan.FromSeconds(1); 
            Storyboard.SetTarget(scaleX, contentGrid);
            Storyboard.SetTargetProperty(scaleX, new PropertyPath("(UIElement.RenderTransform).(TransformGroup.Children)[0].(ScaleTransform.ScaleX)"));
            stb.Children.Add(scaleX);

            DoubleAnimation scaleY = new DoubleAnimation(1, TimeSpan.FromSeconds(0.5));
            scaleY.BeginTime = TimeSpan.FromSeconds(1); 
            Storyboard.SetTarget(scaleY, contentGrid);
            Storyboard.SetTargetProperty(scaleY, new PropertyPath("(UIElement.RenderTransform).(TransformGroup.Children)[0].(ScaleTransform.ScaleY)"));
            stb.Children.Add(scaleY);


            EventHandler handler = null;
            handler = delegate (object s, EventArgs re)
            {
                stb.Completed -= handler;
                contentBehavior.IsEnable = true;
            };

            stb.Completed += handler;

            stb.Begin();

            
        }

        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
        }
    }
}
