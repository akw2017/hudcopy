﻿using System;
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
using System.Collections.ObjectModel;
using AIC.Server.Storage.Contract;

namespace AIC.Cloud.DataReplayer.ViewModels
{
    public class RPM3DSpectrumDataViewModel : HistoricalDataViewModel
    {
        public RPM3DSpectrumDataViewModel()
        {
            //ContractsCollection = new ObservableCollection<ChannelToken>();
            DisplayMode = SignalDisplayType.RPM3D;
        }

        private RPM3DSpectrumObject rpm3DSpectrumData;
        public RPM3DSpectrumObject RPM3DSpectrumData 
        {
            get { return rpm3DSpectrumData; }
            set
            {
                if (rpm3DSpectrumData != value)
                {
                    rpm3DSpectrumData = value;
                    OnPropertyChanged("RPM3DSpectrumData");
                }
            }
        }

        private int sizeX = 100;
        public int SizeX
        {
            get { return sizeX; }
            set
            {
                if (sizeX != value)
                {
                    sizeX = value;
                    OnPropertyChanged("SizeX");
                }
            }
        }

        private int sizeY = 100;
        public int SizeY
        {
            get { return sizeY; }
            set
            {
                if (sizeY != value)
                {
                    sizeY = value;
                    OnPropertyChanged("SizeY");
                }
            }
        }
        
    }
}
