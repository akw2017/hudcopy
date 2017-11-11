using AIC.Server.Video.Contract;
using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.Prism.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AIC.DataModelProvider
{
    public class LMVedio : BindableBase
    {
        public LMVedio():this(null)
        {
        }

        public LMVedio(LMVideoTableContract contract)
        {
            if (contract == null)
            {
                Contract = new LMVideoTableContract();
                DVRIPAddress = "192.0.0.60";
                DVRPortNumber = 8000;
                DVRUserName = "admin";
                DVRPassword = "12345";
                DVRNeedRecord = true;
            }
            else
            {
                Contract = contract;
                ChannelGlobalID = contract.ChannelGlobalID;
                GroupCOName = contract.GroupCOName;
                CorporationName = contract.CorporationName;
                WorkShopName = contract.WorkShopName;
                DevName = contract.DevName;
                DevSN = contract.DevSN;
                Name = contract.Name;
                MSSN = contract.MSSN;
                DVRIPAddress = contract.DVRIPAddress;
                DVRPortNumber = contract.DVRPortNumber;
                DVRUserName = contract.DVRUserName;
                DVRPassword = contract.DVRPassword;
                DVRNeedRecord = contract.DVRNeedRecord;
            }
        }

        private string channelGlobalID;
        public string ChannelGlobalID
        {
            get { return channelGlobalID; }
            set
            {
                if (channelGlobalID != value)
                {
                    channelGlobalID = value;
                    Contract.ChannelGlobalID = value;
                }
            }
        }

        #region Property ID
        private int id;
        public int ID
        {
            get { return id; }
            set
            {
                if (value != id)
                {
                    id = value;
                    Contract.id = value;
                }
            }
        }
        #endregion

        #region Public Property

        #region Property GroupCOName
        private string groupCOName;
        public string GroupCOName
        {
            get { return groupCOName; }
            set
            {
                if (value != groupCOName)
                {
                    groupCOName = value;
                    this.OnPropertyChanged("GroupCOName");
                    Contract.GroupCOName = value;
                }
            }
        }
        #endregion

        #region Property CorporationName
        private string corporationName;
        public string CorporationName
        {
            get { return corporationName; }
            set
            {
                if (value != corporationName)
                {
                    corporationName = value;
                    this.OnPropertyChanged("CorporationName");
                    Contract.CorporationName = value;
                }
            }
        }
        #endregion

        #region Property WorkShopName
        private string workShopName;
        public string WorkShopName
        {
            get { return workShopName; }
            set
            {
                if (value != workShopName)
                {
                    workShopName = value;
                    this.OnPropertyChanged("WorkShopName");
                    Contract.WorkShopName = value;
                }
            }
        }
        #endregion

        #region Property DevName
        private string devName;
        public string DevName
        {
            get { return devName; }
            set
            {
                if (value != devName)
                {
                    devName = value;
                    this.OnPropertyChanged("DevName");
                    Contract.DevName = value;
                }
            }
        }
        #endregion

        #region Property DevSN
        private string devSN;
        public string DevSN
        {
            get { return devSN; }
            set
            {
                if (value != devSN)
                {
                    devSN = value;
                    this.OnPropertyChanged("DevSN");
                    Contract.DevSN = value;
                }
            }
        }
        #endregion

        #region Property Name
        private string name;
        public string Name
        {
            get { return name; }
            set
            {
                if (value != name)
                {
                    name = value;
                    this.OnPropertyChanged("Name");
                    Contract.Name = value;
                }
            }
        }
        #endregion Name

        #region Property MSSN
        private string mssn;
        public string MSSN
        {
            get { return mssn; }
            set
            {
                if (value != mssn)
                {
                    mssn = value;
                    this.OnPropertyChanged("MSSN");
                    Contract.MSSN = value;
                }
            }
        }
        #endregion MSSN

        //设备IP地址或者域名 Device IP
        #region Property DVRIPAddress
        private string dvrIPAddress;
        public string DVRIPAddress 
        {
            get { return dvrIPAddress; }
            set
            {
                if (dvrIPAddress != value)
                {
                    dvrIPAddress = value;
                    OnPropertyChanged("DVRIPAddress");
                    Contract.DVRIPAddress = value;
                }
            }
        }
        #endregion DVRIPAddress

        //设备服务端口号 Device Port
        #region Property DVRPortNumber
        private Int16 dvrPortNumber;
        public Int16 DVRPortNumber
        {
            get { return dvrPortNumber; }
            set
            {
                if (dvrPortNumber != value)
                {
                    dvrPortNumber = value;
                    OnPropertyChanged("DVRPortNumber");
                    Contract.DVRPortNumber = value;
                }
            }
        }
        #endregion DVRPortNumber

        //设备登录用户名 User name to login
        #region Property DVRUserName
        private string dvrUserName;
        public string DVRUserName
        {
            get { return dvrUserName; }
            set
            {
                if (dvrUserName != value)
                {
                    dvrUserName = value;
                    OnPropertyChanged("DVRUserName");
                    Contract.DVRUserName = value;
                }
            }
        }
        #endregion DVRUserName

        //设备登录密码 Password to login
        #region Property DVRPassword
        private string dvrPassword ;
        public string DVRPassword
        {
            get { return dvrPassword; }
            set
            {
                if (dvrPassword != value)
                {
                    dvrPassword = value;
                    OnPropertyChanged("DVRPassword");
                    Contract.DVRPassword = value;
                }
            }
        }
        #endregion DVRPassword

        #region Property DVRNeedRecord
        private bool dvrNeedRecord ;
        public bool DVRNeedRecord
        {
            get { return dvrNeedRecord; }
            set
            {
                if (value != dvrNeedRecord)
                {
                    dvrNeedRecord = value;
                    this.OnPropertyChanged("DVRNeedRecord");
                    Contract.DVRNeedRecord = value;
                }
            }
        }
        #endregion

        public LMVideoTableContract Contract { get;private set; }

        #endregion Public Property
    }
}
