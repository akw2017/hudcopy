using PetaPoco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AIC.M9600.Common.PetaPoco
{
    public class PetaPocoGlobal
    {
        public static IMapper DefaultMapper = new ConventionMapper();

        //public static string FormatSelectSQL<T>(string sql, DateTime time, string splitterExpression)
        //{
        //    var pd = PocoData.ForType(typeof(T), PetaPocoGlobal.DefaultMapper);
        //    string splitename = GetAddTimeFormatEndingInternal(pd, time, splitterExpression);
        //    return sql.ToLower().Contains("select") ? sql.Replace(pd.TableInfo.TableName, splitename) : string.Concat("select * from ", splitename, " ", sql);
        //}

        //public static string FormatSelectSQL<T>(string[] columns,string sql, DateTime time, string splitterExpression)
        //{
        //    var pd = PocoData.ForType(typeof(T), PetaPocoGlobal.DefaultMapper);
        //    string splitename = GetAddTimeFormatEndingInternal(pd, time, splitterExpression);
        //    return sql.ToLower().Contains("select") ? sql.Replace(pd.TableInfo.TableName, splitename) : string.Concat("select ", columns != null && columns.Length != 0 ? string.Join(",", columns) : "*", " from ", splitename, " ", sql);
        //}

        //public static string FormatDeleteSQL<T>(string sql, DateTime time, string splitterExpression)
        //{
        //    var pd = PocoData.ForType(typeof(T), PetaPocoGlobal.DefaultMapper);
        //    string splitename = GetAddTimeFormatEndingInternal(pd, time, splitterExpression);
        //    return sql.ToLower().Contains("delete") ? sql.Replace(pd.TableInfo.TableName, splitename) : string.Concat("delete from ", splitename, " ", sql);
        //}

        //public static string FormatUpdateSQL<T>(string sql, DateTime time, string splitterExpression)
        //{
        //    var pd = PocoData.ForType(typeof(T), PetaPocoGlobal.DefaultMapper);
        //    string splitename = GetAddTimeFormatEndingInternal(pd, time, splitterExpression);
        //    return sql.ToLower().Contains("update") ? sql.Replace(pd.TableInfo.TableName, splitename) : string.Concat("update ", splitename, " ", sql);
        //}

        public static string GetTableName(object poco)
        {
            return PocoData.ForType(poco.GetType(), PetaPocoGlobal.DefaultMapper).TableInfo.TableName;
        }

        public static string GetTableName(Type t)
        {
            return PocoData.ForType(t, PetaPocoGlobal.DefaultMapper).TableInfo.TableName;
        }

        public static string GetAddTimeFormatEnding(object poco, DateTime time, string splitterExpression)
        {
            return GetAddTimeFormatEndingInternal(PocoData.ForType(poco.GetType(), PetaPocoGlobal.DefaultMapper), time, splitterExpression);
        }

        public static string GetAddTimeFormatEnding<T>(DateTime time, string splitterExpression)
        {
            return GetAddTimeFormatEndingInternal(PocoData.ForType(typeof(T), PetaPocoGlobal.DefaultMapper), time, splitterExpression);
        }

        public static string GetAddTimeFormatEnding(Type tableType, DateTime time, string splitterExpression)
        {
            return GetAddTimeFormatEndingInternal(PocoData.ForType(tableType, PetaPocoGlobal.DefaultMapper), time, splitterExpression);
        }

        public static string GetAddTimeFormatEnding(string originalTableName, DateTime time, string splitterExpression)
        {
            return GetAddTimeFormatEndingInternal(originalTableName, time, splitterExpression);
        }

        private static string GetAddTimeFormatEndingInternal(PocoData data, DateTime time, string splitterExpression)
        {
            return GetAddTimeFormatEndingInternal(data.TableInfo.TableName, time, splitterExpression);
        }

        private static string GetAddTimeFormatEndingInternal(string originalTableName, DateTime time, string splitterExpression)
        {
            string splitter = null;

            if (splitterExpression == "yyyy" || splitterExpression == "yyyyMM" || splitterExpression == "yyyyMMdd" || splitterExpression == "yyyyMMddHH")
            {
                splitter = string.Concat("_E", time.ToString(splitterExpression));
            }
            else
            {
                if (splitterExpression.StartsWith("I"))
                {
                    string expression = splitterExpression.Substring(1);
                    int interval = 0;
                    if (int.TryParse(expression, out interval) && interval >= 1)
                    {
                        splitter = string.Concat("_I", ((int)(time - new DateTime(2000, 1, 1)).TotalDays) / interval);
                    }
                    else throw new FormatException();
                }
                else throw new FormatException();
            }

            return string.Concat(originalTableName, splitter);
        }

        public static List<DateTime> GetAddTimeFormatEndingList(DateTime startTime, DateTime endTime, string splitterExpression)
        {
            List<DateTime> result = new List<DateTime>();
            
            if (splitterExpression == "yyyy")
            {
                DateTime st = new DateTime(startTime.Year, 1, 1, 0, 0, 0);
                DateTime et = new DateTime(endTime.Year, 1, 1, 0, 0, 0);
                while (et >= st)
                {
                    result.Add(st);
                    st = st.AddYears(1);
                }
            }
            else if (splitterExpression == "yyyyMM")
            {
                DateTime st = new DateTime(startTime.Year, startTime.Month, 1, 0, 0, 0);
                DateTime et = new DateTime(endTime.Year, endTime.Month, 1, 0, 0, 0);
                while (et >= st)
                {
                    result.Add(st);
                    st = st.AddMonths(1);
                }
            }
            else if (splitterExpression == "yyyyMMdd")
            {
                DateTime st = new DateTime(startTime.Year, startTime.Month, startTime.Day, 0, 0, 0);
                DateTime et = new DateTime(endTime.Year, endTime.Month, endTime.Day, 0, 0, 0);
                while (et >= st)
                {
                    result.Add(st);
                    st = st.AddDays(1);
                }
            }
            else if (splitterExpression == "yyyyMMddHH")
            {
                DateTime st = new DateTime(startTime.Year, startTime.Month, startTime.Day, startTime.Hour, 0, 0);
                DateTime et = new DateTime(endTime.Year, endTime.Month, endTime.Day, endTime.Hour, 0, 0);
                while (et >= st)
                {
                    result.Add(st);
                    st = st.AddHours(1);
                }
            }
            else
            {
                if (splitterExpression.StartsWith("I"))
                {
                    string expression = splitterExpression.Substring(1);
                    int interval = 0;
                    if (int.TryParse(expression, out interval) && interval >= 1)
                    {
                        DateTime st = new DateTime(startTime.Year, startTime.Month, startTime.Day, 0, 0, 0);
                        DateTime et = new DateTime(endTime.Year, endTime.Month, endTime.Day, 0, 0, 0);
                        while (et >= st)
                        {
                            result.Add(st);
                            st = st.AddDays(1);
                        }
                    }
                    else throw new FormatException();
                }
                else throw new FormatException();
            }

            return result;
        }
    }
}
