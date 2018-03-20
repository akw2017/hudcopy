using AIC.Core;
using AIC.HistoryDataPage.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AIC.HistoryDataPage.Views
{
    /// <summary>
    /// Interaction logic for OffDesignConditionDataView.xaml
    /// </summary>
    public partial class OffDesignConditionDataView : DisposableUserControl
    {
        public OffDesignConditionDataView()
        {
            InitializeComponent();
        }

        //protected void ViewModel_Closed(object sender, EventArgs e)
        //{
        //    // Don't forget to clear chart grid child list.
        //    gridChart.Children.Clear();
        //    if (m_chart != null)
        //    {
        //        m_chart.Dispose();
        //        m_chart = null;
        //    }
        //    base.Dispose();
        //    base.GCCollect();
        //}
    }
}
