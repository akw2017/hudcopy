   M S B A M L   `   `   `  ���� U  QMahApps.Metro, Version=1.5.0.23, Culture=neutral, PublicKeyToken=f4fb5a3c4d1e5b4fV;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.MetroMahApps.Metro.Controls   AIC.HomePage$   AIC.HomePage.Views.AlarmAckWin   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     PControls;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.Metro     (   "MahApps.Metro.Controls.MetroWindow    ShowMinButton  .     ShowMaxRestoreButton .  $��2q�   AccentColorBrush#���    � Title$ 报警栏确认��$	��340��$	��340�� � WindowStartupLocation$ CenterScreen=� � 
ResizeMode$ NoResize=� � Icon$1 +/AIC.Resources;component/Images/user_go.png�� � Topmost .  � 	Resources ��%    .���   AccentColorBrush2#���  .+�� $��10q�$	��280�� � RowDefinitions �� $��* ��� $��* ��� $��5* ��� $��* �.+��� $��Center=���`� 	 �� StringFormat$		 {0}��.1��� 
 �� Path$
 DeviceItemName� � ToolTip `� $		 {0}��.1��� $
 FullName��� $��1�� �� Orientation$ 
Horizontal=�.+��� -    ��Name$ AMSText��$��Center=�$��2,0,2,0q����� *Result+$	 F3�� �� Mode$ TwoWay=�����  �� 
TargetType" �� �� Triggers ��  �� Binding �� *DelayAlarmGrade+ �� Value Invalid.a���  �� Property � �� �� Value
 �������  �� *DelayAlarmGrade+ 	HighAlarm.a���  � ��
 � �����  �� *DelayAlarmGrade+ LowAlarm.a���  � ��
 � �����  �� *DelayAlarmGrade+ 
HighDanger.a���  � ��
 �  ����  �� *DelayAlarmGrade+ 	LowDanger.a���  � ��
 �  ���� $��Center=�$��1,0,0,0q����� *Unit+&   System.Windows.Controls.DataGrid  -     Name$ table��$��2��   AutoGenerateColumns .     FrozenColumnCount$ 1��   
IsReadOnly .    ColumnWidth5  /System.Windows.Controls.DataGridLengthConverter$ * ���� *
AlarmLimit+   Columns 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn   Header$ 	报警项��   MinWidth$ 60Q�1  +System.Windows.Controls.DataGridBoundColumn   Binding �� *Name+  $ 	报警值��$ 60Q� �� *Limit+   ColumnHeaderStyle ��   MetroDataGridColumnHeader �� BasedOn# [ =  7System.Windows.Controls.Primitives.DataGridColumnHeader"  .���  � ��  AccentColorBrush3# �  ����   MetroDataGrid# [ "  .���   �@ AlternatingRowBackground  AccentColorBrush4# �  �� $��3��$��1��$ 
Horizontal=�$��Center=�.+��� -     ��Name$  btnOK��  AccentedSquareButtonStyle#���  $	��100��! �� 	IsDefault! . ���� $ 
Horizontal=�.+��� $
��None=�$0��*/AIC.Resources;component/Images/accept.png����  	 strOK#���  $��Center=��� -   $  btnClose��$��3��$��2��#���  $	��100��" �� IsCancel" . $��20,0,0,0q����� $ 
Horizontal=�.+��� $
��None=�$0��*/AIC.Resources;component/Images/cancel.png����   	strCancel#���  $��Center=�