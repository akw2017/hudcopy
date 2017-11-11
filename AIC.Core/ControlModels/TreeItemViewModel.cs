using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace AIC.Core.ControlModels
{
    public class TreeItemViewModel : BindableBase
    {
        #region 基本属性    

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
        public int Parent_Level { get; set; }

        #endregion

        #region 附加属性
        private ObservableCollection<TreeItemViewModel> children;
        public ObservableCollection<TreeItemViewModel> Children
        {
            get
            {
                if (children == null)
                {
                    children = new ObservableCollection<TreeItemViewModel>();
                    children.CollectionChanged += new NotifyCollectionChangedEventHandler(OnChildrenChanged);
                }

                return children;
            }
            //set
            //{
            //    children = value;
            //    OnPropertyChanged("Children");
            //}
        }

        private void OnChildrenChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            // Note: This section does not account for multiple items being involved in change operations.
            // Note: This section does not account for the replace operation.
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                TreeItemViewModel child = (TreeItemViewModel)e.NewItems[0];
                child.Parent = this;
            }
            else if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                TreeItemViewModel child = (TreeItemViewModel)e.OldItems[0];
                if (child.Parent == this)
                {
                    child.Parent = null;
                }
            }
        }

        public TreeItemViewModel Parent { get; set; }

        private bool isExpanded;
        public bool IsExpanded
        {
            get { return isExpanded; }
            set
            {
                isExpanded = value;
                OnPropertyChanged("IsExpanded");               
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
       
        public void AddChild(TreeItemViewModel child)
        {
            if (!Children.Contains(child))
            {
                child.Parent = this;
                Children.Add(child);
            }
        }

        public void AddChildRange(IEnumerable<TreeItemViewModel> childs)
        {
            foreach (var child in childs)
            {
                if (!Children.Contains(child))
                {
                    child.Parent = this;
                    Children.Add(child);
                }
            }
        }
        #endregion

        public TreeItemViewModel()
        { }
       
    }
}
