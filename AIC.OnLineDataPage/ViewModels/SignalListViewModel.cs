using AIC.Core.OrganizationModels;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIC.OnLineDataPage.ViewModels
{
    public class SignalListViewModel : BindableBase
    {
        public SignalListViewModel(DeviceTreeItemViewModel device)
        {
            this.device = device;
        }

        #region Public Property

        private DeviceTreeItemViewModel device;
        public DeviceTreeItemViewModel Device
        {
            get { return device; }
            set
            {
                if (device != value)
                {
                    device = value;
                    OnPropertyChanged(() => Device);
                }
            }
        }
        #endregion

        #region ItemWidth
        private double itemWidth;
        public double ItemWidth
        {
            get { return itemWidth; }
            set
            {
                if (itemWidth != value)
                {
                    itemWidth = value;
                    OnPropertyChanged("ItemWidth");
                }
            }
        }
        #endregion

        #region ItemHeight
        private double itemHeight;
        public double ItemHeight
        {
            get { return itemHeight; }
            set
            {
                if (itemHeight != value)
                {
                    itemHeight = value;
                    OnPropertyChanged("ItemHeight");
                }
            }
        }
        #endregion     

        #region IsSelected
        private bool isSelected;
        public bool IsSelected
        {
            get { return isSelected; }
            set
            {
                if (isSelected != value)
                {
                    isSelected = value;
                    this.OnPropertyChanged("IsSelected");
                }
            }
        }     
        #endregion
    }
}
