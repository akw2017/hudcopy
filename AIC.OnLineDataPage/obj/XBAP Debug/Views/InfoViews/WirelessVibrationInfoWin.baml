   M S B A M L   `   `   `  ���� U  QMahApps.Metro, Version=1.5.0.23, Culture=neutral, PublicKeyToken=f4fb5a3c4d1e5b4fV;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.MetroMahApps.Metro.Controls   AIC.OnLineDataPage7   1AIC.OnLineDataPage.Views.WirelessVibrationInfoWin   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     PControls;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.Metro     (   "MahApps.Metro.Controls.MetroWindow    ShowMinButton  .     ShowMaxRestoreButton .  $��2q�   AccentColorBrush#���    � Title$ 无线振动信号信息��$	��760��$	��500�� � WindowStartupLocation$ CenterScreen=� � 
ResizeMode$ NoResize=� � Icon$1 +/AIC.Resources;component/Images/user_go.png�� � Topmost .  � 	Resources ��%    .���   AccentColorBrush2#���  .+�� #���  .+�� $��10q�$	��700�� � ColumnDefinitions �� $	��100 ��� $��* ��� $	��100 ��� $��* �	 � RowDefinitions	 �� $��* ��� $��* ��� $��* ��� $��* ��� $��* ��� $��* ��� $��* ��� $��* ��� $��* ��� $��* ��� $��* ��� $��* ��� $��* ��� $��* ��� $��* ��� $��4* ��� $��* �.+��� $��0��$��0��$��数采器类型:��$��Center=��� $��0��$��1��  MetroTextBox#���  
 �� 
IsReadOnly
 . ���� *ACQ_Unit_Type+�� $��0��$��2��$��异步/同步:��$��Center=��� $��0��$��3��#���  
 . ���� *AsySyn+�� $��1��$��0��$��主板代码:��$��Center=��� $��1��$��1��#���  
 . ���� *MainCardCode+�� $��1��$��2��$��波形代码:��$��Center=��� $��1��$��3��#���  
 . ���� *SynWaveCode+�� $��2��$��0��$��硬件旁路:��$��Center=��� $��2��$��1��$��Center=�{�.   d� 	IsChecked �� *
IsHdBypass+�� $��2��$��2��$��硬件倍增:��$��Center=��� $��2��$��3��$��Center=�{�.   �� *IsHdMultiplication+�� $��3��$��0��$��通道是否正常:��$��Center=��� $��3��$��1��$��Center=�{�.   �� *	IsNotOK+�� $��3��$��2��$��有效通道:��$��Center=��� $��3��$��3��$��Center=�{�.   �� *	IsValidCH+�� $��4��$��0��$��发送卡ID:��$��Center=��� $��4��$��1��#���  
 . ���� *SlaveIdentifier+�� $��4��$��2��$��接受卡ID:��$��Center=��� $��4��$��3��#���  
 . ���� *MasterIdentifier+�� $��5��$��0��$��电量:��$��Center=��� $��5��$��1��#���  
 . ���� *BatteryEnergy+ �� StringFormat$ F3���� $��5��$��2��$��是否旁路:��$��Center=��� $��5��$��3��$��Center=�{�.   �� *
IsBypass+�� $��6��$��0��$��描述:��$��Center=��� $��6��$��1��#���  
 . ���� *	Remarks+�� $��6��$��2��$��
版本号:��$��Center=��� $��6��$��3��#���  
 . ���� *	Version+�� $��7��$��0��$��接收卡名称:��$��Center=��� $��7��$��1��#���  
 . ���� *ReceiveCardName+�� $��7��$��2��$��传输卡名称:��$��Center=��� $��7��$��3��#���  
 . ���� *TransmissionName+�� $��8��$��0��$��工作时间:��$��Center=��� $��8��$��1��#���  
 . ���� *
WorkTime+�� $��8��$��2��$��休眠时间:��$��Center=��� $��8��$��3��#���  
 . ���� *	SleepTime+�� $��9��$��0��$��转速:��$��Center=��� $��9��$��1��#���  
 . ���� *RPM+�� $��9��$��2��$��积分:��$��Center=��� $��9��$��3��#���  
 . ���� *Integration+�� $��10��$��0��$��是否倍增:��$��Center=��� $��10��$��1��$��Center=�{�.   �� *IsMultiplication+�� $��10��$��2��$��倍增系数:��$��Center=��� $��10��$��3��#���  
 . ���� *MultiplicationCor+�� $��11��$��0��$��偏置高电压:��$��Center=��� $��11��$��1��#���  
 . ���� *BiasVoltHigh+�� $��11��$��2��$��偏置低电压:��$��Center=��� $��11��$��3��#���  
 . ���� *BiasVoltLow+�� $��12��$��0��$��位移校准:��$��Center=��� $��12��$��1��#���  
 . ���� *DisplacementCalibration+�� $��12��$��2��$��速度校准:��$��Center=��� $��12��$��3��#���  
 . ���� *VelocityCalibration+�� $��13��$��0��$��
灵敏度:��$��Center=��� $��13��$��1��#���  
 . ���� *Sensitivity+�� $��13��$��2��$��有效波形通道:��$��Center=��� $��13��$��3��$��Center=�{�.   �� *IsValidWave+�� $��14��$��0��$��报警栏确认:��$��Center=��� -    ��Name$ alarmchk��$��14��$��1��$��Center=� �� *
AlarmAck+&   System.Windows.Controls.DataGrid  -     Name$ table��$��15��$��4��   AutoGenerateColumns .     FrozenColumnCount$ 1��   
IsReadOnly .    ColumnWidth5  /System.Windows.Controls.DataGridLengthConverter$ * ���� *
AlarmLimit+   Columns 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn   Header$ 	报警项��   MinWidth$ 60Q�1  +System.Windows.Controls.DataGridBoundColumn   Binding �� *Name+  $ 	报警值��$ 60Q� �� *Limit+   ColumnHeaderStyle ��   MetroDataGridColumnHeader �� BasedOn# [  �� 
TargetType=  7System.Windows.Controls.Primitives.DataGridColumnHeader"  .���  �� Property � ��  AccentColorBrush3 �� Value# �  ����   MetroDataGrid# [ "  .���   �@ AlternatingRowBackground  AccentColorBrush4# �  �� -    ��Name$ btnOK��$��16��$��1��  AccentedSquareButtonStyle#���  $	��100�� �� 	IsDefault . ����  �� Orientation$ 
Horizontal=�.+��� $
��None=�$0��*/AIC.Resources;component/Images/accept.png����  	 strOK#���  $��Center=��� -   $ btnClose��$��16��$��2��#���  $	��100�� �� IsCancel . ���� $ 
Horizontal=�.+��� $
��None=�$0��*/AIC.Resources;component/Images/cancel.png����  	 	strCancel#��� 	 $��Center=�