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
using Arction.WPF.LightningChartUltimate;
using Microsoft.Practices.Prism.Mvvm;

namespace AIC.Cloud.DataReplayer.ViewModels
{
    public class HistoricalDataViewModel : BindableBase
    {
        public HistoricalDataViewModel()
        {
            DisplayMode = SignalDisplayType.AMSTrend;
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

        #region IsVisible
        private bool isVisible;
        public bool IsVisible
        {
            get { return isVisible; }
            set
            {
                if (isVisible != value)
                {
                    isVisible = value;
                    OnPropertyChanged("IsVisible");
                }
            }
        }
        #endregion

        #endregion

        public IObservable<string> WhenPropertyChanged
        {
            get
            {
                return Observable
                    .FromEventPattern<PropertyChangedEventHandler, PropertyChangedEventArgs>(
                        h => this.PropertyChanged += h,
                        h => this.PropertyChanged -= h)
                    .Select(x => x.EventArgs.PropertyName);
            }
        }
    }
}
