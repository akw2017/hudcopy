using AIC.Core;
using AIC.CoreType;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace AIC.Domain
{
    public delegate void TreeViewItemNameChangedHandler(AdvancedPropertyChangedEventArgs args);
    public class TreeViewItemModel : BindableBase
    {
        private readonly ObservableCollection<TreeViewItemModel> _children;

        protected TreeViewItemModel(string name)
        {
            _children = new ObservableCollection<TreeViewItemModel>();
            Name = new BindableValue<string>(name);
            InitializeLocation();
            Name.PropertyChanged += NameChanged;
        }

        protected virtual void NameChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            //RaiseTreeViewItemNameChanged()
        }

        protected virtual void InitializeLocation()
        {
            Location = new TreeItemLocation(Name);
        }

        public void AddChildren(IEnumerable<TreeViewItemModel> childItems)
        {
            foreach (var child in childItems)
            {
                AddChild(child);
            }
        }

        public virtual void AddChildTree()
        {

        }

        public void AddChild(TreeViewItemModel child)
        {
            if (!_children.Contains(child))
            {
                child.Parent = this;
                SetAddedChildLocation(child);
                _children.Add(child);
            }
        }

        protected virtual void SetAddedChildLocation(TreeViewItemModel child)
        {
            child.Location = Location.Merge(child.Location);
        }

        public void Clear()
        {
            var children = _children.ToArray();
            foreach (var child in children)
            {
                RemoveChild(child);
            }
        }

        public void RemoveChild(TreeViewItemModel child)
        {
            if (_children.Contains(child))
            {
                child.Parent = null;
                _children.Remove(child);
            }
        }

        public int Count { get { return _children.Count; } }

        public IEnumerable<TreeViewItemModel> Children { get { return _children; } }

        #region IsExpanded

        /// <summary>
        /// Gets/sets whether the TreeViewItem 
        /// associated with this object is expanded.
        /// </summary>
        private bool _isExpanded;
        public bool IsExpanded
        {
            get { return _isExpanded; }
            set
            {
                if (value != _isExpanded)
                {
                    _isExpanded = value;
                    OnPropertyChanged("IsExpanded");
                }

                // Expand all the way up to the root.
                //if (_isExpanded && _parent != null)
                //    _parent.IsExpanded = true;

            }
        }

        #endregion

        #region IsSelected
        private bool _isSelected;
        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                if (value != _isSelected)
                {
                    _isSelected = value;

                    if (value)
                    {
                        ItemIsSelected();
                    }
                    OnPropertyChanged("IsSelected");
                }
            }
        }
        #endregion

        public string GetGlobalID()
        {
            if (Location != null)
            {
                return BitConverter.ToString(SHA1Managed.Create().ComputeHash(Encoding.UTF8.GetBytes(string.Concat(Location)))).Replace("-", string.Empty);
            }
            else
            {
                return string.Empty;
            }
        }

        public TreeItemLocation Location { get; protected set; }
        public virtual BindableValue<string> Name { get; }

        protected virtual void ItemIsSelected() {; }

        #region LoadChildren

        /// <summary>
        /// Invoked when the child items need to be loaded on demand.
        /// Subclasses can override this to populate the Children collection.
        /// </summary>
        protected virtual void LoadChildren()
        {
        }

        #endregion // LoadChildren

        #region Parent
        private TreeViewItemModel _parent;
        public TreeViewItemModel Parent
        {
            get { return _parent; }
            set
            {
                if (_parent != value)
                {
                    _parent = value;
                    OnPropertyChanged("Parent");
                }
            }
        }
        #endregion

        private AlarmGrade alarm = AlarmGrade.HighNormal;
        public virtual AlarmGrade Alarm
        {
            get { return alarm; }
            set
            {
                if (alarm != value)
                {
                    alarm = value;
                    if (_children.Count != 0)
                    {
                        var alarms = _children.Select(o => o.Alarm);

                        if (alarms.Contains(AlarmGrade.HighDanger) || alarms.Contains(AlarmGrade.LowDanger))
                        {
                            alarm = AlarmGrade.HighDanger;
                        }
                        else if (alarms.Contains(AlarmGrade.HighAlert) || alarms.Contains(AlarmGrade.LowAlert))
                        {
                            alarm = AlarmGrade.HighAlert;
                        }
                        else
                        {
                            alarm = AlarmGrade.HighNormal;
                        }
                    }
                    else
                    {
                        alarm = value;
                    }

                    if (_parent != null)
                    {
                        Parent.Alarm = alarm;
                    }

                    OnPropertyChanged("Alarm");
                }
            }
        }

        #region IsChecked

        /// <summary>
        /// Gets/sets the state of the associated UI toggle (ex. CheckBox).
        /// The return value is calculated based on the check state of all
        /// child FooViewModels.  Setting this property to true or false
        /// will set all children to the same check state, and setting it 
        /// to any value will cause the parent to verify its check state.
        /// </summary>
        bool? _isChecked = false;
        public bool? IsChecked
        {
            get { return _isChecked; }
            set { this.SetIsChecked(value, true, true); }
        }

        void SetIsChecked(bool? value, bool updateChildren, bool updateParent)
        {
            if (value == _isChecked)
                return;

            _isChecked = value;

            if (updateChildren && _isChecked.HasValue)
                this.Children.ToList().ForEach(c => c.SetIsChecked(_isChecked, true, false));

            if (updateParent && _parent != null)
                _parent.VerifyCheckState();

            OnPropertyChanged("IsChecked");
        }

        void VerifyCheckState()
        {
            bool? state = null;
            for (int i = 0; i < this._children.Count; ++i)
            {
                bool? current = this._children[i].IsChecked;
                if (i == 0)
                {
                    state = current;
                }
                else if (state != current)
                {
                    state = null;
                    break;
                }
            }
            this.SetIsChecked(state, false, true);
        }

        #endregion // IsChecked
    }

}


