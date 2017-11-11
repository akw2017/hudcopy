using AIC.Core.UserManageModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AIC.Core.UserManageModels
{
    public class MenuManageList
    {
        public MyMenu MenuUserManage { get; set; }
        public MyMenu MenuRoleManage { get; set; }
        public MyMenu MenuMenuManage { get; set; }
        public MyMenu MenuOrganizationManage { get; set; }
        public MyMenu MenuManageLog { get; set; }
        public MyMenu MenuServerSetting { get; set; }
        public MyMenu MenuCollectorSetting { get; set; }
        public MyMenu MenuEquipmentSetting { get; set; }
        public MyMenu MenuOnlineData { get; set; }
        public MyMenu MenuHistoricalData { get; set; }
        public MyMenu MenuAlarmData { get; set; }
        public MyMenu MenuRunningMonitor { get; set; }
        public MyMenu MenuRunningAnalyze { get; set; }
        public MyMenu MenuOnlineDataList { get; set; }
        public MyMenu MenuOnlineDataTile { get; set; }
        public MyMenu MenuOnlineDataDiagram { get; set; }
        public MyMenu MenuOnlineDataOverview { get; set; }
        public MyMenu MenuHistoryDataList { get; set; }
        public MyMenu MenuHistoryDataDiagram { get; set; }
        public MyMenu MenuOnlineDataDiagnosis { get; set; }
        public MyMenu MenuOnlineDataStatistics { get; set; }
        public MyMenu MenuHistoryDataStatistics { get; set; }

        public Dictionary<int, MyMenu> Dictionary { get; set; }

        public MenuManageList()
        {
            MenuUserManage = new MyMenu(0, (string)Application.Current.Resources["menuUserManage"]);
            MenuRoleManage = new MyMenu(1, (string)Application.Current.Resources["menuRoleManage"]);
            MenuMenuManage = new MyMenu(2, (string)Application.Current.Resources["menuMenuManage"]);
            MenuOrganizationManage = new MyMenu(3, (string)Application.Current.Resources["menuOrganizationManage"]);
            MenuManageLog = new MyMenu(4, (string)Application.Current.Resources["menuManageLog"]);
            MenuServerSetting = new MyMenu(5, (string)Application.Current.Resources["menuServerSetting"]);
            MenuCollectorSetting = new MyMenu(6, (string)Application.Current.Resources["menuCollectorSetting"]);
            MenuEquipmentSetting = new MyMenu(7, (string)Application.Current.Resources["menuEquipmentSetting"]);
            MenuOnlineData = new MyMenu(8, (string)Application.Current.Resources["menuOnlineData"]);
            MenuHistoricalData = new MyMenu(9, (string)Application.Current.Resources["menuHistoricalData"]);
            MenuAlarmData = new MyMenu(10, (string)Application.Current.Resources["menuAlartmData"]);
            MenuRunningMonitor = new MyMenu(11, (string)Application.Current.Resources["menuRunningMonitor"]);
            MenuRunningAnalyze = new MyMenu(12, (string)Application.Current.Resources["menuRunningAnalyze"]);
            MenuOnlineDataList = new MyMenu(13, (string)Application.Current.Resources["menuOnlineDataList"]);
            MenuOnlineDataTile = new MyMenu(14, (string)Application.Current.Resources["menuOnlineDataTile"]);
            MenuOnlineDataDiagram = new MyMenu(15, (string)Application.Current.Resources["menuOnlineDataDiagram"]);
            MenuOnlineDataOverview = new MyMenu(16, (string)Application.Current.Resources["menuOnlineDataOverview"]);
            MenuHistoryDataList = new MyMenu(17, (string)Application.Current.Resources["menuHistoryDataList"]);
            MenuHistoryDataDiagram = new MyMenu(18, (string)Application.Current.Resources["menuHistoryDataDiagram"]);
            MenuOnlineDataDiagnosis = new MyMenu(19, (string)Application.Current.Resources["menuOnlineDataDiagnosis"]);
            MenuOnlineDataStatistics = new MyMenu(20, (string)Application.Current.Resources["menuOnlineDataStatistics"]);
            MenuHistoryDataStatistics = new UserManageModels.MyMenu(21, (string)Application.Current.Resources["menuHistoryDataStatistics"]);
            Dictionary = new Dictionary<int, MyMenu>();
            Dictionary.Add(0, MenuUserManage);
            Dictionary.Add(1, MenuRoleManage);
            Dictionary.Add(2, MenuMenuManage);
            Dictionary.Add(3, MenuOrganizationManage);
            Dictionary.Add(4, MenuManageLog);
            Dictionary.Add(5, MenuServerSetting);
            Dictionary.Add(6, MenuCollectorSetting);
            //Dictionary.Add(7, MenuEquipmentSetting);
            //Dictionary.Add(8, MenuOnlineData);
            //Dictionary.Add(9, MenuHistoricalData);
            //Dictionary.Add(10, MenuAlarmData);
            //Dictionary.Add(11, MenuRunningMonitor);
            //Dictionary.Add(12, MenuRunningAnalyze);
            Dictionary.Add(13, MenuOnlineDataList);
            Dictionary.Add(14, MenuOnlineDataTile);
            Dictionary.Add(15, MenuOnlineDataDiagram);
            Dictionary.Add(16, MenuOnlineDataOverview);
            Dictionary.Add(17, MenuHistoryDataList);
            Dictionary.Add(18, MenuHistoryDataDiagram);
            //Dictionary.Add(19, MenuOnlineDataDiagnosis);
            //Dictionary.Add(20, MenuOnlineDataStatistics);
            //Dictionary.Add(21, MenuHistoryDataStatistics);
        }
    }
}
