using AIC.Core.DataModels;
using AIC.Core.HardwareModels;
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
using System.Windows.Media;

namespace AIC.HistoryDataPage.Models
{
    public class ChannelToken
    {
        public string DisplayName { get; set; }
        public string IP { get; set; }
        public Guid Guid { get; set; }
        public int ItemType { get; set; }
        public SolidColorBrush SolidColorBrush { get; set; }
    }

    public class BaseWaveChannelToken : BaseAlarmChannelToken
    {        
        private event EventHandler disposed;
        private event EventHandler dataChanged;
        //public new List<IBaseWaveSlot> DataContracts { get; set; }
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

    public class BaseDivfreChannelToken : BaseWaveChannelToken
    {
        //public new List<IBaseDivfreSlot> DataContracts { get; set; }  
    }

    public class BaseAlarmChannelToken : ChannelToken
    {
        public List<IBaseAlarmSlot> DataContracts { get; set; }
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
