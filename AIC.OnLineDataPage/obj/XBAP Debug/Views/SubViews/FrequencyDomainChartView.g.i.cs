﻿#pragma checksum "..\..\..\..\Views\SubViews\FrequencyDomainChartView.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "B6E6EA0547E43D8767AA1EA94E7FF799"
//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.42000
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

using AIC.OnLineDataPage.Views.SubViews;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace AIC.OnLineDataPage.Views.SubViews {
    
    
    /// <summary>
    /// FrequencyDomainChartView
    /// </summary>
    public partial class FrequencyDomainChartView : AIC.OnLineDataPage.Views.SubViews.ChartViewBase, System.Windows.Markup.IComponentConnector {
        
        
        #line 8 "..\..\..\..\Views\SubViews\FrequencyDomainChartView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal AIC.OnLineDataPage.Views.SubViews.FrequencyDomainChartView timeDomainOnLineView;
        
        #line default
        #line hidden
        
        
        #line 61 "..\..\..\..\Views\SubViews\FrequencyDomainChartView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Media.Animation.BeginStoryboard OnLoaded2_BeginStoryboard;
        
        #line default
        #line hidden
        
        
        #line 69 "..\..\..\..\Views\SubViews\FrequencyDomainChartView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid gridChart;
        
        #line default
        #line hidden
        
        
        #line 72 "..\..\..\..\Views\SubViews\FrequencyDomainChartView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock txtValue;
        
        #line default
        #line hidden
        
        
        #line 74 "..\..\..\..\Views\SubViews\FrequencyDomainChartView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox scrollCheckBox;
        
        #line default
        #line hidden
        
        
        #line 75 "..\..\..\..\Views\SubViews\FrequencyDomainChartView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox fitViewCheckBox;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/AIC.OnLineDataPage;component/views/subviews/frequencydomainchartview.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\Views\SubViews\FrequencyDomainChartView.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal System.Delegate _CreateDelegate(System.Type delegateType, string handler) {
            return System.Delegate.CreateDelegate(delegateType, this, handler);
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.timeDomainOnLineView = ((AIC.OnLineDataPage.Views.SubViews.FrequencyDomainChartView)(target));
            return;
            case 2:
            this.OnLoaded2_BeginStoryboard = ((System.Windows.Media.Animation.BeginStoryboard)(target));
            return;
            case 3:
            this.gridChart = ((System.Windows.Controls.Grid)(target));
            return;
            case 4:
            this.txtValue = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 5:
            this.scrollCheckBox = ((System.Windows.Controls.CheckBox)(target));
            
            #line 74 "..\..\..\..\Views\SubViews\FrequencyDomainChartView.xaml"
            this.scrollCheckBox.Checked += new System.Windows.RoutedEventHandler(this.scrollCheckBox_Checked);
            
            #line default
            #line hidden
            
            #line 74 "..\..\..\..\Views\SubViews\FrequencyDomainChartView.xaml"
            this.scrollCheckBox.Unchecked += new System.Windows.RoutedEventHandler(this.scrollCheckBox_Unchecked);
            
            #line default
            #line hidden
            return;
            case 6:
            this.fitViewCheckBox = ((System.Windows.Controls.CheckBox)(target));
            return;
            case 7:
            
            #line 76 "..\..\..\..\Views\SubViews\FrequencyDomainChartView.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.ScreenshotButton_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

