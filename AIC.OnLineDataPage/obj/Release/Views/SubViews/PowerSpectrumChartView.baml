   M S B A M L   `   `   `  ���� M  IAIC.OnLineDataPage, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullU/clr-namespace:AIC.OnLineDataPage.Views.SubViews!AIC.OnLineDataPage.Views.SubViews  C ?AIC.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5(clr-namespace:AIC.Core;assembly=AIC.CoreAIC.Core G CAIC.CoreType, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullA0clr-namespace:AIC.CoreType;assembly=AIC.CoreTypeAIC.CoreType >    8AIC.OnLineDataPage.Views.SubViews.PowerSpectrumChartView   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     ?presentation&http://www.aicmonitor.com/Presentation     @view/clr-namespace:AIC.OnLineDataPage.Views.SubViews     @d2http://schemas.microsoft.com/expression/blend/2008     9core(clr-namespace:AIC.Core;assembly=AIC.Core     EcoreType0clr-namespace:AIC.CoreType;assembly=AIC.CoreType     X TXceed.Wpf.Toolkit, Version=3.2.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit  c _System.Windows.Interactivity, Version=4.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity  -   5   /AIC.OnLineDataPage.Views.SubViews.ChartViewBase   Name$  timeDomainOnLineView�� J� RenderTransformOrigin$ 0.5,0.5&�z�V� .~��� �� �� T�  � 	Resources '  !AIC.Core.EnumDescriptionConverter   enumDescriptionConverter  enumComboItemTemplate �� Path �� 	Converter  AIC.Core.BindingProxy 	 proxy   Data#  AIC.Core.EnumBooleanConverter  enumBooleanConverter(  "AIC.Core.EnumToVisibilityConverter  enumToVisibilityConverter   Reverse  ButtonStyle �� 
TargetType �� Property	 �� Value
 �� 
TargetType J� SnapsToDevicePixels �� Triggers P� Property P� Value  OnLoaded �� 
TargetName �� TargetProperty W� From	 W� To g� Duration��%Y  &	        &	      7   &	 (     &	 6     &	 ;     &	 G     &	 �      �� .R��� ���� $ .�8      ��      .  �� " ��.���  � ��
	 ���� ��  � ��	 �	#FF87CDEE��  � ��$	 Center=���  � ��$	 Center=���  � ��$	 1q���  � ��	 �� "
 ��.R�� #��z��.+��� #��z��#��z����. # z #��z�� P�  �@J�IsMouseOver . . ���  � ��	 �	#7F243859P�  � {� .  . ���  � ��	 �#ADADADP�  � �� . . ���  � ��	 �	#FF20437E�� .��W� $ timeDomainOnLineView��$V P(UIElement.RenderTransform).(TransformGroup.Children)[0].(ScaleTransform.ScaleX)�$	 0.0X�$	 1.0X�$ 0:0:0.3D�W� $ timeDomainOnLineView��$V P(UIElement.RenderTransform).(TransformGroup.Children)[0].(ScaleTransform.ScaleY)�$	 0.0X�$	 1.0X�$ 0:0:0.3D� � Triggers :�  :� RoutedEvent$ FrameworkElement.Loaded��.[��� -    ��Name$ OnLoaded2_BeginStoryboard��#��[ .���  � RowDefinitions �� $��* ��� $
��Auto �.+�� -   $��	gridChart���� -    ��Name$ txtValue��  WhiteColorBrush#���  $��16(�$��2���� $	��Top=�$��Right=� �� Orientation$ 
Horizontal=�.+��� -    ��Name$ filterCheckBox��$��Center=� d� 	IsChecked �� *
IsFilter+&   Xceed.Wpf.Toolkit.DropDownButton  -     Name$ dropDownButton��
������ $��滤波��$��16(�$��Center=�   DropDownContent � $	��320�� �� $
��auto ��� $��* �.+��� $ 
Horizontal=�.+�	� -    	�Name$ 
bandPassRb��  RadioButtonStyle#��[ 
���   �$��带通��$��Center=� ��  �� Source# [ $ Data.Signal.FilterType�# [   �� ConverterParameter$  BandPass��	� -   $ 
highPassRb��#��[ 
���   �$��高通��$��Center=� �� # [ $ Data.Signal.FilterType�# [ $  HighPass��	� -	   $ 	lowPassRb��#��[ 
���   �$��低通��$��Center=� �� # [ $ Data.Signal.FilterType�# [ $  LowPass��� $��1��$��5,5,25,0q�y��� # [ $ Data.Signal.FilterType�# [   AIC.CoreType.FilterType!   BandPass#  Z! " �� FallbackValue$" 	Collapsed�� �� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto �# � ColumnDefinitions# �� $
��auto ��� $��* �.+��� $��通带衰减��$  Xceed.Wpf.Toolkit.DoubleUpDown  $��1���	  �Xceed.Wpf.Toolkit.Primitives.UpDownBase`1[[System.Nullable`1[[System.Double, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]$ 	  Minimum$
$ 0.01X�% 	  Maximum$% 3X��
  �Xceed.Wpf.Toolkit.NumericUpDown`1[[System.Nullable`1[[System.Double, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]& 
  	Increment$	& 0.1X�' 	  Value' �� # [ $0 *Data.Signal.BPFilter.PassbandAttenuationDB��� $��1��$��阻带衰减��  $��1��$��1��$$ 40X�$	% 120X�' �� # [ $0 *Data.Signal.BPFilter.StopbandAttenuationDB���  	 CheckBoxStyle#��[	 
���   �$��2��$��绑定转速��$��2�� �� # [ $' !Data.Signal.BPFilter.IsRPMBinding�.  (System.Windows.Interactivity.Interaction(   Triggers( /  )System.Windows.Interactivity.EventTrigger  )   	EventName$) Checked��.  (System.Windows.Interactivity.TriggerBase*   Actions.* 6  0System.Windows.Interactivity.InvokeCommandAction  +   Command+ �� # [ $) #Data.Signal.BPFilter.BindRPMCommand�,   CommandParameter, �� # [ $ Data.Signal.RPM�  $) 	Unchecked��.*   + �� # [ $+ %Data.Signal.BPFilter.UnBindRPMCommand�, �� # [ $ Data.Signal.RPM��� $��3��$��低逼近带通��  $��3��$��1��$$ 0X�' �� # [ $+ %Data.Signal.BPFilter.BPPassBandFreLow��� $��4��$��高逼近带通��  $��4��$��1��$$ 0X�' �� # [ $, &Data.Signal.BPFilter.BPPassBandFreHigh��� $��5��$��过渡带宽��  $��5��$��1��$$ 0X�' �� # [ $. (Data.Signal.BPFilter.TransitionBandwidth�A� $��6��$��2��.+��� $��保存��$��5q����� # [ $&  Data.Signal.BPFilter.SaveCommand��� $��重置��$��5q����� # [ $' !Data.Signal.BPFilter.ResetCommand�� $��1��$��5,5,25,0q�y��� # [ $ Data.Signal.FilterType�# [ -   HighPass#  Z- $" 	Collapsed�� �� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto �# �� $
��auto ��� $��* �.+��� $��通带衰减��  $��1��$
$ 0.01X�$% 3X�$	& 0.1X�' �� # [ $0 *Data.Signal.HPFilter.PassbandAttenuationDB��� $��1��$��阻带衰减��  $��1��$��1��$$ 40X�$	% 120X�' �� # [ $0 *Data.Signal.HPFilter.StopbandAttenuationDB��� #��[	 
���   �$��2��$��绑定转速��$��2�� �� # [ $' !Data.Signal.HPFilter.IsRPMBinding�. �� Mode$. TwoWay=�(   $) Checked��.*   + �� # [ $) #Data.Signal.HPFilter.BindRPMCommand�, �� # [ $ Data.Signal.RPM�  $) 	Unchecked��.*   + �� # [ $+ %Data.Signal.HPFilter.UnBindRPMCommand�, �� # [ $ Data.Signal.RPM��� $��3��$��通带��  $��3��$��1��$$ 0X�' �� # [ $&  Data.Signal.HPFilter.PassbandFre��� $��4��$��阻带��  $��4��$��1��$$ 0X�' �� # [ $&  Data.Signal.HPFilter.StopbandFre�A� $��6��$��2��.+��� $��保存��$��5q����� # [ $&  Data.Signal.HPFilter.SaveCommand��� $��重置��$��5q����� # [ $' !Data.Signal.HPFilter.ResetCommand�� $��1��$��5,5,25,0q�y��� # [ $ Data.Signal.FilterType�# [ /   LowPass#  Z/ $" 	Collapsed�� �� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto �# �� $
��auto ��� $��* �.+��� $��通带衰减��  $��1��$
$ 0.01X�$% 3X�$	& 0.1X�' �� # [ $0 *Data.Signal.LPFilter.PassbandAttenuationDB��� $��1��$��阻带衰减��  $��1��$��1��$$ 40X�$	% 120X�' �� # [ $0 *Data.Signal.LPFilter.StopbandAttenuationDB��� #��[	 
���   �$��2��$��绑定转速��$��2�� �� # [ $' !Data.Signal.LPFilter.IsRPMBinding�$. TwoWay=�(   $) Checked��.*   + �� # [ $) #Data.Signal.LPFilter.BindRPMCommand�, �� # [ $ Data.Signal.RPM�  $) 	Unchecked��.*   + �� # [ $+ %Data.Signal.LPFilter.UnBindRPMCommand�, �� # [ $ Data.Signal.RPM��� $��3��$��通带��  $��3��$��1��$$ 0X�' �� # [ $&  Data.Signal.LPFilter.PassbandFre��� $��4��$��阻带��  $��4��$��1��$$ 0X�' �� # [ $&  Data.Signal.LPFilter.StopbandFre�A� $��6��$��2��.+��� $��保存��$��5q����� # [ $&  Data.Signal.LPFilter.SaveCommand��� $��重置��$��5q����� # [ $' !Data.Signal.LPFilter.ResetCommand��� 0 �� 
IsEditable0 .  #��[   AIC.Core.EnumHelper1   Enum(  "AIC.CoreType.SignalPreProccessType"1  $��80��$��Center=�
������ $��5,0,5,0q�2 �� SelectedItem2 �� *SignalPreProccessType+����  
 VirtualisedMetroComboBox3 �� BasedOn#3 [
 " ��.���  � ��$	 0q���  � ��$	 0q��� $��dB/Hz��$��Center=�$��0,0,5,0q� �� *Signal.IsPowerSpectrumDB+�� -
   $ fitViewCheckBox��$��Center=�$
 TrueY��� -   $��Right=�#��[ $��	功率谱��$��0,0,5,0q�4 � ToolTip4 �� $��截图��