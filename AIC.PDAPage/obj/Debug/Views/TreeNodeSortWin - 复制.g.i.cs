﻿#pragma checksum "..\..\..\Views\TreeNodeSortWin - 复制.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "50AE6591656DCAB753355D5A60256E52"
//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.42000
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

using AIC.PDAPage.Converters;
using MahApps.Metro.Controls;
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


namespace AIC.PDAPage.Views {
    
    
    /// <summary>
    /// TreeNodeSortWin
    /// </summary>
    public partial class TreeNodeSortWin : MahApps.Metro.Controls.MetroWindow, System.Windows.Markup.IComponentConnector {
        
        
        #line 21 "..\..\..\Views\TreeNodeSortWin - 复制.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListBox LBoxSort;
        
        #line default
        #line hidden
        
        
        #line 33 "..\..\..\Views\TreeNodeSortWin - 复制.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnOK;
        
        #line default
        #line hidden
        
        
        #line 34 "..\..\..\Views\TreeNodeSortWin - 复制.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnClose;
        
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
            System.Uri resourceLocater = new System.Uri("/AIC.PDAPage;component/views/treenodesortwin%20-%20%e5%a4%8d%e5%88%b6.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Views\TreeNodeSortWin - 复制.xaml"
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
            this.LBoxSort = ((System.Windows.Controls.ListBox)(target));
            
            #line 21 "..\..\..\Views\TreeNodeSortWin - 复制.xaml"
            this.LBoxSort.Drop += new System.Windows.DragEventHandler(this.LBoxSort_OnDrop);
            
            #line default
            #line hidden
            
            #line 21 "..\..\..\Views\TreeNodeSortWin - 复制.xaml"
            this.LBoxSort.PreviewMouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(this.LBoxSort_OnPreviewMouseLeftButtonDown);
            
            #line default
            #line hidden
            return;
            case 2:
            this.btnOK = ((System.Windows.Controls.Button)(target));
            
            #line 33 "..\..\..\Views\TreeNodeSortWin - 复制.xaml"
            this.btnOK.Click += new System.Windows.RoutedEventHandler(this.btnOK_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.btnClose = ((System.Windows.Controls.Button)(target));
            
            #line 34 "..\..\..\Views\TreeNodeSortWin - 复制.xaml"
            this.btnClose.Click += new System.Windows.RoutedEventHandler(this.btnClose_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

