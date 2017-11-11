using AIC.CoreType;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIC.ModelWrapperGenerator
{
    public class DivFre : BindableBase
    {
        public DivFre()
        {

        }

        private string name;
        public string Name
        {
            get { return name; }
            set { SetProperty(ref name, value); }
        }

        private double freV;
        public double FreV
        {
            get { return freV; }
            set { SetProperty(ref freV, value); }
        }

        private double freMV;
        public double FreMV
        {
            get { return freMV; }
            set { SetProperty(ref freMV, value); }
        }

        private DivFreType divFreType;
        public DivFreType DivFreType
        {
            get { return divFreType; }
            set { SetProperty(ref divFreType, value); }
        }

        private double? base1Fre;
        public double? Base1Fre
        {
            get { return base1Fre; }
            set { SetProperty(ref base1Fre, value); }
        }

        private double? base1FrePercent;
        public double? Base1FrePercent
        {
            get { return base1FrePercent; }
            set { SetProperty(ref base1FrePercent, value); }
        }

        private double? base2Fre;
        public double? Base2Fre
        {
            get { return base2Fre; }
            set { SetProperty(ref base2Fre, value); }
        }

        private double? multiFre;
        public double? MultiFre
        {
            get { return multiFre; }
            set { SetProperty(ref multiFre, value); }
        }


        private int divFreStrategt;
        public int DivFreStrategt
        {
            get { return divFreStrategt; }
            set { SetProperty(ref divFreStrategt, value); }
        }

        private int? maxFreNum;
        public int? MaxFreNum
        {
            get { return maxFreNum; }
            set { SetProperty(ref maxFreNum, value); }
        }

        private double? rpm;
        public double? RPM
        {
            get { return rpm; }
            set { SetProperty(ref rpm, value); }
        }

        private AlarmGrade alarmGrade;
        public AlarmGrade AlarmGrade
        {
            get { return alarmGrade; }
            set { SetProperty(ref alarmGrade, value); }
        }

        private int alarmType;
        public int AlarmType
        {
            get { return alarmType; }
            set { SetProperty(ref alarmType, value); }
        }

        private string operatingModeUnit;
        public string OperatingModeUnit
        {
            get { return operatingModeUnit; }
            set { SetProperty(ref operatingModeUnit, value); }
        }

        private string operatingModePara;
        public string OperatingModePara
        {
            get { return operatingModePara; }
            set { SetProperty(ref operatingModePara, value); }
        }

        private double? comparativePercent;
        public double? ComparativePercent
        {
            get { return comparativePercent; }
            set { SetProperty(ref comparativePercent, value); }
        }

        private double? characteristicFre;
        public double? CharacteristicFre
        {
            get { return characteristicFre; }
            set { SetProperty(ref characteristicFre, value); }
        }

        private int unit;
        public int Unit
        {
            get { return unit; }
            set { SetProperty(ref unit, value); }
        }

        private double phase;
        public double Phase
        {
            get { return phase; }
            set { SetProperty(ref phase, value); }
        }

        private double highNormal;
        public double HighNormal
        {
            get { return highNormal; }
            set { SetProperty(ref highNormal, value); }
        }

        private double highAlert;
        public double HighAlert
        {
            get { return highAlert; }
            set { SetProperty(ref highAlert, value); }
        }

        private double highDanger;
        public double HighDanger
        {
            get { return highDanger; }
            set { SetProperty(ref highDanger, value); }
        }
        private double lowNormal;
        public double LowNormal
        {
            get { return lowNormal; }
            set { SetProperty(ref lowNormal, value); }
        }

        private double lowAlert;
        public double LowAlert
        {
            get { return lowAlert; }
            set { SetProperty(ref lowAlert, value); }
        }

        private double lowDanger;
        public double LowDanger
        {
            get { return lowDanger; }
            set { SetProperty(ref lowDanger, value); }
        }

        private bool allowLowLimit;
        public bool AllowLowLimit
        {
            get { return allowLowLimit; }
            set { SetProperty(ref allowLowLimit, value); }
        }

        private string formulaHighDanger;
        public string FormulaHighDanger
        {
            get { return formulaHighDanger; }
            set { SetProperty(ref formulaHighDanger, value); }
        }

        private string formulaHighAlert;
        public string FormulaHighAlert
        {
            get { return formulaHighAlert; }
            set { SetProperty(ref formulaHighAlert, value); }
        }

        private string formulaHighNormal;
        public string FormulaHighNormal
        {
            get { return formulaHighNormal; }
            set { SetProperty(ref formulaHighNormal, value); }
        }

        private string formulaLowNormal;
        public string FormulaLowNormal
        {
            get { return formulaLowNormal; }
            set { SetProperty(ref formulaLowNormal, value); }
        }

        private string formulaLowAlert;
        public string FormulaLowAlert
        {
            get { return formulaLowAlert; }
            set { SetProperty(ref formulaLowAlert, value); }
        }

        private string formulaLowDanger;
        public string FormulaLowDanger
        {
            get { return formulaLowDanger; }
            set { SetProperty(ref formulaLowDanger, value); }
        }

        private double? defaultR;
        public double? DefaultR
        {
            get { return defaultR; }
            set { SetProperty(ref defaultR, value); }
        }
    }
}
