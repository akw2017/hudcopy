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
    public class PageNavigationEventArgs
    {
        public int PageSize { get; set; }
        public int CurrectPage { get; set; }
        public PageNavigationEventArgs(int size, int page)
        {
            PageSize = size;
            CurrectPage = page;
        }
    }
    /// <summary>
    /// Interaction logic for PageNavigation.xaml
    /// </summary>
    public partial class PageNavigation : UserControl
    {
        public PageNavigation()
        {
            InitializeComponent();           
        }

        public static DependencyProperty PageSizeListProperty;
        public static DependencyProperty PageSizeProperty;
        public static DependencyProperty CurrentPageProperty;
        public static DependencyProperty TotalPagesProperty;
        public static DependencyProperty FirstItemProperty;
        public static DependencyProperty SecondItemProperty;
        public static DependencyProperty TotalItemsProperty;
        public static DependencyProperty CanPageUpProperty;
        public static DependencyProperty CanPageDownProperty;
        public static DependencyProperty AutoPageDownProperty;

        public List<int> PageSizeList
        {
            get
            {
                return (List<int>)GetValue(PageSizeListProperty);
            }
            set
            {
                SetValue(PageSizeListProperty, value);
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

        public int PageSize
        {
            get
            {
                return (int)GetValue(PageSizeProperty);
            }
            set
            {
                SetValue(PageSizeProperty, value);
            }
        }

        public int CurrentPage
        {
            get
            {
                return (int)GetValue(CurrentPageProperty);
            }
            set
            {
                SetValue(CurrentPageProperty, value);
            }
        }

        public int TotalPages
        {
            get
            {
                return (int)GetValue(TotalPagesProperty);
            }
            private set
            {
                SetValue(TotalPagesProperty, value);
            }
        }

        public int FirstItem
        {
            get
            {
                return (int)GetValue(FirstItemProperty);
            }
            private set
            {
                SetValue(FirstItemProperty, value);
            }
        }

        public int SecondItem
        {
            get
            {
                return (int)GetValue(SecondItemProperty);
            }
            private set
            {
                SetValue(SecondItemProperty, value);
            }
        }

        public int TotalItems
        {
            get
            {
                return (int)GetValue(TotalItemsProperty);
            }
            set
            {
                SetValue(TotalItemsProperty, value);
            }
        }

        public bool CanPageUp;
        public bool CanPageDown;

        public static RoutedCommand PageFirstCommand = new RoutedCommand();
        public static RoutedCommand PageUpCommand = new RoutedCommand();
        public static RoutedCommand PageGotoCommand = new RoutedCommand();
        public static RoutedCommand PageDownCommand = new RoutedCommand();
        public static RoutedCommand PageLastCommand = new RoutedCommand();
        public static RoutedCommand PageRefreshCommand = new RoutedCommand();
        public static RoutedCommand PageSetCommand = new RoutedCommand();

        public static void PageUpCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            PageNavigation pager = (PageNavigation)sender;
            e.CanExecute = pager.CanPageUp;
        }

        public static void PageDownCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            PageNavigation pager = (PageNavigation)sender;
            e.CanExecute = pager.CanPageDown;
        }

        public static void PageFirstExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            PageNavigation pager = (PageNavigation)sender;

            PageNavigationEventArgs oldArgs = new PageNavigationEventArgs(pager.PageSize, pager.CurrentPage);
            pager.SetCurrentPage(1);
            PageNavigationEventArgs newArgs = new PageNavigationEventArgs(pager.PageSize, pager.CurrentPage);

            pager.OnCurrentPageChanged(oldArgs, newArgs);
        }

        public static void PageUpExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            PageNavigation pager = (PageNavigation)sender;

            PageNavigationEventArgs oldArgs = new PageNavigationEventArgs(pager.PageSize, pager.CurrentPage);
            pager.SetCurrentPageUp();
            PageNavigationEventArgs newArgs = new PageNavigationEventArgs(pager.PageSize, pager.CurrentPage);

            pager.OnCurrentPageChanged(oldArgs, newArgs);
        }

        public static void PageGotoExecuted(object sender, ExecutedRoutedEventArgs e)
        {           
            PageNavigation pager = (PageNavigation)sender;            
            int page = 0;
            try
            {
                page = Convert.ToInt32(e.Parameter);
            }
            catch
            {

            }
            PageNavigationEventArgs oldArgs = new PageNavigationEventArgs(pager.PageSize, pager.CurrentPage);
            pager.SetCurrentPage(page);
            PageNavigationEventArgs newArgs = new PageNavigationEventArgs(pager.PageSize, pager.CurrentPage);

            pager.OnCurrentPageChanged(oldArgs, newArgs);
        }

        public static void PageDownExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            PageNavigation pager = (PageNavigation)sender;            

            PageNavigationEventArgs oldArgs = new PageNavigationEventArgs(pager.PageSize, pager.CurrentPage);
            pager.SetCurrentPageDown();
            PageNavigationEventArgs newArgs = new PageNavigationEventArgs(pager.PageSize, pager.CurrentPage);

            pager.OnCurrentPageChanged(oldArgs, newArgs);
        }

        public static void PageLastExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            PageNavigation pager = (PageNavigation)sender;

            PageNavigationEventArgs oldArgs = new PageNavigationEventArgs(pager.PageSize, pager.CurrentPage);
            pager.SetCurrentPage(pager.TotalPages);
            PageNavigationEventArgs newArgs = new PageNavigationEventArgs(pager.PageSize, pager.CurrentPage);

            pager.OnCurrentPageChanged(oldArgs, newArgs);
        }

        public static void PageRefreshExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            PageNavigation pager = (PageNavigation)sender;
         
            PageNavigationEventArgs newArgs;            
            newArgs = new PageNavigationEventArgs(pager.PageSize, pager.CurrentPage);
            pager.OnCurrentPageChanged(newArgs, newArgs);
        }

        public static void PageSetExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            PageNavigation pager = (PageNavigation)sender;
            pager.SetCurrentPage(pager.CurrentPage);
        }

        static PageNavigation()
        {
            PageSizeListProperty = DependencyProperty.Register("PageSizeList", typeof(List<int>),
                typeof(PageNavigation));

            PageSizeProperty = DependencyProperty.Register("PageSize", typeof(int),
                typeof(PageNavigation),
                new FrameworkPropertyMetadata(100, new PropertyChangedCallback(OnPageSizeChanged)));

            CurrentPageProperty = DependencyProperty.Register("CurrentPage", typeof(int),
                typeof(PageNavigation),
                new FrameworkPropertyMetadata(1));

            TotalPagesProperty = DependencyProperty.Register("TotalPages", typeof(int),
                typeof(PageNavigation),
                new FrameworkPropertyMetadata(0));

            FirstItemProperty = DependencyProperty.Register("FirstItem", typeof(int),
                typeof(PageNavigation),
                new FrameworkPropertyMetadata(0));

            SecondItemProperty = DependencyProperty.Register("SecondItem", typeof(int),
                typeof(PageNavigation),
                new FrameworkPropertyMetadata(0));

            TotalItemsProperty = DependencyProperty.Register("TotalItems", typeof(int),
                typeof(PageNavigation),
                new FrameworkPropertyMetadata(0, new PropertyChangedCallback(OnTotalItemsChanged)));

            CanPageUpProperty = DependencyProperty.Register("CanPageUp", typeof(bool),
                typeof(PageNavigation),
                new FrameworkPropertyMetadata(false));

            CanPageDownProperty = DependencyProperty.Register("CanPageDown", typeof(bool),
                typeof(PageNavigation),
                new FrameworkPropertyMetadata(false));

            AutoPageDownProperty = DependencyProperty.Register("AutoPageDown", typeof(bool),
                typeof(PageNavigation),
                new FrameworkPropertyMetadata(false));

            CommandManager.RegisterClassCommandBinding(typeof(PageNavigation),
               new CommandBinding(PageFirstCommand,
               PageFirstExecuted, PageUpCanExecute));

            CommandManager.RegisterClassCommandBinding(typeof(PageNavigation),
               new CommandBinding(PageUpCommand,
               PageUpExecuted, PageUpCanExecute));

            CommandManager.RegisterClassCommandBinding(typeof(PageNavigation),
               new CommandBinding(PageGotoCommand,
               PageGotoExecuted));

            CommandManager.RegisterClassCommandBinding(typeof(PageNavigation),
               new CommandBinding(PageDownCommand,
               PageDownExecuted, PageDownCanExecute));

            CommandManager.RegisterClassCommandBinding(typeof(PageNavigation),
               new CommandBinding(PageLastCommand,
               PageLastExecuted, PageDownCanExecute));

            CommandManager.RegisterClassCommandBinding(typeof(PageNavigation),
               new CommandBinding(PageRefreshCommand,
               PageRefreshExecuted));

            CommandManager.RegisterClassCommandBinding(typeof(PageNavigation),
               new CommandBinding(PageSetCommand,
               PageSetExecuted));
        }       

        //页面大小改变
        private static void OnPageSizeChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            PageNavigation pager = (PageNavigation)sender;
            int oldPageSize = (int)e.OldValue;
            int newPageSize = (int)e.NewValue;

            pager.SetPageSize(newPageSize);
            var oldArgs = new PageNavigationEventArgs(oldPageSize, pager.CurrentPage);
            var newArgs = new PageNavigationEventArgs(newPageSize, pager.CurrentPage);
            pager.OnCurrentPageChanged(oldArgs, newArgs);

        }

        //设置总数，相当于初始化
        private static void OnTotalItemsChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            PageNavigation pager = (PageNavigation)sender;

            var oldArgs = new PageNavigationEventArgs(pager.PageSize, pager.CurrentPage);

            int oldTotal = (int)e.OldValue;
            int newTotal = (int)e.NewValue;
            pager.TotalItems = newTotal;
            //pager.CurrentPage = (pager.TotalItems == 0) ? 0 : 1;
            if (pager.TotalItems == 0)
            {
                pager.CurrentPage = 0;
                pager.TotalPages = 0;
                pager.RollPages();
            }
            else
            {
                pager.AllotPages(pager.AutoPageDown);
            }

            PageNavigationEventArgs newArgs = new PageNavigationEventArgs(pager.PageSize, pager.CurrentPage);
            pager.OnCurrentPageChanged(oldArgs, newArgs);
        }

        public static readonly RoutedEvent CurrentPageChangedEvent =
          EventManager.RegisterRoutedEvent("CurrentPageChanged", RoutingStrategy.Bubble,
              typeof(RoutedPropertyChangedEventHandler<PageNavigationEventArgs>), typeof(PageNavigation));

        public event RoutedPropertyChangedEventHandler<PageNavigationEventArgs> CurrentPageChanged
        {
            add { AddHandler(CurrentPageChangedEvent, value); }
            remove { RemoveHandler(CurrentPageChangedEvent, value); }
        }

        private void OnCurrentPageChanged(PageNavigationEventArgs pagesize, PageNavigationEventArgs currentpage)
        {
            RoutedPropertyChangedEventArgs<PageNavigationEventArgs> args = new RoutedPropertyChangedEventArgs<PageNavigationEventArgs>(pagesize, currentpage);
            args.RoutedEvent = PageNavigation.CurrentPageChangedEvent;
            RaiseEvent(args);
        }

        public void SetPageSize(int size)
        {
            PageSize = size;
            AllotPages(false);
        }

        public void SetCurrentPage(int page)
        {
            if (page > TotalPages)
            {
                page = TotalPages;
            }
            if (page <= 0 && TotalPages > 0)
            {
                page = 1;
            }
            CurrentPage = page;
            RollPages();
        }
        public void SetCurrentPageUp()
        {
            CurrentPage--;
            RollPages();
        }
        public void SetCurrentPageDown()
        {
            CurrentPage++;
            RollPages();
        }

        private void AllotPages(bool pagedown)
        {
            TotalPages = (int)Math.Ceiling((double)TotalItems / (double)PageSize);
            if (pagedown == true)
            {
                CurrentPage = TotalPages;
            }
            else if (CurrentPage == 0 && TotalPages > 0)
            {
                CurrentPage = 1;
            }
            else if (CurrentPage > TotalPages)
            {
                CurrentPage = TotalPages;
            }
            RollPages();
        }

        private void RollPages()
        {
            FirstItem = (CurrentPage == 0) ? 0 : ((CurrentPage - 1) * PageSize + 1);
            SecondItem = ((CurrentPage - 1) * PageSize + PageSize >= TotalItems) ? TotalItems : ((CurrentPage - 1) * PageSize + PageSize);
            CheckPaging();
        }

        private void CheckPaging()
        {
            CanPageUp = (CurrentPage <= 1) ? false : true;
            CanPageDown = (CurrentPage == TotalPages) ? false : true;
        }
    }
}
