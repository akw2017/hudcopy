using AIC.CoreType;

using System.Collections.Generic;

namespace AIC.Domain
{
    public class EquipmentTreeModel : TreeViewItemModel
    {
        public EquipmentTreeModel(string name, string sn)
            : base(name)
        {
            IsExpanded = false;
            MSSN = new BindableValue<string>(sn);
            InitializeLocation();
        }

        protected override void InitializeLocation()
        {
            Location = new TreeItemLocation(Name, MSSN);
        }

        public BindableValue<string> MSSN { get; }

        private bool isSource;
        public bool IsSource
        {
            get { return isSource; }
            set
            {
                if (isSource != value)
                {
                    isSource = value;
                    OnPropertyChanged("IsSource");
                }
            }
        }    
    }
}