   M S B A M L   `   `   `  ����M  IAIC.OnLineDataPage, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullU/clr-namespace:AIC.OnLineDataPage.Views.SubViews!AIC.OnLineDataPage.Views.SubViews  5      C ?AIC.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5(clr-namespace:AIC.Core;assembly=AIC.CoreAIC.Core 5      G CAIC.CoreType, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullA0clr-namespace:AIC.CoreType;assembly=AIC.CoreTypeAIC.CoreType 5	      >    8AIC.OnLineDataPage.Views.SubViews.PowerSpectrumChartView   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      ?presentation&http://www.aicmonitor.com/Presentation     5      @view/clr-namespace:AIC.OnLineDataPage.Views.SubViews     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      9core(clr-namespace:AIC.Core;assembly=AIC.Core     5      EcoreType0clr-namespace:AIC.CoreType;assembly=AIC.CoreType     5	      X TXceed.Wpf.Toolkit, Version=3.2.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit  5
      c _System.Windows.Interactivity, Version=4.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity  5      -   5        5   /AIC.OnLineDataPage.Views.SubViews.ChartViewBase   Name$  timeDomainOnLineView��5       J� RenderTransformOrigin$ 0.5,0.5&�5      z�5      V� 5   
   .~�5      �� �� 5      �� 5      T� 5      5       � 	Resources 5      '  !AIC.Core.EnumDescriptionConverter   enumDescriptionConverter  enumComboItemTemplate �� Path �� 	Converter  AIC.Core.BindingProxy 	 proxy   Data#  AIC.Core.EnumBooleanConverter  enumBooleanConverter(  "AIC.Core.EnumToVisibilityConverter  enumToVisibilityConverter   Reverse  ButtonStyle �� 
TargetType �� Property	 �� Value
 �� 
TargetType J� SnapsToDevicePixels �� Triggers P� Property P� Value  OnLoaded �� 
TargetName �� TargetProperty W� From	 W� To g� Duration��%K  &	        &	      7   &	 V     &	 r     &	 �     &	 �     &	 Y      5   
   6(   �� 5   
   .R�5      �� ��6   �� $ .�8   5        5   
    6*   ��   5   
   6$     5   
    .  6K   �� 5    
   " ��6$   .�5!      ��  � ��
	 ���� 6+   �� 5"       � ��	 �	#FF87CDEE6+   �� 5#       � ��$	 Center=�6;   �� 5$       � ��$	 Center=�69   �� 5%       � ��$	 1q�6(   �� 5&       � ��	 5'      �� 5(      "
 ��6&   .R�5)      � #��z��6   .+�5*      �� #��z��6/   #��z��6r   ��. # z 6�   #��z��6�   5+       5,      P� 5-       �@J�IsMouseOver . . �5.   "   ��  � ��	 �	#7F2438596?   5/      P� 50       � {� .  . �51   "   ��  � ��	 �#ADADAD6?   52      P� 53       � �� . . �54   "   ��  � ��	 �	#FF20437E6?   55      57      59      5:      �� 5;   
   .��5<      W� $ timeDomainOnLineView��5=      $V P(UIElement.RenderTransform).(TransformGroup.Children)[0].(ScaleTransform.ScaleX)�5>      $	 0.0X�5?      $	 1.0X�6   $ 0:0:0.3D�6#   W� 5@      $ timeDomainOnLineView��5A      $V P(UIElement.RenderTransform).(TransformGroup.Children)[0].(ScaleTransform.ScaleY)�5B      $	 0.0X�5C      $	 1.0X�6   $ 0:0:0.3D�6#   5D      5E       � Triggers 5F      :� 5G   
    :� RoutedEvent$ FrameworkElement.Loaded��6   .[�5H      �� -   5         ��Name$ OnLoaded2_BeginStoryboard��5H      #��[ 6A   5I      .��5K      �  � RowDefinitions 5L   
   �� 5M      $��* �6   �� 5N      $
��Auto �6   .+�5P   
   � -   5        $��	gridChart��5P      5Q      �� 5R   
   -   5         ��Name$ txtValue��5R        WhiteColorBrush#���  6&   $��16(�6U   $��2��6c   �� 5S   
   $	��Top=�6   $��Right=�6.    �� Orientation$ 
Horizontal=�6J   .+�5T      �� -   5         ��Name$ filterCheckBox��5T      $��Center=�6N    d� 	IsChecked 6/   �� *
IsFilter+5U      &   Xceed.Wpf.Toolkit.DropDownButton  5V      -   5          Name$ dropDownButton��5V   "   
������ $��滤波��6S   $��16(�6`   $��Center=�6n      DropDownContent 5W      � 5X      $	��320��6    5Y      �� 5Z      $
��auto �6,   �� 5[      $��* �6,   .+�5^      �� $ 
Horizontal=�6%   .+�5`      	� -   5         	�Name$ 
bandPassRb��5`   *     RadioButtonStyle#��[ 6>   
���   �$��带通��6{   $��Center=�6%   6�   ��  �� Source# [ $ Data.Signal.FilterType�# [   �� ConverterParameter$  BandPass��6%  	� 5a      -   5        $ 
highPassRb��5a   *   #��[ 6>   
���   �$��高通��6{   $��Center=�6%   6�   �� # [ $ Data.Signal.FilterType�# [ $  HighPass��6%  	� 5b      -	   5        $ 	lowPassRb��5b   *   #��[ 6=   
���   �$��低通��6z   $��Center=�6#   6�   �� # [ $ Data.Signal.FilterType�# [ $  LowPass��6#  5c      � 5d      $��1��6   $��5,5,25,0q�6,   y�6>   �� # [ $ Data.Signal.FilterType�# [   AIC.CoreType.FilterType!   BandPass#  Z! " �� FallbackValue$" 	Collapsed�� 5e      �� 5f   "   $
��auto �60   �� 5g   "   $
��auto �60   �� 5h   "   $
��auto �60   �� 5i   "   $
��auto �60   �� 5j   "   $
��auto �60   �� 5k   "   $
��auto �60   �� 5l   "   $
��auto �60   # � ColumnDefinitions# 5n      �� 5o   "   $
��auto �63   �� 5p   "   $��* �63   .+�5r      �� $��通带衰减��6$   $  Xceed.Wpf.Toolkit.DoubleUpDown  5s      $��1��60   �	  �Xceed.Wpf.Toolkit.Primitives.UpDownBase`1[[System.Nullable`1[[System.Double, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]$ 	  Minimum$
$ 0.01X�6@   % 	  Maximum$% 3X�6O   �
  �Xceed.Wpf.Toolkit.NumericUpDown`1[[System.Nullable`1[[System.Double, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]& 
  	Increment$	& 0.1X�6[   ' 	  Value' 6k   �� # [ $0 *Data.Signal.BPFilter.PassbandAttenuationDB��� 5t      $��1��6$   $��阻带衰减��61     5u      $��1��60   $��1��6=   $$ 40X�6M   $	% 120X�6Z   ' 6h   �� # [ $0 *Data.Signal.BPFilter.StopbandAttenuationDB��� 5v       	 CheckBoxStyle#��[	 6'   
���   �$��2��6�   $��绑定转速��6�   $��2��6�    6N   �� # [ $' !Data.Signal.BPFilter.IsRPMBinding�.  (System.Windows.Interactivity.Interaction(   Triggers( 5w   "   /  )System.Windows.Interactivity.EventTrigger  5x   &   )   	EventName$) Checked��65   .  (System.Windows.Interactivity.TriggerBase*   Actions.* 5y   *   6  0System.Windows.Interactivity.InvokeCommandAction  +   Command+ 6@   �� # [ $) #Data.Signal.BPFilter.BindRPMCommand�,   CommandParameter, 6�   �� # [ $ Data.Signal.RPM�5z   '     5{   &   $) 	Unchecked��65   .* 5|   *     + 6@   �� # [ $+ %Data.Signal.BPFilter.UnBindRPMCommand�, 6�   �� # [ $ Data.Signal.RPM�5}   '   5      �� 5�      $��3��6$   $��低逼近带通��61     5�      $��3��60   $��1��6=   $$ 0X�6M   ' 6Y   �� # [ $+ %Data.Signal.BPFilter.BPPassBandFreLow��� 5�      $��4��6$   $��高逼近带通��61     5�      $��4��60   $��1��6=   $$ 0X�6M   ' 6Y   �� # [ $, &Data.Signal.BPFilter.BPPassBandFreHigh��� 5�      $��5��6$   $��过渡带宽��61     5�      $��5��61   $��1��6>   $$ 0X�6N   ' 6Z   �� # [ $. (Data.Signal.BPFilter.TransitionBandwidth�A� 5�      $��6��6*   $��2��67   .+�5�   "   �� $��保存��6)   $��5q�66   ��6A   �� # [ $&  Data.Signal.BPFilter.SaveCommand��� 5�   "   $��重置��6)   $��5q�66   ��6A   �� # [ $' !Data.Signal.BPFilter.ResetCommand�5�      5�      � 5�      $��1��6   $��5,5,25,0q�6,   y�6>   �� # [ $ Data.Signal.FilterType�# [ -   HighPass#  Z- $" 	Collapsed�� 5�      �� 5�   "   $
��auto �60   �� 5�   "   $
��auto �60   �� 5�   "   $
��auto �60   �� 5�   "   $
��auto �60   �� 5�   "   $
��auto �60   �� 5�   "   $
��auto �60   # 5�      �� 5�   "   $
��auto �63   �� 5�   "   $��* �63   .+�5�      �� $��通带衰减��6$     5�      $��1��60   $
$ 0.01X�6@   $% 3X�6O   $	& 0.1X�6[   ' 6k   �� # [ $0 *Data.Signal.HPFilter.PassbandAttenuationDB��� 5�      $��1��6$   $��阻带衰减��61     5�      $��1��60   $��1��6=   $$ 40X�6M   $	% 120X�6Z   ' 6h   �� # [ $0 *Data.Signal.HPFilter.StopbandAttenuationDB��� 5�      #��[	 6'   
���   �$��2��6�   $��绑定转速��6�   $��2��6�    6N   �� # [ $' !Data.Signal.HPFilter.IsRPMBinding�. �� Mode$. TwoWay=�( 5�   "     5�   &   $) Checked��65   .* 5�   *     + 6@   �� # [ $) #Data.Signal.HPFilter.BindRPMCommand�, 6�   �� # [ $ Data.Signal.RPM�5�   '     5�   &   $) 	Unchecked��65   .* 5�   *     + 6@   �� # [ $+ %Data.Signal.HPFilter.UnBindRPMCommand�, 6�   �� # [ $ Data.Signal.RPM�5�   '   5�      �� 5�      $��3��6$   $��通带��61     5�      $��3��60   $��1��6=   $$ 0X�6M   ' 6Y   �� # [ $&  Data.Signal.HPFilter.PassbandFre��� 5�      $��4��6$   $��阻带��61     5�      $��4��60   $��1��6=   $$ 0X�6M   ' 6Y   �� # [ $&  Data.Signal.HPFilter.StopbandFre�A� 5�      $��6��6*   $��2��67   .+�5�   "   �� $��保存��6)   $��5q�66   ��6A   �� # [ $&  Data.Signal.HPFilter.SaveCommand��� 5�   "   $��重置��6)   $��5q�66   ��6A   �� # [ $' !Data.Signal.HPFilter.ResetCommand�5�      5�      � 5�      $��1��6   $��5,5,25,0q�6,   y�6>   �� # [ $ Data.Signal.FilterType�# [ /   LowPass#  Z/ $" 	Collapsed�� 5�      �� 5�   "   $
��auto �60   �� 5�   "   $
��auto �60   �� 5�   "   $
��auto �60   �� 5�   "   $
��auto �60   �� 5�   "   $
��auto �60   �� 5�   "   $
��auto �60   # 5�      �� 5�   "   $
��auto �63   �� 5�   "   $��* �63   .+�5�      �� $��通带衰减��6$     5�      $��1��60   $
$ 0.01X�6@   $% 3X�6O   $	& 0.1X�6[   ' 6k   �� # [ $0 *Data.Signal.LPFilter.PassbandAttenuationDB��� 5�      $��1��6$   $��阻带衰减��61     5�      $��1��60   $��1��6=   $$ 40X�6M   $	% 120X�6Z   ' 6h   �� # [ $0 *Data.Signal.LPFilter.StopbandAttenuationDB��� 5�      #��[	 6'   
���   �$��2��6�   $��绑定转速��6�   $��2��6�    6N   �� # [ $' !Data.Signal.LPFilter.IsRPMBinding�$. TwoWay=�( 5�   "     5�   &   $) Checked��65   .* 5�   *     + 6@   �� # [ $) #Data.Signal.LPFilter.BindRPMCommand�, 6�   �� # [ $ Data.Signal.RPM�5�   '     5�   &   $) 	Unchecked��65   .* 5�   *     + 6@   �� # [ $+ %Data.Signal.LPFilter.UnBindRPMCommand�, 6�   �� # [ $ Data.Signal.RPM�5�   '   5�      �� 5�      $��3��6$   $��通带��61     5�      $��3��60   $��1��6=   $$ 0X�6M   ' 6Y   �� # [ $&  Data.Signal.LPFilter.PassbandFre��� 5�      $��4��6$   $��阻带��61     5�      $��4��60   $��1��6=   $$ 0X�6M   ' 6Y   �� # [ $&  Data.Signal.LPFilter.StopbandFre�A� 5�      $��6��6*   $��2��67   .+�5�   "   �� $��保存��6)   $��5q�66   ��6A   �� # [ $&  Data.Signal.LPFilter.SaveCommand��� 5�   "   $��重置��6)   $��5q�66   ��6A   �� # [ $' !Data.Signal.LPFilter.ResetCommand�5�      5�      5�      5�      �� 5�      0 �� 
IsEditable0 .  #��[ 6*     AIC.Core.EnumHelper1   Enum(  "AIC.CoreType.SignalPreProccessType"1  6l   $��80��6�   $��Center=�6�   
������ $��5,0,5,0q�6  2 �� SelectedItem2 6�   �� *SignalPreProccessType+��5�      �� 5�       
 VirtualisedMetroComboBox3 �� BasedOn#3 [
 6   " ��6P   .�5�      ��  � ��$	 0q�6<   �� 5�       � ��$	 0q�64   5�      5�      �� 5�      $��dB/Hz��6   $��Center=�6V   $��0,0,5,0q�6q    6'   �� *Signal.IsPowerSpectrumDB+6q   �� 5�      -
   5        $ fitViewCheckBox��5�      $��Center=�60   $
 TrueY�6K   �� 5�      -   5        $��Right=�5�      #��[ 61   $��	功率谱��6V   $��0,0,5,0q�6�   4 � ToolTip4 5�      �� 5�      $��截图��6    5�      5�      5�      5�      