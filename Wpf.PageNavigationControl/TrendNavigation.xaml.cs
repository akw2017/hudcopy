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

namespace Wpf.PageNavigationControl
{
    public class TrendNavigationEventArgs
    {
        public int TimeSize { get; set; }
        public DateTime CurrectTime { get; set; }
        public TrendNavigationEventArgs(int size, DateTime time)
        {
            TimeSize = size;
            CurrectTime = time;
        }
    }
    /// <summary>
    /// Interaction logic for TrendNavigation.xaml
    /// </summary>
    public partial class TrendNavigation : UserControl
    {
        public TrendNavigation()
        {
            InitializeComponent();           
        }

        public static DependencyProperty TimeSizeListProperty;
        public static DependencyProperty TimeSizeProperty;
        public static DependencyProperty CurrentTimeProperty;
        public static DependencyProperty FirstTimeProperty;
        public static DependencyProperty SecondTimeProperty;
        public static DependencyProperty TotalPointProperty;
        public static DependencyProperty CanPageUpProperty;
        public static DependencyProperty CanPageDownProperty;
        public static DependencyProperty AutoPageDownProperty;

        public List<int> TimeSizeList
        {
            get
            {
                return (List<int>)GetValue(TimeSizeListProperty);
            }
            set
            {
                SetValue(TimeSizeListProperty, value);
            }
        }

        public bool AutoPageDown
        {
            get
            {
                return (bool)GetValue(AutoPageDownProperty);
            }
            set
            {
                SetValue(AutoPageDownProperty, value);
            }
        }

        public int TimeSize
        {
            get
            {
                return (int)GetValue(TimeSizeProperty);
            }
            set
            {
                SetValue(TimeSizeProperty, value);
            }
        }

        public DateTime CurrentTime
        {
            get
            {
                return (DateTime)GetValue(CurrentTimeProperty);
            }
            set
            {
                SetValue(CurrentTimeProperty, value);
            }
        }

        public DateTime FirstTime
        {
            get
            {
                return (DateTime)GetValue(FirstTimeProperty);
            }
            private set
            {
                SetValue(FirstTimeProperty, value);
            }
        }

        public DateTime SecondTime
        {
            get
            {
                return (DateTime)GetValue(SecondTimeProperty);
            }
            private set
            {
                SetValue(SecondTimeProperty, value);
            }
        }

        public int TotalPoint
        {
            get
            {
                return (int)GetValue(TotalPointProperty);
            }
            set
            {
                SetValue(TotalPointProperty, value);
            }
        }

        public bool CanPageUp;
        public bool CanPageDown;

        public static RoutedCommand PageUpCommand = new RoutedCommand();
        public static RoutedCommand PageGotoCommand = new RoutedCommand();
        public static RoutedCommand PageDownCommand = new RoutedCommand();
        public static RoutedCommand PageLastCommand = new RoutedCommand();
        public static RoutedCommand PageRefreshCommand = new RoutedCommand();
        public static RoutedCommand PageSetCommand = new RoutedCommand();

        public static void PageUpCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        public static void PageDownCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }      

        public static void PageUpExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            TrendNavigation pager = (TrendNavigation)sender;

            TrendNavigationEventArgs oldArgs = new TrendNavigationEventArgs(pager.TimeSize, pager.CurrentTime);
            pager.SetCurrentTimeUp();
            TrendNavigationEventArgs newArgs = new TrendNavigationEventArgs(pager.TimeSize, pager.CurrentTime);

            pager.OnCurrentTimeChanged(oldArgs, newArgs);
        }

        public static void PageGotoExecuted(object sender, ExecutedRoutedEventArgs e)
        {           
            TrendNavigation pager = (TrendNavigation)sender;  
            TrendNavigationEventArgs oldArgs = new TrendNavigationEventArgs(pager.oldsize, pager.oldtime);
            pager.SetCurrentTime(pager.CurrentTime);
            TrendNavigationEventArgs newArgs = new TrendNavigationEventArgs(pager.TimeSize, pager.CurrentTime);

            pager.OnCurrentTimeChanged(oldArgs, newArgs);
        }

        public static void PageDownExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            TrendNavigation pager = (TrendNavigation)sender;            

            TrendNavigationEventArgs oldArgs = new TrendNavigationEventArgs(pager.TimeSize, pager.CurrentTime);
            pager.SetCurrentTimeDown();
            TrendNavigationEventArgs newArgs = new TrendNavigationEventArgs(pager.TimeSize, pager.CurrentTime);

            pager.OnCurrentTimeChanged(oldArgs, newArgs);
        }

        public static void PageLastExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            TrendNavigation pager = (TrendNavigation)sender;

            TrendNavigationEventArgs oldArgs = new TrendNavigationEventArgs(pager.TimeSize, pager.CurrentTime);
            pager.SetCurrentTime(DateTime.Now);
            TrendNavigationEventArgs newArgs = new TrendNavigationEventArgs(pager.TimeSize, pager.CurrentTime);

            pager.OnCurrentTimeChanged(oldArgs, newArgs);
        }

        public static void PageRefreshExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            TrendNavigation pager = (TrendNavigation)sender;
         
            TrendNavigationEventArgs newArgs;            
            newArgs = new TrendNavigationEventArgs(pager.TimeSize, pager.CurrentTime);
            pager.OnCurrentTimeChanged(newArgs, newArgs);
        }

        public static void PageSetExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            TrendNavigation pager = (TrendNavigation)sender;
            pager.SetCurrentTime(pager.CurrentTime);
        }

        static TrendNavigation()
        {
            TimeSizeListProperty = DependencyProperty.Register("TimeSizeList", typeof(List<int>),
                typeof(TrendNavigation));

            TimeSizeProperty = DependencyProperty.Register("TimeSize", typeof(int),
                typeof(TrendNavigation),
                new FrameworkPropertyMetadata(60, new PropertyChangedCallback(OnTimeSizeChanged)));

            CurrentTimeProperty = DependencyProperty.Register("CurrentTime", typeof(DateTime),
                typeof(TrendNavigation),
                new FrameworkPropertyMetadata(DateTime.Now.AddMinutes(0 - 60 * 0.8)));

            TotalPointProperty = DependencyProperty.Register("TotalPoint", typeof(int),
              typeof(PageNavigation),
              new FrameworkPropertyMetadata(0));

            FirstTimeProperty = DependencyProperty.Register("FirstTime", typeof(DateTime),
                typeof(TrendNavigation),
                new FrameworkPropertyMetadata(DateTime.Now));

            SecondTimeProperty = DependencyProperty.Register("SecondTime", typeof(DateTime),
                typeof(TrendNavigation),
                new FrameworkPropertyMetadata(DateTime.Now));

            CanPageUpProperty = DependencyProperty.Register("CanPageUp", typeof(bool),
                typeof(TrendNavigation),
                new FrameworkPropertyMetadata(false));

            CanPageDownProperty = DependencyProperty.Register("CanPageDown", typeof(bool),
                typeof(TrendNavigation),
                new FrameworkPropertyMetadata(false));

            AutoPageDownProperty = DependencyProperty.Register("AutoPageDown", typeof(bool),
                typeof(TrendNavigation),
                new FrameworkPropertyMetadata(false));

            CommandManager.RegisterClassCommandBinding(typeof(TrendNavigation),
               new CommandBinding(PageUpCommand,
               PageUpExecuted, PageUpCanExecute));

            CommandManager.RegisterClassCommandBinding(typeof(TrendNavigation),
               new CommandBinding(PageGotoCommand,
               PageGotoExecuted));

            CommandManager.RegisterClassCommandBinding(typeof(TrendNavigation),
               new CommandBinding(PageDownCommand,
               PageDownExecuted, PageDownCanExecute));

            CommandManager.RegisterClassCommandBinding(typeof(TrendNavigation),
               new CommandBinding(PageLastCommand,
               PageLastExecuted, PageDownCanExecute));

            CommandManager.RegisterClassCommandBinding(typeof(TrendNavigation),
               new CommandBinding(PageRefreshCommand,
               PageRefreshExecuted));

            CommandManager.RegisterClassCommandBinding(typeof(TrendNavigation),
               new CommandBinding(PageSetCommand,
               PageSetExecuted));
        }       

        //页面大小改变
        private static void OnTimeSizeChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            TrendNavigation pager = (TrendNavigation)sender;
            int oldTimeSize = (int)e.OldValue;
            int newTimeSize = (int)e.NewValue;

            pager.SetTimeSize(newTimeSize);
            var oldArgs = new TrendNavigationEventArgs(oldTimeSize, pager.CurrentTime);
            var newArgs = new TrendNavigationEventArgs(newTimeSize, pager.CurrentTime);
            pager.OnCurrentTimeChanged(oldArgs, newArgs);

        }

        public static readonly RoutedEvent CurrentTimeChangedEvent =
          EventManager.RegisterRoutedEvent("CurrentTimeChanged", RoutingStrategy.Bubble,
              typeof(RoutedPropertyChangedEventHandler<TrendNavigationEventArgs>), typeof(TrendNavigation));

        public event RoutedPropertyChangedEventHandler<TrendNavigationEventArgs> CurrentTimeChanged
        {
            add { AddHandler(CurrentTimeChangedEvent, value); }
            remove { RemoveHandler(CurrentTimeChangedEvent, value); }
        }

        private void OnCurrentTimeChanged(TrendNavigationEventArgs timesize, TrendNavigationEventArgs currenttime)
        {
            RoutedPropertyChangedEventArgs<TrendNavigationEventArgs> args = new RoutedPropertyChangedEventArgs<TrendNavigationEventArgs>(timesize, currenttime);
            args.RoutedEvent = TrendNavigation.CurrentTimeChangedEvent;
            RaiseEvent(args);
        }

        public void SetTimeSize(int size)
        {
            TimeSize = size;
            RollPages();
        }

        public void SetCurrentTime(DateTime time)
        {
            CurrentTime = time;
            RollPages();
        }
        public void SetCurrentTimeUp()
        {
            CurrentTime = CurrentTime.AddMinutes(0 - TimeSize / 2);
            RollPages();
        }
        public void SetCurrentTimeDown()
        {
            CurrentTime = CurrentTime.AddMinutes(TimeSize / 2);
            RollPages();
        }
        private DateTime oldtime = new DateTime();
        private int oldsize;
        private void RollPages()
        {
            if (CurrentTime > DateTime.Now.AddMinutes(0 - TimeSize * 0.8))
            {
                CurrentTime = DateTime.Now.AddMinutes(0 - TimeSize * 0.8);
            }
            FirstTime = CurrentTime;
            SecondTime = CurrentTime.AddMinutes(TimeSize);
            if (SecondTime > DateTime.Now)
            {
                SecondTime = DateTime.Now;
            }
            oldtime = CurrentTime;
            oldsize = TimeSize;
        }
    }
}
