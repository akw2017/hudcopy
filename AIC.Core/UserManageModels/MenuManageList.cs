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
        public MyMenu MenuDeviceRunTime { get; set; }
        public MyMenu MenuDeviceRunAnalyze { get; set; }
        public MyMenu MenuDeviceHourlyData { get; set; }
        public MyMenu MenuExportDBData { get; set; }
        public MyMenu MenuImportDBData { get; set; }
        public MyMenu MenuFilterDBData { get; set; }
        public MyMenu MenuServerQucikData { get; set; }
        public MyMenu MenuDeviceQucikData { get; set; }
        public MyMenu MenuItemQucikData { get; set; }
        public MyMenu MenuBPNetWorks { get; set; }
        public MyMenu MenuSOMNetWorks { get; set; }
        public MyMenu MenuDeviceFaultDiagnose { get; set; }
        public MyMenu MenuEditDeviceComponents { get; set; }

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
            MenuDeviceRunTime = new MyMenu(24, (string)Application.Current.Resources["menuDeviceRunTime"], "/AIC.Resources;component/Images/cog_go.png");
            MenuDeviceRunAnalyze = new MyMenu(25, (string)Application.Current.Resources["menuDeviceRunAnalyze"], "/AIC.Resources;component/Images/cog_error.png");
            MenuDeviceHourlyData = new MyMenu(26, (string)Application.Current.Resources["menuDeviceHourlyData"], "/AIC.Resources;component/Images/cog_edit.png");
            MenuExportDBData = new MyMenu(27, (string)Application.Current.Resources["menuExportDBData"], "/AIC.Resources;component/Images/export32.png");
            MenuImportDBData = new MyMenu(28, (string)Application.Current.Resources["menuImportDBData"], "/AIC.Resources;component/Images/import32.png");
            MenuFilterDBData = new MyMenu(29, (string)Application.Current.Resources["menuFilterDBData"], "/AIC.Resources;component/Images/search.png");
            MenuServerQucikData = new MyMenu(30, (string)Application.Current.Resources["menuServerQucikData"], "/AIC.Resources;component/Images/lightning.png");
            MenuDeviceQucikData = new MyMenu(31, (string)Application.Current.Resources["menuDeviceQucikData"], "/AIC.Resources;component/Images/lightning.png");
            MenuItemQucikData = new MyMenu(32, (string)Application.Current.Resources["menuItemQucikData"], "/AIC.Resources;component/Images/lightning.png");
            MenuBPNetWorks = new MyMenu(33, (string)Application.Current.Resources["menuBPNetWorks"], "/AIC.Resources;component/Images/lightbulb.png");
            MenuSOMNetWorks = new MyMenu(34, (string)Application.Current.Resources["menuSOMNetWorks"], "/AIC.Resources;component/Images/lightbulb.png");
            MenuDeviceFaultDiagnose = new MyMenu(35, (string)Application.Current.Resources["menuDeviceFaultDiagnose"], "/AIC.Resources;component/Images/lightbulb.png");
            MenuEditDeviceComponents = new MyMenu(36, (string)Application.Current.Resources["menuEditDeviceComponents"], "/AIC.Resources;component/Images/lightbulb.png");
            Dictionary = new Dictionary<int, MyMenu>();
            Dictionary.Add(MenuUserManage.Number, MenuUserManage);
            Dictionary.Add(MenuRoleManage.Number, MenuRoleManage);
            Dictionary.Add(MenuMenuManage.Number, MenuMenuManage);
            Dictionary.Add(MenuOrganizationManage.Number, MenuOrganizationManage);
            Dictionary.Add(MenuManageLog.Number, MenuManageLog);
            Dictionary.Add(MenuServerSetting.Number, MenuServerSetting);
            Dictionary.Add(MenuCollectorSetting.Number, MenuCollectorSetting);
            //Dictionary.Add(7, MenuEquipmentSetting);
            //Dictionary.Add(8, MenuOnlineData);
            //Dictionary.Add(9, MenuHistoricalData);
            //Dictionary.Add(10, MenuAlarmData);
            //Dictionary.Add(11, MenuRunningMonitor);
            //Dictionary.Add(12, MenuRunningAnalyze);
            Dictionary.Add(MenuOnlineDataList.Number, MenuOnlineDataList);
            Dictionary.Add(MenuOnlineDataTile.Number, MenuOnlineDataTile);
            Dictionary.Add(MenuOnlineDataDiagram.Number, MenuOnlineDataDiagram);
            Dictionary.Add(MenuOnlineDataOverview.Number, MenuOnlineDataOverview);
            Dictionary.Add(MenuHistoryDataList.Number, MenuHistoryDataList);
            Dictionary.Add(MenuHistoryDataDiagram.Number, MenuHistoryDataDiagram);
            Dictionary.Add(MenuOnlineDataDiagnosis.Number, MenuOnlineDataDiagnosis);//<!--昌邑石化-->
            Dictionary.Add(MenuOnlineDataStatistics.Number, MenuOnlineDataStatistics);
            Dictionary.Add(MenuHistoryDataStatistics.Number, MenuHistoryDataStatistics);
            Dictionary.Add(MenuSystemEventList.Number, MenuSystemEventList);
            Dictionary.Add(MenuDataTrendChart.Number, MenuDataTrendChart);
            Dictionary.Add(MenuDeviceRunTime.Number, MenuDeviceRunTime);
            Dictionary.Add(MenuDeviceRunAnalyze.Number, MenuDeviceRunAnalyze);
            Dictionary.Add(MenuDeviceHourlyData.Number, MenuDeviceHourlyData);
            Dictionary.Add(MenuExportDBData.Number, MenuExportDBData);
            Dictionary.Add(MenuImportDBData.Number, MenuImportDBData);
            Dictionary.Add(MenuFilterDBData.Number, MenuFilterDBData);
            Dictionary.Add(MenuServerQucikData.Number, MenuServerQucikData);
            Dictionary.Add(MenuDeviceQucikData.Number, MenuDeviceQucikData);
            Dictionary.Add(MenuItemQucikData.Number, MenuItemQucikData);
            Dictionary.Add(MenuBPNetWorks.Number, MenuBPNetWorks);
            Dictionary.Add(MenuSOMNetWorks.Number, MenuSOMNetWorks);
            Dictionary.Add(MenuDeviceFaultDiagnose.Number, MenuDeviceFaultDiagnose);
            Dictionary.Add(MenuEditDeviceComponents.Number, MenuEditDeviceComponents);
        }

        public static MyMenu GetMenu(string strname)
        {
            try
            {
                string name = (string)Application.Current.Resources[strname];
                var menu = (from p in Dictionary where p.Value.Name == name select p.Value).FirstOrDefault();
                return menu;
            }
            catch { return null; }
        }

        public static string GetIconPath(int internalNumber)
        {
            try
            {
                var path = (from p in Dictionary where p.Value.Number == internalNumber select p.Value.IconPath).FirstOrDefault();
                return path;
            }
            catch { return null; }
        }
    }
}
