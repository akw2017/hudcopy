using AIC.Core.DiagnosticModels;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AIC.NetWorksPage.Models
{
    public class NodeViewModel : BindableBase
    {
        public double Left { get; set; }
        public double Top { get; set; }
        public string Name { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
        public int Index { get; set; }

        private double showValue;
        public double ShowValue
        {
            get { return showValue; }
            set
            {
                showValue = value;
                OnPropertyChanged("ShowValue");
            }
        }
        public NodeViewModel(double left, double top, double width, double height, int index) : this(left, top, width, height, index, index.ToString())
        {
        }
        public NodeViewModel(double left, double top, double width, double height, int index, string name)
        {
            Left = left;
            Top = top;
            Width = width;
            Height = height;
            Index = index;
            Name = name;
        }
    }

    public class RectangleNodeViewModel : NodeViewModel
    {
        private bool isSelected;
        public bool IsSelected
        {
            get { return isSelected; }
            set
            {
                isSelected = value;
                OnPropertyChanged("IsSelected");
            }
        }
        public NodeDisplay NodeDisplay { get; set; }
        public RectangleNodeViewModel(double left, double top, double width, double height, int index) : base(left, top, width, height, index) { }
        public RectangleNodeViewModel(NodeDisplay node, string name) : base(node.Left, node.Top, node.Width, node.Height, node.Index)
        {
            NodeDisplay = node;
            Name = name;
        }
    }

    public class EllipseNodeViewModel : NodeViewModel
    {
        private bool isSelected;
        public bool IsSelected
        {
            get { return isSelected; }
            set
            {
                isSelected = value;
                OnPropertyChanged("IsSelected");
            }
        }
        public NodeDisplay NodeDisplay { get; set; }
        public EllipseNodeViewModel(double left, double top, double width, double height, int index) : base(left, top, width, height, index) { }
        public EllipseNodeViewModel(NodeDisplay node, string name) : base(node.Left, node.Top, node.Width, node.Height, node.Index)
        {
            NodeDisplay = node;
            Name = name;
        }
    }

    public class LineNodeViewModel : NodeViewModel
    {
        public Point EndPoint { get; set; }
        public LineDisplay LineDisplay { get; set; }
        public LineNodeViewModel(double left, double top, double width, double height, int index) : base(left, top, width, height, index) { }
        public LineNodeViewModel(LineDisplay node) : base(node.Left, node.Top, node.Width, node.Height, node.Index[0])
        {
            LineDisplay = node;
            EndPoint = new Point(node.Width, node.Height);
        }
    }
}
