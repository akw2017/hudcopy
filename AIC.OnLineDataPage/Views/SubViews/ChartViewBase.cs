using AIC.Core;
using AIC.OnLineDataPage.ViewModels;
using AIC.OnLineDataPage.ViewModels.SubViewModels;
using System;
using System.Windows;
using System.Windows.Controls;

namespace AIC.OnLineDataPage.Views.SubViews
{
    public class ChartViewBase : DisposableUserControl
    {
        //private bool initialized = false;
        static ChartViewBase()
        {
            //DefaultStyleKeyProperty.OverrideMetadata(typeof(ChartViewBase), new FrameworkPropertyMetadata(typeof(ChartViewBase)));
        }
       
        public ChartViewBase()
        {
            Loaded += ChartViewBase_Loaded;
            Unloaded += ChartViewBase_Unloaded;
        }

        public ChartViewModelBase ViewModel { get; set; }

        private bool isloaded = false;//因为人工加载的添加，必须避免反复加载
        protected virtual void ChartViewBase_Loaded(object sender, RoutedEventArgs e)
        {
            if (isloaded == true)
            {
                return;
            }
            isloaded = true;

            if (DataContext is ChartViewModelBase)
            {
                ViewModel = DataContext as ChartViewModelBase;
                ViewModel.ProcessorLoaded();
                ViewModel.SignalChanged -= ViewModel_SignalChanged;
                ViewModel.SignalChanged += ViewModel_SignalChanged; 
                //ViewModel_SignalChanged(); 
                ViewModel.Subscribe(UpdateChart);
                ViewModel.Closed -= ViewModel_Closed;
                ViewModel.Closed += ViewModel_Closed;
                ViewModel.Disposed -= ViewModel_Disposed;
                ViewModel.Disposed += ViewModel_Disposed;
                ViewModel.HandUnloaded -= ViewModel_HandUnloaded;
                ViewModel.HandUnloaded += ViewModel_HandUnloaded;
                ViewModel.HandLoaded -= ViewModel_HandLoaded;
                ViewModel.HandLoaded += ViewModel_HandLoaded;
            }
        }

        protected virtual void ChartViewBase_Unloaded(object sender, RoutedEventArgs e)
        {
            if (isloaded == false)
            {
                return;
            }
            isloaded = false;

            if (ViewModel != null)
            {
                //ViewModel.SignalChanged -= ViewModel_SignalChanged;
                ViewModel.ProcessorUnloaded();
                //ViewModel.Closed -= ViewModel_Closed;
                ViewModel = null;
            }
        }

        protected virtual void ViewModel_HandUnloaded(object sender, EventArgs e)
        {
            ChartViewBase_Unloaded(null, null);
        }

        protected virtual void ViewModel_HandLoaded(object sender, EventArgs e)
        {
            ChartViewBase_Loaded(null, null);
        }

        protected virtual void ViewModel_SignalChanged()
        {
            
        }

        protected virtual void ViewModel_Closed(object sender, EventArgs e)
        {
            
        }

        protected virtual void ViewModel_Disposed(object sender, EventArgs e)
        {

        }

        protected virtual void UpdateChart(object args)
        {

        }
    }
}
