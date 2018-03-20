using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIC.HistoryDataPage.Models
{
    public class ConditionClass
    {
        public static void GetConditionStr(out string conditionWave, out string conditionAlarm, out object[] objectWave, out object[] objectAlarm, bool AllowNormal, bool AllowPreWarning, bool AllowWarning, bool AllowDanger, bool AllowInvalid, bool AllowRPMFilter, string  unit, double downRPMFilter, double upRPMFilter)
        {
            string condition;
            string alarmConditionStr = string.Empty;
            if (AllowNormal)
            {
                if (alarmConditionStr == string.Empty)
                {
                    alarmConditionStr = "8193";
                    alarmConditionStr += ",513";
                }
                else
                {
                    alarmConditionStr += ",8193";
                    alarmConditionStr += ",513";
                }
            }
            if (AllowPreWarning)
            {
                if (alarmConditionStr == string.Empty)
                {
                    alarmConditionStr = "16386";
                    alarmConditionStr += ",1026";
                }
                else
                {
                    alarmConditionStr += ",16386";
                    alarmConditionStr += ",1026";
                }
            }
            if (AllowWarning)
            {
                if (alarmConditionStr == string.Empty)
                {
                    alarmConditionStr = "32771";
                    alarmConditionStr += ",2051";
                }
                else
                {
                    alarmConditionStr += ",32771";
                    alarmConditionStr += ",2051";
                }
            }
            if (AllowDanger)
            {
                if (alarmConditionStr == string.Empty)
                {
                    alarmConditionStr = "65540";
                    alarmConditionStr += ",4100";
                }
                else
                {
                    alarmConditionStr += ",65540";
                    alarmConditionStr += ",4100";
                }
            }
            if (AllowInvalid)
            {
                if (alarmConditionStr == string.Empty)
                {
                    alarmConditionStr = "256";
                }
                else
                {
                    alarmConditionStr += ",256";
                }
            }
            if (!string.IsNullOrEmpty(alarmConditionStr))
            {
                alarmConditionStr = "AlarmGrade in (" + alarmConditionStr + ") ";
            }

            if (!string.IsNullOrEmpty(alarmConditionStr))
            {
                condition = "(" + alarmConditionStr + " and Unit = @0)";
            }
            else
            {
                condition = "(Unit = @0)";
            }
            conditionAlarm = condition;

            if (AllowRPMFilter == true)
            {
                if (!string.IsNullOrEmpty(alarmConditionStr))
                {
                    condition = "(" + alarmConditionStr + " and Unit = @0 and RPM > @1 and RPM <= @2)";
                }
                else
                {
                    condition = "(Unit = @0 and RPM > @1 && and <= @2)";
                }
            }
            conditionWave = condition;

            unit = (unit == "Unit") ? "" : unit;
            objectWave = new object[] { unit, downRPMFilter, upRPMFilter };
            objectAlarm = new object[] { unit };

        }
    }
}
