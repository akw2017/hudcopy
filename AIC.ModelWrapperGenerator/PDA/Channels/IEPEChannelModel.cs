
using System.Collections.Generic;

namespace AIC.ModelWrapperGenerator
{
    public class IEPEChannelModel : BaseChannelModel
    {
        // private ObservableCollection<DivFre> divFreCollection = new ObservableCollection<DivFre>();
        //public IEPEChannelModel(IEPEChannel channel)
        //    : base(ChannelIdentity.Create(channel.IP, channel.CardNum, channel.ChannelNum).Value)
        //{

        //    Channel = channel;
        //    TPDir = Channel.TPDir == null ? TPDir.X : (TPDir)channel.TPDir.Value;
        //    SVType = Channel.SVType == null ? SVType.RMS : (SVType)channel.SVType.Value;
        //    BiasVoltHigh = Channel.BiasVoltHigh ?? 0;
        //    BiasVoltLow = Channel.BiasVoltLow ?? 0;
        //    Sensitivity = Channel.Sensitivity ?? 0;
        //    VelocityCalibration = Channel.VelocityCalibration ?? 0;
        //    DisplacementCalibration = Channel.DisplacementCalibration ?? 0;
        //    IsTwoMAClamp = Channel.IsTwoMAClamp ?? false;
        //    RPMCardNum = Channel.RPMCardNum ?? 0;
        //    RPMChannelNum = Channel.RPMChannelNum ?? 0;
        //    DelayAlarmTime = Channel.DelayAlarmTime ?? 0;
        //    Unit = channel.Unit;
        //    IsBypass = channel.IsBypass ?? false;
        //    IsUpload = channel.IsUpload ?? false;
        //    DivFres = !string.IsNullOrEmpty(channel.DivFres) ? JsonConvert.DeserializeObject<List<DivFre>>(channel.DivFres) : new List<DivFre>();
        //    //divFreCollection.AddRange(divFres);
        //}

        public List<DivFre> DivFres { get; }

        public IEPEChannel Channel { get; }

        private int tpDir;
        public int TPDir
        {
            get { return tpDir; }
            set
            {
                if (SetProperty(ref tpDir, value))
                    Channel.TPDir = (int)value;
            }
        }

        private int svType;
        public int SVType
        {
            get { return svType; }
            set
            {
                if (SetProperty(ref svType, value))
                    Channel.SVType = (int)value;
            }
        }

        private double biasVoltHigh;
        public double BiasVoltHigh
        {
            get { return biasVoltHigh; }
            set
            {
                if (SetProperty(ref biasVoltHigh, value))
                    Channel.BiasVoltHigh = value;
            }
        }

        private double biasVoltLow;
        public double BiasVoltLow
        {
            get { return biasVoltLow; }
            set
            {
                if (SetProperty(ref biasVoltLow, value))
                    Channel.BiasVoltLow = value;
            }
        }

        private double sensitivity;
        public double Sensitivity
        {
            get { return sensitivity; }
            set
            {
                if (SetProperty(ref sensitivity, value))
                    Channel.Sensitivity = value;
            }
        }

        private double velocityCalibration;
        public double VelocityCalibration
        {
            get { return velocityCalibration; }
            set
            {
                if (SetProperty(ref velocityCalibration, value))
                    Channel.VelocityCalibration = value;
            }
        }

        private double displacementCalibration;
        public double DisplacementCalibration
        {
            get { return displacementCalibration; }
            set
            {
                if (SetProperty(ref displacementCalibration, value))
                    Channel.DisplacementCalibration = value;
            }
        }

        private bool isTwoMAClamp;
        public bool IsTwoMAClamp
        {
            get { return isTwoMAClamp; }
            set
            {
                if (SetProperty(ref isTwoMAClamp, value))
                    Channel.IsTwoMAClamp = value;
            }
        }

        private int rpmCardNum;
        public int RPMCardNum
        {
            get { return rpmCardNum; }
            set
            {
                if (SetProperty(ref rpmCardNum, value))
                    Channel.RPMCardNum = value;
            }
        }

        private int rpmChannelNum;
        public int RPMChannelNum
        {
            get { return rpmChannelNum; }
            set
            {
                if (SetProperty(ref rpmChannelNum, value))
                    Channel.RPMChannelNum = value;
            }
        }
        private int delayAlarmTime;
        public int DelayAlarmTime
        {
            get { return delayAlarmTime; }
            set
            {
                if (SetProperty(ref delayAlarmTime, value))
                    Channel.DelayAlarmTime = value;
            }
        }

    }
}
