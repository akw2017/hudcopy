using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AIC.Core.Models
{
    public partial class LoginInfo : BindableBase
    {
        private string userName;
        public string UserName
        {
            get
            {
                return userName;
            }
            set
            {
                userName = value;
                OnPropertyChanged("UserName");
            }
        }

        public string UserCode { get; set; }

        private string password;
        public string Password
        {
            get
            {
                return password;
            }
            set
            {
                password = value;
                OnPropertyChanged("Password");
            }
        }
        public bool LoginStatus { get; set; }
        public string Level { get; set; }

        private string errorInformation;
        public string RrrorInformation
        {
            get
            {
                return errorInformation;
            }
            set
            {
                errorInformation = value;
                OnPropertyChanged("RrrorInformation");
            }
        }

        private ServerInfo serverInfo;
        public ServerInfo ServerInfo
        {
            get
            {
                return serverInfo;
            }
            set
            {
                serverInfo = value;
                OnPropertyChanged("ServerInfo");
            }
        }

        public bool HasSecondaryServer { get; set; }

        public LoginInfo(string user, string password, string level, ServerInfo serverinfo)
        {
            UserName = user;
            Password = password;
            Level = level;
            ServerInfo = serverinfo;
        }
        public LoginInfo(string user, string level, ServerInfo info) : this(user, "", level, info) { }

        public void SetLoginInfo(string user, string password, string level, ServerInfo serverinfo)
        {
            UserName = user;
            Password = password;
            Level = level;
            ServerInfo = serverinfo;           
        }
        public void ClearLoginInfo()
        {
            UserName = "";
            Password = "";
            LoginStatus = false;
            Level = "";
            RrrorInformation = "";
            ServerInfo = new ServerInfo();
            HasSecondaryServer = false;
        }
        public LoginInfo DeepClone()
        {
            return CopyOjbect(this) as LoginInfo;
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        //浅拷贝
        public LoginInfo ShallowClone()
        {
            return this.Clone() as LoginInfo;
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
