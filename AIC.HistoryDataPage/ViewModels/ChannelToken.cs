﻿using AIC.Core.DataModels;
using AIC.Core.LMModels;
using AIC.Core.Models;
using AIC.HistoryDataPage.Models;
using AIC.M9600.Common.SlaveDB.Generated;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIC.HistoryDataPage.ViewModels
{
    public class ChannelToken
    {
        //public IChannel Channel { get; set; }
        //public T1_Item T_Item { get; set; }
        public string DisplayName { get; set; }
        public string IP { get; set; }
        public Guid Guid { get; set; }
    }

    public class BaseWaveChannelToken : ChannelToken
    {
        private event EventHandler disposed;
        private event EventHandler dataChanged;
        public List<IBaseWaveSlot> DataContracts { get; set; }
        public VibrationData VData { get; set; }
        public int CurrentIndex { get; set; }
        public void RaiseDataChanged()
        {
            if (dataChanged != null)
            {
                dataChanged(this, EventArgs.Empty);
            }
        }
        public void RaiseDisposed()
        {
            if (disposed != null)
            {
                disposed(this, EventArgs.Empty);
            }
        }
        public IObservable<object> WhenDataChanged
        {
            get
            {
                return Observable
                    .FromEventPattern(
                        h => this.dataChanged += h,
                        h => this.dataChanged -= h);
            }
        }
        public IObservable<object> WhenDisposed
        {
            get
            {
                return Observable
                    .FromEventPattern(
                        h => this.disposed += h,
                        h => this.disposed -= h);
            }
        }
    }

    public class WirelessVibrationWaveChannelToken : BaseWaveChannelToken
    {       
        public new List<D_WirelessVibrationSlot_Waveform> DataContracts { get; set; }      
    }

    public class BaseDivfreChannelToken : BaseWaveChannelToken
    {      
        public new List<IBaseDivfreSlot> DataContracts { get; set; }  
    }

    public class BaseAlarmChannelToken : ChannelToken
    {
        public List<IBaseAlarmSlot> DataContracts { get; set; }
    }


    public class WirelessScalarChannelToken : BaseAlarmChannelToken
    {
        //public List<D_WirelessScalarSlot> DataContracts { get; set; }
    }

    public class WirelessVibrationChannelToken : BaseDivfreChannelToken
    {
        //public List<D_WirelessVibrationSlot>  DataContracts { get; set; }
    }

    public class DivFreChannelToken : ChannelToken
    {
        private event EventHandler disposed;
        private event EventHandler dataChanged;    
        public ChannelType ChannelType { get; set; }
        public T1_DivFreInfo DivFreChannel { get; set; }
        public List<D1_DivFreInfo> DataContracts { get; set; }
        public List<IBaseDivfreSlot> SlotDataContracts { get; set; }
        public void RaiseDataChanged()
        {
            if (dataChanged != null)
            {
                dataChanged(this, EventArgs.Empty);
            }
        }
        public void RaiseDisposed()
        {
            if (disposed != null)
            {
                disposed(this, EventArgs.Empty);
            }
        }
        public IObservable<object> WhenDataChanged
        {
            get
            {
                return Observable
                    .FromEventPattern(
                        h => this.dataChanged += h,
                        h => this.dataChanged -= h);
            }
        }
        public IObservable<object> WhenDisposed
        {
            get
            {
                return Observable
                    .FromEventPattern(
                        h => this.disposed += h,
                        h => this.disposed -= h);
            }
        }
    }

    //htzk123, 暂时替代
    public class ChannelDistributionContract { }
    public class VInfoTableAMSContract { }
    public class AnInfoTableAMSContract { }
    public class DivFreDistributionContract { }
    public class DivFreTableFreContract { }

    public class SnapshotAMSContract2 { }
    public class SnapshotContract2 { }
}
