   M S B A M L   `   `   `  ���� c  _System.Windows.Interactivity, Version=4.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35qPclr-namespace:System.Windows.Interactivity;assembly=System.Windows.InteractivitySystem.Windows.Interactivity  G CAIC.CoreType, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullA0clr-namespace:AIC.CoreType;assembly=AIC.CoreTypeAIC.CoreType C ?AIC.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5(clr-namespace:AIC.Core;assembly=AIC.CoreAIC.Core M IAIC.OnLineDataPage, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullC&clr-namespace:AIC.OnLineDataPage.ViewsAIC.OnLineDataPage.Views U/clr-namespace:AIC.OnLineDataPage.Views.SubViews!AIC.OnLineDataPage.Views.SubViews E'clr-namespace:AIC.OnLineDataPage.ModelsAIC.OnLineDataPage.Models R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089U QMahApps.Metro, Version=1.5.0.23, Culture=neutral, PublicKeyToken=f4fb5a3c4d1e5b4f'   !AIC.Core.EnumDescriptionConverter   enumDescriptionConverter(  "AIC.Core.BoolToVisibilityConverter  boolToVisibilityConverter   booleanToVisibilityConverter  ValueTemplate  �� DataType3  -AIC.OnLineDataPage.Models.SignalTileViewModel.   (System.Windows.Interactivity.Interaction   	Behaviors%  AIC.Core.FluidMouseDragBehavior   
DragButton � RowDefinitions  AccentColorBrush � ToolTip �� StringFormat �� Path �� Orientation.  (AIC.OnLineDataPage.Views.MoreInformation � DataContext	 �� 	Converter-  'MahApps.Metro.Controls.MetroProgressBar
 � Value �� TextAlignment �� RelativeSource # ChartBackgroundAccentColorBrush F�Name �� Stretch&   System.Windows.Media.TextOptions   TextFormattingMode ��Name  WhiteColorBrush �� Mode ��Name �� Triggers �� Value �� Binding �� 
TargetName �� Property �� Value  TimeDomainTemplate;  5AIC.OnLineDataPage.Views.SubViews.TimeDomainChartView  FrequencyDomainTemplate@	  :AIC.OnLineDataPage.Views.SubViews.FrequencyDomainChartView  	 Time3DSpectrumDomainTemplate7
  1AIC.OnLineDataPage.Views.SubViews.Time3DChartView 
 RPM3DSpectrumDomainTemplate6  0AIC.OnLineDataPage.Views.SubViews.RPM3DChartView  OrthoTemplate6  0AIC.OnLineDataPage.Views.SubViews.OrthoChartView  AMSTrendTemplate9  3AIC.OnLineDataPage.Views.SubViews.RMSTrendChartView  OrderAnalysisTemplate8  2AIC.OnLineDataPage.Views.SubViews.NyquistChartView  MultiDivFreTemplate<  6AIC.OnLineDataPage.Views.SubViews.MultiDivFreChartView  BodeTemplate5  /AIC.OnLineDataPage.Views.SubViews.BodeChartView  NyquistTemplate  PowerSpectrumTemplate>  8AIC.OnLineDataPage.Views.SubViews.PowerSpectrumChartView   PowerSpectrumDensityTemplateE  ?AIC.OnLineDataPage.Views.SubViews.PowerSpectrumDensityChartView  DiagnoseTemplate4  .AIC.OnLineDataPage.Views.SubViews.DiagnoseView�� D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    :x,http://schemas.microsoft.com/winfx/2006/xaml     ViPclr-namespace:System.Windows.Interactivity;assembly=System.Windows.Interactivity  =coreType0clr-namespace:AIC.CoreType;assembly=AIC.CoreType  1core(clr-namespace:AIC.Core;assembly=AIC.Core  ;Controls,http://metro.mahapps.com/winfx/xaml/controls  0views&clr-namespace:AIC.OnLineDataPage.Views  <subviews/clr-namespace:AIC.OnLineDataPage.Views.SubViews  2models'clr-namespace:AIC.OnLineDataPage.Models  %�i  &	        &	      &	 
     &	      7  &	 �    7   7   7  &	 �    7   7   7  &		     7   7   7  &	
 "    7   7   7  &	 )    7   7   7  &	 0    7   7   7  &	 #7    7   7   7  &	 *>    7   7   7  &	 1E    7   7   7  &	 8L    7   7   7  &	 ?S    7   7   7  &	 FZ    7   7   7  &	 Ma    7   7   7       �� �� "   .R�� ���� *	ItemWidth+���� *
ItemHeight+   $
 Left=�.+�� $��2q� �� $��28 ��� $��* �.+�� $��3,3,3,0q�#���   �� $
��Bold"���`� $	 {0}��.1��� $ Signal.DeviceItemName�.+��� $��5,0,0,0q�$ 
Horizontal=�.+��� ���	#FF87CDEE$��Center=���`� $	 {0}��.1��� $ Signal.DeviceItemName�  $��Center=� �� *Signal+� $��Right=�$��0, 0, 30, 0q�y��� *Signal.HasBattery+8	   .+��� 
���   �$��1q�$��32��$��20��._�  $��30��$��18��
��� � �
 �� *Signal.BatteryEnergy+�� $ Center=�
�������$��Center=�$��Center=����� *Signal.BatteryEnergy+$ F0���� $��Right=����� *CloseCommand+����  �� *Self+$ DataContext�.���� $1��+/AIC.Resources;component/Images/delete3.png��� $��1��$��3,0,3,3q�#���  .+�F� $ 	NotOKPath��$y�	Collapsed=�$
 Fill=�$��30q�
��������������C��!�X����!@�e���@�e��C�@�eX��X��@�e��C@�eӨ��@�e��!X����!��C���!��������!��C��!���Cpej�PIpej�����vX�`��Ѡ������h��E�an�E��CӐEP����pej��Cpej�pej��C�pej����E��C�EӰan�E�D�������� �S��v�_vON@@pej�Ipej��C(� $ Ideal=�.���� $ AMSText��#���  $��1,0,0,0q����� *Signal.Result+$ F3��$ TwoWay=��� ���	#FF85CBEB$��Center=�$��0,5,0,0q�$	��Top=�$��9(����� *	Signal.IP+�� $��Center=�$��Bottom=�$��1,0,0,0q����� *Signal.Unit+�� $ connectIcon����Z� F� $ TopLeftPath��$��2��"����k"��pѰ��pѰ��H	$
��Left=�$	��Top=�$��15��$��15��$
 Fill=����	#FF87CDEE$��3��F� $ TopRightPath��$��2��"�������kч	��kч	�e�	$��Right=�$	��Top=�$��15��$��15��$
 Fill=����	#FF87CDEE$��3��F� $ BottomLeftPath��$��2��'���P�Ed7�@�@��E�� ����� 	$
��Left=�$��Bottom=�$��15��$��15��$
 Fill=����	#FF87CDEE$��3��F� $ BottomRightPath��$��2��"����h�흤 �h���.р�7��.	$��Right=�$��Bottom=�$��15��$��15��$
 Fill=����	#FF87CDEE$��3�� �� $ false�� �� *Signal.IsConnected+.a��� $ connectIcon�� � ��$ ../Images/Disconnect.png���� $
 true�� �� *Signal.DelayIsNotOK+.a��� $ 	NotOKPath�� � y�$ Visible=��� $
 True�� �� *
IsSelected+.a��� $ TopLeftPath�� � ��
 � ����� $ TopRightPath�� � ��
 � ����� $ BottomLeftPath�� � ��
 � ����� $ BottomRightPath�� � ��
 � ����� $ Invalid�� �� *Signal.DelayAlarmGrade+.a��� $ AMSText�� � ��
 ��i���� $ HighPreAlarm�� �� *Signal.DelayAlarmGrade+.a��� $ AMSText�� � ��
 � ����� $ LowPreAlarm�� �� *Signal.DelayAlarmGrade+.a��� $ AMSText�� � ��
 � ����� $ 	HighAlarm�� �� *Signal.DelayAlarmGrade+.a��� $ AMSText�� � ��
 � ����� $ LowAlarm�� �� *Signal.DelayAlarmGrade+.a��� $ AMSText�� � ��
 � ����� $ 
HighDanger�� �� *Signal.DelayAlarmGrade+.a��� $ AMSText�� � ��
 �  ���� $ 	LowDanger�� �� *Signal.DelayAlarmGrade+.a��� $ AMSText�� � ��
 �  ���� "   .R�� ���� *	ItemWidth+���� *
ItemHeight+   $
 Left=�.+�� $��2q� �� $��28 ��� $��* �.+�� $��3,3,3,0q�#���   �� ���	#FF87CDEE$��Center=���`� $ {0}({1})��.1��� $ Signal.DeviceItemName��� $ Signal.Unit�8	   .+��� $��5,0,0,0q�$ 
Horizontal=�.+��� ���	#FF87CDEE$��Center=���`� $ {0}({1})��.1��� $ Signal.DeviceItemName��� $ Signal.Unit�8	    $��Center=� �� *Signal+� $��Right=�$��0, 0, 30, 0q�y��� *Signal.HasBattery+8	  .+��� 
���   �$��1q�$��32��$��20��._�  $��30��$��18��
��� � �
 �� *Signal.BatteryEnergy+�� $ Center=�
�������$��Center=�$��Center=����� *Signal.BatteryEnergy+$ F0���� $��Right=����� *CloseCommand+����  �� *Self+$ DataContext�.���� $1��+/AIC.Resources;component/Images/delete3.png��� $��1��$��3,0,3,3q�#���  .+�   �� *DataViewModel+F� $ TopLeftPath��$��2��"����k"��pѰ��pѰ��H	$
��Left=�$	��Top=�$��15��$��15��$
 Fill=����	#FF87CDEE$��3��F� $ TopRightPath��$��2��"�������kч	��kч	�e�	$��Right=�$	��Top=�$��15��$��15��$
 Fill=����	#FF87CDEE$��3��F� $ BottomLeftPath��$��2��'���P�Ed7�@�@��E�� ����� 	$
��Left=�$��Bottom=�$��15��$��15��$
 Fill=����	#FF87CDEE$��3��F� $ BottomRightPath��$��2��"����h�흤 �h���.р�7��.	$��Right=�$��Bottom=�$��15��$��15��$
 Fill=����	#FF87CDEE$��3�� �� $
 True�� �� *
IsSelected+.a��� $ TopLeftPath�� � ��
 � ����� $ TopRightPath�� � ��
 � ����� $ BottomLeftPath�� � ��
 � ����� $ BottomRightPath�� � ��
 � ����� "   .R�� ���� *	ItemWidth+���� *
ItemHeight+   $
 Left=�.+�� $��2q� �� $��28 ��� $��* �.+�� $��3,3,3,0q�#���   �� ���	#FF87CDEE$��Center=���`� $ {0}({1})��.1��� $ Signal.DeviceItemName��� $ Signal.Unit�8	   .+��� $��5,0,0,0q�$ 
Horizontal=�.+��� ���	#FF87CDEE$��Center=���`� $ {0}({1})��.1��� $ Signal.DeviceItemName��� $ Signal.Unit�8	    $��Center=� �� *Signal+� $��Right=�$��0, 0, 30, 0q�y��� *Signal.HasBattery+8	  .+��� 
���   �$��1q�$��32��$��20��._�  $��30��$��18��
��� � �
 �� *Signal.BatteryEnergy+�� $ Center=�
�������$��Center=�$��Center=����� *Signal.BatteryEnergy+$ F0���� $��Right=����� *CloseCommand+����  �� *Self+$ DataContext�.���� $1��+/AIC.Resources;component/Images/delete3.png��� $��1��$��3,0,3,3q�#���  .+�	  $��1�� �� *DataViewModel+F� $ TopLeftPath��$��2��"����k"��pѰ��pѰ��H	$
��Left=�$	��Top=�$��15��$��15��$
 Fill=����	#FF87CDEE$��3��F� $ TopRightPath��$��2��"�������kч	��kч	�e�	$��Right=�$	��Top=�$��15��$��15��$
 Fill=����	#FF87CDEE$��3��F� $ BottomLeftPath��$��2��'���P�Ed7�@�@��E�� ����� 	$
��Left=�$��Bottom=�$��15��$��15��$
 Fill=����	#FF87CDEE$��3��F� $ BottomRightPath��$��2��"����h�흤 �h���.р�7��.	$��Right=�$��Bottom=�$��15��$��15��$
 Fill=����	#FF87CDEE$��3�� �� $
 True�� �� *
IsSelected+.a��� $ TopLeftPath�� � ��
 � ����� $ TopRightPath�� � ��
 � ����� $ BottomLeftPath�� � ��
 � ����� $ BottomRightPath�� � ��
 � ����� "   .R�� ���� *	ItemWidth+���� *
ItemHeight+   $
 Left=�.+�� $��2q� �� $��28 ��� $��* �.+�� $��3,3,3,0q�#���   �� ���	#FF87CDEE$��Center=���`� $ {0}({1})��.1��� $ Signal.DeviceItemName��� $ Signal.Unit�8	   .+��� $��5,0,0,0q�$ 
Horizontal=�.+��� ���	#FF87CDEE$��Center=���`� $ {0}({1})��.1��� $ Signal.DeviceItemName��� $ Signal.Unit�8	    $��Center=� �� *Signal+� $��Right=�$��0, 0, 30, 0q�y��� *Signal.HasBattery+8	  .+��� 
���   �$��1q�$��32��$��20��._�  $��30��$��18��
��� � �
 �� *Signal.BatteryEnergy+�� $ Center=�
�������$��Center=�$��Center=����� *Signal.BatteryEnergy+$ F0���� $��Right=����� *CloseCommand+����  �� *Self+$ DataContext�.���� $1��+/AIC.Resources;component/Images/delete3.png��� $��1��$��3,0,3,3q�#���  .+�
   �� *DataViewModel+F� $ TopLeftPath��$��2��"����k"��pѰ��pѰ��H	$
��Left=�$	��Top=�$��15��$��15��$
 Fill=����	#FF87CDEE$��3��F� $ TopRightPath��$��2��"�������kч	��kч	�e�	$��Right=�$	��Top=�$��15��$��15��$
 Fill=����	#FF87CDEE$��3��F� $ BottomLeftPath��$��2��'���P�Ed7�@�@��E�� ����� 	$
��Left=�$��Bottom=�$��15��$��15��$
 Fill=����	#FF87CDEE$��3��F� $ BottomRightPath��$��2��"����h�흤 �h���.р�7��.	$��Right=�$��Bottom=�$��15��$��15��$
 Fill=����	#FF87CDEE$��3�� �� $
 True�� �� *
IsSelected+.a��� $ TopLeftPath�� � ��
 � ����� $ TopRightPath�� � ��
 � ����� $ BottomLeftPath�� � ��
 � ����� $ BottomRightPath�� � ��
 � ����� "   .R�� ���� *	ItemWidth+���� *
ItemHeight+   $
 Left=�.+�� $��2q� �� $��28 ��� $��* �.+�� $��3,3,3,0q�#���   �� ���	#FF87CDEE$��Center=���`� $ {0}({1})��.1��� $ Signal.DeviceItemName��� $ Signal.Unit�8	   .+��� $��5,0,0,0q�$ 
Horizontal=�.+��� ���	#FF87CDEE$��Center=���`� $ {0}({1})��.1��� $ Signal.DeviceItemName��� $ Signal.Unit�8	    $��Center=� �� *Signal+� $��Right=�$��0, 0, 30, 0q�y��� *Signal.HasBattery+8	  .+��� 
���   �$��1q�$��32��$��20��._�  $��30��$��18��
��� � �
 �� *Signal.BatteryEnergy+�� $ Center=�
�������$��Center=�$��Center=����� *Signal.BatteryEnergy+$ F0���� $��Right=����� *CloseCommand+����  �� *Self+$ DataContext�.���� $1��+/AIC.Resources;component/Images/delete3.png��� $��1��$��3,0,3,3q�#���  .+�   �� *DataViewModel+F� $ TopLeftPath��$��2��"����k"��pѰ��pѰ��H	$
��Left=�$	��Top=�$��15��$��15��$
 Fill=����	#FF87CDEE$��3��F� $ TopRightPath��$��2��"�������kч	��kч	�e�	$��Right=�$	��Top=�$��15��$��15��$
 Fill=����	#FF87CDEE$��3��F� $ BottomLeftPath��$��2��'���P�Ed7�@�@��E�� ����� 	$
��Left=�$��Bottom=�$��15��$��15��$
 Fill=����	#FF87CDEE$��3��F� $ BottomRightPath��$��2��"����h�흤 �h���.р�7��.	$��Right=�$��Bottom=�$��15��$��15��$
 Fill=����	#FF87CDEE$��3�� �� $
 True�� �� *
IsSelected+.a��� $ TopLeftPath�� � ��
 � ����� $ TopRightPath�� � ��
 � ����� $ BottomLeftPath�� � ��
 � ����� $ BottomRightPath�� � ��
 � ����� "   .R�� ���� *	ItemWidth+���� *
ItemHeight+   $
 Left=�.+�� $��2q� �� $��28 ��� $��* �.+�� $��3,3,3,0q�#���   �� ���	#FF87CDEE$��Center=���`� $ {0}({1})��.1��� $ Signal.DeviceItemName��� $ Signal.Unit�8	   .+��� $��5,0,0,0q�$ 
Horizontal=�.+��� ���	#FF87CDEE$��Center=���`� $ {0}({1})��.1��� $ Signal.DeviceItemName��� $ Signal.Unit�8	    $��Center=� �� *Signal+� $��Right=�$��0, 0, 30, 0q�y��� *Signal.HasBattery+8	  .+��� 
���   �$��1q�$��32��$��20��._�  $��30��$��18��
��� � �
 �� *Signal.BatteryEnergy+�� $ Center=�
�������$��Center=�$��Center=����� *Signal.BatteryEnergy+$ F0���� $��Right=����� *CloseCommand+����  �� *Self+$ DataContext�.���� $1��+/AIC.Resources;component/Images/delete3.png��� $��1��$��3,0,3,3q�#���  .+�   �� *DataViewModel+F� $ TopLeftPath��$��2��"����k"��pѰ��pѰ��H	$
��Left=�$	��Top=�$��15��$��15��$
 Fill=����	#FF87CDEE$��3��F� $ TopRightPath��$��2��"�������kч	��kч	�e�	$��Right=�$	��Top=�$��15��$��15��$
 Fill=����	#FF87CDEE$��3��F� $ BottomLeftPath��$��2��'���P�Ed7�@�@��E�� ����� 	$
��Left=�$��Bottom=�$��15��$��15��$
 Fill=����	#FF87CDEE$��3��F� $ BottomRightPath��$��2��"����h�흤 �h���.р�7��.	$��Right=�$��Bottom=�$��15��$��15��$
 Fill=����	#FF87CDEE$��3�� �� $
 True�� �� *
IsSelected+.a��� $ TopLeftPath�� � ��
 � ����� $ TopRightPath�� � ��
 � ����� $ BottomLeftPath�� � ��
 � ����� $ BottomRightPath�� � ��
 � ����� "   .R�� ���� *	ItemWidth+���� *
ItemHeight+   $
 Left=�.+�� $��2q� �� $��28 ��� $��* �.+�� $��3,3,3,0q�#���   �� ���	#FF87CDEE$��Center=���`� $ {0}({1})��.1��� $ Signal.DeviceItemName��� $ Signal.Unit�8	   .+��� $��5,0,0,0q�$ 
Horizontal=�.+��� ���	#FF87CDEE$��Center=���`� $ {0}({1})��.1��� $ Signal.DeviceItemName��� $ Signal.Unit�8	    $��Center=� �� *Signal+� $��Right=�$��0, 0, 30, 0q�y��� *Signal.HasBattery+8	  .+��� 
���   �$��1q�$��32��$��20��._�  $��30��$��18��
��� � �
 �� *Signal.BatteryEnergy+�� $ Center=�
�������$��Center=�$��Center=����� *Signal.BatteryEnergy+$ F0���� $��Right=����� *CloseCommand+����  �� *Self+$ DataContext�.���� $1��+/AIC.Resources;component/Images/delete3.png��� $��1��$��3,0,3,3q�#���  .+�   �� *DataViewModel+F� $ TopLeftPath��$��2��"����k"��pѰ��pѰ��H	$
��Left=�$	��Top=�$��15��$��15��$
 Fill=����	#FF87CDEE$��3��F� $ TopRightPath��$��2��"�������kч	��kч	�e�	$��Right=�$	��Top=�$��15��$��15��$
 Fill=����	#FF87CDEE$��3��F� $ BottomLeftPath��$��2��'���P�Ed7�@�@��E�� ����� 	$
��Left=�$��Bottom=�$��15��$��15��$
 Fill=����	#FF87CDEE$��3��F� $ BottomRightPath��$��2��"����h�흤 �h���.р�7��.	$��Right=�$��Bottom=�$��15��$��15��$
 Fill=����	#FF87CDEE$��3�� �� $
 True�� �� *
IsSelected+.a��� $ TopLeftPath�� � ��
 � ����� $ TopRightPath�� � ��
 � ����� $ BottomLeftPath�� � ��
 � ����� $ BottomRightPath�� � ��
 � ����� "   .R�� ���� *	ItemWidth+���� *
ItemHeight+   $
 Left=�.+�� $��2q� �� $��28 ��� $��* �.+�� $��3,3,3,0q�#���   �� ���	#FF87CDEE$��Center=���`� $ {0}({1})��.1��� $ Signal.DeviceItemName��� $ Signal.Unit�8	   .+��� $��5,0,0,0q�$ 
Horizontal=�.+��� ���	#FF87CDEE$��Center=���`� $ {0}({1})��.1��� $ Signal.DeviceItemName��� $ Signal.Unit�8	    $��Center=� �� *Signal+� $��Right=�$��0, 0, 30, 0q�y��� *Signal.HasBattery+8	  .+��� 
���   �$��1q�$��32��$��20��._�  $��30��$��18��
��� � �
 �� *Signal.BatteryEnergy+�� $ Center=�
�������$��Center=�$��Center=����� *Signal.BatteryEnergy+$ F0���� $��Right=����� *CloseCommand+����  �� *Self+$ DataContext�.���� $1��+/AIC.Resources;component/Images/delete3.png��� $��1��$��3,0,3,3q�#���  .+�   �� *DataViewModel+F� $ TopLeftPath��$��2��"����k"��pѰ��pѰ��H	$
��Left=�$	��Top=�$��15��$��15��$
 Fill=����	#FF87CDEE$��3��F� $ TopRightPath��$��2��"�������kч	��kч	�e�	$��Right=�$	��Top=�$��15��$��15��$
 Fill=����	#FF87CDEE$��3��F� $ BottomLeftPath��$��2��'���P�Ed7�@�@��E�� ����� 	$
��Left=�$��Bottom=�$��15��$��15��$
 Fill=����	#FF87CDEE$��3��F� $ BottomRightPath��$��2��"����h�흤 �h���.р�7��.	$��Right=�$��Bottom=�$��15��$��15��$
 Fill=����	#FF87CDEE$��3�� �� $
 True�� �� *
IsSelected+.a��� $ TopLeftPath�� � ��
 � ����� $ TopRightPath�� � ��
 � ����� $ BottomLeftPath�� � ��
 � ����� $ BottomRightPath�� � ��
 � ����� "   .R�� ���� *	ItemWidth+���� *
ItemHeight+   $
 Left=�.+�� $��2q� �� $��28 ��� $��* �.+�� $��3,3,3,0q�#���   �� ���	#FF87CDEE$��Center=���`� $ {0}({1})��.1��� $ Signal.DeviceItemName��� $ Signal.Unit�8	   .+��� $��5,0,0,0q�$ 
Horizontal=�.+��� ���	#FF87CDEE$��Center=���`� $ {0}({1})��.1��� $ Signal.DeviceItemName��� $ Signal.Unit�8	    $��Center=� �� *Signal+� $��Right=�$��0, 0, 30, 0q�y��� *Signal.HasBattery+8	  .+��� 
���   �$��1q�$��32��$��20��._�  $��30��$��18��
��� � �
 �� *Signal.BatteryEnergy+�� $ Center=�
�������$��Center=�$��Center=����� *Signal.BatteryEnergy+$ F0���� $��Right=����� *CloseCommand+����  �� *Self+$ DataContext�.���� $1��+/AIC.Resources;component/Images/delete3.png��� $��1��$��3,0,3,3q�#���  .+�   �� *DataViewModel+F� $ TopLeftPath��$��2��"����k"��pѰ��pѰ��H	$
��Left=�$	��Top=�$��15��$��15��$
 Fill=����	#FF87CDEE$��3��F� $ TopRightPath��$��2��"�������kч	��kч	�e�	$��Right=�$	��Top=�$��15��$��15��$
 Fill=����	#FF87CDEE$��3��F� $ BottomLeftPath��$��2��'���P�Ed7�@�@��E�� ����� 	$
��Left=�$��Bottom=�$��15��$��15��$
 Fill=����	#FF87CDEE$��3��F� $ BottomRightPath��$��2��"����h�흤 �h���.р�7��.	$��Right=�$��Bottom=�$��15��$��15��$
 Fill=����	#FF87CDEE$��3�� �� $
 True�� �� *
IsSelected+.a��� $ TopLeftPath�� � ��
 � ����� $ TopRightPath�� � ��
 � ����� $ BottomLeftPath�� � ��
 � ����� $ BottomRightPath�� � ��
 � ����� "   .R�� ���� *	ItemWidth+���� *
ItemHeight+   $
 Left=�.+�� $��2q� �� $��28 ��� $��* �.+�� $��3,3,3,0q�#���   �� ���	#FF87CDEE$��Center=���`� $ {0}({1})��.1��� $ Signal.DeviceItemName��� $ Signal.Unit�8	   .+��� $��5,0,0,0q�$ 
Horizontal=�.+��� ���	#FF87CDEE$��Center=���`� $ {0}({1})��.1��� $ Signal.DeviceItemName��� $ Signal.Unit�8	    $��Center=� �� *Signal+� $��Right=�$��0, 0, 30, 0q�y��� *Signal.HasBattery+8	  .+��� 
���   �$��1q�$��32��$��20��._�  $��30��$��18��
��� � �
 �� *Signal.BatteryEnergy+�� $ Center=�
�������$��Center=�$��Center=����� *Signal.BatteryEnergy+$ F0���� $��Right=����� *CloseCommand+����  �� *Self+$ DataContext�.���� $1��+/AIC.Resources;component/Images/delete3.png��� $��1��$��3,0,3,3q�#���  .+�   �� *DataViewModel+F� $ TopLeftPath��$��2��"����k"��pѰ��pѰ��H	$
��Left=�$	��Top=�$��15��$��15��$
 Fill=����	#FF87CDEE$��3��F� $ TopRightPath��$��2��"�������kч	��kч	�e�	$��Right=�$	��Top=�$��15��$��15��$
 Fill=����	#FF87CDEE$��3��F� $ BottomLeftPath��$��2��'���P�Ed7�@�@��E�� ����� 	$
��Left=�$��Bottom=�$��15��$��15��$
 Fill=����	#FF87CDEE$��3��F� $ BottomRightPath��$��2��"����h�흤 �h���.р�7��.	$��Right=�$��Bottom=�$��15��$��15��$
 Fill=����	#FF87CDEE$��3�� �� $
 True�� �� *
IsSelected+.a��� $ TopLeftPath�� � ��
 � ����� $ TopRightPath�� � ��
 � ����� $ BottomLeftPath�� � ��
 � ����� $ BottomRightPath�� � ��
 � ����� "   .R�� ���� *	ItemWidth+���� *
ItemHeight+   $
 Left=�.+�� $��2q� �� $��28 ��� $��* �.+�� $��3,3,3,0q�#���   �� ���	#FF87CDEE$��Center=���`� $ {0}({1})��.1��� $ Signal.DeviceItemName��� $ Signal.Unit�8	   .+��� $��5,0,0,0q�$ 
Horizontal=�.+��� ���	#FF87CDEE$��Center=���`� $ {0}({1})��.1��� $ Signal.DeviceItemName��� $ Signal.Unit�8	    $��Center=� �� *Signal+� $��Right=�$��0, 0, 30, 0q�y��� *Signal.HasBattery+8	  .+��� 
���   �$��1q�$��32��$��20��._�  $��30��$��18��
��� � �
 �� *Signal.BatteryEnergy+�� $ Center=�
�������$��Center=�$��Center=����� *Signal.BatteryEnergy+$ F0���� $��Right=����� *CloseCommand+����  �� *Self+$ DataContext�.���� $1��+/AIC.Resources;component/Images/delete3.png��� $��1��$��3,0,3,3q�#���  .+�   �� *DataViewModel+F� $ TopLeftPath��$��2��"����k"��pѰ��pѰ��H	$
��Left=�$	��Top=�$��15��$��15��$
 Fill=����	#FF87CDEE$��3��F� $ TopRightPath��$��2��"�������kч	��kч	�e�	$��Right=�$	��Top=�$��15��$��15��$
 Fill=����	#FF87CDEE$��3��F� $ BottomLeftPath��$��2��'���P�Ed7�@�@��E�� ����� 	$
��Left=�$��Bottom=�$��15��$��15��$
 Fill=����	#FF87CDEE$��3��F� $ BottomRightPath��$��2��"����h�흤 �h���.р�7��.	$��Right=�$��Bottom=�$��15��$��15��$
 Fill=����	#FF87CDEE$��3�� �� $
 True�� �� *
IsSelected+.a��� $ TopLeftPath�� � ��
 � ����� $ TopRightPath�� � ��
 � ����� $ BottomLeftPath�� � ��
 � ����� $ BottomRightPath�� � ��
 � ����� "   .R�� ���� *	ItemWidth+���� *
ItemHeight+   $
 Left=�.+�� $��2q� �� $��28 ��� $��* �.+�� $��3,3,3,0q�#���   �� ���	#FF87CDEE$��Center=���`� $ {0}({1})��.1��� $ Signal.DeviceItemName��� $ Signal.Unit�8	   .+��� $��5,0,0,0q�$ 
Horizontal=�.+��� ���	#FF87CDEE$��Center=���`� $ {0}({1})��.1��� $ Signal.DeviceItemName��� $ Signal.Unit�8	    $��Center=� �� *Signal+� $��Right=�$��0, 0, 30, 0q�y��� *Signal.HasBattery+8	  .+��� 
���   �$��1q�$��32��$��20��._�  $��30��$��18��
��� � �
 �� *Signal.BatteryEnergy+�� $ Center=�
�������$��Center=�$��Center=����� *Signal.BatteryEnergy+$ F0���� $��Right=����� *CloseCommand+����  �� *Self+$ DataContext�.���� $1��+/AIC.Resources;component/Images/delete3.png��� $��1��$��3,0,3,3q�#���  .+�   �� *DataViewModel+F� $ TopLeftPath��$��2��"����k"��pѰ��pѰ��H	$
��Left=�$	��Top=�$��15��$��15��$
 Fill=����	#FF87CDEE$��3��F� $ TopRightPath��$��2��"�������kч	��kч	�e�	$��Right=�$	��Top=�$��15��$��15��$
 Fill=����	#FF87CDEE$��3��F� $ BottomLeftPath��$��2��'���P�Ed7�@�@��E�� ����� 	$
��Left=�$��Bottom=�$��15��$��15��$
 Fill=����	#FF87CDEE$��3��F� $ BottomRightPath��$��2��"����h�흤 �h���.р�7��.	$��Right=�$��Bottom=�$��15��$��15��$
 Fill=����	#FF87CDEE$��3�� �� $
 True�� �� *
IsSelected+.a��� $ TopLeftPath�� � ��
 � ����� $ TopRightPath�� � ��
 � ����� $ BottomLeftPath�� � ��
 � ����� $ BottomRightPath�� � ��
 � ����� "   .R�� ���� *	ItemWidth+���� *
ItemHeight+   $
 Left=�.+�� $��2q� �� $��28 ��� $��* �.+�� $��3,3,3,0q�#���   �� ���	#FF87CDEE$��Center=���`� $ {0}({1})��.1��� $ Signal.DeviceItemName��� $ Signal.Unit�8	   .+��� $��5,0,0,0q�$ 
Horizontal=�.+��� ���	#FF87CDEE$��Center=���`� $ {0}({1})��.1��� $ Signal.DeviceItemName��� $ Signal.Unit�8	    $��Center=� �� *Signal+� $��Right=�$��0, 0, 30, 0q�y��� *Signal.HasBattery+8	  .+��� 
���   �$��1q�$��32��$��20��._�  $��30��$��18��
��� � �
 �� *Signal.BatteryEnergy+�� $ Center=�
�������$��Center=�$��Center=����� *Signal.BatteryEnergy+$ F0���� $��Right=����� *CloseCommand+����  �� *Self+$ DataContext�.���� $1��+/AIC.Resources;component/Images/delete3.png��� $��1��$��3,0,3,3q�#���  .+�   �� *DataViewModel+F� $ TopLeftPath��$��2��"����k"��pѰ��pѰ��H	$
��Left=�$	��Top=�$��15��$��15��$
 Fill=����	#FF87CDEE$��3��F� $ TopRightPath��$��2��"�������kч	��kч	�e�	$��Right=�$	��Top=�$��15��$��15��$
 Fill=����	#FF87CDEE$��3��F� $ BottomLeftPath��$��2��'���P�Ed7�@�@��E�� ����� 	$
��Left=�$��Bottom=�$��15��$��15��$
 Fill=����	#FF87CDEE$��3��F� $ BottomRightPath��$��2��"����h�흤 �h���.р�7��.	$��Right=�$��Bottom=�$��15��$��15��$
 Fill=����	#FF87CDEE$��3�� �� $
 True�� �� *
IsSelected+.a��� $ TopLeftPath�� � ��
 � ����� $ TopRightPath�� � ��
 � ����� $ BottomLeftPath�� � ��
 � ����� $ BottomRightPath�� � ��
 � ����� "   .R�� ���� *	ItemWidth+���� *
ItemHeight+   $
 Left=�.+�� $��2q� �� $��28 ��� $��* �.+�� $��3,3,3,0q�#���   �� ���	#FF87CDEE$��Center=���`� $ {0}({1})��.1��� $ Signal.DeviceItemName��� $ Signal.Unit�8	   .+��� $��5,0,0,0q�$ 
Horizontal=�.+��� ���	#FF87CDEE$��Center=���`� $ {0}({1})��.1��� $ Signal.DeviceItemName��� $ Signal.Unit�8	    $��Center=� �� *Signal+� $��Right=�$��0, 0, 30, 0q�y��� *Signal.HasBattery+8	  .+��� 
���   �$��1q�$��32��$��20��._�  $��30��$��18��
��� � �
 �� *Signal.BatteryEnergy+�� $ Center=�
�������$��Center=�$��Center=����� *Signal.BatteryEnergy+$ F0���� $��Right=����� *CloseCommand+����  �� *Self+$ DataContext�.���� $1��+/AIC.Resources;component/Images/delete3.png��� $��1��$��3,0,3,3q�#���  .+�   �� *DataViewModel+F� $ TopLeftPath��$��2��"����k"��pѰ��pѰ��H	$
��Left=�$	��Top=�$��15��$��15��$
 Fill=����	#FF87CDEE$��3��F� $ TopRightPath��$��2��"�������kч	��kч	�e�	$��Right=�$	��Top=�$��15��$��15��$
 Fill=����	#FF87CDEE$��3��F� $ BottomLeftPath��$��2��'���P�Ed7�@�@��E�� ����� 	$
��Left=�$��Bottom=�$��15��$��15��$
 Fill=����	#FF87CDEE$��3��F� $ BottomRightPath��$��2��"����h�흤 �h���.р�7��.	$��Right=�$��Bottom=�$��15��$��15��$
 Fill=����	#FF87CDEE$��3�� �� $
 True�� �� *
IsSelected+.a��� $ TopLeftPath�� � ��
 � ����� $ TopRightPath�� � ��
 � ����� $ BottomLeftPath�� � ��
 � ����� $ BottomRightPath�� � ��
 � ���