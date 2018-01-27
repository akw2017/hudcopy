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

namespace Wpf.GridSelected
{
    public class RectangleGridEventArgs
    {
        public int Row { get; set; }
        public int Column { get; set; }
        public RectangleGridEventArgs(int row, int column)
        {
            Row = row;
            Column = column;
        }
    }
    /// <summary>
    /// RectangleGrid.xaml 的交互逻辑
    /// </summary>
    public partial class RectangleGrid : UserControl
    {
        public RectangleGrid()
        {
            InitializeComponent();
        }

        private void Rectangle_MouseEnter(object sender, MouseEventArgs e)
        {
            SolidColorBrush myBrush = new SolidColorBrush(System.Windows.Media.Color.FromArgb(0xff, 0x30, 0x50, 0x68));
            SolidColorBrush myBrush2 = new SolidColorBrush(System.Windows.Media.Color.FromArgb(0xff, 0xff, 0xff, 0xff));

            if (Application.Current.Resources["AccentColorBrush"] != null)
            {
                myBrush = new SolidColorBrush(((SolidColorBrush)Application.Current.Resources["AccentColorBrush"]).Color);
            }

            Rectangle rect = sender as Rectangle;
            #region
            switch(rect.Name)
            {
                case "rect1":
                    {
                        fillRectangle(1, 1);
                        break;
                    }
                case "rect2":
                    {
                        fillRectangle(1, 2);
                        break;
                    }
                case "rect3":
                    {
                        fillRectangle(1, 3);
                        break;
                    }
                case "rect4":
                    {
                        fillRectangle(1, 4);
                        break;
                    }
                case "rect5":
                    {
                        fillRectangle(2, 1);
                        break;
                    }
                case "rect6":
                    {
                        fillRectangle(2, 2);
                        break;
                    }
                case "rect7":
                    {
                        fillRectangle(2, 3);
                        break;
                    }
                case "rect8":
                    {
                        fillRectangle(2, 4);
                        break;
                    }
                case "rect9":
                    {
                        fillRectangle(3, 1);
                        break;
                    }
                case "rect10":
                    {
                        fillRectangle(3, 2);
                        break;
                    }
                case "rect11":
                    {
                        fillRectangle(3, 3);
                        break;
                    }
                case "rect12":
                    {
                        fillRectangle(3, 4);
                        break;
                    }
                case "rect13":
                    {
                        fillRectangle(4, 1);
                        break;
                    }
                case "rect14":
                    {
                        fillRectangle(4, 2);
                        break;
                    }
                case "rect15":
                    {
                        fillRectangle(4, 3);
                        break;
                    }
                case "rect16":
                    {
                        fillRectangle(4, 4);
                        break;
                    }
            }
            #endregion
        }

        private void fillRectangle(int row, int column)
        {
            SolidColorBrush myBrush = new SolidColorBrush(System.Windows.Media.Color.FromArgb(0xff, 0x30, 0x50, 0x68));
            SolidColorBrush myBrush2 = new SolidColorBrush(System.Windows.Media.Color.FromArgb(0xff, 0xff, 0xff, 0xff));

            if (Application.Current.Resources["AccentColorBrush"] != null)
            {
                myBrush = new SolidColorBrush(((SolidColorBrush)Application.Current.Resources["AccentColorBrush"]).Color);
            }

            if (row == 1 && column == 1)
            {
                rect1.Fill = myBrush;
                rect2.Fill = myBrush2;
                rect3.Fill = myBrush2;
                rect4.Fill = myBrush2;
                rect5.Fill = myBrush2;
                rect6.Fill = myBrush2;
                rect7.Fill = myBrush2;
                rect8.Fill = myBrush2;
                rect9.Fill = myBrush2;
                rect10.Fill = myBrush2;
                rect11.Fill = myBrush2;
                rect12.Fill = myBrush2;
                rect13.Fill = myBrush2;
                rect14.Fill = myBrush2;
                rect15.Fill = myBrush2;
                rect16.Fill = myBrush2;
            }
            else if (row == 1 && column == 2)
            {
                rect1.Fill = myBrush;
                rect2.Fill = myBrush;
                rect3.Fill = myBrush2;
                rect4.Fill = myBrush2;
                rect5.Fill = myBrush2;
                rect6.Fill = myBrush2;
                rect7.Fill = myBrush2;
                rect8.Fill = myBrush2;
                rect9.Fill = myBrush2;
                rect10.Fill = myBrush2;
                rect11.Fill = myBrush2;
                rect12.Fill = myBrush2;
                rect13.Fill = myBrush2;
                rect14.Fill = myBrush2;
                rect15.Fill = myBrush2;
                rect16.Fill = myBrush2;
            }
            else if (row == 1 && column == 3)
            {
                rect1.Fill = myBrush;
                rect2.Fill = myBrush;
                rect3.Fill = myBrush;
                rect4.Fill = myBrush2;
                rect5.Fill = myBrush2;
                rect6.Fill = myBrush2;
                rect7.Fill = myBrush2;
                rect8.Fill = myBrush2;
                rect9.Fill = myBrush2;
                rect10.Fill = myBrush2;
                rect11.Fill = myBrush2;
                rect12.Fill = myBrush2;
                rect13.Fill = myBrush2;
                rect14.Fill = myBrush2;
                rect15.Fill = myBrush2;
                rect16.Fill = myBrush2;
            }
            else if (row == 1 && column == 4)
            {
                rect1.Fill = myBrush;
                rect2.Fill = myBrush;
                rect3.Fill = myBrush;
                rect4.Fill = myBrush;
                rect5.Fill = myBrush2;
                rect6.Fill = myBrush2;
                rect7.Fill = myBrush2;
                rect8.Fill = myBrush2;
                rect9.Fill = myBrush2;
                rect10.Fill = myBrush2;
                rect11.Fill = myBrush2;
                rect12.Fill = myBrush2;
                rect13.Fill = myBrush2;
                rect14.Fill = myBrush2;
                rect15.Fill = myBrush2;
                rect16.Fill = myBrush2;
            }
            else if (row == 2 && column == 1)
            {
                rect1.Fill = myBrush;
                rect2.Fill = myBrush2;
                rect3.Fill = myBrush2;
                rect4.Fill = myBrush2;
                rect5.Fill = myBrush;
                rect6.Fill = myBrush2;
                rect7.Fill = myBrush2;
                rect8.Fill = myBrush2;
                rect9.Fill = myBrush2;
                rect10.Fill = myBrush2;
                rect11.Fill = myBrush2;
                rect12.Fill = myBrush2;
                rect13.Fill = myBrush2;
                rect14.Fill = myBrush2;
                rect15.Fill = myBrush2;
                rect16.Fill = myBrush2;
            }
            else if (row == 2 && column == 2)
            {
                rect1.Fill = myBrush;
                rect2.Fill = myBrush;
                rect3.Fill = myBrush2;
                rect4.Fill = myBrush2;
                rect5.Fill = myBrush;
                rect6.Fill = myBrush;
                rect7.Fill = myBrush2;
                rect8.Fill = myBrush2;
                rect9.Fill = myBrush2;
                rect10.Fill = myBrush2;
                rect11.Fill = myBrush2;
                rect12.Fill = myBrush2;
                rect13.Fill = myBrush2;
                rect14.Fill = myBrush2;
                rect15.Fill = myBrush2;
                rect16.Fill = myBrush2;
            }
            else if (row == 2 && column == 3)
            {
                rect1.Fill = myBrush;
                rect2.Fill = myBrush;
                rect3.Fill = myBrush;
                rect4.Fill = myBrush2;
                rect5.Fill = myBrush;
                rect6.Fill = myBrush;
                rect7.Fill = myBrush;
                rect8.Fill = myBrush2;
                rect9.Fill = myBrush2;
                rect10.Fill = myBrush2;
                rect11.Fill = myBrush2;
                rect12.Fill = myBrush2;
                rect13.Fill = myBrush2;
                rect14.Fill = myBrush2;
                rect15.Fill = myBrush2;
                rect16.Fill = myBrush2;
            }
            else if (row == 2 && column == 4)
            {
                rect1.Fill = myBrush;
                rect2.Fill = myBrush;
                rect3.Fill = myBrush;
                rect4.Fill = myBrush;
                rect5.Fill = myBrush;
                rect6.Fill = myBrush;
                rect7.Fill = myBrush;
                rect8.Fill = myBrush;
                rect9.Fill = myBrush2;
                rect10.Fill = myBrush2;
                rect11.Fill = myBrush2;
                rect12.Fill = myBrush2;
                rect13.Fill = myBrush2;
                rect14.Fill = myBrush2;
                rect15.Fill = myBrush2;
                rect16.Fill = myBrush2;
            }
            else if (row == 3 && column == 1)
            {
                rect1.Fill = myBrush;
                rect2.Fill = myBrush2;
                rect3.Fill = myBrush2;
                rect4.Fill = myBrush2;
                rect5.Fill = myBrush;
                rect6.Fill = myBrush2;
                rect7.Fill = myBrush2;
                rect8.Fill = myBrush2;
                rect9.Fill = myBrush;
                rect10.Fill = myBrush2;
                rect11.Fill = myBrush2;
                rect12.Fill = myBrush2;
                rect13.Fill = myBrush2;
                rect14.Fill = myBrush2;
                rect15.Fill = myBrush2;
                rect16.Fill = myBrush2;
            }
            else if (row == 3 && column == 2)
            {
                rect1.Fill = myBrush;
                rect2.Fill = myBrush;
                rect3.Fill = myBrush2;
                rect4.Fill = myBrush2;
                rect5.Fill = myBrush;
                rect6.Fill = myBrush;
                rect7.Fill = myBrush2;
                rect8.Fill = myBrush2;
                rect9.Fill = myBrush;
                rect10.Fill = myBrush;
                rect11.Fill = myBrush2;
                rect12.Fill = myBrush2;
                rect13.Fill = myBrush2;
                rect14.Fill = myBrush2;
                rect15.Fill = myBrush2;
                rect16.Fill = myBrush2;
            }
            else if (row == 3 && column == 3)
            {
                rect1.Fill = myBrush;
                rect2.Fill = myBrush;
                rect3.Fill = myBrush;
                rect4.Fill = myBrush2;
                rect5.Fill = myBrush;
                rect6.Fill = myBrush;
                rect7.Fill = myBrush;
                rect8.Fill = myBrush2;
                rect9.Fill = myBrush;
                rect10.Fill = myBrush;
                rect11.Fill = myBrush;
                rect12.Fill = myBrush2;
                rect13.Fill = myBrush2;
                rect14.Fill = myBrush2;
                rect15.Fill = myBrush2;
                rect16.Fill = myBrush2;
            }
            else if (row == 3 && column == 4)
            {
                rect1.Fill = myBrush;
                rect2.Fill = myBrush;
                rect3.Fill = myBrush;
                rect4.Fill = myBrush;
                rect5.Fill = myBrush;
                rect6.Fill = myBrush;
                rect7.Fill = myBrush;
                rect8.Fill = myBrush;
                rect9.Fill = myBrush;
                rect10.Fill = myBrush;
                rect11.Fill = myBrush;
                rect12.Fill = myBrush;
                rect13.Fill = myBrush2;
                rect14.Fill = myBrush2;
                rect15.Fill = myBrush2;
                rect16.Fill = myBrush2;
            }
            else if (row == 4 && column == 1)
            {
                rect1.Fill = myBrush;
                rect2.Fill = myBrush2;
                rect3.Fill = myBrush2;
                rect4.Fill = myBrush2;
                rect5.Fill = myBrush;
                rect6.Fill = myBrush2;
                rect7.Fill = myBrush2;
                rect8.Fill = myBrush2;
                rect9.Fill = myBrush;
                rect10.Fill = myBrush2;
                rect11.Fill = myBrush2;
                rect12.Fill = myBrush2;
                rect13.Fill = myBrush;
                rect14.Fill = myBrush2;
                rect15.Fill = myBrush2;
                rect16.Fill = myBrush2;
            }
            else if (row == 4 && column == 2)
            {
                rect1.Fill = myBrush;
                rect2.Fill = myBrush;
                rect3.Fill = myBrush2;
                rect4.Fill = myBrush2;
                rect5.Fill = myBrush;
                rect6.Fill = myBrush;
                rect7.Fill = myBrush2;
                rect8.Fill = myBrush2;
                rect9.Fill = myBrush;
                rect10.Fill = myBrush;
                rect11.Fill = myBrush2;
                rect12.Fill = myBrush2;
                rect13.Fill = myBrush;
                rect14.Fill = myBrush;
                rect15.Fill = myBrush2;
                rect16.Fill = myBrush2;
            }
            else if (row == 4 && column == 3)
            {
                rect1.Fill = myBrush;
                rect2.Fill = myBrush;
                rect3.Fill = myBrush;
                rect4.Fill = myBrush2;
                rect5.Fill = myBrush;
                rect6.Fill = myBrush;
                rect7.Fill = myBrush;
                rect8.Fill = myBrush2;
                rect9.Fill = myBrush;
                rect10.Fill = myBrush;
                rect11.Fill = myBrush;
                rect12.Fill = myBrush2;
                rect13.Fill = myBrush;
                rect14.Fill = myBrush;
                rect15.Fill = myBrush;
                rect16.Fill = myBrush2;
            }
            else if (row == 4 && column == 4)
            {
                rect1.Fill = myBrush;
                rect2.Fill = myBrush;
                rect3.Fill = myBrush;
                rect4.Fill = myBrush;
                rect5.Fill = myBrush;
                rect6.Fill = myBrush;
                rect7.Fill = myBrush;
                rect8.Fill = myBrush;
                rect9.Fill = myBrush;
                rect10.Fill = myBrush;
                rect11.Fill = myBrush;
                rect12.Fill = myBrush;
                rect13.Fill = myBrush;
                rect14.Fill = myBrush;
                rect15.Fill = myBrush;
                rect16.Fill = myBrush;
            }           
            else
            {
                rect1.Fill = myBrush2;
                rect2.Fill = myBrush2;
                rect3.Fill = myBrush2;
                rect4.Fill = myBrush2;
                rect5.Fill = myBrush2;
                rect6.Fill = myBrush2;
                rect7.Fill = myBrush2;
                rect8.Fill = myBrush2;
                rect9.Fill = myBrush2;
                rect10.Fill = myBrush2;
                rect11.Fill = myBrush2;
                rect12.Fill = myBrush2;
                rect13.Fill = myBrush2;
                rect14.Fill = myBrush2;
                rect15.Fill = myBrush2;
                rect16.Fill = myBrush2;
            }
        }

        private void Rectangle_MouseLeave(object sender, MouseEventArgs e)
        {
        }

        private void Rectangle_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Rectangle rect = sender as Rectangle;
            switch (rect.Name)
            {
                case "rect1":
                    {
                        this.SelectRow = 1;
                        this.SelectColumn = 1;
                        this.Select = new RectangleGridEventArgs(1, 1);
                        break;
                    }
                case "rect2":
                    {
                        this.SelectRow = 1;
                        this.SelectColumn = 2;
                        this.Select = new RectangleGridEventArgs(1, 2);
                        break;
                    }
                case "rect3":
                    {
                        this.SelectRow = 1;
                        this.SelectColumn = 3;
                        this.Select = new RectangleGridEventArgs(1, 3);
                        break;
                    }
                case "rect4":
                    {
                        this.SelectRow = 1;
                        this.SelectColumn = 4;
                        this.Select = new RectangleGridEventArgs(1, 4);
                        break;
                    }
                case "rect5":
                    {
                        this.SelectRow = 2;
                        this.SelectColumn = 1;
                        this.Select = new RectangleGridEventArgs(2, 1);
                        break;
                    }
                case "rect6":
                    {
                        this.SelectRow = 2;
                        this.SelectColumn = 2;
                        this.Select = new RectangleGridEventArgs(2, 2);
                        break;
                    }
                case "rect7":
                    {
                        this.SelectRow = 2;
                        this.SelectColumn = 3;
                        this.Select = new RectangleGridEventArgs(2, 3);
                        break;
                    }
                case "rect8":
                    {
                        this.SelectRow = 2;
                        this.SelectColumn = 4;
                        this.Select = new RectangleGridEventArgs(2, 4);
                        break;
                    }
                case "rect9":
                    {
                        this.SelectRow = 3;
                        this.SelectColumn = 1;
                        this.Select = new RectangleGridEventArgs(3, 1);
                        break;
                    }
                case "rect10":
                    {
                        this.SelectRow = 3;
                        this.SelectColumn = 2;
                        this.Select = new RectangleGridEventArgs(3, 2);
                        break;
                    }
                case "rect11":
                    {
                        this.SelectRow = 3;
                        this.SelectColumn = 3;
                        this.Select = new RectangleGridEventArgs(3, 3);
                        break;
                    }
                case "rect12":
                    {
                        this.SelectRow = 3;
                        this.SelectColumn = 4;
                        this.Select = new RectangleGridEventArgs(3, 4);
                        break;
                    }
                case "rect13":
                    {
                        this.SelectRow = 4;
                        this.SelectColumn = 1;
                        this.Select = new RectangleGridEventArgs(4, 1);
                        break;
                    }
                case "rect14":
                    {
                        this.SelectRow = 4;
                        this.SelectColumn = 2;
                        this.Select = new RectangleGridEventArgs(4, 2);
                        break;
                    }
                case "rect15":
                    {
                        this.SelectRow = 4;
                        this.SelectColumn = 3;
                        this.Select = new RectangleGridEventArgs(4, 3);
                        break;
                    }
                case "rect16":
                    {
                        this.SelectRow = 4;
                        this.SelectColumn = 4;
                        this.Select = new RectangleGridEventArgs(4, 4);
                        break;
                    }
            }
        }

        private static DependencyProperty SelectRowProperty;
        public int SelectRow
        {
            get
            {
                return (int)GetValue(SelectRowProperty);
            }
            set
            {
                SetValue(SelectRowProperty, value);
            }
        }

        private static DependencyProperty SelectColumnProperty;
        public int SelectColumn
        {
            get
            {
                return (int)GetValue(SelectColumnProperty);
            }
            set
            {
                SetValue(SelectColumnProperty, value);
            }
        }

        private static DependencyProperty SelectProperty;
        public RectangleGridEventArgs Select
        {
            get
            {
                return (RectangleGridEventArgs)GetValue(SelectProperty);
            }
            set
            {
                SetValue(SelectProperty, value);
            }
        }


        static RectangleGrid()
        {
            SelectColumnProperty = DependencyProperty.Register("SelectColumn", typeof(int),
              typeof(RectangleGrid),
              new FrameworkPropertyMetadata(0));
            SelectRowProperty = DependencyProperty.Register("SelectRow", typeof(int),
              typeof(RectangleGrid),
              new FrameworkPropertyMetadata(0));
            SelectProperty = DependencyProperty.Register("Select", typeof(RectangleGridEventArgs),
             typeof(RectangleGrid),
             new FrameworkPropertyMetadata(null, new PropertyChangedCallback(OnSelectChanged)));
        }

        private static void OnSelectRowChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            RectangleGrid grid = (RectangleGrid)sender;

            int oldRow = (int)e.OldValue;
            int newRow = (int)e.NewValue;
            if (oldRow == 0 || newRow == 0)
            {
                grid.fillRectangle((int)e.NewValue, grid.SelectColumn);
            }

            //避免两次触发
            var oldArgs = new RectangleGridEventArgs(oldRow, grid.SelectColumn);
            var newArgs = new RectangleGridEventArgs(newRow, grid.SelectColumn);
            grid.OnSelectColumnChanged(oldArgs, newArgs);
        }

        private static void OnSelectColumnChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            RectangleGrid grid = (RectangleGrid)sender;

            int oldColumn = (int)e.OldValue;
            int newColumn = (int)e.NewValue;

            grid.fillRectangle(grid.SelectRow, (int)e.NewValue);

            var oldArgs = new RectangleGridEventArgs(grid.SelectRow, oldColumn);
            var newArgs = new RectangleGridEventArgs(grid.SelectRow, newColumn);
            grid.OnSelectColumnChanged(oldArgs, newArgs);
        }

        private static void OnSelectChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            RectangleGrid grid = (RectangleGrid)sender;

            RectangleGridEventArgs oldArgs = e.OldValue as RectangleGridEventArgs;
            RectangleGridEventArgs newArgs = e.NewValue as RectangleGridEventArgs;

            grid.fillRectangle(newArgs.Row, newArgs.Column);

            grid.OnSelectColumnChanged(oldArgs, newArgs);
        }

        public static readonly RoutedEvent SelectRowColumnChangedEvent =
          EventManager.RegisterRoutedEvent("SelectRowColumnChanged", RoutingStrategy.Bubble,
              typeof(RoutedPropertyChangedEventHandler<RectangleGridEventArgs>), typeof(RectangleGrid));

        public event RoutedPropertyChangedEventHandler<RectangleGridEventArgs> SelectRowColumnChanged
        {
            add { AddHandler(SelectRowColumnChangedEvent, value); }
            remove { RemoveHandler(SelectRowColumnChangedEvent, value); }
        }

        private void OnSelectColumnChanged(RectangleGridEventArgs oldargs, RectangleGridEventArgs newargs)
        {
            RoutedPropertyChangedEventArgs<RectangleGridEventArgs> args = new RoutedPropertyChangedEventArgs<RectangleGridEventArgs>(oldargs, newargs);
            args.RoutedEvent = RectangleGrid.SelectRowColumnChangedEvent;
            RaiseEvent(args);
        }


    }
}
