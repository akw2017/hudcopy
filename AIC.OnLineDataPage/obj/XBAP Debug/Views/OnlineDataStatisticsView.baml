   M S B A M L   `   `   `  ���� C  ?AIC.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null[;clr-namespace:AIC.Core.OrganizationModels;assembly=AIC.CoreAIC.Core.OrganizationModels  T PWpf.PageNavigationControl, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullhJclr-namespace:Wpf.PageNavigationControl;assembly=Wpf.PageNavigationControlWpf.PageNavigationControl I2clr-namespace:AIC.Core.ExCommand;assembly=AIC.CoreAIC.Core.ExCommand  M IAIC.OnLineDataPage, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullM+clr-namespace:AIC.OnLineDataPage.ConvertersAIC.OnLineDataPage.Converters O5clr-namespace:AIC.Core.SignalModels;assembly=AIC.CoreAIC.Core.SignalModels  5(clr-namespace:AIC.Core;assembly=AIC.CoreAIC.Core  G CAIC.CoreType, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullA0clr-namespace:AIC.CoreType;assembly=AIC.CoreTypeAIC.CoreType C&clr-namespace:AIC.OnLineDataPage.ViewsAIC.OnLineDataPage.Views U QLiveCharts.Wpf, Version=0.9.7.0, Culture=neutral, PublicKeyToken=0bc1f845d1ebb8dfG4clr-namespace:LiveCharts.Wpf;assembly=LiveCharts.WpfLiveCharts.Wpf 7   1AIC.OnLineDataPage.Views.OnlineDataStatisticsView   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     c	 _System.Windows.Interactivity, Version=4.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity 	 Icontrol;clr-namespace:AIC.Core.OrganizationModels;assembly=AIC.Core 	 U
 QMahApps.Metro, Version=1.5.0.23, Culture=neutral, PublicKeyToken=f4fb5a3c4d1e5b4f;Controls,http://metro.mahapps.com/winfx/xaml/controls 
 P LPrism.Wpf, Version=6.2.0.0, Culture=neutral, PublicKeyToken=91a96d2a154366d8$prismhttp://prismlibrary.com/  UpageJclr-namespace:Wpf.PageNavigationControl;assembly=Wpf.PageNavigationControl  @command2clr-namespace:AIC.Core.ExCommand;assembly=AIC.Core  ;	converter+clr-namespace:AIC.OnLineDataPage.Converters  F BItemsFilter, Version=1.1.0.1, Culture=neutral, PublicKeyToken=nullGbsFilter8http://schemas.bolapansoft.com/xaml/Controls/ItemsFilter  Bsignal5clr-namespace:AIC.Core.SignalModels;assembly=AIC.Core  3core(clr-namespace:AIC.Core;assembly=AIC.Core  ?coreType0clr-namespace:AIC.CoreType;assembly=AIC.CoreType  1view&clr-namespace:AIC.OnLineDataPage.Views  >lvc4clr-namespace:LiveCharts.Wpf;assembly=LiveCharts.Wpf  !  Prism.Mvvm.ViewModelLocator    AutoWireViewModel  .    AsyncWorkUserControlStyle#���    � 	Resources  �� MergedDictionaries �� Source%   AIC.Core.GradeToStringConverter  gradeToStringConverter'   !AIC.Core.EnumDescriptionConverter  enumDescriptionConverter$   AIC.Core.EqualBindingConverter  equalBindingConverter��  �� $4 ./AIC.Resources;component/Themes/ScrollBar.xaml?�%-   &	       &	      &	 
           .��� -   $��grdWorkbench�� � RowDefinitions �� $��0 ��� $��* ��� $��* ��� $��0 � � ColumnDefinitions �� $
��Auto ��� $
��Auto ��� $��* �.+�R� -    R�Name$ treeview��$��0��$��4��  VirtualisedMetroTreeView#���  ���� *OrganizationTreeItems+  �� DataType?   9AIC.Core.OrganizationModels.OrganizationTreeItemViewModel �� ItemsSource	 �� Path
 �� Orientation F�Name �� Stretch  AccentColorBrush ��Name �� Mode �� Triggers �� Value �� Binding �� 
TargetName �� Property �� Value9   3AIC.Core.OrganizationModels.DeviceTreeItemViewModel��%\  (��       *, +)(�� 9    *, +)�� "   �� $	 Children�.R��� $
 
Horizontal=�.+�F� $ IconPath��$��18��$��18��$
 Fill=�#���  ������Bnx�V��Bn4�Cќ�O�ob�L�!�!�4�C�71�ܽ�,���ܽ���K�����z@����H��р�e�!��8�obф�4�Cф�x�V��Bnx�V�4�C�Śј��d%ј�(�(��ǘ���@@(�(��ǘ���@@����08�����08�(�(����(�(�����d%�4�C�Ś� $��2,0,2,0q�.+��� $ txtName��$
��Auto������ *Name+$ TwoWay=� �� $ 0�� �� *T_Organization.Level+.a��� $ IconPath�� � ��� �� ��:�����:����pej� �pej� ��:���2��x��2��x����Ѥ����Ѥ��2�� ����є�����'t���������������E�'t�E�'t0P�є�0P�є��E� ��E� ��������EѤ����+�Tn5��+�����+�x����+�x���E����E����x��Tn5�x��Tn5�EѤ���E�$� �E�$� �x��$� l't�T�-���\��l't�\���x��\���EѬ<R�EѬ<R�x��Ԙ� �x��Ԙ� �E�$� �E���-���������%����,�U���-�P��V�� �h�V�� P�7���-����� $ IconPath�� � ��$ 24���� $ Invalid�� �� *Alarm+.a��� $ txtName�� � ��
 ��i���� $ HighPreAlarm�� �� *Alarm+.a��� $ txtName�� � ��
 � ����� $ LowPreAlarm�� �� *Alarm+.a��� $ txtName�� � ��
 � ����� $ 	HighAlarm�� �� *Alarm+.a��� $ txtName�� � ��
 � ����� $ LowAlarm�� �� *Alarm+.a��� $ txtName�� � ��
 � ����� $ 
HighDanger�� �� *Alarm+.a��� $ txtName�� � ��
 �  ���� $ 	LowDanger�� �� *Alarm+.a��� $ txtName�� � ��
 �  ���� "  .R��� $
 
Horizontal=�.+�F� $��18��$��18��$
 Fill=�#���  �����P�x��ݩ(�� ���� p׻\r� $�� �(4��0�	�^)�@@�5������@@�.>�A�G�!اQ���P�x�ز�ݩ������y@@�����p׻�:pΈ"@@$��pP�(4�Ql^�	�^)�@@SP=������@@���A(Ċ�!�L�c����2ز�S�U)�����y@@�� ����d�:pΈ"@@��7!P�Ѵ�� l^����� P=�d*� ��PJ� �Ê��L�c�(�� �2��� �U)\r� �� � �dѐ0�7!є5P�� Ӱ.>��� �Gd*� اQPJ� �P�x�� 6n -1Ӵ�L -1 -1��L -1 6n� -1L���L ?� 6n ?��L� ?� ?�L� ?� 6n� ?���LL� -1 6n -1�� $��2,0,2,0q�$
 
Horizontal=�.+��� $ txtName��$
��Auto������ *Name+$ TwoWay=��� $ txtRun��
��� � �$��:运行���� $ txtStop��
���  ��$��:停止��$y�	Collapsed=� �� $ Invalid�� �� *Alarm+.a��� $ txtName�� � ��
 ��i���� $ false�� �� *	IsRunning+.a��� $ txtRun�� � y�$ 	Collapsed=��� $ txtStop�� � y�$ Visible=��� $ HighPreAlarm�� �� *Alarm+.a��� $ txtName�� � ��
 � ����� $ LowPreAlarm�� �� *Alarm+.a��� $ txtName�� � ��
 � ����� $ 	HighAlarm�� �� *Alarm+.a��� $ txtName�� � ��
 � ����� $ LowAlarm�� �� *Alarm+.a��� $ txtName�� � ��
 � ����� $ 
HighDanger�� �� *Alarm+.a��� $ txtName�� � ��
 �  ���� $ 	LowDanger�� �� *Alarm+.a��� $ txtName�� � ��
 �  ������  �� 
TargetType" Q�  LineTreeViewItem �� BasedOn# [ .���  �@Q�
IsSelected �� *
IsSelected+$ OneTime=���  �@Q�
IsExpanded �� *
IsExpanded+$ OneTime=�. 	 (System.Windows.Interactivity.Interaction   Triggers / 	 )System.Windows.Interactivity.EventTrigger     	EventName$ SelectedItemChanged��.	 	 (System.Windows.Interactivity.TriggerBase 	  Actions. 6
 	 0System.Windows.Interactivity.InvokeCommandAction
   
  Command �� *SelectedTreeChangedComamnd+ 
  CommandParameter �� *SelectedItem+ �� ElementName$ treeview���� -    ��Name$ gsSplitterr��$��0��$��4��$��1��$��3��#���  $��Center=�$��Stretch=�  GridSplitterStyleExpend#��[ �� $��0��$��2��$
 
Horizontal=�.+��� $��数据统计:��$��Center=�$��2,0,2,0q���   AccentedSquareButtonStyle#���  $��4,0,0,0q����� *RefreshCommand+���� $
 
Horizontal=�.+��� $
��None=�$7��1/AIC.Resources;component/Images/arrow_refresh.png����  	 refresh#��� 	 $��Center=�� $��1��$��2�� �� $��* ��� $��* �.+��� $��0��$��报警分布��.��  LiveCharts.Wpf.PieChart  &   LiveCharts.Wpf.Charts.Base.Chart   LegendLocation$ Right=�   Series �� *	PieSeries+    DataClickCommand  �� *SliceClickCommand+!   AxisX!   LiveCharts.Wpf.Axis  "   
Foreground
" �   �#   	Separator#   LiveCharts.Wpf.Separator  $   Step$$ 1Q�{�.  %   ChartLegend% "  LiveCharts.Wpf.DefaultLegend  &   DataTooltip& #  LiveCharts.Wpf.DefaultTooltip  �� $��1������ *AlarmEventTitle+.���� -   ' ��Name$' scrollvw��$��5q�$��1��$
��Auto=�$��Hidden=�$��1q����� *AccentColorBrush+.���� $
 Vertical=�.+�&   System.Windows.Controls.DataGrid  -   (  Name$( table��)   HeadersVisibility$
) None=�*   AutoGenerateColumns* .  +   FrozenColumnCount$+ 1��,   
IsReadOnly, . -   ColumnWidth5  /System.Windows.Controls.DataGridLengthConverter$
- Auto ���� *CustomSystemException+.   Columns. 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn/   Header$/ ID��1  +System.Windows.Controls.DataGridBoundColumn0   Binding0 �� *id+  $/ 类型��0 �� *CustomSystemType+1 �� 	Converter#1 [   $/ 程度��0 �� *CustomSystemDegree+#1 [   $/ 发生时间��0 �� *	EventTime+  $/ 详情��0 �� *	Remarks+2   ColumnHeaderStyle2 ��  
 MetroDataGridColumnHeader# [
 =  7System.Windows.Controls.Primitives.DataGridColumnHeader"  .���  � ��  AccentColorBrush3# �  3   	CellStyle3 ��   MetroDataGridCell# [ *  $System.Windows.Controls.DataGridCell"  .���  � ��$ 0,0,0,0q�4   RowStyle4 ��   MetroDataGridRow# [ )  #System.Windows.Controls.DataGridRow"  ����   MetroDataGrid# [ "  .���   �@ AlternatingRowBackground  AccentColorBrush4# �     $ MouseDoubleClick��. 
   �� *MouseDoubleClickCommand+ �� *SelectedItem+$ table���� $��2��$��2��#���  $��1q�$��5q�._�  -   $( gridview��5   GridLinesVisibility$
5 None=�6   SelectionMode$6 Single=�7   CanUserAddRows7 .  $- * 8 %� VirtualizationMode$8 	Recycling=�9 %� IsVirtualizing9 . !: %� IsVirtualizingWhenGrouping: . * .  ;   EnableRowVirtualization; . !<   EnableColumnVirtualization< . =   RowDetailsVisibilityMode$= 	Collapsed=���. ���� *SignalsView+> �� SelectedItem> �� *SelectedSignal+.   $/ 设备名称��?   
IsReadOnly? . 0 �� *
DeviceName+  $/ 测点名称��? . 0 �� *
ItemName+  $/ 采集时间��? . 0 �� *ACQDatetime+@ �� StringFormat$@ yyyy-MM-dd HH:mm:ss��  $/ 数值��? . 0 �� *Result+$@ F3��  $/ 单位��? . 0 �� *Unit+A �� 
GroupStyleA (  "System.Windows.Controls.GroupStyle  B   ContainerStyleB �� " ��.���  � �� �� C �� 
TargetType"C ��.R�9� $��0,0,0,0q����� $
 
Horizontal=�$
��Auto��$��24��.+��� $
��Bold"�$��5,0,5,0q����� $
	 Name�#1 [ �� $
��Bold"�$��:���� $
��Bold"�$��5,0,5,0q����� $	 	ItemCount�����   AccentColorBrush2#���  $��2q�._��� ���� " 9�.���  �@9�
IsExpanded .  D �� TriggersD ��  True `� E `� 	Converter#E [ .1��� $
	 Name��� $!	 DataContext.FirstAlarmGrade�F �� RelativeSourceF �� G �� Mode$G FindAncestor=�H �� AncestorType$H UserControlL�.a���  �@9�
IsExpanded . I   PanelI �� .R�>  8System.Windows.Controls.Primitives.DataGridRowsPresenter  2 �� # [
 "  .���  � ��# �  3 �� # [ "  4 �� # [ "  D ��  �� $	 DelayAlarmGrade� Invalid.a���  � ��
 ��i����  �� $	 DelayAlarmGrade� HighPreAlarm.a���  � ��
 � �����  �� $	 DelayAlarmGrade� LowPreAlarm.a���  � ��
 � �����  �� $	 DelayAlarmGrade� 	HighAlarm.a���  � ��
 � �����  �� $	 DelayAlarmGrade� LowAlarm.a���  � ��
 � �����  �� $	 DelayAlarmGrade� 
HighDanger.a���  � ��
 �  ����  �� $	 DelayAlarmGrade� 	LowDanger.a���  � ��
 �  ����  �� $	 DelayAlarmGrade� 
DisConnect.a���  � ��
 �  ������ # [ "  .���   �@ AlternatingRowBackground# �     $ SelectionChanged��. 
   �� * SelectedDataGridChangedComamnd+ �� *SelectedItem+$ gridview��