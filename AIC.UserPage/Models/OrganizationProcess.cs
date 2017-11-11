using AIC.Core.LMModels;
using AIC.Core.OrganizationModels;
using AIC.Core.UserManageModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIC.UserPage.Models
{
    public class OrganizationProcess
    {
        public static void GetOrganization(IList<OrganizationTreeItemViewModel> organizations, IList<T1_OrganizationPrivilege> t_organizationPrivilege, IList<T1_Organization> t_organization)
        {
            //从数据库取出，去重复
            organizations.Clear();

            var roots = from p in t_organization where p.Level == 0 orderby p.Sort_No select p;
            foreach (var root in roots.Distinct())//去重复
            {
                if (root.NodeType == 0 && root.Is_Disabled == false)
                {
                    OrganizationTreeItemViewModel organization = new OrganizationTreeItemViewModel(root.Name, root.Sort_No, "");//htzk123 ,可能有问题，没有加入IP
                    organization.T_Organization = root;
                    if ((from p in t_organizationPrivilege where p.T_Organization_Guid == root.Guid select p).Count() >= 1)
                    {
                        organization.IsChecked = true;
                    }
                    organizations.Add(organization);
                    GetSubOrganization(organization, t_organizationPrivilege, t_organization);
                }
            }
        }

        public static void GetSubOrganization(OrganizationTreeItemViewModel parent_organization, IList<T1_OrganizationPrivilege> t_organizationPrivilege, IList<T1_Organization> t_organization)
        {
            if (parent_organization == null)
            {
                return;
            }
            var childs = from p in t_organization where p.Parent_Guid == parent_organization.T_Organization.Guid orderby p.Sort_No select p;
            foreach (var child in childs)
            {
                if (child.NodeType == 0 && child.Is_Disabled == false)
                {
                    OrganizationTreeItemViewModel organization = new OrganizationTreeItemViewModel(child.Name, child.Sort_No, parent_organization);
                    organization.T_Organization = child;
                    if ((from p in t_organizationPrivilege where p.T_Organization_Guid == child.Guid select p).Count() >= 1)
                    {
                        organization.IsChecked = true;
                    }
                    parent_organization.AddChild(organization);
                    GetSubOrganization(organization, t_organizationPrivilege, t_organization);
                }
                else if (child.NodeType == 1 && child.Is_Disabled == false)
                {
                    DeviceTreeItemViewModel organization = new DeviceTreeItemViewModel(child.Name, child.Sort_No, parent_organization);
                    organization.T_Organization = child;
                    if ((from p in t_organizationPrivilege where p.T_Organization_Guid == child.Guid select p).Count() >= 1)
                    {
                        organization.IsChecked = true;
                    }
                    parent_organization.AddChild(organization);
                    GetSubOrganization(organization, t_organizationPrivilege, t_organization);
                }
            }
        }

        public static void GetOrganizationWithIsChecked(IList<OrganizationTreeItemViewModel> organizations, IList<T1_OrganizationPrivilege> t_organizationPrivilege, IList<T1_Organization> t_organization)
        {
            //从数据库取出，去重复
            organizations.Clear();

            var roots = from p in t_organization where p.Level == 0 orderby p.Sort_No select p;
            foreach (var root in roots.Distinct())//去重复
            {
                if (root.NodeType == 0 && root.Is_Disabled == false)
                {
                    OrganizationTreeItemViewModel organization = new OrganizationTreeItemViewModel(root.Name, root.Sort_No,"");//htzk123 ,可能有问题，没有加入IP
                    organization.T_Organization = root;
                    if ((from p in t_organizationPrivilege where p.T_Organization_Guid == root.Guid select p).Count() >= 1)
                    {
                        organization.IsChecked = true;
                        organizations.Add(organization);
                        GetSubOrganizationWithIsChecked(organization, t_organizationPrivilege, t_organization);
                    }                    
                }
            }
        }

        public static void GetSubOrganizationWithIsChecked(OrganizationTreeItemViewModel parent_organization, IList<T1_OrganizationPrivilege> t_organizationPrivilege, IList<T1_Organization> t_organization)
        {
            if (parent_organization == null)
            {
                return;
            }
            var childs = from p in t_organization where p.Parent_Guid == parent_organization.T_Organization.Guid orderby p.Sort_No select p;
            foreach (var child in childs)
            {
                if (child.NodeType == 0 && child.Is_Disabled == false)
                {
                    OrganizationTreeItemViewModel organization = new OrganizationTreeItemViewModel(child.Name, child.Sort_No, parent_organization);
                    organization.T_Organization = child;
                    if ((from p in t_organizationPrivilege where p.T_Organization_Guid == child.Guid select p).Count() >= 1)
                    {
                        organization.IsChecked = true;
                        parent_organization.AddChild(organization);
                        GetSubOrganizationWithIsChecked(organization, t_organizationPrivilege, t_organization);
                    }
                   
                }
                else if (child.NodeType == 1 && child.Is_Disabled == false)
                {
                    DeviceTreeItemViewModel organization = new DeviceTreeItemViewModel(child.Name, child.Sort_No, parent_organization);
                    organization.T_Organization = child;
                    if ((from p in t_organizationPrivilege where p.T_Organization_Guid == child.Guid select p).Count() >= 1)
                    {
                        organization.IsChecked = true;
                        parent_organization.AddChild(organization);
                        GetSubOrganizationWithIsChecked(organization, t_organizationPrivilege, t_organization);
                    }                    
                }
            }
        }

        public static void GetOrganizationCheck(IList<OrganizationTreeItemViewModel> organizations, IList<T1_OrganizationPrivilege> t_organizationPrivilege, Guid guid, string name)
        {
            if (organizations == null || organizations.Count == 0)
            {
                return;
            }

            foreach (var organization in organizations)
            {
                if (organization.IsChecked == true)
                {
                    var t_organization = (from p in t_organizationPrivilege where p.T_Organization_Guid == organization.T_Organization.Guid select p).FirstOrDefault();
                    if (t_organization == null)
                    {
                        t_organizationPrivilege.Add(new T1_OrganizationPrivilege()//新增
                        {
                            id = 0,
                            Guid = guid,
                            Name = name,
                            T_Organization_Code = organization.T_Organization.Code,
                            T_Organization_Guid = organization.T_Organization.Guid
                        });
                    }
                    else
                    {
                        t_organization.Name = name;//改名
                    }
                }
                else
                {
                    var t_organization = (from p in t_organizationPrivilege where p.T_Organization_Guid == organization.T_Organization.Guid select p).FirstOrDefault();
                    if (t_organization != null)//删除
                    {
                        t_organization.Guid = new Guid();
                    }
                }
                GetOrganizationCheck(organization.Children, t_organizationPrivilege, guid, name);
            }
        }
    }
}
