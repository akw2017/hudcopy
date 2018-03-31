using AIC.Core.HardwareModels;
using AIC.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIC.Core.OrganizationModels
{
    public class ChannelTreeItemViewModel : ServerTreeItemViewModel
    {
        public IChannel IChannel { get; set; }             

        public bool isPaired;//绑定
        public bool IsPaired
        {
            get { return isPaired; }
            set
            {
                isPaired = value;
                OnPropertyChanged("IsPaired");
            }
        }

        public string MainControlCardIP
        {
            get
            {
                if (Parent != null && Parent is SlotTreeItemViewModel)
                {
                    return (Parent as SlotTreeItemViewModel).MainControlCardIP;
                }               
                else
                {
                    return null;
                }
            }
        }
        public int CardNum
        {
            get
            {
                if (Parent != null && Parent is SlotTreeItemViewModel)
                {
                    return (Parent as SlotTreeItemViewModel).CardNum;
                }
                else
                {
                    return -1;
                }
            }
        }
        public string SlaveIdentifier
        {
            get
            {
                if (Parent != null && Parent is SlotTreeItemViewModel)
                {
                    return (Parent as SlotTreeItemViewModel).SlaveIdentifier;
                }
                else
                {
                    return null;
                }
            }
        }       
        public int SlotNum
        {
            get
            {
                if (Parent != null && Parent is SlotTreeItemViewModel)
                {
                    return (Parent as SlotTreeItemViewModel).SlotNum;
                }
                else
                {
                    return -1;
                }
            }
        }
        public int CHNum { get; set; }
        //public ChannelTreeItemViewModel(int chNum) : base("")
        //{
        //    CHNum = chNum;            
        //}

        public ChannelTreeItemViewModel(IChannel ichannel) : base("")
        {            
            CHNum = ichannel.CHNum;
            IChannel = ichannel;

            if (IChannel != null && IChannel.T_Device_Guid != null && IChannel.T_Item_Guid != null && IChannel.T_Device_Guid != "" && IChannel.T_Item_Guid != "")
            {
                if ((IChannel.T_Device_Guid != "00000000-0000-0000-0000-000000000000") && (IChannel.T_Item_Guid != "00000000-0000-0000-0000-000000000000"))
                {
                    IsPaired = true;
                }
            }
        }

        public void BindChannel(ItemTreeItemViewModel item, List<Organization> organizations)
        {
            IChannel.T_AbstractChannelInfo.GetTempData();

            //可以考虑将T_AbstractChannelInfo转化为IChannel htzk123
            IChannel.Organization = organizations;

            IChannel.T_Item_Name = item.T_Item.Name;
            IChannel.T_Item_Code = item.T_Item.Code;
            if (item.T_Item.Guid != null && item.T_Item.Guid != new Guid())
            {
                IChannel.T_Item_Guid = item.T_Item.Guid.ToString();
            }
            else
            {
                IChannel.T_Item_Guid = null;
            }
            IChannel.T_Device_Name = item.Parent.T_Organization.Name;
            IChannel.T_Device_Code = item.Parent.T_Organization.Code;
            if (item.Parent.T_Organization.Guid != null && item.Parent.T_Organization.Guid != new Guid())
            {
                IChannel.T_Device_Guid = item.Parent.T_Organization.Guid.ToString();
            }
            else
            {
                IChannel.T_Device_Guid = null;
            }

            IsPaired = true;           
        }

        public void UnBindChannel()
        {
            IChannel.T_AbstractChannelInfo.GetTempData();

            IChannel.Organization = null;

            IChannel.T_Item_Name = "";
            IChannel.T_Item_Code = "";
            IChannel.T_Item_Guid = "";
            IChannel.T_Device_Name = "";
            IChannel.T_Device_Code = "";
            IChannel.T_Device_Guid = "";
            IsPaired = false;
        }

        public void BindTemp(ItemTreeItemViewModel item, List<Organization> organizations, DeviceTreeItemViewModel device)
        {
            IChannel.T_AbstractChannelInfo.BindTemp(item, organizations, device);
        }

        public void UnBindTemp()
        {
            IChannel.T_AbstractChannelInfo.UnBindTemp();
        }
    }
}
