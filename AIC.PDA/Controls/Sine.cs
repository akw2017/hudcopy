using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace AIC.PDA.Controls
{
    public class Sine : Shape
    {

        public double AnimationWidth
        {
            get { return (double)GetValue(AnimationWidthProperty); }
            set { SetValue(AnimationWidthProperty, value); }
        }

        public static readonly DependencyProperty AnimationWidthProperty =
            DependencyProperty.Register("AnimationWidth", typeof(double), typeof(Sine), new PropertyMetadata(0.0));


        protected override Size MeasureOverride(Size constraint)
        {
            // we will size ourselves to fit the available space
            return constraint;
        }

        private Geometry GetGeometry()
        {
            var result = Geometry.Parse(String.Format("M{0},{1}C{2},{3},{4},{5},{6},{7}",
                                            0, ActualHeight * 0.5,
                                            ActualWidth * 0.5, -ActualHeight * 1.22,
                                            ActualWidth * 0.5, ActualHeight * 2.22,
                                            ActualWidth, ActualHeight * 0.5));

            //AnimationPath = Geometry.Parse(String.Format("M{0},{1}C{2},{3},{4},{5},{6},{7}",
            //                                -AnimationWidth / 2, 0,
            //                                ActualWidth * 0.5 - AnimationWidth / 2, -ActualHeight * 1.22 - ActualHeight * 0.5,
            //                                ActualWidth * 0.5 - AnimationWidth / 2, ActualHeight * 2.22 - ActualHeight * 0.5,
            //                                ActualWidth - AnimationWidth / 2, 0));
            return result;
        }

        protected override Geometry DefiningGeometry
        {
            get
            {
                return GetGeometry();
            }
        }

        public Geometry AnimationPath
        {
            get
            {
                if (ActualWidth == 0 || ActualHeight == 0) return null;
                return Geometry.Parse(String.Format("M{0},{1}C{2},{3},{4},{5},{6},{7}",
                                           -AnimationWidth / 2, 0,
                                           ActualWidth * 0.5 - AnimationWidth / 2, -ActualHeight * 1.22 - ActualHeight * 0.5,
                                           ActualWidth * 0.5 - AnimationWidth / 2, ActualHeight * 2.22 - ActualHeight * 0.5,
                                           ActualWidth - AnimationWidth / 2, 0));
            }
        }

    }
}
