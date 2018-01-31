using AIC.OnLineDataPage.ViewModels;
using AIC.OnLineDataPage.ViewModels.SubViewModels;
using System;
using System.Windows;
using System.Windows.Controls;

namespace AIC.OnLineDataPage.Views.SubViews
{
    public class ChartViewBase : UserControl
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
            IsVisibleChanged += ChartViewBase_IsVisibleChanged;               
        }

        private void ChartViewBase_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            Console.WriteLine(this.IsVisible.ToString());
        }

        public ChartViewModelBase ViewModel { get; set; }

        private void ChartViewBase_Loaded(object sender, RoutedEventArgs e)
        {
            if (DataContext is ChartViewModelBase)
            {
                ViewModel = DataContext as ChartViewModelBase;
                ViewModel.Opened += ViewModel_Opened;
                ViewModel.Open();
                ViewModel.SignalChanged += ViewModel_SignalChanged; 
                ViewModel_SignalChanged();
                ViewModel.Closed += ViewModel_Closed;
                ViewModel.Subscribe(UpdateChart);
            }
        }

        private void ChartViewBase_Unloaded(object sender, RoutedEventArgs e)
        {
            if (ViewModel != null)
            {
                ViewModel.Opened -= ViewModel_Opened;
                ViewModel.SignalChanged -= ViewModel_SignalChanged;
                ViewModel.Close();
                ViewModel.Closed -= ViewModel_Closed;

                ViewModel = null;
            }
        }

        protected virtual void ViewModel_SignalChanged()
        {
            
        }

        protected virtual void ViewModel_Closed(object sender, EventArgs e)
        {
        }

        protected virtual void ViewModel_Opened(object sender, EventArgs e)
        {

        }

        protected virtual void UpdateChart(object args)
        {

        }


    }
}
