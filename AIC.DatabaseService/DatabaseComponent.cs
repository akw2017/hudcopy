using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nito.AsyncEx;
using AIC.Core.LMModels;
using AIC.M9600.Client.DataProvider;
using AIC.M9600.Common.DTO.Web;
using AIC.M9600.Common.MasterDB.Generated;
using AIC.ServiceInterface;
using AIC.Core.Helpers;
using AIC.Core.Events;
using AIC.Core;
using AIC.Core.Models;
using Newtonsoft.Json;
using System.Diagnostics;

namespace AIC.DatabaseService
{
    public partial class DatabaseComponent : IDatabaseComponent
    {
        public Dictionary<string, T1_RootCard> T_RootCard { get; private set; }
        public Dictionary<string, List<T1_Organization>> T_Organization { get; private set; }
        public Dictionary<string, List<T1_Device>> T_Device { get; private set; }
        public Dictionary<string, List<T1_Item>> T_Item { get; private set; }
        public Dictionary<string, List<T1_User>> T_User { get; private set; }
        public Dictionary<string, List<T1_Role>> T_Role { get; private set; }
        public Dictionary<string, List<T1_Menu>> T_Menu { get; private set; }
        public Dictionary<string, List<T1_OrganizationPrivilege>> T_OrganizationPrivilege { get; private set; }

        public string MainServerIp { get; set; }

        public DatabaseComponent()
        {
            T_RootCard = new Dictionary<string, T1_RootCard>();
            T_Organization = new Dictionary<string, List<T1_Organization>>();
            T_Device = new Dictionary<string, List<T1_Device>>();
            T_Item = new Dictionary<string, List<T1_Item>>();
            T_User = new Dictionary<string, List<T1_User>>();
            T_Role = new Dictionary<string, List<T1_Role>>();
            T_Menu = new Dictionary<string, List<T1_Menu>>();
            T_OrganizationPrivilege = new Dictionary<string, List<T1_OrganizationPrivilege>>();
        }
      
        public void InitDatabase(string ip)
        {
            //if (!Clients.ContainsKey(ip))
            //{
            //    var client = new DataProvider(ip, LocalSetting.ServerPort, LocalSetting.MajorVersion, LocalSetting.MinorVersion);
            //    lock (Clients)
            //    {
            //        Clients.Add(ip, client);
            //    }
            //}
            if (!T_User.ContainsKey(ip))
            {
                var table = new List<T1_User>();
                T_User.Add(ip, table);
            }
            if (!T_Role.ContainsKey(ip))
            {
                var table = new List<T1_Role>();
                T_Role.Add(ip, table);
            }
            if (!T_Menu.ContainsKey(ip))
            {
                var table = new List<T1_Menu>();
                T_Menu.Add(ip, table);
            }
            if (!T_Device.ContainsKey(ip))
            {
                var table = new List<T1_Device>();
                T_Device.Add(ip, table);
            }
            if (!T_Organization.ContainsKey(ip))
            {
                var table = new List<T1_Organization>();
                T_Organization.Add(ip, table);
            }
            if (!T_Item.ContainsKey(ip))
            {
                var table = new List<T1_Item>();
                T_Item.Add(ip, table);
            }
            if (!T_OrganizationPrivilege.ContainsKey(ip))
            {
                var table = new List<T1_OrganizationPrivilege>();
                T_OrganizationPrivilege.Add(ip, table);
            }
            if (!T_RootCard.ContainsKey(ip))
            {
                var rootCard = new T1_RootCard();
                T_RootCard.Add(ip, rootCard);
            }
        }            

        public void ClearDatabase()
        {
            T_RootCard = new Dictionary<string, T1_RootCard>();
            T_Organization = new Dictionary<string, List<T1_Organization>>();
            T_Device = new Dictionary<string, List<T1_Device>>();
            T_Item = new Dictionary<string, List<T1_Item>>();
            T_User = new Dictionary<string, List<T1_User>>();
            T_Role = new Dictionary<string, List<T1_Role>>();
            T_Menu = new Dictionary<string, List<T1_Menu>>();
            T_OrganizationPrivilege = new Dictionary<string, List<T1_OrganizationPrivilege>>();
        }

        public List<string> GetServerIPCategory()
        {
            return new List<string>(T_RootCard.Keys.ToList());
        }

        public List<T1_User> GetUserData(string ip)
        {
            if (T_User.ContainsKey(ip))
            {
                return T_User[ip];
            }
            else
            {
                return null; 
            }
        }

        public List<T1_Role> GetRoleData(string ip)
        {
            if (T_Role.ContainsKey(ip))
            {
                return T_Role[ip];
            }
            else
            {
                return null;
            }
        }

        public List<T1_Menu> GetMenuData(string ip)
        {
            if (T_Menu.ContainsKey(ip))
            {
                return T_Menu[ip];
            }
            else
            {
                return null;
            }
        }

        public List<T1_Device> GetDeviceData(string ip)
        {
            if (T_Device.ContainsKey(ip))
            {
                return T_Device[ip];
            }
            else
            {
                return null;
            }
        }

        public List<T1_Organization> GetOrganizationData(string ip)
        {
            if (T_Organization.ContainsKey(ip))
            {
                return T_Organization[ip];
            }
            else
            {
                return null;
            }
        }

        public List<T1_Item> GetItemData(string ip)
        {
            if (T_Item.ContainsKey(ip))
            {
                return T_Item[ip];
            }
            else
            {
                return null;
            }
        }

        public List<T1_OrganizationPrivilege> GetOrganizationPrivilegeData(string ip)
        {
            if (T_OrganizationPrivilege.ContainsKey(ip))
            {
                return T_OrganizationPrivilege[ip];
            }
            else
            {
                return null;
            }
        }       

        public T1_RootCard GetRootCard(string ip)
        {
            if (T_RootCard.ContainsKey(ip))
            {
                return T_RootCard[ip];
            }
            else
            {
                return null;
            }
        }

        public async Task<List<T1_User>> LoadUserData(string ip)
        {
            var client = new DataProvider(ip, LocalSetting.ServerPort, LocalSetting.MajorVersion, LocalSetting.MinorVersion);
            //var client = Clients[ip];
            var userTask = Task.Run(() => client.Query<T_User>(null, "where 1 = 1", null));           
            
            await Task.WhenAll(userTask);
         
            if (userTask.Result.IsOK)
            {
                T_User[ip].Clear();
                userTask.Result.ResponseItem.ForEach(p => T_User[ip].Add(ClassCopyHelper.AutoCopy<T_User, T1_User>(p)));
                return T_User[ip];
            }         
            else
            {
                string error = "服务器" + ip + " " +  DateTime.Now.ToString() + "登陆错误:" + userTask.Result.ErrorType + "-" + userTask.Result.ErrorMessage;
                EventAggregatorService.Instance.EventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("数据库操作", new Exception(userTask.Result.ErrorMessage)));
                return null;
            }       
        }

        public async Task<List<T1_Role>> LoadRoleData(string ip)
        {
            var client = new DataProvider(ip, LocalSetting.ServerPort, LocalSetting.MajorVersion, LocalSetting.MinorVersion);
            //var client = Clients[ip];
            var roleTask = Task.Run(() => client.Query<T_Role>(null, "where 1 = 1", null));

            await Task.WhenAll(roleTask);

            if (roleTask.Result.IsOK)
            {
                T_Role[ip].Clear();
                roleTask.Result.ResponseItem.ForEach(p => T_Role[ip].Add(ClassCopyHelper.AutoCopy<T_Role, T1_Role>(p)));
                return T_Role[ip];
            }
            else
            {
                string error = "服务器" + ip + " " + DateTime.Now.ToString() + "登陆错误:" + roleTask.Result.ErrorType + "-" + roleTask.Result.ErrorMessage;
                EventAggregatorService.Instance.EventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("数据库操作", new Exception(error)));
                return null;
            }          
        }

        public async Task<List<T1_Menu>> LoadMenuData(string ip)
        {
            var client = new DataProvider(ip, LocalSetting.ServerPort, LocalSetting.MajorVersion, LocalSetting.MinorVersion);
            //var client = Clients[ip];
            var menuTask = Task.Run(() => client.Query<T_Menu>(null, "where 1 = 1", null));

            await Task.WhenAll(menuTask);

            if (menuTask.Result.IsOK)
            {
                T_Menu[ip].Clear();
                menuTask.Result.ResponseItem.ForEach(p => T_Menu[ip].Add(ClassCopyHelper.AutoCopy<T_Menu, T1_Menu>(p)));
                return T_Menu[ip];
            }
            else
            {
                string error = "服务器" + ip + " " + DateTime.Now.ToString() + "查询错误:" + menuTask.Result.ErrorType + "-" + menuTask.Result.ErrorMessage;
                EventAggregatorService.Instance.EventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("数据库操作", new Exception(error)));
                return null;
            }            
        }

        public async Task<List<T1_Device>> LoadDeviceData(string ip)
        {
            var client = new DataProvider(ip, LocalSetting.ServerPort, LocalSetting.MajorVersion, LocalSetting.MinorVersion);
            //var client = Clients[ip];
            var deviceTask = Task.Run(() => client.Query<T_Device>(null, "where 1 = 1", null));

            await Task.WhenAll(deviceTask);

            if (deviceTask.Result.IsOK)
            {
                T_Device[ip].Clear();
                deviceTask.Result.ResponseItem.ForEach(p => T_Device[ip].Add(ClassCopyHelper.AutoCopy<T_Device, T1_Device>(p)));
                return T_Device[ip];
            }
            else
            {
                string error = "服务器" + ip + " " + DateTime.Now.ToString() + "查询错误:" + deviceTask.Result.ErrorType + "-" + deviceTask.Result.ErrorMessage;
                EventAggregatorService.Instance.EventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("数据库操作", new Exception(error)));
                return null;
            }          
        }

        public async Task<List<T1_Organization>> LoadOrganizationData(string ip)
        {
            var client = new DataProvider(ip, LocalSetting.ServerPort, LocalSetting.MajorVersion, LocalSetting.MinorVersion);
            //var client = Clients[ip];
            var organizationTask = Task.Run(() => client.Query<T_Organization>(null, "where 1 = 1", null));

            await Task.WhenAll(organizationTask);

            if (organizationTask.Result.IsOK)
            {
                T_Organization[ip].Clear();
                organizationTask.Result.ResponseItem.ForEach(p => T_Organization[ip].Add(ClassCopyHelper.AutoCopy<T_Organization, T1_Organization>(p)));
                return T_Organization[ip];
            }
            else
            {
                string error = "服务器" + ip + " " + DateTime.Now.ToString() + "查询错误:" + organizationTask.Result.ErrorType + "-" + organizationTask.Result.ErrorMessage;
                EventAggregatorService.Instance.EventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("数据库操作", new Exception(error)));
                return null;
            }
        }

        public async Task<List<T1_Item>> LoadItemData(string ip)
        {
            var client = new DataProvider(ip, LocalSetting.ServerPort, LocalSetting.MajorVersion, LocalSetting.MinorVersion);
            //var client = Clients[ip];
            var itemTask = Task.Run(() => client.Query<T_Item>(null, "where 1 = 1", null));

            await Task.WhenAll(itemTask);

            if (itemTask.Result.IsOK)
            {
                T_Item[ip].Clear();
                itemTask.Result.ResponseItem.ForEach(p => T_Item[ip].Add(ClassCopyHelper.AutoCopy<T_Item, T1_Item>(p)));
                return T_Item[ip];
            }
            else
            {
                string error = "服务器" + ip + " " + DateTime.Now.ToString() + "查询错误:" + itemTask.Result.ErrorType + "-" + itemTask.Result.ErrorMessage;
                EventAggregatorService.Instance.EventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("数据库操作", new Exception(error)));
                return null;
            }
        }     

        public async Task<List<T1_OrganizationPrivilege>> LoadOrganizationPrivilegeData(string ip)
        {
            var client = new DataProvider(ip, LocalSetting.ServerPort, LocalSetting.MajorVersion, LocalSetting.MinorVersion);
            //var client = Clients[ip];
            var organizationPrivilegeTask = Task.Run(() => client.Query<T_OrganizationPrivilege>(null, "where 1 = 1", null));

            await Task.WhenAll(organizationPrivilegeTask);

            if (organizationPrivilegeTask.Result.IsOK)
            {
                T_OrganizationPrivilege[ip].Clear();
                organizationPrivilegeTask.Result.ResponseItem.ForEach(p => T_OrganizationPrivilege[ip].Add(ClassCopyHelper.AutoCopy<T_OrganizationPrivilege, T1_OrganizationPrivilege>(p)));
                return T_OrganizationPrivilege[ip];
            }
            else
            {
                string error = "服务器" + ip + " " + DateTime.Now.ToString() + "查询错误:" + organizationPrivilegeTask.Result.ErrorType + "-" + organizationPrivilegeTask.Result.ErrorMessage;
                EventAggregatorService.Instance.EventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("数据库操作", new Exception(error)));
                return null;
            }
        }

        public async Task<List<T1_DivFreInfo>> LoadDivFreData(string ip)
        {
            var client = new DataProvider(ip, LocalSetting.ServerPort, LocalSetting.MajorVersion, LocalSetting.MinorVersion);
            //var client = Clients[ip];
            var divFreInfoTask = Task.Run(() => client.Query<T_DivFreInfo>(null, "where 1 = 1", null));
            await Task.WhenAll(divFreInfoTask);
            if (divFreInfoTask.Result.IsOK)
            {
                T_RootCard[ip].T_DivFreInfo.Clear();
                divFreInfoTask.Result.ResponseItem.ForEach(p => T_RootCard[ip].T_DivFreInfo.Add(ClassCopyHelper.AutoCopy<T_DivFreInfo, T1_DivFreInfo>(p)));
                return T_RootCard[ip].T_DivFreInfo;
            }
            else
            {
                string error = "服务器" + ip + " " + DateTime.Now.ToString() + "查询错误:" + divFreInfoTask.Result.ErrorType + "-" + divFreInfoTask.Result.ErrorMessage;
                EventAggregatorService.Instance.EventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("数据库操作", new Exception(error)));
                return null;
            }           
        }

        public async Task<T1_RootCard> LoadHardwave(string ip)
        {
            try
            {               
                await GetHardwareTables(ip, T_RootCard[ip]);               
                return T_RootCard[ip];
            }
            catch (Exception e)
            {
                throw new Exception("数据库初始化失败", e);
            }
        }

        public async Task<bool> UploadHardwave(string ip, T1_RootCard rootcard)
        {
            try
            {
                var client = new DataProvider(ip, LocalSetting.ServerPort, LocalSetting.MajorVersion, LocalSetting.MinorVersion);
                return await AddHardwareTables(ip, client, rootcard); 
            }
            catch (Exception e)
            {
                throw new Exception("数据库初始化失败", e);
            }
        }

        public async Task<bool> DeleteHardwave(string ip, T1_RootCard rootcard)
        {
            try
            {                
                return await DeleteHardwareTables(ip, rootcard);             
            }
            catch (Exception e)
            {
                throw new Exception("数据库初始化失败", e);
            }
        }

        public async Task<bool> Add<T>(string ip, ICollection<T> objs)
        {
            return await Task.Run(() =>
            {
                var client = new DataProvider(ip, LocalSetting.ServerPort, LocalSetting.MajorVersion, LocalSetting.MinorVersion);
                WebResponse<long[]> addResult = client.Add<T>(objs);
                //先判断是不是OK          
                if (addResult.IsOK)
                {
                    int i = 0;
                    //添加成功
                    foreach(var obj in objs)//更新到内存
                    {
                        var id = addResult.ResponseItem[i];
                        add(ip, obj, id);
                        i++;
                    }
                    return true;
                }
                else
                {
                    string error = "服务器" + ip + " " + "类型" + typeof(T).Name + DateTime.Now.ToString() + "添加错误:" + addResult.ErrorType + "-" + addResult.ErrorMessage;
                    //ErrorMessage是错误信息
                    EventAggregatorService.Instance.EventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("数据库操作", new Exception(error)));
                    return false;
                }
            });                
        }

        public async Task<bool> Add<T>(string ip, T obj)
        {            
            return await Task.Run(() =>
            {
                var client = new DataProvider(ip, LocalSetting.ServerPort, LocalSetting.MajorVersion, LocalSetting.MinorVersion);
                WebResponse<long[]> addResult = client.Add<T>(
                    new T[] { obj });
                //先判断是不是OK          
                if (addResult.IsOK)
                {
                    //添加成功
                    add(ip, obj, addResult.ResponseItem[0]);//更新到内存                  
                    return true;
                }
                else
                {
                    string error = "服务器" + ip + " " + "类型" + typeof(T).Name + DateTime.Now.ToString() + "添加错误:" + addResult.ErrorType + "-" + addResult.ErrorMessage;
                    //ErrorMessage是错误信息
                    EventAggregatorService.Instance.EventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("数据库操作", new Exception(error)));
                    return false;
                }
            });            
        }

        public async Task<List<T>> Query<T>(string ip, ICollection<string> columns, string condition, object[] args)
        {          
            return await Task.Run(() =>
            {
                var client = new DataProvider(ip, LocalSetting.ServerPort, LocalSetting.MajorVersion, LocalSetting.MinorVersion);
                WebResponse<List<T>> queryResult = client.Query<T>(columns, condition, args);
                //先判断是不是OK          
                if (queryResult.IsOK)
                {                   
                    //添加成功
                    return queryResult.ResponseItem;
                }
                else
                {
                    string error = "服务器" + ip + " " + "类型" + typeof(T).Name + DateTime.Now.ToString() + "查询错误:" + queryResult.ErrorType + "-" + queryResult.ErrorMessage;
                    //ErrorMessage是错误信息
                    EventAggregatorService.Instance.EventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("数据库操作", new Exception(error)));
                    return null;
                }
            });
        }

        public async Task<bool> Modify<T>(string ip, ICollection<string> columns, ICollection<T> objs)
        {          
            return await Task.Run(() =>
            {
                var client = new DataProvider(ip, LocalSetting.ServerPort, LocalSetting.MajorVersion, LocalSetting.MinorVersion);
                WebResponse modifyResult = client.Modify<T>(columns, objs);
                //先判断是不是OK          
                if (modifyResult.IsOK)
                {
                    //添加成功
                    return true;
                }
                else
                {
                    string error = "服务器" + ip + " " + "类型" + typeof(T).Name + DateTime.Now.ToString() + "修改错误:" + modifyResult.ErrorType + "-" + modifyResult.ErrorMessage;
                    //ErrorMessage是错误信息
                    EventAggregatorService.Instance.EventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("数据库操作", new Exception(error)));
                    return false;
                }
            });
        }

        public async Task<bool> Modify<T>(string ip, ICollection<string> columns, T obj)
        {
            return await Task.Run(() =>
            {
                var client = new DataProvider(ip, LocalSetting.ServerPort, LocalSetting.MajorVersion, LocalSetting.MinorVersion);
                WebResponse modifyResult = client.Modify<T>(columns, new T[] { obj });
                //先判断是不是OK          
                if (modifyResult.IsOK)
                {
                    //添加成功
                    return true;
                }
                else
                {
                    string error = "服务器" + ip + " " + "类型" + typeof(T).Name + DateTime.Now.ToString() + "修改错误:" + modifyResult.ErrorType + "-" + modifyResult.ErrorMessage;
                    //ErrorMessage是错误信息
                    EventAggregatorService.Instance.EventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("数据库操作", new Exception(error)));
                    return false;
                }
            });
        }

        public async Task<bool> Delete<T>(string ip,  ICollection<object> ids)
        {           
            return await Task.Run(() =>
            {
                var client = new DataProvider(ip, LocalSetting.ServerPort, LocalSetting.MajorVersion, LocalSetting.MinorVersion);
                WebResponse deleteResult = client.Delete<T>("id", ids);
                //先判断是不是OK          
                if (deleteResult.IsOK)
                {
                    //删除成功
                    foreach (var id in ids)//更新到内存
                    {
                        delete(ip, typeof(T).Name, (long)id);
                    }
                    return true;
                }
                else
                {
                    string error = "服务器" + ip + " " + "类型" + typeof(T).Name + DateTime.Now.ToString() + "删除错误:" + deleteResult.ErrorType + "-" + deleteResult.ErrorMessage;
                    //ErrorMessage是错误信息
                    EventAggregatorService.Instance.EventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("数据库操作", new Exception(error)));
                    return false;
                }
            });
        }

        public async Task<bool> Delete<T>(string ip, long id)
        {           
            return await Task.Run(() =>
            {
                var client = new DataProvider(ip, LocalSetting.ServerPort, LocalSetting.MajorVersion, LocalSetting.MinorVersion);
                WebResponse deleteResult = client.Delete<T>("id", new object[] { id });
                //先判断是不是OK          
                if (deleteResult.IsOK)
                {
                    //删除成功
                    delete(ip, typeof(T).Name, id);//更新到内存         
                    return true;
                }
                else
                {
                    string error = "服务器" + ip + " " + "类型" + typeof(T).Name + DateTime.Now.ToString() + "删除错误:" + deleteResult.ErrorType + "-" + deleteResult.ErrorMessage;
                    //ErrorMessage是错误信息
                    EventAggregatorService.Instance.EventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("数据库操作", new Exception(error)));
                    return false;
                }
            });
        }

        public async Task<bool> Complex(string ip, IDictionary<string, ICollection<object>> addObjs,
          IDictionary<string, Tuple<ICollection<string>, ICollection<object>>> modifyObjs,
          IDictionary<string, Tuple<string, ICollection<object>>> deleteObjs)
        {
            return await Task.Run(() =>
            {
                var client = new DataProvider(ip, LocalSetting.ServerPort, LocalSetting.MajorVersion, LocalSetting.MinorVersion);
                var complexResult = client.Complex(addObjs, modifyObjs, deleteObjs);
                //先判断是不是OK          
                if (complexResult.IsOK)
                {

                    if (deleteObjs != null)
                    {
                        foreach (KeyValuePair<string, Tuple<string, ICollection<object>>> kvp in deleteObjs)
                        {
                            //删除成功
                            foreach (var obj in kvp.Value.Item2)//更新到内存
                            {                               
                                delete(ip, kvp.Key, (long)obj);//更新到内存  
                            }                            
                        }
                    }

                    if (addObjs != null)
                    {
                        foreach (KeyValuePair<string, ICollection<object>> kvp in addObjs)
                        {
                            var ids = complexResult.ResponseItem[kvp.Key].ToList();
                            //添加数据
                            int i = 0;
                            foreach (var obj in kvp.Value)//更新到内存
                            {
                                var id = ids[i];
                                add(ip, obj, id);
                                i++;
                            }
                        }
                    }
                    return true;
                }
                else
                {
                    string error = "服务器" + ip  + DateTime.Now.ToString() + "复合错误:" + complexResult.ErrorType + "-" + complexResult.ErrorMessage;
                    //ErrorMessage是错误信息
                    EventAggregatorService.Instance.EventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("数据库操作", new Exception(error)));
                    return false;
                }
            });
        }

        public async Task<RootCard> ComplexWithJson(string serverip, string ip, string json, IDictionary<string, ICollection<object>> addObjs,
        IDictionary<string, Tuple<ICollection<string>, ICollection<object>>> modifyObjs,
        IDictionary<string, Tuple<string, ICollection<object>>> deleteObjs)
        {
            return await Task.Run(() =>
            {
                var client = new DataProvider(serverip, LocalSetting.ServerPort, LocalSetting.MajorVersion, LocalSetting.MinorVersion);
                var complexResult = client.CommunicateDevice(ip, @LocalSetting.PDAPort, json, addObjs, modifyObjs, deleteObjs);
                //先判断是不是OK          
                if (complexResult.IsOK)
                {
                    //删除数据
                    var response = complexResult.ResponseItem;
                    if (response != null && response != "")
                    {

                        RootCard root = JsonConvert.DeserializeObject<RootCard>(response);
                        if (root.ResponseError == null || root.ResponseError.Count == 0)
                        {                            
                            return root;
                        }
                        else
                        {
                            foreach (var responseerror in root.ResponseError)
                            {
                                string error = responseerror.DateTime.ToString() + "|" + responseerror.Type + "|" + responseerror.Grade + "|" + responseerror.Content;
                                EventAggregatorService.Instance.EventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("数采器通讯", new Exception(error)));
                            }                            
                        }
                    }
                    return null;
                }
                else
                {
                    string error = "服务器" + ip  + DateTime.Now.ToString() + "通信错误:" + complexResult.ErrorType + "-" + complexResult.ErrorMessage;
                    //ErrorMessage是错误信息
                    EventAggregatorService.Instance.EventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("数据库操作", new Exception(error)));
                    return null;
                }
            });
        }    
        
        private async Task GetHardwareTables(string ip, T1_RootCard rootCard)
        {
            var client = new DataProvider(ip, LocalSetting.ServerPort, LocalSetting.MajorVersion, LocalSetting.MinorVersion);
            var mainControlCardTask = Task.Run(() => client.Query<T_MainControlCard>(null, "where 1 = 1", null));
            var wireMatchingCardTask = Task.Run(() => client.Query<T_WireMatchingCard>(null, "where 1 = 1", null));
            var wirelessReceiveCardTask = Task.Run(() => client.Query<T_WirelessReceiveCard>(null, "where 1 = 1", null));
            var transmissionCardTask = Task.Run(() => client.Query<T_TransmissionCard>(null, "where 1 = 1", null));
            var abstractChannelInfoTask = Task.Run(() => client.Query<T_AbstractChannelInfo>(null, "where 1 = 1", null));
            var iEPEChannelInfoTask = Task.Run(() => client.Query<T_IEPEChannelInfo>(null, "where 1 = 1", null));
            var eddyCurrentDisplacementChannelInfoTask = Task.Run(() => client.Query<T_EddyCurrentDisplacementChannelInfo>(null, "where 1 = 1", null));
            var eddyCurrentKeyPhaseChannelInfoTask = Task.Run(() => client.Query<T_EddyCurrentKeyPhaseChannelInfo>(null, "where 1 = 1", null));
            var eddyCurrentTachometerChannelInfoTask = Task.Run(() => client.Query<T_EddyCurrentTachometerChannelInfo>(null, "where 1 = 1", null));
            var digitTachometerChannelInfoTask = Task.Run(() => client.Query<T_DigitTachometerChannelInfo>(null, "where 1 = 1", null));
            var analogRransducerInChannelInfoTask = Task.Run(() => client.Query<T_AnalogRransducerInChannelInfo>(null, "where 1 = 1", null));
            var relayChannelInfoTask = Task.Run(() => client.Query<T_RelayChannelInfo>(null, "where 1 = 1", null));
            var digitRransducerInChannelInfoTask = Task.Run(() => client.Query<T_DigitRransducerInChannelInfo>(null, "where 1 = 1", null));
            var digitRransducerOutChannelInfoTask = Task.Run(() => client.Query<T_DigitRransducerOutChannelInfo>(null, "where 1 = 1", null));
            var analogRransducerOutChannelInfoTask = Task.Run(() => client.Query<T_AnalogRransducerOutChannelInfo>(null, "where 1 = 1", null));
            var wirelessScalarChannelInfoTask = Task.Run(() => client.Query<T_WirelessScalarChannelInfo>(null, "where 1 = 1", null));
            var wirelessVibrationChannelInfoTask = Task.Run(() => client.Query<T_WirelessVibrationChannelInfo>(null, "where 1 = 1", null));
            var abstractSlotInfoTask = Task.Run(() => client.Query<T_AbstractSlotInfo>(null, "where 1 = 1", null));
            var iEPESlotTask = Task.Run(() => client.Query<T_IEPESlot>(null, "where 1 = 1", null));
            var eddyCurrentDisplacementSlotTask = Task.Run(() => client.Query<T_EddyCurrentDisplacementSlot>(null, "where 1 = 1", null));
            var eddyCurrentKeyPhaseSlotTask = Task.Run(() => client.Query<T_EddyCurrentKeyPhaseSlot>(null, "where 1 = 1", null));
            var eddyCurrentTachometerSlotTask = Task.Run(() => client.Query<T_EddyCurrentTachometerSlot>(null, "where 1 = 1", null));
            var digitTachometerSlotTask = Task.Run(() => client.Query<T_DigitTachometerSlot>(null, "where 1 = 1", null));
            var analogRransducerInSlotTask = Task.Run(() => client.Query<T_AnalogRransducerInSlot>(null, "where 1 = 1", null));
            var relaySlotTask = Task.Run(() => client.Query<T_RelaySlot>(null, "where 1 = 1", null));
            var digitRransducerInSlotTask = Task.Run(() => client.Query<T_DigitRransducerInSlot>(null, "where 1 = 1", null));
            var digitRransducerOutSlotTask = Task.Run(() => client.Query<T_DigitRransducerOutSlot>(null, "where 1 = 1", null));
            var analogRransducerOutSlotTask = Task.Run(() => client.Query<T_AnalogRransducerOutSlot>(null, "where 1 = 1", null));
            var wirelessScalarSlotTask = Task.Run(() => client.Query<T_WirelessScalarSlot>(null, "where 1 = 1", null));
            var wirelessVibrationSlotTask = Task.Run(() => client.Query<T_WirelessVibrationSlot>(null, "where 1 = 1", null));
            var divFreInfoTask = Task.Run(() => client.Query<T_DivFreInfo>(null, "where 1 = 1", null));
            //await Task.WhenAll(mainControlCardTask, wireMatchingCardTask,
            //  wirelessReceiveCardTask, transmissionCardTask,
            //  abstractChannelInfoTask, iEPEChannelInfoTask,
            //  eddyCurrentDisplacementChannelInfoTask, eddyCurrentKeyPhaseChannelInfoTask,
            //  eddyCurrentTachometerChannelInfoTask, digitTachometerChannelInfoTask,
            //  analogRransducerInChannelInfoTask, relayChannelInfoTask,
            //  digitRransducerInChannelInfoTask, digitRransducerOutChannelInfoTask,
            //  analogRransducerOutChannelInfoTask, wirelessScalarChannelInfoTask,
            //  wirelessVibrationChannelInfoTask, abstractSlotInfoTask,
            //  iEPESlotTask, eddyCurrentDisplacementSlotTask,
            //  eddyCurrentKeyPhaseSlotTask, eddyCurrentTachometerSlotTask,
            //  digitTachometerSlotTask, analogRransducerInSlotTask,
            //  relaySlotTask, digitRransducerInSlotTask,
            //  digitRransducerOutSlotTask, analogRransducerOutSlotTask,
            //  wirelessScalarSlotTask, wirelessVibrationSlotTask,
            //  divFreInfoTask);

            List<Task> lttask = new List<Task>();
            lttask.Add(mainControlCardTask);
            lttask.Add(wireMatchingCardTask);
            lttask.Add(wirelessReceiveCardTask);
            lttask.Add(transmissionCardTask);
            lttask.Add(abstractChannelInfoTask);
            lttask.Add(iEPEChannelInfoTask);
            lttask.Add(eddyCurrentDisplacementChannelInfoTask);
            lttask.Add(eddyCurrentKeyPhaseChannelInfoTask);
            lttask.Add(eddyCurrentTachometerChannelInfoTask);
            lttask.Add(digitTachometerChannelInfoTask);
            lttask.Add(analogRransducerInChannelInfoTask);
            lttask.Add(relayChannelInfoTask);
            lttask.Add(digitRransducerInChannelInfoTask);
            lttask.Add(digitRransducerOutChannelInfoTask);
            lttask.Add(analogRransducerOutChannelInfoTask);
            lttask.Add(wirelessScalarChannelInfoTask);
            lttask.Add(wirelessVibrationChannelInfoTask);
            lttask.Add(abstractSlotInfoTask);
            lttask.Add(iEPESlotTask);
            lttask.Add(eddyCurrentDisplacementSlotTask);
            lttask.Add(eddyCurrentKeyPhaseSlotTask);
            lttask.Add(eddyCurrentTachometerSlotTask);
            lttask.Add(digitTachometerSlotTask);
            lttask.Add(analogRransducerInSlotTask);
            lttask.Add(relaySlotTask);
            lttask.Add(digitRransducerInSlotTask);
            lttask.Add(digitRransducerOutSlotTask);
            lttask.Add(analogRransducerOutSlotTask);
            lttask.Add(wirelessScalarSlotTask);
            lttask.Add(wirelessVibrationSlotTask);
            lttask.Add(divFreInfoTask);

            var sw = Stopwatch.StartNew();
            await Task.WhenAll(lttask.ToArray());  
            Console.WriteLine("消耗时间" + sw.Elapsed.ToString());
            if (mainControlCardTask.Result.IsOK)
            {
                rootCard.T_MainControlCard.Clear();
                mainControlCardTask.Result.ResponseItem.ForEach(p => rootCard.T_MainControlCard.Add(ClassCopyHelper.AutoCopy<T_MainControlCard, T1_MainControlCard>(p)));
            }
            else
            {
                string error = "服务器" + ip + " " + DateTime.Now.ToString() + "查询错误:" + mainControlCardTask.Result.ErrorType + "-" + mainControlCardTask.Result.ErrorMessage;
                EventAggregatorService.Instance.EventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("数据库操作", new Exception(error)));
            }
            if (wireMatchingCardTask.Result.IsOK)
            {
                rootCard.T_WireMatchingCard.Clear();
                wireMatchingCardTask.Result.ResponseItem.ForEach(p => rootCard.T_WireMatchingCard.Add(ClassCopyHelper.AutoCopy<T_WireMatchingCard, T1_WireMatchingCard>(p)));
            }
            else
            {
                string error = "服务器" + ip + " " + DateTime.Now.ToString() + "查询错误:" + wireMatchingCardTask.Result.ErrorType + "-" + wireMatchingCardTask.Result.ErrorMessage;
                EventAggregatorService.Instance.EventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("数据库操作", new Exception(error)));
            }
            if (wirelessReceiveCardTask.Result.IsOK)
            {
                rootCard.T_WirelessReceiveCard.Clear();
                wirelessReceiveCardTask.Result.ResponseItem.ForEach(p => rootCard.T_WirelessReceiveCard.Add(ClassCopyHelper.AutoCopy<T_WirelessReceiveCard, T1_WirelessReceiveCard>(p)));
            }
            else
            {
                string error = "服务器" + ip + " " + DateTime.Now.ToString() + "查询错误:" + wirelessReceiveCardTask.Result.ErrorType + "-" + wirelessReceiveCardTask.Result.ErrorMessage;
                EventAggregatorService.Instance.EventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("数据库操作", new Exception(error)));
            }
            if (transmissionCardTask.Result.IsOK)
            {
                rootCard.T_TransmissionCard.Clear();
                transmissionCardTask.Result.ResponseItem.ForEach(p => rootCard.T_TransmissionCard.Add(ClassCopyHelper.AutoCopy<T_TransmissionCard, T1_TransmissionCard>(p)));
            }
            else
            {
                string error = "服务器" + ip + " " + DateTime.Now.ToString() + "查询错误:" + transmissionCardTask.Result.ErrorType + "-" + transmissionCardTask.Result.ErrorMessage;
                EventAggregatorService.Instance.EventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("数据库操作", new Exception(error)));
            }
            if (abstractChannelInfoTask.Result.IsOK)
            {
                rootCard.T_AbstractChannelInfo.Clear();
                abstractChannelInfoTask.Result.ResponseItem.ForEach(p => rootCard.T_AbstractChannelInfo.Add(ClassCopyHelper.AutoCopy<T_AbstractChannelInfo, T1_AbstractChannelInfo>(p)));
            }
            else
            {
                string error = "服务器" + ip + " " + DateTime.Now.ToString() + "查询错误:" + abstractChannelInfoTask.Result.ErrorType + "-" + abstractChannelInfoTask.Result.ErrorMessage;
                EventAggregatorService.Instance.EventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("数据库操作", new Exception(error)));
            }
            if (iEPEChannelInfoTask.Result.IsOK)
            {
                rootCard.T_IEPEChannelInfo.Clear();
                iEPEChannelInfoTask.Result.ResponseItem.ForEach(p => rootCard.T_IEPEChannelInfo.Add(ClassCopyHelper.AutoCopy<T_IEPEChannelInfo, T1_IEPEChannelInfo>(p)));
            }
            else
            {
                string error = "服务器" + ip + " " + DateTime.Now.ToString() + "查询错误:" + iEPEChannelInfoTask.Result.ErrorType + "-" + iEPEChannelInfoTask.Result.ErrorMessage;
                EventAggregatorService.Instance.EventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("数据库操作", new Exception(error)));
            }
            if (eddyCurrentDisplacementChannelInfoTask.Result.IsOK)
            {
                rootCard.T_EddyCurrentDisplacementChannelInfo.Clear();
                eddyCurrentDisplacementChannelInfoTask.Result.ResponseItem.ForEach(p => rootCard.T_EddyCurrentDisplacementChannelInfo.Add(ClassCopyHelper.AutoCopy<T_EddyCurrentDisplacementChannelInfo, T1_EddyCurrentDisplacementChannelInfo>(p)));
            }
            else
            {
                string error = "服务器" + ip + " " + DateTime.Now.ToString() + "查询错误:" + eddyCurrentDisplacementChannelInfoTask.Result.ErrorType + "-" + eddyCurrentDisplacementChannelInfoTask.Result.ErrorMessage;
                EventAggregatorService.Instance.EventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("数据库操作", new Exception(error)));
            }
            if (eddyCurrentKeyPhaseChannelInfoTask.Result.IsOK)
            {
                rootCard.T_EddyCurrentKeyPhaseChannelInfo.Clear();
                eddyCurrentKeyPhaseChannelInfoTask.Result.ResponseItem.ForEach(p => rootCard.T_EddyCurrentKeyPhaseChannelInfo.Add(ClassCopyHelper.AutoCopy<T_EddyCurrentKeyPhaseChannelInfo, T1_EddyCurrentKeyPhaseChannelInfo>(p)));
            }
            else
            {
                string error = "服务器" + ip + " " + DateTime.Now.ToString() + "查询错误:" + eddyCurrentKeyPhaseChannelInfoTask.Result.ErrorType + "-" + eddyCurrentKeyPhaseChannelInfoTask.Result.ErrorMessage;
                EventAggregatorService.Instance.EventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("数据库操作", new Exception(error)));
            }
            if (eddyCurrentTachometerChannelInfoTask.Result.IsOK)
            {
                rootCard.T_EddyCurrentTachometerChannelInfo.Clear();
                eddyCurrentTachometerChannelInfoTask.Result.ResponseItem.ForEach(p => rootCard.T_EddyCurrentTachometerChannelInfo.Add(ClassCopyHelper.AutoCopy<T_EddyCurrentTachometerChannelInfo, T1_EddyCurrentTachometerChannelInfo>(p)));
            }
            else
            {
                string error = "服务器" + ip + " " + DateTime.Now.ToString() + "查询错误:" + eddyCurrentTachometerChannelInfoTask.Result.ErrorType + "-" + eddyCurrentTachometerChannelInfoTask.Result.ErrorMessage;
                EventAggregatorService.Instance.EventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("数据库操作", new Exception(error)));
            }
            if (digitTachometerChannelInfoTask.Result.IsOK)
            {
                rootCard.T_DigitTachometerChannelInfo.Clear();
                digitTachometerChannelInfoTask.Result.ResponseItem.ForEach(p => rootCard.T_DigitTachometerChannelInfo.Add(ClassCopyHelper.AutoCopy<T_DigitTachometerChannelInfo, T1_DigitTachometerChannelInfo>(p)));
            }
            else
            {
                string error = "服务器" + ip + " " + DateTime.Now.ToString() + "查询错误:" + digitTachometerChannelInfoTask.Result.ErrorType + "-" + digitTachometerChannelInfoTask.Result.ErrorMessage;
                EventAggregatorService.Instance.EventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("数据库操作", new Exception(error)));
            }
            if (analogRransducerInChannelInfoTask.Result.IsOK)
            {
                rootCard.T_AnalogRransducerInChannelInfo.Clear();
                analogRransducerInChannelInfoTask.Result.ResponseItem.ForEach(p => rootCard.T_AnalogRransducerInChannelInfo.Add(ClassCopyHelper.AutoCopy<T_AnalogRransducerInChannelInfo, T1_AnalogRransducerInChannelInfo>(p)));
            }
            else
            {
                string error = "服务器" + ip + " " + DateTime.Now.ToString() + "查询错误:" + analogRransducerInChannelInfoTask.Result.ErrorType + "-" + analogRransducerInChannelInfoTask.Result.ErrorMessage;
                EventAggregatorService.Instance.EventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("数据库操作", new Exception(error)));
            }
            if (relayChannelInfoTask.Result.IsOK)
            {
                rootCard.T_RelayChannelInfo.Clear();
                relayChannelInfoTask.Result.ResponseItem.ForEach(p => rootCard.T_RelayChannelInfo.Add(ClassCopyHelper.AutoCopy<T_RelayChannelInfo, T1_RelayChannelInfo>(p)));
            }
            else
            {
                string error = "服务器" + ip + " " + DateTime.Now.ToString() + "查询错误:" + relayChannelInfoTask.Result.ErrorType + "-" + relayChannelInfoTask.Result.ErrorMessage;
                EventAggregatorService.Instance.EventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("数据库操作", new Exception(error)));
            }
            if (digitRransducerInChannelInfoTask.Result.IsOK)
            {
                rootCard.T_DigitRransducerInChannelInfo.Clear();
                digitRransducerInChannelInfoTask.Result.ResponseItem.ForEach(p => rootCard.T_DigitRransducerInChannelInfo.Add(ClassCopyHelper.AutoCopy<T_DigitRransducerInChannelInfo, T1_DigitRransducerInChannelInfo>(p)));
            }
            else
            {
                string error = "服务器" + ip + " " + DateTime.Now.ToString() + "查询错误:" + digitRransducerInChannelInfoTask.Result.ErrorType + "-" + digitRransducerInChannelInfoTask.Result.ErrorMessage;
                EventAggregatorService.Instance.EventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("数据库操作", new Exception(error)));
            }
            if (digitRransducerOutChannelInfoTask.Result.IsOK)
            {
                rootCard.T_DigitRransducerOutChannelInfo.Clear();
                digitRransducerOutChannelInfoTask.Result.ResponseItem.ForEach(p => rootCard.T_DigitRransducerOutChannelInfo.Add(ClassCopyHelper.AutoCopy<T_DigitRransducerOutChannelInfo, T1_DigitRransducerOutChannelInfo>(p)));
            }
            else
            {
                string error = "服务器" + ip + " " + DateTime.Now.ToString() + "查询错误:" + digitRransducerOutChannelInfoTask.Result.ErrorType + "-" + digitRransducerOutChannelInfoTask.Result.ErrorMessage;
                EventAggregatorService.Instance.EventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("数据库操作", new Exception(error)));
            }
            if (analogRransducerOutChannelInfoTask.Result.IsOK)
            {
                rootCard.T_AnalogRransducerOutChannelInfo.Clear();
                analogRransducerOutChannelInfoTask.Result.ResponseItem.ForEach(p => rootCard.T_AnalogRransducerOutChannelInfo.Add(ClassCopyHelper.AutoCopy<T_AnalogRransducerOutChannelInfo, T1_AnalogRransducerOutChannelInfo>(p)));
            }
            else
            {
                string error = "服务器" + ip + " " + DateTime.Now.ToString() + "查询错误:" + analogRransducerOutChannelInfoTask.Result.ErrorType + "-" + analogRransducerOutChannelInfoTask.Result.ErrorMessage;
                EventAggregatorService.Instance.EventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("数据库操作", new Exception(error)));
            }
            if (wirelessScalarChannelInfoTask.Result.IsOK)
            {
                rootCard.T_WirelessScalarChannelInfo.Clear();
                wirelessScalarChannelInfoTask.Result.ResponseItem.ForEach(p => rootCard.T_WirelessScalarChannelInfo.Add(ClassCopyHelper.AutoCopy<T_WirelessScalarChannelInfo, T1_WirelessScalarChannelInfo>(p)));
            }
            else
            {
                string error = "服务器" + ip + " " + DateTime.Now.ToString() + "查询错误:" + wirelessScalarChannelInfoTask.Result.ErrorType + "-" + wirelessScalarChannelInfoTask.Result.ErrorMessage;
                EventAggregatorService.Instance.EventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("数据库操作", new Exception(error)));
            }
            if (wirelessVibrationChannelInfoTask.Result.IsOK)
            {
                rootCard.T_WirelessVibrationChannelInfo.Clear();
                wirelessVibrationChannelInfoTask.Result.ResponseItem.ForEach(p => rootCard.T_WirelessVibrationChannelInfo.Add(ClassCopyHelper.AutoCopy<T_WirelessVibrationChannelInfo, T1_WirelessVibrationChannelInfo>(p)));
            }
            else
            {
                string error = "服务器" + ip + " " + DateTime.Now.ToString() + "查询错误:" + wirelessVibrationChannelInfoTask.Result.ErrorType + "-" + wirelessVibrationChannelInfoTask.Result.ErrorMessage;
                EventAggregatorService.Instance.EventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("数据库操作", new Exception(error)));
            }
            if (abstractSlotInfoTask.Result.IsOK)
            {
                rootCard.T_AbstractSlotInfo.Clear();
                abstractSlotInfoTask.Result.ResponseItem.ForEach(p => rootCard.T_AbstractSlotInfo.Add(ClassCopyHelper.AutoCopy<T_AbstractSlotInfo, T1_AbstractSlotInfo>(p)));
            }
            else
            {
                string error = "服务器" + ip + " " + DateTime.Now.ToString() + "查询错误:" + abstractSlotInfoTask.Result.ErrorType + "-" + abstractSlotInfoTask.Result.ErrorMessage;
                EventAggregatorService.Instance.EventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("数据库操作", new Exception(error)));
            }
            if (iEPESlotTask.Result.IsOK)
            {
                rootCard.T_IEPESlot.Clear();
                iEPESlotTask.Result.ResponseItem.ForEach(p => rootCard.T_IEPESlot.Add(ClassCopyHelper.AutoCopy<T_IEPESlot, T1_IEPESlot>(p)));
            }
            else
            {
                string error = "服务器" + ip + " " + DateTime.Now.ToString() + "查询错误:" + iEPESlotTask.Result.ErrorType + "-" + iEPESlotTask.Result.ErrorMessage;
                EventAggregatorService.Instance.EventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("数据库操作", new Exception(error)));
            }
            if (eddyCurrentDisplacementSlotTask.Result.IsOK)
            {
                rootCard.T_EddyCurrentDisplacementSlot.Clear();
                eddyCurrentDisplacementSlotTask.Result.ResponseItem.ForEach(p => rootCard.T_EddyCurrentDisplacementSlot.Add(ClassCopyHelper.AutoCopy<T_EddyCurrentDisplacementSlot, T1_EddyCurrentDisplacementSlot>(p)));
            }
            else
            {
                string error = "服务器" + ip + " " + DateTime.Now.ToString() + "查询错误:" + eddyCurrentDisplacementSlotTask.Result.ErrorType + "-" + eddyCurrentDisplacementSlotTask.Result.ErrorMessage;
                EventAggregatorService.Instance.EventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("数据库操作", new Exception(error)));
            }
            if (eddyCurrentKeyPhaseSlotTask.Result.IsOK)
            {
                rootCard.T_EddyCurrentKeyPhaseSlot.Clear();
                eddyCurrentKeyPhaseSlotTask.Result.ResponseItem.ForEach(p => rootCard.T_EddyCurrentKeyPhaseSlot.Add(ClassCopyHelper.AutoCopy<T_EddyCurrentKeyPhaseSlot, T1_EddyCurrentKeyPhaseSlot>(p)));
            }
            else
            {
                string error = "服务器" + ip + " " + DateTime.Now.ToString() + "查询错误:" + eddyCurrentKeyPhaseSlotTask.Result.ErrorType + "-" + eddyCurrentKeyPhaseSlotTask.Result.ErrorMessage;
                EventAggregatorService.Instance.EventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("数据库操作", new Exception(error)));
            }
            if (eddyCurrentTachometerSlotTask.Result.IsOK)
            {
                rootCard.T_EddyCurrentTachometerSlot.Clear();
                eddyCurrentTachometerSlotTask.Result.ResponseItem.ForEach(p => rootCard.T_EddyCurrentTachometerSlot.Add(ClassCopyHelper.AutoCopy<T_EddyCurrentTachometerSlot, T1_EddyCurrentTachometerSlot>(p)));
            }
            else
            {
                string error = "服务器" + ip + " " + DateTime.Now.ToString() + "查询错误:" + eddyCurrentTachometerSlotTask.Result.ErrorType + "-" + eddyCurrentTachometerSlotTask.Result.ErrorMessage;
                EventAggregatorService.Instance.EventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("数据库操作", new Exception(error)));
            }
            if (digitTachometerSlotTask.Result.IsOK)
            {
                rootCard.T_DigitTachometerSlot.Clear();
                digitTachometerSlotTask.Result.ResponseItem.ForEach(p => rootCard.T_DigitTachometerSlot.Add(ClassCopyHelper.AutoCopy<T_DigitTachometerSlot, T1_DigitTachometerSlot>(p)));
            }
            else
            {
                string error = "服务器" + ip + " " + DateTime.Now.ToString() + "查询错误:" + digitTachometerSlotTask.Result.ErrorType + "-" + digitTachometerSlotTask.Result.ErrorMessage;
                EventAggregatorService.Instance.EventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("数据库操作", new Exception(error)));
            }
            if (analogRransducerInSlotTask.Result.IsOK)
            {
                rootCard.T_AnalogRransducerInSlot.Clear();
                analogRransducerInSlotTask.Result.ResponseItem.ForEach(p => rootCard.T_AnalogRransducerInSlot.Add(ClassCopyHelper.AutoCopy<T_AnalogRransducerInSlot, T1_AnalogRransducerInSlot>(p)));
            }
            else
            {
                string error = "服务器" + ip + " " + DateTime.Now.ToString() + "查询错误:" + analogRransducerInSlotTask.Result.ErrorType + "-" + analogRransducerInSlotTask.Result.ErrorMessage;
                EventAggregatorService.Instance.EventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("数据库操作", new Exception(error)));
            }
            if (relaySlotTask.Result.IsOK)
            {
                rootCard.T_RelaySlot.Clear();
                relaySlotTask.Result.ResponseItem.ForEach(p => rootCard.T_RelaySlot.Add(ClassCopyHelper.AutoCopy<T_RelaySlot, T1_RelaySlot>(p)));
            }
            else
            {
                string error = "服务器" + ip + " " + DateTime.Now.ToString() + "查询错误:" + relaySlotTask.Result.ErrorType + "-" + relaySlotTask.Result.ErrorMessage;
                EventAggregatorService.Instance.EventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("数据库操作", new Exception(error)));
            }
            if (digitRransducerInSlotTask.Result.IsOK)
            {
                rootCard.T_DigitRransducerInSlot.Clear();
                digitRransducerInSlotTask.Result.ResponseItem.ForEach(p => rootCard.T_DigitRransducerInSlot.Add(ClassCopyHelper.AutoCopy<T_DigitRransducerInSlot, T1_DigitRransducerInSlot>(p)));
            }
            else
            {
                string error = "服务器" + ip + " " + DateTime.Now.ToString() + "查询错误:" + digitRransducerInSlotTask.Result.ErrorType + "-" + digitRransducerInSlotTask.Result.ErrorMessage;
                EventAggregatorService.Instance.EventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("数据库操作", new Exception(error)));
            }
            if (digitRransducerOutSlotTask.Result.IsOK)
            {
                rootCard.T_DigitRransducerOutSlot.Clear();
                digitRransducerOutSlotTask.Result.ResponseItem.ForEach(p => rootCard.T_DigitRransducerOutSlot.Add(ClassCopyHelper.AutoCopy<T_DigitRransducerOutSlot, T1_DigitRransducerOutSlot>(p)));
            }
            else
            {
                string error = "服务器" + ip + " " + DateTime.Now.ToString() + "查询错误:" + digitRransducerOutSlotTask.Result.ErrorType + "-" + digitRransducerOutSlotTask.Result.ErrorMessage;
                EventAggregatorService.Instance.EventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("数据库操作", new Exception(error)));
            }
            if (analogRransducerOutSlotTask.Result.IsOK)
            {
                rootCard.T_AnalogRransducerOutSlot.Clear();
                analogRransducerOutSlotTask.Result.ResponseItem.ForEach(p => rootCard.T_AnalogRransducerOutSlot.Add(ClassCopyHelper.AutoCopy<T_AnalogRransducerOutSlot, T1_AnalogRransducerOutSlot>(p)));
            }
            else
            {
                string error = "服务器" + ip + " " + DateTime.Now.ToString() + "查询错误:" + analogRransducerOutSlotTask.Result.ErrorType + "-" + analogRransducerOutSlotTask.Result.ErrorMessage;
                EventAggregatorService.Instance.EventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("数据库操作", new Exception(error)));
            }
            if (wirelessScalarSlotTask.Result.IsOK)
            {
                rootCard.T_WirelessScalarSlot.Clear();
                wirelessScalarSlotTask.Result.ResponseItem.ForEach(p => rootCard.T_WirelessScalarSlot.Add(ClassCopyHelper.AutoCopy<T_WirelessScalarSlot, T1_WirelessScalarSlot>(p)));
            }
            else
            {
                string error = "服务器" + ip + " " + DateTime.Now.ToString() + "查询错误:" + wirelessScalarSlotTask.Result.ErrorType + "-" + wirelessScalarSlotTask.Result.ErrorMessage;
                EventAggregatorService.Instance.EventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("数据库操作", new Exception(error)));
            }
            if (wirelessVibrationSlotTask.Result.IsOK)
            {
                rootCard.T_WirelessVibrationSlot.Clear();
                wirelessVibrationSlotTask.Result.ResponseItem.ForEach(p => rootCard.T_WirelessVibrationSlot.Add(ClassCopyHelper.AutoCopy<T_WirelessVibrationSlot, T1_WirelessVibrationSlot>(p)));
            }
            else
            {
                string error = "服务器" + ip + " " + DateTime.Now.ToString() + "查询错误:" + wirelessVibrationSlotTask.Result.ErrorType + "-" + wirelessVibrationSlotTask.Result.ErrorMessage;
                EventAggregatorService.Instance.EventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("数据库操作", new Exception(error)));
            }
            if (divFreInfoTask.Result.IsOK)
            {
                rootCard.T_DivFreInfo.Clear();
                divFreInfoTask.Result.ResponseItem.ForEach(p => rootCard.T_DivFreInfo.Add(ClassCopyHelper.AutoCopy<T_DivFreInfo, T1_DivFreInfo>(p)));
            }
            else
            {
                string error = "服务器" + ip + " " + DateTime.Now.ToString() + "查询错误:" + divFreInfoTask.Result.ErrorType + "-" + divFreInfoTask.Result.ErrorMessage;
                EventAggregatorService.Instance.EventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("数据库操作", new Exception(error)));
            }
        }

        private async Task<bool> AddHardwareTables(string ip, DataProvider client, T1_RootCard rootCard)
        {
            Dictionary<string, ICollection<object>> addDic = new Dictionary<string, ICollection<object>>();
            addDic.Add("T_MainControlCard", rootCard.T_MainControlCard.Select(p => p as object).ToList());
            addDic.Add("T_WireMatchingCard", rootCard.T_WireMatchingCard.Select(p => p as object).ToList());
            addDic.Add("T_WirelessReceiveCard", rootCard.T_WirelessReceiveCard.Select(p => p as object).ToList());
            addDic.Add("T_TransmissionCard", rootCard.T_TransmissionCard.Select(p => p as object).ToList());
            addDic.Add("T_AbstractChannelInfo", rootCard.T_AbstractChannelInfo.Select(p => p as object).ToList());
            addDic.Add("T_IEPEChannelInfo", rootCard.T_IEPEChannelInfo.Select(p => p as object).ToList());
            addDic.Add("T_EddyCurrentDisplacementChannelInfo", rootCard.T_EddyCurrentDisplacementChannelInfo.Select(p => p as object).ToList());
            addDic.Add("T_EddyCurrentKeyPhaseChannelInfo", rootCard.T_EddyCurrentKeyPhaseChannelInfo.Select(p => p as object).ToList());
            addDic.Add("T_EddyCurrentTachometerChannelInfo", rootCard.T_EddyCurrentTachometerChannelInfo.Select(p => p as object).ToList());
            addDic.Add("T_DigitTachometerChannelInfo", rootCard.T_DigitTachometerChannelInfo.Select(p => p as object).ToList());
            addDic.Add("T_AnalogRransducerInChannelInfo", rootCard.T_AnalogRransducerInChannelInfo.Select(p => p as object).ToList());
            addDic.Add("T_RelayChannelInfo", rootCard.T_RelayChannelInfo.Select(p => p as object).ToList());
            addDic.Add("T_DigitRransducerInChannelInfo", rootCard.T_DigitRransducerInChannelInfo.Select(p => p as object).ToList());
            addDic.Add("T_DigitRransducerOutChannelInfo", rootCard.T_DigitRransducerOutChannelInfo.Select(p => p as object).ToList());
            addDic.Add("T_AnalogRransducerOutChannelInfo", rootCard.T_AnalogRransducerOutChannelInfo.Select(p => p as object).ToList());
            addDic.Add("T_WirelessScalarChannelInfo", rootCard.T_WirelessScalarChannelInfo.Select(p => p as object).ToList());
            addDic.Add("T_WirelessVibrationChannelInfo", rootCard.T_WirelessVibrationChannelInfo.Select(p => p as object).ToList());
            addDic.Add("T_AbstractSlotInfo", rootCard.T_AbstractSlotInfo.Select(p => p as object).ToList());
            addDic.Add("T_IEPESlot", rootCard.T_IEPESlot.Select(p => p as object).ToList());
            addDic.Add("T_EddyCurrentDisplacementSlot", rootCard.T_EddyCurrentDisplacementSlot.Select(p => p as object).ToList());
            addDic.Add("T_EddyCurrentKeyPhaseSlot", rootCard.T_EddyCurrentKeyPhaseSlot.Select(p => p as object).ToList());
            addDic.Add("T_EddyCurrentTachometerSlot", rootCard.T_EddyCurrentTachometerSlot.Select(p => p as object).ToList());
            addDic.Add("T_DigitTachometerSlot", rootCard.T_DigitTachometerSlot.Select(p => p as object).ToList());
            addDic.Add("T_AnalogRransducerInSlot", rootCard.T_AnalogRransducerInSlot.Select(p => p as object).ToList());
            addDic.Add("T_RelaySlot", rootCard.T_RelaySlot.Select(p => p as object).ToList());
            addDic.Add("T_DigitRransducerInSlot", rootCard.T_DigitRransducerInSlot.Select(p => p as object).ToList());
            addDic.Add("T_DigitRransducerOutSlot", rootCard.T_DigitRransducerOutSlot.Select(p => p as object).ToList());
            addDic.Add("T_AnalogRransducerOutSlot", rootCard.T_AnalogRransducerOutSlot.Select(p => p as object).ToList());
            addDic.Add("T_WirelessScalarSlot", rootCard.T_WirelessScalarSlot.Select(p => p as object).ToList());
            addDic.Add("T_WirelessVibrationSlot", rootCard.T_WirelessVibrationSlot.Select(p => p as object).ToList());
            addDic.Add("T_DivFreInfo", rootCard.T_DivFreInfo.Select(p => p as object).ToList());
            return await Task.Run(() =>
            {              
                var complexResult = client.Complex(addDic, null, null);
                //先判断是不是OK          
                if (complexResult.IsOK)
                {
                    //添加数据                    
                    if (rootCard.T_MainControlCard != null)
                    {                       
                        var ids = complexResult.ResponseItem["T_MainControlCard"].ToList(); 
                        for (int i = 0; i < ids.Count(); i++)
                        {                           
                            add(ip, rootCard.T_MainControlCard[i], ids[i]);
                        }                        
                    }
                    if (rootCard.T_WireMatchingCard != null)
                    {
                        var ids = complexResult.ResponseItem["T_WireMatchingCard"].ToList();
                        for (int i = 0; i < ids.Count(); i++)
                        {
                            add(ip, rootCard.T_WireMatchingCard[i], ids[i]);
                        }
                    }
                    if (rootCard.T_WirelessReceiveCard != null)
                    {
                        var ids = complexResult.ResponseItem["T_WirelessReceiveCard"].ToList();
                        for (int i = 0; i < ids.Count(); i++)
                        {
                            add(ip, rootCard.T_WirelessReceiveCard[i], ids[i]);
                        }
                    }
                    if (rootCard.T_TransmissionCard != null)
                    {
                        var ids = complexResult.ResponseItem["T_TransmissionCard"].ToList();
                        for (int i = 0; i < ids.Count(); i++)
                        {
                            add(ip, rootCard.T_TransmissionCard[i], ids[i]);
                        }
                    }
                    if (rootCard.T_AbstractChannelInfo != null)
                    {
                        var ids = complexResult.ResponseItem["T_AbstractChannelInfo"].ToList();
                        for (int i = 0; i < ids.Count(); i++)
                        {
                            add(ip, rootCard.T_AbstractChannelInfo[i], ids[i]);
                        }
                    }
                    if (rootCard.T_IEPEChannelInfo != null)
                    {
                        var ids = complexResult.ResponseItem["T_IEPEChannelInfo"].ToList();
                        for (int i = 0; i < ids.Count(); i++)
                        {
                            add(ip, rootCard.T_IEPEChannelInfo[i], ids[i]);
                        }
                    }
                    if (rootCard.T_EddyCurrentDisplacementChannelInfo != null)
                    {
                        var ids = complexResult.ResponseItem["T_EddyCurrentDisplacementChannelInfo"].ToList();
                        for (int i = 0; i < ids.Count(); i++)
                        {
                            add(ip, rootCard.T_EddyCurrentDisplacementChannelInfo[i], ids[i]);
                        }
                    }
                    if (rootCard.T_EddyCurrentKeyPhaseChannelInfo != null)
                    {
                        var ids = complexResult.ResponseItem["T_EddyCurrentKeyPhaseChannelInfo"].ToList();
                        for (int i = 0; i < ids.Count(); i++)
                        {
                            add(ip, rootCard.T_EddyCurrentKeyPhaseChannelInfo[i], ids[i]);
                        }
                    }
                    if (rootCard.T_EddyCurrentTachometerChannelInfo != null)
                    {
                        var ids = complexResult.ResponseItem["T_EddyCurrentTachometerChannelInfo"].ToList();
                        for (int i = 0; i < ids.Count(); i++)
                        {
                            add(ip, rootCard.T_EddyCurrentTachometerChannelInfo[i], ids[i]);
                        }
                    }
                    if (rootCard.T_DigitTachometerChannelInfo != null)
                    {
                        var ids = complexResult.ResponseItem["T_DigitTachometerChannelInfo"].ToList();
                        for (int i = 0; i < ids.Count(); i++)
                        {
                            add(ip, rootCard.T_DigitTachometerChannelInfo[i], ids[i]);
                        }
                    }
                    if (rootCard.T_AnalogRransducerInChannelInfo != null)
                    {
                        var ids = complexResult.ResponseItem["T_AnalogRransducerInChannelInfo"].ToList();
                        for (int i = 0; i < ids.Count(); i++)
                        {
                            add(ip, rootCard.T_AnalogRransducerInChannelInfo[i], ids[i]);
                        }
                    }
                    if (rootCard.T_RelayChannelInfo != null)
                    {
                        var ids = complexResult.ResponseItem["T_RelayChannelInfo"].ToList();
                        for (int i = 0; i < ids.Count(); i++)
                        {
                            add(ip, rootCard.T_RelayChannelInfo[i], ids[i]);
                        }
                    }
                    if (rootCard.T_DigitRransducerInChannelInfo != null)
                    {
                        var ids = complexResult.ResponseItem["T_DigitRransducerInChannelInfo"].ToList();
                        for (int i = 0; i < ids.Count(); i++)
                        {
                            add(ip, rootCard.T_DigitRransducerInChannelInfo[i], ids[i]);
                        }
                    }
                    if (rootCard.T_DigitRransducerOutChannelInfo != null)
                    {
                        var ids = complexResult.ResponseItem["T_DigitRransducerOutChannelInfo"].ToList();
                        for (int i = 0; i < ids.Count(); i++)
                        {
                            add(ip, rootCard.T_DigitRransducerOutChannelInfo[i], ids[i]);
                        }
                    }
                    if (rootCard.T_AnalogRransducerOutChannelInfo != null)
                    {
                        var ids = complexResult.ResponseItem["T_AnalogRransducerOutChannelInfo"].ToList();
                        for (int i = 0; i < ids.Count(); i++)
                        {
                            add(ip, rootCard.T_AnalogRransducerOutChannelInfo[i], ids[i]);
                        }
                    }
                    if (rootCard.T_WirelessScalarChannelInfo != null)
                    {
                        var ids = complexResult.ResponseItem["T_WirelessScalarChannelInfo"].ToList();
                        for (int i = 0; i < ids.Count(); i++)
                        {
                            add(ip, rootCard.T_WirelessScalarChannelInfo[i], ids[i]);
                        }
                    }
                    if (rootCard.T_WirelessVibrationChannelInfo != null)
                    {
                        var ids = complexResult.ResponseItem["T_WirelessVibrationChannelInfo"].ToList();
                        for (int i = 0; i < ids.Count(); i++)
                        {
                            add(ip, rootCard.T_WirelessVibrationChannelInfo[i], ids[i]);
                        }
                    }
                    if (rootCard.T_AbstractSlotInfo != null)
                    {
                        var ids = complexResult.ResponseItem["T_AbstractSlotInfo"].ToList();
                        for (int i = 0; i < ids.Count(); i++)
                        {
                            add(ip, rootCard.T_AbstractSlotInfo[i], ids[i]);
                        }
                    }
                    if (rootCard.T_IEPESlot != null)
                    {
                        var ids = complexResult.ResponseItem["T_IEPESlot"].ToList();
                        for (int i = 0; i < ids.Count(); i++)
                        {
                            add(ip, rootCard.T_IEPESlot[i], ids[i]);
                        }
                    }
                    if (rootCard.T_EddyCurrentDisplacementSlot != null)
                    {
                        var ids = complexResult.ResponseItem["T_EddyCurrentDisplacementSlot"].ToList();
                        for (int i = 0; i < ids.Count(); i++)
                        {
                            add(ip, rootCard.T_EddyCurrentDisplacementSlot[i], ids[i]);
                        }
                    }
                    if (rootCard.T_EddyCurrentKeyPhaseSlot != null)
                    {
                        var ids = complexResult.ResponseItem["T_EddyCurrentKeyPhaseSlot"].ToList();
                        for (int i = 0; i < ids.Count(); i++)
                        {
                            add(ip, rootCard.T_EddyCurrentKeyPhaseSlot[i], ids[i]);
                        }
                    }
                    if (rootCard.T_EddyCurrentTachometerSlot != null)
                    {
                        var ids = complexResult.ResponseItem["T_EddyCurrentTachometerSlot"].ToList();
                        for (int i = 0; i < ids.Count(); i++)
                        {
                            add(ip, rootCard.T_EddyCurrentTachometerSlot[i], ids[i]);
                        }
                    }
                    if (rootCard.T_DigitTachometerSlot != null)
                    {
                        var ids = complexResult.ResponseItem["T_DigitTachometerSlot"].ToList();
                        for (int i = 0; i < ids.Count(); i++)
                        {
                            add(ip, rootCard.T_DigitTachometerSlot[i], ids[i]);
                        }
                    }
                    if (rootCard.T_AnalogRransducerInSlot != null)
                    {
                        var ids = complexResult.ResponseItem["T_AnalogRransducerInSlot"].ToList();
                        for (int i = 0; i < ids.Count(); i++)
                        {
                            add(ip, rootCard.T_AnalogRransducerInSlot[i], ids[i]);
                        }
                    }
                    if (rootCard.T_RelaySlot != null)
                    {
                        var ids = complexResult.ResponseItem["T_RelaySlot"].ToList();
                        for (int i = 0; i < ids.Count(); i++)
                        {
                            add(ip, rootCard.T_RelaySlot[i], ids[i]);
                        }
                    }
                    if (rootCard.T_DigitRransducerInSlot != null)
                    {
                        var ids = complexResult.ResponseItem["T_DigitRransducerInSlot"].ToList();
                        for (int i = 0; i < ids.Count(); i++)
                        {
                            add(ip, rootCard.T_DigitRransducerInSlot[i], ids[i]);
                        }
                    }
                    if (rootCard.T_DigitRransducerOutSlot != null)
                    {
                        var ids = complexResult.ResponseItem["T_DigitRransducerOutSlot"].ToList();
                        for (int i = 0; i < ids.Count(); i++)
                        {
                            add(ip, rootCard.T_DigitRransducerOutSlot[i], ids[i]);
                        }
                    }
                    if (rootCard.T_AnalogRransducerOutSlot != null)
                    {
                        var ids = complexResult.ResponseItem["T_AnalogRransducerOutSlot"].ToList();
                        for (int i = 0; i < ids.Count(); i++)
                        {
                            add(ip, rootCard.T_AnalogRransducerOutSlot[i], ids[i]);
                        }
                    }
                    if (rootCard.T_WirelessScalarSlot != null)
                    {
                        var ids = complexResult.ResponseItem["T_WirelessScalarSlot"].ToList();
                        for (int i = 0; i < ids.Count(); i++)
                        {
                            add(ip, rootCard.T_WirelessScalarSlot[i], ids[i]);
                        }
                    }
                    if (rootCard.T_WirelessVibrationSlot != null)
                    {
                        var ids = complexResult.ResponseItem["T_WirelessVibrationSlot"].ToList();
                        for (int i = 0; i < ids.Count(); i++)
                        {
                            add(ip, rootCard.T_WirelessVibrationSlot[i], ids[i]);
                        }
                    }
                    if (rootCard.T_DivFreInfo != null)
                    {
                        var ids = complexResult.ResponseItem["T_DivFreInfo"].ToList();
                        for (int i = 0; i < ids.Count(); i++)
                        {
                            add(ip, rootCard.T_DivFreInfo[i], ids[i]);
                        }
                    }
                    return true;              
                }
                else
                {
                    string error = "服务器" + ip + " " + DateTime.Now.ToString() + "添加错误:" + complexResult.ErrorType + "-" + complexResult.ErrorMessage;
                    //ErrorMessage是错误信息
                    EventAggregatorService.Instance.EventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("数据库操作", new Exception(error)));
                    return false;
                }
            });

            #region
            /*
            var mainControlCardTask = Task.Run(() => client.Add<T_MainControlCard>(rootCard.T_MainControlCard.Select(p => p as T_MainControlCard).ToList()));
                var wireMatchingCardTask = Task.Run(() => client.Add<T_WireMatchingCard>(rootCard.T_WireMatchingCard.Select(p => p as T_WireMatchingCard).ToList()));
                var wirelessReceiveCardTask = Task.Run(() => client.Add<T_WirelessReceiveCard>(rootCard.T_WirelessReceiveCard.Select(p => p as T_WirelessReceiveCard).ToList()));
                var transmissionCardTask = Task.Run(() => client.Add<T_TransmissionCard>(rootCard.T_TransmissionCard.Select(p => p as T_TransmissionCard).ToList()));
                var abstractChannelInfoTask = Task.Run(() => client.Add<T_AbstractChannelInfo>(rootCard.T_AbstractChannelInfo.Select(p => p as T_AbstractChannelInfo).ToList()));
                var iEPEChannelInfoTask = Task.Run(() => client.Add<T_IEPEChannelInfo>(rootCard.T_IEPEChannelInfo.Select(p => p as T_IEPEChannelInfo).ToList()));
                var eddyCurrentDisplacementChannelInfoTask = Task.Run(() => client.Add<T_EddyCurrentDisplacementChannelInfo>(rootCard.T_EddyCurrentDisplacementChannelInfo.Select(p => p as T_EddyCurrentDisplacementChannelInfo).ToList()));
                var eddyCurrentKeyPhaseChannelInfoTask = Task.Run(() => client.Add<T_EddyCurrentKeyPhaseChannelInfo>(rootCard.T_EddyCurrentKeyPhaseChannelInfo.Select(p => p as T_EddyCurrentKeyPhaseChannelInfo).ToList()));
                var eddyCurrentTachometerChannelInfoTask = Task.Run(() => client.Add<T_EddyCurrentTachometerChannelInfo>(rootCard.T_EddyCurrentTachometerChannelInfo.Select(p => p as T_EddyCurrentTachometerChannelInfo).ToList()));
                var digitTachometerChannelInfoTask = Task.Run(() => client.Add<T_DigitTachometerChannelInfo>(rootCard.T_DigitTachometerChannelInfo.Select(p => p as T_DigitTachometerChannelInfo).ToList()));
                var analogRransducerInChannelInfoTask = Task.Run(() => client.Add<T_AnalogRransducerInChannelInfo>(rootCard.T_AnalogRransducerInChannelInfo.Select(p => p as T_AnalogRransducerInChannelInfo).ToList()));
                var relayChannelInfoTask = Task.Run(() => client.Add<T_RelayChannelInfo>(rootCard.T_RelayChannelInfo.Select(p => p as T_RelayChannelInfo).ToList()));
                var digitRransducerInChannelInfoTask = Task.Run(() => client.Add<T_DigitRransducerInChannelInfo>(rootCard.T_DigitRransducerInChannelInfo.Select(p => p as T_DigitRransducerInChannelInfo).ToList()));
                var digitRransducerOutChannelInfoTask = Task.Run(() => client.Add<T_DigitRransducerOutChannelInfo>(rootCard.T_DigitRransducerOutChannelInfo.Select(p => p as T_DigitRransducerOutChannelInfo).ToList()));
                var analogRransducerOutChannelInfoTask = Task.Run(() => client.Add<T_AnalogRransducerOutChannelInfo>(rootCard.T_AnalogRransducerOutChannelInfo.Select(p => p as T_AnalogRransducerOutChannelInfo).ToList()));
                var wirelessScalarChannelInfoTask = Task.Run(() => client.Add<T_WirelessScalarChannelInfo>(rootCard.T_WirelessScalarChannelInfo.Select(p => p as T_WirelessScalarChannelInfo).ToList()));
                var wirelessVibrationChannelInfoTask = Task.Run(() => client.Add<T_WirelessVibrationChannelInfo>(rootCard.T_WirelessVibrationChannelInfo.Select(p => p as T_WirelessVibrationChannelInfo).ToList()));
                var abstractSlotInfoTask = Task.Run(() => client.Add<T_AbstractSlotInfo>(rootCard.T_AbstractSlotInfo.Select(p => p as T_AbstractSlotInfo).ToList()));
                var iEPESlotTask = Task.Run(() => client.Add<T_IEPESlot>(rootCard.T_IEPESlot.Select(p => p as T_IEPESlot).ToList()));
                var eddyCurrentDisplacementSlotTask = Task.Run(() => client.Add<T_EddyCurrentDisplacementSlot>(rootCard.T_EddyCurrentDisplacementSlot.Select(p => p as T_EddyCurrentDisplacementSlot).ToList()));
                var eddyCurrentKeyPhaseSlotTask = Task.Run(() => client.Add<T_EddyCurrentKeyPhaseSlot>(rootCard.T_EddyCurrentKeyPhaseSlot.Select(p => p as T_EddyCurrentKeyPhaseSlot).ToList()));
                var eddyCurrentTachometerSlotTask = Task.Run(() => client.Add<T_EddyCurrentTachometerSlot>(rootCard.T_EddyCurrentTachometerSlot.Select(p => p as T_EddyCurrentTachometerSlot).ToList()));
                var digitTachometerSlotTask = Task.Run(() => client.Add<T_DigitTachometerSlot>(rootCard.T_DigitTachometerSlot.Select(p => p as T_DigitTachometerSlot).ToList()));
                var analogRransducerInSlotTask = Task.Run(() => client.Add<T_AnalogRransducerInSlot>(rootCard.T_AnalogRransducerInSlot.Select(p => p as T_AnalogRransducerInSlot).ToList()));
                var relaySlotTask = Task.Run(() => client.Add<T_RelaySlot>(rootCard.T_RelaySlot.Select(p => p as T_RelaySlot).ToList()));
                var digitRransducerInSlotTask = Task.Run(() => client.Add<T_DigitRransducerInSlot>(rootCard.T_DigitRransducerInSlot.Select(p => p as T_DigitRransducerInSlot).ToList()));
                var digitRransducerOutSlotTask = Task.Run(() => client.Add<T_DigitRransducerOutSlot>(rootCard.T_DigitRransducerOutSlot.Select(p => p as T_DigitRransducerOutSlot).ToList()));
                var analogRransducerOutSlotTask = Task.Run(() => client.Add<T_AnalogRransducerOutSlot>(rootCard.T_AnalogRransducerOutSlot.Select(p => p as T_AnalogRransducerOutSlot).ToList()));
                var wirelessScalarSlotTask = Task.Run(() => client.Add<T_WirelessScalarSlot>(rootCard.T_WirelessScalarSlot.Select(p => p as T_WirelessScalarSlot).ToList()));
                var wirelessVibrationSlotTask = Task.Run(() => client.Add<T_WirelessVibrationSlot>(rootCard.T_WirelessVibrationSlot.Select(p => p as T_WirelessVibrationSlot).ToList()));
                var divFreInfoTask = Task.Run(() => client.Add<T_DivFreInfo>(rootCard.T_DivFreInfo.Select(p => p as T_DivFreInfo).ToList()));

                await Task.WhenAll(mainControlCardTask, wireMatchingCardTask,
                    wirelessReceiveCardTask, transmissionCardTask,
                    abstractChannelInfoTask, iEPEChannelInfoTask,
                    eddyCurrentDisplacementChannelInfoTask, eddyCurrentKeyPhaseChannelInfoTask,
                    eddyCurrentTachometerChannelInfoTask, digitTachometerChannelInfoTask,
                    analogRransducerInChannelInfoTask, relayChannelInfoTask,
                    digitRransducerInChannelInfoTask, digitRransducerOutChannelInfoTask,
                    analogRransducerOutChannelInfoTask, wirelessScalarChannelInfoTask,
                    wirelessVibrationChannelInfoTask, abstractSlotInfoTask,
                    iEPESlotTask, eddyCurrentDisplacementSlotTask,
                    eddyCurrentKeyPhaseSlotTask, eddyCurrentTachometerSlotTask,
                    digitTachometerSlotTask, analogRransducerInSlotTask,
                    relaySlotTask, digitRransducerInSlotTask,
                    digitRransducerOutSlotTask, analogRransducerOutSlotTask,
                    wirelessScalarSlotTask, wirelessVibrationSlotTask,
                    divFreInfoTask);

                if (mainControlCardTask.Result.IsOK)
                {
                    for (int i = 0; i < mainControlCardTask.Result.ResponseItem.Count(); i++)
                    {
                        rootCard.T_MainControlCard[i].id = mainControlCardTask.Result.ResponseItem[i];
                    }
                    T_RootCard[ip].T_MainControlCard.AddRange(rootCard.T_MainControlCard);                
                }
                else
                {
                    string error = DateTime.Now.ToString() + "添加错误:" + mainControlCardTask.Result.ErrorMessage;
                    EventAggregatorService.Instance.EventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("数据库操作", new Exception(error)));
                }
                if (wireMatchingCardTask.Result.IsOK)
                {
                    for (int i = 0; i < wireMatchingCardTask.Result.ResponseItem.Count(); i++)
                    {
                        rootCard.T_WireMatchingCard[i].id = wireMatchingCardTask.Result.ResponseItem[i];
                    }
                    T_RootCard[ip].T_WireMatchingCard.AddRange(rootCard.T_WireMatchingCard);             
                }
                else
                {
                    string error = DateTime.Now.ToString() + "添加错误:" + wireMatchingCardTask.Result.ErrorMessage;
                    EventAggregatorService.Instance.EventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("数据库操作", new Exception(error)));
                }
                if (wirelessReceiveCardTask.Result.IsOK)
                {
                    for (int i = 0; i < wirelessReceiveCardTask.Result.ResponseItem.Count(); i++)
                    {
                        rootCard.T_WirelessReceiveCard[i].id = wirelessReceiveCardTask.Result.ResponseItem[i];
                    }
                    T_RootCard[ip].T_WirelessReceiveCard.AddRange(rootCard.T_WirelessReceiveCard);             
                }
                else
                {
                    string error = DateTime.Now.ToString() + "添加错误:" + wirelessReceiveCardTask.Result.ErrorMessage;
                    EventAggregatorService.Instance.EventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("数据库操作", new Exception(error)));
                }
                if (transmissionCardTask.Result.IsOK)
                {
                    for (int i = 0; i < transmissionCardTask.Result.ResponseItem.Count(); i++)
                    {
                        rootCard.T_TransmissionCard[i].id = transmissionCardTask.Result.ResponseItem[i];
                    }
                    T_RootCard[ip].T_TransmissionCard.AddRange(rootCard.T_TransmissionCard);           
                }
                else
                {
                    string error = DateTime.Now.ToString() + "添加错误:" + transmissionCardTask.Result.ErrorMessage;
                    EventAggregatorService.Instance.EventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("数据库操作", new Exception(error)));
                }
                if (abstractChannelInfoTask.Result.IsOK)
                {
                    for (int i = 0; i < abstractChannelInfoTask.Result.ResponseItem.Count(); i++)
                    {
                        rootCard.T_AbstractChannelInfo[i].id = abstractChannelInfoTask.Result.ResponseItem[i];
                    }
                    T_RootCard[ip].T_AbstractChannelInfo.AddRange(rootCard.T_AbstractChannelInfo);              
                }
                else
                {
                    string error = DateTime.Now.ToString() + "添加错误:" + abstractChannelInfoTask.Result.ErrorMessage;
                    EventAggregatorService.Instance.EventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("数据库操作", new Exception(error)));
                }
                if (iEPEChannelInfoTask.Result.IsOK)
                {
                    for (int i = 0; i < iEPEChannelInfoTask.Result.ResponseItem.Count(); i++)
                    {
                        rootCard.T_IEPEChannelInfo[i].id = iEPEChannelInfoTask.Result.ResponseItem[i];
                    }
                    T_RootCard[ip].T_IEPEChannelInfo.AddRange(rootCard.T_IEPEChannelInfo);               
                }
                else
                {
                    string error = DateTime.Now.ToString() + "添加错误:" + iEPEChannelInfoTask.Result.ErrorMessage;
                    EventAggregatorService.Instance.EventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("数据库操作", new Exception(error)));
                }
                if (eddyCurrentDisplacementChannelInfoTask.Result.IsOK)
                {
                    for (int i = 0; i < eddyCurrentDisplacementChannelInfoTask.Result.ResponseItem.Count(); i++)
                    {
                        rootCard.T_EddyCurrentDisplacementChannelInfo[i].id = eddyCurrentDisplacementChannelInfoTask.Result.ResponseItem[i];
                    }
                    T_RootCard[ip].T_EddyCurrentDisplacementChannelInfo.AddRange(rootCard.T_EddyCurrentDisplacementChannelInfo);
                }
                else
                {
                    string error = DateTime.Now.ToString() + "添加错误:" + eddyCurrentDisplacementChannelInfoTask.Result.ErrorMessage;
                    EventAggregatorService.Instance.EventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("数据库操作", new Exception(error)));
                }
                if (eddyCurrentKeyPhaseChannelInfoTask.Result.IsOK)
                {
                    for (int i = 0; i < eddyCurrentKeyPhaseChannelInfoTask.Result.ResponseItem.Count(); i++)
                    {
                        rootCard.T_EddyCurrentKeyPhaseChannelInfo[i].id = eddyCurrentKeyPhaseChannelInfoTask.Result.ResponseItem[i];
                    }
                    T_RootCard[ip].T_EddyCurrentKeyPhaseChannelInfo.AddRange(rootCard.T_EddyCurrentKeyPhaseChannelInfo);
                }
                else
                {
                    string error = DateTime.Now.ToString() + "添加错误:" + eddyCurrentKeyPhaseChannelInfoTask.Result.ErrorMessage;
                    EventAggregatorService.Instance.EventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("数据库操作", new Exception(error)));
                }
                if (eddyCurrentTachometerChannelInfoTask.Result.IsOK)
                {
                    for (int i = 0; i < eddyCurrentTachometerChannelInfoTask.Result.ResponseItem.Count(); i++)
                    {
                        rootCard.T_EddyCurrentTachometerChannelInfo[i].id = eddyCurrentTachometerChannelInfoTask.Result.ResponseItem[i];
                    }
                    T_RootCard[ip].T_EddyCurrentTachometerChannelInfo.AddRange(rootCard.T_EddyCurrentTachometerChannelInfo);
                }
                else
                {
                    string error = DateTime.Now.ToString() + "添加错误:" + eddyCurrentTachometerChannelInfoTask.Result.ErrorMessage;
                    EventAggregatorService.Instance.EventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("数据库操作", new Exception(error)));
                }
                if (digitTachometerChannelInfoTask.Result.IsOK)
                {
                    for (int i = 0; i < digitTachometerChannelInfoTask.Result.ResponseItem.Count(); i++)
                    {
                        rootCard.T_DigitTachometerChannelInfo[i].id = digitTachometerChannelInfoTask.Result.ResponseItem[i];
                    }
                    T_RootCard[ip].T_DigitTachometerChannelInfo.AddRange(rootCard.T_DigitTachometerChannelInfo);             
                }
                else
                {
                    string error = DateTime.Now.ToString() + "添加错误:" + digitTachometerChannelInfoTask.Result.ErrorMessage;
                    EventAggregatorService.Instance.EventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("数据库操作", new Exception(error)));
                }
                if (analogRransducerInChannelInfoTask.Result.IsOK)
                {
                    for (int i = 0; i < analogRransducerInChannelInfoTask.Result.ResponseItem.Count(); i++)
                    {
                        rootCard.T_AnalogRransducerInChannelInfo[i].id = analogRransducerInChannelInfoTask.Result.ResponseItem[i];
                    }
                    T_RootCard[ip].T_AnalogRransducerInChannelInfo.AddRange(rootCard.T_AnalogRransducerInChannelInfo);
                }
                else
                {
                    string error = DateTime.Now.ToString() + "添加错误:" + analogRransducerInChannelInfoTask.Result.ErrorMessage;
                    EventAggregatorService.Instance.EventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("数据库操作", new Exception(error)));
                }
                if (relayChannelInfoTask.Result.IsOK)
                {
                    for (int i = 0; i < relayChannelInfoTask.Result.ResponseItem.Count(); i++)
                    {
                        rootCard.T_RelayChannelInfo[i].id = relayChannelInfoTask.Result.ResponseItem[i];
                    }
                    T_RootCard[ip].T_RelayChannelInfo.AddRange(rootCard.T_RelayChannelInfo);      
                }
                else
                {
                    string error = DateTime.Now.ToString() + "添加错误:" + relayChannelInfoTask.Result.ErrorMessage;
                    EventAggregatorService.Instance.EventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("数据库操作", new Exception(error)));
                }
                if (digitRransducerInChannelInfoTask.Result.IsOK)
                {
                    for (int i = 0; i < digitRransducerInChannelInfoTask.Result.ResponseItem.Count(); i++)
                    {
                        rootCard.T_DigitRransducerInChannelInfo[i].id = digitRransducerInChannelInfoTask.Result.ResponseItem[i];
                    }
                    T_RootCard[ip].T_DigitRransducerInChannelInfo.AddRange(rootCard.T_DigitRransducerInChannelInfo);
                }
                else
                {
                    string error = DateTime.Now.ToString() + "添加错误:" + digitRransducerInChannelInfoTask.Result.ErrorMessage;
                    EventAggregatorService.Instance.EventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("数据库操作", new Exception(error)));
                }
                if (digitRransducerOutChannelInfoTask.Result.IsOK)
                {
                    for (int i = 0; i < digitRransducerOutChannelInfoTask.Result.ResponseItem.Count(); i++)
                    {
                        rootCard.T_DigitRransducerOutChannelInfo[i].id = digitRransducerOutChannelInfoTask.Result.ResponseItem[i];
                    }
                    T_RootCard[ip].T_DigitRransducerOutChannelInfo.AddRange(rootCard.T_DigitRransducerOutChannelInfo);
                }
                else
                {
                    string error = DateTime.Now.ToString() + "添加错误:" + digitRransducerOutChannelInfoTask.Result.ErrorMessage;
                    EventAggregatorService.Instance.EventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("数据库操作", new Exception(error)));
                }
                if (analogRransducerOutChannelInfoTask.Result.IsOK)
                {
                    for (int i = 0; i < analogRransducerOutChannelInfoTask.Result.ResponseItem.Count(); i++)
                    {
                        rootCard.T_AnalogRransducerOutChannelInfo[i].id = analogRransducerOutChannelInfoTask.Result.ResponseItem[i];
                    }
                    T_RootCard[ip].T_AnalogRransducerOutChannelInfo.AddRange(rootCard.T_AnalogRransducerOutChannelInfo);
                }
                else
                {
                    string error = DateTime.Now.ToString() + "添加错误:" + analogRransducerOutChannelInfoTask.Result.ErrorMessage;
                    EventAggregatorService.Instance.EventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("数据库操作", new Exception(error)));
                }
                if (wirelessScalarChannelInfoTask.Result.IsOK)
                {
                    for (int i = 0; i < wirelessScalarChannelInfoTask.Result.ResponseItem.Count(); i++)
                    {
                        rootCard.T_WirelessScalarChannelInfo[i].id = wirelessScalarChannelInfoTask.Result.ResponseItem[i];
                    }
                    T_RootCard[ip].T_WirelessScalarChannelInfo.AddRange(rootCard.T_WirelessScalarChannelInfo);                
                }
                else
                {
                    string error = DateTime.Now.ToString() + "添加错误:" + wirelessScalarChannelInfoTask.Result.ErrorMessage;
                    EventAggregatorService.Instance.EventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("数据库操作", new Exception(error)));
                }
                if (wirelessVibrationChannelInfoTask.Result.IsOK)
                {
                    for (int i = 0; i < wirelessVibrationChannelInfoTask.Result.ResponseItem.Count(); i++)
                    {
                        rootCard.T_WirelessVibrationChannelInfo[i].id = wirelessVibrationChannelInfoTask.Result.ResponseItem[i];
                    }
                    T_RootCard[ip].T_WirelessVibrationChannelInfo.AddRange(rootCard.T_WirelessVibrationChannelInfo);
                }
                else
                {
                    string error = DateTime.Now.ToString() + "添加错误:" + wirelessVibrationChannelInfoTask.Result.ErrorMessage;
                    EventAggregatorService.Instance.EventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("数据库操作", new Exception(error)));
                }
                if (abstractSlotInfoTask.Result.IsOK)
                {
                    for (int i = 0; i < abstractSlotInfoTask.Result.ResponseItem.Count(); i++)
                    {
                        rootCard.T_AbstractSlotInfo[i].id = abstractSlotInfoTask.Result.ResponseItem[i];
                    }
                    T_RootCard[ip].T_AbstractSlotInfo.AddRange(rootCard.T_AbstractSlotInfo);            
                }
                else
                {
                    string error = DateTime.Now.ToString() + "添加错误:" + abstractSlotInfoTask.Result.ErrorMessage;
                    EventAggregatorService.Instance.EventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("数据库操作", new Exception(error)));
                }
                if (iEPESlotTask.Result.IsOK)
                {
                    for (int i = 0; i < iEPESlotTask.Result.ResponseItem.Count(); i++)
                    {
                        rootCard.T_IEPESlot[i].id = iEPESlotTask.Result.ResponseItem[i];
                    }
                    T_RootCard[ip].T_IEPESlot.AddRange(rootCard.T_IEPESlot);               
                }
                else
                {
                    string error = DateTime.Now.ToString() + "添加错误:" + iEPESlotTask.Result.ErrorMessage;
                    EventAggregatorService.Instance.EventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("数据库操作", new Exception(error)));
                }
                if (eddyCurrentDisplacementSlotTask.Result.IsOK)
                {
                    for (int i = 0; i < eddyCurrentDisplacementSlotTask.Result.ResponseItem.Count(); i++)
                    {
                        rootCard.T_EddyCurrentDisplacementSlot[i].id = eddyCurrentDisplacementSlotTask.Result.ResponseItem[i];
                    }
                    T_RootCard[ip].T_EddyCurrentDisplacementSlot.AddRange(rootCard.T_EddyCurrentDisplacementSlot);
                }
                else
                {
                    string error = DateTime.Now.ToString() + "添加错误:" + eddyCurrentDisplacementSlotTask.Result.ErrorMessage;
                    EventAggregatorService.Instance.EventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("数据库操作", new Exception(error)));
                }
                if (eddyCurrentKeyPhaseSlotTask.Result.IsOK)
                {
                    for (int i = 0; i < eddyCurrentKeyPhaseSlotTask.Result.ResponseItem.Count(); i++)
                    {
                        rootCard.T_EddyCurrentKeyPhaseSlot[i].id = eddyCurrentKeyPhaseSlotTask.Result.ResponseItem[i];
                    }
                    T_RootCard[ip].T_EddyCurrentKeyPhaseSlot.AddRange(rootCard.T_EddyCurrentKeyPhaseSlot);              
                }
                else
                {
                    string error = DateTime.Now.ToString() + "添加错误:" + eddyCurrentKeyPhaseSlotTask.Result.ErrorMessage;
                    EventAggregatorService.Instance.EventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("数据库操作", new Exception(error)));
                }
                if (eddyCurrentTachometerSlotTask.Result.IsOK)
                {
                    for (int i = 0; i < eddyCurrentTachometerSlotTask.Result.ResponseItem.Count(); i++)
                    {
                        rootCard.T_EddyCurrentTachometerSlot[i].id = eddyCurrentTachometerSlotTask.Result.ResponseItem[i];
                    }
                    T_RootCard[ip].T_EddyCurrentTachometerSlot.AddRange(rootCard.T_EddyCurrentTachometerSlot);             
                }
                else
                {
                    string error = DateTime.Now.ToString() + "添加错误:" + eddyCurrentTachometerSlotTask.Result.ErrorMessage;
                    EventAggregatorService.Instance.EventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("数据库操作", new Exception(error)));
                }
                if (digitTachometerSlotTask.Result.IsOK)
                {
                    for (int i = 0; i < digitTachometerSlotTask.Result.ResponseItem.Count(); i++)
                    {
                        rootCard.T_DigitTachometerSlot[i].id = digitTachometerSlotTask.Result.ResponseItem[i];
                    }
                    T_RootCard[ip].T_DigitTachometerSlot.AddRange(rootCard.T_DigitTachometerSlot);               
                }
                else
                {
                    string error = DateTime.Now.ToString() + "添加错误:" + digitTachometerSlotTask.Result.ErrorMessage;
                    EventAggregatorService.Instance.EventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("数据库操作", new Exception(error)));
                }
                if (analogRransducerInSlotTask.Result.IsOK)
                {
                    for (int i = 0; i < analogRransducerInSlotTask.Result.ResponseItem.Count(); i++)
                    {
                        rootCard.T_AnalogRransducerInSlot[i].id = analogRransducerInSlotTask.Result.ResponseItem[i];
                    }
                    T_RootCard[ip].T_AnalogRransducerInSlot.AddRange(rootCard.T_AnalogRransducerInSlot);             
                }
                else
                {
                    string error = DateTime.Now.ToString() + "添加错误:" + analogRransducerInSlotTask.Result.ErrorMessage;
                    EventAggregatorService.Instance.EventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("数据库操作", new Exception(error)));
                }
                if (relaySlotTask.Result.IsOK)
                {
                    for (int i = 0; i < relaySlotTask.Result.ResponseItem.Count(); i++)
                    {
                        rootCard.T_RelaySlot[i].id = relaySlotTask.Result.ResponseItem[i];
                    }
                    T_RootCard[ip].T_RelaySlot.AddRange(rootCard.T_RelaySlot);            
                }
                else
                {
                    string error = DateTime.Now.ToString() + "添加错误:" + relaySlotTask.Result.ErrorMessage;
                    EventAggregatorService.Instance.EventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("数据库操作", new Exception(error)));
                }
                if (digitRransducerInSlotTask.Result.IsOK)
                {
                    for (int i = 0; i < digitRransducerInSlotTask.Result.ResponseItem.Count(); i++)
                    {
                        rootCard.T_DigitRransducerInSlot[i].id = digitRransducerInSlotTask.Result.ResponseItem[i];
                    }
                    T_RootCard[ip].T_DigitRransducerInSlot.AddRange(rootCard.T_DigitRransducerInSlot);               
                }
                else
                {
                    string error = DateTime.Now.ToString() + "添加错误:" + digitRransducerInSlotTask.Result.ErrorMessage;
                    EventAggregatorService.Instance.EventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("数据库操作", new Exception(error)));
                }
                if (digitRransducerOutSlotTask.Result.IsOK)
                {
                    for (int i = 0; i < digitRransducerOutSlotTask.Result.ResponseItem.Count(); i++)
                    {
                        rootCard.T_DigitRransducerOutSlot[i].id = digitRransducerOutSlotTask.Result.ResponseItem[i];
                    }
                    T_RootCard[ip].T_DigitRransducerOutSlot.AddRange(rootCard.T_DigitRransducerOutSlot);            
                }
                else
                {
                    string error = DateTime.Now.ToString() + "添加错误:" + digitRransducerOutSlotTask.Result.ErrorMessage;
                    EventAggregatorService.Instance.EventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("数据库操作", new Exception(error)));
                }
                if (analogRransducerOutSlotTask.Result.IsOK)
                {
                    for (int i = 0; i < analogRransducerOutSlotTask.Result.ResponseItem.Count(); i++)
                    {
                        rootCard.T_AnalogRransducerOutSlot[i].id = analogRransducerOutSlotTask.Result.ResponseItem[i];
                    }
                    T_RootCard[ip].T_AnalogRransducerOutSlot.AddRange(rootCard.T_AnalogRransducerOutSlot);    
                }
                else
                {
                    string error = DateTime.Now.ToString() + "添加错误:" + analogRransducerOutSlotTask.Result.ErrorMessage;
                    EventAggregatorService.Instance.EventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("数据库操作", new Exception(error)));
                }
                if (wirelessScalarSlotTask.Result.IsOK)
                {
                    for (int i = 0; i < wirelessScalarSlotTask.Result.ResponseItem.Count(); i++)
                    {
                        rootCard.T_WirelessScalarSlot[i].id = wirelessScalarSlotTask.Result.ResponseItem[i];
                    }
                    T_RootCard[ip].T_WirelessScalarSlot.AddRange(rootCard.T_WirelessScalarSlot);               
                }
                else
                {
                    string error = DateTime.Now.ToString() + "添加错误:" + wirelessScalarSlotTask.Result.ErrorMessage;
                    EventAggregatorService.Instance.EventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("数据库操作", new Exception(error)));
                }
                if (wirelessVibrationSlotTask.Result.IsOK)
                {
                    for (int i = 0; i < wirelessVibrationSlotTask.Result.ResponseItem.Count(); i++)
                    {
                        rootCard.T_WirelessVibrationSlot[i].id = wirelessVibrationSlotTask.Result.ResponseItem[i];
                    }
                    T_RootCard[ip].T_WirelessVibrationSlot.AddRange(rootCard.T_WirelessVibrationSlot);            
                }
                else
                {
                    string error = DateTime.Now.ToString() + "添加错误:" + wirelessVibrationSlotTask.Result.ErrorMessage;
                    EventAggregatorService.Instance.EventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("数据库操作", new Exception(error)));
                }
                if (divFreInfoTask.Result.IsOK)
                {
                    for (int i = 0; i < divFreInfoTask.Result.ResponseItem.Count(); i++)
                    {
                        rootCard.T_DivFreInfo[i].id = divFreInfoTask.Result.ResponseItem[i];
                    }
                    T_RootCard[ip].T_DivFreInfo.AddRange(rootCard.T_DivFreInfo);            
                }
                else
                {
                    string error = DateTime.Now.ToString() + "添加错误:" + divFreInfoTask.Result.ErrorMessage;
                    EventAggregatorService.Instance.EventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("数据库操作", new Exception(error)));
                }
                */
            #endregion
        }

        private async Task<bool> DeleteHardwareTables(string ip, T1_RootCard rootCard)
        {
            var client = new DataProvider(ip, LocalSetting.ServerPort, LocalSetting.MajorVersion, LocalSetting.MinorVersion);
            Dictionary<string, Tuple<string, ICollection<object>>> deleteDic = new Dictionary<string, Tuple<string, ICollection<object>>>();
            deleteDic.Add("T_MainControlCard", new Tuple<string, ICollection<object>>("id", rootCard.T_MainControlCard.Select(p => p.id as object).ToList()));
            deleteDic.Add("T_WireMatchingCard", new Tuple<string, ICollection<object>>("id", rootCard.T_WireMatchingCard.Select(p => p.id as object).ToList()));
            deleteDic.Add("T_WirelessReceiveCard", new Tuple<string, ICollection<object>>("id", rootCard.T_WirelessReceiveCard.Select(p => p.id as object).ToList()));
            deleteDic.Add("T_TransmissionCard", new Tuple<string, ICollection<object>>("id", rootCard.T_TransmissionCard.Select(p => p.id as object).ToList()));
            deleteDic.Add("T_AbstractChannelInfo", new Tuple<string, ICollection<object>>("id", rootCard.T_AbstractChannelInfo.Select(p => p.id as object).ToList()));
            deleteDic.Add("T_IEPEChannelInfo", new Tuple<string, ICollection<object>>("id", rootCard.T_IEPEChannelInfo.Select(p => p.id as object).ToList()));
            deleteDic.Add("T_EddyCurrentDisplacementChannelInfo", new Tuple<string, ICollection<object>>("id", rootCard.T_EddyCurrentDisplacementChannelInfo.Select(p => p.id as object).ToList()));
            deleteDic.Add("T_EddyCurrentKeyPhaseChannelInfo", new Tuple<string, ICollection<object>>("id", rootCard.T_EddyCurrentKeyPhaseChannelInfo.Select(p => p.id as object).ToList()));
            deleteDic.Add("T_EddyCurrentTachometerChannelInfo", new Tuple<string, ICollection<object>>("id", rootCard.T_EddyCurrentTachometerChannelInfo.Select(p => p.id as object).ToList()));
            deleteDic.Add("T_DigitTachometerChannelInfo", new Tuple<string, ICollection<object>>("id", rootCard.T_DigitTachometerChannelInfo.Select(p => p.id as object).ToList()));
            deleteDic.Add("T_AnalogRransducerInChannelInfo", new Tuple<string, ICollection<object>>("id", rootCard.T_AnalogRransducerInChannelInfo.Select(p => p.id as object).ToList()));
            deleteDic.Add("T_RelayChannelInfo", new Tuple<string, ICollection<object>>("id", rootCard.T_RelayChannelInfo.Select(p => p.id as object).ToList()));
            deleteDic.Add("T_DigitRransducerInChannelInfo", new Tuple<string, ICollection<object>>("id", rootCard.T_DigitRransducerInChannelInfo.Select(p => p.id as object).ToList()));
            deleteDic.Add("T_DigitRransducerOutChannelInfo", new Tuple<string, ICollection<object>>("id", rootCard.T_DigitRransducerOutChannelInfo.Select(p => p.id as object).ToList()));
            deleteDic.Add("T_AnalogRransducerOutChannelInfo", new Tuple<string, ICollection<object>>("id", rootCard.T_AnalogRransducerOutChannelInfo.Select(p => p.id as object).ToList()));
            deleteDic.Add("T_WirelessScalarChannelInfo", new Tuple<string, ICollection<object>>("id", rootCard.T_WirelessScalarChannelInfo.Select(p => p.id as object).ToList()));
            deleteDic.Add("T_WirelessVibrationChannelInfo", new Tuple<string, ICollection<object>>("id", rootCard.T_WirelessVibrationChannelInfo.Select(p => p.id as object).ToList()));
            deleteDic.Add("T_AbstractSlotInfo", new Tuple<string, ICollection<object>>("id", rootCard.T_AbstractSlotInfo.Select(p => p.id as object).ToList()));
            deleteDic.Add("T_IEPESlot", new Tuple<string, ICollection<object>>("id", rootCard.T_IEPESlot.Select(p => p.id as object).ToList()));
            deleteDic.Add("T_EddyCurrentDisplacementSlot", new Tuple<string, ICollection<object>>("id", rootCard.T_EddyCurrentDisplacementSlot.Select(p => p.id as object).ToList()));
            deleteDic.Add("T_EddyCurrentKeyPhaseSlot", new Tuple<string, ICollection<object>>("id", rootCard.T_EddyCurrentKeyPhaseSlot.Select(p => p.id as object).ToList()));
            deleteDic.Add("T_EddyCurrentTachometerSlot", new Tuple<string, ICollection<object>>("id", rootCard.T_EddyCurrentTachometerSlot.Select(p => p.id as object).ToList()));
            deleteDic.Add("T_DigitTachometerSlot", new Tuple<string, ICollection<object>>("id", rootCard.T_DigitTachometerSlot.Select(p => p.id as object).ToList()));
            deleteDic.Add("T_AnalogRransducerInSlot", new Tuple<string, ICollection<object>>("id", rootCard.T_AnalogRransducerInSlot.Select(p => p.id as object).ToList()));
            deleteDic.Add("T_RelaySlot", new Tuple<string, ICollection<object>>("id", rootCard.T_RelaySlot.Select(p => p.id as object).ToList()));
            deleteDic.Add("T_DigitRransducerInSlot", new Tuple<string, ICollection<object>>("id", rootCard.T_DigitRransducerInSlot.Select(p => p.id as object).ToList()));
            deleteDic.Add("T_DigitRransducerOutSlot", new Tuple<string, ICollection<object>>("id", rootCard.T_DigitRransducerOutSlot.Select(p => p.id as object).ToList()));
            deleteDic.Add("T_AnalogRransducerOutSlot", new Tuple<string, ICollection<object>>("id", rootCard.T_AnalogRransducerOutSlot.Select(p => p.id as object).ToList()));
            deleteDic.Add("T_WirelessScalarSlot", new Tuple<string, ICollection<object>>("id", rootCard.T_WirelessScalarSlot.Select(p => p.id as object).ToList()));
            deleteDic.Add("T_WirelessVibrationSlot", new Tuple<string, ICollection<object>>("id", rootCard.T_WirelessVibrationSlot.Select(p => p.id as object).ToList()));
            deleteDic.Add("T_DivFreInfo", new Tuple<string, ICollection<object>>("id", rootCard.T_DivFreInfo.Select(p => p.id as object).ToList()));
            return await Task.Run(() =>
            {
                var complexResult = client.Complex(null, null, deleteDic);
                //先判断是不是OK          
                if (complexResult.IsOK)
                {
                    //添加数据                    
                    if (rootCard.T_MainControlCard != null)
                    {
                        rootCard.T_MainControlCard.ForEach(p => T_RootCard[ip].T_MainControlCard.Remove(p));
                    }
                    if (rootCard.T_WireMatchingCard != null)
                    {
                        rootCard.T_WireMatchingCard.ForEach(p => T_RootCard[ip].T_WireMatchingCard.Remove(p));
                    }
                    if (rootCard.T_WirelessReceiveCard != null)
                    {
                        rootCard.T_WirelessReceiveCard.ForEach(p => T_RootCard[ip].T_WirelessReceiveCard.Remove(p));
                    }
                    if (rootCard.T_TransmissionCard != null)
                    {
                        rootCard.T_TransmissionCard.ForEach(p => T_RootCard[ip].T_TransmissionCard.Remove(p));
                    }
                    if (rootCard.T_AbstractChannelInfo != null)
                    {
                        rootCard.T_AbstractChannelInfo.ForEach(p => T_RootCard[ip].T_AbstractChannelInfo.Remove(p));
                    }
                    if (rootCard.T_IEPEChannelInfo != null)
                    {
                        rootCard.T_IEPEChannelInfo.ForEach(p => T_RootCard[ip].T_IEPEChannelInfo.Remove(p));
                    }
                    if (rootCard.T_EddyCurrentDisplacementChannelInfo != null)
                    {
                        rootCard.T_EddyCurrentDisplacementChannelInfo.ForEach(p => T_RootCard[ip].T_EddyCurrentDisplacementChannelInfo.Remove(p));
                    }
                    if (rootCard.T_EddyCurrentKeyPhaseChannelInfo != null)
                    {
                        rootCard.T_EddyCurrentKeyPhaseChannelInfo.ForEach(p => T_RootCard[ip].T_EddyCurrentKeyPhaseChannelInfo.Remove(p));
                    }
                    if (rootCard.T_EddyCurrentTachometerChannelInfo != null)
                    {
                        rootCard.T_EddyCurrentTachometerChannelInfo.ForEach(p => T_RootCard[ip].T_EddyCurrentTachometerChannelInfo.Remove(p));
                    }
                    if (rootCard.T_DigitTachometerChannelInfo != null)
                    {
                        rootCard.T_DigitTachometerChannelInfo.ForEach(p => T_RootCard[ip].T_DigitTachometerChannelInfo.Remove(p));
                    }
                    if (rootCard.T_AnalogRransducerInChannelInfo != null)
                    {
                        rootCard.T_AnalogRransducerInChannelInfo.ForEach(p => T_RootCard[ip].T_AnalogRransducerInChannelInfo.Remove(p));
                    }
                    if (rootCard.T_RelayChannelInfo != null)
                    {
                        rootCard.T_RelayChannelInfo.ForEach(p => T_RootCard[ip].T_RelayChannelInfo.Remove(p));
                    }
                    if (rootCard.T_DigitRransducerInChannelInfo != null)
                    {
                        rootCard.T_DigitRransducerInChannelInfo.ForEach(p => T_RootCard[ip].T_DigitRransducerInChannelInfo.Remove(p));
                    }
                    if (rootCard.T_DigitRransducerOutChannelInfo != null)
                    {
                        rootCard.T_DigitRransducerOutChannelInfo.ForEach(p => T_RootCard[ip].T_DigitRransducerOutChannelInfo.Remove(p));
                    }
                    if (rootCard.T_AnalogRransducerOutChannelInfo != null)
                    {
                        rootCard.T_AnalogRransducerOutChannelInfo.ForEach(p => T_RootCard[ip].T_AnalogRransducerOutChannelInfo.Remove(p));
                    }
                    if (rootCard.T_WirelessScalarChannelInfo != null)
                    {
                        rootCard.T_WirelessScalarChannelInfo.ForEach(p => T_RootCard[ip].T_WirelessScalarChannelInfo.Remove(p));
                    }
                    if (rootCard.T_WirelessVibrationChannelInfo != null)
                    {
                        rootCard.T_WirelessVibrationChannelInfo.ForEach(p => T_RootCard[ip].T_WirelessVibrationChannelInfo.Remove(p));
                    }
                    if (rootCard.T_AbstractSlotInfo != null)
                    {
                        rootCard.T_AbstractSlotInfo.ForEach(p => T_RootCard[ip].T_AbstractSlotInfo.Remove(p));
                    }
                    if (rootCard.T_IEPESlot != null)
                    {
                        rootCard.T_IEPESlot.ForEach(p => T_RootCard[ip].T_IEPESlot.Remove(p));
                    }
                    if (rootCard.T_EddyCurrentDisplacementSlot != null)
                    {
                        rootCard.T_EddyCurrentDisplacementSlot.ForEach(p => T_RootCard[ip].T_EddyCurrentDisplacementSlot.Remove(p));
                    }
                    if (rootCard.T_EddyCurrentKeyPhaseSlot != null)
                    {
                        rootCard.T_EddyCurrentKeyPhaseSlot.ForEach(p => T_RootCard[ip].T_EddyCurrentKeyPhaseSlot.Remove(p));
                    }
                    if (rootCard.T_EddyCurrentTachometerSlot != null)
                    {
                        rootCard.T_EddyCurrentTachometerSlot.ForEach(p => T_RootCard[ip].T_EddyCurrentTachometerSlot.Remove(p));
                    }
                    if (rootCard.T_DigitTachometerSlot != null)
                    {
                        rootCard.T_DigitTachometerSlot.ForEach(p => T_RootCard[ip].T_DigitTachometerSlot.Remove(p));
                    }
                    if (rootCard.T_AnalogRransducerInSlot != null)
                    {
                        rootCard.T_AnalogRransducerInSlot.ForEach(p => T_RootCard[ip].T_AnalogRransducerInSlot.Remove(p));
                    }
                    if (rootCard.T_RelaySlot != null)
                    {
                        rootCard.T_RelaySlot.ForEach(p => T_RootCard[ip].T_RelaySlot.Remove(p));
                    }
                    if (rootCard.T_DigitRransducerInSlot != null)
                    {
                        rootCard.T_DigitRransducerInSlot.ForEach(p => T_RootCard[ip].T_DigitRransducerInSlot.Remove(p));
                    }
                    if (rootCard.T_DigitRransducerOutSlot != null)
                    {
                        rootCard.T_DigitRransducerOutSlot.ForEach(p => T_RootCard[ip].T_DigitRransducerOutSlot.Remove(p));
                    }
                    if (rootCard.T_AnalogRransducerOutSlot != null)
                    {
                        rootCard.T_AnalogRransducerOutSlot.ForEach(p => T_RootCard[ip].T_AnalogRransducerOutSlot.Remove(p));
                    }
                    if (rootCard.T_WirelessScalarSlot != null)
                    {
                        rootCard.T_WirelessScalarSlot.ForEach(p => T_RootCard[ip].T_WirelessScalarSlot.Remove(p));
                    }
                    if (rootCard.T_WirelessVibrationSlot != null)
                    {
                        rootCard.T_WirelessVibrationSlot.ForEach(p => T_RootCard[ip].T_WirelessVibrationSlot.Remove(p));
                    }
                    if (rootCard.T_DivFreInfo != null)
                    {
                        rootCard.T_DivFreInfo.ForEach(p => T_RootCard[ip].T_DivFreInfo.Remove(p));
                    }
                    return true;
                }
                else
                {
                    string error = "服务器" + ip + " " + DateTime.Now.ToString() + "删除错误:" + complexResult.ErrorType + "-" + complexResult.ErrorMessage;
                    //ErrorMessage是错误信息
                    EventAggregatorService.Instance.EventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("数据库操作", new Exception(error)));
                    return false;
                }
            });

            #region
            /*//恢复id  
            var mainControlCardTask = Task.Run(() => client.Delete<T_MainControlCard>("id", rootCard.T_MainControlCard.Select(p => ((p as T_MainControlCard).id) as object).ToList()));
            var wireMatchingCardTask = Task.Run(() => client.Delete<T_WireMatchingCard>("id", rootCard.T_WireMatchingCard.Select(p => ((p as T_WireMatchingCard).id) as object).ToList()));
            var wirelessReceiveCardTask = Task.Run(() => client.Delete<T_WirelessReceiveCard>("id", rootCard.T_WirelessReceiveCard.Select(p => ((p as T_WirelessReceiveCard).id) as object).ToList()));
            var transmissionCardTask = Task.Run(() => client.Delete<T_TransmissionCard>("id", rootCard.T_TransmissionCard.Select(p => ((p as T_TransmissionCard).id) as object).ToList()));
            var abstractChannelInfoTask = Task.Run(() => client.Delete<T_AbstractChannelInfo>("id", rootCard.T_AbstractChannelInfo.Select(p => ((p as T_AbstractChannelInfo).id) as object).ToList()));
            var iEPEChannelInfoTask = Task.Run(() => client.Delete<T_IEPEChannelInfo>("id", rootCard.T_IEPEChannelInfo.Select(p => ((p as T_IEPEChannelInfo).id) as object).ToList()));
            var eddyCurrentDisplacementChannelInfoTask = Task.Run(() => client.Delete<T_EddyCurrentDisplacementChannelInfo>("id", rootCard.T_EddyCurrentDisplacementChannelInfo.Select(p => ((p as T_EddyCurrentDisplacementChannelInfo).id) as object).ToList()));
            var eddyCurrentKeyPhaseChannelInfoTask = Task.Run(() => client.Delete<T_EddyCurrentKeyPhaseChannelInfo>("id", rootCard.T_EddyCurrentKeyPhaseChannelInfo.Select(p => ((p as T_EddyCurrentKeyPhaseChannelInfo).id) as object).ToList()));
            var eddyCurrentTachometerChannelInfoTask = Task.Run(() => client.Delete<T_EddyCurrentTachometerChannelInfo>("id", rootCard.T_EddyCurrentTachometerChannelInfo.Select(p => ((p as T_EddyCurrentTachometerChannelInfo).id) as object).ToList()));
            var digitTachometerChannelInfoTask = Task.Run(() => client.Delete<T_DigitTachometerChannelInfo>("id", rootCard.T_DigitTachometerChannelInfo.Select(p => ((p as T_DigitTachometerChannelInfo).id) as object).ToList()));
            var analogRransducerInChannelInfoTask = Task.Run(() => client.Delete<T_AnalogRransducerInChannelInfo>("id", rootCard.T_AnalogRransducerInChannelInfo.Select(p => ((p as T_AnalogRransducerInChannelInfo).id) as object).ToList()));
            var relayChannelInfoTask = Task.Run(() => client.Delete<T_RelayChannelInfo>("id", rootCard.T_RelayChannelInfo.Select(p => ((p as T_RelayChannelInfo).id) as object).ToList()));
            var digitRransducerInChannelInfoTask = Task.Run(() => client.Delete<T_DigitRransducerInChannelInfo>("id", rootCard.T_DigitRransducerInChannelInfo.Select(p => ((p as T_DigitRransducerInChannelInfo).id) as object).ToList()));
            var digitRransducerOutChannelInfoTask = Task.Run(() => client.Delete<T_DigitRransducerOutChannelInfo>("id", rootCard.T_DigitRransducerOutChannelInfo.Select(p => ((p as T_DigitRransducerOutChannelInfo).id) as object).ToList()));
            var analogRransducerOutChannelInfoTask = Task.Run(() => client.Delete<T_AnalogRransducerOutChannelInfo>("id", rootCard.T_AnalogRransducerOutChannelInfo.Select(p => ((p as T_AnalogRransducerOutChannelInfo).id) as object).ToList()));
            var wirelessScalarChannelInfoTask = Task.Run(() => client.Delete<T_WirelessScalarChannelInfo>("id", rootCard.T_WirelessScalarChannelInfo.Select(p => ((p as T_WirelessScalarChannelInfo).id) as object).ToList()));
            var wirelessVibrationChannelInfoTask = Task.Run(() => client.Delete<T_WirelessVibrationChannelInfo>("id", rootCard.T_WirelessVibrationChannelInfo.Select(p => ((p as T_WirelessVibrationChannelInfo).id) as object).ToList()));
            var abstractSlotInfoTask = Task.Run(() => client.Delete<T_AbstractSlotInfo>("id", rootCard.T_AbstractSlotInfo.Select(p => ((p as T_AbstractSlotInfo).id) as object).ToList()));
            var iEPESlotTask = Task.Run(() => client.Delete<T_IEPESlot>("id", rootCard.T_IEPESlot.Select(p => ((p as T_IEPESlot).id) as object).ToList()));
            var eddyCurrentDisplacementSlotTask = Task.Run(() => client.Delete<T_EddyCurrentDisplacementSlot>("id", rootCard.T_EddyCurrentDisplacementSlot.Select(p => ((p as T_EddyCurrentDisplacementSlot).id) as object).ToList()));
            var eddyCurrentKeyPhaseSlotTask = Task.Run(() => client.Delete<T_EddyCurrentKeyPhaseSlot>("id", rootCard.T_EddyCurrentKeyPhaseSlot.Select(p => ((p as T_EddyCurrentKeyPhaseSlot).id) as object).ToList()));
            var eddyCurrentTachometerSlotTask = Task.Run(() => client.Delete<T_EddyCurrentTachometerSlot>("id", rootCard.T_EddyCurrentTachometerSlot.Select(p => ((p as T_EddyCurrentTachometerSlot).id) as object).ToList()));
            var digitTachometerSlotTask = Task.Run(() => client.Delete<T_DigitTachometerSlot>("id", rootCard.T_DigitTachometerSlot.Select(p => ((p as T_DigitTachometerSlot).id) as object).ToList()));
            var analogRransducerInSlotTask = Task.Run(() => client.Delete<T_AnalogRransducerInSlot>("id", rootCard.T_AnalogRransducerInSlot.Select(p => ((p as T_AnalogRransducerInSlot).id) as object).ToList()));
            var relaySlotTask = Task.Run(() => client.Delete<T_RelaySlot>("id", rootCard.T_RelaySlot.Select(p => ((p as T_RelaySlot).id) as object).ToList()));
            var digitRransducerInSlotTask = Task.Run(() => client.Delete<T_DigitRransducerInSlot>("id", rootCard.T_DigitRransducerInSlot.Select(p => ((p as T_DigitRransducerInSlot).id) as object).ToList()));
            var digitRransducerOutSlotTask = Task.Run(() => client.Delete<T_DigitRransducerOutSlot>("id", rootCard.T_DigitRransducerOutSlot.Select(p => ((p as T_DigitRransducerOutSlot).id) as object).ToList()));
            var analogRransducerOutSlotTask = Task.Run(() => client.Delete<T_AnalogRransducerOutSlot>("id", rootCard.T_AnalogRransducerOutSlot.Select(p => ((p as T_AnalogRransducerOutSlot).id) as object).ToList()));
            var wirelessScalarSlotTask = Task.Run(() => client.Delete<T_WirelessScalarSlot>("id", rootCard.T_WirelessScalarSlot.Select(p => ((p as T_WirelessScalarSlot).id) as object).ToList()));
            var wirelessVibrationSlotTask = Task.Run(() => client.Delete<T_WirelessVibrationSlot>("id", rootCard.T_WirelessVibrationSlot.Select(p => ((p as T_WirelessVibrationSlot).id) as object).ToList()));
            var divFreInfoTask = Task.Run(() => client.Delete<T_DivFreInfo>("id", rootCard.T_DivFreInfo.Select(p => ((p as T_DivFreInfo).id) as object).ToList()));
         
            await Task.WhenAll(mainControlCardTask, wireMatchingCardTask,
                wirelessReceiveCardTask, transmissionCardTask,
                abstractChannelInfoTask, iEPEChannelInfoTask,
                eddyCurrentDisplacementChannelInfoTask, eddyCurrentKeyPhaseChannelInfoTask,
                eddyCurrentTachometerChannelInfoTask, digitTachometerChannelInfoTask,
                analogRransducerInChannelInfoTask, relayChannelInfoTask,
                digitRransducerInChannelInfoTask, digitRransducerOutChannelInfoTask,
                analogRransducerOutChannelInfoTask, wirelessScalarChannelInfoTask,
                wirelessVibrationChannelInfoTask, abstractSlotInfoTask,
                iEPESlotTask, eddyCurrentDisplacementSlotTask,
                eddyCurrentKeyPhaseSlotTask, eddyCurrentTachometerSlotTask,
                digitTachometerSlotTask, analogRransducerInSlotTask,
                relaySlotTask, digitRransducerInSlotTask,
                digitRransducerOutSlotTask, analogRransducerOutSlotTask,
                wirelessScalarSlotTask, wirelessVibrationSlotTask,
                divFreInfoTask);

            if (mainControlCardTask.Result.IsOK)
            {
                rootCard.T_MainControlCard.ForEach(p => T_RootCard[ip].T_MainControlCard.Remove(p));               
            }
            else
            {
                string error = DateTime.Now.ToString() + "删除错误:" + mainControlCardTask.Result.ErrorMessage;
                EventAggregatorService.Instance.EventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("数据库操作", new Exception(error)));
            }
            if (wireMatchingCardTask.Result.IsOK)
            {
                rootCard.T_WireMatchingCard.ForEach(p => T_RootCard[ip].T_WireMatchingCard.Remove(p));              
            }
            else
            {
                string error = DateTime.Now.ToString() + "删除错误:" + wireMatchingCardTask.Result.ErrorMessage;
                EventAggregatorService.Instance.EventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("数据库操作", new Exception(error)));
            }
            if (wirelessReceiveCardTask.Result.IsOK)
            {
                rootCard.T_WirelessReceiveCard.ForEach(p => T_RootCard[ip].T_WirelessReceiveCard.Remove(p));     
            }
            else
            {
                string error = DateTime.Now.ToString() + "删除错误:" + wirelessReceiveCardTask.Result.ErrorMessage;
                EventAggregatorService.Instance.EventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("数据库操作", new Exception(error)));
            }
            if (transmissionCardTask.Result.IsOK)
            {
                rootCard.T_TransmissionCard.ForEach(p => T_RootCard[ip].T_TransmissionCard.Remove(p));                
            }
            else
            {
                string error = DateTime.Now.ToString() + "删除错误:" + transmissionCardTask.Result.ErrorMessage;
                EventAggregatorService.Instance.EventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("数据库操作", new Exception(error)));
            }
            if (abstractChannelInfoTask.Result.IsOK)
            {
                rootCard.T_AbstractChannelInfo.ForEach(p => T_RootCard[ip].T_AbstractChannelInfo.Remove(p));           
            }
            else
            {
                string error = DateTime.Now.ToString() + "删除错误:" + abstractChannelInfoTask.Result.ErrorMessage;
                EventAggregatorService.Instance.EventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("数据库操作", new Exception(error)));
            }
            if (iEPEChannelInfoTask.Result.IsOK)
            {
                rootCard.T_IEPEChannelInfo.ForEach(p => T_RootCard[ip].T_IEPEChannelInfo.Remove(p));            
            }
            else
            {
                string error = DateTime.Now.ToString() + "删除错误:" + iEPEChannelInfoTask.Result.ErrorMessage;
                EventAggregatorService.Instance.EventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("数据库操作", new Exception(error)));
            }
            if (eddyCurrentDisplacementChannelInfoTask.Result.IsOK)
            {
                rootCard.T_EddyCurrentDisplacementChannelInfo.ForEach(p => T_RootCard[ip].T_EddyCurrentDisplacementChannelInfo.Remove(p));
            }
            else
            {
                string error = DateTime.Now.ToString() + "删除错误:" + eddyCurrentDisplacementChannelInfoTask.Result.ErrorMessage;
                EventAggregatorService.Instance.EventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("数据库操作", new Exception(error)));
            }
            if (eddyCurrentKeyPhaseChannelInfoTask.Result.IsOK)
            {
                rootCard.T_EddyCurrentKeyPhaseChannelInfo.ForEach(p => T_RootCard[ip].T_EddyCurrentKeyPhaseChannelInfo.Remove(p));
            }
            else
            {
                string error = DateTime.Now.ToString() + "删除错误:" + eddyCurrentKeyPhaseChannelInfoTask.Result.ErrorMessage;
                EventAggregatorService.Instance.EventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("数据库操作", new Exception(error)));
            }
            if (eddyCurrentTachometerChannelInfoTask.Result.IsOK)
            {
                rootCard.T_EddyCurrentTachometerChannelInfo.ForEach(p => T_RootCard[ip].T_EddyCurrentTachometerChannelInfo.Remove(p));
            }
            else
            {
                string error = DateTime.Now.ToString() + "删除错误:" + eddyCurrentTachometerChannelInfoTask.Result.ErrorMessage;
                EventAggregatorService.Instance.EventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("数据库操作", new Exception(error)));
            }
            if (digitTachometerChannelInfoTask.Result.IsOK)
            {
                rootCard.T_DigitTachometerChannelInfo.ForEach(p => T_RootCard[ip].T_DigitTachometerChannelInfo.Remove(p));
            }
            else
            {
                string error = DateTime.Now.ToString() + "删除错误:" + digitTachometerChannelInfoTask.Result.ErrorMessage;
                EventAggregatorService.Instance.EventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("数据库操作", new Exception(error)));
            }
            if (analogRransducerInChannelInfoTask.Result.IsOK)
            {
                rootCard.T_AnalogRransducerInChannelInfo.ForEach(p => T_RootCard[ip].T_AnalogRransducerInChannelInfo.Remove(p));
            }
            else
            {
                string error = DateTime.Now.ToString() + "删除错误:" + analogRransducerInChannelInfoTask.Result.ErrorMessage;
                EventAggregatorService.Instance.EventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("数据库操作", new Exception(error)));
            }
            if (relayChannelInfoTask.Result.IsOK)
            {
                rootCard.T_RelayChannelInfo.ForEach(p => T_RootCard[ip].T_RelayChannelInfo.Remove(p));
            }
            else
            {
                string error = DateTime.Now.ToString() + "删除错误:" + relayChannelInfoTask.Result.ErrorMessage;
                EventAggregatorService.Instance.EventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("数据库操作", new Exception(error)));
            }
            if (digitRransducerInChannelInfoTask.Result.IsOK)
            {
                rootCard.T_DigitRransducerInChannelInfo.ForEach(p => T_RootCard[ip].T_DigitRransducerInChannelInfo.Remove(p));
            }
            else
            {
                string error = DateTime.Now.ToString() + "删除错误:" + digitRransducerInChannelInfoTask.Result.ErrorMessage;
                EventAggregatorService.Instance.EventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("数据库操作", new Exception(error)));
            }
            if (digitRransducerOutChannelInfoTask.Result.IsOK)
            {
                rootCard.T_DigitRransducerOutChannelInfo.ForEach(p => T_RootCard[ip].T_DigitRransducerOutChannelInfo.Remove(p));
            }
            else
            {
                string error = DateTime.Now.ToString() + "删除错误:" + digitRransducerOutChannelInfoTask.Result.ErrorMessage;
                EventAggregatorService.Instance.EventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("数据库操作", new Exception(error)));
            }
            if (analogRransducerOutChannelInfoTask.Result.IsOK)
            {
                rootCard.T_AnalogRransducerOutChannelInfo.ForEach(p => T_RootCard[ip].T_AnalogRransducerOutChannelInfo.Remove(p));
            }
            else
            {
                string error = DateTime.Now.ToString() + "删除错误:" + analogRransducerOutChannelInfoTask.Result.ErrorMessage;
                EventAggregatorService.Instance.EventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("数据库操作", new Exception(error)));
            }
            if (wirelessScalarChannelInfoTask.Result.IsOK)
            {
                rootCard.T_WirelessScalarChannelInfo.ForEach(p => T_RootCard[ip].T_WirelessScalarChannelInfo.Remove(p));          
            }
            else
            {
                string error = DateTime.Now.ToString() + "删除错误:" + wirelessScalarChannelInfoTask.Result.ErrorMessage;
                EventAggregatorService.Instance.EventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("数据库操作", new Exception(error)));
            }
            if (wirelessVibrationChannelInfoTask.Result.IsOK)
            {
                rootCard.T_WirelessVibrationChannelInfo.ForEach(p => T_RootCard[ip].T_WirelessVibrationChannelInfo.Remove(p));
            }
            else
            {
                string error = DateTime.Now.ToString() + "删除错误:" + wirelessVibrationChannelInfoTask.Result.ErrorMessage;
                EventAggregatorService.Instance.EventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("数据库操作", new Exception(error)));
            }
            if (abstractSlotInfoTask.Result.IsOK)
            {
                rootCard.T_AbstractSlotInfo.ForEach(p => T_RootCard[ip].T_AbstractSlotInfo.Remove(p));
            }
            else
            {
                string error = DateTime.Now.ToString() + "删除错误:" + abstractSlotInfoTask.Result.ErrorMessage;
                EventAggregatorService.Instance.EventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("数据库操作", new Exception(error)));
            }
            if (iEPESlotTask.Result.IsOK)
            {
                rootCard.T_IEPESlot.ForEach(p => T_RootCard[ip].T_IEPESlot.Remove(p));
            }
            else
            {
                string error = DateTime.Now.ToString() + "删除错误:" + iEPESlotTask.Result.ErrorMessage;
                EventAggregatorService.Instance.EventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("数据库操作", new Exception(error)));
            }
            if (eddyCurrentDisplacementSlotTask.Result.IsOK)
            {
                rootCard.T_EddyCurrentDisplacementSlot.ForEach(p => T_RootCard[ip].T_EddyCurrentDisplacementSlot.Remove(p));
            }
            else
            {
                string error = DateTime.Now.ToString() + "删除错误:" + eddyCurrentDisplacementSlotTask.Result.ErrorMessage;
                EventAggregatorService.Instance.EventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("数据库操作", new Exception(error)));
            }
            if (eddyCurrentKeyPhaseSlotTask.Result.IsOK)
            {
                rootCard.T_EddyCurrentKeyPhaseSlot.ForEach(p => T_RootCard[ip].T_EddyCurrentKeyPhaseSlot.Remove(p));
            }
            else
            {
                string error = DateTime.Now.ToString() + "删除错误:" + eddyCurrentKeyPhaseSlotTask.Result.ErrorMessage;
                EventAggregatorService.Instance.EventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("数据库操作", new Exception(error)));
            }
            if (eddyCurrentTachometerSlotTask.Result.IsOK)
            {
                rootCard.T_EddyCurrentTachometerSlot.ForEach(p => T_RootCard[ip].T_EddyCurrentTachometerSlot.Remove(p));
            }
            else
            {
                string error = DateTime.Now.ToString() + "删除错误:" + eddyCurrentTachometerSlotTask.Result.ErrorMessage;
                EventAggregatorService.Instance.EventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("数据库操作", new Exception(error)));
            }
            if (digitTachometerSlotTask.Result.IsOK)
            {
                rootCard.T_DigitTachometerSlot.ForEach(p => T_RootCard[ip].T_DigitTachometerSlot.Remove(p));
            }
            else
            {
                string error = DateTime.Now.ToString() + "删除错误:" + digitTachometerSlotTask.Result.ErrorMessage;
                EventAggregatorService.Instance.EventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("数据库操作", new Exception(error)));
            }
            if (analogRransducerInSlotTask.Result.IsOK)
            {
                rootCard.T_AnalogRransducerInSlot.ForEach(p => T_RootCard[ip].T_AnalogRransducerInSlot.Remove(p));
            }
            else
            {
                string error = DateTime.Now.ToString() + "删除错误:" + analogRransducerInSlotTask.Result.ErrorMessage;
                EventAggregatorService.Instance.EventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("数据库操作", new Exception(error)));
            }
            if (relaySlotTask.Result.IsOK)
            {
                rootCard.T_RelaySlot.ForEach(p => T_RootCard[ip].T_RelaySlot.Remove(p));
            }
            else
            {
                string error = DateTime.Now.ToString() + "删除错误:" + relaySlotTask.Result.ErrorMessage;
                EventAggregatorService.Instance.EventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("数据库操作", new Exception(error)));
            }
            if (digitRransducerInSlotTask.Result.IsOK)
            {
                rootCard.T_DigitRransducerInSlot.ForEach(p => T_RootCard[ip].T_DigitRransducerInSlot.Remove(p));
            }
            else
            {
                string error = DateTime.Now.ToString() + "删除错误:" + digitRransducerInSlotTask.Result.ErrorMessage;
                EventAggregatorService.Instance.EventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("数据库操作", new Exception(error)));
            }
            if (digitRransducerOutSlotTask.Result.IsOK)
            {
                rootCard.T_DigitRransducerOutSlot.ForEach(p => T_RootCard[ip].T_DigitRransducerOutSlot.Remove(p));
            }
            else
            {
                string error = DateTime.Now.ToString() + "删除错误:" + digitRransducerOutSlotTask.Result.ErrorMessage;
                EventAggregatorService.Instance.EventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("数据库操作", new Exception(error)));
            }
            if (analogRransducerOutSlotTask.Result.IsOK)
            {
                rootCard.T_AnalogRransducerOutSlot.ForEach(p => T_RootCard[ip].T_AnalogRransducerOutSlot.Remove(p));
            }
            else
            {
                string error = DateTime.Now.ToString() + "删除错误:" + analogRransducerOutSlotTask.Result.ErrorMessage;
                EventAggregatorService.Instance.EventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("数据库操作", new Exception(error)));
            }
            if (wirelessScalarSlotTask.Result.IsOK)
            {
                rootCard.T_WirelessScalarSlot.ForEach(p => T_RootCard[ip].T_WirelessScalarSlot.Remove(p));
            }
            else
            {
                string error = DateTime.Now.ToString() + "删除错误:" + wirelessScalarSlotTask.Result.ErrorMessage;
                EventAggregatorService.Instance.EventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("数据库操作", new Exception(error)));
            }
            if (wirelessVibrationSlotTask.Result.IsOK)
            {
                rootCard.T_WirelessVibrationSlot.ForEach(p => T_RootCard[ip].T_WirelessVibrationSlot.Remove(p));
            }
            else
            {
                string error = DateTime.Now.ToString() + "删除错误:" + wirelessVibrationSlotTask.Result.ErrorMessage;
                EventAggregatorService.Instance.EventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("数据库操作", new Exception(error)));
            }
            if (divFreInfoTask.Result.IsOK)
            {
                rootCard.T_DivFreInfo.ForEach(p => T_RootCard[ip].T_DivFreInfo.Remove(p));
            }
            else
            {
                string error = DateTime.Now.ToString() + "删除错误:" + divFreInfoTask.Result.ErrorMessage;
                EventAggregatorService.Instance.EventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("数据库操作", new Exception(error)));
            }
            */
            #endregion
        }

        private void add(string ip, object obj, long id)
        {
            if (obj is Iid)
            {
                (obj as Iid).id = id;
            }
            if (obj is T1_Organization)
            {
                T_Organization[ip].Add(obj as T1_Organization);
            }
            else if (obj is T1_Device)
            {
                T_Device[ip].Add(obj as T1_Device);
            }
            else if (obj is T1_Item)
            {
                T_Item[ip].Add(obj as T1_Item);
            }
            else if (obj is T1_User)
            {
                T_User[ip].Add(obj as T1_User);
            }
            else if (obj is T1_Role)
            {
                T_Role[ip].Add(obj as T1_Role);
            }
            else if (obj is T1_Menu)
            {
                T_Menu[ip].Add(obj as T1_Menu);
            }
            else if (obj is T1_OrganizationPrivilege)
            {
                T_OrganizationPrivilege[ip].Add(obj as T1_OrganizationPrivilege);
            }
            if (obj is T1_MainControlCard)
            {              
                T_RootCard[ip].T_MainControlCard.Add(obj as T1_MainControlCard);
            }
            else if (obj is T1_WireMatchingCard)
            {
                T_RootCard[ip].T_WireMatchingCard.Add(obj as T1_WireMatchingCard);
            }
            else if (obj is T1_WirelessReceiveCard)
            {
                T_RootCard[ip].T_WirelessReceiveCard.Add(obj as T1_WirelessReceiveCard);
            }
            else if (obj is T1_TransmissionCard)
            {
                T_RootCard[ip].T_TransmissionCard.Add(obj as T1_TransmissionCard);
            }
            else if (obj is T1_AbstractChannelInfo)
            {
                T_RootCard[ip].T_AbstractChannelInfo.Add(obj as T1_AbstractChannelInfo);
            }
            else if (obj is T1_IEPEChannelInfo)
            {
                T_RootCard[ip].T_IEPEChannelInfo.Add(obj as T1_IEPEChannelInfo);
            }
            else if (obj is T1_EddyCurrentDisplacementChannelInfo)
            {
                T_RootCard[ip].T_EddyCurrentDisplacementChannelInfo.Add(obj as T1_EddyCurrentDisplacementChannelInfo);
            }
            else if (obj is T1_EddyCurrentKeyPhaseChannelInfo)
            {
                T_RootCard[ip].T_EddyCurrentKeyPhaseChannelInfo.Add(obj as T1_EddyCurrentKeyPhaseChannelInfo);
            }
            else if (obj is T1_EddyCurrentTachometerChannelInfo)
            {
                T_RootCard[ip].T_EddyCurrentTachometerChannelInfo.Add(obj as T1_EddyCurrentTachometerChannelInfo);
            }
            else if (obj is T1_DigitTachometerChannelInfo)
            {
                T_RootCard[ip].T_DigitTachometerChannelInfo.Add(obj as T1_DigitTachometerChannelInfo);
            }
            else if (obj is T1_AnalogRransducerInChannelInfo)
            {
                T_RootCard[ip].T_AnalogRransducerInChannelInfo.Add(obj as T1_AnalogRransducerInChannelInfo);
            }
            else if (obj is T1_RelayChannelInfo)
            {
                T_RootCard[ip].T_RelayChannelInfo.Add(obj as T1_RelayChannelInfo);
            }
            else if (obj is T1_DigitRransducerInChannelInfo)
            {
                T_RootCard[ip].T_DigitRransducerInChannelInfo.Add(obj as T1_DigitRransducerInChannelInfo);
            }
            else if (obj is T1_DigitRransducerOutChannelInfo)
            {
                T_RootCard[ip].T_DigitRransducerOutChannelInfo.Add(obj as T1_DigitRransducerOutChannelInfo);
            }
            else if (obj is T1_AnalogRransducerOutChannelInfo)
            {
                T_RootCard[ip].T_AnalogRransducerOutChannelInfo.Add(obj as T1_AnalogRransducerOutChannelInfo);
            }
            else if (obj is T1_WirelessScalarChannelInfo)
            {
                T_RootCard[ip].T_WirelessScalarChannelInfo.Add(obj as T1_WirelessScalarChannelInfo);
            }
            else if (obj is T1_WirelessVibrationChannelInfo)
            {
                T_RootCard[ip].T_WirelessVibrationChannelInfo.Add(obj as T1_WirelessVibrationChannelInfo);
            }
            else if (obj is T1_AbstractSlotInfo)
            {
                T_RootCard[ip].T_AbstractSlotInfo.Add(obj as T1_AbstractSlotInfo);
            }
            else if (obj is T1_IEPESlot)
            {
                T_RootCard[ip].T_IEPESlot.Add(obj as T1_IEPESlot);
            }
            else if (obj is T1_EddyCurrentDisplacementSlot)
            {
                T_RootCard[ip].T_EddyCurrentDisplacementSlot.Add(obj as T1_EddyCurrentDisplacementSlot);
            }
            else if (obj is T1_EddyCurrentKeyPhaseSlot)
            {
                T_RootCard[ip].T_EddyCurrentKeyPhaseSlot.Add(obj as T1_EddyCurrentKeyPhaseSlot);
            }
            else if (obj is T1_EddyCurrentTachometerSlot)
            {
                T_RootCard[ip].T_EddyCurrentTachometerSlot.Add(obj as T1_EddyCurrentTachometerSlot);
            }
            else if (obj is T1_DigitTachometerSlot)
            {
                T_RootCard[ip].T_DigitTachometerSlot.Add(obj as T1_DigitTachometerSlot);
            }
            else if (obj is T1_AnalogRransducerInSlot)
            {
                T_RootCard[ip].T_AnalogRransducerInSlot.Add(obj as T1_AnalogRransducerInSlot);
            }
            else if (obj is T1_RelaySlot)
            {
                T_RootCard[ip].T_RelaySlot.Add(obj as T1_RelaySlot);
            }
            else if (obj is T1_DigitRransducerInSlot)
            {
                T_RootCard[ip].T_DigitRransducerInSlot.Add(obj as T1_DigitRransducerInSlot);
            }
            else if (obj is T1_DigitRransducerOutSlot)
            {
                T_RootCard[ip].T_DigitRransducerOutSlot.Add(obj as T1_DigitRransducerOutSlot);
            }
            else if (obj is T1_AnalogRransducerOutSlot)
            {
                T_RootCard[ip].T_AnalogRransducerOutSlot.Add(obj as T1_AnalogRransducerOutSlot);
            }
            else if (obj is T1_WirelessScalarSlot)
            {
                T_RootCard[ip].T_WirelessScalarSlot.Add(obj as T1_WirelessScalarSlot);
            }
            else if (obj is T1_WirelessVibrationSlot)
            {
                T_RootCard[ip].T_WirelessVibrationSlot.Add(obj as T1_WirelessVibrationSlot);
            }
            else if (obj is T1_DivFreInfo)
            {
                T_RootCard[ip].T_DivFreInfo.Add(obj as T1_DivFreInfo);
            }
        }

        private void delete(string ip, object obj)
        {
            if (obj is T1_Organization)// && T_Organization[ip].Contains(obj as T1_Organization))
            {
                T_Organization[ip].Remove(obj as T1_Organization);
            }
            else if (obj is T1_Device)// && T_Device[ip].Contains(obj as T1_Device))
            {
                T_Device[ip].Remove(obj as T1_Device);
            }
            else if (obj is T1_Item)// && T_Item[ip].Contains(obj as T1_Item))
            {
                T_Item[ip].Remove(obj as T1_Item);
            }
            else if (obj is T1_User)// && T_User[ip].Contains(obj as T1_User))
            {
                T_User[ip].Remove(obj as T1_User);
            }
            else if (obj is T1_Role)// && T_Role[ip].Contains(obj as T1_Role))
            {
                T_Role[ip].Remove(obj as T1_Role);
            }
            else if (obj is T1_Menu)// && T_Menu[ip].Contains(obj as T1_Menu))
            {
                T_Menu[ip].Remove(obj as T1_Menu);
            }
            else if (obj is T1_OrganizationPrivilege)// && T_OrganizationPrivilege[ip].Contains(obj as T1_OrganizationPrivilege))
            {
                T_OrganizationPrivilege[ip].Remove(obj as T1_OrganizationPrivilege);
            }
            else if (obj is T1_MainControlCard)// && T_RootCard[ip].T_MainControlCard.Contains(obj as T1_MainControlCard))
            {
                T_RootCard[ip].T_MainControlCard.Remove(obj as T1_MainControlCard);
            }
            else if (obj is T1_WireMatchingCard)// && T_RootCard[ip].T_WireMatchingCard.Contains(obj as T1_WireMatchingCard))
            {
                T_RootCard[ip].T_WireMatchingCard.Remove(obj as T1_WireMatchingCard);
            }
            else if (obj is T1_WirelessReceiveCard)// && T_RootCard[ip].T_WirelessReceiveCard.Contains(obj as T1_WirelessReceiveCard))
            {
                T_RootCard[ip].T_WirelessReceiveCard.Remove(obj as T1_WirelessReceiveCard);
            }
            else if (obj is T1_TransmissionCard)// && T_RootCard[ip].T_TransmissionCard.Contains(obj as T1_TransmissionCard))
            {
                T_RootCard[ip].T_TransmissionCard.Remove(obj as T1_TransmissionCard);
            }
            else if (obj is T1_AbstractChannelInfo)// && T_RootCard[ip].T_AbstractChannelInfo.Contains(obj as T1_AbstractChannelInfo))
            {
                T_RootCard[ip].T_AbstractChannelInfo.Remove(obj as T1_AbstractChannelInfo);
            }
            else if (obj is T1_IEPEChannelInfo)// && T_RootCard[ip].T_IEPEChannelInfo.Contains(obj as T1_IEPEChannelInfo))
            {
                T_RootCard[ip].T_IEPEChannelInfo.Remove(obj as T1_IEPEChannelInfo);
            }
            else if (obj is T1_EddyCurrentDisplacementChannelInfo)// && T_RootCard[ip].T_EddyCurrentDisplacementChannelInfo.Contains(obj as T1_EddyCurrentDisplacementChannelInfo))
            {
                T_RootCard[ip].T_EddyCurrentDisplacementChannelInfo.Remove(obj as T1_EddyCurrentDisplacementChannelInfo);
            }
            else if (obj is T1_EddyCurrentKeyPhaseChannelInfo)// && T_RootCard[ip].T_EddyCurrentKeyPhaseChannelInfo.Contains(obj as T1_EddyCurrentKeyPhaseChannelInfo))
            {
                T_RootCard[ip].T_EddyCurrentKeyPhaseChannelInfo.Remove(obj as T1_EddyCurrentKeyPhaseChannelInfo);
            }
            else if (obj is T1_EddyCurrentTachometerChannelInfo)// && T_RootCard[ip].T_EddyCurrentTachometerChannelInfo.Contains(obj as T1_EddyCurrentTachometerChannelInfo))
            {
                T_RootCard[ip].T_EddyCurrentTachometerChannelInfo.Remove(obj as T1_EddyCurrentTachometerChannelInfo);
            }
            else if (obj is T1_DigitTachometerChannelInfo)// && T_RootCard[ip].T_DigitTachometerChannelInfo.Contains(obj as T1_DigitTachometerChannelInfo))
            {
                T_RootCard[ip].T_DigitTachometerChannelInfo.Remove(obj as T1_DigitTachometerChannelInfo);
            }
            else if (obj is T1_AnalogRransducerInChannelInfo)// && T_RootCard[ip].T_AnalogRransducerInChannelInfo.Contains(obj as T1_AnalogRransducerInChannelInfo))
            {
                T_RootCard[ip].T_AnalogRransducerInChannelInfo.Remove(obj as T1_AnalogRransducerInChannelInfo);
            }
            else if (obj is T1_RelayChannelInfo)// && T_RootCard[ip].T_RelayChannelInfo.Contains(obj as T1_RelayChannelInfo))
            {
                T_RootCard[ip].T_RelayChannelInfo.Remove(obj as T1_RelayChannelInfo);
            }
            else if (obj is T1_DigitRransducerInChannelInfo)// && T_RootCard[ip].T_DigitRransducerInChannelInfo.Contains(obj as T1_DigitRransducerInChannelInfo))
            {
                T_RootCard[ip].T_DigitRransducerInChannelInfo.Remove(obj as T1_DigitRransducerInChannelInfo);
            }
            else if (obj is T1_DigitRransducerOutChannelInfo)// && T_RootCard[ip].T_DigitRransducerOutChannelInfo.Contains(obj as T1_DigitRransducerOutChannelInfo))
            {
                T_RootCard[ip].T_DigitRransducerOutChannelInfo.Remove(obj as T1_DigitRransducerOutChannelInfo);
            }
            else if (obj is T1_AnalogRransducerOutChannelInfo)// && T_RootCard[ip].T_AnalogRransducerOutChannelInfo.Contains(obj as T1_AnalogRransducerOutChannelInfo))
            {
                T_RootCard[ip].T_AnalogRransducerOutChannelInfo.Remove(obj as T1_AnalogRransducerOutChannelInfo);
            }
            else if (obj is T1_WirelessScalarChannelInfo)// && T_RootCard[ip].T_WirelessScalarChannelInfo.Contains(obj as T1_WirelessScalarChannelInfo))
            {
                T_RootCard[ip].T_WirelessScalarChannelInfo.Remove(obj as T1_WirelessScalarChannelInfo);
            }
            else if (obj is T1_WirelessVibrationChannelInfo)// && T_RootCard[ip].T_WirelessVibrationChannelInfo.Contains(obj as T1_WirelessVibrationChannelInfo))
            {
                T_RootCard[ip].T_WirelessVibrationChannelInfo.Remove(obj as T1_WirelessVibrationChannelInfo);
            }
            else if (obj is T1_AbstractSlotInfo)// && T_RootCard[ip].T_AbstractSlotInfo.Contains(obj as T1_AbstractSlotInfo))
            {
                T_RootCard[ip].T_AbstractSlotInfo.Remove(obj as T1_AbstractSlotInfo);
            }
            else if (obj is T1_IEPESlot)// && T_RootCard[ip].T_IEPESlot.Contains(obj as T1_IEPESlot))
            {
                T_RootCard[ip].T_IEPESlot.Remove(obj as T1_IEPESlot);
            }
            else if (obj is T1_EddyCurrentDisplacementSlot)// && T_RootCard[ip].T_EddyCurrentDisplacementSlot.Contains(obj as T1_EddyCurrentDisplacementSlot))
            {
                T_RootCard[ip].T_EddyCurrentDisplacementSlot.Remove(obj as T1_EddyCurrentDisplacementSlot);
            }
            else if (obj is T1_EddyCurrentKeyPhaseSlot)// && T_RootCard[ip].T_EddyCurrentKeyPhaseSlot.Contains(obj as T1_EddyCurrentKeyPhaseSlot))
            {
                T_RootCard[ip].T_EddyCurrentKeyPhaseSlot.Remove(obj as T1_EddyCurrentKeyPhaseSlot);
            }
            else if (obj is T1_EddyCurrentTachometerSlot)// && T_RootCard[ip].T_EddyCurrentTachometerSlot.Contains(obj as T1_EddyCurrentTachometerSlot))
            {
                T_RootCard[ip].T_EddyCurrentTachometerSlot.Remove(obj as T1_EddyCurrentTachometerSlot);
            }
            else if (obj is T1_DigitTachometerSlot)// && T_RootCard[ip].T_DigitTachometerSlot.Contains(obj as T1_DigitTachometerSlot))
            {
                T_RootCard[ip].T_DigitTachometerSlot.Remove(obj as T1_DigitTachometerSlot);
            }
            else if (obj is T1_AnalogRransducerInSlot)// && T_RootCard[ip].T_AnalogRransducerInSlot.Contains(obj as T1_AnalogRransducerInSlot))
            {
                T_RootCard[ip].T_AnalogRransducerInSlot.Remove(obj as T1_AnalogRransducerInSlot);
            }
            else if (obj is T1_RelaySlot)// && T_RootCard[ip].T_RelaySlot.Contains(obj as T1_RelaySlot))
            {
                T_RootCard[ip].T_RelaySlot.Remove(obj as T1_RelaySlot);
            }
            else if (obj is T1_DigitRransducerInSlot)// && T_RootCard[ip].T_DigitRransducerInSlot.Contains(obj as T1_DigitRransducerInSlot))
            {
                T_RootCard[ip].T_DigitRransducerInSlot.Remove(obj as T1_DigitRransducerInSlot);
            }
            else if (obj is T1_DigitRransducerOutSlot)// && T_RootCard[ip].T_DigitRransducerOutSlot.Contains(obj as T1_DigitRransducerOutSlot))
            {
                T_RootCard[ip].T_DigitRransducerOutSlot.Remove(obj as T1_DigitRransducerOutSlot);
            }
            else if (obj is T1_AnalogRransducerOutSlot)// && T_RootCard[ip].T_AnalogRransducerOutSlot.Contains(obj as T1_AnalogRransducerOutSlot))
            {
                T_RootCard[ip].T_AnalogRransducerOutSlot.Remove(obj as T1_AnalogRransducerOutSlot);
            }
            else if (obj is T1_WirelessScalarSlot)// && T_RootCard[ip].T_WirelessScalarSlot.Contains(obj as T1_WirelessScalarSlot))
            {
                T_RootCard[ip].T_WirelessScalarSlot.Remove(obj as T1_WirelessScalarSlot);
            }
            else if (obj is T1_WirelessVibrationSlot)// && T_RootCard[ip].T_WirelessVibrationSlot.Contains(obj as T1_WirelessVibrationSlot))
            {
                T_RootCard[ip].T_WirelessVibrationSlot.Remove(obj as T1_WirelessVibrationSlot);
            }
            else if (obj is T1_DivFreInfo)// && T_RootCard[ip].T_DivFreInfo.Contains(obj as T1_DivFreInfo))
            {
                T_RootCard[ip].T_DivFreInfo.Remove(obj as T1_DivFreInfo);
            }
        }

        private void delete(string ip, string name, long id)
        {
            if (name == "T_Organization")
            {
                var obj = T_Organization[ip].Where(p => p.id == id).FirstOrDefault();
                T_Organization[ip].Remove(obj);
            }
            else if (name == "T_Device")
            {
                var obj = T_Device[ip].Where(p => p.id == id).FirstOrDefault();
                T_Device[ip].Remove(obj);
            }
            else if (name == "T_Item")
            {
                var obj = T_Item[ip].Where(p => p.id == id).FirstOrDefault();
                T_Item[ip].Remove(obj);
            }
            else if (name == "T_User")
            {
                var obj = T_User[ip].Where(p => p.id == id).FirstOrDefault();
                T_User[ip].Remove(obj);
            }
            else if (name == "T_Role")
            {
                var obj = T_Role[ip].Where(p => p.id == id).FirstOrDefault();
                T_Role[ip].Remove(obj);
            }
            else if (name == "T_Menu")
            {
                var obj = T_Menu[ip].Where(p => p.id == id).FirstOrDefault();
                T_Menu[ip].Remove(obj);
            }
            else if (name == "T_OrganizationPrivilege")
            {
                var obj = T_OrganizationPrivilege[ip].Where(p => p.id == id).FirstOrDefault();
                T_OrganizationPrivilege[ip].Remove(obj);
            }
            else if (name == "T_MainControlCard")
            {
                var obj = T_RootCard[ip].T_MainControlCard.Where(p => p.id == id).FirstOrDefault();
                T_RootCard[ip].T_MainControlCard.Remove(obj);
            }
            else if (name == "T_WireMatchingCard")
            {
                var obj = T_RootCard[ip].T_WireMatchingCard.Where(p => p.id == id).FirstOrDefault();
                T_RootCard[ip].T_WireMatchingCard.Remove(obj);
            }
            else if (name == "T_WirelessReceiveCard")
            {
                var obj = T_RootCard[ip].T_WirelessReceiveCard.Where(p => p.id == id).FirstOrDefault();
                T_RootCard[ip].T_WirelessReceiveCard.Remove(obj);
            }
            else if (name == "T_TransmissionCard")
            {
                var obj = T_RootCard[ip].T_TransmissionCard.Where(p => p.id == id).FirstOrDefault();
                T_RootCard[ip].T_TransmissionCard.Remove(obj);
            }
            else if (name == "T_AbstractChannelInfo")
            {
                var obj = T_RootCard[ip].T_AbstractChannelInfo.Where(p => p.id == id).FirstOrDefault();
                T_RootCard[ip].T_AbstractChannelInfo.Remove(obj);
            }
            else if (name == "T_IEPEChannelInfo")
            {
                var obj = T_RootCard[ip].T_IEPEChannelInfo.Where(p => p.id == id).FirstOrDefault();
                T_RootCard[ip].T_IEPEChannelInfo.Remove(obj);
            }
            else if (name == "T_EddyCurrentDisplacementChannelInfo")
            {
                var obj = T_RootCard[ip].T_EddyCurrentDisplacementChannelInfo.Where(p => p.id == id).FirstOrDefault();
                T_RootCard[ip].T_EddyCurrentDisplacementChannelInfo.Remove(obj);
            }
            else if (name == "T_EddyCurrentKeyPhaseChannelInfo")
            {
                var obj = T_RootCard[ip].T_EddyCurrentKeyPhaseChannelInfo.Where(p => p.id == id).FirstOrDefault();
                T_RootCard[ip].T_EddyCurrentKeyPhaseChannelInfo.Remove(obj);
            }
            else if (name == "T_EddyCurrentTachometerChannelInfo")
            {
                var obj = T_RootCard[ip].T_EddyCurrentTachometerChannelInfo.Where(p => p.id == id).FirstOrDefault();
                T_RootCard[ip].T_EddyCurrentTachometerChannelInfo.Remove(obj);
            }
            else if (name == "T_DigitTachometerChannelInfo")
            {
                var obj = T_RootCard[ip].T_DigitTachometerChannelInfo.Where(p => p.id == id).FirstOrDefault();
                T_RootCard[ip].T_DigitTachometerChannelInfo.Remove(obj as T1_DigitTachometerChannelInfo);
            }
            else if (name == "T_AnalogRransducerInChannelInfo")
            {
                var obj = T_RootCard[ip].T_AnalogRransducerInChannelInfo.Where(p => p.id == id).FirstOrDefault();
                T_RootCard[ip].T_AnalogRransducerInChannelInfo.Remove(obj);
            }
            else if (name == "T_RelayChannelInfo")
            {
                var obj = T_RootCard[ip].T_RelayChannelInfo.Where(p => p.id == id).FirstOrDefault();
                T_RootCard[ip].T_RelayChannelInfo.Remove(obj);
            }
            else if (name == "T_DigitRransducerInChannelInfo")
            {
                var obj = T_RootCard[ip].T_DigitRransducerInChannelInfo.Where(p => p.id == id).FirstOrDefault();
                T_RootCard[ip].T_DigitRransducerInChannelInfo.Remove(obj);
            }
            else if (name == "T_DigitRransducerOutChannelInfo")
            {
                var obj = T_RootCard[ip].T_DigitRransducerOutChannelInfo.Where(p => p.id == id).FirstOrDefault();
                T_RootCard[ip].T_DigitRransducerOutChannelInfo.Remove(obj);
            }
            else if (name == "T_AnalogRransducerOutChannelInfo")
            {
                var obj = T_RootCard[ip].T_AnalogRransducerOutChannelInfo.Where(p => p.id == id).FirstOrDefault();
                T_RootCard[ip].T_AnalogRransducerOutChannelInfo.Remove(obj);
            }
            else if (name == "T_WirelessScalarChannelInfo")
            {
                var obj = T_RootCard[ip].T_WirelessScalarChannelInfo.Where(p => p.id == id).FirstOrDefault();
                T_RootCard[ip].T_WirelessScalarChannelInfo.Remove(obj);
            }
            else if (name == "T_WirelessVibrationChannelInfo")
            {
                var obj = T_RootCard[ip].T_WirelessVibrationChannelInfo.Where(p => p.id == id).FirstOrDefault();
                T_RootCard[ip].T_WirelessVibrationChannelInfo.Remove(obj);
            }
            else if (name == "T_AbstractSlotInfo")
            {
                var obj = T_RootCard[ip].T_AbstractSlotInfo.Where(p => p.id == id).FirstOrDefault();
                T_RootCard[ip].T_AbstractSlotInfo.Remove(obj);
            }
            else if (name == "T_IEPESlot")
            {
                var obj = T_RootCard[ip].T_IEPESlot.Where(p => p.id == id).FirstOrDefault();
                T_RootCard[ip].T_IEPESlot.Remove(obj);
            }
            else if (name == "T_EddyCurrentDisplacementSlot")
            {
                var obj = T_RootCard[ip].T_EddyCurrentDisplacementSlot.Where(p => p.id == id).FirstOrDefault();
                T_RootCard[ip].T_EddyCurrentDisplacementSlot.Remove(obj);
            }
            else if (name == "T_EddyCurrentKeyPhaseSlot")
            {
                var obj = T_RootCard[ip].T_EddyCurrentKeyPhaseSlot.Where(p => p.id == id).FirstOrDefault();
                T_RootCard[ip].T_EddyCurrentKeyPhaseSlot.Remove(obj);
            }
            else if (name == "T_EddyCurrentTachometerSlot")
            {
                var obj = T_RootCard[ip].T_EddyCurrentTachometerSlot.Where(p => p.id == id).FirstOrDefault();
                T_RootCard[ip].T_EddyCurrentTachometerSlot.Remove(obj);
            }
            else if (name == "T_DigitTachometerSlot")
            {
                var obj = T_RootCard[ip].T_DigitTachometerSlot.Where(p => p.id == id).FirstOrDefault();
                T_RootCard[ip].T_DigitTachometerSlot.Remove(obj);
            }
            else if (name == "T_AnalogRransducerInSlot")
            {
                var obj = T_RootCard[ip].T_AnalogRransducerInSlot.Where(p => p.id == id).FirstOrDefault();
                T_RootCard[ip].T_AnalogRransducerInSlot.Remove(obj);
            }
            else if (name == "T_RelaySlot")
            {
                var obj = T_RootCard[ip].T_RelaySlot.Where(p => p.id == id).FirstOrDefault();
                T_RootCard[ip].T_RelaySlot.Remove(obj);
            }
            else if (name == "T_DigitRransducerInSlot")
            {
                var obj = T_RootCard[ip].T_DigitRransducerInSlot.Where(p => p.id == id).FirstOrDefault();
                T_RootCard[ip].T_DigitRransducerInSlot.Remove(obj);
            }
            else if (name == "T_DigitRransducerOutSlot")
            {
                var obj = T_RootCard[ip].T_DigitRransducerOutSlot.Where(p => p.id == id).FirstOrDefault();
                T_RootCard[ip].T_DigitRransducerOutSlot.Remove(obj);
            }
            else if (name == "T_AnalogRransducerOutSlot")
            {
                var obj = T_RootCard[ip].T_AnalogRransducerOutSlot.Where(p => p.id == id).FirstOrDefault();
                T_RootCard[ip].T_AnalogRransducerOutSlot.Remove(obj);
            }
            else if (name == "T_WirelessScalarSlot")
            {
                var obj = T_RootCard[ip].T_WirelessScalarSlot.Where(p => p.id == id).FirstOrDefault();
                T_RootCard[ip].T_WirelessScalarSlot.Remove(obj);
            }
            else if (name == "T_WirelessVibrationSlot")
            {
                var obj = T_RootCard[ip].T_WirelessVibrationSlot.Where(p => p.id == id).FirstOrDefault();
                T_RootCard[ip].T_WirelessVibrationSlot.Remove(obj);
            }
            else if (name == "T_DivFreInfo")
            {
                var obj = T_RootCard[ip].T_DivFreInfo.Where(p => p.id == id).FirstOrDefault();
                T_RootCard[ip].T_DivFreInfo.Remove(obj);
            }
        }

        public async Task<Tuple<T1_Role, T1_User>> UserLogin(string ip, string user, string pwd)
        {            
            return await Task.Run(() =>
            {             
                var client = new DataProvider(ip, LocalSetting.ServerPort, LocalSetting.MajorVersion, LocalSetting.MinorVersion);

                var queryResult = client.Query<T_User>(null, "(Name = @0 and (Password = @1 or Password = @2))", new object[] { user, pwd, MyEncrypt.EncryptDES(pwd) });
                //先判断是不是OK          
                if (queryResult.IsOK && queryResult.ResponseItem.Count > 0)
                {
                    //添加成功
                    var t_user = ClassCopyHelper.AutoCopy<T_User, T1_User>(queryResult.ResponseItem[0]);
                    var roleResult = client.Query<T_Role>(null, "(Guid = @0)", new object[] { t_user.T_Role_Guid });
                    if (roleResult.IsOK && roleResult.ResponseItem.Count > 0)
                    {
                        return new Tuple<T1_Role, T1_User>(ClassCopyHelper.AutoCopy<T_Role, T1_Role>(roleResult.ResponseItem[0]), t_user);
                    }
                    else
                    {
                        EventAggregatorService.Instance.EventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("服务器" + ip + " " + "用户登录", new Exception(roleResult.ErrorMessage)));
                        return null;
                    }
                }
                else
                {                 
                    //ErrorMessage是错误信息
                    EventAggregatorService.Instance.EventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("服务器" + ip + " " + "用户登录", new Exception(queryResult.ErrorMessage)));
                    return null;
                }
            });
           
        }

        public async Task<string> UserPing(string ip)
        {
            return await Task.Run(() =>
            {
                var client = new DataProvider(ip, LocalSetting.ServerPort, LocalSetting.MajorVersion, LocalSetting.MinorVersion);

                var queryResult = client.Query<T_Role>(null, "where 1 = 1", null);
                //先判断是不是OK          
                if (queryResult.IsOK)
                {
                    return "OK";
                }
                else
                {
                    string error = queryResult.ErrorMessage;
                    if (queryResult.ErrorType == "#ServerVersionUnmatch")
                    {
                        error = "版本" + LocalSetting.Version + "与服务器版本" + error + "不匹配！#";
                    }
                    else
                    {
                        error = "请确定服务器正常#" + error;
                    }
                    //ErrorMessage是错误信息
                    EventAggregatorService.Instance.EventAggregator.GetEvent<ThrowExceptionEvent>().Publish(Tuple.Create<string, Exception>("服务器" + "数据库访问", new Exception(error)));
                    return error.Substring(0, error.IndexOf("#")) + "...";
                }
            });

        }
    }
}
