   M S B A M L   `   `   `  ����M  IAIC.OnLineDataPage, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullC&clr-namespace:AIC.OnLineDataPage.ViewsAIC.OnLineDataPage.Views  5      C ?AIC.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5(clr-namespace:AIC.Core;assembly=AIC.CoreAIC.Core 5      G CAIC.CoreType, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullA0clr-namespace:AIC.CoreType;assembly=AIC.CoreTypeAIC.CoreType 5	      T PWpf.PageNavigationControl, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullhJclr-namespace:Wpf.PageNavigationControl;assembly=Wpf.PageNavigationControlWpf.PageNavigationControl 5
      [;clr-namespace:AIC.Core.OrganizationModels;assembly=AIC.CoreAIC.Core.OrganizationModels 5      I2clr-namespace:AIC.Core.ExCommand;assembly=AIC.CoreAIC.Core.ExCommand 5      5    /AIC.OnLineDataPage.Views.OnlineDataOverviewView   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      8local&clr-namespace:AIC.OnLineDataPage.Views     5      c _System.Windows.Interactivity, Version=4.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity  5      3core(clr-namespace:AIC.Core;assembly=AIC.Core  5      ?coreType0clr-namespace:AIC.CoreType;assembly=AIC.CoreType  5	      UpageJclr-namespace:Wpf.PageNavigationControl;assembly=Wpf.PageNavigationControl  5
      Icontrol;clr-namespace:AIC.Core.OrganizationModels;assembly=AIC.Core  5      @command2clr-namespace:AIC.Core.ExCommand;assembly=AIC.Core  5      P	 LPrism.Wpf, Version=6.2.0.0, Culture=neutral, PublicKeyToken=91a96d2a154366d8$prismhttp://prismlibrary.com/ 	 5      ! 	 Prism.Mvvm.ViewModelLocator    AutoWireViewModel  .  � 	Resources 5       �� MergedDictionaries �� Source   SignalMonitorStyle �� 
TargetType �� Property �� Value �� 
TargetType ��Name	 �� Padding
 J� SnapsToDevicePixels �� Triggers P� Property P� Value  ValueTemplate �� Triggers �� Binding �� ElementName �� Path �� Value  BarTemplate�� 5   
    5      �� 5      $5 //AIC.Resources;component/Themes/ColorStyle.xaml?�6%   �� 5      $J D/AIC.OnLineDataPage;component/Themes/OnlineDataOverviewTemplate.xaml?�6%   �� 5      $4 ./AIC.Resources;component/Themes/ScrollBar.xaml?�6%   %�  &	        7  7  7  �� 5      " ��6/   .�5      ��  � ��$ 2q�6+   �� 5       � ��$ 2q�6,   �� 5       � ��
 ���� 6/   �� 5       � �� 5      �� 5      " ��6*   .R�5      �� $ Bd��6%   #��z��61   #��z��6]   #��z��6�   #	 z��6�   
 . ._�5    "   �� #��z��63   #
 z
 6v   #��z��6�   5!       5"      P� 5#   "    � {� .  . �5$   &   �� # � ��6C    � ��5%   #   5'      5)      �� 5*       � ��8   64    5+      �� 5,       6"   �� $ 
radioValue��$ 	IsChecked� true6\   .a�5-      ��  � ��8  6<   5.      �� 5/       6"   �� $ radioBar��$ 	IsChecked� true6Z   .a�50      ��  � ��8  6<   51      53      54      .��57      � -   5        $��grdWorkbench��57       � RowDefinitions 58   
   �� 59      $
��Auto �6   �� 5:      $��* �6   �� 5;      $
��Auto �6    � ColumnDefinitions 5=   
   �� 5>      $
��Auto �6   �� 5?      $
��Auto �6   �� 5@      $��* �6   .+�5C   
   R� -   5         R�Name$ treeview��5C   1   $��0��6   $��3��6      VirtualisedMetroTreeView#���  5E      ��5D      �� *OrganizationTreeItems+ 5F       �� DataType?  9AIC.Core.OrganizationModels.OrganizationTreeItemViewModel �� ItemsSource �� Orientation F�Name �� Stretch  AccentColorBrush ��Name �� Mode �� Triggers �� 
TargetName9  3AIC.Core.OrganizationModels.DeviceTreeItemViewModel��%p  (��       5G   +   *, +)(�� [    5j   +   *, +)�� 5G      "  6+    6e   �� $ Children�.R�5H      �� $ 
Horizontal=�6!   .+�5I      F� $ IconPath��6   $��18��61   $��18��6<   $
 Fill=�6H   #���  6W   ������Bnx�V��Bn4�Cќ�O�ob�L�!�!�4�C�71�ܽ�,���ܽ���K�����z@����H��р�e�!��8�obф�4�Cф�x�V��Bnx�V�4�C�Śј��d%ј�(�(��ǘ���@@(�(��ǘ���@@����08�����08�(�(����(�(�����d%�4�C�Ś6�   � 5J      $��2,0,2,0q�6   .+�5K      �� $ txtName��6(   $
��Auto��6\   ��69   �� *Name+$ TwoWay=�6\   5L      5M       5N      �� 5O      $ 0��6O    6&   �� *T_Organization.Level+.a�5P      �� $ IconPath��6%    � ��� �� ��:�����:����pej� �pej� ��:���2��x��2��x����Ѥ����Ѥ��2�� ����є�����'t���������������E�'t�E�'t0P�є�0P�є��E� ��E� ��������EѤ����+�Tn5��+�����+�x����+�x���E����E����x��Tn5�x��Tn5�EѤ���E�$� �E�$� �x��$� l't�T�-���\��l't�\���x��\���EѬ<R�EѬ<R�x��Ԙ� �x��Ԙ� �E�$� �E���-���������%����,�U���-�P��V�� �h�V�� P�7���-���6K   �� 5Q      $ IconPath��6%    � ��$ 24��6L   5R      �� 5S      $ Invalid��6@    6&   �� *Alarm+.a�5T      �� $ txtName��6%    � ��
 ��i��6P   5U      �� 5V      $ HighPreAlarm��6@    6&   �� *Alarm+.a�5W      �� $ txtName��6%    � ��
 � ���6P   5X      �� 5Y      $ LowPreAlarm��6@    6&   �� *Alarm+.a�5Z      �� $ txtName��6%    � ��
 � ���6P   5[      �� 5\      $ 	HighAlarm��6@    6&   �� *Alarm+.a�5]      �� $ txtName��6%    � ��
 � ���6P   5^      �� 5_      $ LowAlarm��6@    6&   �� *Alarm+.a�5`      �� $ txtName��6%    � ��
 � ���6P   5a      �� 5b      $ 
HighDanger��6@    6&   �� *Alarm+.a�5c      �� $ txtName��6%    � ��
 �  ��6P   5d      �� 5e      $ 	LowDanger��6@    6&   �� *Alarm+.a�5f      �� $ txtName��6%    � ��
 �  ��6P   5g      5i      �� 5j      "  6+   .R�5k      �� $ 
Horizontal=�6!   .+�5l      F� $��18��6   $��18��6*   $
 Fill=�66   #���  6E   �����P�x��ݩ(�� ���� p׻\r� $�� �(4��0�	�^)�@@�5������@@�.>�A�G�!اQ���P�x�ز�ݩ������y@@�����p׻�:pΈ"@@$��pP�(4�Ql^�	�^)�@@SP=������@@���A(Ċ�!�L�c����2ز�S�U)�����y@@�� ����d�:pΈ"@@��7!P�Ѵ�� l^����� P=�d*� ��PJ� �Ê��L�c�(�� �2��� �U)\r� �� � �dѐ0�7!є5P�� Ӱ.>��� �Gd*� اQPJ� �P�x�� 6n -1Ӵ�L -1 -1��L -1 6n� -1L���L ?� 6n ?��L� ?� ?�L� ?� 6n� ?���LL� -1 6n -16o   � 5m      $��2,0,2,0q�6   .+�5n      �� $ txtName��6(   $
��Auto��6\   ��69   �� *Name+$ TwoWay=�6\   5o      5p       5q      �� 5r      $ Invalid��6@    6&   �� *Alarm+.a�5s      �� $ txtName��6%    � ��
 ��i��6P   5t      �� 5u      $ HighPreAlarm��6@    6&   �� *Alarm+.a�5v      �� $ txtName��6%    � ��
 � ���6P   5w      �� 5x      $ LowPreAlarm��6@    6&   �� *Alarm+.a�5y      �� $ txtName��6%    � ��
 � ���6P   5z      �� 5{      $ 	HighAlarm��6@    6&   �� *Alarm+.a�5|      �� $ txtName��6%    � ��
 � ���6P   5}      �� 5~      $ LowAlarm��6@    6&   �� *Alarm+.a�5      �� $ txtName��6%    � ��
 � ���6P   5�      �� 5�      $ 
HighDanger��6@    6&   �� *Alarm+.a�5�      �� $ txtName��6%    � ��
 �  ��6P   5�      �� 5�      $ 	LowDanger��6@    6&   �� *Alarm+.a�5�      �� $ txtName��6%    � ��
 �  ��6P   5�      5�      5�      ��5�      �� 5�      " Q�6     LineTreeViewItem �� BasedOn# [ 62   .�5�      ��  �@Q�
IsSelected 63   �� *
IsSelected+$ OneTime=��� 5�       �@Q�
IsExpanded 63   �� *
IsExpanded+$ OneTime=�5�      .  (System.Windows.Interactivity.Interaction    Triggers  5�      /  )System.Windows.Interactivity.EventTrigger  5�      !   	EventName$! SelectedItemChanged��6!   .  (System.Windows.Interactivity.TriggerBase"   Actions." 5�      6  0System.Windows.Interactivity.InvokeCommandAction  #   Command# 6,   �� *SelectedTreeChangedComamnd+$   CommandParameter$ 6[   �� *SelectedItem+$ treeview��5�      5�      �� 5�   
   -   5        % ��Name$% gsSplitterr��5�      $��0��6,   $��1��69   $��3��6I   $��3��6Z   #���  6d   $��Center=�6�   $��Stretch=�6�     GridSplitterStyleExpend#��[ 6�   �� 5�   
   $��0��6   $��3��6   $	 5q�6.     AccentColorBrush2#���  6:   ._�5�      �� $ 
Horizontal=�6   .+�5�      �� $��报警级别:��6   $��Center=�6)   $��2,0,2,0q�6D   �� 5�      $��5,2,5,2q�6   $ 
Horizontal=�6.   .+�5�      �� $��无效��6    $��Center=�6*   �� 5�      $��Center=�6?   $��2,0,2,0q�6Z   ��6    �� *AbnormalCount+6Z   �� 5�      $��Center=�6   
����i��  CheckBoxStyle#��[ 6�   & d� 	IsChecked& 6P   �� *IsInvalidSignal+$ TwoWay=�6�   5�      �� 5�      $��5,2,5,2q�6   $ 
Horizontal=�6.   .+�5�      �� $��正常��6    $��Center=�6*   �� 5�      $��Center=�6=   $��2,0,2,0q�6X   ��6    �� *NormalCount+6X   �� 5�      $��Center=�6    	 	TextBrush#��� 	 6:   #��[ 6�   & 6d   �� *IsNormalSignal+$ TwoWay=�6�   5�      �� 5�      $��5,2,5,2q�6   $ 
Horizontal=�6.   .+�5�      �� $��预警��6    $��Center=�6*   �� 5�      $��Center=�6?   $��2,0,2,0q�6Z   ��6    �� *PreAlertCount+6Z   �� 5�      $��Center=�6   
��� ���#��[ 6�   & 6O   �� *IsPreAlarmSignal+$ TwoWay=�6�   5�      �� 5�      $��5,2,5,2q�6   $ 
Horizontal=�6.   .+�5�      �� $��警告��6    $��Center=�6*   �� 5�      $��Center=�6<   $��2,0,2,0q�6W   ��6    �� *
AlertCount+6W   �� 5�      $��Center=�6   
��� ���#��[ 6�   & 6S   �� *IsAlarmSignal+$ TwoWay=�6�   5�      �� 5�      $��5,2,5,2q�6   $ 
Horizontal=�6.   .+�5�      �� $��危险��6    $��Center=�6*   �� 5�      $��Center=�6=   $��2,0,2,0q�6X   ��6    �� *DangerCount+6X   �� 5�      $��Center=�6   
���  ��#��[ 6~   & 6L   �� *IsDangerSignal+$ TwoWay=�6~   5�      �� 5�      $��5,2,5,2q�6   $ 
Horizontal=�6.   .+�5�      �� $��断线��6    $��Center=�6*   �� 5�      $��Center=�6@   $��2,0,2,0q�6[   ��6    �� *UnConnectCount+6[   �� 5�      $��Center=�6   
���  ��#��[ 6�   & 6P   �� *DisConnectSignal+$ TwoWay=�6�   5�      �� 5�      $��Stretch=�6   $��2��68   #���  6B   $	��5,0q�6n   �� 5�      $��显示模式:��6   $��Center=�6)   $��2,0,2,0q�6D   	� 5�      -   5        ' 	�Name$' 
radioValue��5�      $��数值��62   $
& TrueY�6?    
 RadioButtonStyle#��[
 6P   	� 5�      -   5        $' radioBar��5�      $��	柱状图��60   #��[
 6>   5�      5�      � 5�   
   $��1��6   $��3��6   $��0,0,0,0q�6,    5�      �� 5�      $
��Auto �6    �� 5�      $��* �6     5�      �� 5�      $��* �6#   �� 5�      $
��auto �6#   .+�5�      �� -   5        ( ��Name$( listview��5�   4   $��1��6   $��0��6$   $��Stretch=�6F   $��Stretch=�6d   $��0,1,0,0q�6�   #��[  5�      ��5�   �   Z� ��6�   Z� ��5�      �� *
SignalList+) �� SelectedItem) 69   �� *SelectedSignalMonitor+��5�      �� *HorizontalScrollBarVisibility+$ TwoWay=���5�      �� *VerticalScrollBarVisibility+$ TwoWay=� 5�        defaultGroup��%<   &	       �� 5�      " ��6&   .R�5�      �� 5�      5�      ��5�      �� 5�      .R�5�        AIC.Core.FluidWrapPanel  *  Name$* fluidWrapPanel��6.   $
��Left=�6F   $	��Top=�6a   }�.  +   DragOpacity$+ 1Q�6  ,   	DragScale$, 1Q�6(  -   
ItemHeight- 6�   �� *
ItemHeight+.   	ItemWidth. 6�   �� *	ItemWidth+/   IsComposing/ 6�   �� *IsComposing+0   Orientation0 6�   �� *Orientation+1   ElementEasing1 5�      -	  'System.Windows.Media.Animation.BackEase	  5�   "   7
  1System.Windows.Media.Animation.EasingFunctionBase2 
  
EasingMode$2 EaseOut=�6+   3 	  	Amplitude$
3 0.35Q�6@   6S   4   
DragEasing4 5�      	  5�   "   $2 EaseOut=�6+   $
3 0.65Q�6@   6S   5�      5�      5�      �� 5�      $��0��6   $��0��6"     
FlatSlider#���  6X   5 � Minimum$	5 120Q�6}   6 � Maximum$
6 1550Q�6�   7 �� Orientation$7 
Horizontal=�6�   8 � Value8 62   �� *	ItemWidth+9 �� Delay$	9 200��6�   �� 5�      $��1��6   $��1��6"   #���  6Y   $	5 120Q�6~   $	6 600Q�6�   $7 Vertical=�6�   8 62   �� *
ItemHeight+$	9 200��6�   �� 5�      $��0��6   $��1��6$   : � ToolTip$: 允许拖拽��6c   & 64   �� *IsComposing+$ TwoWay=���6r   Z� 5�      5�      5�      