﻿#pragma checksum "..\..\..\Views\HistoryDataListView - Copy.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "7DFDFFD94544B9A95AB198F2662243ED"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using AIC.Core;
using AIC.Core.ExCommand;
using AIC.Core.OrganizationModels;
using AIC.CoreType;
using BolapanControl.ItemsFilter;
using BolapanControl.ItemsFilter.Initializer;
using BolapanControl.ItemsFilter.Model;
using BolapanControl.ItemsFilter.View;
using BolapanControl.ItemsFilter.ViewModel;
using Loya.Dameer;
using Prism.Interactivity;
using Prism.Interactivity.InteractionRequest;
using Prism.Mvvm;
using Prism.Regions;
using Prism.Regions.Behaviors;
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
using Wpf.PageNavigationControl;


namespace AIC.HistoryDataPage.Views {
    
    
    /// <summary>
    /// HistoryDataListView
    /// </summary>
    public partial class HistoryDataListView : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 301 "..\..\..\Views\HistoryDataListView - Copy.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TreeView treeview;
        
        #line default
        #line hidden
        
        
        #line 522 "..\..\..\Views\HistoryDataListView - Copy.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Wpf.PageNavigationControl.PageNavigation pager;
        
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
            System.Uri resourceLocater = new System.Uri("/AIC.HistoryDataPage;component/views/historydatalistview%20-%20copy.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Views\HistoryDataListView - Copy.xaml"
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
            this.treeview = ((System.Windows.Controls.TreeView)(target));
            return;
            case 2:
            
            #line 404 "..\..\..\Views\HistoryDataListView - Copy.xaml"
            ((BolapanControl.ItemsFilter.FilterDataGrid)(target)).LoadingRow += new System.EventHandler<System.Windows.Controls.DataGridRowEventArgs>(this.FilterDataGrid_LoadingRow);
            
            #line default
            #line hidden
            return;
            case 3:
            
            #line 437 "..\..\..\Views\HistoryDataListView - Copy.xaml"
            ((BolapanControl.ItemsFilter.FilterDataGrid)(target)).LoadingRow += new System.EventHandler<System.Windows.Controls.DataGridRowEventArgs>(this.FilterDataGrid_LoadingRow);
            
            #line default
            #line hidden
            return;
            case 4:
            
            #line 470 "..\..\..\Views\HistoryDataListView - Copy.xaml"
            ((BolapanControl.ItemsFilter.FilterDataGrid)(target)).LoadingRow += new System.EventHandler<System.Windows.Controls.DataGridRowEventArgs>(this.FilterDataGrid_LoadingRow);
            
            #line default
            #line hidden
            return;
            case 5:
            this.pager = ((Wpf.PageNavigationControl.PageNavigation)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

