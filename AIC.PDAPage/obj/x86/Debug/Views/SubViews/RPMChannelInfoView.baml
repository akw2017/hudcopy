   M S B A M L   `   `   `  ����F  BAIC.PDAPage, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullG(clr-namespace:AIC.PDAPage.Views.SubViewsAIC.PDAPage.Views.SubViews  5      3    -AIC.PDAPage.Views.SubViews.RPMChannelInfoView   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      :local(clr-namespace:AIC.PDAPage.Views.SubViews     5      .��5	      �   � RowDefinitions  5
   
   �� 5      $��* �6   �� 5      $��* �6   �� 5      $��* �6    � ColumnDefinitions 5   
   �� 5      $	��120 �6   �� 5      $��* �6   �� 5      $	��120 �6   �� 5      $��* �6   .+�5   
   �� $��0��6   $��0��6!   $��转速耦合设置��61   $��Center=�6@   $
��Bold"�6[   �� 5   
   $��1��6   $��0��6!   $��转速校准系数:��61   $��Center=�6@   �� 5   
   $��1��6   $��1��6      MetroTextBox#���   6/   ��5      �� *CalibrationCor+ �� Mode$ TwoWay=� �� UpdateSourceTrigger$ PropertyChanged=� �� ValidatesOnExceptions .  �� ValidatesOnDataErrors .  �� NotifyOnValidationError . .��6�   5      �� 5   
   $��1��6   $��2��6!   $��凹槽还是凸槽:��61   $��Center=�6@   �� 5   
   $��1��6   $��3��6   #���   6H   ��6/   �� *	IsNotch+6H   �� 5   
   $��2��6   $��0��6!   $��平均次数:��61   $��Center=�6>   �� 5   
   $��2��6   $��1��6   #���   6/   ��5      �� *AverageNumber+$ TwoWay=�$ PropertyChanged=� .  .  . .��6�   5      �� 5    
   $��2��6   $��2��6!   $��齿数:��61   $��Center=�6<   �� 5!   
   $��2��6   $��3��6   #���   6/   ��5"      �� *TeethNumber+$ TwoWay=�$ PropertyChanged=� .  .  . .��6�   5#      5$      5%      