   M S B A M L   `   `   `  ����Q  MAIC.Cloud.DataReplayer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullK*clr-namespace:AIC.Cloud.DataReplayer.ViewsAIC.Cloud.DataReplayer.Views  5      G CAIC.CoreType, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullA0clr-namespace:AIC.CoreType;assembly=AIC.CoreTypeAIC.CoreType 5
      G CAICMathTools, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullA0clr-namespace:AICMathTools;assembly=AICMathToolsAICMathTools 5      :    4AIC.Cloud.DataReplayer.Views.FrequencyDomainDataView   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      c _System.Windows.Interactivity, Version=4.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity  5      6views*clr-namespace:AIC.Cloud.DataReplayer.Views  5      Q	 MAIC.Cloud.Presentation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9presentation&http://www.aicmonitor.com/Presentation 	 5      X
 TXceed.Wpf.Toolkit, Version=2.5.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit 
 5	      ?coreType0clr-namespace:AIC.CoreType;assembly=AIC.CoreType 
 5
      l hArction.WPF.LightningChartUltimate, Version=6.5.6.4001, Culture=neutral, PublicKeyToken=6484d7bb14b95dd3)lcuhttp://www.arction.com/schemas/  5      CaicMathTools0clr-namespace:AICMathTools;assembly=AICMathTools  5        � 	Resources  5         ButtonStyle �� 
TargetType �� Property �� Value �� 
TargetType J� SnapsToDevicePixels �� Triggers P� Property P� Value  RepeatButtonStyle   booleanToVisibilityConverter< 	 6AIC.Cloud.Presentation.Converters.EnumBooleanConverter  enumBooleanConverterA 	 ;AIC.Cloud.Presentation.Converters.EnumToVisibilityConverter  enumToVisibilityConverter	   Reverse) 	 #AIC.Cloud.Presentation.BindingProxy 	 proxy
   Data  DefaultErrorTemplate �� Orientation �� CornerRadius �Name K� ZIndex � Points J� IsHitTestVisible � Points �� StrokeEndLineCap �� StrokeStartLineCap ��Name ^� InitialShowDelay � ToolTip �� ElementName �� Path$ 
 Xceed.Wpf.Toolkit.DoubleUpDown  NormalRadioButtonStyle  NormalCheckBoxStyle��%
  &	        &	 �    &	 t    &	 �    &	 �    &	 �    &	 �    '  	    7  &	 n	    &	 �	    �� 5   
   " ��6$   .�5      ��  � ��
 ���� 6+   �� 5       � �� �	#FF87CDEE6+   �� 5       � ��$ Center=�6;   �� 5       � ��$ Center=�69   �� 5       � ��$ 1q�6(   �� 5       � �� 5      �� 5      " ��6&   .R�5      � #��z��6   .+�5      �� #��z��6/   #��z��6r   ��. # z 6�   #��z��6�   5       5      P� 5       �@J�IsMouseOver . . �5   "   ��  � �� �	#7F2438596?   5       P� 5!       � {� .  . �5"   "   ��  � �� �#ADADAD6?   5#      P� 5$       � �� . . �5%   "   ��  � �� �	#FF20437E6?   5&      5(      5*      5+      �� 5,   
   " ��6*   .�5-      ��  � ��
 ���� 6+   �� 5.       � �� �	#FF87CDEE6+   �� 5/       � ��$ Center=�6;   �� 50       � ��$ Center=�69   �� 51       � ��$ 1q�6(   �� 52       � �� 53      �� 54      " ��6&   .R�55      � #��z��6   .+�56      �� #��z��6/   #��z��6r   ��. # z 6�   #��z��6�   57       58      P� 59       �@J�IsMouseOver . . �5:   "   ��  � �� �	#7F2438596?   5;      P� 5<       � {� .  . �5=   "   ��  � �� �#ADADAD6?   5>      P� 5?       � �� . . �5@   "   ��  � �� �	#FF20437E6?   5A      5C      5E      5F      �� 5G   
   6'     5H   
   6,     5I   
   	 .  6S     5J   
   
 62   �� �� 5L   
   .R�5M      �� $ 
Horizontal=�6   .+�5N      �� $��1q�6   ���	#FFdc000c$	 0.7��6E   $	��Top=�6X   ._�5O      � .+�5P      � $ toolTipCorner��6"   $ 2��69   $��-1q�6I    �   ��[ ��[ ��[ ���	#FFdc000c$��Right=�5Q   .   $	��Top=�6J    . 6b   � 5R      $ 3��6#    �   ��j ��j $��-1q�6D   $��Right=�6P   $	��Top=�6l    . $	��1.5��5S   /   $ Round=�6E   $ Round=�6^   
�������6y   �� 5T      $ placeholder��64   5U      5V      �� 5W      
���  ��$��3q�6*   $ 10��65   $��20��6G   $��20��6R   $	��Top=�6^   $ 0��5Y   X    5X      �� $ placeholder��$8 2AdornedElement.(Validation.Errors)[0].ErrorContent�._�5Z      �� $��!��6    
�������$��Center=�6<   $��Center=�6Y   5[      5\      5]      �� 5^   
   "  6   .�5_      ��  �@=�ErrorTemplate8   69   5`      �� 5a   
   " 	�6/   �� 5b   
   " ��6,   5c      .��5d      � .+�5e   
   � $��2q�6   ���	#7F243859 � RowDefinitions 5f      �� 5g      $��25 �6    �� 5h      $��* �6    .+�5j      �� ���	#FF87CDEE$��Center=�6G   $
��Left=�6b   $��5,0,0,0q�6}   ��6   �� *Title+6}   �� 5k      $��Right=�6   $ 
Horizontal=�65   .+�5l      �� -   5         ��Name$ filterCheckBox��5l      $
��Left=�63   $��Center=�6N   $��5,0,0,0q�6i   6�   & 
  Xceed.Wpf.Toolkit.DropDownButton  5m      -   5          Name$ dropDownButton��5m   &   $
��Left=�6>   $	��Top=�6Y   $��滤波��6q      DropDownContent 5n      � 5o      $	��200��6    5p      �� 5q   "   $
��auto �60   �� 5r   "   $��* �60   .+�5u      �� $ 
Horizontal=�6)   .+�5v   "   	� -   5         	�Name$ 
bandPassRb��5v   .   #��[ 6B   
���   �$��带通��6�    d� 	IsChecked 6�   ��  �� Source# [ $ Data.FilterType�  �� 	Converter#  [ ! �� ConverterParameter$! BandPass��	� 5w   "   -   5        $ 
highPassRb��5w   .   #��[ 6B   
���   �$��高通��6�    6�   �� # [ $ Data.FilterType�#  [ $! HighPass��	� 5x   "   -   5        $ 	lowPassRb��5x   .   #��[ 6A   
���   �$��低通��6�    6�   �� # [ $ Data.FilterType�#  [ $! LowPass��5y      � 5z      $��1��6#   $��5,5,25,0q�60   y�6B   �� # [ $ Data.FilterType�#  [   AICMathTools.FilterType"   BandPass#! Z" # �� FallbackValue$# 	Collapsed�� 5{   "   �� 5|   &   $
��auto �64   �� 5}   &   $
��auto �64   �� 5~   &   $
��auto �64   �� 5   &   $
��auto �64   �� 5�   &   $
��auto �64   �� 5�   &   $
��auto �64   �� 5�   &   $
��auto �64   $ � ColumnDefinitions$ 5�   "   �� 5�   &   $
��auto �67   �� 5�   &   $��* �67   .+�5�   "   �� $��通带衰减��6(     5�   "   $��1��64   � 
 �Xceed.Wpf.Toolkit.Primitives.UpDownBase`1[[System.Nullable`1[[System.Double, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]%   Minimum$
% 0.01X�6D   &   Maximum$& 3X�6S   � 
 �Xceed.Wpf.Toolkit.NumericUpDown`1[[System.Nullable`1[[System.Double, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]'   	Increment$	' 0.1X�6_   (   Value( 6o   �� # [ $) #Data.BPFilter.PassbandAttenuationDB��� 5�   "   $��1��6(   $��阻带衰减��65     5�   "   $��1��64   $��1��6A   $% 40X�6Q   $	& 120X�6^   ( 6l   �� # [ $) #Data.BPFilter.StopbandAttenuationDB��� 5�   "   #��[ 6+   
���   �$��2��6k   $��绑定转速��6x   $��2��6�   .	  (System.Windows.Interactivity.Interaction) 	  Triggers) 5�   &   /
  )System.Windows.Interactivity.EventTrigger
  5�   *   * 
  	EventName$* Checked��69   .  (System.Windows.Interactivity.TriggerBase+   Actions.+ 5�   .   6  0System.Windows.Interactivity.InvokeCommandAction  ,   Command, 6D   �� # [ $" Data.BPFilter.BindRPMCommand�-   CommandParameter- 6�   �� # [ $ Data.RPM�5�   +   
  5�   *   $* 	Unchecked��69   .+ 5�   .     , 6D   �� # [ $$ Data.BPFilter.UnBindRPMCommand�- 6�   �� # [ $ Data.RPM�5�   +   5�   #   �� 5�   "   $��3��6(   $��低逼近带通��65     5�   "   $��3��64   $��1��6A   $% 0X�6Q   ( 6]   �� # [ $$ Data.BPFilter.BPPassBandFreLow��� 5�   "   $��4��6(   $��高逼近带通��65     5�   "   $��4��64   $��1��6A   $% 0X�6Q   ( 6]   �� # [ $% Data.BPFilter.BPPassBandFreHigh��� 5�   "   $��5��6(   $��过渡带宽��65     5�   "   $��5��65   $��1��6B   $% 0X�6R   ( 6^   �� # [ $' !Data.BPFilter.TransitionBandwidth�A� 5�   "   $��6��6.   $��2��6;   .+�5�   &   �� $��保存��6-   $��20��6:   $��5q�6F   ��6Q   �� # [ $ Data.BPFilter.SaveCommand��� 5�   &   $��重置��6-   $��20��6:   $��5q�6F   ��6Q   �� # [ $  Data.BPFilter.ResetCommand�5�   #   5�      � 5�      $��1��6#   $��5,5,25,0q�60   y�6B   �� # [ $ Data.FilterType�#  [ .   HighPass#! Z. $# 	Collapsed�� 5�   "   �� 5�   &   $
��auto �64   �� 5�   &   $
��auto �64   �� 5�   &   $
��auto �64   �� 5�   &   $
��auto �64   �� 5�   &   $
��auto �64   �� 5�   &   $
��auto �64   $ 5�   "   �� 5�   &   $
��auto �67   �� 5�   &   $��* �67   .+�5�   "   �� $��通带衰减��6(     5�   "   $��1��64   $
% 0.01X�6D   $& 3X�6S   $	' 0.1X�6_   ( 6o   �� # [ $) #Data.HPFilter.PassbandAttenuationDB��� 5�   "   $��1��6(   $��阻带衰减��65     5�   "   $��1��64   $��1��6A   $% 40X�6Q   $	& 120X�6^   ( 6l   �� # [ $) #Data.HPFilter.StopbandAttenuationDB��� 5�   "   #��[ 6+   
���   �$��2��6k   $��绑定转速��6x   $��2��6�   ) 5�   &   
  5�   *   $* Checked��69   .+ 5�   .     , 6D   �� # [ $" Data.HPFilter.BindRPMCommand�- 6�   �� # [ $ Data.RPM�5�   +   
  5�   *   $* 	Unchecked��69   .+ 5�   .     , 6D   �� # [ $$ Data.HPFilter.UnBindRPMCommand�- 6�   �� # [ $ Data.RPM�5�   +   5�   #   �� 5�   "   $��3��6(   $��通带��65     5�   "   $��3��64   $��1��6A   $% 0X�6Q   ( 6]   �� # [ $ Data.HPFilter.PassbandFre��� 5�   "   $��4��6(   $��阻带��65     5�   "   $��4��64   $��1��6A   $% 0X�6Q   ( 6]   �� # [ $ Data.HPFilter.StopbandFre�A� 5�   "   $��6��6.   $��2��6;   .+�5�   &   �� $��保存��6-   $��20��6:   $��5q�6F   ��6Q   �� # [ $ Data.HPFilter.SaveCommand��� 5�   &   $��重置��6-   $��20��6:   $��5q�6F   ��6Q   �� # [ $  Data.HPFilter.ResetCommand�5�   #   5�      � 5�      $��1��6#   $��5,5,25,0q�60   y�6B   �� # [ $ Data.FilterType�#  [ /   LowPass#! Z/ $# 	Collapsed�� 5�   "   �� 5�   &   $
��auto �64   �� 5�   &   $
��auto �64   �� 5�   &   $
��auto �64   �� 5�   &   $
��auto �64   �� 5�   &   $
��auto �64   �� 5�   &   $
��auto �64   $ 5�   "   �� 5�   &   $
��auto �67   �� 5�   &   $��* �67   .+�5�   "   �� $��通带衰减��6(     5�   "   $��1��64   $
% 0.01X�6D   $& 3X�6S   $	' 0.1X�6_   ( 6o   �� # [ $) #Data.LPFilter.PassbandAttenuationDB��� 5�   "   $��1��6(   $��阻带衰减��65     5�   "   $��1��64   $��1��6A   $% 40X�6Q   $	& 120X�6^   ( 6l   �� # [ $) #Data.LPFilter.StopbandAttenuationDB��� 5�   "   #��[ 6+   
���   �$��2��6k   $��绑定转速��6x   $��2��6�   ) 5�   &   
  5�   *   $* Checked��69   .+ 5�   .     , 6D   �� # [ $" Data.LPFilter.BindRPMCommand�- 6�   �� # [ $ Data.RPM�5�   +   
  5�   *   $* 	Unchecked��69   .+ 5�   .     , 6D   �� # [ $$ Data.LPFilter.UnBindRPMCommand�- 6�   �� # [ $ Data.RPM�5�   +   5�   #   �� 5�   "   $��3��6(   $��通带��65     5�   "   $��3��64   $��1��6A   $% 0X�6Q   ( 6]   �� # [ $ Data.LPFilter.PassbandFre��� 5�   "   $��4��6(   $��阻带��65     5�   "   $��4��64   $��1��6A   $% 0X�6Q   ( 6]   �� # [ $ Data.LPFilter.StopbandFre�A� 5�   "   $��6��6.   $��2��6;   .+�5�   &   �� $��保存��6-   $��20��6:   $��5q�6F   ��6Q   �� # [ $ Data.LPFilter.SaveCommand��� 5�   &   $��重置��6-   $��20��6:   $��5q�6F   ��6Q   �� # [ $  Data.LPFilter.ResetCommand�5�   #   5�      5�      5�      �� 5�      -   5        $ envelopeCheckBox��5�      $��包络��65   ���	#FF87CDEE$��Center=�6Y   6�   �� 5�      -   5        $ tffCheckBox��5�      $	��TFF��64   ���	#FF87CDEE$��Center=�6Y   6�   �� 5�      -   5        $ cepstrumCheckBox��5�      $��	倒频谱��69   ���	#FF87CDEE$��Center=�6^   6�   �� 5�      -	   5        $��Marlett)�5�   #   $��20(�68   $��3��6F   $	��2,0q�6R   #��[ 6z   �� 5�      -
   5        $��Marlett)�5�   #   $��20(�68   $��4��6F   #��[ 6j   �� 5�      $��参数��6   $��Right=�6,   ���	#FF87CDEE$��Center=�6�   $��2,0,5,0q�6�    6H   �� *
ShowDetail+6�   5�      � 5�      -   5        $��	gridChart��5�      $��1��6$   5�      �� 5�      -   5        $��1��5�      #��[  6"   $��25��6G   $��25��6R   $��截图��6_   $��0,0,5,0q�6l   $��Right=�6}   $	��Top=�6�    5�      �� 5�      $��截图��6   5�      5�      F� 5�   
   "����k"��pѰ��pѰ��H	$��2��6T   $
��Left=�6e   $	��Top=�6�   $��15��6�   $��15��6�   0 �� Stretch$
0 Fill=�6�   ���	#FF87CDEE$��3��6�   F� 5�   
   "�������kч	��kч	�e�	$��2��6L   $��Right=�6]   $	��Top=�6y   $��15��6�   $��15��6�   $
0 Fill=�6�   ���	#FF87CDEE$��3��6�   F� 5   
   '���P�Ed7�@�@��E�� ����� 	$��2��6M   $
��Left=�6^   $��Bottom=�6y   $��15��6�   $��15��6�   $
0 Fill=�6�   ���	#FF87CDEE$��3��6�   F� 5  
   "����h�흤 �h���.р�7��.	$��2��6G   $��Right=�6X   $��Bottom=�6t   $��15��6�   $��15��6�   $
0 Fill=�6�   ���	#FF87CDEE$��3��6�   5     5     