using AIC.Core.LMModels;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace AIC.Core.Models
{
    //[Serializable]
    public partial class ServerInfo : BindableBase//, ICloneable
    {
        private int id;
        public int ID
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
                OnPropertyChanged("ID");
            }
        }

        private string name;
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
                OnPropertyChanged("Name");
            }
        }

        private string ip;
        public string IP
        {
            get
            {
                return ip;
            }
            set
            {
                ip = value;
                OnPropertyChanged("IP");
            }
        }

        private int port;
        public int Port
        {
            get
            {
                return port;
            }
            set
            {
                port = value;
                OnPropertyChanged("Port");
            }
        }

        private bool isCloud;
        public bool IsCloud
        {
            get
            {
                return isCloud;
            }
            set
            {
                isCloud = value;
                OnPropertyChanged("IsCloud");
            }
        }

        private string factory;
        public string Factory
        {
            get
            {
                return factory;
            }
            set
            {
                factory = value;
                OnPropertyChanged("Factory");
            }
        }

        private double longitude;
        public double Longitude
        {
            get
            {
                return longitude;
            }
            set
            {
                longitude = value;
                OnPropertyChanged("Longitude");
            }
        }

        private double latitude;
        public double Latitude
        {
            get
            {
                return latitude;
            }
            set
            {
                latitude = value;
                OnPropertyChanged("Latitude");
            }
        }

        private bool isLogin;
        public bool IsLogin
        {
            get
            {
                return isLogin;
            }
            set
            {
                isLogin = value;
                OnPropertyChanged("IsLogin");
            }
        }

        private bool loginResult;
        public bool LoginResult
        {
            get
            {
                return loginResult;
            }
            set
            {
                loginResult = value;
                OnPropertyChanged("LoginResult");
            }
        }

        private string permission;
        public string Permission
        {
            get
            {
                return permission;
            }
            set
            {
                permission = value;
                OnPropertyChanged("Permission");
            }
        }

        public T1_User T_User { get; set; }
        public string UserName { get; set; }
        public string UserPwd { get; set; }
        public bool IsSaveUserName { get; set; }
        public bool IsSaveUserPwd { get; set; }

        public ServerInfo()
        {
            IP = "127.0.0.1";
            Longitude = 116.404;
            Latitude = 39.915;            
        }

        //深拷贝
        //public ServerInfo DeepClone()
        //{
        //    using (Stream objectStream = new MemoryStream())
        //    {
        //        IFormatter formatter = new BinaryFormatter();
        //        formatter.Serialize(objectStream, this);
        //        objectStream.Seek(0, SeekOrigin.Begin);
        //        return formatter.Deserialize(objectStream) as ServerInfo;
        //    }
        //}

        public ServerInfo DeepClone()
        {
            return CopyOjbect(this) as ServerInfo;
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        //浅拷贝
        public ServerInfo ShallowClone()
        {
            return this.Clone() as ServerInfo;
        }

        #region 对象拷贝,反射法
        private static object CopyOjbect(object obj)
        {
            if (obj == null)
            {
                return null;
            }

            //拷贝目标
            Object targetDeepCopyObj;

            //元类型
            Type targetType = obj.GetType();

            //值类型  
            if (targetType.IsValueType == true)
            {
                targetDeepCopyObj = obj;
            }
            //引用类型   
            else
            {
                //创建引用对象
                targetDeepCopyObj = System.Activator.CreateInstance(targetType);

                //获取引用对象的所有公共成员
                System.Reflection.MemberInfo[] memberCollection = obj.GetType().GetMembers();

                foreach (System.Reflection.MemberInfo member in memberCollection)
                {
                    //拷贝字段
                    if (member.MemberType == System.Reflection.MemberTypes.Field)
                    {
                        System.Reflection.FieldInfo field = (System.Reflection.FieldInfo)member;
                        Object fieldValue = field.GetValue(obj);
                        if (fieldValue is ICloneable)
                        {
                            field.SetValue(targetDeepCopyObj, (fieldValue as ICloneable).Clone());
                        }
                        else
                        {
                            field.SetValue(targetDeepCopyObj, CopyOjbect(fieldValue));
                        }

                    }//拷贝属性
                    else if (member.MemberType == System.Reflection.MemberTypes.Property)
                    {
                        System.Reflection.PropertyInfo myProperty = (System.Reflection.PropertyInfo)member;

                        MethodInfo info = myProperty.GetSetMethod(false);
                        if (info != null)
                        {
                            try
                            {
                                object propertyValue = myProperty.GetValue(obj, null);
                                if (propertyValue is ICloneable)
                                {
                                    myProperty.SetValue(targetDeepCopyObj, (propertyValue as ICloneable).Clone(), null);
                                }
                                else
                                {
                                    myProperty.SetValue(targetDeepCopyObj, CopyOjbect(propertyValue), null);
                                }
                            }
                            catch
                            {
                                //TODO
                                //输出你要处理的异常代码
                                
                            }
                        }
                    }
                }
            }
            return targetDeepCopyObj;
        }
        #endregion
    }
}
