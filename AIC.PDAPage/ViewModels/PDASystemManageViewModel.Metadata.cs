using AIC.Core.HardwareModels;
using AIC.Core.Helpers;
using AIC.Core.Models;
using AIC.Core.OrganizationModels;
using AIC.Core.Validation;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AIC.PDAPage.ViewModels
{
    public partial class PDASystemManageViewModel : IDataErrorInfo
    {
        class PDASystemManageViewModelMetadata
        {
            [IPValidation]
            public string MainControlCardIPEdit { get; set; }
        }

        [JsonIgnore]
        public string Error
        {
            get
            {
                string error = null;
                PropertyInfo[] propertys = this.GetType().GetProperties();
                foreach (PropertyInfo pinfo in propertys)
                {
                    //循环遍历属性
                    if (pinfo.CanRead && pinfo.CanWrite)
                    {
                        error = this.ValidateProperty<PDASystemManageViewModelMetadata>(pinfo.Name);
                        if (error != null && error.Length > 0)
                        {
                            break;
                        }
                    }                                  
                }
                return error;
            }
        }

        public string this[string columnName]
        {
            get
            {
                return this.ValidateProperty<PDASystemManageViewModelMetadata>(columnName);
            }
        }


        private string GetRootjson(RequestType type, MainCardTreeItemViewModel maincard, string serverip)
        {
            var root = GetRootCard(type, maincard, serverip);
            string json = JsonConvert.SerializeObject(root);
            return json;
        }

        private RootCard GetRootCard(RequestType type, MainCardTreeItemViewModel maincard, string serverip)
        {
            RootCard root = new RootCard();

            switch (type)
            {
                case RequestType.ModifiedConfig:
                case RequestType.DeleteConfig:
                    {
                        if (maincard.MainControlCard != null)
                        {
                            root.MainControlCard = maincard.MainControlCard;
                        }
                        if (maincard.WireMatchingCard != null)
                        {
                            root.WireMatchingCard = maincard.WireMatchingCard;
                        }
                        if (maincard.WirelessReceiveCard != null)
                        {
                            root.WirelessReceiveCard = maincard.WirelessReceiveCard;
                        }
                        root.RequestCommand = new RequestCommand() { Type = (int)type };
                        break;
                    }
                default:
                    {
                        root.MainControlCard = new MainControlCard() { ServerIP = serverip };
                        root.RequestCommand = new RequestCommand() { Type = (int)type };
                        break;
                    }
            }
            return root;
        }

        private void ClearEdited(MainCardTreeItemViewModel maincard)
        {
            if (maincard == null)
            {
                return;
            }
            if (maincard.MainControlCard != null)
            {
                maincard.MainControlCard.IsEdited = false;
            }
            if (maincard.WirelessReceiveCard != null)
            {
                maincard.WirelessReceiveCard.IsEdited = false;
                if (maincard.WirelessReceiveCard.TransmissionCard != null)
                {
                    foreach (var transmissionCard in maincard.WirelessReceiveCard.TransmissionCard)
                    {
                        transmissionCard.IsEdited = false;
                        if (transmissionCard.WirelessScalarSlot != null)
                        {
                            transmissionCard.WirelessScalarSlot.IsEdited = false;
                            if (transmissionCard.WirelessScalarSlot.WirelessScalarChannelInfo != null)
                            {
                                foreach (var channelinfo in transmissionCard.WirelessScalarSlot.WirelessScalarChannelInfo)
                                {
                                    channelinfo.IsEdited = false;
                                }
                            }
                        }
                        if (transmissionCard.WirelessVibrationSlot != null)
                        {
                            transmissionCard.WirelessVibrationSlot.IsEdited = false;
                            if (transmissionCard.WirelessVibrationSlot.WirelessVibrationChannelInfo != null)
                            {
                                foreach (var channelinfo in transmissionCard.WirelessVibrationSlot.WirelessVibrationChannelInfo)
                                {
                                    channelinfo.IsEdited = false;
                                    if (channelinfo.DivFreInfo != null)
                                    {
                                        foreach (var divFreInfo in channelinfo.DivFreInfo)
                                        {
                                            divFreInfo.IsEdited = false;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            if (maincard.WireMatchingCard != null)
            {
                foreach (var wireMatchingCard in maincard.WireMatchingCard)
                {
                    wireMatchingCard.IsEdited = false;
                    if (wireMatchingCard.IEPESlot != null)
                    {
                        wireMatchingCard.IEPESlot.IsEdited = false;
                        if (wireMatchingCard.IEPESlot.IEPEChannelInfo != null)
                        {
                            foreach (var channelinfo in wireMatchingCard.IEPESlot.IEPEChannelInfo)
                            {
                                channelinfo.IsEdited = false;
                                if (channelinfo.DivFreInfo != null)
                                {
                                    foreach (var divFreInfo in channelinfo.DivFreInfo)
                                    {
                                        divFreInfo.IsEdited = false;
                                    }
                                }
                            }
                        }
                    }

                    if (wireMatchingCard.EddyCurrentDisplacementSlot != null)
                    {
                        wireMatchingCard.EddyCurrentDisplacementSlot.IsEdited = false;
                        if (wireMatchingCard.EddyCurrentDisplacementSlot.EddyCurrentDisplacementChannelInfo != null)
                        {
                            foreach (var channelinfo in wireMatchingCard.EddyCurrentDisplacementSlot.EddyCurrentDisplacementChannelInfo)
                            {
                                channelinfo.IsEdited = false;
                                if (channelinfo.DivFreInfo != null)
                                {
                                    foreach (var divFreInfo in channelinfo.DivFreInfo)
                                    {
                                        divFreInfo.IsEdited = false;
                                    }
                                }
                            }
                        }
                    }

                    if (wireMatchingCard.EddyCurrentKeyPhaseSlot != null)
                    {
                        wireMatchingCard.EddyCurrentKeyPhaseSlot.IsEdited = false;
                        if (wireMatchingCard.EddyCurrentKeyPhaseSlot.EddyCurrentKeyPhaseChannelInfo != null)
                        {
                            foreach (var channelinfo in wireMatchingCard.EddyCurrentKeyPhaseSlot.EddyCurrentKeyPhaseChannelInfo)
                            {
                                channelinfo.IsEdited = false;
                            }
                        }
                    }

                    if (wireMatchingCard.EddyCurrentTachometerSlot != null)
                    {
                        wireMatchingCard.EddyCurrentTachometerSlot.IsEdited = false;
                        if (wireMatchingCard.EddyCurrentTachometerSlot.EddyCurrentTachometerChannelInfo != null)
                        {
                            foreach (var channelinfo in wireMatchingCard.EddyCurrentTachometerSlot.EddyCurrentTachometerChannelInfo)
                            {
                                channelinfo.IsEdited = false;
                            }
                        }
                    }

                    if (wireMatchingCard.DigitTachometerSlot != null)
                    {
                        wireMatchingCard.DigitTachometerSlot.IsEdited = false;
                        if (wireMatchingCard.DigitTachometerSlot.DigitTachometerChannelInfo != null)
                        {
                            foreach (var channelinfo in wireMatchingCard.DigitTachometerSlot.DigitTachometerChannelInfo)
                            {
                                channelinfo.IsEdited = false;
                            }
                        }
                    }

                    if (wireMatchingCard.AnalogRransducerInSlot != null)
                    {
                        wireMatchingCard.AnalogRransducerInSlot.IsEdited = false;
                        if (wireMatchingCard.AnalogRransducerInSlot.AnalogRransducerInChannelInfo != null)
                        {
                            foreach (var channelinfo in wireMatchingCard.AnalogRransducerInSlot.AnalogRransducerInChannelInfo)
                            {
                                channelinfo.IsEdited = false;
                            }
                        }
                    }

                    if (wireMatchingCard.RelaySlot != null)
                    {
                        wireMatchingCard.RelaySlot.IsEdited = false;
                        if (wireMatchingCard.RelaySlot.RelayChannelInfo != null)
                        {
                            foreach (var channelinfo in wireMatchingCard.RelaySlot.RelayChannelInfo)
                            {
                                channelinfo.IsEdited = false;
                            }
                        }
                    }

                    if (wireMatchingCard.DigitRransducerInSlot != null)
                    {
                        wireMatchingCard.DigitRransducerInSlot.IsEdited = false;
                        if (wireMatchingCard.DigitRransducerInSlot.DigitRransducerInChannelInfo != null)
                        {
                            foreach (var channelinfo in wireMatchingCard.DigitRransducerInSlot.DigitRransducerInChannelInfo)
                            {
                                channelinfo.IsEdited = false;
                            }
                        }
                    }

                    if (wireMatchingCard.DigitRransducerOutSlot != null)
                    {
                        wireMatchingCard.DigitRransducerOutSlot.IsEdited = false;
                        if (wireMatchingCard.DigitRransducerOutSlot.DigitRransducerOutChannelInfo != null)
                        {
                            foreach (var channelinfo in wireMatchingCard.DigitRransducerOutSlot.DigitRransducerOutChannelInfo)
                            {
                                channelinfo.IsEdited = false;
                            }
                        }
                    }

                    if (wireMatchingCard.AnalogRransducerOutSlot != null)
                    {
                        wireMatchingCard.AnalogRransducerOutSlot.IsEdited = false;
                        if (wireMatchingCard.AnalogRransducerOutSlot.AnalogRransducerOutChannelInfo != null)
                        {
                            foreach (var channelinfo in wireMatchingCard.AnalogRransducerOutSlot.AnalogRransducerOutChannelInfo)
                            {
                                channelinfo.IsEdited = false;
                            }
                        }
                    }
                }
            }

        }

        //删除分频通知
        private void DeleteDivfreNotify(DivFreTreeItemViewModel divfretree)
        {
            _organizationService.DeleteDivFre(divfretree);//删除分频从组织管理
            _signalProcess.DeleteDivfre(divfretree);//删除分频从信号
        }

        //添加分频通知
        private void AddDivfreNotify(DivFreTreeItemViewModel divfretree)
        {
            _organizationService.AddDivFre(divfretree);//添加分频到组织管理
            _signalProcess.AddDivfre(divfretree);//添加分频到信号
        }

        //解除绑定通知
        private void UnBindNotify(ChannelTreeItemViewModel channeltree, ItemTreeItemViewModel itemtree)
        {
            if (channeltree == null && itemtree != null)//删除了硬件通道
            {
                var recycleditemtree = _cardProcess.RecycleRecycledItem(RecycledTreeItems, itemtree);//生成回收站测点
                _organizationService.DeleteItem(itemtree);//删除旧的测点从组织管理
                _organizationService.AddItem(recycleditemtree);//加入新的测点从组织管理
                _signalProcess.UnBindItem(itemtree);//删除测点从信号
                itemtree.UnBindItem();
            }
            else if (channeltree != null && itemtree != null)
            {
                channeltree.UnBindChannel();
                var recycleditemtree = _cardProcess.RecycleRecycledItem(RecycledTreeItems, itemtree);//生成回收站测点
                _organizationService.DeleteItem(itemtree);//删除旧的测点从组织管理
                _organizationService.AddItem(recycleditemtree);//加入新的测点从组织管理
                _signalProcess.UnBindItem(itemtree);//添加测点从信号
                itemtree.UnBindItem();
            }
            else if (channeltree != null && itemtree == null)
            {
                channeltree.UnBindChannel();
            }
        }

        //绑定通知
        private void BindNotify(ChannelTreeItemViewModel channeltree, List<Organization> organizations, ItemTreeItemViewModel itemtree, ItemTreeItemViewModel recycleditemtree)
        {
            if (recycleditemtree != null)//从回收站回收
            {
                itemtree.RestoredItemTreeItem(recycleditemtree);//恢复     
                if (channeltree != null)
                {
                    channeltree.BindChannel(itemtree, organizations);
                }
                _signalProcess.BindItem(itemtree);//添加测点到信号
                _organizationService.DeleteItem(recycleditemtree);//删除旧的测点从组织管理
                _organizationService.AddItem(itemtree);//加入新的测点到组织管理
                _cardProcess.RecycleDeleteItem(RecycledTreeItems, recycleditemtree);//回收站
            }
            else
            {
                itemtree.BindItem();
                channeltree.BindChannel(itemtree, organizations);
                _signalProcess.BindItem(itemtree);//添加测点到信号
                _organizationService.AddItem(itemtree);//加入新的测点到组织管理 
            }
        }
    }
}
