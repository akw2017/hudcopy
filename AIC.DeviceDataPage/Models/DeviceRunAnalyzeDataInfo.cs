using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIC.DeviceDataPage.Models
{
    class DeviceRunAnalyzeDataInfo : BindableBase
    {
        private string conclusionHeader;
        public string ConclusionHeader
        {
            get { return conclusionHeader; }
            set
            {
                if (conclusionHeader != value)
                {
                    conclusionHeader = value;
                    OnPropertyChanged("ConclusionHeader");
                }
            }
        }
        private string conclusionAlarm;
        public string ConclusionAlarm
        {
            get { return conclusionAlarm; }
            set
            {
                if (conclusionAlarm != value)
                {
                    conclusionAlarm = value;
                    OnPropertyChanged("ConclusionAlarm");
                }
            }
        }
        private string conclusionDanger;
        public string ConclusionDanger
        {
            get { return conclusionDanger; }
            set
            {
                if (conclusionDanger != value)
                {
                    conclusionDanger = value;
                    OnPropertyChanged("ConclusionDanger");
                }
            }
        }
        private string conclusionEnd;
        public string ConclusionEnd
        {
            get { return conclusionEnd; }
            set
            {
                if (conclusionEnd != value)
                {
                    conclusionEnd = value;
                    OnPropertyChanged("ConclusionEnd");
                }
            }
        }
        private string conclusionDevices;
        public string ConclusionDevices
        {
            get { return conclusionDevices; }
            set
            {
                if (conclusionDevices != value)
                {
                    conclusionDevices = value;
                    OnPropertyChanged("ConclusionDevices");
                }
            }
        }

        //public string RunTimePicPath { get; set; }
        //public string MaxValuePicPath { get; set; }

        public System.Windows.Media.Imaging.BitmapImage RunTimeImage { get; set; }
        public System.Windows.Media.Imaging.BitmapImage MaxValueImage { get; set; }

    }
}
