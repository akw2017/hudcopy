   M S B A M L   `   `   `  ���� G  CAIC.HomePage, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null7 clr-namespace:AIC.HomePage.ViewsAIC.HomePage.Views  C ?AIC.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullQ6clr-namespace:AIC.Core.ControlModels;assembly=AIC.CoreAIC.Core.ControlModels 5(clr-namespace:AIC.Core;assembly=AIC.CoreAIC.Core '    !AIC.HomePage.Views.MainRegionView   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     2local clr-namespace:AIC.HomePage.Views     P LPrism.Wpf, Version=6.2.0.0, Culture=neutral, PublicKeyToken=91a96d2a154366d8$prismhttp://prismlibrary.com/  U QMahApps.Metro, Version=1.5.0.23, Culture=neutral, PublicKeyToken=f4fb5a3c4d1e5b4f;Controls,http://metro.mahapps.com/winfx/xaml/controls  Amenu6clr-namespace:AIC.Core.ControlModels;assembly=AIC.Core  3core(clr-namespace:AIC.Core;assembly=AIC.Core  -     >�Name$  usercontrol��!  Prism.Mvvm.ViewModelLocator   AutoWireViewModel .    AsyncWorkUserControlStyle#���    � 	Resources (  "AIC.Core.BoolToVisibilityConverter  boolToVisibilityConverter1  +MahApps.Metro.Controls.WindowButtonCommands �� 
TargetType �� Property �� Value  AppThemeMenuIcon �� Mode  AccentMenuIcon  AppThemeMenuItemStyle  MetroMenuItem �� BasedOn  AccentColorMenuItemStyle � Cursor	 �� Path
 �� Orientation  AccentColorBrush �� RelativeSource �� Mode �� AncestorType �� Self�� %  &	       '       &	 ?    &	 �    &	 �     7  7  &	 u    7  7  '��        �� "  .���  � ��$	 Top=���  � ��$ 24��A� $��16��$��16��$��1������ *BorderColorBrush+$ OneWay=����� *
ColorBrush+$ OneWay=�A� $��16��$��16������ *
ColorBrush+$ OneWay=��� " m�8   .���  �@m�Command �� *ChangeAccentCommand+��  �@m�Icon8  ��  � �� �� *Name+$ OneWay=��� " m�8   .���  �@m�Command �� *ChangeAccentCommand+��  �@m�Icon8  ��  � �� �� *Name+$ OneWay=��� " ��.���  � �� �� .R�]� .+��� $
��None=�$5��//AIC.Resources;component/Images/application.png��$
��Left=�$��0,0,6,0q�� $��Right=�$��6,0,0,0q�.+��� $
 Hand��$
��Bold"�$��24��$��24��$��Center=�$��Center=����� $	 CloseCommand�y��� $	 
Visibility����� $
 
Horizontal=�.+��� $��X��#���  $��Center=��� $
 Hand��$��24��$��24��y��� $	 LockVisibility����� *DataContext.UnLockCommand+ �� $ FindAncestor=�$ UserControlL�.���� $.��(/AIC.Resources;component/Images/lock.png��$��-1,-1,-1,-1q�$
��Fill=��� $��Center=����� $	 Title���  � �� �� $	 Content.Closer�# Z .��� -    �Name$
 grid�� � RowDefinitions �� $
��Auto ��� $
��Auto ��� $��* ��� $
��Auto �.+�o� -   #���    �� DataType.  (AIC.Core.ControlModels.MenuItemViewModel �� ItemsSource m�Name  DefaultMenuItem m� Command m� CommandParameter m� Icon �� Triggers �� Value �� Binding �� 
TargetName/  )AIC.Core.ControlModels.SeparatorViewModel	 �� X1	 �� X2 �� Stretch��%c  (��       *, +)7  (�� �     *, +)�� "   �� *
Children+.R�m� $
 menu��8��  ���� *Header+ �� *	Command+ �� *Header+ �� ���� $
	 Icon� �� $ 	Collapsed�� �� *
Visibility+.a��� $
 menu�� � y�$ 	Collapsed=��� "  .R��� ���� .R��� $ 0��$ 1��
���   �$��1��$
 Fill=�.@�m�  	 menuUser#��� 	  
 TransparentWhiteBrush#��� 
 $y�	Collapsed=�.@�m�   	menuLogin#���   �� *LoginCommand+ �� $1��+/AIC.Resources;component/Images/user_go.png��m�   
menuLogout#���   �� *LogoutCommand+ �� $5��//AIC.Resources;component/Images/user_delete.png��m�   menuLock#���   �� *LockCommand+ �� $.��(/AIC.Resources;component/Images/lock.png��m�   
menuUnLock#���  $	 All�� �� *UnLockCommand+ �� $3��-/AIC.Resources;component/Images/lock_open.png��m�   menuExit#���   �� *ExitCommand+ �� $0��*/AIC.Resources;component/Images/export.png��m�   
menuManage#���  #��� 
 $y�	Collapsed=�.@�m�   menuUserManage#���  $ MenuUserManage�� �� *NavigateCommand+y��� **(MenuManageList.MenuUserManage.Visibility+ �� $/��)/AIC.Resources;component/Images/users.png��m�   menuRoleManage#���  $ MenuRoleManage�� �� *NavigateCommand+y��� **(MenuManageList.MenuRoleManage.Visibility+ �� $.��(/AIC.Resources;component/Images/user.png��m�   menuMenuManage#���  $ MenuMenuManage�� �� *NavigateCommand+y��� **(MenuManageList.MenuMenuManage.Visibility+ �� $:��4/AIC.Resources;component/Images/application_edit.png��m�   menuOrganizationManage#���  $ MenuOrganizationManage�� �� *NavigateCommand+y��� *20MenuManageList.MenuOrganizationManage.Visibility+ �� $<��6/AIC.Resources;component/Images/chart_organisation.png��m�   menuManageLog#���  $ MenuManageLog�� �� *NavigateCommand+y��� *)'MenuManageList.MenuManageLog.Visibility+ �� $4��./AIC.Resources;component/Images/page_error.png��m�   menuSetting#���  #��� 
 $y�	Collapsed=�.@�m�   menuServerSetting#���  $ MenuServerSetting�� �� *NavigateCommand+y��� *-+MenuManageList.MenuServerSetting.Visibility+ �� $7��1/AIC.Resources;component/Images/database_gear.png��m�   menuCollectorSetting#���  $ MenuCollectorSetting�� �� *NavigateCommand+y��� *0.MenuManageList.MenuCollectorSetting.Visibility+ �� $4��./AIC.Resources;component/Images/brick_edit.png��m�   menuOnlineData#���  #��� 
 .@�m�   menuOnlineDataDiagram#���  $ MenuOnlineDataDiagram�� �� *NavigateCommand+y��� *1/MenuManageList.MenuOnlineDataDiagram.Visibility+ �� $B��</AIC.Resources;component/Images/application_view_gallery.png��m�   menuOnlineDataList#���  $ MenuOnlineDataList�� �� *NavigateCommand+y��� *.,MenuManageList.MenuOnlineDataList.Visibility+ �� $A��;/AIC.Resources;component/Images/application_view_detail.png��m� $��在线频谱分析��$ MenuOnlineDataTile�� �� *NavigateCommand+y��� *.,MenuManageList.MenuOnlineDataTile.Visibility+ �� $?��9/AIC.Resources;component/Images/application_view_tile.png��m�   menuOnlineDataOverview#���  $ MenuOnlineDataOverview�� �� *NavigateCommand+y��� *20MenuManageList.MenuOnlineDataOverview.Visibility+ �� $:��4/AIC.Resources;component/Images/application_home.png��m�   menuHistoricalData#���  #��� 
 .@�m�   menuHistoryDataList#���  $ MenuHistoryDataList�� �� *NavigateCommand+y��� */-MenuManageList.MenuHistoryDataList.Visibility+ �� $4��./AIC.Resources;component/Images/table_save.png��m�   menuHistoryDataDiagram#���  $ MenuHistoryDataDiagram�� �� *NavigateCommand+y��� *20MenuManageList.MenuHistoryDataDiagram.Visibility+ �� $6��0/AIC.Resources;component/Images/picture_save.png��m�    menuData#���   #��� 
 $y�	Collapsed=�.@�m�  ! menuAlartmData#��� ! $ MenuAlarmData�� �� *NavigateCommand+y��� *)'MenuManageList.MenuAlarmData.Visibility+ �� $8��2/AIC.Resources;component/Images/database_error.png��m�  " menuRunningMonitor#��� " $ MenuRunningMonitor�� �� *NavigateCommand+y��� *.,MenuManageList.MenuRunningMonitor.Visibility+ �� $5��//AIC.Resources;component/Images/database_go.png��m�  # menuRunningAnalyze#��� # $ MenuRunningAnalyze�� �� *NavigateCommand+y��� *.,MenuManageList.MenuRunningAnalyze.Visibility+ �� $<��6/AIC.Resources;component/Images/database_lightning.png��m�  $ 	menuTheme#��� $ #��� 
 #��[ $y�	Collapsed=����� *	AppThemes+$ OneWay=�m�  % 	menuStyle#��� % #��� 
 #��[ $y�	Collapsed=����� *AccentColors+$ OneWay=�m�  & menuHelp#��� & #��� 
 $y�	Collapsed=�.@�m�  ' menuAbnormalList#��� '  �� *AbnormalListCommand+ �� $5��//AIC.Resources;component/Images/table_error.png��m�  ( menuSystemLog#��� (  �� *SystemLogCommand+ �� $6��0/AIC.Resources;component/Images/folder_error.png��m� $��综合管理��#��� 
 .@�m� #���   �� *LoginCommand+ �� $1��+/AIC.Resources;component/Images/user_go.png��m� #���   �� *LogoutCommand+ �� $5��//AIC.Resources;component/Images/user_delete.png��m� #���   �� *LockCommand+ �� $.��(/AIC.Resources;component/Images/lock.png��m� #���  $	 All�� �� *UnLockCommand+ �� $3��-/AIC.Resources;component/Images/lock_open.png��m� #���   �� *ExitCommand+ �� $0��*/AIC.Resources;component/Images/export.png��m� #���  $ MenuUserManage�� �� *NavigateCommand+y��� **(MenuManageList.MenuUserManage.Visibility+ �� $/��)/AIC.Resources;component/Images/users.png��m� #���  $ MenuRoleManage�� �� *NavigateCommand+y��� **(MenuManageList.MenuRoleManage.Visibility+ �� $.��(/AIC.Resources;component/Images/user.png��m� #���  $ MenuMenuManage�� �� *NavigateCommand+y��� **(MenuManageList.MenuMenuManage.Visibility+ �� $:��4/AIC.Resources;component/Images/application_edit.png��m� #���  $ MenuOrganizationManage�� �� *NavigateCommand+y��� *20MenuManageList.MenuOrganizationManage.Visibility+ �� $<��6/AIC.Resources;component/Images/chart_organisation.png��m� #���  $ MenuManageLog�� �� *NavigateCommand+y��� *)'MenuManageList.MenuManageLog.Visibility+ �� $4��./AIC.Resources;component/Images/page_error.png��m� #���  $ MenuServerSetting�� �� *NavigateCommand+y��� *-+MenuManageList.MenuServerSetting.Visibility+ �� $7��1/AIC.Resources;component/Images/database_gear.png��m� #���  $ MenuCollectorSetting�� �� *NavigateCommand+y��� *0.MenuManageList.MenuCollectorSetting.Visibility+ �� $4��./AIC.Resources;component/Images/brick_edit.png��m� $��显示管理��#��� 
 .@�m� #��� $ #��� 
 #��[ ���� *	AppThemes+$ OneWay=� �� $7��1/AIC.Resources;component/Images/wrench_orange.png��m� #��� % #��� 
 #��[ ���� *AccentColors+$ OneWay=� �� $7��1/AIC.Resources;component/Images/wrench_orange.png��m� #��� '  �� *AbnormalListCommand+ �� $5��//AIC.Resources;component/Images/table_error.png��m� #��� (  �� *SystemLogCommand+ �� $6��0/AIC.Resources;component/Images/folder_error.png���� $��Center=�$��Right=����� *	LoginUser+� $��1��#���  .+�c� .@��� $��MenuOnlineDataDiagram������ *NavigateCommand+y��� *1/MenuManageList.MenuOnlineDataDiagram.Visibility+���� $
 
Horizontal=�.+��� $
��None=�$B��</AIC.Resources;component/Images/application_view_gallery.png���� #���  $��Center=��� $��MenuHistoryData������ *NavigateCommand+y��� *20MenuManageList.MenuHistoryDataDiagram.Visibility+���� $
 
Horizontal=�.+��� $
��None=�$6��0/AIC.Resources;component/Images/picture_save.png���� #���  $��Center=�c� $��Right=�.@��� $��MenuRefreshData������ *NavigateCommand+y��� *LoginVisibility+���� $
 
Horizontal=�.+��� $
��None=�$:��4/AIC.Resources;component/Images/database_refresh.png����  ) refresh#��� ) $��Center=��� $
��test��$y�	Collapsed=����� *NavigateCommand+���� $
 
Horizontal=�.+��� $
��None=�$:��4/AIC.Resources;component/Images/database_refresh.png���� $��测试��$��Center=��� -    ��Name$ 
ScreenShot��y��� *LoginVisibility+���� $
 
Horizontal=�.+��� $
��None=�$-��'/AIC.Resources;component/Images/cut.png����  * 
screenShot#��� * $��Center=�d� -    d�Name$ 	togglebtn�� + PauseToggleButton#��[+   d� 	IsChecked  �� *Pause+y��� *LoginVisibility+�� -   ! ��Name$! 
tabControl��$��2��!  Prism.Regions.RegionManager"   
RegionName$" MainTabRegion��$��3q�� $��3��#���  .+��� ���� .R�� # � ColumnDefinitions# �� $
��Auto ��� $
��Auto ��� $��* ��� $
��Auto ��� $
��Auto ��� $
��Auto �.@��� $��0����Z� .@��� $
 
Horizontal=�$��Right=�.+�F� �����4���ejѼ߲�ej� ��Z�Q4��d]�F�@@�4���ej���������#���@@�����Z���[�����[� �l't� ���4����4��l't�pej��[ф:�[� :����ej����ej��Ѡyv���P������������������������Xy���Xy�����є�H	�є�\����M\����M����'t����l't�ш���Ѹ��Tn5��Z�Tn5�do�р�C�р�C���������$
 Fill=�
��� � �$��20��$��20��$��Center=�$��Center=�y��� *IsConnected+$ �� 	Converter#$ [ �� $�� 在线��
��� � �$��Center=�y��� *IsConnected+#$ [ F� �����?�D�[�$Z)���,tu��[���D�|���IL���,���ѐtuTn5�$Z)�S�Ѹ?�Tn5�����ш�L�������|Ѹ?�D�[�ej�ej�����ej�����Z�Qpejd]�F�@@�ej�ej�߲���Ѩ1����Ѩ1��[� ���[����l't������ej��pejl't�H<R��[�\�!�[�\�!����H<R����H<R����P^\�ь˦��� ���Ѽ߲���� �D��� �Xy��`�Xy��T?�L����*�D���D�[����D�[��`d����x��n5�2��n5�<F���X�+��X�+���� �D�����H	���Z�H	���Z�\���Tn5\����m5�p��Y)D�|Ѥ���5@Ѥ��H	�$
 Fill=�
���  ��$��20��$��20��$��Center=�$��Center=�y��� *IsConnected+#$ [ % �� ConverterParameter$% invert���� $�� 离线��
���  ��$��Center=�y��� *IsConnected+#$ [ $% invert���� 
��������� .�测点总数: �� ��� *Count+�� ���	#FF87CDEE�� .�	正常: �� ��� *NormalCount+�� 
��� ����� .�	警告: �� ��� *WarningCount+�� 
���  ���� .�	危险: �� ��� *DangerCount+�� 
��������� .�	断线: �� ��� *UnConnectCount+d� -   $ mutebtn�� , SoundToggleButton#��[,   �� *Mute+�� $��1�� - MetroStatusBarSeparator#��[- �� $��3��$y�	Collapsed=�.���� $
 
Horizontal=�.+�A� -   & A�Name$& ellipse��$��12��$��12��
���  ��' � Triggers' :� ( :� RoutedEvent$( Window.Loaded��.[��� .���� ) g� RepeatBehavior$) Forever��.��W� * g� 	BeginTime$* 0X�+ �� 
TargetName$+ ellipse��, �� TargetProperty$, (UIElement.Opacity)�- W� From$- 1X�	. W� To$. 0X�/ g� Duration$/ 00:00:01D��� $��Center=��� 0 �� Command0 �� *AlarmListCommmad+.�.��� $��报警列表���� $��4��y��� *ExceptListVisibility+.���� $
 
Horizontal=�.+�A� -	   $& ellipse2��$��12��$��12��
��� ���' :� $( Window.Loaded��.[��� .���� $) Forever��.��W� $* 0X�$+ ellipse2��$, (UIElement.Opacity)�$- 1X�$. 0X�$/ 00:00:01D��� $��Center=��� 0 �� *ExceptListCommmad+.�.��� $��异常列表���� $��5��$��Right=����� *	NowTime+