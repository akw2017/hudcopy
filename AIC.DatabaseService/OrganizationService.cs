using AIC.Core.LMModels;
using AIC.Core.Models;
using AIC.Core.OrganizationModels;
using AIC.DatabaseService.Models;
using AIC.ServiceInterface;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIC.HardwareService
{
    public class OrganizationService : IOrganizationService
    {
        private readonly ICardProcess _cardProcess;
        private readonly IDatabaseComponent _databaseComponent;

        #region 字段
        public ObservableCollection<OrganizationTreeItemViewModel> OrganizationTreeItems { get; private set; }
        public ObservableCollection<OrganizationTreeItemViewModel> RecycledTreeItems { get; private set; }
        public List<ItemTreeItemViewModel> ItemTreeItems { get; private set; }
        public List<DivFreTreeItemViewModel> DivFreTreeItems { get; private set; }
        public Dictionary<string, List<T1_DivFreInfo>> T_DivFreInfo { get; private set; }
        private Dictionary<string, List<T1_OrganizationPrivilege>> T_OrganizationPrivilege { get; set; }
        #endregion

        public OrganizationService(ICardProcess cardProcess, IDatabaseComponent databaseComponent)
        {
            _cardProcess = cardProcess;
            _databaseComponent = databaseComponent;

            OrganizationTreeItems = new ObservableCollection<OrganizationTreeItemViewModel>();           
            RecycledTreeItems = new ObservableCollection<OrganizationTreeItemViewModel>();
            ItemTreeItems = new List<ItemTreeItemViewModel>();
            DivFreTreeItems = new List<DivFreTreeItemViewModel>();
            //T_Organization = new Dictionary<string, List<T1_Organization>>();
            T_OrganizationPrivilege = new Dictionary<string, List<T1_OrganizationPrivilege>>();
            //T_Item = new Dictionary<string, List<T1_Item>>();
            T_DivFreInfo = new Dictionary<string, List<T1_DivFreInfo>>();
        }

        public void Initialize()
        {
            
        }
        public void InitOrganizations(bool isadmin)
        {
            //从数据库取出，去重复
            OrganizationTreeItems.Clear();           
            RecycledTreeItems.Clear();
            //测点数据
            ItemTreeItems.Clear();

            foreach (var item in _databaseComponent.GetServerIPCategory())
            {
                GetOrganizationFromDatabase(item, isadmin);                             
            }
        }
        public void AddItem(ItemTreeItemViewModel item)
        {
            if (item == null)
            {
                return;
            }
            if (!ItemTreeItems.Contains(item))
            {
                ItemTreeItems.Add(item);                
            }
        }
        public void DeleteItem(ItemTreeItemViewModel item)
        {
            if (item == null)
            {
                return;
            }
            if (ItemTreeItems.Contains(item))
            {
                ItemTreeItems.Remove(item);
            }
        }
        public void AddDivFre(DivFreTreeItemViewModel divfre)
        {
            if (divfre == null)
            {
                return;
            }
            if (!DivFreTreeItems.Contains(divfre))
            {
                DivFreTreeItems.Add(divfre);
            }
        }
        public void DeleteDivFre(DivFreTreeItemViewModel divfre)
        {
            if (divfre == null)
            {
                return;
            }
            if (DivFreTreeItems.Contains(divfre))
            {
                DivFreTreeItems.Remove(divfre);
            }
        }  
        public void SetDivFres()
        {
            T_DivFreInfo.Clear();
            foreach (var serverip in _databaseComponent.GetServerIPCategory())
            {
                T_DivFreInfo.Add(serverip, _databaseComponent.GetRootCard(serverip).T_DivFreInfo);
            }
        }
        public void SetUserOrganizationPrivilege(Guid? guid)
        {
            T_OrganizationPrivilege.Clear();
            foreach (var organizationPrivilege in _databaseComponent.T_OrganizationPrivilege)
            {
                var userorganizationPrivilege = (from p in organizationPrivilege.Value where p.Guid == guid select p).ToList();
                T_OrganizationPrivilege.Add(organizationPrivilege.Key, userorganizationPrivilege);
            }
        }
        public void SaveOrganizationToDatabase(string ip)//测试，废弃
        {
            //T_Organization[ip].Clear();
            //T_Item[ip].Clear();           

            //var organizationlist = _cardProcess.GetOrganizations(OrganizationTreeItems);
            //foreach(var organization in organizationlist)
            //{
            //    T_Organization[ip].Add(organization.T_Organization);
            //    if (organization is ItemTreeItemViewModel)
            //    {
            //        var item = organization as ItemTreeItemViewModel;
            //        if (item.IsPaired == true)
            //        {
            //            T_Item[ip].Add(item.T_Item);
            //        }
            //    }
            //}
            ////回收站
            //foreach(var organization in RecycledTreeItems[0].Children)
            //{
            //    T_Organization[ip].Add(organization.T_Organization);
            //}
        }
        public void GetOrganizationFromDatabase(string ip, bool isadmin)
        {
            var roots = from p in _databaseComponent.GetOrganizationData(ip) where p.Level == 0 && p.Is_Disabled == false orderby p.Sort_No select p;
            foreach(var root in roots.Distinct())//去重复
            {
                if (root.NodeType == 0 && root.Is_Disabled == false)
                {
                    T1_OrganizationPrivilege t_organization = null;
                    if (isadmin == false)
                    {
                        t_organization = (from p in T_OrganizationPrivilege[ip] where p.T_Organization_Guid == root.Guid select p).FirstOrDefault();
                    }                       
                    if (t_organization != null || isadmin == true)
                    {
                        OrganizationTreeItemViewModel organization = new OrganizationTreeItemViewModel(root, ip);
                        OrganizationTreeItems.Add(organization);
                        GetSubOrganization(organization, ip, isadmin);
                    }
                }                
            }

            //新建回收站
            OrganizationTreeItemViewModel recyclednode = new OrganizationTreeItemViewModel("回收站", 0, ip);
            recyclednode.IsExpanded = true;
            RecycledTreeItems.Add(recyclednode);

            //回收站,有问题,htzk123，忘记是否修复了没有
            var recycles = from p in _databaseComponent.GetItemData(ip) where p.Is_Disabled == true orderby p.Modify_Time select p;
            foreach (var recycle in recycles.Distinct())//去重复
            {
                ItemTreeItemViewModel organization = new ItemTreeItemViewModel(recycle);
                recyclednode.AddChild(organization);

                ItemTreeItems.Add(organization);
            }
        }
        private void GetSubOrganization(OrganizationTreeItemViewModel parent_organization, string ip, bool isadmin)
        {
            if (parent_organization == null)
            {
                return;
            }
            var childs = from p in _databaseComponent.GetOrganizationData(ip) where p.Parent_Guid == parent_organization.T_Organization.Guid && p.Is_Disabled == false orderby p.Sort_No select p;
            foreach (var child in childs)
            {
                if (child.NodeType == 0 && child.Is_Disabled == false)
                {
                    T1_OrganizationPrivilege t_organization = null;
                    if (isadmin == false)
                    {
                        t_organization = (from p in T_OrganizationPrivilege[ip] where p.T_Organization_Guid == child.Guid select p).FirstOrDefault();
                    }
                    if (t_organization != null || isadmin == true)
                    {
                        OrganizationTreeItemViewModel organization = new OrganizationTreeItemViewModel(child, parent_organization);
                        //organization.T_Organization = child;
                        parent_organization.AddChild(organization);
                        GetSubOrganization(organization, ip, isadmin);
                    }
                }
                else if (child.NodeType == 1 && child.Is_Disabled == false)
                {
                    T1_OrganizationPrivilege t_organization = null;
                    if (isadmin == false)
                    {
                        t_organization = (from p in T_OrganizationPrivilege[ip] where p.T_Organization_Guid == child.Guid select p).FirstOrDefault();
                    }
                    if (t_organization != null || isadmin == true)
                    {
                        DeviceTreeItemViewModel organization = new DeviceTreeItemViewModel(child, parent_organization);
                        parent_organization.AddChild(organization);

                        GetSubOrganization(organization, ip, isadmin);
                    }
                }
                else if (child.NodeType == 2 && child.Is_Disabled == false)
                {
                    ItemTreeItemViewModel organization = new ItemTreeItemViewModel(child, parent_organization as DeviceTreeItemViewModel);
                    parent_organization.AddChild(organization);
                    //测点数据
                    var t_item = _databaseComponent.GetItemData(ip).Where(p => p.Is_Disabled == false && p.Guid == organization.T_Organization.Guid).FirstOrDefault();
                    if (t_item != null)
                    {
                        organization.RecoverBind(t_item);                       
                    }
                    ItemTreeItems.Add(organization);

                    GetSubOrganization(organization, ip, isadmin);
                }
                else if (child.NodeType == 3 && child.Is_Disabled == false)
                {                    
                    DivFreTreeItemViewModel organization = new DivFreTreeItemViewModel(child, parent_organization as ItemTreeItemViewModel);                   
                    
                    //分频数据
                    var t_divfre = T_DivFreInfo[ip].Where(p => p.Guid == organization.T_Organization.Guid).FirstOrDefault();
                    if (t_divfre != null)//分频一定不为空
                    {
                        parent_organization.AddChild(organization);//否则丢弃无效分频数据

                        organization.T_DivFreInfo = t_divfre;
                        DivFreTreeItems.Add(organization);

                        //GetSubOrganization(organization, ip, isadmin);//分频下无节点
                    }
                }
            }
        }
        public void SaveItemToDatabase(string ip)////测试，废弃
        {
            //T_Item[ip].Clear();
            //var itemlist = from p in _cardProcess.GetItems(OrganizationTreeItems) where p.IsPaired == true select p;
            //foreach (var item in itemlist)
            //{
            //    T_Item[ip].Add(item.T_Item);
            //}
        }
        public void GetItemFromDatabase(string ip)////测试，废弃
        { 
            //从数据库取出，去重复
            var treeitems = _cardProcess.GetItems(OrganizationTreeItems);
            if (treeitems != null)
            {
                foreach (var t_item in _databaseComponent.GetItemData(ip).Where(p=> p.Is_Disabled == false))
                {
                    var item = (from p in treeitems where p.T_Organization.Guid == t_item.Guid select p).FirstOrDefault();
                    if (item != null)
                    {
                        item.RecoverBind(t_item);
                    }
                }
            }
        }          

        #region 测试
        private OrganizationTreeItemViewModel node1;
        private OrganizationTreeItemViewModel node1_1;
        private OrganizationTreeItemViewModel node1_2;
        private OrganizationTreeItemViewModel node1_3;
        private DeviceTreeItemViewModel node1_1_1;
        private DeviceTreeItemViewModel node1_1_2;
        private DeviceTreeItemViewModel node1_1_3;
        private ItemTreeItemViewModel node1_1_1_1;
        private ItemTreeItemViewModel node1_1_1_2;
        private ItemTreeItemViewModel node1_1_1_3;
        private ItemTreeItemViewModel node1_1_2_1;
        private ItemTreeItemViewModel node1_1_2_2;
        private ItemTreeItemViewModel node1_1_2_3;
        private ItemTreeItemViewModel node1_1_3_1;
        private ItemTreeItemViewModel node1_1_3_2;
        private ItemTreeItemViewModel node1_1_3_3;
        private OrganizationTreeItemViewModel node2;
        private OrganizationTreeItemViewModel node3;
        private OrganizationTreeItemViewModel node4;
        private void getTree()
        {
            node1 = new OrganizationTreeItemViewModel("北京航天智控测试工厂", 0, "192.168.0.210");
            node1_1 = new OrganizationTreeItemViewModel("厂区1", 2, node1);
            node1_2 = new OrganizationTreeItemViewModel("厂区2", 1, node1);
            node1_3 = new OrganizationTreeItemViewModel("厂区3", 0, node1);
            node1_1_1 = new DeviceTreeItemViewModel("设备1", 2, node1_1);
            node1_1_2 = new DeviceTreeItemViewModel("设备2", 1, node1_1);
            node1_1_3 = new DeviceTreeItemViewModel("设备3", 0, node1_1);
            node1_1_1_1 = new ItemTreeItemViewModel("测点1", 2, node1_1_1);
            node1_1_1_2 = new ItemTreeItemViewModel("测点2", 1, node1_1_1);
            node1_1_1_3 = new ItemTreeItemViewModel("测点3", 0, node1_1_1);
            node1_1_2_1 = new ItemTreeItemViewModel("测点4", 2, node1_1_2);
            node1_1_2_2 = new ItemTreeItemViewModel("测点5", 1, node1_1_2);
            node1_1_2_3 = new ItemTreeItemViewModel("测点6", 0, node1_1_2);
            node1_1_3_1 = new ItemTreeItemViewModel("测点7", 2, node1_1_3);
            node1_1_3_2 = new ItemTreeItemViewModel("测点8", 1, node1_1_3);
            node1_1_3_3 = new ItemTreeItemViewModel("测点9", 0, node1_1_3);
            node2 = new OrganizationTreeItemViewModel("测试工厂1", 3,"");
            node3 = new OrganizationTreeItemViewModel("测试工厂2", 2,"");
            node4 = new OrganizationTreeItemViewModel("测试工厂3", 1,"");

            OrganizationTreeItems.Clear();

            List<ItemTreeItemViewModel> item1 = new List<ItemTreeItemViewModel>();
            item1.Add(node1_1_1_1);
            item1.Add(node1_1_1_2);
            item1.Add(node1_1_1_3);
            node1_1_1.AddChildRange(from p in item1 orderby p.T_Organization.Sort_No select p as OrganizationTreeItemViewModel);

            List<ItemTreeItemViewModel> item2 = new List<ItemTreeItemViewModel>();
            item2.Add(node1_1_2_1);
            item2.Add(node1_1_2_2);
            item2.Add(node1_1_2_3);
            node1_1_2.AddChildRange(from p in item2 orderby p.T_Organization.Sort_No select p as OrganizationTreeItemViewModel);

            List<ItemTreeItemViewModel> item3 = new List<ItemTreeItemViewModel>();
            item3.Add(node1_1_3_1);
            item3.Add(node1_1_3_2);
            item3.Add(node1_1_3_3);
            node1_1_3.AddChildRange(from p in item3 orderby p.T_Organization.Sort_No select p as OrganizationTreeItemViewModel);

            List<DeviceTreeItemViewModel> device = new List<DeviceTreeItemViewModel>();
            device.Add(node1_1_1);
            device.Add(node1_1_2);
            device.Add(node1_1_3);
            node1_1.AddChildRange(from p in device orderby p.T_Organization.Sort_No select p as OrganizationTreeItemViewModel);

            List<OrganizationTreeItemViewModel> organization = new List<OrganizationTreeItemViewModel>();
            organization.Add(node1_1);
            organization.Add(node1_2);
            organization.Add(node1_3);
            node1.AddChildRange(from p in organization orderby p.T_Organization.Sort_No select p as OrganizationTreeItemViewModel);

            List<OrganizationTreeItemViewModel> root = new List<OrganizationTreeItemViewModel>();
            root.Add(node1);
            root.Add(node2);
            root.Add(node3);
            root.Add(node4);
            OrganizationTreeItems.AddRange(from p in root orderby p.T_Organization.Sort_No select p);

            RecycledTreeItems.Clear();
            OrganizationTreeItemViewModel recyclednode = new OrganizationTreeItemViewModel("回收站", 0, "192.168.0.1");
            recyclednode.IsExpanded = true;
            RecycledTreeItems.Add(recyclednode);

            //SaveOrganizationToDatabase();
            //SaveItemToDatabase();
        }
        #endregion

    }
}
