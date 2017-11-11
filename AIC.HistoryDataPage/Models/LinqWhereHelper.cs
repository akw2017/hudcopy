using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AIC.HistoryDataPage.Models
{
    public class LinqWhereHelper
    {
        public List<object> Values { get; set; }
        private StringBuilder _whereBuilder = null;

        public LinqWhereHelper()
        {
            Values = new List<object>();
            _whereBuilder = new StringBuilder("where 1 = 1");
        }

        public LinqWhereHelper(string headerSql)
        {
            Values = new List<object>();
            _whereBuilder = new StringBuilder(headerSql);

            if (GetCharInStringCount("where", headerSql.ToLower()) == 0)
                _whereBuilder.Append(" where 1 = 1");
        }


        private void _AddCondition(string name, string condition, object value)
        {
            _whereBuilder.Append(" and ");

            if (condition == "in")
            {
                string[] orvalues = value.ToString().Split(new char[] { '(', ',', ')' }, StringSplitOptions.RemoveEmptyEntries);
                if (orvalues.Length != 0)
                {
                    _whereBuilder.Append(" (");
                    foreach (string orvalue in orvalues)
                    {
                        _whereBuilder.Append(name);
                        _whereBuilder.Append(" = ");
                        _whereBuilder.Append(" @");
                        _whereBuilder.Append(Values.Count.ToString());
                        _whereBuilder.Append(" or ");
                        Values.Add(orvalue);
                    }
                    _whereBuilder.Remove(_whereBuilder.Length - 4, 4);
                    _whereBuilder.Append(")");
                }
            }
            else if (condition == "not in")
            {
                string[] orvalues = value.ToString().Split(new char[] { '(', ',', ')' }, StringSplitOptions.RemoveEmptyEntries);
                if (orvalues.Length != 0)
                {
                    _whereBuilder.Append(" (");
                    foreach (string orvalue in orvalues)
                    {
                        _whereBuilder.Append(name);
                        _whereBuilder.Append(" != ");
                        _whereBuilder.Append(" @");
                        _whereBuilder.Append(Values.Count.ToString());
                        _whereBuilder.Append(" and ");
                        Values.Add(orvalue);
                    }
                    _whereBuilder.Remove(_whereBuilder.Length - 5, 5);
                    _whereBuilder.Append(")");
                }
            }
            else
            {
                _whereBuilder.Append(name);
                _whereBuilder.Append(" ");
                _whereBuilder.Append(condition);
                _whereBuilder.Append(" @");
                _whereBuilder.Append(Values.Count.ToString());
                Values.Add(value);
            }
        }


        public void AddCondition(string name, string condition, object value)
        {
            if (value != null)
            {
                Type t = value.GetType();
                if (t.IsArray)
                {
                    if ((value as ICollection).Count != 0)
                    {
                        Type basetype = GetArrayBaseType(null, t);
                        if (basetype != null && (basetype.IsValueType || basetype.Equals(typeof(String))))
                        {

                            _AddCondition(name, condition, (Array)value);
                        }
                    }
                }
                else
                {
                    if (t.Equals(typeof(String)))
                    {
                        if (!string.IsNullOrWhiteSpace(value as string))
                        {
                            if (condition == "like")
                            {
                                if (value.ToString().IndexOf("%") < 0)
                                    _AddCondition(name, condition, "%" + value + "%");
                                else
                                    _AddCondition(name, condition, value);

                            }
                            else
                            {
                                _AddCondition(name, condition, value);
                            }
                        }
                    }
                    else if (t.IsGenericType && t.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
                    {
                        if (value != null)
                        {
                            _AddCondition(name, condition, value);
                        }
                    }
                    else if (t.IsValueType)
                    {
                        _AddCondition(name, condition, value);
                    }
                }
            }
        }

        /// <summary>
        /// 添加排序字段
        /// </summary>
        /// <param name="orderStr">如：username desc,age desc</param>
        public void OrderBy(string orderStr)
        {
            if (!string.IsNullOrWhiteSpace(orderStr))
                _whereBuilder.AppendFormat(" order by {0}", orderStr);
        }

        public override string ToString()
        {
            return _whereBuilder.ToString();
        }

        private Type GetArrayBaseType(Assembly asm, Type arrayType)
        {
            Type result = null;
            string typename = arrayType.FullName.Replace("[", null).Replace("]", null);
            if (asm != null)
            {
                result = asm.GetType(typename);
            }
            if (result == null)
            {
                result = Type.GetType(typename);
            }
            return result;
        }

        /// <summary>
        /// 返回字符串在另一个字符串中出现的次数
        /// </summary>
        /// <param name="Char">要检测出现的字符</param>
        /// <param name="String">要检测的字符串</param>
        /// <returns>出现次数</returns>

        private int GetCharInStringCount(string Char, string String)
        {
            string str = String.Replace(Char, "");
            return (String.Length - str.Length) / Char.Length;

        }
    }
}
