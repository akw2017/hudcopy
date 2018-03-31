using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace AIC.Core.Helpers
{
    public class ClassCopyHelper
    {
        public static TChild AutoCopy<TParent, TChild>(TParent parent) where TChild : TParent, new()
        {
            TChild child = new TChild();
            var ParentType = typeof(TParent);
            var Properties = ParentType.GetProperties();
            foreach (var Propertie in Properties)
            {
                //循环遍历属性
                if (Propertie.CanRead && Propertie.CanWrite)
                {
                    //进行属性拷贝
                    Propertie.SetValue(child, Propertie.GetValue(parent, null), null);
                }
            }
            return child;
        }

        public static T AutoCopy<T>(T source) where T : new()
        {
            T target = new T();
            var Properties = typeof(T).GetProperties();
            foreach (var Propertie in Properties)
            {
                //循环遍历属性
                if (Propertie.CanRead && Propertie.CanWrite)
                {
                    //进行属性拷贝
                    Propertie.SetValue(target, Propertie.GetValue(source, null), null);
                }
            }
            return target;
        }

        //public static T Copy<T>(T obj)
        //{
        //    //如果是字符串或值类型则直接返回
        //    if (obj is string || obj.GetType().IsValueType) return obj;

        //    object retval = Activator.CreateInstance(obj.GetType());
        //    FieldInfo[] fields = obj.GetType().GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);
        //    foreach (FieldInfo field in fields)
        //    {
        //        try { field.SetValue(retval, Copy(field.GetValue(obj))); }
        //        catch { }
        //    }
        //    return (T)retval;
        //}

        //public static T Copy<T>(T obj_left, T obj_right)
        //{
        //    //如果是字符串或值类型则直接返回
        //    if (obj_left is string || obj_left.GetType().IsValueType) return obj_left;

        //    //object retval = Activator.CreateInstance(obj_left.GetType());
        //    FieldInfo[] fields = obj_left.GetType().GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);
        //    foreach (FieldInfo field in fields)
        //    {                
        //        try { field.SetValue(obj_right, field.GetValue(obj_left)); }
        //        catch { }
        //    }
        //    return obj_right;
        //}

        public static T DeepCopy<T>(T obj)//需要类标记为可序列化
        {
            object retval;
            using (MemoryStream ms = new MemoryStream())
            {
                BinaryFormatter bf = new BinaryFormatter();
                //序列化成流
                bf.Serialize(ms, obj);
                ms.Seek(0, SeekOrigin.Begin);
                //反序列化成对象
                retval = bf.Deserialize(ms);
                ms.Close();
            }
            return (T)retval;
        }

        public static D Mapper<D, S>(S s)//相同属性复制
        {
            D d = Activator.CreateInstance<D>();
            try
            {
                var sType = s.GetType();
                var dType = typeof(D);
                foreach (PropertyInfo sP in sType.GetProperties())
                {
                    foreach (PropertyInfo dP in dType.GetProperties())
                    {
                        if (dP.Name == sP.Name)
                        {
                            dP.SetValue(d, sP.GetValue(s));
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return d;
        }
    }
}
