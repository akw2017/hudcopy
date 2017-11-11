using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIC.CYSHPage.Models
{
    public class PropertyNodeItem : BindableBase
    {
        private string icon;
        public string Icon
        {
            get { return icon; }
            set
            {
                icon = value;
                OnPropertyChanged("Icon");
            }
        }
        private string name;
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged("Name");
            }
        }      

        private ObservableCollection<PropertyNodeItem> children;
        public ObservableCollection<PropertyNodeItem> Children
        {
            get { return children; }
            set
            {
                children = value;
                OnPropertyChanged("Children");
            }
        }

        public int ParentLevel { get; set; }

        public delegate void TransferExpanded(PropertyNodeItem item);
        public event TransferExpanded ItemExpanded;
        public PropertyNodeItem Parent { get; set; }
        private bool isExpanded;
        public bool IsExpanded
        {
            get { return isExpanded; }
            set
            {
                isExpanded = value;
                OnPropertyChanged("IsExpanded");
                if (value && ParentLevel >= 3)
                {
                    ItemExpanded(this);
                }
            }
        }
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

        public void AddChild(PropertyNodeItem child)
        {
            if (!Children.Contains(child))
            {
                child.Parent = this;
                Children.Add(child);
            }
        }

        public PropertyNodeItem(string name)
        {
            Name = name;
            Children = new ObservableCollection<PropertyNodeItem>();
        }     
    }
}
