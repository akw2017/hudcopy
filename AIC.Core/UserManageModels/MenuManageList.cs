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
        public MyMenu MenuDataTrendChart { get; set; }
        public MyMenu MenuSystemEventList { get; set; }
        public MyMenu MenuDeviceRunStatus { get; set; }
        public MyMenu MenuDeviceRunAnalyze { get; set; }
        public MyMenu MenuDeviceHourlyData { get; set; }
        public MyMenu MenuExportDBData { get; set; }
        public MyMenu MenuImportDBData { get; set; }
        public MyMenu MenuFilterDBData { get; set; }
        public MyMenu ServerQucikData { get; set; }
        public MyMenu DeviceQucikData { get; set; }
        public MyMenu ItemQucikData { get; set; }

        public static Dictionary<int, MyMenu> Dictionary;

        public MenuManageList()
        {
            MenuUserManage = new MyMenu(0, (string)Application.Current.Resources["menuUserManage"], "/AIC.Resources;component/Images/users.png");
            MenuRoleManage = new MyMenu(1, (string)Application.Current.Resources["menuRoleManage"], "/AIC.Resources;component/Images/user.png");
            MenuMenuManage = new MyMenu(2, (string)Application.Current.Resources["menuMenuManage"], "/AIC.Resources;component/Images/application_edit.png");
            MenuOrganizationManage = new MyMenu(3, (string)Application.Current.Resources["menuOrganizationManage"], "/AIC.Resources;component/Images/chart_organisation.png");
            MenuManageLog = new MyMenu(4, (string)Application.Current.Resources["menuManageLog"], "/AIC.Resources;component/Images/page_error.png");
            MenuServerSetting = new MyMenu(5, (string)Application.Current.Resources["menuServerSetting"], "/AIC.Resources;component/Images/database_gear.png");
            MenuCollectorSetting = new MyMenu(6, (string)Application.Current.Resources["menuCollectorSetting"], "/AIC.Resources;component/Images/brick_edit.png");
            //MenuEquipmentSetting = new MyMenu(7, (string)Application.Current.Resources["menuEquipmentSetting"], "/AIC.Resources;component/Images/chart_organisation.png");
            //MenuOnlineData = new MyMenu(8, (string)Application.Current.Resources["menuOnlineData"]);
            //MenuHistoricalData = new MyMenu(9, (string)Application.Current.Resources["menuHistoricalData"]);
            //MenuAlarmData = new MyMenu(10, (string)Application.Current.Resources["menuAlartmData"]);
            //MenuRunningMonitor = new MyMenu(11, (string)Application.Current.Resources["menuRunningMonitor"]);
            //MenuRunningAnalyze = new MyMenu(12, (string)Application.Current.Resources["menuRunningAnalyze"]);
            MenuOnlineDataList = new MyMenu(13, (string)Application.Current.Resources["menuOnlineDataList"], "/AIC.Resources;component/Images/application_view_detail.png");
            MenuOnlineDataTile = new MyMenu(14, (string)Application.Current.Resources["menuOnlineDataTile"], "/AIC.Resources;component/Images/application_view_tile.png");
            MenuOnlineDataDiagram = new MyMenu(15, (string)Application.Current.Resources["menuOnlineDataDiagram"], "/AIC.Resources;component/Images/application_view_gallery.png");
            MenuOnlineDataOverview = new MyMenu(16, (string)Application.Current.Resources["menuOnlineDataOverview"], "/AIC.Resources;component/Images/application_home.png");
            MenuHistoryDataList = new MyMenu(17, (string)Application.Current.Resources["menuHistoryDataList"], "/AIC.Resources;component/Images/table_save.png");
            MenuHistoryDataDiagram = new MyMenu(18, (string)Application.Current.Resources["menuHistoryDataDiagram"], "/AIC.Resources;component/Images/application_view_gallery.png");
            MenuOnlineDataDiagnosis = new MyMenu(19, (string)Application.Current.Resources["menuOnlineDataDiagnosis"], "/AIC.Resources;component/Images/pill_go.png");
            MenuOnlineDataStatistics = new MyMenu(20, (string)Application.Current.Resources["menuOnlineDataStatistics"], "/AIC.Resources;component/Images/chart_pie.png");
            MenuHistoryDataStatistics = new MyMenu(21, (string)Application.Current.Resources["menuHistoryDataStatistics"], "/AIC.Resources;component/Images/chart_curve.png");
            MenuSystemEventList = new MyMenu(22, (string)Application.Current.Resources["menuSystemEventList"], "/AIC.Resources;component/Images/monitor_lightning.png");
            MenuDataTrendChart = new MyMenu(23, (string)Application.Current.Resources["menuDataTrendChart"], "/AIC.Resources;component/Images/chart_curve.png");
            MenuDeviceRunStatus = new MyMenu(24, (string)Application.Current.Resources["menuDeviceRunStatus"], "/AIC.Resources;component/Images/cog_go.png");
            MenuDeviceRunAnalyze = new MyMenu(25, (string)Application.Current.Resources["menuDeviceRunAnalyze"], "/AIC.Resources;component/Images/cog_error.png");
            MenuDeviceHourlyData = new MyMenu(26, (string)Application.Current.Resources["menuDeviceHourlyData"], "/AIC.Resources;component/Images/cog_edit.png");
            MenuExportDBData = new MyMenu(27, (string)Application.Current.Resources["menuExportDBData"], "/AIC.Resources;component/Images/export32.png");
            MenuImportDBData = new MyMenu(28, (string)Application.Current.Resources["menuImportDBData"], "/AIC.Resources;component/Images/import32.png");
            MenuFilterDBData = new MyMenu(29, (string)Application.Current.Resources["menuFilterDBData"], "/AIC.Resources;component/Images/search.png");
            ServerQucikData = new MyMenu(30, (string)Application.Current.Resources["menuServerQucikData"], "/AIC.Resources;component/Images/search.png");
            DeviceQucikData = new MyMenu(31, (string)Application.Current.Resources["menuDeviceQucikData"], "/AIC.Resources;component/Images/search.png");
            ItemQucikData = new MyMenu(32, (string)Application.Current.Resources["menuItemQucikData"], "/AIC.Resources;component/Images/search.png");
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
            Dictionary.Add(19, MenuOnlineDataDiagnosis);//<!--昌邑石化-->
            Dictionary.Add(20, MenuOnlineDataStatistics);
            Dictionary.Add(21, MenuHistoryDataStatistics);
            Dictionary.Add(22, MenuSystemEventList);
            Dictionary.Add(23, MenuDataTrendChart);
            Dictionary.Add(24, MenuDeviceRunStatus);
            Dictionary.Add(25, MenuDeviceRunAnalyze);
            Dictionary.Add(26, MenuDeviceHourlyData);
            Dictionary.Add(27, MenuExportDBData);
            Dictionary.Add(28, MenuImportDBData);
            Dictionary.Add(29, MenuFilterDBData);
            Dictionary.Add(30, ServerQucikData);
            Dictionary.Add(31, DeviceQucikData);
            Dictionary.Add(32, ItemQucikData);
        }

        public static MyMenu GetMenu(string strname)
        {
            string name = (string)Application.Current.Resources[strname];
            var menu = (from p in Dictionary where p.Value.Name == name select p.Value).FirstOrDefault();
            return menu;
        }

        public static string GetIconPath(int internalNumber)
        {
            var path = (from p in Dictionary where p.Value.Number == internalNumber select p.Value.IconPath).FirstOrDefault();
            return path;
        }
    }
}
