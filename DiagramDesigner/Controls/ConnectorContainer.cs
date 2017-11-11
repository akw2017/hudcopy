using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Collections.Specialized;

namespace DiagramDesigner
{
    //public class ConnectorContainer : Control
    //{
    //    private Canvas rootGrid;
    //  //  private Dictionary<object, UIElement> controlDict;

    //    //static ConnectorContainer()
    //    //{
    //    //    DefaultStyleKeyProperty.OverrideMetadata(typeof(ConnectorContainer), new FrameworkPropertyMetadata(typeof(ConnectorContainer)));
    //    //}

    //    public ConnectorContainer()
    //    {
    //        //controlDict = new Dictionary<object, UIElement>();
    //        SizeChanged += ConnectorContainer_SizeChanged;
    //    }

    //    void ConnectorContainer_SizeChanged(object sender, SizeChangedEventArgs e)
    //    {
    //        var connectors = rootGrid.Children.OfType<Connector>();
    //        foreach (var connector in connectors)
    //        {
    //            var vm = connector.DataContext as FullyCreatedConnectorInfo;
    //            if(vm!=null)
    //            {
    //                Canvas.SetLeft(connector, vm.DataItem.ItemWidth * vm.XRatio);
    //                Canvas.SetTop(connector, vm.DataItem.ItemHeight * vm.YRatio);
    //            }
    //        }
    //    }

    //    public override void OnApplyTemplate()
    //    {
    //        //base.OnApplyTemplate();
    //        this.rootGrid = (GetTemplateChild("Root") as Canvas);
    //        foreach (var item in Connectors)
    //        {
    //            FullyCreatedConnectorInfo info = item as FullyCreatedConnectorInfo;
    //            Connector connector = new Connector();
    //            connector.DataContext = info;

    //            //Binding b = new Binding();
    //            //b.Source = info;
    //            //b.Path = new PropertyPath("Position");
    //            //b.Mode = BindingMode.TwoWay;
    //            //BindingOperations.SetBinding(connector, Connector.PositionProperty, b);

    //            //Binding b1 = new Binding();
    //            //b1.Source = info;
    //            //b1.Path = new PropertyPath("Orientation");
    //            //b1.Mode = BindingMode.TwoWay;
    //            //BindingOperations.SetBinding(connector, Connector.OrientationProperty, b1);

    //            rootGrid.Children.Add(connector);
    //            //Canvas.SetLeft(connector, connector.Position.X - info.DataItem.Left - 8);
    //            //Canvas.SetTop(connector, connector.Position.Y - info.DataItem.Top - 8);

    //            //controlDict.Add(info, connector);
    //        }
    //    }

    //    //protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
    //    //{
    //    //    //base.OnMouseLeftButtonDown(e);
    //    //    //Connector connector = new Connector();
    //    //    //rootGrid.Children.Add(connector);
    //    //    //double x = e.GetPosition(this).X;
    //    //    //double y = e.GetPosition(this).Y;
    //    //    //Canvas.SetLeft(connector, x);
    //    //    //Canvas.SetTop(connector, y);
    //    //}

    //    #region Dependency Property

    //    #region Controls
    //    public static readonly DependencyProperty ConnectorsProperty = DependencyProperty.Register
    //        ("Connectors", typeof(ObservableCollection<FullyCreatedConnectorInfo>), typeof(ConnectorContainer), new FrameworkPropertyMetadata(null, OnConnectorsChanged));

    //    public ObservableCollection<FullyCreatedConnectorInfo> Connectors
    //    {
    //        get { return (ObservableCollection<FullyCreatedConnectorInfo>)GetValue(ConnectorsProperty); }
    //        set { SetValue(ConnectorsProperty, value); }
    //    }

    //    private static void OnConnectorsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    //    {
    //        var container = d as ConnectorContainer;
    //        if (container == null) return;
    //        if (container.Connectors == null) return;

    //        container.Connectors.CollectionChanged -= container.Connectors_CollectionChanged;



    //        container.Connectors.CollectionChanged += container.Connectors_CollectionChanged;
    //        //container.rootGrid.Children.Clear();

    //        //foreach (var connectorInfo in container.Connectors)
    //        //{
    //        //    Connector connector = new Connector();
    //        //    connector.DataContext = connectorInfo;
    //        //    container.rootGrid.Children.Add(connector);
    //        //}
    //    }

    //    private void Connectors_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
    //    {
    //        var viewModel = DataContext as DesignerItemViewModelBase;
    //        if (viewModel == null) return;
    //        //e.Action
    //        if (e.Action == NotifyCollectionChangedAction.Add)
    //        {
    //            if (e.NewItems != null)
    //            {
    //                foreach (var item in e.NewItems)
    //                {
    //                    FullyCreatedConnectorInfo info = item as FullyCreatedConnectorInfo;
    //                    Connector connector = new Connector();
    //                    connector.DataContext = info;

    //                    //Binding b = new Binding();
    //                    //b.Source = info;
    //                    //b.Path = new PropertyPath("Position");
    //                    //b.Mode = BindingMode.TwoWay;
    //                    //BindingOperations.SetBinding(connector, Connector.PositionProperty, b);

    //                    //Binding b1 = new Binding();
    //                    //b1.Source = info;
    //                    //b1.Path = new PropertyPath("Orientation");
    //                    //b1.Mode = BindingMode.TwoWay;
    //                    //BindingOperations.SetBinding(connector, Connector.OrientationProperty, b1);  

    //                    rootGrid.Children.Add(connector);
    //                    ////double x = e.GetPosition(this).X;
    //                    ////double y = e.GetPosition(this).Y;
    //                    Canvas.SetLeft(connector, viewModel.ItemWidth * info.XRatio);
    //                    Canvas.SetTop(connector, viewModel.ItemHeight * info.YRatio);

    //                    // Canvas.SetLeft(connector, connector.Position.X - info.DataItem.Left - 8);
    //                    // Canvas.SetTop(connector, connector.Position.Y - info.DataItem.Top - 8);

    //                   // controlDict.Add(info, connector);
    //                }
    //            }
    //        }
    //        else if (e.Action == NotifyCollectionChangedAction.Remove)
    //        {
    //            if (e.OldItems != null)
    //            {
    //                foreach (var item in e.OldItems)
    //                {
    //                    var connector=rootGrid.Children.OfType<Connector>().Where(o=>o)
    //                    //if (controlDict.Keys.Contains(item))
    //                    //{
                       
    //                        //rootGrid.Children.Remove(controlDict[item]);
    //                       // controlDict.Remove(item);
    //                    //}
    //                }
    //            }
    //        }
    //        else if(e.Action==NotifyCollectionChangedAction.Reset)
    //        {
    //            rootGrid.Children.Clear();
    //        }       
    //    }
    //    #endregion

    //    #endregion
    //}


    //public class ConnectorContainer : ItemsControl
    //{
    //    private Canvas rootGrid;
    //    public ConnectorContainer()
    //    {
    //        //controlDict = new Dictionary<object, UIElement>();
    //        SizeChanged += ConnectorContainer_SizeChanged;
    //    }

    //    void ConnectorContainer_SizeChanged(object sender, SizeChangedEventArgs e)
    //    {
    //        var connectors = rootGrid.Children.OfType<Connector>();
    //        foreach (var connector in connectors)
    //        {
    //            var vm = connector.DataContext as FullyCreatedConnectorInfo;
    //            if (vm != null)
    //            {
    //                Canvas.SetLeft(connector, vm.DataItem.ItemWidth * vm.XRatio);
    //                Canvas.SetTop(connector, vm.DataItem.ItemHeight * vm.YRatio);
    //            }
    //        }
    //    }
    //}

}
