   M S B A M L   `   `   `  ���� M  IAIC.OnLineDataPage, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullC&clr-namespace:AIC.OnLineDataPage.ViewsAIC.OnLineDataPage.Views  C ?AIC.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5(clr-namespace:AIC.Core;assembly=AIC.CoreAIC.Core G CAIC.CoreType, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullA0clr-namespace:AIC.CoreType;assembly=AIC.CoreTypeAIC.CoreType T PWpf.PageNavigationControl, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullhJclr-namespace:Wpf.PageNavigationControl;assembly=Wpf.PageNavigationControlWpf.PageNavigationControl [;clr-namespace:AIC.Core.OrganizationModels;assembly=AIC.CoreAIC.Core.OrganizationModels I2clr-namespace:AIC.Core.ExCommand;assembly=AIC.CoreAIC.Core.ExCommand 5    /AIC.OnLineDataPage.Views.OnlineDataOverviewView   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     8local&clr-namespace:AIC.OnLineDataPage.Views     c _System.Windows.Interactivity, Version=4.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity  3core(clr-namespace:AIC.Core;assembly=AIC.Core  ?coreType0clr-namespace:AIC.CoreType;assembly=AIC.CoreType  UpageJclr-namespace:Wpf.PageNavigationControl;assembly=Wpf.PageNavigationControl  Icontrol;clr-namespace:AIC.Core.OrganizationModels;assembly=AIC.Core  @command2clr-namespace:AIC.Core.ExCommand;assembly=AIC.Core  P	 LPrism.Wpf, Version=6.2.0.0, Culture=neutral, PublicKeyToken=91a96d2a154366d8$prismhttp://prismlibrary.com/ 	 ! 	 Prism.Mvvm.ViewModelLocator    AutoWireViewModel  .  � 	Resources  �� MergedDictionaries �� Source   SignalMonitorStyle �� 
TargetType �� Property �� Value �� 
TargetType ��Name	 �� Padding
 J� SnapsToDevicePixels �� Triggers P� Property P� Value  ValueTemplate �� Triggers �� Binding �� ElementName �� Path �� Value  BarTemplate��  �� $5 //AIC.Resources;component/Themes/ColorStyle.xaml?��� $J D/AIC.OnLineDataPage;component/Themes/OnlineDataOverviewTemplate.xaml?��� $4 ./AIC.Resources;component/Themes/ScrollBar.xaml?�%�  &	        7  7  7  �� " ��.���  � ��$ 2q���  � ��$ 2q���  � ��
 ���� ��  � �� �� " ��.R��� $ Bd��#��z��#��z��#��z��#	 z��
 . ._��� #��z��#
 z
 #��z�� P�  � {� .  . ��� # � �� � ����  � ��8    ��  �� $ 
radioValue��$ 	IsChecked� true.a���  � ��8  ��  �� $ radioBar��$ 	IsChecked� true.a���  � ��8  .��� -   $��grdWorkbench�� � RowDefinitions �� $
��Auto ��� $��* ��� $
��Auto � � ColumnDefinitions �� $
��Auto ��� $
��Auto ��� $��* �.+�R� -    R�Name$ treeview��$��0��$��3��  VirtualisedMetroTreeView#���  ���� *OrganizationTreeItems+  �� DataType?  9AIC.Core.OrganizationModels.OrganizationTreeItemViewModel �� ItemsSource �� Orientation F�Name �� Stretch  AccentColorBrush ��Name �� Mode �� Triggers �� 
TargetName9  3AIC.Core.OrganizationModels.DeviceTreeItemViewModel��%f  (��       *, +)(�� 9    *, +)�� "   �� $ Children�.R��� $ 
Horizontal=�.+�F� $ IconPath��$��18��$��18��$
 Fill=�#���  ������Bnx�V��Bn4�Cќ�O�ob�L�!�!�4�C�71�ܽ�,���ܽ���K�����z@����H��р�e�!��8�obф�4�Cф�x�V��Bnx�V�4�C�Śј��d%ј�(�(��ǘ���@@(�(��ǘ���@@����08�����08�(�(����(�(�����d%�4�C�Ś� $��2,0,2,0q�.+��� $ txtName��$
��Auto������ *Name+$ TwoWay=� �� $ 0�� �� *T_Organization.Level+.a��� $ IconPath�� � ��� �� ��:�����:����pej� �pej� ��:���2��x��2��x����Ѥ����Ѥ��2�� ����є�����'t���������������E�'t�E�'t0P�є�0P�є��E� ��E� ��������EѤ����+�Tn5��+�����+�x����+�x���E����E����x��Tn5�x��Tn5�EѤ���E�$� �E�$� �x��$� l't�T�-���\��l't�\���x��\���EѬ<R�EѬ<R�x��Ԙ� �x��Ԙ� �E�$� �E���-���������%����,�U���-�P��V�� �h�V�� P�7���-����� $ IconPath�� � ��$ 24���� $ Invalid�� �� *Alarm+.a��� $ txtName�� � ��
 ��i���� $ HighPreAlarm�� �� *Alarm+.a��� $ txtName�� � ��
 � ����� $ LowPreAlarm�� �� *Alarm+.a��� $ txtName�� � ��
 � ����� $ 	HighAlarm�� �� *Alarm+.a��� $ txtName�� � ��
 � ����� $ LowAlarm�� �� *Alarm+.a��� $ txtName�� � ��
 � ����� $ 
HighDanger�� �� *Alarm+.a��� $ txtName�� � ��
 �  ���� $ 	LowDanger�� �� *Alarm+.a��� $ txtName�� � ��
 �  ���� "  .R��� $ 
Horizontal=�.+�F� $��18��$��18��$
 Fill=�#���  �����P�x��ݩ(�� ���� p׻\r� $�� �(4��0�	�^)�@@�5������@@�.>�A�G�!اQ���P�x�ز�ݩ������y@@�����p׻�:pΈ"@@$��pP�(4�Ql^�	�^)�@@SP=������@@���A(Ċ�!�L�c����2ز�S�U)�����y@@�� ����d�:pΈ"@@��7!P�Ѵ�� l^����� P=�d*� ��PJ� �Ê��L�c�(�� �2��� �U)\r� �� � �dѐ0�7!є5P�� Ӱ.>��� �Gd*� اQPJ� �P�x�� 6n -1Ӵ�L -1 -1��L -1 6n� -1L���L ?� 6n ?��L� ?� ?�L� ?� 6n� ?���LL� -1 6n -1� $��2,0,2,0q�.+��� $ txtName��$
��Auto������ *Name+$ TwoWay=� �� $ Invalid�� �� *Alarm+.a��� $ txtName�� � ��
 ��i���� $ HighPreAlarm�� �� *Alarm+.a��� $ txtName�� � ��
 � ����� $ LowPreAlarm�� �� *Alarm+.a��� $ txtName�� � ��
 � ����� $ 	HighAlarm�� �� *Alarm+.a��� $ txtName�� � ��
 � ����� $ LowAlarm�� �� *Alarm+.a��� $ txtName�� � ��
 � ����� $ 
HighDanger�� �� *Alarm+.a��� $ txtName�� � ��
 �  ���� $ 	LowDanger�� �� *Alarm+.a��� $ txtName�� � ��
 �  ������ " Q�  LineTreeViewItem �� BasedOn# [ .���  �@Q�
IsSelected �� *
IsSelected+$ OneTime=���  �@Q�
IsExpanded �� *
IsExpanded+$ OneTime=�.  (System.Windows.Interactivity.Interaction    Triggers  /  )System.Windows.Interactivity.EventTrigger  !   	EventName$! SelectedItemChanged��.  (System.Windows.Interactivity.TriggerBase"   Actions." 6  0System.Windows.Interactivity.InvokeCommandAction  #   Command# �� *SelectedTreeChangedComamnd+$   CommandParameter$ �� *SelectedItem+$ treeview���� -   % ��Name$% gsSplitterr��$��0��$��1��$��3��$��3��#���  $��Center=�$��Stretch=�  GridSplitterStyleExpend#��[ �� $��0��$��3��$	 5q�  AccentColorBrush2#���  ._��� $ 
Horizontal=�.+��� $��报警级别:��$��Center=�$��2,0,2,0q��� $��5,2,5,2q�$ 
Horizontal=�.+��� $��无效��$��Center=��� $��Center=�$��2,0,2,0q����� *AbnormalCount+�� $��Center=�
����i��  CheckBoxStyle#��[ & d� 	IsChecked& �� *IsInvalidSignal+$ TwoWay=��� $��5,2,5,2q�$ 
Horizontal=�.+��� $��正常��$��Center=��� $��Center=�$��2,0,2,0q����� *NormalCount+�� $��Center=� 	 	TextBrush#��� 	 #��[ & �� *IsNormalSignal+$ TwoWay=��� $��5,2,5,2q�$ 
Horizontal=�.+��� $��预警��$��Center=��� $��Center=�$��2,0,2,0q����� *PreAlertCount+�� $��Center=�
��� ���#��[ & �� *IsPreAlarmSignal+$ TwoWay=��� $��5,2,5,2q�$ 
Horizontal=�.+��� $��警告��$��Center=��� $��Center=�$��2,0,2,0q����� *
AlertCount+�� $��Center=�
��� ���#��[ & �� *IsAlarmSignal+$ TwoWay=��� $��5,2,5,2q�$ 
Horizontal=�.+��� $��危险��$��Center=��� $��Center=�$��2,0,2,0q����� *DangerCount+�� $��Center=�
���  ��#��[ & �� *IsDangerSignal+$ TwoWay=��� $��5,2,5,2q�$ 
Horizontal=�.+��� $��断线��$��Center=��� $��Center=�$��2,0,2,0q����� *UnConnectCount+�� $��Center=�
���  ��#��[ & �� *DisConnectSignal+$ TwoWay=��� $��Stretch=�$��2��#���  $	��5,0q��� $��显示模式:��$��Center=�$��2,0,2,0q�	� -   ' 	�Name$' 
radioValue��$��数值��$
& TrueY� 
 RadioButtonStyle#��[
 	� -   $' radioBar��$��	柱状图��#��[
 � $��1��$��3��$��0,0,0,0q� �� $
��Auto ��� $��* � �� $��* ��� $
��auto �.+��� -   ( ��Name$( listview��$��1��$��0��$��Stretch=�$��Stretch=�$��0,1,0,0q�#��[  ��Z� ��Z� ���� *
SignalList+) �� SelectedItem) �� *SelectedSignalMonitor+���� *HorizontalScrollBarVisibility+$ TwoWay=����� *VerticalScrollBarVisibility+$ TwoWay=�   defaultGroup��%   &	       �� " ��.R��� ���� .R�  AIC.Core.FluidWrapPanel  *  Name$* fluidWrapPanel��$
��Left=�$	��Top=�}�.  +   DragOpacity$+ 1Q�,   	DragScale$, 1Q�-   
ItemHeight- �� *
ItemHeight+.   	ItemWidth. �� *	ItemWidth+/   IsComposing/ �� *IsComposing+0   Orientation0 �� *Orientation+1   ElementEasing1 -	  'System.Windows.Media.Animation.BackEase	  7
  1System.Windows.Media.Animation.EasingFunctionBase2 
  
EasingMode$2 EaseOut=�3 	  	Amplitude$
3 0.35Q�4   
DragEasing4 	  $2 EaseOut=�$
3 0.65Q��� $��0��$��0��  
FlatSlider#���  5 � Minimum$	5 120Q�6 � Maximum$
6 1550Q�7 �� Orientation$7 
Horizontal=�8 � Value8 �� *	ItemWidth+9 �� Delay$	9 200���� $��1��$��1��#���  $	5 120Q�$	6 600Q�$7 Vertical=�8 �� *
ItemHeight+$	9 200���� $��0��$��1��: � ToolTip$: 允许拖拽��& �� *IsComposing+$ TwoWay=���Z� 