/* Author : zhengyangyong */
using PetaPoco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIC.M9600.Common.PetaPoco
{
    public static class PetaPocoExtension
    {
        public static bool CheckTableExist(this Database orm, string targetTableName)
        {
            int? tableid = orm.ExecuteScalar<int?>("select object_id from sys.objects where name = @0 and type = 'U'", targetTableName);
            return tableid.HasValue;
        }

        public static bool CheckTableExist(this Database orm, string originalTableName, DateTime time, string splitterExpression)
        {
            string targetTableName = PetaPocoGlobal.GetAddTimeFormatEnding(originalTableName, time, splitterExpression);
            return CheckTableExist(orm, targetTableName);
        }

        public static IEnumerable<T> QueryAndConvert<S, T>(this Database orm, string sql, params object[] args)
        {
            var pd = PocoData.ForType(typeof(S), new ConventionMapper());
            return orm.Query<T>("select * from " + pd.TableInfo.TableName + " " + sql, args);
        }

        public static IEnumerable<T> QueryAndConvert<S, T>(this Database orm, string[] columns, string sql, params object[] args)
        {
            var pd = PocoData.ForType(typeof(S), new ConventionMapper());
            string columnstr = columns == null || columns.Length == 0 ? "*" : string.Join(",", columns);
            return orm.Query<T>(string.Concat("select ", columnstr, " from ", pd.TableInfo.TableName, " ", sql, args));
        }
    }
}
