using AIC.Server.ThirdParty.Contract;
using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.Prism.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AIC.DataModelProvider
{
    public class ThirdDataMapper : BindableBase
    {
        public ThirdDataMapper(ThirdPartyDataMapperContract contract)
        {
            Contract = contract;
            Tag = contract.Tag;
            Name = contract.Name;
            DataType = contract.DataType;
            ExpireSeconds = contract.ExpireSeconds;
            Description = contract.Description;
            CreateTime = contract.CreateTime;
        }

        #region Public Property

        #region Property Tag
        private string tag;
        public string Tag
        {
            get { return tag; }
            set
            {
                if (tag != value)
                {
                    tag = value;
                    OnPropertyChanged(() => this.Tag);
                    Contract.Tag = value;
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
                if (name != value)
                {
                    name = value;
                    OnPropertyChanged(() => this.Name);
                    Contract.Name = value;
                }
            }
        }
        #endregion

        #region Property DataType
        private string dataType = "System.String";
        public string DataType
        {
            get { return dataType; }
            set
            {
                if (dataType != value)
                {
                    dataType = value;
                    OnPropertyChanged(() => this.DataType);
                    Contract.DataType = value;
                }
            }
        }
        #endregion

        #region Property ExpireSeconds
        private int? expireSeconds = 30;
        public int? ExpireSeconds
        {
            get { return expireSeconds; }
            set
            {
                if (expireSeconds != value)
                {
                    expireSeconds = value;
                    OnPropertyChanged(() => this.ExpireSeconds);
                    Contract.ExpireSeconds = value;
                }
            }
        }
        #endregion

        #region Property Description
        private string description;
        public string Description
        {
            get { return description; }
            set
            {
                if (description != value)
                {
                    description = value;
                    OnPropertyChanged(() => this.Description);
                    Contract.Description = value;
                }
            }
        }
        #endregion

        #region Property CreateTime
        private DateTime? createTime;
        public DateTime? CreateTime
        {
            get { return createTime; }
            set
            {
                if (createTime != value)
                {
                    createTime = value;
                    OnPropertyChanged(() => this.CreateTime);
                    Contract.CreateTime = value;
                }
            }
        }
        #endregion

        public ThirdPartyDataMapperContract Contract { get; private set; }

        #endregion
    }
}
