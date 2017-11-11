using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Text;

using Microsoft.Practices.Prism.ViewModel;

using AIC.CoreType;
using AIC.Cloud.Domain;
using Microsoft.Practices.Prism.Mvvm;

namespace AIC.Cloud.DataReplayer.ViewModels
{
    public class SignalMonitorValue : BindableBase
    {
        public SignalMonitorValue()
        {

        }
        #region Public Property

        #region DisplayMode
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

        #endregion
    }
}
