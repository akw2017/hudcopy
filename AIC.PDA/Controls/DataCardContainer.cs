using AIC.CoreType;
using AIC.Domain;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Animation;
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
    ///     <MyNamespace:DataCardContainer/>
    ///
    /// </summary>
    [TemplateVisualState(Name = DataCardContainer.StateCycleTrigger, GroupName = DataCardContainer.GroupTriggerState)]
    [TemplateVisualState(Name = DataCardContainer.StateAngleTrigger, GroupName = DataCardContainer.GroupTriggerState)]
    [TemplateVisualState(Name = DataCardContainer.StateRPMTrigger, GroupName = DataCardContainer.GroupTriggerState)]
    [TemplateVisualState(Name = DataCardContainer.StateAutoTrigger, GroupName = DataCardContainer.GroupTriggerState)]
    [TemplatePart(Name = "CycleTriggerPath", Type = typeof(Path))]
    [TemplatePart(Name = "AngleTriggerPath", Type = typeof(Path))]
    [TemplatePart(Name = "CycleEllipse", Type = typeof(Ellipse))]
    [TemplatePart(Name = "RPMEllipse", Type = typeof(Ellipse))]
    [TemplatePart(Name = "AutoTriggerEllipse", Type = typeof(Path))]
    [TemplatePart(Name = "AutoTriggerPath", Type = typeof(Path))]
    [TemplatePart(Name = "Root", Type = typeof(Grid))]
    [TemplatePart(Name = "TriggerChannelContainer", Type = typeof(ListBox))]
    [TemplatePart(Name = "rectangle", Type = typeof(Rectangle))]
    [TemplatePart(Name = "rectangle1", Type = typeof(Rectangle))]
    [TemplatePart(Name = "AutoTriggerDataRect", Type = typeof(Rectangle))]
    public class DataCardContainer : Control
    {
        internal const string GroupTriggerState = "TriggerStates";
        internal const string StateCycleTrigger = "CycleTrigger";
        internal const string StateAngleTrigger = "AngleTrigger";
        internal const string StateRPMTrigger = "RPMTrigger";
        internal const string StateAutoTrigger = "AutoTrigger";

        private Storyboard autoStb;
        private Storyboard cycleStb;
        private Storyboard angleStb;    
        private Storyboard dataTransformStb;
        private Storyboard dataTransformStb2;
        private Storyboard autoTriggerDataTransformStb;
        private Path cycleTriggerPath;
        private Path angleTriggerPath;     
        private Path autoTriggerPath;
        private Grid root;
        private ListBox triggerChannelContainer;
        private Rectangle dataRect;
        private Rectangle dataRect2;
        private Rectangle autoTriggerDataRect;
        private Ellipse cycleEllipse;
        private Ellipse rpmEllipse;
        private Path autoTriggerEllipse;

        private FromToPair fromTo;

        public static readonly DependencyProperty TriggerProperty = DependencyProperty.Register("Trigger", typeof(TriggerType), typeof(DataCardContainer), new PropertyMetadata(TriggerType.Auto, OnTriggerPropertyChanged));
        public static readonly DependencyProperty SelectedTriggerChannelProperty = DependencyProperty.Register("SelectedTriggerChannel", typeof(object), typeof(DataCardContainer), new PropertyMetadata(null, OnSelectedTriggerChannelChanged));



        public IEnumerable<TriggerChannel> TiggerChannelSource
        {
            get { return (IEnumerable<TriggerChannel>)GetValue(TiggerChannelSourceProperty); }
            set { SetValue(TiggerChannelSourceProperty, value); }
        }

        public static readonly DependencyProperty TiggerChannelSourceProperty =
            DependencyProperty.Register("TiggerChannelSource", typeof(IEnumerable<TriggerChannel>), typeof(DataCardContainer), new PropertyMetadata(null,OnTriggerChannelSourceChanged));

        private static void OnTriggerChannelSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var source = d as DataCardContainer;
            if(source.IsLoaded)
            {
                source.triggerChannelContainer.ItemsSource = source.TiggerChannelSource;
            }

        }

        static DataCardContainer()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(DataCardContainer), new FrameworkPropertyMetadata(typeof(DataCardContainer)));
        }

        public DataCardContainer()
        {
            fromTo = new FromToPair();
            Loaded += DataCardContainer_Loaded;
        }

        private void DataCardContainer_Loaded(object sender, RoutedEventArgs e)
        {
            autoStb = CreateAutoStoryboard();
            cycleStb = CreateCycleStoryboard();
            angleStb = CreateAngleStoryboard();
            dataTransformStb = CreateDataTransformStoryboard();
            dataTransformStb2 = CreateDataTransformStoryboard2();
            autoTriggerDataTransformStb = CreateAutoTriggerDataTransformStoryboard();

            autoStb.Completed += (s, arg) =>
            {
                if (fromTo.To == 360)
                {
                    fromTo.To = 0;
                }
                fromTo.From = fromTo.To;
                fromTo.To += 30;
                autoTriggerDataTransformStb.Begin();
            };
            autoTriggerDataTransformStb.Completed += (s, arg) =>
            {
                autoStb.Begin();
            };

            dataTransformStb.Completed += (s, arg) =>
            {
                if(Trigger != TriggerType.Auto)
                {
                    TriggerAnimation();
                }   
            };
            cycleStb.Completed += (s, arg) =>
            {
                dataTransformStb2.Begin();
            };
            angleStb.Completed += (s, arg) =>
            {
                dataTransformStb2.Begin();
            };
            dataTransformStb2.Completed += (s, arg) =>
            {
                if(SelectedTriggerChannel != null)
                {
                    dataTransformStb.Begin();
                }
            };
            GoToState();
            //TriggerAnimation();
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            cycleTriggerPath = GetTemplateChild("CycleTriggerPath") as Path;
            angleTriggerPath = GetTemplateChild("AngleTriggerPath") as Path;
            autoTriggerPath = GetTemplateChild("AutoTriggerPath") as Path;
            root = GetTemplateChild("Root") as Grid;
            triggerChannelContainer = GetTemplateChild("TriggerChannelContainer") as ListBox;
            dataRect = GetTemplateChild("rectangle") as Rectangle;
            dataRect2 = GetTemplateChild("rectangle1") as Rectangle;
            autoTriggerDataRect = GetTemplateChild("AutoTriggerDataRect") as Rectangle;
            cycleEllipse = GetTemplateChild("CycleEllipse") as Ellipse;
            rpmEllipse = GetTemplateChild("RPMEllipse") as Ellipse;
            autoTriggerEllipse = GetTemplateChild("AutoTriggerEllipse") as Path;

            cycleEllipse.MouseDown += (s, e) =>
            {
                Trigger = TriggerType.Cycle;
            };
            angleTriggerPath.MouseDown += (s, e) =>
            {
                Trigger = TriggerType.Angle;
            };
            rpmEllipse.MouseDown += (s, e) =>
            {
                Trigger = TriggerType.RPM;
            };
            autoTriggerEllipse.MouseDown += (s, e) =>
            {
                Trigger = TriggerType.Auto;
            };
            CreateBindings(triggerChannelContainer);
        }



        public TriggerType Trigger
        {
            get { return (TriggerType)GetValue(TriggerProperty); }
            set { SetValue(TriggerProperty, value); }
        }

        public object SelectedTriggerChannel
        {
            get { return (object)GetValue(SelectedTriggerChannelProperty); }
            set { SetValue(SelectedTriggerChannelProperty, value); }
        }

        private static void OnTriggerPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var source = (DataCardContainer)d;
            source.GoToState();
            if(source.IsLoaded)
            {
                source.StopAnimation();
                source.TriggerAnimation();
            } 
        }

        private void GoToState()
        {
            if (Trigger == TriggerType.Cycle)
            {
                VisualStateManager.GoToState(this, StateCycleTrigger, true);
            }
            else if (Trigger == TriggerType.Angle)
            {
                VisualStateManager.GoToState(this, StateAngleTrigger, true);
            }
            else if (Trigger == TriggerType.RPM)
            {
                VisualStateManager.GoToState(this, StateRPMTrigger, true);
            }
            else if (Trigger == TriggerType.Auto)
            {
                VisualStateManager.GoToState(this, StateAutoTrigger, true);
            }
        }

        private void TriggerAnimation()
        {
            if (Trigger == TriggerType.Cycle)
            {
                cycleStb.Begin();
            }
            else if (Trigger == TriggerType.Angle)
            {
                angleStb.Begin();
            }
            else if (Trigger == TriggerType.RPM)
            {
                dataTransformStb2.Begin();
            }
            else if (Trigger == TriggerType.Auto)
            {
                fromTo.From = 0;
                fromTo.To = 30;
                autoStb.Begin();
            }
        }

        private void StopAnimation()
        {
            autoTriggerDataTransformStb.Stop();
            dataTransformStb2.Stop();
            dataTransformStb.Stop();
            cycleStb.Stop();
            angleStb.Stop();
            autoStb.Stop();
        }

        private static void OnSelectedTriggerChannelChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var source = (DataCardContainer)d;
            if (source.Trigger != TriggerType.Auto)
            {
                source.StopAnimation();
                if (source.SelectedTriggerChannel != null)
                {
                    source.dataTransformStb.Begin();
                }
            }
        }

        private void CreateBindings(ListBox listBox)
        {
            var groupBinding = new Binding();
            groupBinding.Source = this;
            groupBinding.Mode = BindingMode.TwoWay;
            groupBinding.Path = new PropertyPath("SelectedTriggerChannel");
            BindingOperations.SetBinding(listBox, ListBox.SelectedItemProperty, groupBinding);
        }

        private Storyboard CreateAutoStoryboard()
        {
            Storyboard stb = new Storyboard();
            DoubleAnimation animation = new DoubleAnimation();

            var fromBinding = new Binding();
            fromBinding.Source = fromTo;
            fromBinding.Mode = BindingMode.TwoWay;
            fromBinding.Path = new PropertyPath("From");
            BindingOperations.SetBinding(animation, DoubleAnimation.FromProperty, fromBinding);

            var toBinding = new Binding();
            toBinding.Source = fromTo;
            toBinding.Mode = BindingMode.TwoWay;
            toBinding.Path = new PropertyPath("To");
            BindingOperations.SetBinding(animation, DoubleAnimation.ToProperty, toBinding);


            animation.Duration = TimeSpan.FromMilliseconds(1000);
            Storyboard.SetTarget(animation, autoTriggerPath);
            Storyboard.SetTargetProperty(animation, new PropertyPath("(UIElement.RenderTransform).(TransformGroup.Children)[2].(RotateTransform.Angle)"));
            stb.Children.Add(animation);
            return stb;
        }

        private Storyboard CreateCycleStoryboard()
        {
            Storyboard stb = new Storyboard();
           
            DoubleAnimation animation = new DoubleAnimation();
            animation.From = 0;
            animation.To = 360;
            animation.Duration = TimeSpan.FromMilliseconds(1000);
            Storyboard.SetTarget(animation, cycleTriggerPath);
            Storyboard.SetTargetProperty(animation, new PropertyPath("(UIElement.RenderTransform).(TransformGroup.Children)[2].(RotateTransform.Angle)"));
            stb.Children.Add(animation);

            return stb;
        }

        private Storyboard CreateAngleStoryboard()
        {
            Storyboard stb = new Storyboard();

            DoubleAnimation animation = new DoubleAnimation();
            animation.From = 0;
            animation.To = 30;
            animation.Duration = TimeSpan.FromMilliseconds(1000);
            Storyboard.SetTarget(animation, angleTriggerPath);
            Storyboard.SetTargetProperty(animation, new PropertyPath("(UIElement.RenderTransform).(TransformGroup.Children)[2].(RotateTransform.Angle)"));
            stb.Children.Add(animation);

            return stb;
        }

        private Storyboard CreateDataTransformStoryboard()
        {
            return CreatePathAnimation(dataRect, "M2.583,-0.084 L-71.801,0.916 L-71.801,203.416");
        }

        private Storyboard CreateDataTransformStoryboard2()
        {           
            return CreatePathAnimation(dataRect2, "M-0.23457633,-1.4999849 L-0.38401216,183.47027 L343.61602,184.19002");
        }

        private Storyboard CreateAutoTriggerDataTransformStoryboard()
        {
            return CreatePathAnimation(autoTriggerDataRect, "M-0.13626291,-1.4999889 L-0.333,142.69 L90.167,142.69");
        }

        private Storyboard CreatePathAnimation(DependencyObject target,string pathData)
        {
            Storyboard stb = new Storyboard();

            DoubleAnimationUsingPath animationX = new DoubleAnimationUsingPath();
            animationX.Source = PathAnimationSource.X;
            animationX.PathGeometry = PathGeometry.CreateFromGeometry(PathGeometry.Parse(pathData));
            animationX.Duration = TimeSpan.FromMilliseconds(1000);
            Storyboard.SetTarget(animationX, target);
            Storyboard.SetTargetProperty(animationX, new PropertyPath("(UIElement.RenderTransform).(TransformGroup.Children)[3].(TranslateTransform.X)"));
            stb.Children.Add(animationX);

            DoubleAnimationUsingPath animationY = new DoubleAnimationUsingPath();
            animationY.Source = PathAnimationSource.Y;
            animationY.PathGeometry = PathGeometry.CreateFromGeometry(PathGeometry.Parse(pathData)); ;
            animationY.Duration = TimeSpan.FromMilliseconds(1000);
            Storyboard.SetTarget(animationY, target);
            Storyboard.SetTargetProperty(animationY, new PropertyPath("(UIElement.RenderTransform).(TransformGroup.Children)[3].(TranslateTransform.Y)"));
            stb.Children.Add(animationY);

            DoubleAnimationUsingPath animationAngle = new DoubleAnimationUsingPath();
            animationAngle.Source = PathAnimationSource.Angle;
            animationAngle.PathGeometry = PathGeometry.CreateFromGeometry(PathGeometry.Parse(pathData));
            animationAngle.Duration = TimeSpan.FromMilliseconds(1000);
            Storyboard.SetTarget(animationAngle, target);
            Storyboard.SetTargetProperty(animationAngle, new PropertyPath("(UIElement.RenderTransform).(TransformGroup.Children)[2].(RotateTransform.Angle)"));
            stb.Children.Add(animationAngle);

            ObjectAnimationUsingKeyFrames animationVisiblity = new ObjectAnimationUsingKeyFrames();
            var keyFrame1 = new DiscreteObjectKeyFrame(Visibility.Visible, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0)));
            var keyFrame2 = new DiscreteObjectKeyFrame(Visibility.Collapsed, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(1)));
            animationVisiblity.KeyFrames.Add(keyFrame1);
            animationVisiblity.KeyFrames.Add(keyFrame2);
            Storyboard.SetTarget(animationVisiblity, target);
            Storyboard.SetTargetProperty(animationVisiblity, new PropertyPath("(UIElement.Visibility)"));
            stb.Children.Add(animationVisiblity);

            return stb;
        }
    }
}
