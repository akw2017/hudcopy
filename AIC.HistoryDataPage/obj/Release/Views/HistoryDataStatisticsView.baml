   M S B A M L   `   `   `  ���� C  ?AIC.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null[;clr-namespace:AIC.Core.OrganizationModels;assembly=AIC.CoreAIC.Core.OrganizationModels  T PWpf.PageNavigationControl, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullhJclr-namespace:Wpf.PageNavigationControl;assembly=Wpf.PageNavigationControlWpf.PageNavigationControl I2clr-namespace:AIC.Core.ExCommand;assembly=AIC.CoreAIC.Core.ExCommand  O5clr-namespace:AIC.Core.SignalModels;assembly=AIC.CoreAIC.Core.SignalModels  5(clr-namespace:AIC.Core;assembly=AIC.CoreAIC.Core  G CAIC.CoreType, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullA0clr-namespace:AIC.CoreType;assembly=AIC.CoreTypeAIC.CoreType  AIC.HistoryDataPage9   3AIC.HistoryDataPage.Views.HistoryDataStatisticsView   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     c _System.Windows.Interactivity, Version=4.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity  Icontrol;clr-namespace:AIC.Core.OrganizationModels;assembly=AIC.Core  U	 QMahApps.Metro, Version=1.5.0.23, Culture=neutral, PublicKeyToken=f4fb5a3c4d1e5b4f;Controls,http://metro.mahapps.com/winfx/xaml/controls 	 P
 LPrism.Wpf, Version=6.2.0.0, Culture=neutral, PublicKeyToken=91a96d2a154366d8$prismhttp://prismlibrary.com/ 
 UpageJclr-namespace:Wpf.PageNavigationControl;assembly=Wpf.PageNavigationControl 
 @command2clr-namespace:AIC.Core.ExCommand;assembly=AIC.Core 
 F BItemsFilter, Version=1.1.0.1, Culture=neutral, PublicKeyToken=nullGbsFilter8http://schemas.bolapansoft.com/xaml/Controls/ItemsFilter  Bsignal5clr-namespace:AIC.Core.SignalModels;assembly=AIC.Core  3core(clr-namespace:AIC.Core;assembly=AIC.Core  ?coreType0clr-namespace:AIC.CoreType;assembly=AIC.CoreType  ! 
 Prism.Mvvm.ViewModelLocator    AutoWireViewModel  .    AsyncWorkUserControlStyle#���    � 	Resources  �� MergedDictionaries �� Source��  �� $4 ./AIC.Resources;component/Themes/ScrollBar.xaml?�%    .��� -   $��grdWorkbench�� � RowDefinitions �� $
��Auto ��� $��2* ��� $
��Auto ��� $��* � � ColumnDefinitions �� $
��Auto ��� $
��Auto ��� $��* �.+�R� -    R�Name$ treeview��$��0��$��4��  VirtualisedMetroTreeView#���  ���� *OrganizationTreeItems+  �� DataType?   9AIC.Core.OrganizationModels.OrganizationTreeItemViewModel �� ItemsSource	 �� Path
 �� Orientation F�Name �� Stretch  AccentColorBrush ��Name �� Mode �� Triggers �� Value �� Binding �� 
TargetName �� Property �� Value9   3AIC.Core.OrganizationModels.DeviceTreeItemViewModel��%�  (��       *, +)(�� �    *, +)�� "   �� $	 Children�.R��� $
 
Horizontal=�.+�F� $ IconPath��$��18��$��18��$
 Fill=�#���  ������Bnx�V��Bn4�Cќ�O�ob�L�!�!�4�C�71�ܽ�,���ܽ���K�����z@����H��р�e�!��8�obф�4�Cф�x�V��Bnx�V�4�C�Śј��d%ј�(�(��ǘ���@@(�(��ǘ���@@����08�����08�(�(����(�(�����d%�4�C�Ś� $��2,0,2,0q�.+��� $ txtName��$
��Auto������ *Name+$ TwoWay=� �� $ 0�� �� *T_Organization.Level+.a��� $ IconPath�� � ��� �� ��:�����:����pej� �pej� ��:���2��x��2��x����Ѥ����Ѥ��2�� ����є�����'t���������������E�'t�E�'t0P�є�0P�є��E� ��E� ��������EѤ����+�Tn5��+�����+�x����+�x���E����E����x��Tn5�x��Tn5�EѤ���E�$� �E�$� �x��$� l't�T�-���\��l't�\���x��\���EѬ<R�EѬ<R�x��Ԙ� �x��Ԙ� �E�$� �E���-���������%����,�U���-�P��V�� �h�V�� P�7���-����� $ IconPath�� � ��$ 24���� "  .R��� $
 
Horizontal=�.+�F� $��18��$��18��$
 Fill=�#���  �����P�x��ݩ(�� ���� p׻\r� $�� �(4��0�	�^)�@@�5������@@�.>�A�G�!اQ���P�x�ز�ݩ������y@@�����p׻�:pΈ"@@$��pP�(4�Ql^�	�^)�@@SP=������@@���A(Ċ�!�L�c����2ز�S�U)�����y@@�� ����d�:pΈ"@@��7!P�Ѵ�� l^����� P=�d*� ��PJ� �Ê��L�c�(�� �2��� �U)\r� �� � �dѐ0�7!є5P�� Ӱ.>��� �Gd*� اQPJ� �P�x�� 6n -1Ӵ�L -1 -1��L -1 6n� -1L���L ?� 6n ?��L� ?� ?�L� ?� 6n� ?���LL� -1 6n -1�� $��2,0,2,0q�$
 
Horizontal=�.+��� $ txtName��$
��Auto������ *Name+$ TwoWay=�����  �� 
TargetType" Q�  LineTreeViewItem �� BasedOn# [ .���  �@Q�
IsSelected �� *
IsSelected+$ OneTime=���  �@Q�
IsExpanded �� *
IsExpanded+$ OneTime=�.  (System.Windows.Interactivity.Interaction   Triggers /  )System.Windows.Interactivity.EventTrigger     	EventName$ SelectedItemChanged��.  (System.Windows.Interactivity.TriggerBase   Actions. 6  0System.Windows.Interactivity.InvokeCommandAction     Command �� *SelectedTreeChangedComamnd+   CommandParameter �� *SelectedItem+ �� ElementName$ treeview���� -    ��Name$ gsSplitterr��$��0��$��4��$��1��$��3��#���  $��Center=�$��Stretch=�  GridSplitterStyleExpend#��[ �� $��0��$��2�� �� Padding$ 2q�  AccentColorBrush2#���  ._��� $
 
Horizontal=�.+��� $��报警统计:��$��Center=�$��2,0,2,0q���   AccentedSquareButtonStyle#���  $��4,0,0,0q����� *RefreshCommand+���� $
 
Horizontal=�.+��� $
��None=�$7��1/AIC.Resources;component/Images/arrow_refresh.png����   refresh#���  $��Center=�&   System.Windows.Controls.DataGrid  -     Name$ gridview��$��1��$��2��    GridLinesVisibility$
  None=�$��0,1,0,0q�!   SelectionMode$! Single=�"   CanUserAddRows" .  # %� VirtualizationMode$# 	Recycling=�$ %� IsVirtualizing$ . !% %� IsVirtualizingWhenGrouping% . &   AutoGenerateColumns& .  '   EnableRowVirtualization' . !(   EnableColumnVirtualization( . )   RowDetailsVisibilityMode$) 	Collapsed=���. ���� *SignalsView+* �� SelectedItem* �� *SelectedSignal++   Columns+ 0	  *System.Windows.Controls.DataGridTextColumn	  ,
  &System.Windows.Controls.DataGridColumn, 
  Header$, 测点名称��- 
  
IsReadOnly- . 1  +System.Windows.Controls.DataGridBoundColumn.   Binding. �� *
ItemName+	  $, 第一次采集时间��- . . �� *FirstUploadTime+/ �� StringFormat$/ yyyy-MM-dd HH:mm:ss��	  $, 最后一次采集时间��- . . �� *LastUploadTime+$/ yyyy-MM-dd HH:mm:ss��	  $, 预警次数��- . . �� *PreAlarmCount+	  $, 报警次数��- . . �� *
AlarmCount+	  $, 危险次数��- . . �� *DangerCount+0 �� 
GroupStyle0 (  "System.Windows.Controls.GroupStyle  1   ContainerStyle1 �� " ��.���  � �� �� 2 �� 
TargetType"2 ��.R�9� 3 9� 
IsExpanded3 . $��0,0,0,0q����� $
 
Horizontal=�$
��Auto��$��24��.+��� $
��Bold"�$��5,0,0,0q����� $
	 Name����� #���  $��2q�._��� 4   Panel4 �� .R�>  8System.Windows.Controls.Primitives.DataGridRowsPresenter  5   ColumnHeaderStyle5 ��   MetroDataGridColumnHeader# [ =  7System.Windows.Controls.Primitives.DataGridColumnHeader"  .���  � �� 	 AccentColorBrush3# � 	 6   	CellStyle6 ��  
 MetroDataGridCell# [
 *  $System.Windows.Controls.DataGridCell"  .���  � ��$ 0,0,20,0q�7   RowStyle7 ��   MetroDataGridRow# [ )  #System.Windows.Controls.DataGridRow"  ����   MetroDataGrid# [ "  .���   �@ AlternatingRowBackground  AccentColorBrush4# �     $ SelectionChanged��.    �� * SelectedDataGridChangedComamnd+ �� *SelectedItem+$ gridview���� $��2��$��2��$��3��#���  $��Stretch=�$��Center=�� -   $��	gridChart��$��3��$��2��