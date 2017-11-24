using AIC.Core.DataModels;
using AIC.Core.SignalModels;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace AIC.HistoryDataPage.Models
{
    public delegate void LimitChangedHandler(SignalToken token);
    public class SignalToken : BindableBase
    {
        public event LimitChangedHandler LimitChanged;
        private int index;
        public int Index
        {
            get { return index; }
            set
            {
                if (index != value)
                {
                    index = value;
                    OnPropertyChanged(() => Index);
                }
            }
        }
        public string DisplayName { get; set; }
        public string IP { get; set; }
        public Guid Guid { get; set; }
        public int ItemType { get; set; }
        public BaseAlarmSignal BaseAlarmSignal { get; set; }
        private double lowerLimit;
        public double LowerLimit
        {
            get { return lowerLimit; }
            set
            {
                if (lowerLimit != value)
                {
                    lowerLimit = value;
                    OnPropertyChanged(() => LowerLimit);
                    if (LimitChanged != null)
                    {
                        LimitChanged(this);
                    }
                }
            }
        }
        private double upperLimit;
        public double UpperLimit
        {
            get { return upperLimit; }
            set
            {
                if (upperLimit != value)
                {
                    upperLimit = value;
                    OnPropertyChanged(() => UpperLimit);
                    if (LimitChanged != null)
                    {
                        LimitChanged(this);
                    }
                }
            }
        }
        public string Unit { get; set; }

        private double selectedResult;
        public double SelectedResult
        {
            get { return selectedResult; }
            set
            {
                if (selectedResult != value)
                {
                    selectedResult = value;
                    OnPropertyChanged(() => SelectedResult);
                }
            }
        }
        private DateTime selectedTime { get; set; }
        public DateTime SelectedTime
        {
            get { return selectedTime; }
            set
            {
                if (selectedTime != value)
                {
                    selectedTime = value;
                    OnPropertyChanged(() => SelectedTime);
                }
            }
        }       
        public bool IsShow { get; set; }
        public SolidColorBrush SolidColorBrush { get; set;}
        public ChannelToken ChannelToken { get; set; }
    }

    public class BaseWaveSignalToken : SignalToken
    {
        public List<IBaseWaveSlot> FirstDatas { get; set; }
        public List<IBaseWaveSlot> SecondDatas { get; set; }
        public List<IBaseWaveSlot> ThirdDatas { get; set; }
        public List<IBaseWaveSlot> FourthDatas { get; set; }
        public List<IBaseWaveSlot> FifthDatas { get; set; }
        public List<IBaseWaveSlot> SixthDatas { get; set; }
        public List<IBaseWaveSlot> PreviousDatas { get; set; }
        public List<IBaseWaveSlot> NextDatas { get; set; }
        public List<IBaseWaveSlot> DataContracts
        {
            get
            {
                return (FirstDatas ?? new List<IBaseWaveSlot>())
                    .Union(SecondDatas ?? new List<IBaseWaveSlot>())
                    .Union(ThirdDatas ?? new List<IBaseWaveSlot>())
                    .Union(FourthDatas ?? new List<IBaseWaveSlot>())
                    .Union(FifthDatas ?? new List<IBaseWaveSlot>())
                    .Union(SixthDatas ?? new List<IBaseWaveSlot>())
                    .ToList();
            }
        }

        public VibrationData VData { get; set; }
        public int CurrentIndex { get; set; }
    }

    public class BaseDivfreSignalToken : BaseWaveSignalToken
    {   
        public new List<IBaseDivfreSlot> FirstDatas { get; set; }
        public new List<IBaseDivfreSlot> SecondDatas { get; set; }
        public new List<IBaseDivfreSlot> ThirdDatas { get; set; }
        public new List<IBaseDivfreSlot> FourthDatas { get; set; }
        public new List<IBaseDivfreSlot> FifthDatas { get; set; }
        public new List<IBaseDivfreSlot> SixthDatas { get; set; }
        public new List<IBaseDivfreSlot> PreviousDatas { get; set; }
        public new List<IBaseDivfreSlot> NextDatas { get; set; }
        public new List<IBaseDivfreSlot> DataContracts
        {
            get
            {
                return (FirstDatas ?? new List<IBaseDivfreSlot>())
                    .Union(SecondDatas ?? new List<IBaseDivfreSlot>())
                    .Union(ThirdDatas ?? new List<IBaseDivfreSlot>())
                    .Union(FourthDatas ?? new List<IBaseDivfreSlot>())
                    .Union(FifthDatas ?? new List<IBaseDivfreSlot>())
                    .Union(SixthDatas ?? new List<IBaseDivfreSlot>())
                    .ToList();
            }
        }
    }

    public class BaseAlarmSignalToken : SignalToken
    {     
        public List<IBaseAlarmSlot> FirstDatas { get; set; }
        public List<IBaseAlarmSlot> SecondDatas { get; set; }
        public List<IBaseAlarmSlot> ThirdDatas { get; set; }
        public List<IBaseAlarmSlot> FourthDatas { get; set; }
        public List<IBaseAlarmSlot> FifthDatas { get; set; }
        public List<IBaseAlarmSlot> SixthDatas { get; set; }
        public List<IBaseAlarmSlot> PreviousDatas { get; set; }
        public List<IBaseAlarmSlot> NextDatas { get; set; }
        public List<IBaseAlarmSlot> DataContracts
        {
            get
            {
                return (FirstDatas ?? new List<IBaseAlarmSlot>())
                    .Union(SecondDatas ?? new List<IBaseAlarmSlot>())
                    .Union(ThirdDatas ?? new List<IBaseAlarmSlot>())
                    .Union(FourthDatas ?? new List<IBaseAlarmSlot>())
                    .Union(FifthDatas ?? new List<IBaseAlarmSlot>())
                    .Union(SixthDatas ?? new List<IBaseAlarmSlot>())
                    .ToList();
            }
        }
    }
}
