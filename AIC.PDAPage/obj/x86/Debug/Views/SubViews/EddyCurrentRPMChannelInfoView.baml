   M S B A M L   `   `   `  ����F  BAIC.PDAPage, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullG(clr-namespace:AIC.PDAPage.Views.SubViewsAIC.PDAPage.Views.SubViews  5      ?$clr-namespace:AIC.PDAPage.ConvertersAIC.PDAPage.Converters  5      >    8AIC.PDAPage.Views.SubViews.EddyCurrentRPMChannelInfoView   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      <subview(clr-namespace:AIC.PDAPage.Views.SubViews     5      8convert$clr-namespace:AIC.PDAPage.Converters     5        � 	Resources  5
      6   0AIC.PDAPage.Converters.CodeSelectedItemConverter   codeSelectedItemConverter��%   &	          5   
   6,   5      .��5      �  � RowDefinitions 5   
   �� 5      $��* �6   �� 5      $��* �6   �� 5      $��* �6   �� 5      $	��12* �6   �� 5      $��3* �6   �� 5      $��4* �6    � ColumnDefinitions 5   
   �� 5      $	��120 �6   �� 5      $��* �6   �� 5      $	��120 �6   �� 5      $��* �6   .+�5   
   �� $��0��6   $��0��6!   $��电涡流转速设置��61   $��Center=�6A   $
��Bold"�6\   �� 5   
   $��1��6   $��0��6!   $��门槛值电压:��61   $��Center=�6?   �� 5   
   $��1��6   $��1��6     MetroTextBox#���  6/   ��5      �� *ThresholdVolt+ �� Mode$ TwoWay=� �� UpdateSourceTrigger$ PropertyChanged=� �� ValidatesOnExceptions .  �� ValidatesOnDataErrors .  �� NotifyOnValidationError . .��6�   5       �� 5!   
   $��1��6   $��2��6!   $��迟滞电压:��61   $��Center=�6>   �� 5"   
   $��1��6   $��3��6   #���  6/   ��5#      �� *HysteresisVolt+$ TwoWay=�$ PropertyChanged=� .  .  . .��6�   5$      �� 5%   
   $��2��6   $��0��6!   $��门槛值种类:��61   $��Center=�6?   �� 5&   
   $��2��6   $��1��6     �� DisplayMemberPath$
 Name��6^     VirtualisedMetroComboBox#���  6w   ��60   �� *ThresholdModeCategory+	 �� SelectedItem	 5'      `� 5(      
 `� 	Converter#
 [  6   .1�5)      ��  �� Path$ ThresholdModeCode�6   �� 5*      $ ThresholdModeCategory�6   $ OneWay=�6;   5+      5-      8   2AIC.PDAPage.Views.SubViews.AbstractChannelInfoView  5.   
   $��3��6*   $��0��67   $��4��6G   3   -AIC.PDAPage.Views.SubViews.RPMChannelInfoView  5/   
   $��4��6%   $��0��62   $��4��6B   6   0AIC.PDAPage.Views.SubViews.VibrationAdditionView  50   
   $��5��6(   $��0��65   $��4��6E   51      52      