//namespace AIC.ModelWrapperGenerator
//{
//    public class PDABase
//    {
//        public string IP { get; set; }
//        public bool Is4G { get; set; }
//        public bool IsSyn { get; set; }
//        public bool IsTotalBypass { get; set; }
//        public bool IsZipDownload { get; set; }
//        public bool IsZipUpload { get; set; }
//        public int Language { get; set; }
//        public string MasterWirelessMAC { get; set; }
//        public string PDAAliasName { get; set; }
//        public string PDAIP { get; set; }
//        public string PDAMAC { get; set; }
//        public float SampleFre { get; set; }
//        public int SamplePoint { get; set; }
//        public int SampleType { get; set; }
//        public string ServerIP { get; set; }
//        public int ServerPort { get; set; }
//        public int TriggerCardNum { get; set; }
//        public int TriggerChannelNum { get; set; }
//        public int TriggerType { get; set; }
//        public int UploadMode { get; set; }
//        public int Count { get; set; }
//    }

//    public abstract class CardBase
//    {
//        private static NullCard nullCard = new NullCard();

//        public string IP { get; set; }
//        public int CardNum { get; set; }
//        public string CardName { get; set; }
//        public int Count { get; set; }

//        public static NullCard Null { get { return nullCard; } }
//    }



//    public class NullCard : CardBase
//    {
//        public NullCard()
//        {

//        }
//    }

//    public class AnalogInCard : CardBase
//    {
//        public string InSignalCategory { get; set; }
//        public int InSignalCode { get; set; }
//        public int UploadIntevalTime { get; set; }
//    }

//    public class DigitTachometerCard : CardBase
//    {
//        public string InSignalCategory { get; set; }
//        public int InSignalCode { get; set; }
//        public int UploadIntevalTime { get; set; }
//    }

//    public class EddyCurrentDisplacementCard : CardBase
//    {
//        public float SampleFre { get; set; }
//        public int SamplePoint { get; set; }
//        public int SampleType { get; set; }
//        public string InSignalCategory { get; set; }
//        public int InSignalCode { get; set; }
//        public int HighPass { get; set; }
//        public int TriggerType { get; set; }
//        public int TriggerChannelNum { get; set; }
//        public int TriggerCardNum { get; set; }
//        public int UploadIntevalTime { get; set; }
//        public int Cycles { get; set; }
//        public bool Is24V { get; set; }
//    }

//    public class EddyCurrentTachometerCard : CardBase
//    {
//        public string InSignalCategory { get; set; }
//        public int InSignalCode { get; set; }
//        public int UploadIntevalTime { get; set; }
//        public bool Is24V { get; set; }
//    }

//    public class EddyCurrentKeyPhaseCard : CardBase
//    {
//        public string InSignalCategory { get; set; }
//        public int InSignalCode { get; set; }
//        public int UploadIntevalTime { get; set; }
//        public bool Is24V { get; set; }
//    }

//    public class IEPECard : CardBase
//    {
//        public string SlaveWirelessMAC { get; set; }
//        public float SampleFre { get; set; }
//        public int SamplePoint { get; set; }
//        public int SampleType { get; set; }
//        public string InSignalCategory { get; set; }
//        public int InSignalCode { get; set; }
//        public int Integration { get; set; }
//        public int HighPass { get; set; }
//        public int TriggerType { get; set; }
//        public int TriggerChannelNum { get; set; }
//        public int TriggerCardNum { get; set; }
//        public int UploadIntevalTime { get; set; }
//        public int HibernationTime { get; set; }
//        public int WorkTime { get; set; }
//        public int Cycles { get; set; }

//    }

//    public class RelayCard : CardBase
//    {
//        public bool IsEnableSetup { get; set; }
//    }

//    public class ChannelBase
//    {
//        public string IP { get; set; }
//        public int CardNum { get; set; }
//        public int ChannelNum { get; set; }
//        public string Unit { get; set; }
//        public bool IsBypass { get; set; }
//        public bool IsUpload { get; set; }
//    }

//    public class AnalogInChannel : ChannelBase
//    {
//        public float Calibration { get; set; }
//        public float X0 { get; set; }
//        public float Y0 { get; set; }
//        public float X1 { get; set; }
//        public float Y1 { get; set; }
//        public bool IsEnableFormula { get; set; }
//    }

//    public class DigitTachometerChannel : ChannelBase
//    {
//        public float Calibration { get; set; }
//        public bool IsNotch { get; set; }
//        public float DefaultRPM { get; set; }
//        public float TeethNumber { get; set; }
//        public bool IsTwoMAClamp { get; set; }
//    }

//    public class EddyCurrentDisplacementChannel : ChannelBase
//    {
//        public int TPDir { get; set; }
//        public string ResultType { get; set; }
//        public int ResultCode { get; set; }
//        public float BiasVoltHigh { get; set; }
//        public float BiasVoltLow { get; set; }
//        public float Sensitivity { get; set; }
//        public bool IsTwoMAClamp { get; set; }
//        public int RPMCardNum { get; set; }
//        public int RPMChannelNum { get; set; }
//        public int DelayAlarmTime { get; set; }

//    }

//    public class EddyCurrentTachometerChannel : ChannelBase
//    {
//        public float BiasVoltHigh { get; set; }
//        public float BiasVoltLow { get; set; }
//        public float ThresholdVolt { get; set; }
//        public float HysteresisVolt { get; set; }
//        public int ThresholdMode { get; set; }
//        public float Calibration { get; set; }
//        public bool IsNotch { get; set; }
//        public float DefaultRPM { get; set; }
//        public float TeethNumber { get; set; }
//        public bool IsTwoMAClamp { get; set; }
//        public int DelayAlarmTime { get; set; }
//        public int ChannelType { get; set; }
//    }

//    public class EddyCurrentKeyPhaseChannel : ChannelBase
//    {
//        public float BiasVoltHigh { get; set; }
//        public float BiasVoltLow { get; set; }
//        public float ThresholdVolt { get; set; }
//        public float HysteresisVolt { get; set; }
//        public int ThresholdMode { get; set; }
//        public float Calibration { get; set; }
//        public bool IsNotch { get; set; }
//        public float DefaultRPM { get; set; }
//        public float TeethNumber { get; set; }
//        public bool IsTwoMAClamp { get; set; }
//        public int DelayAlarmTime { get; set; }
//    }

//    public class IEPEChannel : ChannelBase
//    {
//        public int TPDir { get; set; }
//        public int SVType { get; set; }
//        public float BiasVoltHigh { get; set; }
//        public float BiasVoltLow { get; set; }
//        public float Sensitivity { get; set; }
//        public float VelocityCalibration { get; set; }
//        public float DisplacementCalibration { get; set; }
//        public bool IsTwoMAClamp { get; set; }
//        public int RPMCardNum { get; set; }
//        public int RPMChannelNum { get; set; }
//        public int DelayAlarmTime { get; set; }
//    }

//    public class RelayChannel : ChannelBase
//    {
//        public string Expression { get; set; }
//    }
//}
