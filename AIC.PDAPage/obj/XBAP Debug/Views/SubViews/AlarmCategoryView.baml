   M S B A M L   `   `   `  ���� F  BAIC.PDAPage, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullG(clr-namespace:AIC.PDAPage.Views.SubViewsAIC.PDAPage.Views.SubViews  C ?AIC.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5(clr-namespace:AIC.Core;assembly=AIC.CoreAIC.Core 2    ,AIC.PDAPage.Views.SubViews.AlarmCategoryView   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     :local(clr-namespace:AIC.PDAPage.Views.SubViews     9core(clr-namespace:AIC.Core;assembly=AIC.Core       � 	Resources  (  "AIC.Core.BoolToVisibilityConverter   boolToVisibilityConverter  AIC.Core.BindingProxy 	 proxy   Data��%'   &	        &	           �� .���  � RowDefinitions �� $��* ��� $��9* � � ColumnDefinitions �� $	��120 ��� $��* ��� $	��120 ��� $��* �.+��� $��绝对值报警种类:��$��0��$��0��$��4��$��Center=��� $��1��$��4��$��1q�  AccentColorBrush#���  $��	-1,0,-1,0q�._�&   System.Windows.Controls.DataGrid  -     Name$ table��   AutoGenerateColumns .     CanUserAddRows .     ColumnWidth5  /System.Windows.Controls.DataGridLengthConverter$
 Auto ���� *
Category+   Columns 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn	   MinWidth$	 60Q�
   Header$
 代码��   
IsReadOnly . 1  +System.Windows.Controls.DataGridBoundColumn   Binding �� *Code+  $	 60Q�$
 名称�� .  �� *Name+4  .System.Windows.Controls.DataGridCheckBoxColumn  $	 60Q�$
 启用�� �� *	IsAllow+ �� Mode$ TwoWay=� �� UpdateSourceTrigger$ PropertyChanged=�4	  .System.Windows.Controls.DataGridTemplateColumn	  $	 60Q�$
 报警槛值�� 	  CellTemplate �� .R���  ��Name$ txtValue��  MetroTextBox#���   �� 
IsReadOnly . ���� *Value+$ TwoWay=�$ PropertyChanged=� �� ValidatesOnExceptions .  �� ValidatesOnDataErrors .  �� NotifyOnValidationError . .�� �� Triggers ��  �� Value$
 true�� �� Binding �� *	IsAllow+.a���  �� 
TargetName$ txtValue�� �� Property �@��
IsReadOnly �� Value .    $	 60Q�$
 上传波形�� �� *	IsACQWave+$ TwoWay=�$ PropertyChanged=� �� *	IsAllow+   
Visibility �� *Data.IsUploadWaveShow+ �� Source# [  �� 	Converter# [  	  $	 60Q�$
 报警方程�� .  �� .R��� $ txtValue��#���   . ���� *	Formula+$ TwoWay=�$ PropertyChanged=� .  .  . .�� �� $
 true�� �� *	IsAllow+.a��� $ txtValue�� �@��
IsReadOnly .     ColumnHeaderStyle ��   MetroDataGridColumnHeader �� BasedOn# [   �� 
TargetType=
  7System.Windows.Controls.Primitives.DataGridColumnHeader"  
 .���  � ��  AccentColorBrush3# �  ��  � ��$ 35������   MetroDataGrid# [ "   .���   �@ AlternatingRowBackground  AccentColorBrush4# �    *  $System.Windows.Controls.DataGridCell��%-   '        �� "   .���  � ��$ 34��