﻿#pragma checksum "..\..\..\..\Views\SubViews\PowerSpectrumDensityDataView.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "21462859D8543AF58B61A8F533C3D8C017AA364C"
//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.42000
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

using AIC.Core;
using AIC.CoreType;
using AIC.HistoryDataPage.Views;
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
using System.Windows.Interactivity;
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
using Xceed.Wpf.Toolkit;
using Xceed.Wpf.Toolkit.Chromes;
using Xceed.Wpf.Toolkit.Core.Converters;
using Xceed.Wpf.Toolkit.Core.Input;
using Xceed.Wpf.Toolkit.Core.Media;
using Xceed.Wpf.Toolkit.Core.Utilities;
using Xceed.Wpf.Toolkit.Panels;
using Xceed.Wpf.Toolkit.Primitives;
using Xceed.Wpf.Toolkit.PropertyGrid;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;
using Xceed.Wpf.Toolkit.PropertyGrid.Commands;
using Xceed.Wpf.Toolkit.PropertyGrid.Converters;
using Xceed.Wpf.Toolkit.PropertyGrid.Editors;
using Xceed.Wpf.Toolkit.Zoombox;


namespace AIC.HistoryDataPage.Views {
    
    
    /// <summary>
    /// PowerSpectrumDensityDataView
    /// </summary>
    public partial class PowerSpectrumDensityDataView : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 80 "..\..\..\..\Views\SubViews\PowerSpectrumDensityDataView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox filterCheckBox;
        
        #line default
        #line hidden
        
        
        #line 81 "..\..\..\..\Views\SubViews\PowerSpectrumDensityDataView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Xceed.Wpf.Toolkit.DropDownButton dropDownButton;
        
        #line default
        #line hidden
        
        
        #line 89 "..\..\..\..\Views\SubViews\PowerSpectrumDensityDataView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RadioButton bandPassRb;
        
        #line default
        #line hidden
        
        
        #line 90 "..\..\..\..\Views\SubViews\PowerSpectrumDensityDataView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RadioButton highPassRb;
        
        #line default
        #line hidden
        
        
        #line 91 "..\..\..\..\Views\SubViews\PowerSpectrumDensityDataView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RadioButton lowPassRb;
        
        #line default
        #line hidden
        
        
        #line 207 "..\..\..\..\Views\SubViews\PowerSpectrumDensityDataView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox envelopeCheckBox;
        
        #line default
        #line hidden
        
        
        #line 208 "..\..\..\..\Views\SubViews\PowerSpectrumDensityDataView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox tffCheckBox;
        
        #line default
        #line hidden
        
        
        #line 209 "..\..\..\..\Views\SubViews\PowerSpectrumDensityDataView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox cepstrumCheckBox;
        
        #line default
        #line hidden
        
        
        #line 210 "..\..\..\..\Views\SubViews\PowerSpectrumDensityDataView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox dbCheckBox;
        
        #line default
        #line hidden
        
        
        #line 211 "..\..\..\..\Views\SubViews\PowerSpectrumDensityDataView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox showCheckBox;
        
        #line default
        #line hidden
        
        
        #line 214 "..\..\..\..\Views\SubViews\PowerSpectrumDensityDataView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid gridChart;
        
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
            System.Uri resourceLocater = new System.Uri("/AIC.HistoryDataPage;component/views/subviews/powerspectrumdensitydataview.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\Views\SubViews\PowerSpectrumDensityDataView.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
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
            this.filterCheckBox = ((System.Windows.Controls.CheckBox)(target));
            
            #line 80 "..\..\..\..\Views\SubViews\PowerSpectrumDensityDataView.xaml"
            this.filterCheckBox.Checked += new System.Windows.RoutedEventHandler(this.filterCheckBox_Checked);
            
            #line default
            #line hidden
            
            #line 80 "..\..\..\..\Views\SubViews\PowerSpectrumDensityDataView.xaml"
            this.filterCheckBox.Unchecked += new System.Windows.RoutedEventHandler(this.CheckBox_Unchecked);
            
            #line default
            #line hidden
            return;
            case 2:
            this.dropDownButton = ((Xceed.Wpf.Toolkit.DropDownButton)(target));
            return;
            case 3:
            this.bandPassRb = ((System.Windows.Controls.RadioButton)(target));
            return;
            case 4:
            this.highPassRb = ((System.Windows.Controls.RadioButton)(target));
            return;
            case 5:
            this.lowPassRb = ((System.Windows.Controls.RadioButton)(target));
            return;
            case 6:
            this.envelopeCheckBox = ((System.Windows.Controls.CheckBox)(target));
            
            #line 207 "..\..\..\..\Views\SubViews\PowerSpectrumDensityDataView.xaml"
            this.envelopeCheckBox.Checked += new System.Windows.RoutedEventHandler(this.filterCheckBox_Checked);
            
            #line default
            #line hidden
            
            #line 207 "..\..\..\..\Views\SubViews\PowerSpectrumDensityDataView.xaml"
            this.envelopeCheckBox.Unchecked += new System.Windows.RoutedEventHandler(this.CheckBox_Unchecked);
            
            #line default
            #line hidden
            return;
            case 7:
            this.tffCheckBox = ((System.Windows.Controls.CheckBox)(target));
            
            #line 208 "..\..\..\..\Views\SubViews\PowerSpectrumDensityDataView.xaml"
            this.tffCheckBox.Checked += new System.Windows.RoutedEventHandler(this.filterCheckBox_Checked);
            
            #line default
            #line hidden
            
            #line 208 "..\..\..\..\Views\SubViews\PowerSpectrumDensityDataView.xaml"
            this.tffCheckBox.Unchecked += new System.Windows.RoutedEventHandler(this.CheckBox_Unchecked);
            
            #line default
            #line hidden
            return;
            case 8:
            this.cepstrumCheckBox = ((System.Windows.Controls.CheckBox)(target));
            
            #line 209 "..\..\..\..\Views\SubViews\PowerSpectrumDensityDataView.xaml"
            this.cepstrumCheckBox.Checked += new System.Windows.RoutedEventHandler(this.filterCheckBox_Checked);
            
            #line default
            #line hidden
            
            #line 209 "..\..\..\..\Views\SubViews\PowerSpectrumDensityDataView.xaml"
            this.cepstrumCheckBox.Unchecked += new System.Windows.RoutedEventHandler(this.CheckBox_Unchecked);
            
            #line default
            #line hidden
            return;
            case 9:
            this.dbCheckBox = ((System.Windows.Controls.CheckBox)(target));
            
            #line 210 "..\..\..\..\Views\SubViews\PowerSpectrumDensityDataView.xaml"
            this.dbCheckBox.Checked += new System.Windows.RoutedEventHandler(this.dbCheckBox_Checked);
            
            #line default
            #line hidden
            
            #line 210 "..\..\..\..\Views\SubViews\PowerSpectrumDensityDataView.xaml"
            this.dbCheckBox.Unchecked += new System.Windows.RoutedEventHandler(this.dbCheckBox_UnChecked);
            
            #line default
            #line hidden
            return;
            case 10:
            this.showCheckBox = ((System.Windows.Controls.CheckBox)(target));
            return;
            case 11:
            this.gridChart = ((System.Windows.Controls.Grid)(target));
            return;
            case 12:
            
            #line 218 "..\..\..\..\Views\SubViews\PowerSpectrumDensityDataView.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.ScreenshotButton_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

