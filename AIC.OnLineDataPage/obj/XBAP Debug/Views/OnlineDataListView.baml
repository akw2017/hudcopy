   M S B A M L   `   `   `  ���� C  ?AIC.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null[;clr-namespace:AIC.Core.OrganizationModels;assembly=AIC.CoreAIC.Core.OrganizationModels  T PWpf.PageNavigationControl, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullhJclr-namespace:Wpf.PageNavigationControl;assembly=Wpf.PageNavigationControlWpf.PageNavigationControl I2clr-namespace:AIC.Core.ExCommand;assembly=AIC.CoreAIC.Core.ExCommand  M IAIC.OnLineDataPage, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullM+clr-namespace:AIC.OnLineDataPage.ConvertersAIC.OnLineDataPage.Converters O5clr-namespace:AIC.Core.SignalModels;assembly=AIC.CoreAIC.Core.SignalModels  5(clr-namespace:AIC.Core;assembly=AIC.CoreAIC.Core  G CAIC.CoreType, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullA0clr-namespace:AIC.CoreType;assembly=AIC.CoreTypeAIC.CoreType C&clr-namespace:AIC.OnLineDataPage.ViewsAIC.OnLineDataPage.Views 1   +AIC.OnLineDataPage.Views.OnlineDataListView   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     c _System.Windows.Interactivity, Version=4.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity  Icontrol;clr-namespace:AIC.Core.OrganizationModels;assembly=AIC.Core  U	 QMahApps.Metro, Version=1.5.0.23, Culture=neutral, PublicKeyToken=f4fb5a3c4d1e5b4f;Controls,http://metro.mahapps.com/winfx/xaml/controls 	 P
 LPrism.Wpf, Version=6.2.0.0, Culture=neutral, PublicKeyToken=91a96d2a154366d8$prismhttp://prismlibrary.com/ 
 UpageJclr-namespace:Wpf.PageNavigationControl;assembly=Wpf.PageNavigationControl 
 @command2clr-namespace:AIC.Core.ExCommand;assembly=AIC.Core 
 ;	converter+clr-namespace:AIC.OnLineDataPage.Converters 
 F BItemsFilter, Version=1.1.0.1, Culture=neutral, PublicKeyToken=nullGbsFilter8http://schemas.bolapansoft.com/xaml/Controls/ItemsFilter  Bsignal5clr-namespace:AIC.Core.SignalModels;assembly=AIC.Core  3core(clr-namespace:AIC.Core;assembly=AIC.Core  ?coreType0clr-namespace:AIC.CoreType;assembly=AIC.CoreType  1view&clr-namespace:AIC.OnLineDataPage.Views  ! 
 Prism.Mvvm.ViewModelLocator    AutoWireViewModel  .  � 	Resources  �� MergedDictionaries �� Source>  8AIC.OnLineDataPage.Converters.IntToAlarmTypeStrConverter   intToAlarmTypeStrConverter$   AIC.Core.EqualBindingConverter  equalBindingConverter%   AIC.Core.GradeToStringConverter  gradeToStringConverter*   $AIC.Core.AlarmLimitToStringConverter  alarmLimitToStringConverter   AIC.Core.TypeOfConverter  typeOfConverter    AIC.Core.IsDivfreConverter  isDivfreConverter(   "AIC.Core.VisibilityToBoolConverter  visibilityToBoolConverter(	   "AIC.Core.BoolToVisibilityConverter  boolToVisibilityConverter'
   !AIC.Core.EnumDescriptionConverter  enumDescriptionConverter 	 expandToggleStyle �� 
TargetType �� BasedOn �� Property �� Value 
 DarkAccentColorBrush �� 
TargetType'  !System.Windows.VisualStateManager	   VisualStateGroups%  System.Windows.VisualStateGroup
   Transitions%  System.Windows.VisualTransition   GeneratedDuration   States   System.Windows.VisualState   
Storyboard g� Duration	 W� To �� TargetProperty �� 
TargetName F�Name �� Stretch J� Opacity � UseLayoutRounding J� RenderTransformOrigin �� Angle �� Triggers P� Property P� Value  HeiachyTemplate&   System.Windows.Controls.DataGrid   
IsReadOnly   AutoGenerateColumns   GridLinesVisibility   RowHeaderWidth   Columns4  .System.Windows.Controls.DataGridTemplateColumn,  &System.Windows.Controls.DataGridColumn    Header!   SortMemberPath"   
IsReadOnly#   CellTemplate$ A�Name% �� Triggers& �� Value' �� Binding( �� Path) �� 
TargetName0  *System.Windows.Controls.DataGridTextColumn1  +System.Windows.Controls.DataGridBoundColumn*   Binding+ �� StringFormat  enumComboItemTemplate, �� 	Converter��  �� $4 ./AIC.Resources;component/Themes/ScrollBar.xaml?�%�  &	        &	      &	 
     &	      &	      &	      &	      &	 #     &	 (     &		 -     7d�&	 �    &	 *    7                	  
  �� " d�8   .���  �@�Cursor$
 Hand����  � ��# � 
 ��  � ��
 ���� ��  � �� �� " d�.R�� 	   CommonStates��
   $ 0:0:0.1D�.   
Normal��  	MouseOver��. �� .��W� $ 0D�$ 1X�$ (UIElement.Opacity)�$ normal��  Pressed��. �� .��W� $ 0D�$ 1X�$ (UIElement.Opacity)�$ normal��  Disabled��. �� .��W� $ 0D�$	 0.3X�$ (UIElement.Opacity)�$ normal��  CheckStates��.   Checked��. �� .��W� $ 0:0:0.3D�$	 180X�$ Angle�$ symbolRotation��  	Unchecked��. �� .��W� $ 0:0:0.3D�$ 90X�$ Angle�$ symbolRotation��  Indeterminate��  FocusStates��.   Focused��  	Unfocused��.+��� $��2��$��3��#��z��#��z��#��z��F� $ normal��$��Center=�$��10��$
 Fill=�$	 0.6Q������@B ��[ �@B ��[ �ا �ا �@B �ا �pr ����n@�pr ����n@S�pr ����n@��� �ا ��� �ا Ӏ�� �ا ��� ��[ ��� ��[ Ӏ�� ��[ �pr P�^����?�pr P�^����?S�pr P�^����?��[ ��[ 	#��z�� .  $��Center=�$��7��$ 0.5,0.5&�z�V� .~��� �� �� symbolRotation��$ 90Q�T�  P�  �@d�	IsChecked$
 trueY�. ���  � ��$	 Top=��� .R�  $��0q�
������ $��0q� .  .  $
 None=�$ 0Q����� *	DivFres+��Z�    $  报警级别��$! DelayAlarmGrade��" . # �� .R�A� $$ ellipse�����	#FF85CBEB$��Center=�$��12��$��0q�$��12��$��Center=�% �� $& Invalid��' �� $( DelayAlarmGrade�.a��� $) ellipse�� � ��
 ��i���� $& 	HighAlarm��' �� $( DelayAlarmGrade�.a��� $) ellipse�� � ��
 � ����� $& LowAlarm��' �� $( DelayAlarmGrade�.a��� $) ellipse�� � ��
 � ����� $& 
HighDanger��' �� $( DelayAlarmGrade�.a��� $) ellipse�� � ��
 �  ���� $& 	LowDanger��' �� $( DelayAlarmGrade�.a��� $) ellipse�� � ��
 �  ��  $  分频名称��" . * �� *DisPlayName+  $  采集时间��" . * �� *ACQDatetime+$+ yyyy-MM-dd HH:mm:ss��  $  频率��" . * �� *FreV+$+ F3��  $  幅值��" . * �� *FreMV+$+ F3��  $  相位��" . * �� *Phase+$+ F1��  $  高危��" . * �� *
HighDanger+$+ F3��  $  高警��" . * �� *	HighAlarm+$+ F3��  $  正常(高)��" . * �� *
HighNormal+$+ F3��  $  正常(低)��" . * �� *	LowNormal+$+ F3��  $  低警��" . * �� *
LowAlarm+$+ F3��  $  低危��" . * �� *	LowDanger+$+ F3��  $  转速��" . * �� *RPM+$+ F1��  $  默认转速��" . * �� *
DefaultR+  $  基础频率1��" . * �� *
Base1Fre+  $  基础频率2��" . * �� *
Base2Fre+  $  最大频率数��" . * �� *	MaxFreNum+$+ F3��  $  倍频��" . * �� *
MultiFre+  $  特征频率��" . * �� *CharacteristicFre+  $  浮动百分比��" . * �� *Base1FrePercent+$+ F3���� .R��� ���� $( .�8,   .��� -   $��grdWorkbench��- � RowDefinitions- �� $
��Auto ��� $��3* ��� $
��Auto �. � ColumnDefinitions. �� $
��Auto ��� $
��Auto ��� $��* �.+�R� -   / R�Name$/ treeview��$��0��$��3��  VirtualisedMetroTreeView#���  ���� *OrganizationTreeItems+ 0 �� DataType?   9AIC.Core.OrganizationModels.OrganizationTreeItemViewModel1 �� ItemsSource2 �� Orientation  AccentColorBrush3 ��Name4 �� Mode9   3AIC.Core.OrganizationModels.DeviceTreeItemViewModel��%\  (��       *, +)(�� 9    *, +)�� "0  1 �� $( Children�.R��� $2 
Horizontal=�.+�F� $ IconPath��$��18��$��18��$
 Fill=�#���  ������Bnx�V��Bn4�Cќ�O�ob�L�!�!�4�C�71�ܽ�,���ܽ���K�����z@����H��р�e�!��8�obф�4�Cф�x�V��Bnx�V�4�C�Śј��d%ј�(�(��ǘ���@@(�(��ǘ���@@����08�����08�(�(����(�(�����d%�4�C�Ś� $��2,0,2,0q�.+��� $3 txtName��$
��Auto������ *Name+$4 TwoWay=�% �� $& 0��' �� *T_Organization.Level+.a��� $) IconPath�� � ��� �� ��:�����:����pej� �pej� ��:���2��x��2��x����Ѥ����Ѥ��2�� ����є�����'t���������������E�'t�E�'t0P�є�0P�є��E� ��E� ��������EѤ����+�Tn5��+�����+�x����+�x���E����E����x��Tn5�x��Tn5�EѤ���E�$� �E�$� �x��$� l't�T�-���\��l't�\���x��\���EѬ<R�EѬ<R�x��Ԙ� �x��Ԙ� �E�$� �E���-���������%����,�U���-�P��V�� �h�V�� P�7���-����� $) IconPath�� � ��$ 24���� $& Invalid��' �� *Alarm+.a��� $) txtName�� � ��
 ��i���� $& HighPreAlarm��' �� *Alarm+.a��� $) txtName�� � ��
 � ����� $& LowPreAlarm��' �� *Alarm+.a��� $) txtName�� � ��
 � ����� $& 	HighAlarm��' �� *Alarm+.a��� $) txtName�� � ��
 � ����� $& LowAlarm��' �� *Alarm+.a��� $) txtName�� � ��
 � ����� $& 
HighDanger��' �� *Alarm+.a��� $) txtName�� � ��
 �  ���� $& 	LowDanger��' �� *Alarm+.a��� $) txtName�� � ��
 �  ���� "0  .R��� $2 
Horizontal=�.+�F� $��18��$��18��$
 Fill=�#���  �����P�x��ݩ(�� ���� p׻\r� $�� �(4��0�	�^)�@@�5������@@�.>�A�G�!اQ���P�x�ز�ݩ������y@@�����p׻�:pΈ"@@$��pP�(4�Ql^�	�^)�@@SP=������@@���A(Ċ�!�L�c����2ز�S�U)�����y@@�� ����d�:pΈ"@@��7!P�Ѵ�� l^����� P=�d*� ��PJ� �Ê��L�c�(�� �2��� �U)\r� �� � �dѐ0�7!є5P�� Ӱ.>��� �Gd*� اQPJ� �P�x�� 6n -1Ӵ�L -1 -1��L -1 6n� -1L���L ?� 6n ?��L� ?� ?�L� ?� 6n� ?���LL� -1 6n -1�� $��2,0,2,0q�$2 
Horizontal=�.+��� $3 txtName��$
��Auto������ *Name+$4 TwoWay=��� $3 txtRun��
��� � �$��:运行���� $3 txtStop��
���  ��$��:停止��$y�	Collapsed=�% �� $& Invalid��' �� *Alarm+.a��� $) txtName�� � ��
 ��i���� $& false��' �� *	IsRunning+.a��� $) txtRun�� � y�$ 	Collapsed=��� $) txtStop�� � y�$ Visible=��� $& HighPreAlarm��' �� *Alarm+.a��� $) txtName�� � ��
 � ����� $& LowPreAlarm��' �� *Alarm+.a��� $) txtName�� � ��
 � ����� $& 	HighAlarm��' �� *Alarm+.a��� $) txtName�� � ��
 � ����� $& LowAlarm��' �� *Alarm+.a��� $) txtName�� � ��
 � ����� $& 
HighDanger��' �� *Alarm+.a��� $) txtName�� � ��
 �  ���� $& 	LowDanger��' �� *Alarm+.a��� $) txtName�� � ��
 �  ������ " Q�  LineTreeViewItem# [ .���  �@Q�
IsSelected �� *
IsSelected+$4 OneTime=���  �@Q�
IsExpanded �� *
IsExpanded+$4 OneTime=�.  (System.Windows.Interactivity.Interaction5   Triggers5 /  )System.Windows.Interactivity.EventTrigger  6   	EventName$6 SelectedItemChanged��.  (System.Windows.Interactivity.TriggerBase7   Actions.7 6  0System.Windows.Interactivity.InvokeCommandAction  8   Command8 �� *SelectedTreeChangedComamnd+9   CommandParameter9 �� *SelectedItem+: �� ElementName$: treeview���� -   ; ��Name$; gsSplitterr��$��0��$��3��$��1��$��3��#���  $��Center=�$��Stretch=�  GridSplitterStyleExpend#��[ �� $��0��$��2��< �� Padding$< 5q�  AccentColorBrush2#���  ._�� .+��� $2 
Horizontal=�.+��� $��报警级别:��$��Center=�$��2,0,2,0q��� $��5,2,5,2q�$2 
Horizontal=�.+��� $��无效��$��Center=��� $��Center=�$��2,0,2,0q����� *AbnormalCount+�� $��Center=�
����i��  CheckBoxStyle#��[ = d� 	IsChecked= �� *IsInvalidSignal+$4 TwoWay=��� $��5,2,5,2q�$2 
Horizontal=�.+��� $��正常��$��Center=��� $��Center=�$��2,0,2,0q����� *NormalCount+�� $��Center=�  	TextBrush#���  #��[ = �� *IsNormalSignal+$4 TwoWay=��� $��5,2,5,2q�$2 
Horizontal=�.+��� $��预警��$��Center=��� $��Center=�$��2,0,2,0q����� *PreAlertCount+�� $��Center=�
��� ���#��[ = �� *IsPreAlarmSignal+$4 TwoWay=��� $��5,2,5,2q�$2 
Horizontal=�.+��� $��警告��$��Center=��� $��Center=�$��2,0,2,0q����� *
AlertCount+�� $��Center=�
��� ���#��[ = �� *IsAlarmSignal+$4 TwoWay=��� $��5,2,5,2q�$2 
Horizontal=�.+��� $��危险��$��Center=��� $��Center=�$��2,0,2,0q����� *DangerCount+�� $��Center=�
���  ��#��[ = �� *IsDangerSignal+$4 TwoWay=��� $��5,2,5,2q�$2 
Horizontal=�.+��� $��断线��$��Center=��� $��Center=�$��2,0,2,0q����� *UnConnectCount+�� $��Center=�
���  ��#��[ = �� *DisConnectSignal+$4 TwoWay=��� $2 
Horizontal=�$��Right=�.+��� $��Center=�$
= TrueY�$��分组显示��#��[ 5   $6 Checked��.7   8 �� *GroupViewCommand+  $6 	Unchecked��.7   8 �� *UnGroupViewCommand+  -   >  Name$> gridview��$��1��$��2��$��0,1,0,0q�? %� IsVirtualizing? . @ %� VirtualizationMode$@ 	Recycling=�!A %� IsVirtualizingWhenGroupingA . ��. $
 None=�B   EnableRowVirtualizationB .  .  C   CanUserAddRowsC .  ���� *SignalsView+D �� SelectedItemD �� *SelectedSignal+   defaultGroup��%   &	       �� " ��.R���    $  报警级别��" . * �� *DelayAlarmGrade+#, [   $  测点名称��" . * �� *
ItemName+  $  采集时间��" . * �� *ACQDatetime+$+ yyyy-MM-dd HH:mm:ss��  $  数值��" . * �� *Result+$+ F3��  $  单位��" . * �� *Unit+  $  传感器地址��" . * �� *ChannelHDID+  $  报警信息��" . * �� *AlarmLimitString+  $  传感器电量��# �� .R�� .+�� E �Name$E ProgressBar��
��� � �$��20��$��40��$	��5 2q�F � ValueF �� *BatteryEnergy+�� G �� TextAlignment$G Center=�
�������$��Center=�$��Center=�$��16(����� *BatteryEnergy+$+ F0��H �� 
GroupStyleH (  "System.Windows.Controls.GroupStyle  I   ContainerStyleI �� " ��.���  � �� �� " ��.R�9� $��0,0,0,0q����� $2 
Horizontal=�$
��Auto��$��24��.+��� $
��Bold"�$��5,0,5,0q����� $
( Name��� $
��Bold"�$��:���� $
��Bold"�$��5,0,5,0q����� $( 	ItemCount����� #���  $��2q�._��� ���� " 9�.���  �@9�
IsExpanded .  J �� TriggersJ �� & True' `� K `� 	Converter#K [ .1��� $
( Name��� $( DataContext.FirstName�L �� RelativeSourceL �� M �� Mode$M FindAncestor=�N �� AncestorType$N UserControlL�.a���  �@9�
IsExpanded . J �� ' �� $
( Name�&  .a���  � ��# [ O   PanelO �� .R�>  8System.Windows.Controls.Primitives.DataGridRowsPresenter  P   ColumnHeaderStyleP ��   MetroDataGridColumnHeader# [ =  7System.Windows.Controls.Primitives.DataGridColumnHeader"  .���  � ��  AccentColorBrush3# �  Q   	CellStyleQ ��   MetroDataGridCell# [ *  $System.Windows.Controls.DataGridCell"  .���  � ��$ 0,0,20,0q�R   RowStyleR ��   MetroDataGridRow# [ )  #System.Windows.Controls.DataGridRow"  J �� ' �� $( DelayAlarmGrade�& Invalid.a���  � ��
 ��i���� ' �� $( DelayAlarmGrade�& HighPreAlarm.a���  � ��
 � ����� ' �� $( DelayAlarmGrade�& LowPreAlarm.a���  � ��
 � ����� ' �� $( DelayAlarmGrade�& 	HighAlarm.a���  � ��
 � ����� ' �� $( DelayAlarmGrade�& LowAlarm.a���  � ��
 � ����� ' �� $( DelayAlarmGrade�& 
HighDanger.a���  � ��
 �  ���� ' �� $( DelayAlarmGrade�& 	LowDanger.a���  � ��
 �  ���� ' �� $( DelayAlarmGrade�& 
DisConnect.a���  � ��
 �  ������   MetroDataGrid# [ "  .���   �@ AlternatingRowBackground  AccentColorBrush4# �  5   $6 SelectionChanged��.7   8 �� * SelectedDataGridChangedComamnd+9 �� *SelectedItem+$: gridview��� $��2��$��2��.+�9� ���� *DiagnosticInfo+���� *DiagnosticAdvice+