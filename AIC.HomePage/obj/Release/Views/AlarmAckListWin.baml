   M S B A M L   `   `   `  ���� U  QMahApps.Metro, Version=1.5.0.23, Culture=neutral, PublicKeyToken=f4fb5a3c4d1e5b4fV;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.MetroMahApps.Metro.Controls   AIC.HomePage(   "AIC.HomePage.Views.AlarmAckListWin   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     PControls;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.Metro     (   "MahApps.Metro.Controls.MetroWindow    ShowMinButton  .     ShowMaxRestoreButton .  $��2q�   AccentColorBrush#���    � Title$ 报警栏列表��$	��410��$	��410�� � WindowStartupLocation$ CenterScreen=� � 
ResizeMode$ NoResize=� � Icon$1 +/AIC.Resources;component/Images/user_go.png�� � Topmost .  � 	Resources ��%    .���   AccentColorBrush2#���  .+�� $��10q�$	��350�� � RowDefinitions �� $��9* ��� $��* �.+�&   System.Windows.Controls.DataGrid  -   	  Name$	 gridview��$��0��
   AutoGenerateColumns
 .     ColumnWidth5  /System.Windows.Controls.DataGridLengthConverter$ * ����    Columns 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn   Header$ 测点名称��   Width$
 Auto    
IsReadOnly . 1  +System.Windows.Controls.DataGridBoundColumn   Binding �� *
ItemName+  $ 数值�� .  �� *Result+ �� StringFormat$ F3��  $ 单位�� .  �� *Unit+4  .System.Windows.Controls.DataGridCheckBoxColumn     MinWidth$ 60Q�$ 确认�� �� *
AlarmAck+ �� Mode$ TwoWay=� �� UpdateSourceTrigger$ PropertyChanged=�   ColumnHeaderStyle ��   MetroDataGridColumnHeader �� BasedOn# [  �� 
TargetType=  7System.Windows.Controls.Primitives.DataGridColumnHeader"  .���  �� Property � ��  AccentColorBrush3 �� Value# �     	CellStyle ��   MetroDataGridCell# [ *	  $System.Windows.Controls.DataGridCell" 	    RowStyle ��   MetroDataGridRow# [ )
  #System.Windows.Controls.DataGridRow" 
  �� Triggers ��  �� Binding ��  �� Path$ DelayAlarmGrade� �� Value Invalid.a���  � ��
 ��i����  �� $ DelayAlarmGrade� HighPreAlarm.a���  � ��
 � �����  �� $ DelayAlarmGrade� LowPreAlarm.a���  � ��
 � �����  �� $ DelayAlarmGrade� 	HighAlarm.a���  � ��
 � �����  �� $ DelayAlarmGrade� LowAlarm.a���  � ��
 � �����  �� $ DelayAlarmGrade� 
HighDanger.a���  � ��
 �  ����  �� $ DelayAlarmGrade� 	LowDanger.a���  � ��
 �  ������   MetroDataGrid# [ "  .���   �@ AlternatingRowBackground  AccentColorBrush4# �  �� $��1��$��1��  �� Orientation$  
Horizontal=�$��Center=�.+��� -   ! ��Name$! btnClose��$��1��$��2��  AccentedSquareButtonStyle#���  $	��100��" �� IsCancel" . ���� $  
Horizontal=�.+��� $
��None=�$0��*/AIC.Resources;component/Images/cancel.png���� $��关闭��$��Center=�