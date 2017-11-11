using AIC.MatlabMath;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIC.Core.Filters
{
    public class HighPassFilter : NotifyDataErrorInfoBase
    {
        private Dictionary<string, double> _originalValues;

        private double passbandAttenuationDB = 0.2;
        private double stopbandAttenuationDB = 60;
        private double passbandFre;
        private double stopbandFre;
        private bool isValid;
        private bool isChanged;
        private readonly HighPassFilterValidator validator;
        private bool isRPMBinding;

        public HighPassFilter(double sampleFre = 1024)
        {
            _originalValues = new Dictionary<string, double>();
            validator = new HighPassFilterValidator();
            BindRPMCommand = new DelegateCommand<object>(BindRPM);
            UnBindRPMCommand = new DelegateCommand<object>(UnBindRPM);
            SaveCommand = new DelegateCommand<object>(Save, CanSave);
            ResetCommand = new DelegateCommand<object>(Reset, CanReset);
            PropertyChanged += BandPassFilter_PropertyChanged;
            Initialize(sampleFre);
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
            StopbandFre = (sampleFre / 2.56) / 3;
            PassbandFre = (sampleFre / 2.56) * 2 / 3;
            Save(null);
        }

        private void BindRPM(object args)
        {
            if (IsRPMBinding)
            {
                if (args is double && (double)args != 0)
                {
                    double rpm = (double)args;
                    // isRPMBinding = true;
                    StopbandFre = StopbandFre / (rpm / 60);
                    PassbandFre = PassbandFre / (rpm / 60);
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
                    //isRPMBinding = false;
                    StopbandFre = StopbandFre * (rpm / 60);
                    PassbandFre = PassbandFre * (rpm / 60);
                    Save(null);
                }
            }
        }

        private void Save(object args)
        {
            _originalValues["PassbandAttenuationDB"] = PassbandAttenuationDB;
            _originalValues["StopbandAttenuationDB"] = StopbandAttenuationDB;
            _originalValues["StopbandFre"] = StopbandFre;
            _originalValues["PassbandFre"] = PassbandFre;
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
            StopbandFre = _originalValues["StopbandFre"];
            PassbandFre = _originalValues["PassbandFre"];
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

        //通带频率
        public double PassbandFre
        {
            get { return passbandFre; }
            set
            {
                if (SetProperty(ref passbandFre, value))
                {
                    Validate();
                    IsChanged = true;
                }
            }
        }
        //阻带频率
        public double StopbandFre
        {
            get { return stopbandFre; }
            set
            {
                if (SetProperty(ref stopbandFre, value))
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
                if (_originalValues["PassbandFre"] > sampleFre / 2.56)
                {
                    throw new Exception(string.Format("通带频率必须小于等于采样频率的1/2.56({0})", sampleFre / 2.56));
                }
                return Algorithm.Instance.HighPassFilter(input, samplePoint, sampleFre,
                    _originalValues["PassbandFre"],
                    _originalValues["StopbandFre"],
                    _originalValues["PassbandAttenuationDB"],
                    _originalValues["StopbandAttenuationDB"]);
            }
            else
            {
                if (_originalValues["PassbandFre"] * (rpm / 60) > sampleFre / 2.56)
                {
                    throw new Exception(string.Format("通带频率必须小于等于采样频率的1/2.56({0})", sampleFre / 2.56));
                }
                return Algorithm.Instance.HighPassFilter(input, samplePoint, sampleFre,
                    _originalValues["PassbandFre"] * (rpm / 60),
                    _originalValues["StopbandFre"] * (rpm / 60),
                    _originalValues["PassbandAttenuationDB"],
                    _originalValues["StopbandAttenuationDB"]);
            }
        }
    }
}
