


using AIC.Core.Events;
using AIC.Core.SignalModels;
using AIC.CoreType;
using AIC.Domain;
using Akka.Actor;
using Arction.Wpf.Charting;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace AIC.OnLineDataPage.ViewModels.SubViewModels
{
    public class OrthoChartViewModel : ChartViewModelBase
    {
        public OrthoChartViewModel(BaseAlarmSignal signal)
            : base(signal)
        {
          
        }

        private ObservableCollection<BaseWaveSignal> relativeSignalCollection = new ObservableCollection<BaseWaveSignal>();
        //private ISignalCache signalCache;       

        private BaseWaveSignal selectedRelativeSignal;
        public BaseWaveSignal SelectedRelativeSignal
        {
            get { return selectedRelativeSignal; }
            set
            {
                if (selectedRelativeSignal != value)
                {
                    selectedRelativeSignal = value;
                    OnPropertyChanged(() => SelectedRelativeSignal);
                }
            }
        }

        public override void SetSignal(BaseAlarmSignal sg)
        {
            try
            {
                RemoveProcessor();
                if (sg is BaseWaveSignal)
                {
                    //Signal = sg;
                    //if (signalCache == null)
                    //{
                    //    signalCache = ServiceLocator.Current.GetInstance<ISignalCache>();
                    //}
                    //if (signalCache != null)
                    //{
                    //    var relatives = signalCache.Signals.OfType<VibrationSignal>().Where(o => o.IP == ((VibrationSignal)Signal).IP && o.SlotNum == ((VibrationSignal)Signal).SlotNum).Except(new VibrationSignal[] { ((VibrationSignal)Signal) });
                    //    foreach (var item in relatives)
                    //    {
                    //        relativeSignalCollection.Add(item);
                    //    }
                    //    SelectedRelativeSignal = RelativeSignals.FirstOrDefault();
                    //}
                    AddProcessor();
                }
                else
                {
                    Signal = null;
                    relativeSignalCollection.Clear();
                    SelectedRelativeSignal = null;
                }
                RaisedSiganlChanged();
            }
            catch (Exception ex)
            {
                EventAggregatorService.Instance.EventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("在线监测-轴心轨迹-设置信号", ex));
            }
        }

        public override void AddProcessor()
        {
            if (Signal is BaseWaveSignal)
            {
                ((BaseWaveSignal)Signal).AddProcess(SignalProcessorType.VData);
                ((BaseWaveSignal)Signal).AddProcess(SignalProcessorType.Frequency);
            }
        }

        public override void RemoveProcessor()
        {
            if (Signal is BaseWaveSignal)
            {
                ((BaseWaveSignal)Signal).RemoveProcess(SignalProcessorType.VData);
                ((BaseWaveSignal)Signal).RemoveProcess(SignalProcessorType.Frequency);
            }
        }


        public IEnumerable<BaseWaveSignal> RelativeSignals { get { return relativeSignalCollection; } }
    }
}
