   M S B A M L   `   `   `  ����F  BAIC.PDAPage, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullG(clr-namespace:AIC.PDAPage.Views.SubViewsAIC.PDAPage.Views.SubViews  5      ?$clr-namespace:AIC.PDAPage.ConvertersAIC.PDAPage.Converters  5      6    0AIC.PDAPage.Views.SubViews.VibrationAdditionView   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      :local(clr-namespace:AIC.PDAPage.Views.SubViews     5      8convert$clr-namespace:AIC.PDAPage.Converters     5        � 	Resources  5
      6   0AIC.PDAPage.Converters.CodeSelectedItemConverter   codeSelectedItemConverter��%   &	          5   
   6,   5      .��5      �  � RowDefinitions 5   
   �� 5      $��* �6   �� 5      $��* �6   �� 5      $��* �6   �� 5      $��* �6    � ColumnDefinitions 5   
   �� 5      $	��120 �6   �� 5      $��* �6   �� 5      $	��120 �6   �� 5      $��* �6   .+�5   
   �� $��0��6   $��0��6!   $��测点方向:��61   $��Center=�6>   �� 5   
   -   5         ��Name$ myComBox��5   0   $��0��6   $��1��6     �� DisplayMemberPath$
 Name��6h     VirtualisedMetroComboBox#���  6�   ��6B   �� *TPDirCategory+ �� SelectedItem 5      `� 5       `� 	Converter# [  6   .1�5      ��  �� Path$ 	TPDirCode�6   �� 5      $ TPDirCategory�6    �� Mode$ OneWay=�63   5       5"      �� 5#   
   $��1��6   $��0��6   $��4��6.   $��1q�6B     AccentColorBrush#���  6V   $��	-1,0,-1,0q�6�   ._�5$      �  5%      �� 5&      $	��120 �6'   �� 5'      $��* �6'   �� 5(      $	��120 �6'   �� 5)      $��* �6'   .+�5+      �� $��0��6   $��0��6)   $��安装角度:��69   $��Center=�6F   �� 5,      $��0��6   $��1��6'     MetroTextBox#���  6q   	 � DataContext	 67   �� *SelectedItem+
 �� ElementName$
 myComBox����5-      �� *Degree+$ TwoWay=� �� UpdateSourceTrigger$ PropertyChanged=� �� ValidatesOnExceptions .  �� ValidatesOnDataErrors .  �� NotifyOnValidationError . .��6�   5.      5/      50      �� 51   
   $��2��6   $��0��6!   $��偏置高电压:��61   $��Center=�6?   �� 52   
   $��2��6   $��1��6    #���  60   ��53      �� *BiasVoltHigh+$ TwoWay=�$ PropertyChanged=� .  .  . .��6�   54      �� 55   
   $��2��6   $��2��6"   $��偏置低电压:��62   $��Center=�6A   �� 56   
   $��2��6   $��3��6    #���  60   ��57      �� *BiasVoltLow+$ TwoWay=�$ PropertyChanged=� .  .  . .��6�   58      �� 59   
   $��3��6   $��0��6!   $��
灵敏度:��61   $��Center=�6>   �� 5:   
   $��3��6   $��1��6   #���  6/    �� 
IsReadOnly 6V   �� *IsReadOnlySensitivity+��5;      �� *Sensitivity+$ TwoWay=�$ PropertyChanged=� .  .  . .��6�   5<      5>      5?      