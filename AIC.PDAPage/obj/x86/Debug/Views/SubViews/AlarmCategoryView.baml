   M S B A M L   `   `   `  ����F  BAIC.PDAPage, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullG(clr-namespace:AIC.PDAPage.Views.SubViewsAIC.PDAPage.Views.SubViews  5      C ?AIC.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5(clr-namespace:AIC.Core;assembly=AIC.CoreAIC.Core 5      2    ,AIC.PDAPage.Views.SubViews.AlarmCategoryView   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      :local(clr-namespace:AIC.PDAPage.Views.SubViews     5      9core(clr-namespace:AIC.Core;assembly=AIC.Core     5        � 	Resources  5
      (  "AIC.Core.BoolToVisibilityConverter   boolToVisibilityConverter  AIC.Core.BindingProxy 	 proxy   Data��%C   &	        &	        5   
   6)     5   
    6*   �� 5      .��5      �  � RowDefinitions 5   
   �� 5      $��* �6   �� 5      $��9* �6    � ColumnDefinitions 5   
   �� 5      $	��120 �6   �� 5      $��* �6   �� 5      $	��120 �6   �� 5      $��* �6   .+�5   
   �� $��绝对值报警种类:��6   $��0��6$   $��0��61   $��4��6A   $��Center=�6U   �� 5   
   $��1��6   $��4��6   $��1q�62     AccentColorBrush#���  6F   $��	-1,0,-1,0q�6x   ._�5      &   System.Windows.Controls.DataGrid  -   5          Name$ table��5         AutoGenerateColumns .     CanUserAddRows .     ColumnWidth5  /System.Windows.Controls.DataGridLengthConverter$
 Auto 6z   ��6Y   �� *
Category+   Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn	   MinWidth$	 60Q�6)   
   Header$
 代码��67      
IsReadOnly . 1  +System.Windows.Controls.DataGridBoundColumn   Binding 6U   �� *Code+6p     5      $	 60Q�6)   $
 名称��67    .  6U   �� *Name+6p   4  .System.Windows.Controls.DataGridCheckBoxColumn  5      $	 60Q�6-   $
 启用��6;    6G   �� *	IsAllow+ �� Mode$ TwoWay=� �� UpdateSourceTrigger$ PropertyChanged=�6�   4	  .System.Windows.Controls.DataGridTemplateColumn	  5       $	 60Q�6-   $
 报警槛值��6;    	  CellTemplate 5!      �� 5"      .R�5#   "   ��  ��Name$ txtValue��6*     MetroTextBox#���  6=    �� 
IsReadOnly . ��5$   %   �� *Value+$ TwoWay=�$ PropertyChanged=� �� ValidatesOnExceptions .  �� ValidatesOnDataErrors .  �� NotifyOnValidationError . .��6�   5%   #    �� Triggers 5&   "   �� 5'   &    �� Value$
 true��6N    �� Binding 62   �� *	IsAllow+.a�5(   *   ��  �� 
TargetName$ txtValue��61    �� Property �@��
IsReadOnly �� Value .  6]   5)   '   5+      5-        5.      $	 60Q�6-   $
 上传波形��6;    6I   �� *	IsACQWave+$ TwoWay=�$ PropertyChanged=� 6�   �� *	IsAllow+   
Visibility 6�   �� *Data.IsUploadWaveShow+ �� Source# [  �� 	Converter# [  	  5;      $	 60Q�6-   $
 报警方程��6;    .  5<      �� 5=      .R�5>   "   �� $ txtValue��6*   #���  6=    . ��5?   %   �� *	Formula+$ TwoWay=�$ PropertyChanged=� .  .  . .��6�   5@   #    5A   "   �� 5B   &   $
 true��6N    62   �� *	IsAllow+.a�5C   *   �� $ txtValue��61    �@��
IsReadOnly .  6]   5D   '   5F      5H         ColumnHeaderStyle 5J      �� 5K        MetroDataGridColumnHeader �� BasedOn# [ 6     �� 
TargetType=
  7System.Windows.Controls.Primitives.DataGridColumnHeader"  
 6Q   .�5L      ��  � ��  AccentColorBrush3# �  67   �� 5M       � ��$ 35��63   5N      ��5P      �� 5Q        MetroDataGrid# [ 6   "   5R      .�5S      ��   �@ AlternatingRowBackground  AccentColorBrush4# �  5T      5V        5X      *  $System.Windows.Controls.DataGridCell��%R   '        �� 5Y      "   6   .�5Z      ��  � ��$ 34��63   5[      5\      5]      5^      5_      5`      