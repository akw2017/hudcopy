   M S B A M L   `   `   `  ����F  BAIC.PDAPage, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullG(clr-namespace:AIC.PDAPage.Views.SubViewsAIC.PDAPage.Views.SubViews  5      ?$clr-namespace:AIC.PDAPage.ConvertersAIC.PDAPage.Converters  5      5    /AIC.PDAPage.Views.SubViews.AbstractSlotInfoView   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      :local(clr-namespace:AIC.PDAPage.Views.SubViews     5      8convert$clr-namespace:AIC.PDAPage.Converters     5        � 	Resources  5
      6   0AIC.PDAPage.Converters.CodeSelectedItemConverter   codeSelectedItemConverter��%   &	          5   
   6,   5      .��5      �  � RowDefinitions 5   
   �� 5      $��* �6   �� 5      $��* �6   �� 5      $��* �6    � ColumnDefinitions 5   
   �� 5      $	��120 �6   �� 5      $��* �6   �� 5      $	��120 �6   �� 5      $��* �6   .+�5   
   �� $��0��6   $��0��6!   $��输入信号种类:��61   $��Center=�6@   �� 5   
   $��0��6   $��1��6     �� DisplayMemberPath$
 Name��6Y     VirtualisedMetroComboBox#���  6s   ��60   �� *InSignalCategory+ �� SelectedItem 5      `� 5       `� 	Converter# [  6   .1�5      ��  �� Path$ InSignalCode�6   �� 5      $ InSignalCategory�6    �� Mode$ OneWay=�66   5      5!      �� 5"   
   $��0��6   $��2��6!   $��槽号:��61   $��Center=�6<   �� 5#   
   $��0��6   $��3��6    �� 
IsReadOnly . 
�������  MetroTextBox#���  6l   ��6/   �� *	SlotNum+6l   �� 5$   
   $��1��6   $��0��6!   $��槽名:��61   $��Center=�6<   �� 5%   
   $��1��6   $��1��6   #���  6I   ��6/   �� *
SlotName+6I   �� 5&   
   $��1��6   $��2��6!   $��上传时间间隔:��61   $��Center=�6@   �� 5'   
   $��1��6   $��3��6   #���  6/   ��5(      �� *UploadIntevalTime+$ TwoWay=�	 �� UpdateSourceTrigger$	 PropertyChanged=�
 �� ValidatesOnExceptions
 .  �� ValidatesOnDataErrors .  �� NotifyOnValidationError . .��6�   5)      �� 5*   
   $��2��6   $��0��6!   $��输入或输出槽:��61   $��Center=�6@   �� 5+   
   $��2��6   $��1��6    {�.  $��Center=�6`    d� 	IsChecked 60   �� *	IsInput+6`   �� 5,   
   $��2��6   $��2��6!   $��单位:��61   $��Center=�6<   �� 5-   
   $��2��6   $��3��6   #���  6E   ��6/   �� *Unit+6E   5.      5/      