using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIC.Core.OrganizationModels
{
    public class ServerTreeItemViewModel : BindableBase 
    {
        public string Name {get; set; }

        public string serverIP;
        public string ServerIP
        {
            get
            {
                if (Parent != null)
                {
                    return Parent.ServerIP;
                }
                else
                {
                    return serverIP;
                }
            }
            set
            {
                serverIP = value;
            }
        }

        #region 附加属性
        private ObservableCollection<ServerTreeItemViewModel> children;
        public ObservableCollection<ServerTreeItemViewModel> Children
        {
            get
            {
                if (children == null)
                {
                    children = new ObservableCollection<ServerTreeItemViewModel>();
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
                ServerTreeItemViewModel child = (ServerTreeItemViewModel)e.NewItems[0];
                child.Parent = this;
            }
            else if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                ServerTreeItemViewModel child = (ServerTreeItemViewModel)e.OldItems[0];
                if (child.Parent == this)
                {
                    child.Parent = null;
                }
            }
        }

        public ServerTreeItemViewModel Parent { get; set; }

        private bool isExpanded;
        public bool IsExpanded
        {
            get { return isExpanded; }
            set
            {
                if (value != isExpanded)
                {
                    isExpanded = value;
                    OnPropertyChanged("IsExpanded");
                }
            }
        }
        private bool isSelected;
        public bool IsSelected
        {
            get { return isSelected; }
            set
            {
                if (value != isSelected)
                {
                    isSelected = value;
                    OnPropertyChanged("IsSelected");
                }
            }
        }

        public void AddChild(ServerTreeItemViewModel child)
        {
            if (!Children.Contains(child))
            {
                child.Parent = this;
                Children.Add(child);
            }
        }

        public void AddChildRange(IEnumerable<ServerTreeItemViewModel> childs)
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

        public void RemoveChild(ServerTreeItemViewModel child)
        {
            if (Children.Contains(child))
            {
                child.Parent = null;
                Children.Remove(child);
            }
        }

        public void ClearChild()
        {
            if (children != null)
            {
                children.Clear();
            }
        }
        #endregion

        public ServerTreeItemViewModel()
        {

        }
        public ServerTreeItemViewModel(string name)
        {
            Name = name;
            ServerIP = name;            
        }

        public ServerTreeItemViewModel(string name, string ip) : this(name)
        {
            ServerIP = ip;
        }
    }
}
