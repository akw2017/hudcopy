   M S B A M L   `   `   `  ����N  JAIC.HistoryDataPage, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullE'clr-namespace:AIC.HistoryDataPage.ViewsAIC.HistoryDataPage.Views  5      C ?AIC.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5(clr-namespace:AIC.Core;assembly=AIC.CoreAIC.Core 5	      G CAIC.CoreType, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullA0clr-namespace:AIC.CoreType;assembly=AIC.CoreTypeAIC.CoreType 5
      <    6AIC.HistoryDataPage.Views.PowerSpectrumDensityDataView   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      c _System.Windows.Interactivity, Version=4.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity  5      3views'clr-namespace:AIC.HistoryDataPage.Views  5      X TXceed.Wpf.Toolkit, Version=3.2.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit  5      3core(clr-namespace:AIC.Core;assembly=AIC.Core  5	      ?coreType0clr-namespace:AIC.CoreType;assembly=AIC.CoreType  5
      )lcuhttp://www.arction.com/schemas/  5        � 	Resources  5         ButtonStyle �� 
TargetType �� Property �� Value �� 
TargetType �� CornerRadius J� SnapsToDevicePixels �� Triggers P� Property	 P� Value   booleanToVisibilityConverter#  AIC.Core.EnumBooleanConverter  enumBooleanConverter(  "AIC.Core.EnumToVisibilityConverter  enumToVisibilityConverter
   Reverse  AIC.Core.BindingProxy 	 proxy   Data  DefaultErrorTemplate �� Orientation �Name K� ZIndex � Points J� IsHitTestVisible � Points �� StrokeEndLineCap �� StrokeStartLineCap ��Name ^� InitialShowDelay � ToolTip �� ElementName �� Path$  Xceed.Wpf.Toolkit.DoubleUpDown  NormalRadioButtonStyle  NormalCheckBoxStyle��%]  &	        &	 �    &	 �    &	 �    &	     &	 1    '  }    7  &	 �    &	 �    �� 5   
   " ��6$   .�5      ��  � ��
 ���� 6+   �� 5       � �� �	#FF87CDEE6+   �� 5       � ��$ Center=�6;   �� 5       � ��$ Center=�69   �� 5       � ��$ 1q�6(   �� 5       � �� 5      �� 5      " ��6&   .R�5      � .+�5      �� #��z��6%   $ 7��6O   �� 5      #��z��6/   #��z��6r   ��. # z 6�   #��z��6�   5       5      P� 5       �@J�IsMouseOver	 . . �5   "   ��  � �� �	#7F2438596?   5       P� 5!       � {�	 .  . �5"   "   ��  � �� �#ADADAD6?   5#      P� 5$       � ��	 . . �5%   "   ��  � �� �	#FF20437E6?   5&      5(      5*      5+      �� 5,   
   6'     5-   
   6$     5.   
   
 .  6K     5/   
    6*   �� �� 51   
   .R�52      �� $ 
Horizontal=�6   .+�53      �� $��1q�6   ���	#FFdc000c$	 0.7��6E   $	��Top=�6X   ._�54      � .+�55      � $ toolTipCorner��6"   $ 2��69   $��-1q�6I    �   ��[ ��[ ��[ ���	#FFdc000c$��Right=�56   .   $	��Top=�6J    . 6b   � 57      $ 3��6#    �   ��j ��j $��-1q�6D   $��Right=�6P   $	��Top=�6l    . $	��1.5��58   /   $ Round=�6E   $ Round=�6^   
�������6y   �� 59      $ placeholder��64   5:      5;      �� 5<      
���  ��$��3q�6*   $ 10��65   $��20��6G   $��20��6R   $	��Top=�6^   $ 0��5>   X    5=      �� $ placeholder��$8 2AdornedElement.(Validation.Errors)[0].ErrorContent�._�5?      �� $��!��6    
�������$��Center=�6<   $��Center=�6Y   5@      5A      5B      �� 5C   
   "  6   .�5D      ��  �@=�ErrorTemplate8   69   5E      �� 5F   
   " 	�6/   �� 5G   
   " ��6,   5H      .��5I      � .+�5J   
   � $��2q�6    � RowDefinitions 5K      �� 5L      $
��Auto �6   �� 5M      $��* �6   .+�5O      �� $��Right=�6   $ 
Horizontal=�65   $	��Top=�6N   .+�5P      �� -   5         ��Name$ filterCheckBox��5P      $
��Left=�63   $��Center=�6N   $��5,0,0,0q�6i   6�   &   Xceed.Wpf.Toolkit.DropDownButton  5Q      -   5          Name$ dropDownButton��5Q   &   $
��Left=�6>   $��Center=�6Y   
������ ���	#FF87CDEE$��滤波��6�      DropDownContent 5R      � 5S      $	��200��6    5T      �� 5U   "   $
��auto �60   �� 5V   "   $��* �60   .+�5X      �� $ 
Horizontal=�6)   .+�5Y   "   	� -   5         	�Name$ 
bandPassRb��5Y   .     RadioButtonStyle#��[ 6B   
���   �$��带通��6    d� 	IsChecked 6�   ��  �� Source# [ $ Data.FilterType�  �� 	Converter#  [ ! �� ConverterParameter$! BandPass��	� 5Z   "   -   5        $ 
highPassRb��5Z   .   #��[ 6B   
���   �$��高通��6    6�   �� # [ $ Data.FilterType�#  [ $! HighPass��	� 5[   "   -   5        $ 	lowPassRb��5[   .   #��[ 6A   
���   �$��低通��6~    6�   �� # [ $ Data.FilterType�#  [ $! LowPass��5\      � 5]      $��1��6#   $��5,5,25,0q�60   y�6B   �� # [ $ Data.FilterType�#  [   AIC.CoreType.FilterType"   BandPass#! Z" # �� FallbackValue$# 	Collapsed�� 5^   "   �� 5_   &   $
��auto �64   �� 5`   &   $
��auto �64   �� 5a   &   $
��auto �64   �� 5b   &   $
��auto �64   �� 5c   &   $
��auto �64   �� 5d   &   $
��auto �64   �� 5e   &   $
��auto �64   $ � ColumnDefinitions$ 5g   "   �� 5h   &   $
��auto �67   �� 5i   &   $��* �67   .+�5k   "   �� $��通带衰减��6(     5l   "   $��1��64   �  �Xceed.Wpf.Toolkit.Primitives.UpDownBase`1[[System.Nullable`1[[System.Double, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]%   Minimum$
% 0.01X�6D   &   Maximum$& 3X�6S   �  �Xceed.Wpf.Toolkit.NumericUpDown`1[[System.Nullable`1[[System.Double, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]'   	Increment$	' 0.1X�6_   (   Value( 6o   �� # [ $) #Data.BPFilter.PassbandAttenuationDB��� 5m   "   $��1��6(   $��阻带衰减��65     5n   "   $��1��64   $��1��6A   $% 40X�6Q   $	& 120X�6^   ( 6l   �� # [ $) #Data.BPFilter.StopbandAttenuationDB��� 5o   "    	 CheckBoxStyle#��[	 6+   
���   �$��2��6e   $��绑定转速��6r   $��2��6�   .	  (System.Windows.Interactivity.Interaction) 	  Triggers) 5p   &   /
  )System.Windows.Interactivity.EventTrigger
  5q   *   * 
  	EventName$* Checked��69   .  (System.Windows.Interactivity.TriggerBase+   Actions.+ 5r   .   6  0System.Windows.Interactivity.InvokeCommandAction  ,   Command, 6D   �� # [ $" Data.BPFilter.BindRPMCommand�-   CommandParameter- 6�   �� # [ $ Data.RPM�5s   +   
  5t   *   $* 	Unchecked��69   .+ 5u   .     , 6D   �� # [ $$ Data.BPFilter.UnBindRPMCommand�- 6�   �� # [ $ Data.RPM�5v   +   5x   #   �� 5y   "   $��3��6(   $��低逼近带通��65     5z   "   $��3��64   $��1��6A   $% 0X�6Q   ( 6]   �� # [ $$ Data.BPFilter.BPPassBandFreLow��� 5{   "   $��4��6(   $��高逼近带通��65     5|   "   $��4��64   $��1��6A   $% 0X�6Q   ( 6]   �� # [ $% Data.BPFilter.BPPassBandFreHigh��� 5}   "   $��5��6(   $��过渡带宽��65     5~   "   $��5��65   $��1��6B   $% 0X�6R   ( 6^   �� # [ $' !Data.BPFilter.TransitionBandwidth�A� 5   "   $��6��6.   $��2��6;   .+�5�   &   �� $��保存��6-   $��5q�6:   ��6E   �� # [ $ Data.BPFilter.SaveCommand��� 5�   &   $��重置��6-   $��5q�6:   ��6E   �� # [ $  Data.BPFilter.ResetCommand�5�   #   5�      � 5�      $��1��6#   $��5,5,25,0q�60   y�6B   �� # [ $ Data.FilterType�#  [ .   HighPass#! Z. $# 	Collapsed�� 5�   "   �� 5�   &   $
��auto �64   �� 5�   &   $
��auto �64   �� 5�   &   $
��auto �64   �� 5�   &   $
��auto �64   �� 5�   &   $
��auto �64   �� 5�   &   $
��auto �64   $ 5�   "   �� 5�   &   $
��auto �67   �� 5�   &   $��* �67   .+�5�   "   �� $��通带衰减��6(     5�   "   $��1��64   $
% 0.01X�6D   $& 3X�6S   $	' 0.1X�6_   ( 6o   �� # [ $) #Data.HPFilter.PassbandAttenuationDB��� 5�   "   $��1��6(   $��阻带衰减��65     5�   "   $��1��64   $��1��6A   $% 40X�6Q   $	& 120X�6^   ( 6l   �� # [ $) #Data.HPFilter.StopbandAttenuationDB��� 5�   "   #��[	 6+   
���   �$��2��6e   $��绑定转速��6r   $��2��6�   ) 5�   &   
  5�   *   $* Checked��69   .+ 5�   .     , 6D   �� # [ $" Data.HPFilter.BindRPMCommand�- 6�   �� # [ $ Data.RPM�5�   +   
  5�   *   $* 	Unchecked��69   .+ 5�   .     , 6D   �� # [ $$ Data.HPFilter.UnBindRPMCommand�- 6�   �� # [ $ Data.RPM�5�   +   5�   #   �� 5�   "   $��3��6(   $��通带��65     5�   "   $��3��64   $��1��6A   $% 0X�6Q   ( 6]   �� # [ $ Data.HPFilter.PassbandFre��� 5�   "   $��4��6(   $��阻带��65     5�   "   $��4��64   $��1��6A   $% 0X�6Q   ( 6]   �� # [ $ Data.HPFilter.StopbandFre�A� 5�   "   $��6��6.   $��2��6;   .+�5�   &   �� $��保存��6-   $��5q�6:   ��6E   �� # [ $ Data.HPFilter.SaveCommand��� 5�   &   $��重置��6-   $��5q�6:   ��6E   �� # [ $  Data.HPFilter.ResetCommand�5�   #   5�      � 5�      $��1��6#   $��5,5,25,0q�60   y�6B   �� # [ $ Data.FilterType�#  [ /   LowPass#! Z/ $# 	Collapsed�� 5�   "   �� 5�   &   $
��auto �64   �� 5�   &   $
��auto �64   �� 5�   &   $
��auto �64   �� 5�   &   $
��auto �64   �� 5�   &   $
��auto �64   �� 5�   &   $
��auto �64   $ 5�   "   �� 5�   &   $
��auto �67   �� 5�   &   $��* �67   .+�5�   "   �� $��通带衰减��6(     5�   "   $��1��64   $
% 0.01X�6D   $& 3X�6S   $	' 0.1X�6_   ( 6o   �� # [ $) #Data.LPFilter.PassbandAttenuationDB��� 5�   "   $��1��6(   $��阻带衰减��65     5�   "   $��1��64   $��1��6A   $% 40X�6Q   $	& 120X�6^   ( 6l   �� # [ $) #Data.LPFilter.StopbandAttenuationDB��� 5�   "   #��[	 6+   
���   �$��2��6e   $��绑定转速��6r   $��2��6�   ) 5�   &   
  5�   *   $* Checked��69   .+ 5�   .     , 6D   �� # [ $" Data.LPFilter.BindRPMCommand�- 6�   �� # [ $ Data.RPM�5�   +   
  5�   *   $* 	Unchecked��69   .+ 5�   .     , 6D   �� # [ $$ Data.LPFilter.UnBindRPMCommand�- 6�   �� # [ $ Data.RPM�5�   +   5�   #   �� 5�   "   $��3��6(   $��通带��65     5�   "   $��3��64   $��1��6A   $% 0X�6Q   ( 6]   �� # [ $ Data.LPFilter.PassbandFre��� 5�   "   $��4��6(   $��阻带��65     5�   "   $��4��64   $��1��6A   $% 0X�6Q   ( 6]   �� # [ $ Data.LPFilter.StopbandFre�A� 5�   "   $��6��6.   $��2��6;   .+�5�   &   �� $��保存��6-   $��5q�6:   ��6E   �� # [ $ Data.LPFilter.SaveCommand��� 5�   &   $��重置��6-   $��5q�6:   ��6E   �� # [ $  Data.LPFilter.ResetCommand�5�   #   5�      5�      5�      �� 5�      -   5        $ envelopeCheckBox��5�      $��包络��65   ���	#FF87CDEE$��Center=�6Y   6�   �� 5�      -   5        $ tffCheckBox��5�      $	��TFF��60   ���	#FF87CDEE$��Center=�6U   6�   �� 5�      -   5        $ cepstrumCheckBox��5�      $��	倒频谱��65   ���	#FF87CDEE$��Center=�6Z   6�   �� 5�      -	   5        $ 
dbCheckBox��5�      $��dB/Hz��6/   ���	#FF87CDEE$��Center=�6V   $
 TrueY�6�   �� 5�      -
   5        $ showCheckBox��5�      $��参数��61   $
 TrueY�6>   $��Right=�6O   ���	#FF87CDEE$��Center=�6�   $��0,0,5,0q�6�   5�      � 5�      -   5        $��	gridChart��5�      $��1��6$   5�      �� 5�      ���	#FF87CDEE$��Center=�6G   $
��Left=�6b   $��5,0,0,0q�6}   ��6   �� *Title+6}   �� 5�      -   5        $��1��5�      #��[  6"   $��25��6G   $��截图��6T   $��0,0,5,0q�6a   $��Right=�6r   $	��Top=�6�    5�      �� 5�      $��截图��6    5�      5�      5�      5�      