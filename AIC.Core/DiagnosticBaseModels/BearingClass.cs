using System;

namespace AIC.Core.DiagnosticBaseModels
{
    public class BearingClass : IMach
    {
        public int ID { get; set; } = -1;//新增为-1
        public Guid BearingID { get; set; }
        //轴承型号
        public string Designation { get; set; }
        //节圆直径
        public double PitchDiameter { get; set; }
        //滚子直径
        public double RollerDiameter { get; set; }
        //接触角
        public double ContactAngle { get; set; }
        //滚子个数
        public int NumberOfRoller { get; set; }
        //外圈直径
        public double OuterRingDiameter { get; set; }
        //内圈直径
        public double InnerRingDiameter { get; set; }
        //列数
        public int NumberOfColumns { get; set; }
        //内环特征频率
        public double InnerRingFrequency { get; set; }
        //外环特征频率
        public double OuterRingFrequency { get; set; }
        //滚动体特征频率
        public double RollerFrequency { get; set; }
        //保持架特征频率
        public double MaintainsFrequency { get; set; }
        //轴承系列
        //[AllowNull]
        public string BearingSeries { get; set; }
        //转速
        public double RPM { get; set; }
        public string Name { get; set; }
    }
}
