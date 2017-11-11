using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace DiagramDesigner
{

    public abstract class SelectableDesignerItemViewModelBase : INPCBase
    {
        private bool isSelected;

        public SelectableDesignerItemViewModelBase()
        {

        }

        public SelectableDesignerItemViewModelBase(string id)
        {
            this.Id = id;
        }

        public List<SelectableDesignerItemViewModelBase> SelectedItems
        {
            get { return Parent.SelectedItems; }
        }

        public IDiagramViewModel Parent { get; set; }
        public SimpleCommand SelectItemCommand { get; private set; }
        public string Id { get; set; }

        public bool IsSelected
        {
            get { return isSelected;}
            set
            {
                if (isSelected != value)
                {
                    isSelected = value;
                    NotifyChanged("IsSelected");
                    if (value)
                    {
                        ItemIsSelected();
                    }
                }
            }
        }

        protected virtual void ItemIsSelected() { ;}


    }
}
