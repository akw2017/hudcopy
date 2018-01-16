using AIC.Core.Helpers;
using AIC.Core.Models;
using AIC.ServiceInterface;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIC.PDAPage.Models
{
    class CardCopyHelper
    {
        #region 主板参数
        public static List<CardParaCopyModel> MainControlCardCopy(MainControlCard old, MainControlCard copy)
        {
            List<CardParaCopyModel> paras = new List<CardParaCopyModel>();
            paras.Add(new CardParaCopyModel()
            {
                Name = "传输种类",
                CopyChecked = true,
                CopyType = 1,
                Category = (from p in copy.CommunicationCategory select p as ICategory).ToList(),
                //CopyBoolPara, 
                //OldBoolPara,
                //NewBoolPara,
                CopyIntPara = copy.CommunicationCode,
                OldIntPara = old.CommunicationCode,
                NewIntPara = copy.CommunicationCode,
                //CopyStringPara,  
                //OldStringPara,   
                //NewStringPara,   
            });
            paras.Add(new CardParaCopyModel()
            {
                Name = "别名",
                CopyChecked = true,
                CopyType = 2,
                //Category = new List<ICategory>() { new BaseCategory() { Code = 0, Name = "" }},
                //CopyBoolPara = copy.IsAlarmLatch,
                //OldBoolPara = old.IsAlarmLatch,
                //NewBoolPara = copy.IsAlarmLatch,
                //CopyIntPara = 0,
                //OldIntPara = 0,
                //NewIntPara = 0,
                CopyStringPara = copy.AliasName,
                OldStringPara = old.AliasName,
                NewStringPara = copy.AliasName,
            });
            paras.Add(new CardParaCopyModel()
            {
                Name = "报警自锁",
                CopyChecked = true,
                CopyType = 0,
                //Category = new List<ICategory>() { new BaseCategory() { Code = 0, Name = "" }},
                CopyBoolPara = copy.IsAlarmLatch,
                OldBoolPara = old.IsAlarmLatch,
                NewBoolPara = copy.IsAlarmLatch,
                //CopyIntPara = 0,
                //OldIntPara = 0,
                //NewIntPara = 0,
                //CopyStringPara = copy.ScaleDataRange.ToString(),
                //OldStringPara = old.ScaleDataRange.ToString(),
                //NewStringPara = copy.ScaleDataRange.ToString(),
            });
            paras.Add(new CardParaCopyModel()
            {
                Name = "软件允许配置",
                CopyChecked = true,
                CopyType = 0,
                //Category = new List<ICategory>() { new BaseCategory() { Code = 0, Name = "" }},
                CopyBoolPara = copy.IsConfiguration,
                OldBoolPara = old.IsConfiguration,
                NewBoolPara = copy.IsConfiguration,
                //CopyIntPara = 0,
                //OldIntPara = 0,
                //NewIntPara = 0,
                //CopyStringPara = copy.ScaleDataRange.ToString(),
                //OldStringPara = old.ScaleDataRange.ToString(),
                //NewStringPara = copy.ScaleDataRange.ToString(),
            });
            paras.Add(new CardParaCopyModel()
            {
                Name = "监听",
                CopyChecked = true,
                CopyType = 0,
                //Category = new List<ICategory>() { new BaseCategory() { Code = 0, Name = "" }},
                CopyBoolPara = copy.IsListen,
                OldBoolPara = old.IsListen,
                NewBoolPara = copy.IsListen,
                //CopyIntPara = 0,
                //OldIntPara = 0,
                //NewIntPara = 0,
                //CopyStringPara = copy.ScaleDataRange.ToString(),
                //OldStringPara = old.ScaleDataRange.ToString(),
                //NewStringPara = copy.ScaleDataRange.ToString(),
            });
            paras.Add(new CardParaCopyModel()
            {
                Name = "异步/同步",
                CopyChecked = true,
                CopyType = 1,
                Category = new List<ICategory>() {
                    new BaseCategory() { Code = 0, Name = "异步" },
                    new BaseCategory() { Code = 0, Name = "同步" },
                    new BaseCategory() { Code = 0, Name = "伪同步" }
                },
                //CopyBoolPara, 
                //OldBoolPara,
                //NewBoolPara,
                CopyIntPara = copy.AsySyn,
                OldIntPara = old.AsySyn,
                NewIntPara = copy.AsySyn,
                //CopyStringPara,  
                //OldStringPara,   
                //NewStringPara,   
            });
            paras.Add(new CardParaCopyModel()
            {
                Name = "语言种类",
                CopyChecked = true,
                CopyType = 1,
                Category = (from p in copy.LanguageCategory select p as ICategory).ToList(),
                //CopyBoolPara, 
                //OldBoolPara,
                //NewBoolPara,
                CopyIntPara = copy.LanguageCode,
                OldIntPara = old.LanguageCode,
                NewIntPara = copy.LanguageCode,
                //CopyStringPara,  
                //OldStringPara,   
                //NewStringPara,   
            });
            paras.Add(new CardParaCopyModel()
            {
                Name = "主板种类",
                CopyChecked = true,
                CopyType = 1,
                Category = (from p in copy.MainCardCategory select p as ICategory).ToList(),
                //CopyBoolPara, 
                //OldBoolPara,
                //NewBoolPara,
                CopyIntPara = copy.MainCardCode,
                OldIntPara = old.MainCardCode,
                NewIntPara = copy.MainCardCode,
                //CopyStringPara,  
                //OldStringPara,   
                //NewStringPara,   
            });
            paras.Add(new CardParaCopyModel()
            {
                Name = "采样方式",
                CopyChecked = true,
                CopyType = 1,
                Category = new List<ICategory>() {
                    new BaseCategory() { Code = copy.SampleMode.FreeSample.Code, Name = copy.SampleMode.FreeSample.Name },
                    new BaseCategory() { Code = copy.SampleMode.RPMTriggerSample.Code, Name = copy.SampleMode.RPMTriggerSample.Name },
                    new BaseCategory() { Code = copy.SampleMode.EqualCycleSample.Code, Name = copy.SampleMode.EqualCycleSample.Name },
                    new BaseCategory() { Code = copy.SampleMode.EqualAngleSample.Code, Name = copy.SampleMode.EqualAngleSample.Name },
                },
                //CopyBoolPara, 
                //OldBoolPara,
                //NewBoolPara,
                CopyIntPara = copy.SampleMode.Code,
                OldIntPara = old.SampleMode.Code,
                NewIntPara = copy.SampleMode.Code,
                //CopyStringPara,  
                //OldStringPara,   
                //NewStringPara,   
            });
            paras.Add(new CardParaCopyModel()
            {
                Name = "波形代码",
                CopyChecked = true,
                CopyType = 1,
                Category = (from p in copy.WaveCategory select p as ICategory).ToList(),
                //CopyBoolPara, 
                //OldBoolPara,
                //NewBoolPara,
                CopyIntPara = copy.SynWaveCode,
                OldIntPara = old.SynWaveCode,
                NewIntPara = copy.SynWaveCode,
                //CopyStringPara,  
                //OldStringPara,   
                //NewStringPara,   
            });
            paras.Add(new CardParaCopyModel()
            {
                Name = "比例数据量程",
                CopyChecked = true,
                CopyType = 2,
                //Category = new List<ICategory>() { new BaseCategory() { Code = 0, Name = "" }},
                //CopyBoolPara,
                //OldBoolPara,
                //NewBoolPara,
                //CopyIntPara=0,
                //OldIntPara=0,
                //NewIntPara=0,
                CopyStringPara = copy.ScaleDataRange.ToString(),
                OldStringPara = old.ScaleDataRange.ToString(),
                NewStringPara = copy.ScaleDataRange.ToString(),
            });

            return paras;

        }

        public static void CopyToMainControlCard(MainControlCard old, List<CardParaCopyModel> paras)
        {
            if (old == null)
            {
                return;
            }
            old.CommunicationCode = (from p in paras where p.Name == "传输种类" select p.NewIntPara).FirstOrDefault();
            old.AliasName = (from p in paras where p.Name == "别名" select p.NewStringPara).FirstOrDefault();
            old.IsAlarmLatch = (from p in paras where p.Name == "报警自锁" select p.NewBoolPara).FirstOrDefault();
            old.IsConfiguration = (from p in paras where p.Name == "软件允许配置" select p.NewBoolPara).FirstOrDefault();
            old.IsListen = (from p in paras where p.Name == "监听" select p.NewBoolPara).FirstOrDefault();
            old.AsySyn = (from p in paras where p.Name == "异步/同步" select p.NewIntPara).FirstOrDefault();
            old.LanguageCode = (from p in paras where p.Name == "语言种类" select p.NewIntPara).FirstOrDefault();
            old.MainCardCode = (from p in paras where p.Name == "主板种类" select p.NewIntPara).FirstOrDefault();
            old.SampleMode.Code = (from p in paras where p.Name == "采样方式" select p.NewIntPara).FirstOrDefault();
            old.SynWaveCode = (from p in paras where p.Name == "波形代码" select p.NewIntPara).FirstOrDefault();
            old.ScaleDataRange = (from p in paras where p.Name == "比例数据量程" select Convert.ToSingle(p.NewStringPara)).FirstOrDefault();
        }
        #endregion

        #region 主板
        public static void MainControlCardLeftCopyToRight(MainControlCard left, MainControlCard right)
        {
            if (right == null)
            {
                return;
            }
            right.CommunicationCategory = left.CommunicationCategory;
            right.CommunicationCode = left.CommunicationCode;
            right.Identifier = left.Identifier;
            right.AliasName = left.AliasName;
            right.DataSourceCategory = left.DataSourceCategory;
            right.DataSourceCode = left.DataSourceCode;
            right.IsAlarmLatch = left.IsAlarmLatch;
            right.IsConfiguration = left.IsConfiguration;
            right.IsHdBypass = left.IsHdBypass;
            right.IsHdConfiguration = left.IsHdConfiguration;
            right.IsHdMultiplication = left.IsHdMultiplication;
            right.IsListen = left.IsListen;
            right.AsySyn = left.AsySyn;
            right.LanguageCategory = left.LanguageCategory;
            right.LanguageCode = left.LanguageCode;
            right.MainCardCategory = left.MainCardCategory;
            right.MainCardCode = left.MainCardCode;

            if (right.SampleMode == null)
            {
                right.SampleMode = new SampleMode();
                right.SampleMode.FreeSample = left.SampleMode.FreeSample;
                right.SampleMode.RPMTriggerSample = left.SampleMode.RPMTriggerSample;
                right.SampleMode.EqualCycleSample = left.SampleMode.EqualCycleSample;
                right.SampleMode.EqualAngleSample = left.SampleMode.EqualAngleSample;
            }
            right.SampleMode.Code = left.SampleMode.Code;

            right.ServerIP = left.ServerIP;
            right.WaveCategory = left.WaveCategory;
            right.SynWaveCode = left.SynWaveCode;
            right.Version = left.Version;
            right.ScaleDataRange = left.ScaleDataRange;
        }
        #endregion
        #region 配板
        public static void WireMatchingCardLeftCopyToRight(WireMatchingCard left, WireMatchingCard right)
        {
            if (right == null)
            {
                return;
            }
            right.CardName = left.CardName;
            right.CardNum = left.CardNum;
        }
        #endregion
        #region 无线接受卡
        public static void WirelessReceiveCardLeftCopyToRight(WirelessReceiveCard left, WirelessReceiveCard right)
        {
            if (right == null)
            {
                return;
            }
            right.ReceiveCardName = left.ReceiveCardName;
            right.MasterIdentifier = left.MasterIdentifier;
        }
        #endregion
        #region 传输卡
        public static void TransmissionCardLeftCopyToRight(TransmissionCard left, TransmissionCard right)
        {
            if (right == null)
            {
                return;
            }
            right.SlaveIdentifier = left.SlaveIdentifier;
            right.TransmissionType = left.TransmissionType;
            right.Version = left.Version;
            right.TransmissionName = left.TransmissionName;
            right.WorkTime = left.WorkTime;
            right.SleepTime = left.SleepTime;
            right.BatteryEnergy = left.BatteryEnergy;
            right.Remarks = left.Remarks;
        }
        #endregion
        #region 槽
        public static void SlotLeftCopyToRight(ISlot left, ISlot right)
        {
            if (left is WirelessVibrationSlot)
            {
                VibrationSlotLeftCopyToRight(left as WirelessVibrationSlot, right as WirelessVibrationSlot);
            }
            else if (left is IEPESlot)
            {
                IEPESlotLeftCopyToRight(left as IEPESlot, right as IEPESlot);
            }
            else if (left is EddyCurrentDisplacementSlot)
            {
                EddyCurrentDisplacementSlotLeftCopyToRight(left as EddyCurrentDisplacementSlot, right as EddyCurrentDisplacementSlot);
            }
            else if (left is EddyCurrentKeyPhaseSlot)
            {
                EddyCurrentKeyPhaseSlotLeftCopyToRight(left as EddyCurrentKeyPhaseSlot, right as EddyCurrentKeyPhaseSlot);
            }
            else if (left is EddyCurrentTachometerSlot)
            {
                EddyCurrentTachometerSlotLeftCopyToRight(left as EddyCurrentTachometerSlot, right as EddyCurrentTachometerSlot);
            }
            else if (left is DigitTachometerSlot)
            {
                DigitTachometerSlotLeftCopyToRight(left as DigitTachometerSlot, right as DigitTachometerSlot);
            }
            else if (left is AnalogRransducerInSlot)
            {
                AnalogRransducerInSlotLeftCopyToRight(left as AnalogRransducerInSlot, right as AnalogRransducerInSlot);
            }
            else if (left is RelaySlot)
            {
                RelaySlotLeftCopyToRight(left as RelaySlot, right as RelaySlot);
            }
            else if (left is DigitRransducerInSlot)
            {
                DigitRransducerInSlotLeftCopyToRight(left as DigitRransducerInSlot, right as DigitRransducerInSlot);
            }
            else if (left is DigitRransducerOutSlot)
            {
                DigitRransducerOutSlotLeftCopyToRight(left as DigitRransducerOutSlot, right as DigitRransducerOutSlot);
            }
            else if (left is AnalogRransducerOutSlot)
            {
                AnalogRransducerOutSlotLeftCopyToRight(left as AnalogRransducerOutSlot, right as AnalogRransducerOutSlot);
            }
        }

        public static void VibrationSlotLeftCopyToRight(WirelessVibrationSlot left, WirelessVibrationSlot right)
        {
            if (right == null)
            {
                return;
            }
            right.Integration = left.Integration;
            right.Unit = left.Unit;
            right.SlotNum = left.SlotNum;
            right.SampleFreCategory = left.SampleFreCategory;
            right.SampleFreCode = left.SampleFreCode;
            right.SamplePointCategory = left.SamplePointCategory;
            right.SamplePointCode = left.SamplePointCode;
        }

        public static void IEPESlotLeftCopyToRight(IEPESlot left, IEPESlot right)
        {
            if (right == null)
            {
                return;
            }
            right.Integration = left.Integration;
            VibrationSlotInfoLeftCopyToRight(left, right);
        }      

        public static void EddyCurrentDisplacementSlotLeftCopyToRight(EddyCurrentDisplacementSlot left, EddyCurrentDisplacementSlot right)
        {
            if (right == null)
            {
                return;
            }
            right.Is24V = left.Is24V;
            VibrationSlotInfoLeftCopyToRight(left, right);
        }

        public static void EddyCurrentKeyPhaseSlotLeftCopyToRight(EddyCurrentKeyPhaseSlot left, EddyCurrentKeyPhaseSlot right)
        {
            if (right == null)
            {
                return;
            }
            right.Is24V = left.Is24V;
            EddyCurrentRPMSlotInfoCopyToRight(left, right);
        }

        public static void EddyCurrentTachometerSlotLeftCopyToRight(EddyCurrentTachometerSlot left, EddyCurrentTachometerSlot right)
        {
            if (right == null)
            {
                return;
            }
            right.Is24V = left.Is24V;
            right.IsEnableMainCH = left.IsEnableMainCH;
            EddyCurrentRPMSlotInfoCopyToRight(left, right);
        }

        public static void DigitTachometerSlotLeftCopyToRight(DigitTachometerSlot left, DigitTachometerSlot right)
        {
            if (right == null)
            {
                return;
            }
            AbstractSlotInfoLeftCopyToRight(left, right);
        }

        public static void AnalogRransducerInSlotLeftCopyToRight(AnalogRransducerInSlot left, AnalogRransducerInSlot right)
        {
            if (right == null)
            {
                return;
            }
            AbstractSlotInfoLeftCopyToRight(left, right);
        }

        public static void RelaySlotLeftCopyToRight(RelaySlot left, RelaySlot right)
        {
            if (right == null)
            {
                return;
            }
            AbstractSlotInfoLeftCopyToRight(left, right);
        }

        public static void DigitRransducerInSlotLeftCopyToRight(DigitRransducerInSlot left, DigitRransducerInSlot right)
        {
            if (right == null)
            {
                return;
            }
            AbstractSlotInfoLeftCopyToRight(left, right);
        }

        public static void DigitRransducerOutSlotLeftCopyToRight(DigitRransducerOutSlot left, DigitRransducerOutSlot right)
        {
            if (right == null)
            {
                return;
            }
            AbstractSlotInfoLeftCopyToRight(left, right);
        }

        public static void AnalogRransducerOutSlotLeftCopyToRight(AnalogRransducerOutSlot left, AnalogRransducerOutSlot right)
        {
            if (right == null)
            {
                return;
            }
            AbstractSlotInfoLeftCopyToRight(left, right);
        }

        public static void VibrationSlotInfoLeftCopyToRight(VibrationSlotInfo left, VibrationSlotInfo right)
        {
            right.HighPassCategory = left.HighPassCategory;
            right.HighPassCode = left.HighPassCode;
            right.WaveCategory = left.WaveCategory;
            right.WaveCode = left.WaveCode;
            if (right.SampleMode == null)
            {
                right.SampleMode = new SampleMode();
                right.SampleMode.FreeSample = left.SampleMode.FreeSample;
                right.SampleMode.RPMTriggerSample = left.SampleMode.RPMTriggerSample;
                right.SampleMode.EqualCycleSample = left.SampleMode.EqualCycleSample;
                right.SampleMode.EqualAngleSample = left.SampleMode.EqualAngleSample;
            }
            right.SampleMode.Code = left.SampleMode.Code;
            AbstractSlotInfoLeftCopyToRight(left, right);
        }

        public static void EddyCurrentRPMSlotInfoCopyToRight(EddyCurrentRPMSlotInfo left, EddyCurrentRPMSlotInfo right)
        {
            right.EddyCurrentRPMSample = left.EddyCurrentRPMSample;
            right.EddyCurrentRPMCode = left.EddyCurrentRPMCode;           
            AbstractSlotInfoLeftCopyToRight(left, right);
        }

        public static void AbstractSlotInfoLeftCopyToRight(AbstractSlotInfo left, AbstractSlotInfo right)
        {
            right.InSignalCategory = left.InSignalCategory;
            right.InSignalCode = left.InSignalCode;
            right.SlotNum = left.SlotNum;
            right.SlotName = left.SlotName;
            right.UploadIntevalTime = left.UploadIntevalTime;
            right.IsInput = left.IsInput;
            right.Unit = left.Unit;
            right.Version = left.Version;           
        }
        #endregion
        #region 通道
        public static void ChannelLeftCopyToRight(IChannel left, IChannel right)
        {
            if (left is IEPEChannelInfo)
            {
                IEPEChannelInfoLeftCopyToRight(left as IEPEChannelInfo, right as IEPEChannelInfo);
            }
            else if (left is EddyCurrentDisplacementChannelInfo)
            {
                EddyCurrentDisplacementChannelInfoLeftCopyToRight(left as EddyCurrentDisplacementChannelInfo, right as EddyCurrentDisplacementChannelInfo);
            }
            else if (left is EddyCurrentKeyPhaseChannelInfo)
            {
                EddyCurrentKeyPhaseChannelInfoLeftCopyToRight(left as EddyCurrentKeyPhaseChannelInfo, right as EddyCurrentKeyPhaseChannelInfo);
            }
            else if (left is EddyCurrentTachometerChannelInfo)
            {
                EddyCurrentTachometerChannelInfoLeftCopyToRight(left as EddyCurrentTachometerChannelInfo, right as EddyCurrentTachometerChannelInfo);
            }
            else if (left is DigitTachometerChannelInfo)
            {
                DigitTachometerChannelInfoLeftCopyToRight(left as DigitTachometerChannelInfo, right as DigitTachometerChannelInfo);
            }
            else if (left is AnalogRransducerInChannelInfo)
            {
                AnalogRransducerInChannelInfoLeftCopyToRight(left as AnalogRransducerInChannelInfo, right as AnalogRransducerInChannelInfo);
            }
            else if (left is RelayChannelInfo)
            {
                RelayChannelInfoLeftCopyToRight(left as RelayChannelInfo, right as RelayChannelInfo);
            }
            else if (left is DigitRransducerInChannelInfo)
            {
                DigitRransducerInChannelInfoLeftCopyToRight(left as DigitRransducerInChannelInfo, right as DigitRransducerInChannelInfo);
            }
            else if (left is DigitRransducerOutChannelInfo)
            {
                DigitRransducerOutChannelInfoLeftCopyToRight(left as DigitRransducerOutChannelInfo, right as DigitRransducerOutChannelInfo);
            }
            else if (left is AnalogRransducerOutChannelInfo)
            {
                AnalogRransducerOutChannelInfoLeftCopyToRight(left as AnalogRransducerOutChannelInfo, right as AnalogRransducerOutChannelInfo);
            }
            else if (left is WirelessScalarChannelInfo)
            {
                WirelessScalarChannelInfoLeftCopyToRight(left as WirelessScalarChannelInfo, right as WirelessScalarChannelInfo);
            }
            else if (left is WirelessVibrationChannelInfo)
            {
                WirelessVibrationChannelInfoLeftCopyToRight(left as WirelessVibrationChannelInfo, right as WirelessVibrationChannelInfo);
            }
        }

        public static void WirelessScalarChannelInfoLeftCopyToRight(WirelessScalarChannelInfo left, WirelessScalarChannelInfo right)
        {
            if (right == null)
            {
                return;
            }
            TransformMethodLeftCopyToRight(left, right);
        }

        public static void WirelessVibrationChannelInfoLeftCopyToRight(WirelessVibrationChannelInfo left, WirelessVibrationChannelInfo right)
        {
            if (right == null)
            {
                return;
            }
            right.VelocityCalibration = left.VelocityCalibration;
            right.DisplacementCalibration = left.DisplacementCalibration;
            VibrationChannelInfoLeftCopyToRight(left, right);
        }

        public static void IEPEChannelInfoLeftCopyToRight(IEPEChannelInfo left, IEPEChannelInfo right)
        {
            if (right == null)
            {
                return;
            }
            right.VelocityCalibration = left.VelocityCalibration;
            right.DisplacementCalibration = left.DisplacementCalibration;
            VibrationChannelInfoLeftCopyToRight(left, right);
        }

        public static void EddyCurrentDisplacementChannelInfoLeftCopyToRight(EddyCurrentDisplacementChannelInfo left, EddyCurrentDisplacementChannelInfo right)
        {
            if (right == null)
            {
                return;
            }           
            VibrationChannelInfoLeftCopyToRight(left, right);
        }

        public static void EddyCurrentKeyPhaseChannelInfoLeftCopyToRight(EddyCurrentKeyPhaseChannelInfo left, EddyCurrentKeyPhaseChannelInfo right)
        {
            if (right == null)
            {
                return;
            }
            EddyCurrentRPMChannelInfoLeftCopyToRight(left, right);
        }

        public static void EddyCurrentTachometerChannelInfoLeftCopyToRight(EddyCurrentTachometerChannelInfo left, EddyCurrentTachometerChannelInfo right)
        {
            if (right == null)
            {
                return;
            }
            right.RPMCouplingCategory = left.RPMCouplingCategory;
            right.RPMCouplingCode = left.RPMCouplingCode;
            EddyCurrentRPMChannelInfoLeftCopyToRight(left, right);
        }

        public static void DigitTachometerChannelInfoLeftCopyToRight(DigitTachometerChannelInfo left, DigitTachometerChannelInfo right)
        {
            if (right == null)
            {
                return;
            }
            RPMChannelInfoLeftCopyToRight(left, right);
        }

        public static void AnalogRransducerInChannelInfoLeftCopyToRight(AnalogRransducerInChannelInfo left, AnalogRransducerInChannelInfo right)
        {
            if (right == null)
            {
                return;
            }
            TransformMethodLeftCopyToRight(left, right);
        }

        public static void RelayChannelInfoLeftCopyToRight(RelayChannelInfo left, RelayChannelInfo right)
        {
            if (right == null)
            {
                return;
            }
            AbstractChannelInfoLeftCopyToRight(left, right);
        }

        public static void DigitRransducerInChannelInfoLeftCopyToRight(DigitRransducerInChannelInfo left, DigitRransducerInChannelInfo right)
        {
            if (right == null)
            {
                return;
            }
            DigitRransducerAdditionLeftCopyToRight(left, right);
        }

        public static void DigitRransducerOutChannelInfoLeftCopyToRight(DigitRransducerOutChannelInfo left, DigitRransducerOutChannelInfo right)
        {
            if (right == null)
            {
                return;
            }
            SourceChannelInfoLeftCopyToRight(left, right);
        }

        public static void AnalogRransducerOutChannelInfoLeftCopyToRight(AnalogRransducerOutChannelInfo left, AnalogRransducerOutChannelInfo right)
        {
            if (right == null)
            {
                return;
            }
            SourceChannelInfoLeftCopyToRight(left, right);
        }

        public static void VibrationChannelInfoLeftCopyToRight(VibrationChannelInfo left, VibrationChannelInfo right)
        {
            if (right == null)
            {
                return;
            }
            right.RPMCardNum = left.RPMCardNum;
            right.RPMSlotNum = left.RPMSlotNum;
            right.RPMCHNum = left.RPMCHNum;
            right.IsMultiplication = left.IsMultiplication;
            right.MultiplicationCor = left.MultiplicationCor;
            right.IsSaveWaveToSD = left.IsSaveWaveToSD;
            right.IsUploadWave = left.IsUploadWave;
            right.DefaultRPM = left.DefaultRPM;
            VibrationAdditionLeftCopyToRight(left, right);
        }

        public static void VibrationAdditionLeftCopyToRight(VibrationAddition left, VibrationAddition right)
        {
            if (right == null)
            {
                return;
            }
            right.TPDirCategory = left.TPDirCategory;
            right.TPDirCode = left.TPDirCode;
            right.BiasVoltHigh = left.BiasVoltHigh;
            right.BiasVoltLow = left.BiasVoltLow;
            right.Sensitivity = left.Sensitivity;
            RPMChannelInfoLeftCopyToRight(left, right);
        }

        public static void RPMChannelInfoLeftCopyToRight(RPMChannelInfo left, RPMChannelInfo right)
        {
            if (right == null)
            {
                return;
            }
            right.CalibrationCor = left.CalibrationCor;
            right.IsNotch = left.IsNotch;
            right.AverageNumber = left.AverageNumber;
            right.TeethNumber = left.TeethNumber;
            AbstractChannelInfoLeftCopyToRight(left, right);
        }

        public static void EddyCurrentRPMChannelInfoLeftCopyToRight(EddyCurrentRPMChannelInfo left, EddyCurrentRPMChannelInfo right)
        {
            if (right == null)
            {
                return;
            }
            right.ThresholdVolt = left.ThresholdVolt;
            right.HysteresisVolt = left.HysteresisVolt;
            right.ThresholdModeCategory = left.ThresholdModeCategory;
            right.ThresholdModeCode = left.ThresholdModeCode;
            VibrationAdditionLeftCopyToRight(left, right);
        }

        public static void TransformMethodLeftCopyToRight(TransformMethod left, TransformMethod right)
        {
            if (right == null)
            {
                return;
            }
            right.EquationCategory = left.EquationCategory;
            right.EquationCode = left.EquationCode;
            AbstractChannelInfoLeftCopyToRight(left, right);
        }

        public static void DigitRransducerAdditionLeftCopyToRight(DigitRransducerAddition left, DigitRransducerAddition right)
        {
            if (right == null)
            {
                return;
            }
            right.SwitchCategory = left.SwitchCategory;
            right.SwitchCode = left.SwitchCode;
            right.ModBusFunCategory = left.ModBusFunCategory;
            right.ModBusFunCode = left.ModBusFunCode;
            TransformMethodLeftCopyToRight(left, right);
        }

        public static void SourceChannelInfoLeftCopyToRight(SourceChannelInfo left, SourceChannelInfo right)
        {
            if (right == null)
            {
                return;
            }
            right.SourceCardNum = left.SourceCardNum;
            right.SourceSlotNum = left.SourceSlotNum;
            right.SourceCHNum = left.SourceCHNum;
            right.SourceSubCHNum = left.SourceSubCHNum;
            DigitRransducerAdditionLeftCopyToRight(left, right);
        }

        public static void AbstractChannelInfoLeftCopyToRight(AbstractChannelInfo left, AbstractChannelInfo right)
        {
            if (right == null)
            {
                return;
            }
            IDatabaseComponent _databaseComponent = ServiceLocator.Current.GetInstance<IDatabaseComponent>();
            right.UnitCategory = _databaseComponent.GetUnitCategory();

            right.Organization = left.Organization;
            right.T_Device_Name = left.T_Device_Name;
            right.T_Device_Code = left.T_Device_Code;
            right.T_Device_Guid = left.T_Device_Guid;
            right.T_Item_Name = left.T_Item_Name;
            right.T_Item_Code = left.T_Item_Code;
            right.T_Item_Guid = left.T_Item_Guid;
            right.CHNum = left.CHNum;
            right.SubCHNum = left.SubCHNum;
            right.IsUploadData = left.IsUploadData;
            right.Unit = left.Unit;
            right.SVTypeCategory = left.SVTypeCategory;
            right.SVTypeCode = left.SVTypeCode;
            right.LocalSaveCategory = left.LocalSaveCategory;
            right.LocalSaveCode = left.LocalSaveCode;
            right.IsBypass = left.IsBypass;
            right.DelayAlarmTime = left.DelayAlarmTime;
            right.NotOKDelayAlarmTime = left.NotOKDelayAlarmTime;
            right.IsLogic = left.IsLogic;
            right.LogicExpression = left.LogicExpression;
            right.Remarks = left.Remarks;
            right.Extra_Information = left.Extra_Information;
            if (right.AlarmStrategy == null)
            {
                right.AlarmStrategy = new AlarmStrategy();
            }
            if (right.AlarmStrategy.Absolute == null)
            {
                right.AlarmStrategy.Absolute = new AbsoluteAlarm();
            }
            if (right.AlarmStrategy.Comparative == null)
            {
                right.AlarmStrategy.Comparative = new ComparativeAlarm();
            }
            if (left.AlarmStrategy != null)
            {
                if (left.AlarmStrategy.Absolute != null)
                {
                    right.AlarmStrategy.Absolute.Category = left.AlarmStrategy.Absolute.Category;
                    right.AlarmStrategy.Absolute.Para = left.AlarmStrategy.Absolute.Para;
                    right.AlarmStrategy.Absolute.Mode = left.AlarmStrategy.Absolute.Mode;
                    right.AlarmStrategy.Absolute.ModeCode = left.AlarmStrategy.Absolute.ModeCode;                  
                }
                if (left.AlarmStrategy.Comparative != null)
                {
                    right.AlarmStrategy.Comparative.Range = left.AlarmStrategy.Comparative.Range;
                    right.AlarmStrategy.Comparative.IntevalTime = left.AlarmStrategy.Comparative.IntevalTime;
                    right.AlarmStrategy.Comparative.Percent = left.AlarmStrategy.Comparative.Percent;
                    right.AlarmStrategy.Comparative.IsAllow = left.AlarmStrategy.Comparative.IsAllow;
                    right.AlarmStrategy.Comparative.Para = left.AlarmStrategy.Comparative.Para;                    
                }
            }
        }
        #endregion
        #region 分频
        public static void DivFreInfoLeftCopyToRight(DivFreInfo left, DivFreInfo right)
        {
            if (right == null)
            {
                return;
            }
            right.Guid = left.Guid;
            right.Code = left.Code;
            right.Name = left.Name;
            right.Create_Time = left.Create_Time;
            right.Modify_Time = left.Modify_Time;
            right.Remarks = left.Remarks;
            right.T_Item_Guid = left.T_Item_Guid;
            right.T_Item_Name = left.T_Item_Name;
            right.T_Item_Code = left.T_Item_Code;
            right.DivFreCode = left.DivFreCode;
            right.BasedOnRPM = left.BasedOnRPM;
            right.FixedFre = left.FixedFre;
            right.BasedOnRange = left.BasedOnRange;
            if (right.AlarmStrategy == null)
            {
                right.AlarmStrategy = new AlarmStrategy();  
            }
            if (right.AlarmStrategy.Absolute == null)
            {
                right.AlarmStrategy.Absolute = new AbsoluteAlarm();
            }
            if (right.AlarmStrategy.Comparative == null)
            {
                right.AlarmStrategy.Comparative = new ComparativeAlarm();
            }
            if (left.AlarmStrategy != null)
            {
                if (left.AlarmStrategy.Absolute != null)
                {
                    right.AlarmStrategy.Absolute.Category = left.AlarmStrategy.Absolute.Category;
                    right.AlarmStrategy.Absolute.Para = left.AlarmStrategy.Absolute.Para;
                    right.AlarmStrategy.Absolute.Mode = left.AlarmStrategy.Absolute.Mode;
                    right.AlarmStrategy.Absolute.ModeCode = left.AlarmStrategy.Absolute.ModeCode;
                }
                if (left.AlarmStrategy.Comparative != null)
                {
                    right.AlarmStrategy.Comparative.Range = left.AlarmStrategy.Comparative.Range;
                    right.AlarmStrategy.Comparative.IntevalTime = left.AlarmStrategy.Comparative.IntevalTime;
                    right.AlarmStrategy.Comparative.Percent = left.AlarmStrategy.Comparative.Percent;
                    right.AlarmStrategy.Comparative.IsAllow = left.AlarmStrategy.Comparative.IsAllow;
                    right.AlarmStrategy.Comparative.Para = left.AlarmStrategy.Comparative.Para;
                }
            }
        }
        #endregion
    }
}