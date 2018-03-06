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
    public class ConvertFromDataBaseFormat : IConvertFromDataBaseFormat
    {
        public MainControlCard MainControlCardConvert(T1_MainControlCard t_card)
        {
            MainControlCard card = new MainControlCard();
            card.CommunicationCategory = JsonConvert.DeserializeObject<List<CommunicationCategory>>(t_card.CommunicationCategory);
            card.CommunicationCode = t_card.CommunicationCode;
            card.Identifier = t_card.Identifier;
            card.AliasName = t_card.AliasName;
            card.ACQ_Unit_Type = t_card.ACQ_Unit_Type.Value;
            card.DataSourceCategory = JsonConvert.DeserializeObject<List<DataSourceCategory>>(t_card.DataSourceCategory);
            card.DataSourceCode = t_card.DataSourceCode;
            card.IsAlarmLatch = t_card.IsAlarmLatch;
            card.IsConfiguration = t_card.IsConfiguration;
            card.IsHdBypass = t_card.IsHdBypass;
            card.IsHdConfiguration = t_card.IsHdConfiguration;
            card.IsHdMultiplication = t_card.IsHdMultiplication;
            card.IsListen = t_card.IsListen;
            card.AsySyn = t_card.AsySyn;
            card.LanguageCategory = JsonConvert.DeserializeObject<List<LanguageCategory>>(t_card.LanguageCategory);
            card.LanguageCode = t_card.LanguageCode;
            card.MainCardCategory = JsonConvert.DeserializeObject<List<MainCardCategory>>(t_card.MainCardCategory);
            card.MainCardCode = t_card.MainCardCode;
            card.SampleMode = JsonConvert.DeserializeObject<SampleMode>(t_card.SampleMode);
            card.ServerIP = t_card.ServerIP;
            card.WaveCategory = JsonConvert.DeserializeObject<List<WaveCategory>>(t_card.WaveCategory);
            card.SynWaveCode = t_card.SynWaveCode;
            card.Version = t_card.Version;
            card.ScaleDataRange = (float)t_card.ScaleDataRange;

            card.T_MainControlCard = t_card;
            return card;
        }

        public WireMatchingCard WireMatchingCardConvert(T1_WireMatchingCard t_card)
        {
            WireMatchingCard card = new WireMatchingCard();
            card.CardName = t_card.CardName;
            card.CardNum = t_card.CardNum;

            card.T_WireMatchingCard = t_card;
            return card;
        }

        public WirelessReceiveCard WirelessReceiveCardConvert(T1_WirelessReceiveCard t_card)
        {
            WirelessReceiveCard card = new WirelessReceiveCard();
            card.MasterIdentifier = t_card.MasterIdentifier;
            card.ReceiveCardName = t_card.ReceiveCardName;

            card.T_WirelessReceiveCard = t_card;
            return card;
        }

        public TransmissionCard TransmissionCardConvert(T1_TransmissionCard t_card)
        {
            TransmissionCard card = new TransmissionCard();
            card.SlaveIdentifier = t_card.SlaveIdentifier;
            card.TransmissionType = t_card.TransmissionType;
            card.TransmissionName = t_card.TransmissionName;
            card.Version = t_card.Version;
            card.WorkTime = t_card.WorkTime.Value;
            card.SleepTime = t_card.SleepTime.Value;
            card.BatteryEnergy = (float)t_card.BatteryEnergy.Value; //htzk123
            card.Remarks = t_card.Remarks;
            card.ExtraInfo = JsonConvert.DeserializeObject<ChannelExtraInfo>(t_card.ExtraInfo);

            card.T_TransmissionCard = t_card;
            return card;
        }

        public ISlot BaseSlotConvert(I_BaseSlot t_slot)
        {
            if (t_slot is T1_IEPESlot)
            {
                T1_IEPESlot slot = t_slot as T1_IEPESlot;
                IEPESlot i_slot = new IEPESlot();
                i_slot.Integration = slot.Integration.Value;
                WaveInfo waveinfo = JsonConvert.DeserializeObject<WaveInfo>(slot.WaveInfo);
                i_slot.HighPassCategory = waveinfo.HighPassCategory;
                i_slot.HighPassCode = waveinfo.HighPassCode;
                i_slot.WaveCategory = waveinfo.WaveCategory;
                i_slot.WaveCode = waveinfo.WaveCode;
                i_slot.SampleMode = JsonConvert.DeserializeObject<SampleMode>(slot.SampleMode);

                i_slot.T_IEPESlot = slot;                         
                return i_slot;
            }
            else if (t_slot is T1_EddyCurrentDisplacementSlot)
            {
                T1_EddyCurrentDisplacementSlot slot = t_slot as T1_EddyCurrentDisplacementSlot;
                EddyCurrentDisplacementSlot i_slot = new EddyCurrentDisplacementSlot();              
                WaveInfo waveinfo = JsonConvert.DeserializeObject<WaveInfo>(slot.WaveInfo);
                i_slot.HighPassCategory = waveinfo.HighPassCategory;
                i_slot.HighPassCode = waveinfo.HighPassCode;
                i_slot.WaveCategory = waveinfo.WaveCategory;
                i_slot.WaveCode = waveinfo.WaveCode;
                i_slot.SampleMode = JsonConvert.DeserializeObject<SampleMode>(slot.SampleMode);
                i_slot.Is24V = slot.Is24V.Value;

                i_slot.T_EddyCurrentDisplacementSlot = slot;
                return i_slot;
            }
            if (t_slot is T1_EddyCurrentKeyPhaseSlot)
            {
                T1_EddyCurrentKeyPhaseSlot slot = t_slot as T1_EddyCurrentKeyPhaseSlot;
                EddyCurrentKeyPhaseSlot i_slot = new EddyCurrentKeyPhaseSlot();               
                i_slot.Is24V = slot.Is24V.Value;
                EddyCurrentRPMSampleInfo eddyCurrentRPMSampleInfo = JsonConvert.DeserializeObject<EddyCurrentRPMSampleInfo>(slot.EddyCurrentRPMSampleInfo);
                i_slot.EddyCurrentRPMSample = eddyCurrentRPMSampleInfo.EddyCurrentRPMSample;
                i_slot.EddyCurrentRPMCode = eddyCurrentRPMSampleInfo.EddyCurrentRPMCode;

                i_slot.T_EddyCurrentKeyPhaseSlot = slot;
                return i_slot;
            }
            if (t_slot is T1_EddyCurrentTachometerSlot)
            {
                T1_EddyCurrentTachometerSlot slot = t_slot as T1_EddyCurrentTachometerSlot;
                EddyCurrentTachometerSlot i_slot = new EddyCurrentTachometerSlot();                
                i_slot.Is24V = slot.Is24V.Value;
                EddyCurrentRPMSampleInfo eddyCurrentRPMSampleInfo = JsonConvert.DeserializeObject<EddyCurrentRPMSampleInfo>(slot.EddyCurrentRPMSampleInfo);
                i_slot.EddyCurrentRPMSample = eddyCurrentRPMSampleInfo.EddyCurrentRPMSample;
                i_slot.EddyCurrentRPMCode = eddyCurrentRPMSampleInfo.EddyCurrentRPMCode;
                i_slot.IsEnableMainCH = slot.IsEnableMainCH.Value;

                i_slot.T_EddyCurrentTachometerSlot = slot;
                return i_slot;
            }
            if (t_slot is T1_DigitTachometerSlot)
            {
                T1_DigitTachometerSlot slot = t_slot as T1_DigitTachometerSlot;
                DigitTachometerSlot i_slot = new DigitTachometerSlot();              

                i_slot.T_DigitTachometerSlot = slot;
                return i_slot;
            }
            if (t_slot is T1_AnalogRransducerInSlot)
            {
                T1_AnalogRransducerInSlot slot = t_slot as T1_AnalogRransducerInSlot;
                AnalogRransducerInSlot i_slot = new AnalogRransducerInSlot();

                i_slot.T_AnalogRransducerInSlot = slot;
                return i_slot;
            }
            if (t_slot is T1_RelaySlot)
            {
                T1_RelaySlot slot = t_slot as T1_RelaySlot;
                RelaySlot i_slot = new RelaySlot();

                i_slot.T_RelaySlot = slot;
                return i_slot;
            }
            if (t_slot is T1_DigitRransducerInSlot)
            {
                T1_DigitRransducerInSlot slot = t_slot as T1_DigitRransducerInSlot;
                DigitRransducerInSlot i_slot = new DigitRransducerInSlot();

                i_slot.T_DigitRransducerInSlot = slot;
                return i_slot;
            }
            if (t_slot is T1_DigitRransducerOutSlot)
            {
                T1_DigitRransducerOutSlot slot = t_slot as T1_DigitRransducerOutSlot;
                DigitRransducerOutSlot i_slot = new DigitRransducerOutSlot();

                i_slot.T_DigitRransducerOutSlot = slot;
                return i_slot;
            }
            if (t_slot is T1_AnalogRransducerOutSlot)
            {
                T1_AnalogRransducerOutSlot slot = t_slot as T1_AnalogRransducerOutSlot;
                AnalogRransducerOutSlot i_slot = new AnalogRransducerOutSlot();

                i_slot.T_AnalogRransducerOutSlot = slot;
                return i_slot;
            }
            if (t_slot is T1_WirelessScalarSlot)
            {
                T1_WirelessScalarSlot slot = t_slot as T1_WirelessScalarSlot;
                WirelessScalarSlot i_slot = new WirelessScalarSlot();
                i_slot.SlotNum = slot.SlotNum;

                i_slot.T_WirelessScalarSlot = slot;
                return i_slot;
            }
            if (t_slot is T1_WirelessVibrationSlot)
            {
                T1_WirelessVibrationSlot slot = t_slot as T1_WirelessVibrationSlot;
                WirelessVibrationSlot i_slot = new WirelessVibrationSlot();
                i_slot.Integration = slot.Integration;
                i_slot.Unit = slot.Unit;
                i_slot.SampleFreCategory = JsonConvert.DeserializeObject<List<SampleFreCategory>>(slot.SampleFreCategory);
                i_slot.SampleFreCode = slot.SampleFreCode;
                i_slot.SamplePointCategory = JsonConvert.DeserializeObject<List<SamplePointCategory>>(slot.SamplePointCategory);
                i_slot.SamplePointCode = slot.SamplePointCode;
                i_slot.SlotNum = slot.SlotNum;

                i_slot.T_WirelessVibrationSlot = slot;
                return i_slot;
            }
            return null;
        }

        public IChannel BaseChannelConvert(I_BaseChannelInfo t_channel)
        {
            #region IEPEChannelInfo
            if (t_channel is T1_IEPEChannelInfo)
            {
                T1_IEPEChannelInfo channel = t_channel as T1_IEPEChannelInfo;
                IEPEChannelInfo i_channel = new IEPEChannelInfo();
                CalibrationlInfo calibrationlInfo = JsonConvert.DeserializeObject<CalibrationlInfo>(channel.CalibrationlInfo);
                i_channel.VelocityCalibration = calibrationlInfo.VelocityCalibration;
                i_channel.DisplacementCalibration = calibrationlInfo.DisplacementCalibration;
                OtherInfo otherInfo = JsonConvert.DeserializeObject<OtherInfo>(channel.OtherInfo);
                i_channel.RPMCardNum = otherInfo.RPMCardNum;
                i_channel.RPMSlotNum = otherInfo.RPMSlotNum;
                i_channel.RPMCHNum = otherInfo.RPMCHNum;
                i_channel.IsMultiplication = otherInfo.IsMultiplication;
                i_channel.MultiplicationCor = otherInfo.MultiplicationCor;
                i_channel.IsSaveWaveToSD = otherInfo.IsSaveWaveToSD;
                i_channel.IsUploadWave = otherInfo.IsUploadWave;
                i_channel.DefaultRPM = otherInfo.DefaultRPM;
                VibrationAddition vibrationAddition = JsonConvert.DeserializeObject<VibrationAddition>(channel.VibrationAddition);
                i_channel.TPDirCategory = vibrationAddition.TPDirCategory;
                i_channel.TPDirCode = vibrationAddition.TPDirCode;
                i_channel.BiasVoltHigh = vibrationAddition.BiasVoltHigh;
                i_channel.BiasVoltLow = vibrationAddition.BiasVoltLow;
                i_channel.Sensitivity = vibrationAddition.Sensitivity;

                i_channel.T_IEPEChannelInfo = channel;
                return i_channel;
            }
            #endregion

            #region EddyCurrentDisplacementChannelInfo
            if (t_channel is T1_EddyCurrentDisplacementChannelInfo)
            {
                T1_EddyCurrentDisplacementChannelInfo channel = t_channel as T1_EddyCurrentDisplacementChannelInfo;
                EddyCurrentDisplacementChannelInfo i_channel = new EddyCurrentDisplacementChannelInfo();
                OtherInfo otherInfo = JsonConvert.DeserializeObject<OtherInfo>(channel.OtherInfo);
                i_channel.RPMCardNum = otherInfo.RPMCardNum;
                i_channel.RPMSlotNum = otherInfo.RPMSlotNum;
                i_channel.RPMCHNum = otherInfo.RPMCHNum;
                i_channel.IsMultiplication = otherInfo.IsMultiplication;
                i_channel.MultiplicationCor = otherInfo.MultiplicationCor;
                i_channel.IsSaveWaveToSD = otherInfo.IsSaveWaveToSD;
                i_channel.IsUploadWave = otherInfo.IsUploadWave;
                i_channel.DefaultRPM = otherInfo.DefaultRPM;
                VibrationAddition vibrationAddition = JsonConvert.DeserializeObject<VibrationAddition>(channel.VibrationAddition);
                i_channel.TPDirCategory = vibrationAddition.TPDirCategory;
                i_channel.TPDirCode = vibrationAddition.TPDirCode;
                i_channel.BiasVoltHigh = vibrationAddition.BiasVoltHigh;
                i_channel.BiasVoltLow = vibrationAddition.BiasVoltLow;
                i_channel.Sensitivity = vibrationAddition.Sensitivity;

                i_channel.T_EddyCurrentDisplacementChannelInfo = channel;
                return i_channel;
            }
            #endregion

            #region EddyCurrentKeyPhaseChannelInfo
            if (t_channel is T1_EddyCurrentKeyPhaseChannelInfo)
            {
                T1_EddyCurrentKeyPhaseChannelInfo channel = t_channel as T1_EddyCurrentKeyPhaseChannelInfo;
                EddyCurrentKeyPhaseChannelInfo i_channel = new EddyCurrentKeyPhaseChannelInfo();
                ThresholdInfo thresholdInfo = JsonConvert.DeserializeObject<ThresholdInfo>(channel.ThresholdInfo);
                i_channel.ThresholdVolt = thresholdInfo.ThresholdVolt;
                i_channel.HysteresisVolt = thresholdInfo.HysteresisVolt;
                i_channel.ThresholdModeCategory = thresholdInfo.ThresholdModeCategory;
                i_channel.ThresholdModeCode = thresholdInfo.ThresholdModeCode;
                VibrationAddition vibrationAddition = JsonConvert.DeserializeObject<VibrationAddition>(channel.VibrationAddition);
                i_channel.TPDirCategory = vibrationAddition.TPDirCategory;
                i_channel.TPDirCode = vibrationAddition.TPDirCode;
                i_channel.BiasVoltHigh = vibrationAddition.BiasVoltHigh;
                i_channel.BiasVoltLow = vibrationAddition.BiasVoltLow;
                i_channel.Sensitivity = vibrationAddition.Sensitivity;
                RPMChannelInfo rPMChannelInfo = JsonConvert.DeserializeObject<RPMChannelInfo>(channel.RPMChannelInfo);
                i_channel.CalibrationCor = rPMChannelInfo.CalibrationCor;
                i_channel.IsNotch = rPMChannelInfo.IsNotch;
                i_channel.AverageNumber = rPMChannelInfo.AverageNumber;
                i_channel.TeethNumber = rPMChannelInfo.TeethNumber;

                i_channel.T_EddyCurrentKeyPhaseChannelInfo = channel;
                return i_channel;
            }
            #endregion

            #region EddyCurrentTachometerChannelInfo
            if (t_channel is T1_EddyCurrentTachometerChannelInfo)
            {
                T1_EddyCurrentTachometerChannelInfo channel = t_channel as T1_EddyCurrentTachometerChannelInfo;
                EddyCurrentTachometerChannelInfo i_channel = new EddyCurrentTachometerChannelInfo();
                ThresholdInfo thresholdInfo = JsonConvert.DeserializeObject<ThresholdInfo>(channel.ThresholdInfo);
                i_channel.ThresholdVolt = thresholdInfo.ThresholdVolt;
                i_channel.HysteresisVolt = thresholdInfo.HysteresisVolt;
                i_channel.ThresholdModeCategory = thresholdInfo.ThresholdModeCategory;
                i_channel.ThresholdModeCode = thresholdInfo.ThresholdModeCode;
                VibrationAddition vibrationAddition = JsonConvert.DeserializeObject<VibrationAddition>(channel.VibrationAddition);
                i_channel.TPDirCategory = vibrationAddition.TPDirCategory;
                i_channel.TPDirCode = vibrationAddition.TPDirCode;
                i_channel.BiasVoltHigh = vibrationAddition.BiasVoltHigh;
                i_channel.BiasVoltLow = vibrationAddition.BiasVoltLow;
                i_channel.Sensitivity = vibrationAddition.Sensitivity;
                RPMChannelInfo rPMChannelInfo = JsonConvert.DeserializeObject<RPMChannelInfo>(channel.RPMChannelInfo);
                i_channel.CalibrationCor = rPMChannelInfo.CalibrationCor;
                i_channel.IsNotch = rPMChannelInfo.IsNotch;
                i_channel.AverageNumber = rPMChannelInfo.AverageNumber;
                i_channel.TeethNumber = rPMChannelInfo.TeethNumber;
                RPMCouplingInfo rPMCouplingInfo = JsonConvert.DeserializeObject<RPMCouplingInfo>(channel.RPMCouplingInfo);
                i_channel.RPMCouplingCategory = rPMCouplingInfo.RPMCouplingCategory;
                i_channel.RPMCouplingCode = rPMCouplingInfo.RPMCouplingCode;

                i_channel.T_EddyCurrentTachometerChannelInfo = channel;
                return i_channel;
            }
            #endregion

            #region DigitTachometerChannelInfo
            if (t_channel is T1_DigitTachometerChannelInfo)
            {
                T1_DigitTachometerChannelInfo channel = t_channel as T1_DigitTachometerChannelInfo;
                DigitTachometerChannelInfo i_channel = new DigitTachometerChannelInfo();
                RPMChannelInfo rPMChannelInfo = JsonConvert.DeserializeObject<RPMChannelInfo>(channel.RPMChannelInfo);
                i_channel.CalibrationCor = rPMChannelInfo.CalibrationCor;
                i_channel.IsNotch = rPMChannelInfo.IsNotch;
                i_channel.AverageNumber = rPMChannelInfo.AverageNumber;
                i_channel.TeethNumber = rPMChannelInfo.TeethNumber;

                i_channel.T_DigitTachometerChannelInfo = channel;
                return i_channel;
            }
            #endregion

            #region AnalogRransducerInChannelInfo
            if (t_channel is T1_AnalogRransducerInChannelInfo)
            {
                T1_AnalogRransducerInChannelInfo channel = t_channel as T1_AnalogRransducerInChannelInfo;
                AnalogRransducerInChannelInfo i_channel = new AnalogRransducerInChannelInfo();
                TransformMethod transformMethod = JsonConvert.DeserializeObject<TransformMethod>(channel.TransformMethod);
                i_channel.EquationCategory = transformMethod.EquationCategory;
                i_channel.EquationCode = transformMethod.EquationCode;

                i_channel.T_AnalogRransducerInChannelInfo = channel;
                return i_channel;
            }
            #endregion

            #region RelayChannelInfo
            if (t_channel is T1_RelayChannelInfo)
            {
                T1_RelayChannelInfo channel = t_channel as T1_RelayChannelInfo;
                RelayChannelInfo i_channel = new RelayChannelInfo();              

                i_channel.T_RelayChannelInfo = channel;
                return i_channel;
            }
            #endregion

            #region DigitRransducerInChannelInfo
            if (t_channel is T1_DigitRransducerInChannelInfo)
            {
                T1_DigitRransducerInChannelInfo channel = t_channel as T1_DigitRransducerInChannelInfo;
                DigitRransducerInChannelInfo i_channel = new DigitRransducerInChannelInfo();
                TransformMethod transformMethod = JsonConvert.DeserializeObject<TransformMethod>(channel.TransformMethod);
                i_channel.EquationCategory = transformMethod.EquationCategory;
                i_channel.EquationCode = transformMethod.EquationCode;
                DigitRransducerInfo digitRransducerInfo = JsonConvert.DeserializeObject<DigitRransducerInfo>(channel.DigitRransducerInfo);
                digitRransducerInfo.SwitchCategory = digitRransducerInfo.SwitchCategory;
                digitRransducerInfo.SwitchCode = digitRransducerInfo.SwitchCode;
                digitRransducerInfo.ModBusFunCategory = digitRransducerInfo.ModBusFunCategory;
                digitRransducerInfo.ModBusFunCode = digitRransducerInfo.ModBusFunCode;

                i_channel.T_DigitRransducerInChannelInfo = channel;
                return i_channel;
            }
            #endregion

            #region DigitRransducerOutChannelInfo
            if (t_channel is T1_DigitRransducerOutChannelInfo)
            {
                T1_DigitRransducerOutChannelInfo channel = t_channel as T1_DigitRransducerOutChannelInfo;
                DigitRransducerOutChannelInfo i_channel = new DigitRransducerOutChannelInfo();
                TransformMethod transformMethod = JsonConvert.DeserializeObject<TransformMethod>(channel.TransformMethod);
                i_channel.EquationCategory = transformMethod.EquationCategory;
                i_channel.EquationCode = transformMethod.EquationCode;
                DigitRransducerInfo digitRransducerInfo = JsonConvert.DeserializeObject<DigitRransducerInfo>(channel.DigitRransducerInfo);
                digitRransducerInfo.SwitchCategory = digitRransducerInfo.SwitchCategory;
                digitRransducerInfo.SwitchCode = digitRransducerInfo.SwitchCode;
                digitRransducerInfo.ModBusFunCategory = digitRransducerInfo.ModBusFunCategory;
                digitRransducerInfo.ModBusFunCode = digitRransducerInfo.ModBusFunCode;
                SourceChannelInfo sourceChannelInfo = JsonConvert.DeserializeObject<SourceChannelInfo>(channel.SourceChannelInfo);
                i_channel.SourceCardNum = sourceChannelInfo.SourceCardNum;
                i_channel.SourceSlotNum = sourceChannelInfo.SourceSlotNum;
                i_channel.SourceCHNum = sourceChannelInfo.SourceCHNum;
                i_channel.SourceSubCHNum = sourceChannelInfo.SourceSubCHNum;

                i_channel.T_DigitRransducerOutChannelInfo = channel;
                return i_channel;
            }
            #endregion

            #region AnalogRransducerOutChannelInfo
            if (t_channel is T1_AnalogRransducerOutChannelInfo)
            {
                T1_AnalogRransducerOutChannelInfo channel = t_channel as T1_AnalogRransducerOutChannelInfo;
                AnalogRransducerOutChannelInfo i_channel = new AnalogRransducerOutChannelInfo();
                TransformMethod transformMethod = JsonConvert.DeserializeObject<TransformMethod>(channel.TransformMethod);
                i_channel.EquationCategory = transformMethod.EquationCategory;
                i_channel.EquationCode = transformMethod.EquationCode;
                SourceChannelInfo sourceChannelInfo = JsonConvert.DeserializeObject<SourceChannelInfo>(channel.SourceChannelInfo);
                i_channel.SourceCardNum = sourceChannelInfo.SourceCardNum;
                i_channel.SourceSlotNum = sourceChannelInfo.SourceSlotNum;
                i_channel.SourceCHNum = sourceChannelInfo.SourceCHNum;
                i_channel.SourceSubCHNum = sourceChannelInfo.SourceSubCHNum;

                i_channel.T_AnalogRransducerOutChannelInfo = channel;
                return i_channel;
            }
            #endregion

            #region WirelessScalarChannelInfo
            if (t_channel is T1_WirelessScalarChannelInfo)
            {
                T1_WirelessScalarChannelInfo channel = t_channel as T1_WirelessScalarChannelInfo;
                WirelessScalarChannelInfo i_channel = new WirelessScalarChannelInfo();
                TransformMethod transformMethod = JsonConvert.DeserializeObject<TransformMethod>(channel.TransformMethod);
                i_channel.EquationCategory = transformMethod.EquationCategory;
                i_channel.EquationCode = transformMethod.EquationCode;

                i_channel.T_WirelessScalarChannelInfo = channel;
                return i_channel;
            }
            #endregion

            #region WirelessVibrationChannelInfo
            if (t_channel is T1_WirelessVibrationChannelInfo)
            {
                T1_WirelessVibrationChannelInfo channel = t_channel as T1_WirelessVibrationChannelInfo;
                WirelessVibrationChannelInfo i_channel = new WirelessVibrationChannelInfo();
                CalibrationlInfo calibrationlInfo = JsonConvert.DeserializeObject<CalibrationlInfo>(channel.CalibrationlInfo);
                i_channel.VelocityCalibration = calibrationlInfo.VelocityCalibration;
                i_channel.DisplacementCalibration = calibrationlInfo.DisplacementCalibration;
                OtherInfo otherInfo = JsonConvert.DeserializeObject<OtherInfo>(channel.OtherInfo);
                i_channel.RPMCardNum = otherInfo.RPMCardNum;
                i_channel.RPMSlotNum = otherInfo.RPMSlotNum;
                i_channel.RPMCHNum = otherInfo.RPMCHNum;
                i_channel.IsMultiplication = otherInfo.IsMultiplication;
                i_channel.MultiplicationCor = otherInfo.MultiplicationCor;
                i_channel.IsSaveWaveToSD = otherInfo.IsSaveWaveToSD;
                i_channel.IsUploadWave = otherInfo.IsUploadWave;
                i_channel.DefaultRPM = otherInfo.DefaultRPM;
                VibrationAddition vibrationAddition = JsonConvert.DeserializeObject<VibrationAddition>(channel.VibrationAddition);
                i_channel.TPDirCategory = vibrationAddition.TPDirCategory;
                i_channel.TPDirCode = vibrationAddition.TPDirCode;
                i_channel.BiasVoltHigh = vibrationAddition.BiasVoltHigh;
                i_channel.BiasVoltLow = vibrationAddition.BiasVoltLow;
                i_channel.Sensitivity = vibrationAddition.Sensitivity;

                i_channel.T_WirelessVibrationChannelInfo = channel;
                return i_channel;
            }
            #endregion
            return null;
        }

        public AbstractSlotInfo AbstractSlotInfoConvert(T1_AbstractSlotInfo t_slot)
        {
            AbstractSlotInfo slot = new AbstractSlotInfo();
            slot.InSignalCategory = JsonConvert.DeserializeObject<List<InSignalCategory>>(t_slot.InSignalCategory);
            slot.InSignalCode = t_slot.InSignalCode;
            slot.SlotNum = t_slot.SlotNum;
            slot.SlotName = t_slot.SlotName;
            slot.UploadIntevalTime = t_slot.UploadIntevalTime.Value;
            slot.IsInput = t_slot.IsInput.Value;
            slot.Unit = t_slot.Unit;
            slot.Version = t_slot.Version;

            slot.T_AbstractSlotInfo = t_slot;
            return slot;
        }

        public void AbstractSlotInfoConvert(AbstractSlotInfo slot, T1_AbstractSlotInfo t_slot)
        {           
            slot.InSignalCategory = JsonConvert.DeserializeObject<List<InSignalCategory>>(t_slot.InSignalCategory);
            slot.InSignalCode = t_slot.InSignalCode;
            slot.SlotNum = t_slot.SlotNum;
            slot.SlotName = t_slot.SlotName;
            slot.UploadIntevalTime = t_slot.UploadIntevalTime.Value;
            slot.IsInput = t_slot.IsInput.Value;
            slot.Unit = t_slot.Unit;
            slot.Version = t_slot.Version;

            slot.T_AbstractSlotInfo = t_slot;
        }

        public AbstractChannelInfo AbstractChannelInfoConvert(T1_AbstractChannelInfo t_channel)
        {
            AbstractChannelInfo channel = new AbstractChannelInfo();
            channel.Organization = JsonConvert.DeserializeObject<List<Organization>>(t_channel.Organization);
            channel.T_Device_Name = t_channel.T_Device_Name;
            channel.T_Device_Code = t_channel.T_Device_Code;
            channel.T_Device_Guid = t_channel.T_Device_Guid.ToString();
            channel.T_Item_Name = t_channel.T_Item_Name;
            channel.T_Item_Code = t_channel.T_Item_Code;
            channel.T_Item_Guid = t_channel.T_Item_Guid.ToString();
            channel.CHNum = t_channel.CHNum;
            channel.SubCHNum = t_channel.SubCHNum;
            channel.IsUploadData = t_channel.IsUploadData;
            channel.Unit = t_channel.Unit;
            channel.SVTypeCategory = JsonConvert.DeserializeObject<List<SVTypeCategory>>(t_channel.SVTypeCategory);
            channel.SVTypeCode = t_channel.SVTypeCode;
            channel.LocalSaveCategory = JsonConvert.DeserializeObject<List<LocalSaveCategory>>(t_channel.LocalSaveCategory);
            channel.LocalSaveCode = t_channel.LocalSaveCode;
            channel.IsBypass = t_channel.IsBypass;
            channel.DelayAlarmTime = t_channel.DelayAlarmTime;
            channel.NotOKDelayAlarmTime = t_channel.NotOKDelayAlarmTime;
            channel.IsLogic = t_channel.IsLogic;
            channel.LogicExpression = t_channel.LogicExpression;
            channel.Remarks = t_channel.Remarks;
            channel.Extra_Information = t_channel.Extra_Information;
            channel.AlarmStrategy = JsonConvert.DeserializeObject<AlarmStrategy>(t_channel.AlarmStrategy);

            channel.T_AbstractChannelInfo = t_channel;
            return channel;
        }

        public void AbstractChannelInfoConvert(AbstractChannelInfo channel, T1_AbstractChannelInfo t_channel)
        {         
            channel.Organization = JsonConvert.DeserializeObject<List<Organization>>(t_channel.Organization);
            channel.T_Device_Name = t_channel.T_Device_Name;
            channel.T_Device_Code = t_channel.T_Device_Code;
            channel.T_Device_Guid = t_channel.T_Device_Guid.ToString();
            channel.T_Item_Name = t_channel.T_Item_Name;
            channel.T_Item_Code = t_channel.T_Item_Code;
            channel.T_Item_Guid = t_channel.T_Item_Guid.ToString();
            channel.CHNum = t_channel.CHNum;
            channel.SubCHNum = t_channel.SubCHNum;
            channel.IsUploadData = t_channel.IsUploadData;
            channel.Unit = t_channel.Unit;
            channel.SVTypeCategory = JsonConvert.DeserializeObject<List<SVTypeCategory>>(t_channel.SVTypeCategory);
            channel.SVTypeCode = t_channel.SVTypeCode;
            channel.LocalSaveCategory = JsonConvert.DeserializeObject<List<LocalSaveCategory>>(t_channel.LocalSaveCategory);
            channel.LocalSaveCode = t_channel.LocalSaveCode;
            channel.IsBypass = t_channel.IsBypass;
            channel.DelayAlarmTime = t_channel.DelayAlarmTime;
            channel.NotOKDelayAlarmTime = t_channel.NotOKDelayAlarmTime;
            channel.IsLogic = t_channel.IsLogic;
            channel.LogicExpression = t_channel.LogicExpression;
            channel.Remarks = t_channel.Remarks;
            channel.Extra_Information = t_channel.Extra_Information;
            channel.AlarmStrategy = JsonConvert.DeserializeObject<AlarmStrategy>(t_channel.AlarmStrategy);

            channel.T_AbstractChannelInfo = t_channel;
        }

        public DivFreInfo DivFreInfoConvert(T1_DivFreInfo t_divfreInfo)
        {
            DivFreInfo divfreInfo = new DivFreInfo();
            divfreInfo.Guid = t_divfreInfo.Guid.ToString();
            divfreInfo.Name = t_divfreInfo.Name;
            if (t_divfreInfo.Create_Time != null)
            {
                divfreInfo.Create_Time = t_divfreInfo.Create_Time.Value.ToString("yyyy-MM-dd HH:mm:ss");
            }
            else
            {
                divfreInfo.Create_Time = "";
            }
            if (t_divfreInfo.Modify_Time != null)
            {
                divfreInfo.Modify_Time = t_divfreInfo.Modify_Time.Value.ToString("yyyy-MM-dd HH:mm:ss");
            }
            else
            {
                divfreInfo.Modify_Time = "";
            }
            divfreInfo.Remarks = t_divfreInfo.Remarks;
            divfreInfo.T_Item_Guid = t_divfreInfo.T_Item_Guid.ToString();
            divfreInfo.T_Item_Name = t_divfreInfo.T_Item_Name;
            divfreInfo.T_Item_Code = t_divfreInfo.T_Item_Code;
            divfreInfo.DivFreCode = t_divfreInfo.DivFreCode;
            divfreInfo.BasedOnRPM = JsonConvert.DeserializeObject<BasedOnRPM>(t_divfreInfo.BasedOnRPM);
            divfreInfo.FixedFre = JsonConvert.DeserializeObject<FixedFre>(t_divfreInfo.FixedFre);
            divfreInfo.BasedOnRange = JsonConvert.DeserializeObject<BasedOnRange>(t_divfreInfo.BasedOnRange);
            divfreInfo.AlarmStrategy = JsonConvert.DeserializeObject<AlarmStrategy>(t_divfreInfo.AlarmStrategy);

            divfreInfo.T_DivFreInfo = t_divfreInfo;

            return divfreInfo;
        }        
    }
}
