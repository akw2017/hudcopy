using AIC.Cloud.Domain;
using AIC.Server.Storage.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;

namespace AIC.Cloud.DataReplayer.ViewModels
{
    public class ChannelToken
    {
        public ChannelDistributionContract Channel { get; set; }
        //public DateTime BeginTime { get; set; }
        //public DateTime EndTime { get; set; }
    }

    public class VibrationChannelToken : ChannelToken
    {
        private event EventHandler disposed;
        private event EventHandler dataChanged;
        public VInfoTableAMSContract[] DataContracts { get; set; }
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

    public class AnalogChannelToken : ChannelToken
    {
        public AnInfoTableAMSContract[] DataContracts { get; set; }
    }

    public class DivFreChannelToken:ChannelToken
    {
        private event EventHandler disposed;
        private event EventHandler dataChanged;
        public string GroupCOName { get; set; }
        public string CorporationName { get; set; }
        public string WorkShopName { get; set; }
        public string DevName { get; set; }
        public string DevSN { get; set; }
        public string Name { get; set; }
        public string MSSN { get; set; }
        public new DivFreDistributionContract Channel { get; set; }
        public DivFreTableFreContract[] DataContracts { get; set; }
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
}
