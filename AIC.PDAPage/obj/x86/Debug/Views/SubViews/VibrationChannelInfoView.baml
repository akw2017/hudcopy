   M S B A M L   `   `   `  ����F  BAIC.PDAPage, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullG(clr-namespace:AIC.PDAPage.Views.SubViewsAIC.PDAPage.Views.SubViews  5      G(clr-namespace:AIC.PDAPage.Views.SubViewsAIC.PDAPage.Views.SubViews  5      9    3AIC.PDAPage.Views.SubViews.VibrationChannelInfoView   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      :local(clr-namespace:AIC.PDAPage.Views.SubViews     5      <subview(clr-namespace:AIC.PDAPage.Views.SubViews     5      .��5
      �   � RowDefinitions  5   
   �� 5      $	��12* �6   �� 5      $��4* �6   �� 5      $��* �6   �� 5      $��* �6   �� 5      $��* �6   �� 5      $��* �6    � ColumnDefinitions 5   
   �� 5      $	��120 �6   �� 5      $��* �6   �� 5      $	��120 �6   �� 5      $��* �6   .+�5   
   8   2AIC.PDAPage.Views.SubViews.AbstractChannelInfoView  $��0��6*   $��0��67   $��4��6G   6   0AIC.PDAPage.Views.SubViews.VibrationAdditionView  5   
   $��1��6(   $��0��65   $��4��6E   �� 5   
   $��2��6   $��0��6!   $��转速卡号:��61   $��Center=�6>   �� 5   
   $��2��6   $��1��6      MetroTextBox#���   6/   ��5      �� *
RPMCardNum+ �� Mode$ TwoWay=� �� UpdateSourceTrigger$ PropertyChanged=� �� ValidatesOnExceptions .  �� ValidatesOnDataErrors .  �� NotifyOnValidationError . .��6�   5      �� 5   
   $��2��6   $��2��6!   $��转速槽号:��61   $��Center=�6>   �� 5    
   $��2��6   $��3��6   #���   6/   ��5!      �� *
RPMSlotNum+$ TwoWay=�$ PropertyChanged=� .  .  . .��6�   5"      �� 5#   
   $��3��6   $��0��6!   $��转速通道号:��61   $��Center=�6?   �� 5$   
   $��3��6   $��1��6   #���   6/   ��5%      �� *
RPMCHNum+$ TwoWay=�$ PropertyChanged=� .  .  . .��6�   5&      �� 5'   
   $��3��6   $��2��6!   $��是否倍增:��61   $��Center=�6>   �� 5(   
   $��3��6   $��3��6    $��Center=�6W    d� 	IsChecked 60   �� *IsMultiplication+6W   �� 5)   
   $��4��6   $��0��6!   $��倍增系数:��61   $��Center=�6>   �� 5*   
   $��4��6   $��1��6   #���   6R   ��6/   �� *MultiplicationCor+6R   �� 5+   
   $��4��6   $��2��6!   $��是否在SD存储波形:��61   $��Center=�6C   �� 5,   
   $��4��6   $��3��6    $��Center=�6U    60   �� *IsSaveWaveToSD+6U   �� 5-   
   $��5��6   $��0��6!   $��是否上传波形:��61   $��Center=�6@   �� 5.   
   $��5��6   $��1��6    $��Center=�6S    60   �� *IsUploadWave+6S   �� 5/   
   $��5��6   $��2��6!   $��默认转速:��61   $��Center=�6>   �� 50   
   $��5��6   $��3��6   #���   60   ��51      �� *
DefaultRPM+$ TwoWay=�$ PropertyChanged=� .  .  . .��6�   52      53      54      