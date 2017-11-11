using AIC.CoreType;
using AIC.OnLineDataPage.ViewModels;
using BolapanControl.ItemsFilter;
using BolapanControl.ItemsFilter.Initializer;
using BolapanControl.ItemsFilter.Model;
using BolapanControl.ItemsFilter.View;
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

namespace AIC.OnLineDataPage.Views
{
    /// <summary>
    /// Interaction logic for MultiValueFilterView.xaml
    /// </summary>
    public partial class AlarmGradeFilterView : MultiValueFilterView
    {
        public AlarmGradeFilterView()
        {
            InitializeComponent();
        }

        private void MultiValueFilterView_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            OnlineDataListViewModel vm = e.NewValue as OnlineDataListViewModel;
            // Define Filter that must be use.
            EnumFilterInitializer initializer = new EnumFilterInitializer();
            // Get FilterPresenter that connected to default collection view for Workspace.This.Products collection.
            FilterPresenter fpr = FilterPresenter.TryGet(vm.Signals);

            // Get EqualFilter that use Category item property.
            EnumFilter<AlarmGrade> filter = ((EnumFilter<AlarmGrade>)(fpr.TryGetFilter("AlarmGrade", initializer)));
            // Use instance of EqualFilter as Model.
            Model = filter;

            //OnLineMonitorViewModel vm = e.NewValue as OnLineMonitorViewModel;
            //// Define Filter that must be use.
            //EqualFilterInitializer initializer = new EqualFilterInitializer();
            //// Get FilterPresenter that connected to default collection view for Workspace.This.Products collection.
            //FilterPresenter fpr = FilterPresenter.TryGet(vm.Signals);
            //// Get EqualFilter that use Category item property.
            //EqualFilter filter = ((EqualFilter)(fpr.TryGetFilter("GroupCOName", initializer)));
            //// Use instance of EqualFilter as Model.
            //Model = filter;
        }
    }
}
