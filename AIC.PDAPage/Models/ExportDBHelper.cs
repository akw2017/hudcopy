using AIC.Core.LMModels;
using AIC.Core.Models;
using AIC.Core.OrganizationModels;
using AIC.ServiceInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIC.PDAPage.Models
{
    class ExportDBHelper
    {
        private static string NamesConvertToStructure(string[] names)
        {
            string structure = string.Empty;
            foreach (var name in names)
            {
                structure = " -> " + name + structure;
            }
            return structure.Substring(4);
        }

        public static string[] StructureConvertToNames(string structure)
        {
            string[] Names = structure.Split(new string[] { " -> " }, StringSplitOptions.None);

            return Names;
        }

        private static AlarmCategory GetAlarmValue(List<AlarmCategory> category, string name)
        {
            switch (name)
            {
                case "危险": return category.Where(p => p.Name == "危险" || p.Name == "高危" || p.Name == "高危险").FirstOrDefault();
                case "警告": return category.Where(p => p.Name == "警告" || p.Name == "高警" || p.Name == "高警告").FirstOrDefault();
                case "预警": return category.Where(p => p.Name == "预警" || p.Name == "预警告" || p.Name == "高预警" || p.Name == "高预警告").FirstOrDefault();
                case "正常": return category.Where(p => p.Name == "正常" || p.Name == "高正常").FirstOrDefault();
                case "低危险": return category.Where(p => p.Name == "低危险" || p.Name == "低危").FirstOrDefault();
                case "低警告": return category.Where(p => p.Name == "低警告" || p.Name == "低警").FirstOrDefault();
                case "低预警": return category.Where(p => p.Name == "低预警" || p.Name == "低预警告").FirstOrDefault();
                case "低正常": return category.Where(p => p.Name == "低正常").FirstOrDefault();
                default: return null;
            }
        }

        public static List<AlarmCategory> GetAlarmCategory(
            float? dangerValue, float? alarmValue, float? preAlarmValue, float? normalValue, float? lowDangerValue, float? lowAlarmValue, float? lowPreAlarmValue, float? lowNormalValue,
            bool? dangerIsAllow, bool? alarmIsAllow, bool? preAlarmIsAllow, bool? normalIsAllow, bool? lowDangerIsAllow, bool? lowAlarmIsAllow, bool? lowPreAlarmIsAllow, bool? lowNormalIsAllow,
            bool? dangerIsACQWave, bool? alarmIsACQWave, bool? preAlarmIsACQWave, bool? normalIsACQWave, bool? lowDangerIsACQWave, bool? lowAlarmIsACQWave, bool? lowPreAlarmIsACQWave, bool? lowNormalIsACQWave)
        {
            List<AlarmCategory> categorys = new List<AlarmCategory>();
            if (dangerValue != null && dangerIsAllow != null && dangerIsACQWave != null)
            {
                AlarmCategory category = new AlarmCategory() { Value = dangerValue ?? 0, IsAllow = dangerIsAllow ?? false, IsACQWave = dangerIsACQWave ?? false };
                categorys.Add(category);
            }
            if (alarmValue != null && alarmIsAllow != null && alarmIsACQWave != null)
            {
                AlarmCategory category = new AlarmCategory() { Value = alarmValue ?? 0, IsAllow = alarmIsAllow ?? false, IsACQWave = alarmIsACQWave ?? false };
                categorys.Add(category);
            }
            if (preAlarmValue != null && preAlarmIsAllow != null && preAlarmIsACQWave != null)
            {
                AlarmCategory category = new AlarmCategory() { Value = preAlarmValue ?? 0, IsAllow = preAlarmIsAllow ?? false, IsACQWave = preAlarmIsACQWave ?? false };
                categorys.Add(category);
            }
            if (normalValue != null && normalIsAllow != null && normalIsACQWave != null)
            {
                AlarmCategory category = new AlarmCategory() { Value = normalValue ?? 0, IsAllow = normalIsAllow ?? false, IsACQWave = normalIsACQWave ?? false };
                categorys.Add(category);
            }
            if (lowNormalValue != null && lowNormalIsAllow != null && lowNormalIsACQWave != null)
            {
                AlarmCategory category = new AlarmCategory() { Value = lowNormalValue ?? 0, IsAllow = lowNormalIsAllow ?? false, IsACQWave = lowNormalIsACQWave ?? false };
                categorys.Add(category);
            }
            if (lowPreAlarmValue != null && lowPreAlarmIsAllow != null && lowPreAlarmIsACQWave != null)
            {
                AlarmCategory category = new AlarmCategory() { Value = lowPreAlarmValue ?? 0, IsAllow = lowPreAlarmIsAllow ?? false, IsACQWave = lowPreAlarmIsACQWave ?? false };
                categorys.Add(category);
            }
            if (lowAlarmValue != null && lowAlarmIsAllow != null && lowAlarmIsACQWave != null)
            {
                AlarmCategory category = new AlarmCategory() { Value = lowAlarmValue ?? 0, IsAllow = lowAlarmIsAllow ?? false, IsACQWave = lowAlarmIsACQWave ?? false };
                categorys.Add(category);
            }
            if (lowDangerValue != null && lowDangerIsAllow != null && lowDangerIsACQWave != null)
            {
                AlarmCategory category = new AlarmCategory() { Value = lowDangerValue ?? 0, IsAllow = lowDangerIsAllow ?? false, IsACQWave = lowDangerIsACQWave ?? false };
                categorys.Add(category);
            }
            return categorys;
        }

        public static bool VerifyAlarmCategory(List<AlarmCategory> categorys)
        {
            if (categorys == null || categorys.Count == 0)
            {
                return false;
            }
            bool right = true;
            for (int i = 0; i < categorys.Count - 1; i++)
            {
                for(int j = i + 1; j < categorys.Count; j++)
                {
                    if (categorys[i].Value < categorys[j].Value)
                    {
                        right = false;
                        break;
                    }
                }
            }
            return right;
        }

        public static void GetOrganizationStructure(IList<T2_Organization> organizations, List<OrganizationTreeItemViewModel> organizationTrees, bool root = true)
        {
            //遍历树结构
            foreach (var organizationTree in organizationTrees)
            {
                if (organizationTree.T_Organization != null)
                {
                    var organization = organizations.Where(p => p.Guid == organizationTree.T_Organization.Guid).FirstOrDefault();
                    if (organization != null)
                    {
                        organization.Structure = NamesConvertToStructure(organizationTree.Names);
                        GetOrganizationStructure(organizations, organizationTree.Children.ToList(), false);
                    }
                    else
                    {
                        Console.WriteLine("find more null organization");
                    }
                }
            }
            if (root == true)
            {
                //删除多余的organization
                for (int i = organizations.Count - 1; i >= 0; i--)
                {
                    if (organizations[i].Structure == null)
                    {
                        organizations.Remove(organizations[i]);
                    }
                }
            }
        }

        public static void GetItemStructure(IList<T2_Item> items, List<ItemTreeItemViewModel> itemTrees)
        {
            //遍历树结构
            foreach (var itemTree in itemTrees)
            {
                if (itemTree.T_Item != null)
                {
                    var item = items.Where(p => p.Guid == itemTree.T_Item.Guid).FirstOrDefault();
                    if (item != null)
                    {
                        item.Structure = NamesConvertToStructure(itemTree.Names);
                    }
                    else
                    {
                        Console.WriteLine("find more null item");
                    }
                }
            }
            //删除多余的item
            for (int i = items.Count - 1; i >= 0; i--)
            {
                if (items[i].Structure == null)
                {
                    items.Remove(items[i]);
                }
            }
        }

        public static void GetAbstractChannelInfo(IList<I_WirelessChannelExport> channels, List<ServerTreeItemViewModel> serverTrees, ICardProcess _cardProcess)
        {
            //遍历树结构
            var channelTrees = _cardProcess.GetChannels(serverTrees);
            foreach (ChannelTreeItemViewModel channelTree in channelTrees)
            {
                var channel = channels.Where(p => p.T_AbstractChannelInfo_Code == channelTree.IChannel.T_AbstractChannelInfo.Code).FirstOrDefault();
                if (channel != null)
                {
                    if (channel is T2_WirelessVibrationChannelInfo)
                    {
                        T2_WirelessVibrationChannelInfo wirelessvibrationchannel = channel as T2_WirelessVibrationChannelInfo;
                        wirelessvibrationchannel.DefaultRPM = (channelTree.IChannel as WirelessVibrationChannelInfo).DefaultRPM;
                        wirelessvibrationchannel.VelocityCalibration = (channelTree.IChannel as WirelessVibrationChannelInfo).VelocityCalibration;
                        wirelessvibrationchannel.DisplacementCalibration = (channelTree.IChannel as WirelessVibrationChannelInfo).DisplacementCalibration;
                    }


                    //channel.T_AbstractChannelInfo = channelTree.IChannel.T_AbstractChannelInfo;
                    channel.T_Item_Name = channelTree.IChannel.T_AbstractChannelInfo.T_Item_Name;
                    channel.CHNum = channelTree.IChannel.T_AbstractChannelInfo.CHNum;
                    channel.SubCHNum = channelTree.IChannel.T_AbstractChannelInfo.SubCHNum;
                    channel.IsBypass = channelTree.IChannel.T_AbstractChannelInfo.IsBypass;
                    channel.Unit = channelTree.IChannel.T_AbstractChannelInfo.Unit;
                    channel.DelayAlarmTime = channelTree.IChannel.T_AbstractChannelInfo.DelayAlarmTime;
                    channel.NotOKDelayAlarmTime = channelTree.IChannel.T_AbstractChannelInfo.NotOKDelayAlarmTime;
                    channel.MainControlCardIP = channelTree.MainControlCardIP;
                    channel.SlaveIdentifier = channelTree.SlaveIdentifier;
                    channel.SlotNum = channelTree.SlotNum;
                    var category = channelTree.IChannel.AlarmStrategy.Absolute.Category;
                    var dangercategory = GetAlarmValue(category, "危险");
                    var alarmcategory = GetAlarmValue(category, "警告");
                    var prealarmcategory = GetAlarmValue(category, "预警");
                    var normalcategory = GetAlarmValue(category, "正常");
                    var lowdangercategory = GetAlarmValue(category, "低危险");
                    var lowalarmcategory = GetAlarmValue(category, "低警告");
                    var lowprealarmcategory = GetAlarmValue(category, "低预警");
                    var lownormalcategory = GetAlarmValue(category, "低正常");
                    if (dangercategory != null)
                    {
                        channel.DangerValue = dangercategory.Value;
                        channel.DangerIsAllow = dangercategory.IsAllow;
                        channel.DangerIsACQWave = dangercategory.IsACQWave;
                    }
                    if (alarmcategory != null)
                    {
                        channel.AlarmValue = alarmcategory.Value;
                        channel.AlarmIsAllow = alarmcategory.IsAllow;
                        channel.AlarmIsACQWave = alarmcategory.IsACQWave;
                    }
                    if (prealarmcategory != null)
                    {
                        channel.PreAlarmValue = prealarmcategory.Value;
                        channel.PreAlarmIsAllow = prealarmcategory.IsAllow;
                        channel.PreAlarmIsACQWave = prealarmcategory.IsACQWave;
                    }
                    if (normalcategory != null)
                    {
                        channel.NormalValue = normalcategory.Value;
                        channel.NormalIsAllow = normalcategory.IsAllow;
                        channel.NormalIsACQWave = normalcategory.IsACQWave;
                    }
                    if (lowdangercategory != null)
                    {
                        channel.LowDangerValue = lowdangercategory.Value;
                        channel.LowDangerIsAllow = lowdangercategory.IsAllow;
                        channel.LowDangerIsACQWave = lowdangercategory.IsACQWave;
                    }
                    if (lowalarmcategory != null)
                    {
                        channel.LowAlarmValue = lowalarmcategory.Value;
                        channel.LowAlarmIsAllow = lowalarmcategory.IsAllow;
                        channel.LowAlarmIsACQWave = lowalarmcategory.IsACQWave;
                    }
                    if (lowprealarmcategory != null)
                    {
                        channel.LowPreAlarmValue = lowprealarmcategory.Value;
                        channel.LowPreAlarmIsAllow = lowprealarmcategory.IsAllow;
                        channel.LowPreAlarmIsACQWave = lowprealarmcategory.IsACQWave;
                    }
                    if (lownormalcategory != null)
                    {
                        channel.LowNormalValue = lownormalcategory.Value;
                        channel.LowNormalIsAllow = lownormalcategory.IsAllow;
                        channel.LowNormalIsACQWave = lownormalcategory.IsACQWave;
                    }

                }
                else
                {
                    Console.WriteLine("find more null channel");
                }
            }
        }

        public static void OrganizationLeftCopyToRight(T2_Organization left, T2_Organization right)
        {
            if (right == null)
            {
                return;
            }
            right.Name = left.Name;
            right.Level = left.Level;
            right.Sort_No = left.Sort_No;
            right.NodeType = left.NodeType;
            right.Remarks = left.Remarks;
        }

        public static bool OrganizationEqual(T2_Organization left, T2_Organization right)
        {
            if (left == null || right == null)
            {
                return false;
            }
            if (right.Name != left.Name || right.Level != left.Level || right.Sort_No != left.Sort_No || right.NodeType != left.NodeType || right.Remarks != left.Remarks)
            {
                return false;
            }
            return true;
        }    

        public static void ItemLeftCopyToRight(T2_Item left, T2_Item right)
        {
            if (right == null)
            {
                return;
            }
            right.Name = left.Name;
            right.IP = left.IP;
            right.SlaveIdentifier = left.SlaveIdentifier;
            right.CardNum = left.CardNum;
            right.SlotNum = left.SlotNum;
            right.CHNum = left.CHNum;
            right.ItemType = left.ItemType;
            right.Remarks = left.Remarks;
        }

        public static bool ItemEqual(T2_Item left, T2_Item right)
        {
            if (left == null || right == null)
            {
                return false;
            }
            if (right.Name != left.Name || right.IP != left.IP || right.SlaveIdentifier.PadLeft(4, '0') != left.SlaveIdentifier.PadLeft(4, '0') || right.CardNum != left.CardNum
                || right.SlotNum != left.SlotNum || right.CHNum != left.CHNum || right.ItemType != left.ItemType || right.Remarks != left.Remarks)
            {
                return false;
            }
            return true;
        }

        public static void WirelessScalarChannelInfoLeftCopyToRight(T2_WirelessScalarChannelInfo left, T2_WirelessScalarChannelInfo right)
        {
            if (right == null)
            {
                return;
            }
            right.Unit = left.Unit;
            right.IsBypass = left.IsBypass;
            right.DelayAlarmTime = left.DelayAlarmTime;
            right.NotOKDelayAlarmTime = left.NotOKDelayAlarmTime;
            right.AlarmCategory = left.AlarmCategory;
        }

        public static bool WirelessScalarChannelInfoEqual(T2_WirelessScalarChannelInfo left, T2_WirelessScalarChannelInfo right)
        {
            if (left == null || right == null)
            {
                return false;
            }
            if ((right.Unit??"") != (left.Unit??"") || right.IsBypass != left.IsBypass || right.DelayAlarmTime != left.DelayAlarmTime || right.NotOKDelayAlarmTime != left.NotOKDelayAlarmTime
                || right.DangerValue != left.DangerValue || right.DangerIsAllow != left.DangerIsAllow || right.AlarmValue != left.AlarmValue || right.AlarmIsAllow != left.AlarmIsAllow
                 || right.PreAlarmValue != left.PreAlarmValue || right.PreAlarmIsAllow != left.PreAlarmIsAllow || right.NormalValue != left.NormalValue || right.NormalIsAllow != left.NormalIsAllow
                  || right.LowNormalValue != left.LowNormalValue || right.LowNormalIsAllow != left.LowNormalIsAllow || right.LowPreAlarmValue != left.LowPreAlarmValue || right.LowPreAlarmIsAllow != left.LowPreAlarmIsAllow
                   || right.LowAlarmValue != left.LowAlarmValue || right.LowAlarmIsAllow != left.LowAlarmIsAllow || right.LowDangerValue != left.LowDangerValue || right.LowDangerIsAllow != left.LowDangerIsAllow)
            {
                return false;
            }
            return true;
        }

        public static void WirelessVibrationChannelInfoLeftCopyToRight(T2_WirelessVibrationChannelInfo left, T2_WirelessVibrationChannelInfo right)
        {
            if (right == null)
            {
                return;
            }
            right.IsBypass = left.IsBypass;
            right.DelayAlarmTime = left.DelayAlarmTime;
            right.NotOKDelayAlarmTime = left.NotOKDelayAlarmTime;           
            right.DefaultRPM = left.DefaultRPM;
            right.VelocityCalibration = left.VelocityCalibration;
            right.DisplacementCalibration = left.DisplacementCalibration;
            right.AlarmCategory = left.AlarmCategory;
        }

        public static bool WirelessVibrationChannelInfoEqual(T2_WirelessVibrationChannelInfo left, T2_WirelessVibrationChannelInfo right)
        {
            if (left == null || right == null)
            {
                return false;
            }
            if (right.IsBypass != left.IsBypass || right.DelayAlarmTime != left.DelayAlarmTime || right.NotOKDelayAlarmTime != left.NotOKDelayAlarmTime
               || right.DefaultRPM != left.DefaultRPM || right.VelocityCalibration != left.VelocityCalibration || right.DisplacementCalibration != left.DisplacementCalibration
                || right.DangerValue != left.DangerValue || right.DangerIsAllow != left.DangerIsAllow || right.AlarmValue != left.AlarmValue || right.AlarmIsAllow != left.AlarmIsAllow
                 || right.PreAlarmValue != left.PreAlarmValue || right.PreAlarmIsAllow != left.PreAlarmIsAllow || right.NormalValue != left.NormalValue || right.NormalIsAllow != left.NormalIsAllow
                  || right.LowNormalValue != left.LowNormalValue || right.LowNormalIsAllow != left.LowNormalIsAllow || right.LowPreAlarmValue != left.LowPreAlarmValue || right.LowPreAlarmIsAllow != left.LowPreAlarmIsAllow
                   || right.LowAlarmValue != left.LowAlarmValue || right.LowAlarmIsAllow != left.LowAlarmIsAllow || right.LowDangerValue != left.LowDangerValue || right.LowDangerIsAllow != left.LowDangerIsAllow)
            {
                return false;
            }
            return true;
        }

    }
}
