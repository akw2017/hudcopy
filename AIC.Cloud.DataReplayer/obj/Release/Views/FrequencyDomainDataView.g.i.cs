﻿#pragma checksum "..\..\..\Views\FrequencyDomainDataView.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "EC4AB24A46765103CCE5943F9279EE58"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using AIC.Cloud.DataReplayer.Views;
using AIC.Cloud.Presentation;
using AIC.Cloud.Presentation.Actions;
using AIC.Cloud.Presentation.Behaviors;
using AIC.Cloud.Presentation.Controls;
using AIC.Cloud.Presentation.Converters;
using AIC.CoreType;
using AICMathTools;
using Arction.WPF.LightningChartUltimate;
using Arction.WPF.LightningChartUltimate.Annotations;
using Arction.WPF.LightningChartUltimate.Axes;
using Arction.WPF.LightningChartUltimate.ChartManager;
using Arction.WPF.LightningChartUltimate.DataBinding;
using Arction.WPF.LightningChartUltimate.EventMarkers;
using Arction.WPF.LightningChartUltimate.OverlayElements;
using Arction.WPF.LightningChartUltimate.Series3D;
using Arction.WPF.LightningChartUltimate.SeriesPolar;
using Arction.WPF.LightningChartUltimate.SeriesXY;
using Arction.WPF.LightningChartUltimate.Titles;
using Arction.WPF.LightningChartUltimate.TypeConverters;
using Arction.WPF.LightningChartUltimate.Views;
using Arction.WPF.LightningChartUltimate.Views.View3D;
using Arction.WPF.LightningChartUltimate.Views.ViewPie3D;
using Arction.WPF.LightningChartUltimate.Views.ViewPolar;
using Arction.WPF.LightningChartUltimate.Views.ViewXY;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms.Integration;
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


namespace AIC.Cloud.DataReplayer.Views {
    
    
    /// <summary>
    /// FrequencyDomainDataView
    /// </summary>
    public partial class FrequencyDomainDataView : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 108 "..\..\..\Views\FrequencyDomainDataView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox filterCheckBox;
        
        #line default
        #line hidden
        
        
        #line 109 "..\..\..\Views\FrequencyDomainDataView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Xceed.Wpf.Toolkit.DropDownButton dropDownButton;
        
        #line default
        #line hidden
        
        
        #line 118 "..\..\..\Views\FrequencyDomainDataView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RadioButton bandPassRb;
        
        #line default
        #line hidden
        
        
        #line 119 "..\..\..\Views\FrequencyDomainDataView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RadioButton highPassRb;
        
        #line default
        #line hidden
        
        
        #line 120 "..\..\..\Views\FrequencyDomainDataView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RadioButton lowPassRb;
        
        #line default
        #line hidden
        
        
        #line 236 "..\..\..\Views\FrequencyDomainDataView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox envelopeCheckBox;
        
        #line default
        #line hidden
        
        
        #line 237 "..\..\..\Views\FrequencyDomainDataView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox tffCheckBox;
        
        #line default
        #line hidden
        
        
        #line 238 "..\..\..\Views\FrequencyDomainDataView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox cepstrumCheckBox;
        
        #line default
        #line hidden
        
        
        #line 245 "..\..\..\Views\FrequencyDomainDataView.xaml"
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
            System.Uri resourceLocater = new System.Uri("/AIC.Cloud.DataReplayer;component/views/frequencydomaindataview.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Views\FrequencyDomainDataView.xaml"
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
            
            #line 108 "..\..\..\Views\FrequencyDomainDataView.xaml"
            this.filterCheckBox.Checked += new System.Windows.RoutedEventHandler(this.filterCheckBox_Checked);
            
            #line default
            #line hidden
            
            #line 108 "..\..\..\Views\FrequencyDomainDataView.xaml"
            this.filterCheckBox.Unchecked += new System.Windows.RoutedEventHandler(this.CheckBox_UnChecked);
            
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
            
            #line 236 "..\..\..\Views\FrequencyDomainDataView.xaml"
            this.envelopeCheckBox.Checked += new System.Windows.RoutedEventHandler(this.filterCheckBox_Checked);
            
            #line default
            #line hidden
            
            #line 236 "..\..\..\Views\FrequencyDomainDataView.xaml"
            this.envelopeCheckBox.Unchecked += new System.Windows.RoutedEventHandler(this.CheckBox_UnChecked);
            
            #line default
            #line hidden
            return;
            case 7:
            this.tffCheckBox = ((System.Windows.Controls.CheckBox)(target));
            
            #line 237 "..\..\..\Views\FrequencyDomainDataView.xaml"
            this.tffCheckBox.Checked += new System.Windows.RoutedEventHandler(this.filterCheckBox_Checked);
            
            #line default
            #line hidden
            
            #line 237 "..\..\..\Views\FrequencyDomainDataView.xaml"
            this.tffCheckBox.Unchecked += new System.Windows.RoutedEventHandler(this.CheckBox_UnChecked);
            
            #line default
            #line hidden
            return;
            case 8:
            this.cepstrumCheckBox = ((System.Windows.Controls.CheckBox)(target));
            
            #line 238 "..\..\..\Views\FrequencyDomainDataView.xaml"
            this.cepstrumCheckBox.Checked += new System.Windows.RoutedEventHandler(this.filterCheckBox_Checked);
            
            #line default
            #line hidden
            
            #line 238 "..\..\..\Views\FrequencyDomainDataView.xaml"
            this.cepstrumCheckBox.Unchecked += new System.Windows.RoutedEventHandler(this.CheckBox_UnChecked);
            
            #line default
            #line hidden
            return;
            case 9:
            
            #line 239 "..\..\..\Views\FrequencyDomainDataView.xaml"
            ((System.Windows.Controls.Primitives.RepeatButton)(target)).Click += new System.Windows.RoutedEventHandler(this.MovePrevious_Click);
            
            #line default
            #line hidden
            return;
            case 10:
            
            #line 240 "..\..\..\Views\FrequencyDomainDataView.xaml"
            ((System.Windows.Controls.Primitives.RepeatButton)(target)).Click += new System.Windows.RoutedEventHandler(this.MoveNext_Click);
            
            #line default
            #line hidden
            return;
            case 11:
            this.gridChart = ((System.Windows.Controls.Grid)(target));
            return;
            case 12:
            
            #line 248 "..\..\..\Views\FrequencyDomainDataView.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.ScreenshotButton_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

