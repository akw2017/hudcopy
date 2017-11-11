using AIC.MatlabMath;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIC.Core.Filters
{
    public class BandPassFilter : NotifyDataErrorInfoBase
    {
        private Dictionary<string, double> _originalValues;

        private double passbandAttenuationDB = 0.2;
        private double stopbandAttenuationDB = 60;
        private double bpPassBandFreLow = 400;
        private double bpPassBandFreHigh = 600;
        private double transitionBandwidth = 100;
        private bool isValid;
        private bool isChanged;
        private readonly BandPassFilterValidator validator;
        private bool isRPMBinding;

        public BandPassFilter(double sampleFre = 1024)
        {
            _originalValues = new Dictionary<string, double>();
            validator = new BandPassFilterValidator();
            BindRPMCommand = new DelegateCommand<object>(BindRPM);
            UnBindRPMCommand = new DelegateCommand<object>(UnBindRPM);
            SaveCommand = new DelegateCommand<object>(Save, CanSave);
            ResetCommand = new DelegateCommand<object>(Reset, CanReset);
            PropertyChanged += BandPassFilter_PropertyChanged;
            Initialize(sampleFre);

            //WhenPropertyChanged.Where(o => o == "IsRPMBinding").Subscribe(OnIsRPMBindingChanged);
        }

        private void BandPassFilter_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            InvalidCommand();
        }

        private void Validate()
        {
            ClearErrors();
            var result = validator.Validate(this);
            if (result != null)
            {
                if (result.Errors.Any())
                {
                    var propertyNames = result.Errors.Select(o => o.PropertyName).Distinct().ToList();
                    foreach (var propertyName in propertyNames)
                    {
                        Errors[propertyName] = result.Errors
                            .Where(o => o.PropertyName == propertyName)
                            .Select(o => o.ErrorMessage)
                            .Distinct()
                            .ToList();
                        OnErrorsChanged(propertyName);
                    }
                }
                IsValid = result.IsValid;
            }
        }

        private void Initialize(double sampleFre)
        {
            TransitionBandwidth = (sampleFre / 2.56) / 6;
            BPPassBandFreLow = (sampleFre / 2.56) / 3;
            BPPassBandFreHigh = (sampleFre / 2.56) * 2 / 3;
            Save(null);
        }

        private void BindRPM(object args)
        {
            if (IsRPMBinding)
            {
                if (args is double && (double)args != 0)
                {
                    double rpm = (double)args;
                    //isRPMBinding = true;
                    TransitionBandwidth = (BPStopBandFreHigh - BPPassBandFreHigh) / (rpm / 60);
                    BPPassBandFreLow = BPPassBandFreLow / (rpm / 60);
                    BPPassBandFreHigh = BPPassBandFreHigh / (rpm / 60);
                    Save(null);
                }
            }
        }

        private void UnBindRPM(object args)
        {
            if (!IsRPMBinding)
            {
                if (args is double && (double)args != 0)
                {
                    double rpm = (double)args;
                    // isRPMBinding = false;
                    TransitionBandwidth = (BPStopBandFreHigh - BPPassBandFreHigh) * (rpm / 60);
                    BPPassBandFreLow = BPPassBandFreLow * (rpm / 60);
                    BPPassBandFreHigh = BPPassBandFreHigh * (rpm / 60);
                    Save(null);
                }
            }
        }

        private void Save(object args)
        {
            _originalValues["PassbandAttenuationDB"] = PassbandAttenuationDB;
            _originalValues["StopbandAttenuationDB"] = StopbandAttenuationDB;
            _originalValues["BPPassBandFreLow"] = BPPassBandFreLow;
            _originalValues["BPPassBandFreHigh"] = BPPassBandFreHigh;
            _originalValues["TransitionBandwidth"] = TransitionBandwidth;
            IsChanged = false;
        }
        private bool CanSave(object args)
        {
            return IsValid;
        }

        private void Reset(object args)
        {
            PassbandAttenuationDB = _originalValues["PassbandAttenuationDB"];
            StopbandAttenuationDB = _originalValues["StopbandAttenuationDB"];
            BPPassBandFreLow = _originalValues["BPPassBandFreLow"];
            BPPassBandFreHigh = _originalValues["BPPassBandFreHigh"];
            TransitionBandwidth = _originalValues["TransitionBandwidth"];
            IsChanged = false;
        }
        private bool CanReset(object args)
        {
            return IsChanged;
        }

        public DelegateCommand<object> BindRPMCommand { get; private set; }
        public DelegateCommand<object> UnBindRPMCommand { get; private set; }
        public DelegateCommand<object> SaveCommand { get; private set; }
        public DelegateCommand<object> ResetCommand { get; private set; }

        private void InvalidCommand()
        {
            SaveCommand.RaiseCanExecuteChanged();
            ResetCommand.RaiseCanExecuteChanged();
        }

        //通带衰减，建议值0.2
        public double PassbandAttenuationDB
        {
            get { return passbandAttenuationDB; }
            set
            {
                if (SetProperty(ref passbandAttenuationDB, value))
                {
                    IsChanged = true;
                }
            }
        }
        //阻带衰减，建议值60
        public double StopbandAttenuationDB
        {
            get { return stopbandAttenuationDB; }
            set
            {
                if (SetProperty(ref stopbandAttenuationDB, value))
                {
                    IsChanged = true;
                }
            }
        }

        //带通低阻带频率
        public double BPStopBandFreLow => BPPassBandFreLow - TransitionBandwidth;
        //带通高阻带频率
        public double BPStopBandFreHigh => BPPassBandFreHigh + TransitionBandwidth;

        //带通低逼近通带频率
        public double BPPassBandFreLow
        {
            get { return bpPassBandFreLow; }
            set
            {
                if (SetProperty(ref bpPassBandFreLow, value))
                {
                    Validate();
                    IsChanged = true;
                }
            }
        }
        //带通高逼近通带频率
        public double BPPassBandFreHigh
        {
            get { return bpPassBandFreHigh; }
            set
            {
                if (SetProperty(ref bpPassBandFreHigh, value))
                {
                    Validate();
                    IsChanged = true;
                }
            }
        }
        public double TransitionBandwidth
        {
            get { return transitionBandwidth; }
            set
            {
                if (SetProperty(ref transitionBandwidth, value))
                {
                    Validate();
                    IsChanged = true;
                }
            }
        }

        public bool IsValid
        {
            get { return isValid; }
            set { SetProperty(ref isValid, value); }
        }

        public bool IsChanged
        {
            get { return isChanged; }
            set { SetProperty(ref isChanged, value); }
        }

        public bool IsRPMBinding
        {
            get { return isRPMBinding; }
            set { SetProperty(ref isRPMBinding, value); }
        }

        public double[] Filter(double[] input, int samplePoint, double sampleFre, double rpm = 0)
        {
            if (!IsRPMBinding)
            {
                if (_originalValues["BPPassBandFreHigh"] + _originalValues["TransitionBandwidth"] > sampleFre / 2.56)
                {
                    throw new Exception(string.Format("带通高阻带频率必须小于等于采样频率的1/2.56({0})", sampleFre / 2.56));
                }
                return Algorithm.Instance.BandPassFilter(input, samplePoint, sampleFre,
                    _originalValues["PassbandAttenuationDB"],
                    _originalValues["StopbandAttenuationDB"],
                    _originalValues["BPPassBandFreLow"] - _originalValues["TransitionBandwidth"],
                    _originalValues["BPPassBandFreLow"],
                    _originalValues["BPPassBandFreHigh"],
                    _originalValues["BPPassBandFreHigh"] + _originalValues["TransitionBandwidth"]);

                // BPPassBandFreLow / (rpm / 60);
            }
            else
            {
                if ((_originalValues["BPPassBandFreHigh"] + _originalValues["TransitionBandwidth"]) * (rpm / 60) > sampleFre / 2.56)
                {
                    throw new Exception(string.Format("带通高阻带频率必须小于等于采样频率的1/2.56({0})", sampleFre / 2.56));
                }
                return Algorithm.Instance.BandPassFilter(input, samplePoint, sampleFre,
                    _originalValues["PassbandAttenuationDB"],
                    _originalValues["StopbandAttenuationDB"],
                    (_originalValues["BPPassBandFreLow"] - _originalValues["TransitionBandwidth"]) * (rpm / 60),
                    _originalValues["BPPassBandFreLow"] * (rpm / 60),
                    _originalValues["BPPassBandFreHigh"] * (rpm / 60),
                    (_originalValues["BPPassBandFreHigh"] + _originalValues["TransitionBandwidth"]) * (rpm / 60));
            }
        }

        //public IObservable<string> WhenPropertyChanged
        //{
        //    get
        //    {
        //        return Observable
        //            .FromEventPattern<PropertyChangedEventHandler, PropertyChangedEventArgs>(
        //                h => PropertyChanged += h,
        //                h => PropertyChanged -= h)
        //            .Select(x => x.EventArgs.PropertyName);
        //    }
        //}
    }
}
