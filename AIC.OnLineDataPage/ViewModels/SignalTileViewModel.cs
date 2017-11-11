using AIC.Core.SignalModels;
using AIC.CoreType;
using AIC.Domain;
using AIC.OnLineDataPage.ViewModels.SubViewModels;
using Prism.Commands;
using Prism.Mvvm;

namespace AIC.OnLineDataPage.ViewModels
{
    public class SignalTileViewModel : BindableBase, ISelectable
    {
        public SignalTileViewModel(BaseAlarmSignal signal)
        {
            this.signal = signal;
        }

        public void SetDisplayModeAsync(SignalDisplayType mode)
        {
            if (DisplayMode != mode)
            {
                DisplayMode = mode;
                switch (mode)
                {
                    case SignalDisplayType.AMSTrend:
                        {
                            DataViewModel = new RMSTrendChartViewModel(Signal);
                            break;
                        }
                    case SignalDisplayType.TimeDomain:
                        if (Signal is BaseWaveSignal)
                        {
                            DataViewModel = new TimeDomainChartViewModel(Signal);
                        }
                        else
                        {
                            DataViewModel = null;
                        }
                        break;
                    case SignalDisplayType.FrequencyDomain:
                        if (Signal is BaseWaveSignal)
                        {
                            DataViewModel = new FrequencyDomainChartViewModel(Signal);
                        }
                        else
                        {
                            DataViewModel = null;
                        }
                        break;
                    case SignalDisplayType.PowerSpectrum:
                        if (Signal is BaseWaveSignal)
                        {
                            DataViewModel = new PowerSpectrumChartViewModel(Signal);
                        }
                        else
                        {
                            DataViewModel = null;
                        }
                        break;
                    case SignalDisplayType.PowerSpectrumDensity:
                        if (Signal is BaseWaveSignal)
                        {
                            DataViewModel = new PowerSpectrumDensityChartViewModel(Signal);
                        }
                        else
                        {
                            DataViewModel = null;
                        }
                        break;
                    case SignalDisplayType.Ortho:
                        if (Signal is BaseWaveSignal)
                        {
                            DataViewModel = new OrthoChartViewModel(Signal);
                        }
                        else
                        {
                            DataViewModel = null;
                        }
                        break;
                    case SignalDisplayType.Bode:
                        if (Signal is BaseWaveSignal)
                        {
                            DataViewModel = new BodeChartViewModel(Signal);
                        }
                        else
                        {
                            DataViewModel = null;
                        }
                        break;
                    case SignalDisplayType.Nyquist:
                        if (Signal is BaseWaveSignal)
                        {
                            DataViewModel = new NyquistChartViewModel(Signal);
                        }
                        else
                        {
                            DataViewModel = null;
                        }
                        break;
                    case SignalDisplayType.MultiDivFre:
                        if (Signal is BaseWaveSignal)
                        {
                            DataViewModel = new MultiDivFreChartViewModel(Signal);
                        }
                        else
                        {
                            DataViewModel = null;
                        }
                        break;
                    case SignalDisplayType.OrderAnalysis:
                        if (Signal is BaseWaveSignal)
                        {
                            DataViewModel = new OrthoChartViewModel(Signal);
                        }
                        else
                        {
                            DataViewModel = null;
                        }
                        break;
                    case SignalDisplayType.Time3DSpectrum:
                        if (Signal is BaseWaveSignal)
                        {
                            DataViewModel = new Time3DChartViewModel(Signal);
                        }
                        else
                        {
                            DataViewModel = null;
                        }
                        break;
                    case SignalDisplayType.RPM3D:
                        if (Signal is BaseWaveSignal)
                        {
                            DataViewModel = new RPM3DChartViewModel(Signal);
                        }
                        else
                        {
                            DataViewModel = null;
                        }
                        break;                   
                    default:
                        DataViewModel = null;
                        break;
                }
            }
        }

        public void Close()
        {
            if (DataViewModel != null)
            {
                DataViewModel.Close();               
            }
        }

        #region Public Property
        public bool IsUpdated { get; set; }

        private bool isShowAlarm = false;
        public bool IsShowAlarm
        {
            get { return isShowAlarm; }
            set
            {
                if (isShowAlarm != value)
                {
                    isShowAlarm = value;
                    OnPropertyChanged("IsShowAlarm");                   
                }
            }
        }

        private BaseAlarmSignal signal;
        public BaseAlarmSignal Signal
        {
            get { return signal; }
            set
            {
                if (signal != value)
                {
                    signal = value;
                    OnPropertyChanged(() => Signal);
                }
            }
        }
        
        public string DeviceName
        {
            get { return Signal.DeviceName; }
            private set
            {              
            }
        }

        public string NullName
        {
            get { return ""; }           
        }

        #region SignalMonitorVM
        private ChartViewModelBase dataViewModel;
        public ChartViewModelBase DataViewModel
        {
            get { return dataViewModel; }
            set
            {
                if (dataViewModel != value)
                {
                    dataViewModel = value;
                    OnPropertyChanged(() => DataViewModel);
                }
            }
        }
        #endregion

        #region DisplayMode
      //  public INotifyTaskCompletion<SignalDisplayType> DisplayMode { get; private set; }

        private SignalDisplayType displayMode;
        public SignalDisplayType DisplayMode
        {
            get { return displayMode; }
            set
            {
                if (displayMode != value)
                {
                    displayMode = value;
                    this.OnPropertyChanged("DisplayMode");
                }
            }
        }
        #endregion

        #region ItemWidth
        private double itemWidth;
        public double ItemWidth
        {
            get { return itemWidth; }
            set
            {
                if (itemWidth != value)
                {
                    itemWidth = value;
                    OnPropertyChanged("ItemWidth");
                }
            }
        }
        #endregion

        #region ItemHeight
        private double itemHeight;
        public double ItemHeight
        {
            get { return itemHeight; }
            set
            {
                if (itemHeight != value)
                {
                    itemHeight = value;
                    OnPropertyChanged("ItemHeight");
                }
            }
        }
        #endregion

        #region DivFreDescription
        private string divFreDescription;
        public string DivFreDescription
        {
            get { return divFreDescription; }
            set
            {
                if (divFreDescription != value)
                {
                    divFreDescription = value;
                    OnPropertyChanged("DivFreDescription");
                }
            }
        }
        #endregion

        #region Title
        private string title;
        public string Title
        {
            get { return title; }
            set
            {
                if (title != value)
                {
                    title = value;
                    OnPropertyChanged("Title");
                }
            }
        }
        #endregion

        #region IsSelected
        private bool isSelected;
        public bool IsSelected
        {
            get { return isSelected; }
            set
            {
                if (isSelected != value)
                {
                    isSelected = value;
                    this.OnPropertyChanged("IsSelected");
                }
            }
        }
        #endregion

        public DelegateCommand<object> CloseCommand { get; set; }

        #endregion
    }
}
