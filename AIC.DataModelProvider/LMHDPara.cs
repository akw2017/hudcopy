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
    public class LMHDPara : BindableBase
    {
        public LMHDPara(LMHDParaTableContract contract)
        {
            Contract = contract;
            ip = contract.IP;
            sampleFre = contract.SampleFre / 2.56;
            samplePoint = contract.SamplePoint;
            sensorType = (SensorType)Enum.Parse(typeof(SensorType), contract.SensorType.ToString());
            avs = (AVS)Enum.Parse(typeof(AVS), contract.AVS.ToString());
            hp = (HP)Enum.Parse(typeof(HP), contract.HP.ToString());
            isEnp = contract.IsEnp;
            triggerN = (TriggerType)Enum.Parse(typeof(TriggerType), contract.TriggerN.ToString());
            lPCorner = contract.LPCorner;
            amp = (AMP)Enum.Parse(typeof(AMP), contract.AMP.ToString());
            isAntiAliase = contract.IsAntiAliase;
            winfun = (Winfun)Enum.Parse(typeof(Winfun), contract.Winfun.ToString());
            intevalTime = contract.IntevalTime;
            slotNum = contract.SlotNum + 1;
            rSlotNum = contract.RSlotNum + 1;
            rChaN = contract.RChaN + 1;
            hibernationTime = contract.HibernationTime;
            workTime = contract.WorkTime;
            wirelessMAC = contract.WirelessMAC;
            sampleType = (SampleType)Enum.Parse(typeof(SampleType), contract.SampleType.ToString());
            cycles = contract.Cycles;
            startCycles = contract.StartCycles;
            stopCycles = contract.StopCycles;
            startDeltaRPM = contract.StartDeltaRPM;
            stopDeltaRPM = contract.StopDeltaRPM;
            runningMode = (RunningMode)Enum.Parse(typeof(RunningState), contract.RunningMode.ToString());
            runningState = (RunningState)Enum.Parse(typeof(RunningState), contract.RunningState.ToString());
        }

        #region Property IP
        private string ip;
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

        #region Property SampleFre
        private double sampleFre;
        [DisplayName("分析频率")]
        public double SampleFre
        {
            get { return sampleFre; }
            set
            {
                if (value != sampleFre)
                {
                    sampleFre = value;
                    this.OnPropertyChanged("SampleFre");
                    Contract.SampleFre = value * 2.56;
                }
            }
        }
        #endregion

        #region Property SamplePoint
        private int samplePoint;
        [DisplayName("采样点数")]
        public int SamplePoint
        {
            get { return samplePoint; }
            set
            {
                if (value != samplePoint)
                {
                    samplePoint = value;
                    this.OnPropertyChanged("SamplePoint");
                    Contract.SamplePoint = value;
                }
            }
        }
        #endregion

        #region Property SensorType
        private SensorType sensorType;
        public SensorType SensorType
        {
            get { return sensorType; }
            set
            {
                if (value != sensorType)
                {
                    sensorType = value;
                    this.OnPropertyChanged("SensorType");
                    Contract.SensorType = (int)value;
                }
            }
        }
        #endregion

        #region Property AVS
        private AVS avs;
        [DisplayName("AVS")]
        public AVS AVS
        {
            get { return avs; }
            set
            {
                if (value != avs)
                {
                    avs = value;
                    this.OnPropertyChanged("AVS");
                    Contract.AVS = (int)value;
                }
            }
        }
        #endregion

        #region Property HP
        private HP hp;
        [DisplayName("高通")]
        public HP HP
        {
            get { return hp; }
            set
            {
                if (value != hp)
                {
                    hp = value;
                    this.OnPropertyChanged("HP");
                    Contract.HP = (int)value;
                }
            }
        }
        #endregion

        #region Property IsEnp
        private bool isEnp;
        [DisplayName("包络")]
        public bool IsEnp
        {
            get { return isEnp; }
            set
            {
                if (value != isEnp)
                {
                    isEnp = value;
                    this.OnPropertyChanged("IsEnp");
                    Contract.IsEnp = value;
                }
            }
        }
        #endregion

        #region Property TriggerN
        private TriggerType triggerN;
        public TriggerType TriggerN
        {
            get { return triggerN; }
            set
            {
                if (value != triggerN)
                {
                    triggerN = value;
                    this.OnPropertyChanged("TriggerN");
                    Contract.TriggerN = (int)value;
                }
            }
        }
        #endregion

        #region Property LPCorner
        private int lPCorner;
        public int LPCorner
        {
            get { return lPCorner; }
            set
            {
                if (value != lPCorner)
                {
                    lPCorner = value;
                    this.OnPropertyChanged("LPCorner");
                    Contract.LPCorner = value;
                }
            }
        }
        #endregion

        #region Property AMP
        private AMP amp;
        public AMP AMP
        {
            get { return amp; }
            set
            {
                if (value != amp)
                {
                    amp = value;
                    this.OnPropertyChanged("AMP");
                    Contract.AMP = (int)value;
                }
            }
        }
        #endregion

        #region Property IsAntiAliase
        private bool isAntiAliase;
        [DisplayName("抗混")]
        public bool IsAntiAliase
        {
            get { return isAntiAliase; }
            set
            {
                if (value != isAntiAliase)
                {
                    isAntiAliase = value;
                    this.OnPropertyChanged("IsAntiAliase");
                    Contract.IsAntiAliase = value;
                }
            }
        }
        #endregion

        #region Property Winfun
        private Winfun winfun;
        [DisplayName("加窗")]
        public Winfun Winfun
        {
            get { return winfun; }
            set
            {
                if (value != winfun)
                {
                    winfun = value;
                    this.OnPropertyChanged("Winfun");
                    Contract.Winfun = (int)value;
                }
            }
        }
        #endregion

        #region Property IntevalTime
        private int intevalTime;
        [DisplayName("发送间隔")]
        public int IntevalTime
        {
            get { return intevalTime; }
            set
            {
                if (value != intevalTime)
                {
                    intevalTime = value;
                    this.OnPropertyChanged("IntevalTime");
                    Contract.IntevalTime = value;
                }
            }
        }
        #endregion

        #region Property SlotNum
        private int slotNum;
        [DisplayName("插槽号")]
        public int SlotNum
        {
            get { return slotNum; }
            set
            {
                if (value != slotNum)
                {
                    slotNum = value;
                    this.OnPropertyChanged("SlotNum");
                    Contract.SlotNum = value - 1;
                }
            }
        }
        #endregion

        #region Property RSlotNum
        private int? rSlotNum;
        public int? RSlotNum
        {
            get { return rSlotNum; }
            set
            {
                if (value != rSlotNum)
                {
                    rSlotNum = value;
                    this.OnPropertyChanged("RSlotNum");
                    Contract.RSlotNum = value-1;
                }
            }
        }
        #endregion

        #region Property RChaN
        private int? rChaN;
        public int? RChaN
        {
            get { return rChaN; }
            set
            {
                if (value != rChaN)
                {
                    rChaN = value;
                    this.OnPropertyChanged("RChaN");
                    Contract.RChaN = value - 1;
                }
            }
        }
        #endregion

        #region Property AvailableRSlotNums
        private int[] availableRSlotNums;
        public int[] AvailableRSlotNums
        {
            get { return availableRSlotNums; }
            set
            {
                if (availableRSlotNums != value)
                {
                    availableRSlotNums = value;
                    OnPropertyChanged("AvailableRSlotNums");
                }
            }
        }
        #endregion

        #region Property AvailableRChaNs
        private int[] availableRChaNs;
        public int[] AvailableRChaNs
        {
            get { return availableRChaNs; }
            set
            {
                if (availableRChaNs != value)
                {
                    availableRChaNs = value;
                    OnPropertyChanged("AvailableRChaNs");
                }
            }
        }
        #endregion        

        #region Property SampleType
        private SampleType sampleType;
        public SampleType SampleType
        {
            get { return sampleType; }
            set
            {
                if (value != sampleType)
                {
                    sampleType = value;
                    this.OnPropertyChanged("SampleType");
                    Contract.SampleType = (int)SampleType;
                }
            }
        }
        #endregion

        #region Property HibernationTime
        private int? hibernationTime;
        public int? HibernationTime
        {
            get { return hibernationTime; }
            set
            {
                if (value != hibernationTime)
                {
                    hibernationTime = value;
                    this.OnPropertyChanged("HibernationTime");
                    Contract.HibernationTime = HibernationTime;
                }
            }
        }
        #endregion

        #region Property WorkTime
        private int? workTime;
        public int? WorkTime
        {
            get { return workTime; }
            set
            {
                if (value != workTime)
                {
                    workTime = value;
                    this.OnPropertyChanged("WorkTime");
                    Contract.WorkTime = WorkTime;
                }
            }
        }
        #endregion

        #region Property WirelessMAC
        private string wirelessMAC;
        public string WirelessMAC
        {
            get { return wirelessMAC; }
            set
            {
                if (value != wirelessMAC)
                {
                    wirelessMAC = value;
                    this.OnPropertyChanged("WirelessMAC");
                    Contract.WirelessMAC = WirelessMAC;
                }
            }
        }
        #endregion

        //平稳运行采样周期数
        #region Property Cycles
        private int cycles;
        public int Cycles
        {
            get { return cycles; }
            set
            {
                if (value != cycles)
                {
                    cycles = value;
                    this.OnPropertyChanged("Cycles");
                    Contract.Cycles = value;
                }
            }
        }
        #endregion

        //启机采样周期数
        #region Property StartCycles
        private int startCycles;
        public int StartCycles
        {
            get { return startCycles; }
            set
            {
                if (value != startCycles)
                {
                    startCycles = value;
                    this.OnPropertyChanged("StartCycles");
                    Contract.StartCycles = value;
                }
            }
        }
        #endregion

        //停机采样周期数
        #region Property StopCycles
        private int stopCycles;
        public int StopCycles
        {
            get { return stopCycles; }
            set
            {
                if (value != stopCycles)
                {
                    stopCycles = value;
                    this.OnPropertyChanged("StopCycles");
                    Contract.StopCycles = value;
                }
            }
        }
        #endregion

        //启机转速变化率
        #region Property StartDeltaRPM
        private double startDeltaRPM;
        public double StartDeltaRPM
        {
            get { return startDeltaRPM; }
            set
            {
                if (value != startDeltaRPM)
                {
                    startDeltaRPM = value;
                    this.OnPropertyChanged("StartDeltaRPM");
                    Contract.StartDeltaRPM = value;
                }
            }
        }
        #endregion

        //停机转速变化率
        #region Property StopDeltaRPM
        private double stopDeltaRPM;
        public double StopDeltaRPM
        {
            get { return stopDeltaRPM; }
            set
            {
                if (value != stopDeltaRPM)
                {
                    stopDeltaRPM = value;
                    this.OnPropertyChanged("StopDeltaRPM");
                    Contract.StopDeltaRPM = value;
                }
            }
        }
        #endregion

        //启停机判断模式
        #region Property RunningMode
        private RunningMode runningMode;
        public RunningMode RunningMode
        {
            get { return runningMode; }
            set
            {
                if (value != runningMode)
                {
                    runningMode = value;
                    this.OnPropertyChanged("RunningMode");
                    Contract.RunningMode = (int)value;
                }
            }
        }
        #endregion

        //运行状态
        #region Property RunningState
        private RunningState runningState;
        public RunningState RunningState
        {
            get { return runningState; }
            set
            {
                if (value != runningState)
                {
                    runningState = value;
                    this.OnPropertyChanged("RunningState");
                    Contract.RunningState = (int)value;
                }
            }
        }
        #endregion

        public LMHDParaTableContract Contract { get;private set; }

    }
}
