using AIC.Core.Models;
using AIC.Core.OrganizationModels;
using AIC.PDAPage.Models;
using AIC.PDAPage.ViewModels;
using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace AIC.PDAPage.Views
{
    /// <summary>
    /// Interaction logic for MainControlCardCopyWin.xaml
    /// </summary>
    public partial class TreeNodeDebugInfoWin : MetroWindow
    {
        public TreeNodeDebugInfoWin(OrganizationTreeItemViewModel organizationTree)
        { 
            InitializeComponent();
            
            List<string> infos = new List<string>();
            infos.Add("id " + organizationTree.T_Organization.id.ToString());
            infos.Add("Name " + organizationTree.T_Organization.Name);
            infos.Add("Code " + organizationTree.T_Organization.Code);
            infos.Add("Guid " + organizationTree.T_Organization.Guid.ToString());
            infos.Add("Level " + organizationTree.T_Organization.Level.ToString());
            infos.Add("Sort_No " + organizationTree.T_Organization.Sort_No.ToString());
            infos.Add("Create_Time " + organizationTree.T_Organization.Create_Time.ToString());
            infos.Add("Modify_Time " + organizationTree.T_Organization.Modify_Time.ToString());
            infos.Add("Is_Disabled " + organizationTree.T_Organization.Is_Disabled.ToString());
            infos.Add("Parent_Code " + organizationTree.T_Organization.Parent_Code);
            infos.Add("Parent_Guid " + organizationTree.T_Organization.Parent_Guid.ToString());
            infos.Add("Parent_Level " + organizationTree.T_Organization.Parent_Level.ToString());
            infos.Add("Remarks " + organizationTree.T_Organization.Remarks);
            infos.Add("NodeType " + organizationTree.T_Organization.NodeType.ToString());

            ItemTreeItemViewModel itemTree = organizationTree as ItemTreeItemViewModel;
            if (itemTree != null && itemTree.T_Item != null)
            {
                infos.Add("id " + itemTree.T_Item.id.ToString());
                infos.Add("Guid " + itemTree.T_Item.Guid.ToString());
                infos.Add("ChannelHDID " + itemTree.T_Item.ChannelHDID);
                infos.Add("Name " + itemTree.T_Item.Name);
                infos.Add("Code " + itemTree.T_Item.Code);
                infos.Add("CardNum " + itemTree.T_Item.CardNum.ToString());
                infos.Add("SlotNum " + itemTree.T_Item.SlotNum.ToString());
                infos.Add("CHNum " + itemTree.T_Item.CHNum.ToString());
                infos.Add("T_Device_Guid " + itemTree.T_Item.T_Device_Guid.ToString());
                infos.Add("T_Device_Code " + itemTree.T_Item.T_Device_Code);
                infos.Add("Remarks " + itemTree.T_Item.Remarks);
                infos.Add("Create_Time " + itemTree.T_Item.Create_Time.ToString());
                infos.Add("Modify_Time " + itemTree.T_Item.Modify_Time.ToString());
                infos.Add("Sort_No " + itemTree.T_Item.Sort_No.ToString());
                infos.Add("Is_Disabled " + itemTree.T_Item.Is_Disabled.ToString());
                infos.Add("IP " + itemTree.T_Item.IP);              
                infos.Add("Identifier " + itemTree.T_Item.Identifier);
                //infos.Add("ServerIP" + itemTree.T_Item.ServerIP);
                infos.Add("ItemType " + itemTree.T_Item.ItemType.ToString());
                infos.Add("SlaveIdentifier " + itemTree.T_Item.SlaveIdentifier);
            }

            LBoxSort.ItemsSource = infos;
           
        }      

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
           
            this.Close();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }  

}
