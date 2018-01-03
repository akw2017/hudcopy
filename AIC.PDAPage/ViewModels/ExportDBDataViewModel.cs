using Prism.Mvvm;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using AIC.ServiceInterface;
using AIC.Core.LMModels;
using AIC.Resources.Models;

namespace AIC.PDAPage.ViewModels
{
    class ExportDBDataViewModel : BindableBase
    {           
        private readonly IOrganizationService _organizationService;
        private readonly ICardProcess _cardProcess;
        private readonly IDatabaseComponent _databaseComponent;

        public ExportDBDataViewModel(IOrganizationService organizationService, ICardProcess cardProcess, IDatabaseComponent databaseComponent)
        {      
            _organizationService = organizationService;
            _cardProcess = cardProcess;
            _databaseComponent = databaseComponent;

            ServerIPCategory = new ObservableCollection<string>(_databaseComponent.T_RootCard.Keys.ToList());
            ServerIP = _databaseComponent.MainServerIp;
            OrganizationView = _databaseComponent.GetOrganizationData(ServerIP);
        }
        #region 属性与字段
        private string _serverIP;
        public string ServerIP
        {
            get { return _serverIP; }
            set
            {
                if (_serverIP != value)
                {
                    _serverIP = value;
                    OnPropertyChanged("ServerIP");
                }
            }
        }

        private ObservableCollection<string> _serverIPCategory;
        public ObservableCollection<string> ServerIPCategory
        {
            get { return _serverIPCategory; }
            set
            {
                _serverIPCategory = value;
                OnPropertyChanged("ServerIPCategory");
            }
        }

        private List<T1_Organization> organizationView;
        public List<T1_Organization> OrganizationView
        {
            get { return organizationView; }
            set
            {
                organizationView = value;
                OnPropertyChanged("OrganizationView");
            }
        }

        private ViewModelStatus _status = ViewModelStatus.None;
        public ViewModelStatus Status
        {
            get { return _status; }
            set
            {
                if (_status != value)
                {
                    _status = value;
                    OnPropertyChanged("Status");
                }
            }
        }

        public string waitInfo;
        public string WaitInfo
        {
            get { return waitInfo; }
            set
            {
                waitInfo = value;
                OnPropertyChanged("WaitInfo");
            }
        }
        #endregion

        #region 命令
        #endregion
    }
}
