   M S B A M L   `   `   `  ����F  BAIC.PDAPage, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullG(clr-namespace:AIC.PDAPage.Views.SubViewsAIC.PDAPage.Views.SubViews  5      6    0AIC.PDAPage.Views.SubViews.SourceChannelInfoView   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      :local(clr-namespace:AIC.PDAPage.Views.SubViews     5      .��5	      �   � RowDefinitions  5
   
   �� 5      $��* �6   �� 5      $��* �6   �� 5      $��* �6    � ColumnDefinitions 5   
   �� 5      $	��120 �6   �� 5      $��* �6   �� 5      $	��120 �6   �� 5      $��* �6   .+�5   
   �� $��0��6   $��0��6!   $��数据来源设置��61   $��Center=�6@   $
��Bold"�6[   �� 5   
   $��1��6   $��0��6!   $��卡号:��61   $��Center=�6<   �� 5   
   $��1��6   $��1��6      MetroTextBox#���   6/   ��5      �� *SourceCardNum+ �� Mode$ TwoWay=� �� UpdateSourceTrigger$ PropertyChanged=� �� ValidatesOnExceptions .  �� ValidatesOnDataErrors .  �� NotifyOnValidationError . .��6�   5      �� 5   
   $��1��6   $��2��6!   $��槽号:��61   $��Center=�6<   �� 5   
   $��1��6   $��3��6    #���   60   ��5      �� *SourceSlotNum+$ TwoWay=�$ PropertyChanged=� .  .  . .��6�   5      �� 5   
   $��2��6   $��0��6"   $��
通道号:��62   $��Center=�6>   �� 5   
   $��2��6   $��1��6    #���   60   ��5       �� *SourceCHNum+$ TwoWay=�$ PropertyChanged=� .  .  . .��6�   5!      �� 5"   
   $��2��6   $��2��6"   $��子通道号:��62   $��Center=�6?   �� 5#   
   $��2��6   $��3��6    #���   60   ��5$      �� *SourceSubCHNum+$ TwoWay=�$ PropertyChanged=� .  .  . .��6�   5%      5&      5'      