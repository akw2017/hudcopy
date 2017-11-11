using AIC.Core;
using AIC.CoreType;
using AIC.Domain;
using AIC.ServiceInterface;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Interactivity.InteractionRequest;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace AIC.PDA.ViewModels
{
    public class EddyCurrentDisplacementChannelViewModel : BindableBase, INavigationAware,IRegionManagerAware, IConfirmNavigationRequest
    {

        private readonly IPDAService _pdaService;
        public EddyCurrentDisplacementChannelViewModel(IPDAService pdaService)
        {
            _pdaService = pdaService;
            SaveCommand = new DelegateCommand<object>(Save, CanSave);
            ResetCommand = new DelegateCommand<object>(Reset, CanReset);
            AddDivFreCommand = new DelegateCommand<object>(AddDivFre, CanAddDivFre);
            SelectDivFreCommand = new DelegateCommand<object>(SelectDivFre);
            ConfirmationRequest = new InteractionRequest<IConfirmation>();
        }

        public IRegionManager RegionManager { get; set; }

        private void SelectDivFre(object obj)
        {
            var divfre = obj as DivFreWrapper;
            if (divfre != null)
            {
                var parameter = new NavigationParameters();
                parameter.Add("DivFre", divfre);
                RegionManager.RequestNavigate("DivFreRegion", "DivFreView", parameter);
            }
        }

        private void AddDivFre(object obj)
        {
            string divFreName = "分频#1";
            int max = 0;
            if (Channel.DivFres.Count > 0)
            {
                string[] indexString = Channel.DivFres.Select(o => o.Name.Split('#').LastOrDefault()).ToArray();
                int[] indexs = new int[indexString.Length];
                for (int i = 0; i < indexs.Length; i++)
                {
                    int.TryParse(indexString[i], out indexs[i]);
                }
                max = indexs.Max();
                divFreName = string.Format("分频#{0}", max + 1);
            }

            Channel.DivFres.Add(new DivFreWrapper(new DivFre() { Name = divFreName }));
        }

        private bool CanAddDivFre(object arg)
        {
            return Channel != null;
        }

        private void Save(object obj)
        {
            if (Channel != null)
            {
                Channel.AcceptChanges();
                _pdaService.UpdateChannel(Channel.Model);
                InvalidateCommands();
            }
        }
        private bool CanSave(object arg)
        {
            if (Channel == null) return false;
            return Channel.IsChanged && Channel.IsValid;
        }

        private void Reset(object obj)
        {
            Channel.RejectChanges();
        }
        private bool CanReset(object arg)
        {
            if (Channel == null) return false;
            return Channel.IsChanged;
        }

        private void InvalidateCommands()
        {
            SaveCommand.RaiseCanExecuteChanged();
            ResetCommand.RaiseCanExecuteChanged();
        }

        private EddyCurrentDisplacementChannelWrapper channel;
        public EddyCurrentDisplacementChannelWrapper Channel
        {
            get { return channel; }
            set
            {
                if (channel != null)
                {
                    channel.PropertyChanged -= Channel_PropertyChanged;
                }
                SetProperty(ref channel, value);
                if (channel != null)
                {
                    channel.PropertyChanged += Channel_PropertyChanged;
                }
                AddDivFreCommand.RaiseCanExecuteChanged();
            }
        }

        private void Channel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Channel.IsChanged)
                || e.PropertyName == nameof(Channel.IsValid)
                || e.PropertyName == "")
            {
                InvalidateCommands();
            }
        }

        public DelegateCommand<object> SelectDivFreCommand { get; }
        public DelegateCommand<object> AddDivFreCommand { get; }
        public DelegateCommand<object> SaveCommand { get; }
        public DelegateCommand<object> ResetCommand { get; }

        public InteractionRequest<IConfirmation> ConfirmationRequest { get; private set; }

        #region IConfirmNavigationRequest
        public void ConfirmNavigationRequest(NavigationContext navigationContext, Action<bool> continuationCallback)
        {
            if (Channel == null) return;
            bool result = true;
            if (Channel.IsChanged)
            {
                ConfirmationRequest.Raise(new Confirmation() { Content = "有未提交的修改操作,是否要保存修改", Title = "修改未提交" }, confirm =>
                {
                    if (confirm.Confirmed)
                    {
                        result = true;
                        Save(null);
                    }
                    else
                    {
                        result = false;
                        Channel.RejectChanges();
                    }
                });
            }
            continuationCallback(result);
        }
        #endregion

        #region INavigationAware
        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            var model = navigationContext.Parameters["Channel"] as EddyCurrentDisplacementChannelModel;
            if (model != null)
            {
                if (Channel == null || Channel.Model != model)
                {
                    Channel = new EddyCurrentDisplacementChannelWrapper(model);
                }
            }
            else
            {
                Channel = null;
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
