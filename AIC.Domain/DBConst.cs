namespace AIC.Domain
{
    public class DBConst
    {
        //public DBConst(double alarmDelay)
        //{
        //    AlarmDelay = alarmDelay;
        //}

        //public DBConst()
        //{
        //}

        public static double AlarmDelay { get; private set; }

        public static void SetAlarmDelay(double alarmDelay)
        {
            AlarmDelay = alarmDelay;
        }
    }
}
