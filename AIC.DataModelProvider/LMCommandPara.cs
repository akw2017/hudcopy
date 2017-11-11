using AIC.Cloud.Applications;
using AIC.CoreType;
using AIC.Server.Storage.Contract;
using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.Prism.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace AIC.DataModelProvider
{
    public class LMCommandPara : BindableBase
    {
        public LMCommandPara(LMCommandParaTableContract contract)
        {
            Contract = contract;
            ID = contract.id;
            IP = contract.IP;
            InstrumentDortN = contract.InstrumentDortN;
            InstrumentAliasName = contract.InstrumentAliasName;
            ServerIPAddr = contract.ServerIPAddr;
            ServerDortN = contract.ServerDortN;
            NType = (nType)Enum.Parse(typeof(nType), contract.nType.ToString());
            NCommand = contract.nCommand;
        }

        #region Property ID
        private int id;
        public int ID
        {
            get { return id; }
            private set
            {
                if (value != id)
                {
                    id = value;
                    Contract.id = value;
                }
            }
        }
        #endregion

        #region Property IP
        private string ip ;
        public string IP
        {
            get { return ip; }
            set
            {
                if (value != ip)
                {
                    ip = value;
                    this.OnPropertyChanged("IP");
                    Contract.IP = value;
                }
            }
        }
        #endregion

        #region Property InstrumentDortN
        private int instrumentDortN;
        [DisplayName("仪器端口")]
        public int InstrumentDortN
        {
            get { return instrumentDortN; }
            set
            {
                if (value != instrumentDortN)
                {
                    instrumentDortN = value;
                    this.OnPropertyChanged("InstrumentDortN");
                    Contract.InstrumentDortN = value;
                }
            }
        }
        #endregion

        #region Property InstrumentAliasName
        private string instrumentAliasName = "";
        public string InstrumentAliasName
        {
            get { return instrumentAliasName; }
            set
            {
                if (value != instrumentAliasName)
                {
                    instrumentAliasName = value;
                    this.OnPropertyChanged("InstrumentAliasName");
                    Contract.InstrumentAliasName = value;
                }
            }
        }
        #endregion

        #region Property ServerIPAddr
        private string serverIPAddr;
        [DisplayName("服务器IP")]
        public string ServerIPAddr
        {
            get { return serverIPAddr; }
            set
            {
                if (value != serverIPAddr)
                {
                    serverIPAddr = value;
                    this.OnPropertyChanged("ServerIPAddr");
                    Contract.ServerIPAddr = value;
                }
            }
        }
        #endregion

        #region Property ServerDortN
        private int serverDortN;
        [DisplayName("服务器端口")]
        public int ServerDortN
        {
            get { return serverDortN; }
            set
            {
                if (value != serverDortN)
                {
                    serverDortN = value;
                    this.OnPropertyChanged("ServerDortN");
                    Contract.ServerDortN = value;
                }
            }
        }
        #endregion

        #region Property NType
        private nType nType;
        [DisplayName("数据源")]
        public nType NType
        {
            get { return nType; }
            set
            {
                if (value != nType)
                {
                    nType = value;
                    this.OnPropertyChanged("NType");
                    Contract.nType = (int)value;
                }
            }
        }
        #endregion

        #region Property NCommand
        private string nCommand = "";
        public string NCommand
        {
            get { return nCommand; }
            set
            {
                if (value != nCommand)
                {
                    nCommand = value;
                    this.OnPropertyChanged("NCommand");
                    Contract.nCommand = value;
                }
            }
        }
        #endregion

        public LMCommandParaTableContract Contract { get;private set; }
    }
}
