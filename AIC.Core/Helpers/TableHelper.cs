
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AIC.Core
{
    public class TableHelper<T> where T : new()
    {
        public static DataTable ToDataTable(IEnumerable<T> collection, bool asc)
        {
            var props = typeof(T).GetProperties();
            var dt = new DataTable();

            dt.Columns.AddRange(props.Where(p => (p.Name != "")).Select(p => new DataColumn(p.Name, p.PropertyType)).ToArray());
            if (collection.Count() > 0)
            {
                if (asc)//升序
                {
                    for (int i = 0; i < collection.Count(); i++)
                    {
                        ArrayList tempList = new ArrayList();
                        foreach (PropertyInfo pi in props)
                        {
                            if (dt.Columns.Contains(pi.Name))
                            {
                                // 判断此属性是否有Setter      
                                if (!pi.CanWrite) continue;
                                object obj = pi.GetValue(collection.ElementAt(i), null);
                                tempList.Add(obj);
                            }
                        }
                        object[] array = tempList.ToArray();
                        dt.LoadDataRow(array, true);
                    }
                }
                else//降序
                {
                    for (int i = collection.Count() - 1; i >= 0; i--)
                    {
                        ArrayList tempList = new ArrayList();
                        foreach (PropertyInfo pi in props)
                        {
                            if (dt.Columns.Contains(pi.Name))
                            {
                                // 判断此属性是否有Setter      
                                if (!pi.CanWrite) continue;
                                object obj = pi.GetValue(collection.ElementAt(i), null);
                                tempList.Add(obj);
                            }
                        }
                        object[] array = tempList.ToArray();
                        dt.LoadDataRow(array, true);
                    }
                }
            }
            return dt;
        }

        public static IList<T> ConvertToModel(DataTable dt)
        {
            // 定义集合    
            IList<T> ts = new List<T>();

            // 获得此模型的类型   
            Type type = typeof(T);
            string tempName = "";

            foreach (DataRow dr in dt.Rows)
            {
                T t = new T();
                // 获得此模型的公共属性      
                PropertyInfo[] propertys = t.GetType().GetProperties();
                foreach (PropertyInfo pi in propertys)
                {
                    tempName = pi.Name;  // 检查DataTable是否包含此列    

                    if (dt.Columns.Contains(tempName))
                    {
                        // 判断此属性是否有Setter      
                        if (!pi.CanWrite) continue;

                        

                        object value = dr[tempName];

                        if (value != DBNull.Value)
                        {
                            if (pi.PropertyType.FullName == "System.Int32" || pi.PropertyType.FullName.Contains("System.Nullable`1[[System.Int32"))
                            {
                                int intvalue = Convert.ToInt32(value);
                                pi.SetValue(t, intvalue, null);
                            }
                            else if (pi.PropertyType.FullName == "System.Single" || pi.PropertyType.FullName.Contains("System.Nullable`1[[System.Single"))
                            {
                                float floatvalue = Convert.ToSingle(value);
                                pi.SetValue(t, floatvalue, null);
                            }
                            else if (pi.PropertyType.FullName == "System.String")
                            {
                                string stringvalue = Convert.ToString(value);
                                pi.SetValue(t, stringvalue, null);
                            }
                            else
                            {
                                pi.SetValue(t, value, null);
                            }
                        }
                    }
                }
                ts.Add(t);
            }
            return ts;
        }
    }
}
