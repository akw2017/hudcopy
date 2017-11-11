using System;
using System.Linq;
using AIC.ModelWrapperGenerator;

namespace AIC.ModelWrapperGenerator
{
  public partial class DivFreWrapper : ModelWrapper<DivFre>
  {
    public DivFreWrapper(DivFre model) : base(model)
    {
    }

    public System.String Name
    {
      get { return GetValue<System.String>(); }
      set { SetValue(value); }
    }

    public System.String NameOriginalValue => GetOriginalValue<System.String>(nameof(Name));

    public bool NameIsChanged => GetIsChanged(nameof(Name));

    public System.Double FreV
    {
      get { return GetValue<System.Double>(); }
      set { SetValue(value); }
    }

    public System.Double FreVOriginalValue => GetOriginalValue<System.Double>(nameof(FreV));

    public bool FreVIsChanged => GetIsChanged(nameof(FreV));

    public System.Double FreMV
    {
      get { return GetValue<System.Double>(); }
      set { SetValue(value); }
    }

    public System.Double FreMVOriginalValue => GetOriginalValue<System.Double>(nameof(FreMV));

    public bool FreMVIsChanged => GetIsChanged(nameof(FreMV));

    public AIC.CoreType.DivFreType DivFreType
    {
      get { return GetValue<AIC.CoreType.DivFreType>(); }
      set { SetValue(value); }
    }

    public AIC.CoreType.DivFreType DivFreTypeOriginalValue => GetOriginalValue<AIC.CoreType.DivFreType>(nameof(DivFreType));

    public bool DivFreTypeIsChanged => GetIsChanged(nameof(DivFreType));

    public System.Nullable<System.Double> Base1Fre
    {
      get { return GetValue<System.Nullable<System.Double>>(); }
      set { SetValue(value); }
    }

    public System.Nullable<System.Double> Base1FreOriginalValue => GetOriginalValue<System.Nullable<System.Double>>(nameof(Base1Fre));

    public bool Base1FreIsChanged => GetIsChanged(nameof(Base1Fre));

    public System.Nullable<System.Double> Base1FrePercent
    {
      get { return GetValue<System.Nullable<System.Double>>(); }
      set { SetValue(value); }
    }

    public System.Nullable<System.Double> Base1FrePercentOriginalValue => GetOriginalValue<System.Nullable<System.Double>>(nameof(Base1FrePercent));

    public bool Base1FrePercentIsChanged => GetIsChanged(nameof(Base1FrePercent));

    public System.Nullable<System.Double> Base2Fre
    {
      get { return GetValue<System.Nullable<System.Double>>(); }
      set { SetValue(value); }
    }

    public System.Nullable<System.Double> Base2FreOriginalValue => GetOriginalValue<System.Nullable<System.Double>>(nameof(Base2Fre));

    public bool Base2FreIsChanged => GetIsChanged(nameof(Base2Fre));

    public System.Nullable<System.Double> MultiFre
    {
      get { return GetValue<System.Nullable<System.Double>>(); }
      set { SetValue(value); }
    }

    public System.Nullable<System.Double> MultiFreOriginalValue => GetOriginalValue<System.Nullable<System.Double>>(nameof(MultiFre));

    public bool MultiFreIsChanged => GetIsChanged(nameof(MultiFre));

    public System.Int32 DivFreStrategt
    {
      get { return GetValue<System.Int32>(); }
      set { SetValue(value); }
    }

    public System.Int32 DivFreStrategtOriginalValue => GetOriginalValue<System.Int32>(nameof(DivFreStrategt));

    public bool DivFreStrategtIsChanged => GetIsChanged(nameof(DivFreStrategt));

    public System.Nullable<System.Int32> MaxFreNum
    {
      get { return GetValue<System.Nullable<System.Int32>>(); }
      set { SetValue(value); }
    }

    public System.Nullable<System.Int32> MaxFreNumOriginalValue => GetOriginalValue<System.Nullable<System.Int32>>(nameof(MaxFreNum));

    public bool MaxFreNumIsChanged => GetIsChanged(nameof(MaxFreNum));

    public System.Nullable<System.Double> RPM
    {
      get { return GetValue<System.Nullable<System.Double>>(); }
      set { SetValue(value); }
    }

    public System.Nullable<System.Double> RPMOriginalValue => GetOriginalValue<System.Nullable<System.Double>>(nameof(RPM));

    public bool RPMIsChanged => GetIsChanged(nameof(RPM));

    public AIC.CoreType.AlarmGrade AlarmGrade
    {
      get { return GetValue<AIC.CoreType.AlarmGrade>(); }
      set { SetValue(value); }
    }

    public AIC.CoreType.AlarmGrade AlarmGradeOriginalValue => GetOriginalValue<AIC.CoreType.AlarmGrade>(nameof(AlarmGrade));

    public bool AlarmGradeIsChanged => GetIsChanged(nameof(AlarmGrade));

    public System.Int32 AlarmType
    {
      get { return GetValue<System.Int32>(); }
      set { SetValue(value); }
    }

    public System.Int32 AlarmTypeOriginalValue => GetOriginalValue<System.Int32>(nameof(AlarmType));

    public bool AlarmTypeIsChanged => GetIsChanged(nameof(AlarmType));

    public System.String OperatingModeUnit
    {
      get { return GetValue<System.String>(); }
      set { SetValue(value); }
    }

    public System.String OperatingModeUnitOriginalValue => GetOriginalValue<System.String>(nameof(OperatingModeUnit));

    public bool OperatingModeUnitIsChanged => GetIsChanged(nameof(OperatingModeUnit));

    public System.String OperatingModePara
    {
      get { return GetValue<System.String>(); }
      set { SetValue(value); }
    }

    public System.String OperatingModeParaOriginalValue => GetOriginalValue<System.String>(nameof(OperatingModePara));

    public bool OperatingModeParaIsChanged => GetIsChanged(nameof(OperatingModePara));

    public System.Nullable<System.Double> ComparativePercent
    {
      get { return GetValue<System.Nullable<System.Double>>(); }
      set { SetValue(value); }
    }

    public System.Nullable<System.Double> ComparativePercentOriginalValue => GetOriginalValue<System.Nullable<System.Double>>(nameof(ComparativePercent));

    public bool ComparativePercentIsChanged => GetIsChanged(nameof(ComparativePercent));

    public System.Nullable<System.Double> CharacteristicFre
    {
      get { return GetValue<System.Nullable<System.Double>>(); }
      set { SetValue(value); }
    }

    public System.Nullable<System.Double> CharacteristicFreOriginalValue => GetOriginalValue<System.Nullable<System.Double>>(nameof(CharacteristicFre));

    public bool CharacteristicFreIsChanged => GetIsChanged(nameof(CharacteristicFre));

    public System.Int32 Unit
    {
      get { return GetValue<System.Int32>(); }
      set { SetValue(value); }
    }

    public System.Int32 UnitOriginalValue => GetOriginalValue<System.Int32>(nameof(Unit));

    public bool UnitIsChanged => GetIsChanged(nameof(Unit));

    public System.Double Phase
    {
      get { return GetValue<System.Double>(); }
      set { SetValue(value); }
    }

    public System.Double PhaseOriginalValue => GetOriginalValue<System.Double>(nameof(Phase));

    public bool PhaseIsChanged => GetIsChanged(nameof(Phase));

    public System.Double HighNormal
    {
      get { return GetValue<System.Double>(); }
      set { SetValue(value); }
    }

    public System.Double HighNormalOriginalValue => GetOriginalValue<System.Double>(nameof(HighNormal));

    public bool HighNormalIsChanged => GetIsChanged(nameof(HighNormal));

    public System.Double HighAlert
    {
      get { return GetValue<System.Double>(); }
      set { SetValue(value); }
    }

    public System.Double HighAlertOriginalValue => GetOriginalValue<System.Double>(nameof(HighAlert));

    public bool HighAlertIsChanged => GetIsChanged(nameof(HighAlert));

    public System.Double HighDanger
    {
      get { return GetValue<System.Double>(); }
      set { SetValue(value); }
    }

    public System.Double HighDangerOriginalValue => GetOriginalValue<System.Double>(nameof(HighDanger));

    public bool HighDangerIsChanged => GetIsChanged(nameof(HighDanger));

    public System.Double LowNormal
    {
      get { return GetValue<System.Double>(); }
      set { SetValue(value); }
    }

    public System.Double LowNormalOriginalValue => GetOriginalValue<System.Double>(nameof(LowNormal));

    public bool LowNormalIsChanged => GetIsChanged(nameof(LowNormal));

    public System.Double LowAlert
    {
      get { return GetValue<System.Double>(); }
      set { SetValue(value); }
    }

    public System.Double LowAlertOriginalValue => GetOriginalValue<System.Double>(nameof(LowAlert));

    public bool LowAlertIsChanged => GetIsChanged(nameof(LowAlert));

    public System.Double LowDanger
    {
      get { return GetValue<System.Double>(); }
      set { SetValue(value); }
    }

    public System.Double LowDangerOriginalValue => GetOriginalValue<System.Double>(nameof(LowDanger));

    public bool LowDangerIsChanged => GetIsChanged(nameof(LowDanger));

    public System.Boolean AllowLowLimit
    {
      get { return GetValue<System.Boolean>(); }
      set { SetValue(value); }
    }

    public System.Boolean AllowLowLimitOriginalValue => GetOriginalValue<System.Boolean>(nameof(AllowLowLimit));

    public bool AllowLowLimitIsChanged => GetIsChanged(nameof(AllowLowLimit));

    public System.String FormulaHighDanger
    {
      get { return GetValue<System.String>(); }
      set { SetValue(value); }
    }

    public System.String FormulaHighDangerOriginalValue => GetOriginalValue<System.String>(nameof(FormulaHighDanger));

    public bool FormulaHighDangerIsChanged => GetIsChanged(nameof(FormulaHighDanger));

    public System.String FormulaHighAlert
    {
      get { return GetValue<System.String>(); }
      set { SetValue(value); }
    }

    public System.String FormulaHighAlertOriginalValue => GetOriginalValue<System.String>(nameof(FormulaHighAlert));

    public bool FormulaHighAlertIsChanged => GetIsChanged(nameof(FormulaHighAlert));

    public System.String FormulaHighNormal
    {
      get { return GetValue<System.String>(); }
      set { SetValue(value); }
    }

    public System.String FormulaHighNormalOriginalValue => GetOriginalValue<System.String>(nameof(FormulaHighNormal));

    public bool FormulaHighNormalIsChanged => GetIsChanged(nameof(FormulaHighNormal));

    public System.String FormulaLowNormal
    {
      get { return GetValue<System.String>(); }
      set { SetValue(value); }
    }

    public System.String FormulaLowNormalOriginalValue => GetOriginalValue<System.String>(nameof(FormulaLowNormal));

    public bool FormulaLowNormalIsChanged => GetIsChanged(nameof(FormulaLowNormal));

    public System.String FormulaLowAlert
    {
      get { return GetValue<System.String>(); }
      set { SetValue(value); }
    }

    public System.String FormulaLowAlertOriginalValue => GetOriginalValue<System.String>(nameof(FormulaLowAlert));

    public bool FormulaLowAlertIsChanged => GetIsChanged(nameof(FormulaLowAlert));

    public System.String FormulaLowDanger
    {
      get { return GetValue<System.String>(); }
      set { SetValue(value); }
    }

    public System.String FormulaLowDangerOriginalValue => GetOriginalValue<System.String>(nameof(FormulaLowDanger));

    public bool FormulaLowDangerIsChanged => GetIsChanged(nameof(FormulaLowDanger));

    public System.Nullable<System.Double> DefaultR
    {
      get { return GetValue<System.Nullable<System.Double>>(); }
      set { SetValue(value); }
    }

    public System.Nullable<System.Double> DefaultROriginalValue => GetOriginalValue<System.Nullable<System.Double>>(nameof(DefaultR));

    public bool DefaultRIsChanged => GetIsChanged(nameof(DefaultR));
  }
}
