   M S B A M L   `   `   `  ����F  BAIC.PDAPage, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullG(clr-namespace:AIC.PDAPage.Views.SubViewsAIC.PDAPage.Views.SubViews  5      ?$clr-namespace:AIC.PDAPage.ConvertersAIC.PDAPage.Converters  5      C ?AIC.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5(clr-namespace:AIC.Core;assembly=AIC.CoreAIC.Core 5      G CAIC.CoreType, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullA0clr-namespace:AIC.CoreType;assembly=AIC.CoreTypeAIC.CoreType 5	      8    2AIC.PDAPage.Views.SubViews.AbstractChannelInfoView   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      <subview(clr-namespace:AIC.PDAPage.Views.SubViews     5      8convert$clr-namespace:AIC.PDAPage.Converters     5      9core(clr-namespace:AIC.Core;assembly=AIC.Core     5      EcoreType0clr-namespace:AIC.CoreType;assembly=AIC.CoreType     5	        � 	Resources  5      6   0AIC.PDAPage.Converters.CodeSelectedItemConverter   codeSelectedItemConverter(  "AIC.Core.BoolToVisibilityConverter  boolToVisibilityConverter'  !AIC.Core.EnumDescriptionConverter  enumDescriptionConverter  enumComboItemTemplate �� Path �� 	Converter��%�   &	        &	      &	 &     &	 9     7    5   
   6,     5   
   6)     5   
   6(   �� 5   
   .R�5      �� ��6   �� $ .�8   5      5      .��5      �  � RowDefinitions 5   
   �� 5      $��* �6   �� 5      $��* �6   �� 5      $��* �6   �� 5      $��* �6   �� 5      $��* �6   �� 5      $��* �6   �� 5      $��* �6   �� 5      $��* �6   �� 5      $��* �6   �� 5       $��* �6   �� 5!      $��* �6   �� 5"      $��* �6   �� 5#      $��* �6    � ColumnDefinitions 5&   
   �� 5'      $	��120 �6   �� 5(      $��* �6   �� 5)      $	��120 �6   �� 5*      $��* �6   .+�5,   
   �� $��0��6   $��0��6!   $��通用设置��61   $��Center=�6>   $
��Bold"�6Y   �� 5.   
   $��1��6   $��0��6!   $��设备名称:��61   $��Center=�6>   �� 5/   
   $��1��6   $��1��6     MetroTextBox#���  6N    �� 
IsReadOnly . ��6/   �� *T_Device_Name+6u   �� 50   
   $��1��6   $��2��6!   $��设备编号:��61   $��Center=�6>   �� 51   
   $��1��6   $��3��6   #���  6N    . ��6/   �� *T_Device_Code+6u   �� 52   
   $��2��6   $��0��6!   $��设备Guid:��61   $��Center=�6@   �� 53   
   $��2��6   $��1��6   #���  6N    . ��6/   �� *T_Device_Guid+6u   �� 54   
   $��2��6   $��2��6!   $��测点名称:��61   $��Center=�6>   �� 55   
   $��2��6   $��3��6   #���  6L    . ��6/   �� *T_Item_Name+6s   �� 56   
   $��3��6   $��0��6!   $��测点编号:��61   $��Center=�6>   �� 57   
   $��3��6   $��1��6   #���  6L    . ��6/   �� *T_Item_Code+6s   �� 58   
   $��3��6   $��2��6!   $��测点Guid:��61   $��Center=�6@   �� 59   
   $��3��6   $��3��6   #���  6L    . ��6/   �� *T_Item_Guid+6s   �� 5:   
   $��4��6   $��0��6!   $��
通道号:��61   $��Center=�6=   �� 5;   
   $��4��6   $��1��6   #���  6F    . ��6/   �� *CHNum+6m   �� 5<   
   $��4��6   $��2��6!   $��子通道号:��61   $��Center=�6>   �� 5=   
   $��4��6   $��3��6   #���  6I    . ��6/   �� *
SubCHNum+6p   �� 5>   
   $��5��6   $��0��6!   $��是否上传:��61   $��Center=�6>   �� 5?   
   $��5��6   $��1��6    $��Center=�6S    d� 	IsChecked 60   �� *IsUploadData+6S   �� 5@   
   $��5��6   $��2��6!   $��单位:��61   $��Center=�6<   y�6W   �� *
IsShowUnit+# [ �� 5B   
   $��5��6   $��3��6      VirtualisedMetroComboBox#���  6s    �� 
IsEditable . ��60   �� *UnitCategory+ �� SelectedItem 6U   �� *Unit+y�6�   �� *
IsShowUnit+# [ �� 5D   
   $��6��6   $��0��6!   $��单值类型:��61   $��Center=�6>   y�6Y   �� *IsShowSVType+# [ �� 5F   
   $��6��6   $��1��6    	 �� DisplayMemberPath$
	 Name��6W   #���  6p   ��60   �� *SVTypeCategory+y�6�   �� *IsShowSVType+# [  5H      `� 5I      
 `� 	Converter#
 [  6   .1�5J      �� $ 
SVTypeCode�6   �� 5K      $ SVTypeCategory�6    �� Mode$ OneWay=�64   5L      5N      �� 5O   
   $��7��6   $��0��6   $��4��6.   $��1q�6B     AccentColorBrush#���  6V   $��	-1,0,-1,0q�6�   ._�5P      �  5Q      �� 5R      $	��120 �6'   �� 5S      $��* �6'   �� 5T      $	��120 �6'   �� 5U      $��* �6'   .+�5W      �� $��0��6   $��0��6)   $��零点:��69   $��Center=�6D   �� 5X      $��0��6   $��1��6'   #���  6q    � DataContext 67   �� *SelectedItem+ �� ElementName$ myComBox����5Y      �� *	ZeroValue+$ TwoWay=� �� UpdateSourceTrigger$ PropertyChanged=� �� ValidatesOnExceptions .  �� ValidatesOnDataErrors .  �� NotifyOnValidationError . .��6�   5Z      5[      5\      �� 5]   
   $��8��6   $��0��6!   $��本地存储策略:��61   $��Center=�6@   �� 5^   
   $��8��6   $��1��6    $
	 Name��6Z   #���  6s   ��60   �� *LocalSaveCategory+ 5_      `� 5`      #
 [  6   .1�5a      �� $ LocalSaveCode�6   �� 5b      $ LocalSaveCategory�6   $ OneWay=�67   5c      5e      �� 5f   
   $��8��6   $��2��6!   $��是否旁路:��61   $��Center=�6>   �� 5g   
   $��8��6   $��3��6    $��Center=�6O    60   �� *
IsBypass+6O   �� 5h   
   $��9��6   $��0��6!   $��延时报警:��61   $��Center=�6>   �� 5i   
   $��9��6   $��1��6   #���  6/   ��5j      �� *DelayAlarmTime+$ TwoWay=�$ PropertyChanged=� .  .  . .��6�   5k      �� 5l   
   $��9��6   $��2��6!   $��NotOK延时报警:��61   $��Center=�6C   �� 5m   
   $��9��6   $��3��6   #���  6/   ��5n      �� *NotOKDelayAlarmTime+$ TwoWay=�$ PropertyChanged=� .  .  . .��6�   5o      �� 5p   
   $��10��6   $��0��6"   $��启用逻辑表达式:��62   $��Center=�6B   �� 5q   
   $��10��6   $��1��6!   $��Center=�6O    61   �� *	IsLogic+6O   �� 5r   
   $��10��6   $��2��6"   $��逻辑表达式:��62   $��Center=�6@   �� 5s   
   $��10��6   $��3��6    #���  6u   ��60   �� *LogicExpression+$ PropertyChanged=�6u   �� 5t   
   $��11��6   $��0��6"   $��备注:��62   $��Center=�6=   �� 5u   
   $��11��6   $��1��6    #���  6m   ��60   �� *	Remarks+$ PropertyChanged=�6m   �� 5v   
   $��11��6   $��2��6"   $��附加信息:��62   $��Center=�6?   �� 5w   
   $��11��6   $��3��6    #���  6w   ��60   �� *Extra_Information+$ PropertyChanged=�6w   5y      5z      