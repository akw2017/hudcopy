   M S B A M L   `   `   `  ���� Q  MAIC.Cloud.DataReplayer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullK*clr-namespace:AIC.Cloud.DataReplayer.ViewsAIC.Cloud.DataReplayer.Views  G CAIC.CoreType, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullA0clr-namespace:AIC.CoreType;assembly=AIC.CoreTypeAIC.CoreType G CAICMathTools, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullA0clr-namespace:AICMathTools;assembly=AICMathToolsAICMathTools 8    2AIC.Cloud.DataReplayer.Views.PowerSpectrumDataView   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     c _System.Windows.Interactivity, Version=4.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity  6views*clr-namespace:AIC.Cloud.DataReplayer.Views  Q	 MAIC.Cloud.Presentation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9presentation&http://www.aicmonitor.com/Presentation 	 X
 TXceed.Wpf.Toolkit, Version=2.5.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit 
 ?coreType0clr-namespace:AIC.CoreType;assembly=AIC.CoreType 
 CaicMathTools0clr-namespace:AICMathTools;assembly=AICMathTools 
 l hArction.WPF.LightningChartUltimate, Version=6.5.6.4001, Culture=neutral, PublicKeyToken=6484d7bb14b95dd3)lcuhttp://www.arction.com/schemas/    � 	Resources     ButtonStyle �� 
TargetType �� Property �� Value �� 
TargetType �� CornerRadius J� SnapsToDevicePixels �� Triggers P� Property	 P� Value   booleanToVisibilityConverter< 	 6AIC.Cloud.Presentation.Converters.EnumBooleanConverter  enumBooleanConverterA 	 ;AIC.Cloud.Presentation.Converters.EnumToVisibilityConverter  enumToVisibilityConverter
   Reverse) 	 #AIC.Cloud.Presentation.BindingProxy 	 proxy   Data  DefaultErrorTemplate �� Orientation �Name K� ZIndex � Points J� IsHitTestVisible � Points �� StrokeEndLineCap �� StrokeStartLineCap ��Name ^� InitialShowDelay � ToolTip �� ElementName �� Path$ 
 Xceed.Wpf.Toolkit.DoubleUpDown  NormalRadioButtonStyle  NormalCheckBoxStyle��%�  &	        &	 �    &	 �    &	 �    &	 �    &	 �    '  �    7  &	     &	 #    �� " ��.���  � ��
 ���� ��  � �� �	#FF87CDEE��  � ��$ Center=���  � ��$ Center=���  � ��$ 1q���  � �� �� " ��.R�� .+��� #��z��$ 7���� #��z��#��z����. # z #��z�� P�  �@J�IsMouseOver	 . . ���  � �� �	#7F243859P�  � {�	 .  . ���  � �� �#ADADADP�  � ��	 . . ���  � �� �	#FF20437E��     
 .     �� �� .R��� $ 
Horizontal=�.+��� $��1q����	#FFdc000c$	 0.7��$	��Top=�._�� .+�� $ toolTipCorner��$ 2��$��-1q� �   ��[ ��[ ��[ ���	#FFdc000c$��Right=�$	��Top=� . � $ 3�� �   ��j ��j $��-1q�$��Right=�$	��Top=� . $	��1.5��$ Round=�$ Round=�
��������� $ placeholder���� 
���  ��$��3q�$ 10��$��20��$��20��$	��Top=�$ 0�� �� $ placeholder��$8 2AdornedElement.(Validation.Errors)[0].ErrorContent�._��� $��!��
�������$��Center=�$��Center=��� "  .���  �@=�ErrorTemplate8   �� " 	��� " ��.��� .+�� $��2q����	#7F243859 � RowDefinitions �� $��25 ��� $��* �.+��� $��Right=�$ 
Horizontal=�$	��Top=�.+��� -    ��Name$ filterCheckBox��$
��Left=�$��Center=�$��5,0,0,0q�& 
  Xceed.Wpf.Toolkit.DropDownButton  -     Name$ dropDownButton��$
��Left=�$	��Top=�$��滤波��   DropDownContent � $	��200�� �� $
��auto ��� $��* �.+��� $ 
Horizontal=�.+�	� -    	�Name$ 
bandPassRb��#��[ 
���   �$��带通�� d� 	IsChecked ��  �� Source# [ $ Data.FilterType�  �� 	Converter#  [ ! �� ConverterParameter$! BandPass��	� -   $ 
highPassRb��#��[ 
���   �$��高通�� �� # [ $ Data.FilterType�#  [ $! HighPass��	� -   $ 	lowPassRb��#��[ 
���   �$��低通�� �� # [ $ Data.FilterType�#  [ $! LowPass��� $��1��$��5,5,25,0q�y��� # [ $ Data.FilterType�#  [   AICMathTools.FilterType"   BandPass#! Z" # �� FallbackValue$# 	Collapsed�� �� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto �$ � ColumnDefinitions$ �� $
��auto ��� $��* �.+��� $��通带衰减��  $��1��� 
 �Xceed.Wpf.Toolkit.Primitives.UpDownBase`1[[System.Nullable`1[[System.Double, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]%   Minimum$
% 0.01X�&   Maximum$& 3X�� 
 �Xceed.Wpf.Toolkit.NumericUpDown`1[[System.Nullable`1[[System.Double, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]'   	Increment$	' 0.1X�(   Value( �� # [ $) #Data.BPFilter.PassbandAttenuationDB��� $��1��$��阻带衰减��  $��1��$��1��$% 40X�$	& 120X�( �� # [ $) #Data.BPFilter.StopbandAttenuationDB��� #��[ 
���   �$��2��$��绑定转速��$��2��.	  (System.Windows.Interactivity.Interaction) 	  Triggers) /
  )System.Windows.Interactivity.EventTrigger
  * 
  	EventName$* Checked��.  (System.Windows.Interactivity.TriggerBase+   Actions.+ 6  0System.Windows.Interactivity.InvokeCommandAction  ,   Command, �� # [ $" Data.BPFilter.BindRPMCommand�-   CommandParameter- �� # [ $ Data.RPM�
  $* 	Unchecked��.+   , �� # [ $$ Data.BPFilter.UnBindRPMCommand�- �� # [ $ Data.RPM��� $��3��$��低逼近带通��  $��3��$��1��$% 0X�( �� # [ $$ Data.BPFilter.BPPassBandFreLow��� $��4��$��高逼近带通��  $��4��$��1��$% 0X�( �� # [ $% Data.BPFilter.BPPassBandFreHigh��� $��5��$��过渡带宽��  $��5��$��1��$% 0X�( �� # [ $' !Data.BPFilter.TransitionBandwidth�A� $��6��$��2��.+��� $��保存��$��20��$��5q����� # [ $ Data.BPFilter.SaveCommand��� $��重置��$��20��$��5q����� # [ $  Data.BPFilter.ResetCommand�� $��1��$��5,5,25,0q�y��� # [ $ Data.FilterType�#  [ .   HighPass#! Z. $# 	Collapsed�� �� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto �$ �� $
��auto ��� $��* �.+��� $��通带衰减��  $��1��$
% 0.01X�$& 3X�$	' 0.1X�( �� # [ $) #Data.HPFilter.PassbandAttenuationDB��� $��1��$��阻带衰减��  $��1��$��1��$% 40X�$	& 120X�( �� # [ $) #Data.HPFilter.StopbandAttenuationDB��� #��[ 
���   �$��2��$��绑定转速��$��2��) 
  $* Checked��.+   , �� # [ $" Data.HPFilter.BindRPMCommand�- �� # [ $ Data.RPM�
  $* 	Unchecked��.+   , �� # [ $$ Data.HPFilter.UnBindRPMCommand�- �� # [ $ Data.RPM��� $��3��$��通带��  $��3��$��1��$% 0X�( �� # [ $ Data.HPFilter.PassbandFre��� $��4��$��阻带��  $��4��$��1��$% 0X�( �� # [ $ Data.HPFilter.StopbandFre�A� $��6��$��2��.+��� $��保存��$��20��$��5q����� # [ $ Data.HPFilter.SaveCommand��� $��重置��$��20��$��5q����� # [ $  Data.HPFilter.ResetCommand�� $��1��$��5,5,25,0q�y��� # [ $ Data.FilterType�#  [ /   LowPass#! Z/ $# 	Collapsed�� �� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto �$ �� $
��auto ��� $��* �.+��� $��通带衰减��  $��1��$
% 0.01X�$& 3X�$	' 0.1X�( �� # [ $) #Data.LPFilter.PassbandAttenuationDB��� $��1��$��阻带衰减��  $��1��$��1��$% 40X�$	& 120X�( �� # [ $) #Data.LPFilter.StopbandAttenuationDB��� #��[ 
���   �$��2��$��绑定转速��$��2��) 
  $* Checked��.+   , �� # [ $" Data.LPFilter.BindRPMCommand�- �� # [ $ Data.RPM�
  $* 	Unchecked��.+   , �� # [ $$ Data.LPFilter.UnBindRPMCommand�- �� # [ $ Data.RPM��� $��3��$��通带��  $��3��$��1��$% 0X�( �� # [ $ Data.LPFilter.PassbandFre��� $��4��$��阻带��  $��4��$��1��$% 0X�( �� # [ $ Data.LPFilter.StopbandFre�A� $��6��$��2��.+��� $��保存��$��20��$��5q����� # [ $ Data.LPFilter.SaveCommand��� $��重置��$��20��$��5q����� # [ $  Data.LPFilter.ResetCommand��� -   $ envelopeCheckBox��$��包络�����	#FF87CDEE$��Center=��� -   $ tffCheckBox��$	��TFF�����	#FF87CDEE$��Center=��� -   $ cepstrumCheckBox��$��	倒频谱�����	#FF87CDEE$��Center=��� -	   $ 
dbCheckBox��$��dB/Hz�����	#FF87CDEE$��Center=�$
 TrueY��� $��参数��$��Right=����	#FF87CDEE$��Center=�$��0,0,5,0q� �� *
ShowDetail+� -
   $��	gridChart��$��1���� ���	#FF87CDEE$��Center=�$
��Left=�$��5,0,0,0q����� *Title+�� -   $��1��#��[  $��25��$��25��$��截图��$��0,0,5,0q�$��Right=�$	��Top=� �� $��截图��F� "����k"��pѰ��pѰ��H	$��2��$
��Left=�$	��Top=�$��15��$��15��0 �� Stretch$
0 Fill=����	#FF87CDEE$��3��F� "�������kч	��kч	�e�	$��2��$��Right=�$	��Top=�$��15��$��15��$
0 Fill=����	#FF87CDEE$��3��F� '���P�Ed7�@�@��E�� ����� 	$��2��$
��Left=�$��Bottom=�$��15��$��15��$
0 Fill=����	#FF87CDEE$��3��F� "����h�흤 �h���.р�7��.	$��2��$��Right=�$��Bottom=�$��15��$��15��$
0 Fill=����	#FF87CDEE$��3��