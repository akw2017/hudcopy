using AIC.Core.DiagnosticBaseModels;
using AIC.DiagnosePage.TestDatas;
using AIC.PDAPage.Models;
using AIC.ServiceInterface;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace AIC.DiagnosePage.ViewModels
{
    public class EditShaftComponentViewModel : BindableBase, INavigationAware
    {

        private readonly IDeviceDiagnoseTemplateService _deviceDiagnoseTemplateService;
        public EditShaftComponentViewModel(IDeviceDiagnoseTemplateService deviceDiagnoseTemplateService)
        {
            _deviceDiagnoseTemplateService = deviceDiagnoseTemplateService;

            if (Shafts == null)
            {
                Shafts = _deviceDiagnoseTemplateService.ShaftClassList;
            }
        }

        #region 属性与字段
        private ShaftComponent shaftComponent;

        public ShaftComponent ShaftComponent
        {
            get { return shaftComponent; }
            set
            {
                shaftComponent = value;
                OnPropertyChanged("ShaftComponent");
            }
        }    

        private ObservableCollection<ShaftClass> shafts;
        public ObservableCollection<ShaftClass> Shafts
        {
            get { return shafts; }
            set
            {
                shafts = value;
                OnPropertyChanged("Shafts");
            }
        }

        private ShaftClass selectedShaft;
        public ShaftClass SelectedShaft
        {
            get { return selectedShaft; }
            set
            {
                selectedShaft = value;
                OnPropertyChanged("SelectedShaft");
            }
        }
        #endregion

        #region 命令
        private ICommand addShaftCommand;
        public ICommand AddShaftCommand
        {
            get
            {
                return this.addShaftCommand ?? (this.addShaftCommand = new DelegateCommand(() => this.AddShaft()));
            }
        }

        private ICommand deleteShaftCommand;
        public ICommand DeleteShaftCommand
        {
            get
            {
                return this.deleteShaftCommand ?? (this.deleteShaftCommand = new DelegateCommand(() => this.DeleteShaft()));
            }
        }

        private ICommand removeNegationDivFreStrategyCommand;
        public ICommand RemoveNegationDivFreStrategyCommand
        {
            get
            {
                return this.removeNegationDivFreStrategyCommand ?? (this.removeNegationDivFreStrategyCommand = new DelegateCommand<object>(para => this.RemoveNegationDivFreStrategy(para)));
            }
        }

        private ICommand removeNaturalFreCommand;
        public ICommand RemoveNaturalFreCommand
        {
            get
            {
                return this.removeNaturalFreCommand ?? (this.removeNaturalFreCommand = new DelegateCommand<object>(para => this.RemoveNaturalFre(para)));
            }
        }

        private ICommand removeDivFreThresholdProportionCommand;
        public ICommand RemoveDivFreThresholdProportionCommand
        {
            get
            {
                return this.removeDivFreThresholdProportionCommand ?? (this.removeDivFreThresholdProportionCommand = new DelegateCommand<object>(para => this.RemoveDivFreThresholdProportion(para)));
            }
        }

        private ICommand selectedShaftChangedComamnd;
        public ICommand SelectedShaftChangedComamnd
        {
            get
            {
                return this.selectedShaftChangedComamnd ?? (this.selectedShaftChangedComamnd = new DelegateCommand<object>(para => this.SelectedShaftChanged(para)));
            }
        }
        #endregion

        #region 编辑
        private void AddShaft()
        {
            ShaftComponent = new ShaftComponent()
            {
                Component = new ShaftClass()
                {
                    MachComponents = new ObservableCollection<IMachComponent>()
                    {
                        new BearingComponent(),
                    }
                },
                ID = Guid.NewGuid(),
                Name = "新建轴",
            };
            devicemodel.Component.AddChild(ShaftComponent);
        }

        private void DeleteShaft()
        {
#if XBAP
            MessageBoxResult result = MessageBox.Show("确定要删除" + selectedshaft.Component.SelectedComponent.Name + "?", "删除", MessageBoxButton.OK, MessageBoxImage.Warning);
#else
            MessageBoxResult result = Xceed.Wpf.Toolkit.MessageBox.Show("确定要删除" + devicemodel.Component.SelectedShaft.Name + "?", "删除", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
#endif
            if (result == MessageBoxResult.OK)
            {
                devicemodel.Component.Shafts.Remove(devicemodel.Component.SelectedShaft);
            }
        }

        private void RemoveNegationDivFreStrategy(object para)
        {
            NegationDivFreStrategy fre = para as NegationDivFreStrategy;
            if (fre != null)
            {
                ShaftComponent.Component.NegationDivFreStrategies.Remove(fre);
            }
        }

        private void RemoveNaturalFre(object para)
        {
            NaturalFre fre = para as NaturalFre;
            if (fre != null)
            {
                ShaftComponent.Component.NaturalFres.Remove(fre);
            }
        }

        private void RemoveDivFreThresholdProportion(object para)
        {
            DivFreThresholdProportion fre = para as DivFreThresholdProportion;
            if (fre != null)
            {
                ShaftComponent.Component.DivFreThresholdProportiones.Remove(fre);
            }
        }

        private void SelectedShaftChanged(object para)
        {
            ShaftClass shaftclass = para as ShaftClass;
            if (shaftclass != null)
            {
                devicemodel.Component.SelectedShaft.Component = shaftclass.DeepClone();
            }
        }
        #endregion

        #region 导航
        private DeviceDiagnosisComponent devicemodel;
        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            var navigationService = navigationContext.NavigationService;
            devicemodel = navigationContext.Parameters["DeviceDiagnosisComponent"] as DeviceDiagnosisComponent;
            if (devicemodel != null && devicemodel.Component.SelectedShaft != null)
            {
                ShaftComponent = devicemodel.Component.SelectedShaft as ShaftComponent;
            }
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
          
        }
        #endregion
    }
}
