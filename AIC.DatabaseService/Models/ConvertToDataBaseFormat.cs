using AIC.Core.HardwareModels;
using AIC.Core.LMModels;
using AIC.Core.Models;
using AIC.Core.OrganizationModels;
using AIC.ServiceInterface;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIC.DatabaseService.Models
{
    public class ConvertToDataBaseFormat : IConvertToDataBaseFormat
    {
        public T1_MainControlCard MainControlCardConvert(MainControlCard card, string IP)
        {
            T1_MainControlCard t_card = new T1_MainControlCard();
            t_card.CommunicationCategory = JsonConvert.SerializeObject(card.CommunicationCategory);
            t_card.CommunicationCode = card.CommunicationCode;
            t_card.Identifier = card.Identifier;
            t_card.AliasName = card.AliasName;
            t_card.ACQ_Unit_Type = card.ACQ_Unit_Type;
            t_card.DataSourceCategory = JsonConvert.SerializeObject(card.DataSourceCategory);
            t_card.DataSourceCode = card.DataSourceCode;
            t_card.IsAlarmLatch = card.IsAlarmLatch;
            t_card.IsConfiguration = card.IsConfiguration;
            t_card.IsHdBypass = card.IsHdBypass;
            t_card.IsHdConfiguration = card.IsHdConfiguration;
            t_card.IsHdMultiplication = card.IsHdMultiplication;
            t_card.IsListen = card.IsListen;
            t_card.AsySyn = card.AsySyn;
            t_card.LanguageCategory = JsonConvert.SerializeObject(card.LanguageCategory);
            t_card.LanguageCode = card.LanguageCode;
            t_card.MainCardCategory = JsonConvert.SerializeObject(card.MainCardCategory);
            t_card.MainCardCode = card.MainCardCode;
            t_card.SampleMode = JsonConvert.SerializeObject(card.SampleMode);
            t_card.ServerIP = card.ServerIP;
            t_card.WaveCategory = JsonConvert.SerializeObject(card.WaveCategory);
            t_card.SynWaveCode = card.SynWaveCode;
            t_card.Version = card.Version;
            t_card.ScaleDataRange = card.ScaleDataRange;
            t_card.IP = IP;
            return t_card;
        }       

        public T1_WireMatchingCard WireMatchingCardConvert(WireMatchingCard card, string IP)
        {
            T1_WireMatchingCard t_card = new T1_WireMatchingCard();
            t_card.CardName = card.CardName;
            t_card.CardNum = card.CardNum;
            t_card.T_MainControlCard_IP = IP;
            //t_card.Code = IP + "_" + card.CardNum;
            t_card.Code = card.CardNum + "@" + IP;
            return t_card;
        }

        public T1_WirelessReceiveCard WirelessReceiveCardConvert(WirelessReceiveCard card, string IP)
        {
            T1_WirelessReceiveCard t_card = new T1_WirelessReceiveCard();
            t_card.MasterIdentifier = card.MasterIdentifier;
            t_card.ReceiveCardName = card.ReceiveCardName;
            t_card.T_MainControlCard_IP = IP;
            //t_card.Code = IP + "_" + card.MasterIdentifier;
            t_card.Code = card.MasterIdentifier + "@" + IP;
            return t_card;
        }

        public T1_TransmissionCard TransmissionCardConvert(TransmissionCard card, string IP, string masterIdentifier)
        {
            T1_TransmissionCard t_card = new T1_TransmissionCard();
            t_card.SlaveIdentifier = card.SlaveIdentifier;
            t_card.TransmissionType = card.TransmissionType;
            t_card.TransmissionName = card.TransmissionName;
            t_card.Version = card.Version;
            t_card.WorkTime = card.WorkTime;
            t_card.SleepTime = card.SleepTime;
            t_card.BatteryEnergy = card.BatteryEnergy;
            t_card.Remarks = card.Remarks;
            t_card.ExtraInfo = JsonConvert.SerializeObject(card.ExtraInfo);
            //t_card.T_WirelessReceiveCard_Code = IP + "_" + masterIdentifier;
            //t_card.Code = IP + "_" + card.SlaveIdentifier;
            t_card.T_WirelessReceiveCard_Code = masterIdentifier + "@" + IP;
            t_card.Code = card.SlaveIdentifier + "@" + IP;

            return t_card;
        }

        //有线
        public I_BaseSlot BaseSlotConvert(IWireSlot i_slot, string IP, int cardNum)
        {           
            if (i_slot is IEPESlot)
            {
                IEPESlot slot = i_slot as IEPESlot;
                T1_IEPESlot t_slot = new T1_IEPESlot();               
                t_slot.Integration = slot.Integration;
                t_slot.WaveInfo = JsonConvert.SerializeObject(
                    new WaveInfo
                    {
                        HighPassCategory = slot.HighPassCategory,
                        HighPassCode = slot.HighPassCode,
                        WaveCategory = slot.WaveCategory,
                        WaveCode = slot.WaveCode
                    });
                t_slot.SampleMode = JsonConvert.SerializeObject(slot.SampleMode);
                //t_slot.Code = IP + "_" + cardNum + "_" + slot.SlotNum;
                t_slot.Code = cardNum + "_" + slot.SlotNum + "@" + IP;
                t_slot.T_AbstractSlotInfo_Code = t_slot.Code;               
                return t_slot;
            }
            if (i_slot is EddyCurrentDisplacementSlot)
            {
                EddyCurrentDisplacementSlot slot = i_slot as EddyCurrentDisplacementSlot;
                T1_EddyCurrentDisplacementSlot t_slot = new T1_EddyCurrentDisplacementSlot();               
                t_slot.WaveInfo = JsonConvert.SerializeObject(
                  new WaveInfo
                  {
                      HighPassCategory = slot.HighPassCategory,
                      HighPassCode = slot.HighPassCode,
                      WaveCategory = slot.WaveCategory,
                      WaveCode = slot.WaveCode
                  });
                t_slot.SampleMode = JsonConvert.SerializeObject(slot.SampleMode);
                t_slot.Is24V = slot.Is24V;
                //t_slot.Code = IP + "_" + cardNum + "_" + slot.SlotNum;
                t_slot.Code = cardNum + "_" + slot.SlotNum + "@" + IP;
                t_slot.T_AbstractSlotInfo_Code = t_slot.Code;                
                return t_slot;
            }
            if (i_slot is EddyCurrentKeyPhaseSlot)
            {
                EddyCurrentKeyPhaseSlot slot = i_slot as EddyCurrentKeyPhaseSlot;
                T1_EddyCurrentKeyPhaseSlot t_slot = new T1_EddyCurrentKeyPhaseSlot();  
                t_slot.Is24V = slot.Is24V;
                t_slot.EddyCurrentRPMSampleInfo = JsonConvert.SerializeObject(new { slot.EddyCurrentRPMSample, slot.EddyCurrentRPMCode });
                //t_slot.Code = IP + "_" + cardNum + "_" + slot.SlotNum;
                t_slot.Code = cardNum + "_" + slot.SlotNum + "@" + IP;
                t_slot.T_AbstractSlotInfo_Code = t_slot.Code;               
                return t_slot;
            }
            if (i_slot is EddyCurrentTachometerSlot)
            {
                EddyCurrentTachometerSlot slot = i_slot as EddyCurrentTachometerSlot;
                T1_EddyCurrentTachometerSlot t_slot = new T1_EddyCurrentTachometerSlot(); 
                t_slot.Is24V = slot.Is24V;
                t_slot.EddyCurrentRPMSampleInfo = JsonConvert.SerializeObject(new { slot.EddyCurrentRPMSample, slot.EddyCurrentRPMCode });
                t_slot.IsEnableMainCH = slot.IsEnableMainCH;
                //t_slot.Code = IP + "_" + cardNum + "_" + slot.SlotNum;
                t_slot.Code = cardNum + "_" + slot.SlotNum + "@" + IP;
                t_slot.T_AbstractSlotInfo_Code = t_slot.Code;               
                return t_slot;
            }
            if (i_slot is DigitTachometerSlot)
            {
                DigitTachometerSlot slot = i_slot as DigitTachometerSlot;
                T1_DigitTachometerSlot t_slot = new T1_DigitTachometerSlot();
                //t_slot.Code = IP + "_" + cardNum + "_" + slot.SlotNum;
                t_slot.Code = cardNum + "_" + slot.SlotNum + "@" + IP;
                t_slot.T_AbstractSlotInfo_Code = t_slot.Code;                
                return t_slot;
            }
            if (i_slot is AnalogRransducerInSlot)
            {
                AnalogRransducerInSlot slot = i_slot as AnalogRransducerInSlot;
                T1_AnalogRransducerInSlot t_slot = new T1_AnalogRransducerInSlot();
                //t_slot.Code = IP + "_" + cardNum + "_" + slot.SlotNum;
                t_slot.Code = cardNum + "_" + slot.SlotNum + "@" + IP;
                t_slot.T_AbstractSlotInfo_Code = t_slot.Code;                
                return t_slot;
            }
            if (i_slot is RelaySlot)
            {
                RelaySlot slot = i_slot as RelaySlot;
                T1_RelaySlot t_slot = new T1_RelaySlot();
                //t_slot.Code = IP + "_" + cardNum + "_" + slot.SlotNum;
                t_slot.Code = cardNum + "_" + slot.SlotNum + "@" + IP;
                t_slot.T_AbstractSlotInfo_Code = t_slot.Code;               
                return t_slot;
            }
            if (i_slot is DigitRransducerInSlot)
            {
                DigitRransducerInSlot slot = i_slot as DigitRransducerInSlot; 
                T1_DigitRransducerInSlot t_slot = new T1_DigitRransducerInSlot();
                //t_slot.Code = IP + "_" + cardNum + "_" + slot.SlotNum;
                t_slot.Code = cardNum + "_" + slot.SlotNum + "@" + IP;
                t_slot.T_AbstractSlotInfo_Code = t_slot.Code;                
                return t_slot;
            }
            if (i_slot is DigitRransducerOutSlot)
            {
                DigitRransducerOutSlot slot = i_slot as DigitRransducerOutSlot;
                T1_DigitRransducerOutSlot t_slot = new T1_DigitRransducerOutSlot();
                //t_slot.Code = IP + "_" + cardNum + "_" + slot.SlotNum;
                t_slot.Code = cardNum + "_" + slot.SlotNum + "@" + IP;
                t_slot.T_AbstractSlotInfo_Code = t_slot.Code;                
                return t_slot;
            }
            if (i_slot is AnalogRransducerOutSlot)
            {
                AnalogRransducerOutSlot slot = i_slot as AnalogRransducerOutSlot;
                T1_AnalogRransducerOutSlot t_slot = new T1_AnalogRransducerOutSlot();
                //t_slot.Code = IP + "_" + cardNum + "_" + slot.SlotNum;
                t_slot.Code = cardNum + "_" + slot.SlotNum + "@" + IP;
                t_slot.T_AbstractSlotInfo_Code = t_slot.Code;                
                return t_slot;
            }           
          
            return null;
        }

        //无线
        public I_BaseSlot BaseSlotConvert(IWirelessSlot i_slot, string IP, string slaveIdentifier)
        {            
            if (i_slot is WirelessScalarSlot)
            {
                WirelessScalarSlot slot = i_slot as WirelessScalarSlot;
                T1_WirelessScalarSlot t_slot = new T1_WirelessScalarSlot();
                t_slot.SlotNum = slot.SlotNum;
                //t_slot.Code = IP + "_" + slaveIdentifier + "_" + slot.SlotNum;               
                //t_slot.T_TransmissionCard_Code = IP + "_" + slaveIdentifier;
                t_slot.Code = slaveIdentifier + "_" + slot.SlotNum + "@" + IP;
                t_slot.T_TransmissionCard_Code = slaveIdentifier + "@" + IP;
                return t_slot;
            }
            if (i_slot is WirelessVibrationSlot)
            {
                WirelessVibrationSlot slot = i_slot as WirelessVibrationSlot;
                T1_WirelessVibrationSlot t_slot = new T1_WirelessVibrationSlot();
                t_slot.Integration = slot.Integration;
                t_slot.Unit = slot.Unit;
                t_slot.SampleFreCategory = JsonConvert.SerializeObject(slot.SampleFreCategory);
                t_slot.SampleFreCode = slot.SampleFreCode;
                t_slot.SamplePointCategory = JsonConvert.SerializeObject(slot.SamplePointCategory);
                t_slot.SamplePointCode = slot.SamplePointCode;
                t_slot.SlotNum = slot.SlotNum;
                //t_slot.Code = IP + "_" + slaveIdentifier + "_" + slot.SlotNum;
                //t_slot.T_TransmissionCard_Code = IP + "_" + slaveIdentifier;
                t_slot.Code = slaveIdentifier + "_" + slot.SlotNum + "@" + IP;
                t_slot.T_TransmissionCard_Code = slaveIdentifier + "@" + IP;
                return t_slot;
            }
            return null;
        }

        //有线
        public I_BaseChannelInfo BaseChannelConvert(IChannel i_channel, string IP, int cardNum, int slotNum)
        {
            #region IEPEChannelInfo
            if (i_channel is IEPEChannelInfo)
            {
                IEPEChannelInfo channel = i_channel as IEPEChannelInfo;
                T1_IEPEChannelInfo t_channel = new T1_IEPEChannelInfo();
                t_channel.CalibrationlInfo = JsonConvert.SerializeObject(new { channel.VelocityCalibration, channel.DisplacementCalibration });
                t_channel.OtherInfo = JsonConvert.SerializeObject(
                    new
                    {
                        channel.RPMCardNum,
                        channel.RPMSlotNum,
                        channel.RPMCHNum,
                        channel.IsMultiplication,
                        channel.MultiplicationCor,
                        channel.IsSaveWaveToSD,
                        channel.IsUploadWave,
                        channel.DefaultRPM
                    });               
                t_channel.VibrationAddition = JsonConvert.SerializeObject(
                    new VibrationAddition
                    {
                        TPDirCategory = channel.TPDirCategory,
                        TPDirCode = channel.TPDirCode,
                        BiasVoltHigh = channel.BiasVoltHigh,
                        BiasVoltLow = channel.BiasVoltLow,
                        Sensitivity = channel.Sensitivity,
                    });
                //t_channel.Code = IP + "_" + cardNum + "_" + slotNum + "_" + channel.CHNum;
                //t_channel.T_AbstractChannelInfo_Code = t_channel.Code;
                t_channel.Code = cardNum + "_" + slotNum + "_" + channel.CHNum + "@" + IP;
                t_channel.T_AbstractChannelInfo_Code = t_channel.Code;
                return t_channel;
            }
            #endregion

            #region EddyCurrentDisplacementChannelInfo
            if (i_channel is EddyCurrentDisplacementChannelInfo)
            {
                EddyCurrentDisplacementChannelInfo channel = i_channel as EddyCurrentDisplacementChannelInfo;
                T1_EddyCurrentDisplacementChannelInfo t_channel = new T1_EddyCurrentDisplacementChannelInfo();               
                t_channel.OtherInfo = JsonConvert.SerializeObject(
                    new
                    {
                        channel.RPMCardNum,
                        channel.RPMSlotNum,
                        channel.RPMCHNum,
                        channel.IsMultiplication,
                        channel.MultiplicationCor,
                        channel.IsSaveWaveToSD,
                        channel.IsUploadWave,
                        channel.DefaultRPM
                    });              
                t_channel.VibrationAddition = JsonConvert.SerializeObject(
                    new VibrationAddition
                    {
                        TPDirCategory = channel.TPDirCategory,
                        TPDirCode = channel.TPDirCode,
                        BiasVoltHigh = channel.BiasVoltHigh,
                        BiasVoltLow = channel.BiasVoltLow,
                        Sensitivity = channel.Sensitivity,
                    });
                //t_channel.Code = IP + "_" + cardNum + "_" + slotNum + "_" + channel.CHNum;
                //t_channel.T_AbstractChannelInfo_Code = t_channel.Code;    
                t_channel.Code = cardNum + "_" + slotNum + "_" + channel.CHNum + "@" + IP;
                t_channel.T_AbstractChannelInfo_Code = t_channel.Code;
                return t_channel;
            }
            #endregion

            #region EddyCurrentKeyPhaseChannelInfo
            if (i_channel is EddyCurrentKeyPhaseChannelInfo)
            {
                EddyCurrentKeyPhaseChannelInfo channel = i_channel as EddyCurrentKeyPhaseChannelInfo;
                T1_EddyCurrentKeyPhaseChannelInfo t_channel = new T1_EddyCurrentKeyPhaseChannelInfo();              
                t_channel.ThresholdInfo = JsonConvert.SerializeObject(new { channel.ThresholdVolt, channel.HysteresisVolt, channel.ThresholdModeCategory, channel.ThresholdModeCode });
                t_channel.VibrationAddition = JsonConvert.SerializeObject(
                    new VibrationAddition
                    {
                        TPDirCategory = channel.TPDirCategory,
                        TPDirCode = channel.TPDirCode,
                        BiasVoltHigh = channel.BiasVoltHigh,
                        BiasVoltLow = channel.BiasVoltLow,
                        Sensitivity = channel.Sensitivity,
                    });
                t_channel.RPMChannelInfo = JsonConvert.SerializeObject(
                    new RPMChannelInfo
                    {
                        CalibrationCor = channel.CalibrationCor,
                        IsNotch = channel.IsNotch,
                        AverageNumber = channel.AverageNumber,
                        TeethNumber = channel.TeethNumber,
                    });
                //t_channel.Code = IP + "_" + cardNum + "_" + slotNum + "_" + channel.CHNum;
                //t_channel.T_AbstractChannelInfo_Code = t_channel.Code;           
                t_channel.Code = cardNum + "_" + slotNum + "_" + channel.CHNum + "@" + IP;
                t_channel.T_AbstractChannelInfo_Code = t_channel.Code;
                return t_channel;
            }
            #endregion

            #region EddyCurrentTachometerChannelInfo
            if (i_channel is EddyCurrentTachometerChannelInfo)
            {
                EddyCurrentTachometerChannelInfo channel = i_channel as EddyCurrentTachometerChannelInfo;
                T1_EddyCurrentTachometerChannelInfo t_channel = new T1_EddyCurrentTachometerChannelInfo();               
                t_channel.ThresholdInfo = JsonConvert.SerializeObject(new { channel.ThresholdVolt, channel.HysteresisVolt, channel.ThresholdModeCategory, channel.ThresholdModeCode });
                t_channel.VibrationAddition = JsonConvert.SerializeObject(
                    new VibrationAddition
                    {
                        TPDirCategory = channel.TPDirCategory,
                        TPDirCode = channel.TPDirCode,
                        BiasVoltHigh = channel.BiasVoltHigh,
                        BiasVoltLow = channel.BiasVoltLow,
                        Sensitivity = channel.Sensitivity,
                    });
                t_channel.RPMChannelInfo = JsonConvert.SerializeObject(
                    new RPMChannelInfo
                    {
                        CalibrationCor = channel.CalibrationCor,
                        IsNotch = channel.IsNotch,
                        AverageNumber = channel.AverageNumber,
                        TeethNumber = channel.TeethNumber,
                    });
                t_channel.RPMCouplingInfo = JsonConvert.SerializeObject(new { channel.RPMCouplingCategory, channel.RPMCouplingCode });
                //t_channel.Code = IP + "_" + cardNum + "_" + slotNum + "_" + channel.CHNum;
                //t_channel.T_AbstractChannelInfo_Code = t_channel.Code;               
                t_channel.Code = cardNum + "_" + slotNum + "_" + channel.CHNum + "@" + IP;
                t_channel.T_AbstractChannelInfo_Code = t_channel.Code;
                return t_channel;
            }
            #endregion

            #region DigitTachometerChannelInfo
            if (i_channel is DigitTachometerChannelInfo)
            {
                DigitTachometerChannelInfo channel = i_channel as DigitTachometerChannelInfo;
                T1_DigitTachometerChannelInfo t_channel = new T1_DigitTachometerChannelInfo();                
                t_channel.RPMChannelInfo = JsonConvert.SerializeObject(
                    new RPMChannelInfo
                    {
                        CalibrationCor = channel.CalibrationCor,
                        IsNotch = channel.IsNotch,
                        AverageNumber = channel.AverageNumber,
                        TeethNumber = channel.TeethNumber,
                    });
                //t_channel.Code = IP + "_" + cardNum + "_" + slotNum + "_" + channel.CHNum;
                //t_channel.T_AbstractChannelInfo_Code = t_channel.Code;       
                t_channel.Code = cardNum + "_" + slotNum + "_" + channel.CHNum + "@" + IP;
                t_channel.T_AbstractChannelInfo_Code = t_channel.Code;
                return t_channel;
            }
            #endregion

            #region AnalogRransducerInChannelInfo
            if (i_channel is AnalogRransducerInChannelInfo)
            {
                AnalogRransducerInChannelInfo channel = i_channel as AnalogRransducerInChannelInfo;
                T1_AnalogRransducerInChannelInfo t_channel = new T1_AnalogRransducerInChannelInfo();                
                t_channel.TransformMethod = JsonConvert.SerializeObject(
                    new TransformMethod
                    {
                        EquationCategory = channel.EquationCategory,
                        EquationCode = channel.EquationCode,
                    });
                //t_channel.Code = IP + "_" + cardNum + "_" + slotNum + "_" + channel.CHNum;
                //t_channel.T_AbstractChannelInfo_Code = t_channel.Code;      
                t_channel.Code = cardNum + "_" + slotNum + "_" + channel.CHNum + "@" + IP;
                t_channel.T_AbstractChannelInfo_Code = t_channel.Code;
                return t_channel;
            }
            #endregion

            #region RelayChannelInfo
            if (i_channel is RelayChannelInfo)
            {
                RelayChannelInfo channel = i_channel as RelayChannelInfo;
                T1_RelayChannelInfo t_channel = new T1_RelayChannelInfo();
                //t_channel.Code = IP + "_" + cardNum + "_" + slotNum + "_" + channel.CHNum;
                //t_channel.T_AbstractChannelInfo_Code = t_channel.Code;     
                t_channel.Code = cardNum + "_" + slotNum + "_" + channel.CHNum + "@" + IP;
                t_channel.T_AbstractChannelInfo_Code = t_channel.Code;
                return t_channel;
            }
            #endregion

            #region DigitRransducerInChannelInfo
            if (i_channel is DigitRransducerInChannelInfo)
            {
                DigitRransducerInChannelInfo channel = i_channel as DigitRransducerInChannelInfo;
                T1_DigitRransducerInChannelInfo t_channel = new T1_DigitRransducerInChannelInfo();               
                t_channel.TransformMethod = JsonConvert.SerializeObject(
                    new TransformMethod
                    {
                        EquationCategory = channel.EquationCategory,
                        EquationCode = channel.EquationCode,
                    });
                t_channel.DigitRransducerInfo = JsonConvert.SerializeObject(
                    new
                    {
                        channel.SwitchCategory,
                        channel.SwitchCode,
                        channel.ModBusFunCategory,
                        channel.ModBusFunCode,
                    });
                //t_channel.Code = IP + "_" + cardNum + "_" + slotNum + "_" + channel.CHNum;
                //t_channel.T_AbstractChannelInfo_Code = t_channel.Code;       
                t_channel.Code = cardNum + "_" + slotNum + "_" + channel.CHNum + "@" + IP;
                t_channel.T_AbstractChannelInfo_Code = t_channel.Code;
                return t_channel;
            }
            #endregion

            #region DigitRransducerOutChannelInfo
            if (i_channel is DigitRransducerOutChannelInfo)
            {
                DigitRransducerOutChannelInfo channel = i_channel as DigitRransducerOutChannelInfo;
                T1_DigitRransducerOutChannelInfo t_channel = new T1_DigitRransducerOutChannelInfo();              
                t_channel.TransformMethod = JsonConvert.SerializeObject(
                    new TransformMethod
                    {
                        EquationCategory = channel.EquationCategory,
                        EquationCode = channel.EquationCode,
                    });
                t_channel.DigitRransducerInfo = JsonConvert.SerializeObject(
                    new
                    {
                        channel.SwitchCategory,
                        channel.SwitchCode,
                        channel.ModBusFunCategory,
                        channel.ModBusFunCode,
                    });
                t_channel.SourceChannelInfo = JsonConvert.SerializeObject(
                    new SourceChannelInfo
                    {
                        SourceCardNum = channel.SourceCardNum,
                        SourceSlotNum = channel.SourceSlotNum,
                        SourceCHNum = channel.SourceCHNum,
                        SourceSubCHNum = channel.SourceSubCHNum,
                    });
                //t_channel.Code = IP + "_" + cardNum + "_" + slotNum + "_" + channel.CHNum;
                //t_channel.T_AbstractChannelInfo_Code = t_channel.Code;          
                t_channel.Code = cardNum + "_" + slotNum + "_" + channel.CHNum + "@" + IP;
                t_channel.T_AbstractChannelInfo_Code = t_channel.Code;
                return t_channel;
            }
            #endregion

            #region AnalogRransducerOutChannelInfo
            if (i_channel is AnalogRransducerOutChannelInfo)
            {
                AnalogRransducerOutChannelInfo channel = i_channel as AnalogRransducerOutChannelInfo;
                T1_AnalogRransducerOutChannelInfo t_channel = new T1_AnalogRransducerOutChannelInfo();               
                t_channel.TransformMethod = JsonConvert.SerializeObject(
                    new TransformMethod
                    {
                        EquationCategory = channel.EquationCategory,
                        EquationCode = channel.EquationCode,
                    });               
                t_channel.SourceChannelInfo = JsonConvert.SerializeObject(
                    new SourceChannelInfo
                    {
                        SourceCardNum = channel.SourceCardNum,
                        SourceSlotNum = channel.SourceSlotNum,
                        SourceCHNum = channel.SourceCHNum,
                        SourceSubCHNum = channel.SourceSubCHNum,
                    });
                //t_channel.Code = IP + "_" + cardNum + "_" + slotNum + "_" + channel.CHNum;
                //t_channel.T_AbstractChannelInfo_Code = t_channel.Code;    
                t_channel.Code = cardNum + "_" + slotNum + "_" + channel.CHNum + "@" + IP;
                t_channel.T_AbstractChannelInfo_Code = t_channel.Code;
                return t_channel;
            }
            #endregion           
            return null;
        }

        //无线
        public I_BaseChannelInfo BaseChannelConvert(IChannel i_channel, string IP, string slaveIdentifier, int slotNum)
        { 
            #region WirelessScalarChannelInfo
            if (i_channel is WirelessScalarChannelInfo)
            {
                WirelessScalarChannelInfo channel = i_channel as WirelessScalarChannelInfo;
                T1_WirelessScalarChannelInfo t_channel = new T1_WirelessScalarChannelInfo();
                t_channel.TransformMethod = JsonConvert.SerializeObject(
                    new TransformMethod
                    {
                        EquationCategory = channel.EquationCategory,
                        EquationCode = channel.EquationCode,
                    });
                //t_channel.Code = IP + "_" + slaveIdentifier + "_" + slotNum + "_" + channel.CHNum;
                //t_channel.T_AbstractChannelInfo_Code = t_channel.Code;
                t_channel.Code = slaveIdentifier + "_" + slotNum + "_" + channel.CHNum + "@" + IP;
                t_channel.T_AbstractChannelInfo_Code = t_channel.Code;
                return t_channel;
            }
            #endregion

            #region WirelessVibrationChannelInfo
            if (i_channel is WirelessVibrationChannelInfo)
            {
                WirelessVibrationChannelInfo channel = i_channel as WirelessVibrationChannelInfo;
                T1_WirelessVibrationChannelInfo t_channel = new T1_WirelessVibrationChannelInfo();
                t_channel.CalibrationlInfo = JsonConvert.SerializeObject(new { channel.VelocityCalibration, channel.DisplacementCalibration });
                t_channel.OtherInfo = JsonConvert.SerializeObject(
                    new
                    {
                        channel.RPMCardNum,
                        channel.RPMSlotNum,
                        channel.RPMCHNum,
                        channel.IsMultiplication,
                        channel.MultiplicationCor,
                        channel.IsSaveWaveToSD,
                        channel.IsUploadWave,
                        channel.DefaultRPM
                    });
                t_channel.VibrationAddition = JsonConvert.SerializeObject(
                    new VibrationAddition
                    {
                        TPDirCategory = channel.TPDirCategory,
                        TPDirCode = channel.TPDirCode,
                        BiasVoltHigh = channel.BiasVoltHigh,
                        BiasVoltLow = channel.BiasVoltLow,
                        Sensitivity = channel.Sensitivity,
                    });
                //t_channel.Code = IP + "_" + slaveIdentifier + "_" + slotNum + "_" + channel.CHNum;
                //t_channel.T_AbstractChannelInfo_Code = t_channel.Code;
                t_channel.Code = slaveIdentifier + "_" + slotNum + "_" + channel.CHNum + "@" + IP;
                t_channel.T_AbstractChannelInfo_Code = t_channel.Code;
                return t_channel;
            }
            #endregion
            return null;
        }

        //有线
        public T1_AbstractSlotInfo AbstractSlotInfoConvert(IWireSlot i_slot, string IP, int cardNum)
        {
            T1_AbstractSlotInfo t_slot = new T1_AbstractSlotInfo();
            t_slot.InSignalCategory = JsonConvert.SerializeObject(i_slot.InSignalCategory);
            t_slot.InSignalCode = i_slot.InSignalCode;
            t_slot.SlotNum = i_slot.SlotNum;
            t_slot.SlotName = i_slot.SlotName;
            t_slot.UploadIntevalTime = i_slot.UploadIntevalTime;
            t_slot.IsInput = i_slot.IsInput;
            t_slot.Unit = i_slot.Unit;
            t_slot.Version = i_slot.Version;
            //t_slot.Code = IP + "_" + cardNum + "_" + i_slot.SlotNum;
            //t_slot.T_WireMatchingCard_Code = IP + "_" + cardNum;
            t_slot.Code = cardNum + "_" + i_slot.SlotNum + "@" + IP;
            t_slot.T_WireMatchingCard_Code = cardNum + "@" + IP;
            return t_slot;
        }

        //有线
        public T1_AbstractChannelInfo AbstractChannelInfoConvert(IChannel i_channel, string IP, int cardNum, int slotNum)
        {
            T1_AbstractChannelInfo t_channel = new T1_AbstractChannelInfo();
            t_channel.Organization = JsonConvert.SerializeObject(i_channel.Organization);
            t_channel.T_Device_Name = i_channel.T_Device_Name;
            t_channel.T_Device_Code = i_channel.T_Device_Code;
            if (i_channel.T_Device_Guid != null && i_channel.T_Device_Guid != "")
            {
                t_channel.T_Device_Guid = new Guid(i_channel.T_Device_Guid);
            }
            t_channel.T_Item_Name = i_channel.T_Item_Name;
            t_channel.T_Item_Code = i_channel.T_Item_Code;
            if (i_channel.T_Item_Guid != null && i_channel.T_Item_Guid != "")
            {
                t_channel.T_Item_Guid = new Guid(i_channel.T_Item_Guid);
            }
            t_channel.CHNum = i_channel.CHNum;
            t_channel.SubCHNum = i_channel.SubCHNum;
            t_channel.IsUploadData = i_channel.IsUploadData;
            t_channel.Unit = i_channel.Unit;
            t_channel.SVTypeCategory = JsonConvert.SerializeObject(i_channel.SVTypeCategory);
            t_channel.SVTypeCode = i_channel.SVTypeCode;
            t_channel.LocalSaveCategory = JsonConvert.SerializeObject(i_channel.LocalSaveCategory);
            t_channel.LocalSaveCode = i_channel.LocalSaveCode;
            t_channel.IsBypass = i_channel.IsBypass;
            t_channel.DelayAlarmTime = i_channel.DelayAlarmTime;
            t_channel.NotOKDelayAlarmTime = i_channel.NotOKDelayAlarmTime;
            t_channel.IsLogic = i_channel.IsLogic;
            t_channel.LogicExpression = i_channel.LogicExpression;
            t_channel.Remarks = i_channel.Remarks;
            t_channel.Extra_Information = i_channel.Extra_Information;
            t_channel.AlarmStrategy = JsonConvert.SerializeObject(i_channel.AlarmStrategy);
            //t_channel.Code = IP + "_" + cardNum + "_" + slotNum + "_" + i_channel.CHNum;
            //t_channel.T_AbstractSlotInfo_Code = IP + "_" + cardNum + "_" + slotNum;
            t_channel.Code = cardNum + "_" + slotNum + "_" + i_channel.CHNum + "@" + IP;
            t_channel.T_AbstractSlotInfo_Code = cardNum + "_" + slotNum + "@" + IP;
            return t_channel;            
        }

        //无线
        public T1_AbstractChannelInfo AbstractChannelInfoConvert(IChannel i_channel, string IP, string slaveIdentifier, int slotNum)
        {
            T1_AbstractChannelInfo t_channel = new T1_AbstractChannelInfo();
            t_channel.Organization = JsonConvert.SerializeObject(i_channel.Organization);
            t_channel.T_Device_Name = i_channel.T_Device_Name;
            t_channel.T_Device_Code = i_channel.T_Device_Code;           
            if (i_channel.T_Device_Guid != null && i_channel.T_Device_Guid != "")
            {
                t_channel.T_Device_Guid = new Guid(i_channel.T_Device_Guid);
            }
            t_channel.T_Item_Name = i_channel.T_Item_Name;
            t_channel.T_Item_Code = i_channel.T_Item_Code;           
            if (i_channel.T_Item_Guid != null && i_channel.T_Item_Guid != "")
            {
                t_channel.T_Item_Guid = new Guid(i_channel.T_Item_Guid);
            }
            t_channel.CHNum = i_channel.CHNum;
            t_channel.SubCHNum = i_channel.SubCHNum;
            t_channel.IsUploadData = i_channel.IsUploadData;
            t_channel.Unit = i_channel.Unit;
            t_channel.SVTypeCategory = JsonConvert.SerializeObject(i_channel.SVTypeCategory);
            t_channel.SVTypeCode = i_channel.SVTypeCode;
            t_channel.LocalSaveCategory = JsonConvert.SerializeObject(i_channel.LocalSaveCategory);
            t_channel.LocalSaveCode = i_channel.LocalSaveCode;
            t_channel.IsBypass = i_channel.IsBypass;
            t_channel.DelayAlarmTime = i_channel.DelayAlarmTime;
            t_channel.NotOKDelayAlarmTime = i_channel.NotOKDelayAlarmTime;
            t_channel.IsLogic = i_channel.IsLogic;
            t_channel.LogicExpression = i_channel.LogicExpression;
            t_channel.Remarks = i_channel.Remarks;
            t_channel.Extra_Information = i_channel.Extra_Information;
            t_channel.AlarmStrategy = JsonConvert.SerializeObject(i_channel.AlarmStrategy);
            //t_channel.Code = IP + "_" + slaveIdentifier + "_" + slotNum + "_" + i_channel.CHNum;
            //t_channel.T_AbstractSlotInfo_Code = IP + "_" + slaveIdentifier + "_" + slotNum;
            t_channel.Code = slaveIdentifier + "_" + slotNum + "_" + i_channel.CHNum + "@" + IP;
            t_channel.T_AbstractSlotInfo_Code = slaveIdentifier + "_" + slotNum + "@" + IP;
            return t_channel;
        }

        //有问题Organization不是list，hztk123,可以删除
        public T1_AbstractChannelInfo AbstractChannelInfoConvert(IChannel i_channel, string IP, int cardNum, int slotNum, Organization organization)//附加组织机构
        {
            T1_AbstractChannelInfo t_channel = AbstractChannelInfoConvert(i_channel, IP, cardNum, slotNum);
            t_channel.Organization = JsonConvert.SerializeObject(organization);           

            return t_channel;
        }

        //有线
        public T1_DivFreInfo DivFreInfoConvert(DivFreInfo divfreInfo, string IP, int cardNum, int slotNum, int chNum)
        {
            T1_DivFreInfo t_divfreInfo = new T1_DivFreInfo();
            if (divfreInfo.Guid != null && divfreInfo.Guid != "")
            {
                t_divfreInfo.Guid = new Guid(divfreInfo.Guid);
            }            
            t_divfreInfo.Name = divfreInfo.Name;
            if (divfreInfo.Create_Time == "" || divfreInfo.Create_Time == null)
            {
                t_divfreInfo.Create_Time = null;
            }
            else
            {
                t_divfreInfo.Create_Time = DateTime.ParseExact(divfreInfo.Create_Time, "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.CurrentCulture);
            }
            if (divfreInfo.Modify_Time == "" || divfreInfo.Modify_Time == null)
            {
                t_divfreInfo.Modify_Time = null;
            }
            else
            {
                t_divfreInfo.Modify_Time = DateTime.ParseExact(divfreInfo.Modify_Time, "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.CurrentCulture);
            }            
            t_divfreInfo.Remarks = divfreInfo.Remarks;
            if (divfreInfo.T_Item_Guid != null && divfreInfo.T_Item_Guid != "")
            {
                t_divfreInfo.T_Item_Guid = new Guid(divfreInfo.T_Item_Guid);
            }
            t_divfreInfo.T_Item_Name = divfreInfo.T_Item_Name;
            t_divfreInfo.T_Item_Code = divfreInfo.T_Item_Code;
            t_divfreInfo.DivFreCode = divfreInfo.DivFreCode;
            t_divfreInfo.BasedOnRPM = JsonConvert.SerializeObject(divfreInfo.BasedOnRPM);
            t_divfreInfo.FixedFre = JsonConvert.SerializeObject(divfreInfo.FixedFre);
            t_divfreInfo.BasedOnRange = JsonConvert.SerializeObject(divfreInfo.BasedOnRange);
            t_divfreInfo.AlarmStrategy = JsonConvert.SerializeObject(divfreInfo.AlarmStrategy);
            //t_divfreInfo.T_AbstractChannelInfo_Code = IP + "_" + cardNum + "_" + slotNum + "_" + chNum;
            t_divfreInfo.T_AbstractChannelInfo_Code = cardNum + "_" + slotNum + "_" + chNum + "@" + IP;
            return t_divfreInfo;
        }

        //无线
        public T1_DivFreInfo DivFreInfoConvert(DivFreInfo divfreInfo, string IP, string slaveIdentifier, int slotNum, int chNum)
        {
            T1_DivFreInfo t_divfreInfo = new T1_DivFreInfo();
            if (divfreInfo.Guid != null && divfreInfo.Guid != "")
            {
                t_divfreInfo.Guid = new Guid(divfreInfo.Guid);
            }
            t_divfreInfo.Name = divfreInfo.Name;
            if (divfreInfo.Create_Time == "" || divfreInfo.Create_Time == null)
            {
                t_divfreInfo.Create_Time = null;
            }
            else
            {
                t_divfreInfo.Create_Time = DateTime.ParseExact(divfreInfo.Create_Time, "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.CurrentCulture);
            }
            if (divfreInfo.Modify_Time == "" || divfreInfo.Modify_Time == null)
            {
                t_divfreInfo.Modify_Time = null;
            }
            else
            {
                t_divfreInfo.Modify_Time = DateTime.ParseExact(divfreInfo.Modify_Time, "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.CurrentCulture);
            }
            t_divfreInfo.Remarks = divfreInfo.Remarks;
            if (divfreInfo.T_Item_Guid != null && divfreInfo.T_Item_Guid != "")
            {
                t_divfreInfo.T_Item_Guid = new Guid(divfreInfo.T_Item_Guid);
            }
            t_divfreInfo.T_Item_Name = divfreInfo.T_Item_Name;
            t_divfreInfo.T_Item_Code = divfreInfo.T_Item_Code;
            t_divfreInfo.DivFreCode = divfreInfo.DivFreCode;
            t_divfreInfo.BasedOnRPM = JsonConvert.SerializeObject(divfreInfo.BasedOnRPM);
            t_divfreInfo.FixedFre = JsonConvert.SerializeObject(divfreInfo.FixedFre);
            t_divfreInfo.BasedOnRange = JsonConvert.SerializeObject(divfreInfo.BasedOnRange);
            t_divfreInfo.AlarmStrategy = JsonConvert.SerializeObject(divfreInfo.AlarmStrategy);
            //t_divfreInfo.T_AbstractChannelInfo_Code = IP + "_" + slaveIdentifier + "_" + slotNum + "_" + chNum;
            t_divfreInfo.T_AbstractChannelInfo_Code = slaveIdentifier + "_" + slotNum + "_" + chNum + "@" + IP;
            return t_divfreInfo;
        }

        //根据测点类型判断有线无线
        public T1_DivFreInfo DivFreInfoConvert(DivFreInfo divfreInfo, ItemTreeItemViewModel item)//更新测点信息
        {
            T1_DivFreInfo t_divfreInfo = new T1_DivFreInfo();
            if (divfreInfo.Guid != null && divfreInfo.Guid != "")
            {
                t_divfreInfo.Guid = new Guid(divfreInfo.Guid);
            }
            t_divfreInfo.Name = divfreInfo.Name;
            if (divfreInfo.Create_Time == "" || divfreInfo.Create_Time == null)
            {
                t_divfreInfo.Create_Time = null;
            }
            else
            {
                t_divfreInfo.Create_Time = DateTime.ParseExact(divfreInfo.Create_Time, "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.CurrentCulture);
            }
            if (divfreInfo.Modify_Time == "" || divfreInfo.Modify_Time == null)
            {
                t_divfreInfo.Modify_Time = null;
            }
            else
            {
                t_divfreInfo.Modify_Time = DateTime.ParseExact(divfreInfo.Modify_Time, "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.CurrentCulture);
            }
            t_divfreInfo.Remarks = divfreInfo.Remarks;
            t_divfreInfo.T_Item_Guid = item.T_Item.Guid;
            t_divfreInfo.T_Item_Name = item.T_Item.Name;
            t_divfreInfo.T_Item_Code = item.T_Item.Code;
            t_divfreInfo.DivFreCode = divfreInfo.DivFreCode;
            t_divfreInfo.BasedOnRPM = JsonConvert.SerializeObject(divfreInfo.BasedOnRPM);
            t_divfreInfo.FixedFre = JsonConvert.SerializeObject(divfreInfo.FixedFre);
            t_divfreInfo.BasedOnRange = JsonConvert.SerializeObject(divfreInfo.BasedOnRange);
            t_divfreInfo.AlarmStrategy = JsonConvert.SerializeObject(divfreInfo.AlarmStrategy);

            if (item.T_Item.ItemType == (int)ChannelType.WirelessVibrationChannelInfo)
            {
                t_divfreInfo.T_AbstractChannelInfo_Code = item.T_Item.SlaveIdentifier + "_" + item.T_Item.SlotNum + "_" + item.T_Item.CHNum + "@" + item.T_Item.IP ;
            }
            else
            {
                t_divfreInfo.T_AbstractChannelInfo_Code = item.T_Item.CardNum + "_" + item.T_Item.SlotNum + "_" + item.T_Item.CHNum + "@" + item.T_Item.IP;
            }

            return t_divfreInfo;
        }

        public T1_Item ItemConvert(ItemTreeItemViewModel item, string serverIP, string IP, string identifier, int cardNum, int slotNum, int cnNum)//更新卡号，槽号，通道号，IP
        {
            T1_Item t_item = new T1_Item();
            t_item.Guid = item.T_Item.Guid;
            t_item.Name = item.T_Item.Name;
            t_item.Code = item.T_Item.Code;
            t_item.CardNum = cardNum;
            t_item.SlotNum = slotNum;
            t_item.CHNum = cnNum;
            t_item.T_Device_Guid = item.T_Item.T_Device_Guid;
            t_item.T_Device_Code = item.T_Item.T_Device_Code;
            t_item.Remarks = item.T_Item.Remarks; 
            t_item.Create_Time = item.T_Item.Create_Time;
            t_item.Modify_Time = item.T_Item.Modify_Time;
            t_item.Sort_No = item.T_Item.Sort_No;
            t_item.Is_Disabled = item.T_Item.Is_Disabled;
            t_item.IP = IP;
            t_item.Identifier = identifier;
            t_item.ServerIP = serverIP;//废弃ServerIP，但数据库不允许为空，依旧填充//改为初始化时候填充，覆盖数据库的ServerIP。

            return t_item;
        }

        public T1_Item ItemConvert(ItemTreeItemViewModel item, Organization parent_organization)//更新设备信息
        {
            T1_Item t_item = new T1_Item();
            t_item.Guid = item.T_Item.Guid;
            t_item.Name = item.T_Item.Name;
            t_item.Code = item.T_Item.Code;
            t_item.CardNum = item.T_Item.CardNum;
            t_item.SlotNum = item.T_Item.SlotNum;
            t_item.CHNum = item.T_Item.CHNum;
            if (parent_organization.Guid != null && parent_organization.Guid != "")
            {
                t_item.T_Device_Guid = new Guid(parent_organization.Guid);
            };
            t_item.T_Device_Code = parent_organization.Code;
            t_item.Remarks = item.T_Item.Remarks;
            t_item.Create_Time = item.T_Item.Create_Time;
            t_item.Modify_Time = item.T_Item.Modify_Time;
            t_item.Sort_No = item.T_Item.Sort_No;
            t_item.Is_Disabled = item.T_Item.Is_Disabled;
            t_item.IP = item.T_Item.IP;
            t_item.Identifier = item.T_Item.Identifier;
            t_item.ServerIP = item.T_Item.ServerIP;//废弃ServerIP，但数据库不允许为空，依旧填充//改为初始化时候填充，覆盖数据库的ServerIP。

            return t_item;
        }

        public T1_Item ItemConvert(ItemTreeItemViewModel item)//直接转换
        {
            T1_Item t_item = new T1_Item();
            t_item.Guid = item.T_Item.Guid;
            t_item.Name = item.T_Item.Name;
            t_item.Code = item.T_Item.Code;
            t_item.CardNum = item.T_Item.CardNum;
            t_item.SlotNum = item.T_Item.SlotNum;
            t_item.CHNum = item.T_Item.CHNum;
            t_item.T_Device_Guid = item.T_Item.T_Device_Guid;
            t_item.T_Device_Code = item.T_Item.T_Device_Code;
            t_item.Remarks = item.T_Item.Remarks;
            t_item.Create_Time = item.T_Item.Create_Time;
            t_item.Modify_Time = item.T_Item.Modify_Time;
            t_item.Sort_No = item.T_Item.Sort_No;
            t_item.Is_Disabled = item.T_Item.Is_Disabled;
            t_item.IP = item.T_Item.IP;
            t_item.Identifier = item.T_Item.Identifier;
            t_item.ServerIP = item.T_Item.ServerIP;//废弃ServerIP，但数据库不允许为空，依旧填充//改为初始化时候填充，覆盖数据库的ServerIP。

            return t_item;
        }      

        public T1_Organization OrganizationConvert(OrganizationTreeItemViewModel organization, OrganizationTreeItemViewModel parent)//更新设备信息
        {
            T1_Organization t_organization = new T1_Organization();
            t_organization.Name = organization.T_Organization.Name;
            t_organization.Code = organization.T_Organization.Code;
            t_organization.Guid = organization.T_Organization.Guid;
            t_organization.Level = organization.T_Organization.Level;
            t_organization.Sort_No = organization.T_Organization.Sort_No;
            t_organization.Create_Time = organization.T_Organization.Create_Time;
            t_organization.Modify_Time = organization.T_Organization.Modify_Time;
            t_organization.Is_Disabled = organization.T_Organization.Is_Disabled;
            t_organization.Parent_Code = parent.T_Organization.Code;
            t_organization.Parent_Guid = parent.T_Organization.Guid;
            t_organization.Parent_Level = parent.T_Organization.Level;
            t_organization.Remarks = organization.T_Organization.Remarks;
            t_organization.NodeType = organization.T_Organization.NodeType;
            return t_organization;
        }

        public void MainControlCardTempConvert(MainControlCard card, MainControlCard targetcard)
        {
            var t_card = targetcard.T_MainControlCard;            
            t_card.SaveTempData();            
            t_card.TempData.CommunicationCategory = JsonConvert.SerializeObject(card.CommunicationCategory);
            t_card.TempData.CommunicationCode = card.CommunicationCode;
            t_card.TempData.Identifier = card.Identifier;
            t_card.TempData.AliasName = card.AliasName;
            t_card.TempData.ACQ_Unit_Type = card.ACQ_Unit_Type;
            t_card.TempData.DataSourceCategory = JsonConvert.SerializeObject(card.DataSourceCategory);
            t_card.TempData.DataSourceCode = card.DataSourceCode;
            t_card.TempData.IsAlarmLatch = card.IsAlarmLatch;
            t_card.TempData.IsConfiguration = card.IsConfiguration;
            t_card.TempData.IsHdBypass = card.IsHdBypass;
            t_card.TempData.IsHdConfiguration = card.IsHdConfiguration;
            t_card.TempData.IsHdMultiplication = card.IsHdMultiplication;
            t_card.TempData.IsListen = card.IsListen;
            t_card.TempData.AsySyn = card.AsySyn;
            t_card.TempData.LanguageCategory = JsonConvert.SerializeObject(card.LanguageCategory);
            t_card.TempData.LanguageCode = card.LanguageCode;
            t_card.TempData.MainCardCategory = JsonConvert.SerializeObject(card.MainCardCategory);
            t_card.TempData.MainCardCode = card.MainCardCode;
            t_card.TempData.SampleMode = JsonConvert.SerializeObject(card.SampleMode);
            t_card.TempData.ServerIP = card.ServerIP;
            t_card.TempData.WaveCategory = JsonConvert.SerializeObject(card.WaveCategory);
            t_card.TempData.SynWaveCode = card.SynWaveCode;
            t_card.TempData.Version = card.Version;
            t_card.TempData.ScaleDataRange = card.ScaleDataRange;
            //t_card.TempData.IP = IP;            
        }

        public void WireMatchingCardTempConvert(WireMatchingCard card, WireMatchingCard targetcard)
        {
            var t_card = targetcard.T_WireMatchingCard;
            t_card.SaveTempData();
            t_card.TempData.CardName = card.CardName;
            t_card.TempData.CardNum = card.CardNum;
            //t_card.TempData.T_MainControlCard_IP = IP;
            //t_card.TempData.Code = IP + "_" + card.CardNum;         
        }

        public void WirelessReceiveCardTempConvert(WirelessReceiveCard card, WirelessReceiveCard targetcard)
        {
            var t_card = targetcard.T_WirelessReceiveCard;
            t_card.SaveTempData();           
            t_card.TempData.MasterIdentifier = card.MasterIdentifier;
            t_card.TempData.ReceiveCardName = card.ReceiveCardName;
            //t_card.TempData.T_MainControlCard_IP = IP;
            //t_card.TempData.Code = IP + "_" + card.MasterIdentifier;         
        }

        public void TransmissionCardTempConvert(TransmissionCard card, TransmissionCard targetcard)
        {
            var t_card = targetcard.T_TransmissionCard;
            t_card.SaveTempData();           
            t_card.TempData.SlaveIdentifier = card.SlaveIdentifier;
            t_card.TempData.TransmissionType = card.TransmissionType;
            t_card.TempData.TransmissionName = card.TransmissionName;
            t_card.TempData.Version = card.Version;
            t_card.TempData.WorkTime = card.WorkTime;
            t_card.TempData.SleepTime = card.SleepTime;
            t_card.TempData.BatteryEnergy = card.BatteryEnergy;
            t_card.TempData.Remarks = card.Remarks;
            t_card.TempData.ExtraInfo = JsonConvert.SerializeObject(card.ExtraInfo);
            //t_card.TempData.T_WirelessReceiveCard_Code = IP + "_" + masterIdentifier;
            //t_card.TempData.Code = IP + "_" + card.SlaveIdentifier;
        }

        public void SlotTempConvert(ISlot i_slot, ISlot i_targetslot)
        {           
            if (i_slot is IEPESlot)
            {
                IEPESlot slot = i_slot as IEPESlot;
                var t_slot = (i_targetslot as IEPESlot).T_IEPESlot;
                t_slot.SaveTempData();
                t_slot.TempData.Integration = slot.Integration;
                t_slot.TempData.WaveInfo = JsonConvert.SerializeObject(
                    new WaveInfo
                    {
                        HighPassCategory = slot.HighPassCategory,
                        HighPassCode = slot.HighPassCode,
                        WaveCategory = slot.WaveCategory,
                        WaveCode = slot.WaveCode
                    });
                t_slot.TempData.SampleMode = JsonConvert.SerializeObject(slot.SampleMode);                         
            }
            else if (i_slot is EddyCurrentDisplacementSlot)
            {
                EddyCurrentDisplacementSlot slot = i_slot as EddyCurrentDisplacementSlot;
                var t_slot = (i_targetslot as EddyCurrentDisplacementSlot).T_EddyCurrentDisplacementSlot;
                t_slot.SaveTempData();
                t_slot.TempData.WaveInfo = JsonConvert.SerializeObject(
                  new WaveInfo
                  {
                      HighPassCategory = slot.HighPassCategory,
                      HighPassCode = slot.HighPassCode,
                      WaveCategory = slot.WaveCategory,
                      WaveCode = slot.WaveCode
                  });
                t_slot.TempData.SampleMode = JsonConvert.SerializeObject(slot.SampleMode);
                t_slot.TempData.Is24V = slot.Is24V;                        
            }
            else if (i_slot is EddyCurrentKeyPhaseSlot)
            {
                EddyCurrentKeyPhaseSlot slot = i_slot as EddyCurrentKeyPhaseSlot;
                var t_slot = (i_targetslot as EddyCurrentKeyPhaseSlot).T_EddyCurrentKeyPhaseSlot;
                t_slot.SaveTempData();
                t_slot.TempData.Is24V = slot.Is24V;
                t_slot.TempData.EddyCurrentRPMSampleInfo = JsonConvert.SerializeObject(new { slot.EddyCurrentRPMSample, slot.EddyCurrentRPMCode });
            }
            else if (i_slot is EddyCurrentTachometerSlot)
            {
                EddyCurrentTachometerSlot slot = i_slot as EddyCurrentTachometerSlot;
                var t_slot = (i_targetslot as EddyCurrentTachometerSlot).T_EddyCurrentTachometerSlot;
                t_slot.SaveTempData();
                t_slot.TempData.Is24V = slot.Is24V;
                t_slot.TempData.EddyCurrentRPMSampleInfo = JsonConvert.SerializeObject(new { slot.EddyCurrentRPMSample, slot.EddyCurrentRPMCode });
                t_slot.TempData.IsEnableMainCH = slot.IsEnableMainCH;               
            }
            else if (i_slot is DigitTachometerSlot)
            {
                DigitTachometerSlot slot = i_slot as DigitTachometerSlot;
                var t_slot = (i_targetslot as DigitTachometerSlot).T_DigitTachometerSlot;
                t_slot.SaveTempData();              
            }
            else if (i_slot is AnalogRransducerInSlot)
            {
                AnalogRransducerInSlot slot = i_slot as AnalogRransducerInSlot;
                var t_slot = (i_targetslot as AnalogRransducerInSlot).T_AnalogRransducerInSlot;
                t_slot.SaveTempData();
            }
            else if (i_slot is RelaySlot)
            {
                RelaySlot slot = i_slot as RelaySlot;
                var t_slot = (i_targetslot as RelaySlot).T_RelaySlot;
                t_slot.SaveTempData();               
            }
            else if (i_slot is DigitRransducerInSlot)
            {
                DigitRransducerInSlot slot = i_slot as DigitRransducerInSlot;
                var t_slot = (i_targetslot as DigitRransducerInSlot).T_DigitRransducerInSlot;
                t_slot.SaveTempData();
            }
            else if (i_slot is DigitRransducerOutSlot)
            {
                DigitRransducerOutSlot slot = i_slot as DigitRransducerOutSlot;
                var t_slot = (i_targetslot as DigitRransducerOutSlot).T_DigitRransducerOutSlot;
                t_slot.SaveTempData();
            }
            else if (i_slot is AnalogRransducerOutSlot)
            {
                AnalogRransducerOutSlot slot = i_slot as AnalogRransducerOutSlot;
                var t_slot = (i_targetslot as AnalogRransducerOutSlot).T_AnalogRransducerOutSlot;
                t_slot.SaveTempData();
            }
            else if (i_slot is WirelessScalarSlot)
            {
                WirelessScalarSlot slot = i_slot as WirelessScalarSlot;
                var t_slot = (i_targetslot as WirelessScalarSlot).T_WirelessScalarSlot;
                t_slot.SaveTempData();
                t_slot.TempData.SlotNum = slot.SlotNum;  
            }
            else if (i_slot is WirelessVibrationSlot)
            {
                WirelessVibrationSlot slot = i_slot as WirelessVibrationSlot;
                var t_slot = (i_targetslot as WirelessVibrationSlot).T_WirelessVibrationSlot;
                t_slot.SaveTempData();
                t_slot.TempData.Integration = slot.Integration;
                t_slot.TempData.Unit = slot.Unit;
                t_slot.TempData.SampleFreCategory = JsonConvert.SerializeObject(slot.SampleFreCategory);
                t_slot.TempData.SampleFreCode = slot.SampleFreCode;
                t_slot.TempData.SamplePointCategory = JsonConvert.SerializeObject(slot.SamplePointCategory);
                t_slot.TempData.SamplePointCode = slot.SamplePointCode;
                t_slot.TempData.SlotNum = slot.SlotNum;               
            }

            if (i_targetslot is IWireSlot)
            {
                IWireSlot slot = i_slot as IWireSlot;
                var t_slot = (i_targetslot as IWireSlot).T_AbstractSlotInfo;
                t_slot.SaveTempData();
                t_slot.TempData.InSignalCategory = JsonConvert.SerializeObject(slot.InSignalCategory);
                t_slot.TempData.InSignalCode = slot.InSignalCode;
                t_slot.TempData.SlotNum = slot.SlotNum;
                t_slot.TempData.SlotName = slot.SlotName;
                t_slot.TempData.UploadIntevalTime = slot.UploadIntevalTime;
                t_slot.TempData.IsInput = slot.IsInput;
                t_slot.TempData.Unit = slot.Unit;
                t_slot.TempData.Version = slot.Version;          
            }


        }

        public void ChannelTempConvert(IChannel i_channel, IChannel i_targetchannel)
        {
            #region IEPEChannelInfo
            if (i_channel is IEPEChannelInfo)
            {
                IEPEChannelInfo channel = i_channel as IEPEChannelInfo;
                var t_channel = (i_targetchannel as IEPEChannelInfo).T_IEPEChannelInfo;
                t_channel.SaveTempData();              
                t_channel.TempData.CalibrationlInfo = JsonConvert.SerializeObject(new { channel.VelocityCalibration, channel.DisplacementCalibration });
                t_channel.TempData.OtherInfo = JsonConvert.SerializeObject(
                    new
                    {
                        channel.RPMCardNum,
                        channel.RPMSlotNum,
                        channel.RPMCHNum,
                        channel.IsMultiplication,
                        channel.MultiplicationCor,
                        channel.IsSaveWaveToSD,
                        channel.IsUploadWave,
                        channel.DefaultRPM
                    });
                t_channel.TempData.VibrationAddition = JsonConvert.SerializeObject(
                    new VibrationAddition
                    {
                        TPDirCategory = channel.TPDirCategory,
                        TPDirCode = channel.TPDirCode,
                        BiasVoltHigh = channel.BiasVoltHigh,
                        BiasVoltLow = channel.BiasVoltLow,
                        Sensitivity = channel.Sensitivity,
                    });              
            }
            #endregion

            #region EddyCurrentDisplacementChannelInfo
            else if (i_channel is EddyCurrentDisplacementChannelInfo)
            {
                EddyCurrentDisplacementChannelInfo channel = i_channel as EddyCurrentDisplacementChannelInfo;
                var t_channel = (i_targetchannel as EddyCurrentDisplacementChannelInfo).T_EddyCurrentDisplacementChannelInfo;
                t_channel.SaveTempData();
                t_channel.TempData.OtherInfo = JsonConvert.SerializeObject(
                    new
                    {
                        channel.RPMCardNum,
                        channel.RPMSlotNum,
                        channel.RPMCHNum,
                        channel.IsMultiplication,
                        channel.MultiplicationCor,
                        channel.IsSaveWaveToSD,
                        channel.IsUploadWave,
                        channel.DefaultRPM
                    });
                t_channel.TempData.VibrationAddition = JsonConvert.SerializeObject(
                    new VibrationAddition
                    {
                        TPDirCategory = channel.TPDirCategory,
                        TPDirCode = channel.TPDirCode,
                        BiasVoltHigh = channel.BiasVoltHigh,
                        BiasVoltLow = channel.BiasVoltLow,
                        Sensitivity = channel.Sensitivity,
                    });              
            }
            #endregion

            #region EddyCurrentKeyPhaseChannelInfo
            else if (i_channel is EddyCurrentKeyPhaseChannelInfo)
            {
                EddyCurrentKeyPhaseChannelInfo channel = i_channel as EddyCurrentKeyPhaseChannelInfo;
                var t_channel = (i_targetchannel as EddyCurrentKeyPhaseChannelInfo).T_EddyCurrentKeyPhaseChannelInfo;
                t_channel.SaveTempData();
                t_channel.TempData.ThresholdInfo = JsonConvert.SerializeObject(new { channel.ThresholdVolt, channel.HysteresisVolt, channel.ThresholdModeCategory, channel.ThresholdModeCode });
                t_channel.TempData.VibrationAddition = JsonConvert.SerializeObject(
                    new VibrationAddition
                    {
                        TPDirCategory = channel.TPDirCategory,
                        TPDirCode = channel.TPDirCode,
                        BiasVoltHigh = channel.BiasVoltHigh,
                        BiasVoltLow = channel.BiasVoltLow,
                        Sensitivity = channel.Sensitivity,
                    });
                t_channel.TempData.RPMChannelInfo = JsonConvert.SerializeObject(
                    new RPMChannelInfo
                    {
                        CalibrationCor = channel.CalibrationCor,
                        IsNotch = channel.IsNotch,
                        AverageNumber = channel.AverageNumber,
                        TeethNumber = channel.TeethNumber,
                    });               
            }
            #endregion

            #region EddyCurrentTachometerChannelInfo
            else if (i_channel is EddyCurrentTachometerChannelInfo)
            {
                EddyCurrentTachometerChannelInfo channel = i_channel as EddyCurrentTachometerChannelInfo;
                var t_channel = (i_targetchannel as EddyCurrentTachometerChannelInfo).T_EddyCurrentTachometerChannelInfo;
                t_channel.SaveTempData();
                t_channel.TempData.ThresholdInfo = JsonConvert.SerializeObject(new { channel.ThresholdVolt, channel.HysteresisVolt, channel.ThresholdModeCategory, channel.ThresholdModeCode });
                t_channel.TempData.VibrationAddition = JsonConvert.SerializeObject(
                    new VibrationAddition
                    {
                        TPDirCategory = channel.TPDirCategory,
                        TPDirCode = channel.TPDirCode,
                        BiasVoltHigh = channel.BiasVoltHigh,
                        BiasVoltLow = channel.BiasVoltLow,
                        Sensitivity = channel.Sensitivity,
                    });
                t_channel.TempData.RPMChannelInfo = JsonConvert.SerializeObject(
                    new RPMChannelInfo
                    {
                        CalibrationCor = channel.CalibrationCor,
                        IsNotch = channel.IsNotch,
                        AverageNumber = channel.AverageNumber,
                        TeethNumber = channel.TeethNumber,
                    });
                t_channel.TempData.RPMCouplingInfo = JsonConvert.SerializeObject(new { channel.RPMCouplingCategory, channel.RPMCouplingCode });
            }
            #endregion

            #region DigitTachometerChannelInfo
            else if (i_channel is DigitTachometerChannelInfo)
            {
                DigitTachometerChannelInfo channel = i_channel as DigitTachometerChannelInfo;
                var t_channel = (i_targetchannel as DigitTachometerChannelInfo).T_DigitTachometerChannelInfo;
                t_channel.SaveTempData();
                t_channel.TempData.RPMChannelInfo = JsonConvert.SerializeObject(
                    new RPMChannelInfo
                    {
                        CalibrationCor = channel.CalibrationCor,
                        IsNotch = channel.IsNotch,
                        AverageNumber = channel.AverageNumber,
                        TeethNumber = channel.TeethNumber,
                    });               
            }
            #endregion

            #region AnalogRransducerInChannelInfo
            else if (i_channel is AnalogRransducerInChannelInfo)
            {
                AnalogRransducerInChannelInfo channel = i_channel as AnalogRransducerInChannelInfo;
                var t_channel = (i_targetchannel as AnalogRransducerInChannelInfo).T_AnalogRransducerInChannelInfo;
                t_channel.SaveTempData();
                t_channel.TempData.TransformMethod = JsonConvert.SerializeObject(
                    new TransformMethod
                    {
                        EquationCategory = channel.EquationCategory,
                        EquationCode = channel.EquationCode,
                    });               
            }
            #endregion

            #region RelayChannelInfo
            else if (i_channel is RelayChannelInfo)
            {
                RelayChannelInfo channel = i_channel as RelayChannelInfo;
                var t_channel = (i_targetchannel as RelayChannelInfo).T_RelayChannelInfo;
                t_channel.SaveTempData();
            }
            #endregion

            #region DigitRransducerInChannelInfo
            else if (i_channel is DigitRransducerInChannelInfo)
            {
                DigitRransducerInChannelInfo channel = i_channel as DigitRransducerInChannelInfo;
                var t_channel = (i_targetchannel as DigitRransducerInChannelInfo).T_DigitRransducerInChannelInfo;
                t_channel.SaveTempData();
                t_channel.TempData.TransformMethod = JsonConvert.SerializeObject(
                    new TransformMethod
                    {
                        EquationCategory = channel.EquationCategory,
                        EquationCode = channel.EquationCode,
                    });
                t_channel.TempData.DigitRransducerInfo = JsonConvert.SerializeObject(
                    new
                    {
                        channel.SwitchCategory,
                        channel.SwitchCode,
                        channel.ModBusFunCategory,
                        channel.ModBusFunCode,
                    });
            }
            #endregion

            #region DigitRransducerOutChannelInfo
            else if (i_channel is DigitRransducerOutChannelInfo)
            {
                DigitRransducerOutChannelInfo channel = i_channel as DigitRransducerOutChannelInfo;
                var t_channel = (i_targetchannel as DigitRransducerOutChannelInfo).T_DigitRransducerOutChannelInfo;
                t_channel.SaveTempData();
                t_channel.TempData.TransformMethod = JsonConvert.SerializeObject(
                    new TransformMethod
                    {
                        EquationCategory = channel.EquationCategory,
                        EquationCode = channel.EquationCode,
                    });
                t_channel.TempData.DigitRransducerInfo = JsonConvert.SerializeObject(
                    new
                    {
                        channel.SwitchCategory,
                        channel.SwitchCode,
                        channel.ModBusFunCategory,
                        channel.ModBusFunCode,
                    });
                t_channel.TempData.SourceChannelInfo = JsonConvert.SerializeObject(
                    new SourceChannelInfo
                    {
                        SourceCardNum = channel.SourceCardNum,
                        SourceSlotNum = channel.SourceSlotNum,
                        SourceCHNum = channel.SourceCHNum,
                        SourceSubCHNum = channel.SourceSubCHNum,
                    });
            }
            #endregion

            #region AnalogRransducerOutChannelInfo
            else if (i_channel is AnalogRransducerOutChannelInfo)
            {
                AnalogRransducerOutChannelInfo channel = i_channel as AnalogRransducerOutChannelInfo;
                var t_channel = (i_targetchannel as AnalogRransducerOutChannelInfo).T_AnalogRransducerOutChannelInfo;
                t_channel.SaveTempData();
                t_channel.TempData.TransformMethod = JsonConvert.SerializeObject(
                    new TransformMethod
                    {
                        EquationCategory = channel.EquationCategory,
                        EquationCode = channel.EquationCode,
                    });
                t_channel.TempData.SourceChannelInfo = JsonConvert.SerializeObject(
                    new SourceChannelInfo
                    {
                        SourceCardNum = channel.SourceCardNum,
                        SourceSlotNum = channel.SourceSlotNum,
                        SourceCHNum = channel.SourceCHNum,
                        SourceSubCHNum = channel.SourceSubCHNum,
                    });
            }
            #endregion

            #region WirelessScalarChannelInfo
            else if (i_channel is WirelessScalarChannelInfo)
            {
                WirelessScalarChannelInfo channel = i_channel as WirelessScalarChannelInfo;
                var t_channel = (i_targetchannel as WirelessScalarChannelInfo).T_WirelessScalarChannelInfo;
                t_channel.SaveTempData();
                t_channel.TempData.TransformMethod = JsonConvert.SerializeObject(
                    new TransformMethod
                    {
                        EquationCategory = channel.EquationCategory,
                        EquationCode = channel.EquationCode,
                    });               
            }
            #endregion

            #region WirelessVibrationChannelInfo
            else if (i_channel is WirelessVibrationChannelInfo)
            {
                WirelessVibrationChannelInfo channel = i_channel as WirelessVibrationChannelInfo;
                var t_channel = (i_targetchannel as WirelessVibrationChannelInfo).T_WirelessVibrationChannelInfo;
                t_channel.SaveTempData();
                t_channel.TempData.CalibrationlInfo = JsonConvert.SerializeObject(new { channel.VelocityCalibration, channel.DisplacementCalibration });
                t_channel.TempData.OtherInfo = JsonConvert.SerializeObject(
                    new
                    {
                        channel.RPMCardNum,
                        channel.RPMSlotNum,
                        channel.RPMCHNum,
                        channel.IsMultiplication,
                        channel.MultiplicationCor,
                        channel.IsSaveWaveToSD,
                        channel.IsUploadWave,
                        channel.DefaultRPM
                    });
                t_channel.TempData.VibrationAddition = JsonConvert.SerializeObject(
                    new VibrationAddition
                    {
                        TPDirCategory = channel.TPDirCategory,
                        TPDirCode = channel.TPDirCode,
                        BiasVoltHigh = channel.BiasVoltHigh,
                        BiasVoltLow = channel.BiasVoltLow,
                        Sensitivity = channel.Sensitivity,
                    });               
            }
            #endregion

            if (i_targetchannel is IChannel)
            { 
                var t_channel = i_targetchannel.T_AbstractChannelInfo;
                t_channel.SaveTempData();
                t_channel.TempData.Organization = JsonConvert.SerializeObject(i_channel.Organization);
                t_channel.TempData.T_Device_Name = i_channel.T_Device_Name;
                t_channel.TempData.T_Device_Code = i_channel.T_Device_Code;
                if (i_channel.T_Device_Guid != null && i_channel.T_Device_Guid != "")
                {
                    t_channel.TempData.T_Device_Guid = new Guid(i_channel.T_Device_Guid);
                }
                t_channel.TempData.T_Item_Name = i_channel.T_Item_Name;
                t_channel.TempData.T_Item_Code = i_channel.T_Item_Code;
                if (i_channel.T_Item_Guid != null && i_channel.T_Item_Guid != "")
                {
                    t_channel.TempData.T_Item_Guid = new Guid(i_channel.T_Item_Guid);
                }
                t_channel.TempData.CHNum = i_channel.CHNum;
                t_channel.TempData.SubCHNum = i_channel.SubCHNum;
                t_channel.TempData.IsUploadData = i_channel.IsUploadData;
                t_channel.TempData.Unit = i_channel.Unit;
                t_channel.TempData.SVTypeCategory = JsonConvert.SerializeObject(i_channel.SVTypeCategory);
                t_channel.TempData.SVTypeCode = i_channel.SVTypeCode;
                t_channel.TempData.LocalSaveCategory = JsonConvert.SerializeObject(i_channel.LocalSaveCategory);
                t_channel.TempData.LocalSaveCode = i_channel.LocalSaveCode;
                t_channel.TempData.IsBypass = i_channel.IsBypass;
                t_channel.TempData.DelayAlarmTime = i_channel.DelayAlarmTime;
                t_channel.TempData.NotOKDelayAlarmTime = i_channel.NotOKDelayAlarmTime;
                t_channel.TempData.IsLogic = i_channel.IsLogic;
                t_channel.TempData.LogicExpression = i_channel.LogicExpression;
                t_channel.TempData.Remarks = i_channel.Remarks;
                t_channel.TempData.Extra_Information = i_channel.Extra_Information;
                t_channel.TempData.AlarmStrategy = JsonConvert.SerializeObject(i_channel.AlarmStrategy);                
            }
        }

        public void DivFreInfoTempConvert(DivFreInfo divfreInfo, DivFreInfo targetdivfreInfo)
        {
            var t_divfreInfo = targetdivfreInfo.T_DivFreInfo;
            t_divfreInfo.SaveTempData();
            if (divfreInfo.Guid != null && divfreInfo.Guid != "")
            {
                t_divfreInfo.TempData.Guid = new Guid(divfreInfo.Guid);
            }
            t_divfreInfo.TempData.Name = divfreInfo.Name;
            if (divfreInfo.Create_Time == "" || divfreInfo.Create_Time == null)
            {
                t_divfreInfo.TempData.Create_Time = null;
            }
            else
            {
                t_divfreInfo.TempData.Create_Time = DateTime.ParseExact(divfreInfo.Create_Time, "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.CurrentCulture);
            }
            if (divfreInfo.Modify_Time == "" || divfreInfo.Modify_Time == null)
            {
                t_divfreInfo.TempData.Modify_Time = null;
            }
            else
            {
                t_divfreInfo.TempData.Modify_Time = DateTime.ParseExact(divfreInfo.Modify_Time, "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.CurrentCulture);
            }
            t_divfreInfo.TempData.Remarks = divfreInfo.Remarks;
            if (divfreInfo.T_Item_Guid != null && divfreInfo.T_Item_Guid != "")
            {
                t_divfreInfo.TempData.T_Item_Guid = new Guid(divfreInfo.T_Item_Guid);
            }
            t_divfreInfo.TempData.T_Item_Name = divfreInfo.T_Item_Name;
            t_divfreInfo.TempData.T_Item_Code = divfreInfo.T_Item_Code;
            t_divfreInfo.TempData.DivFreCode = divfreInfo.DivFreCode;
            t_divfreInfo.TempData.BasedOnRPM = JsonConvert.SerializeObject(divfreInfo.BasedOnRPM);
            t_divfreInfo.TempData.FixedFre = JsonConvert.SerializeObject(divfreInfo.FixedFre);
            t_divfreInfo.TempData.BasedOnRange = JsonConvert.SerializeObject(divfreInfo.BasedOnRange);
            t_divfreInfo.TempData.AlarmStrategy = JsonConvert.SerializeObject(divfreInfo.AlarmStrategy);
        }
    }
}
