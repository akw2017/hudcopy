   M S B A M L   `   `   `  ����U  QMahApps.Metro, Version=1.5.0.23, Culture=neutral, PublicKeyToken=f4fb5a3c4d1e5b4fV;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.MetroMahApps.Metro.Controls  5   	   C ?AIC.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5(clr-namespace:AIC.Core;assembly=AIC.CoreAIC.Core 5   	   F BAIC.PDAPage, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null?$clr-namespace:AIC.PDAPage.ConvertersAIC.PDAPage.Converters 5   	   O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem 5	   	   $   AIC.PDAPage.Views.CardCopyView   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   PControls;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.Metro     5   	   9core(clr-namespace:AIC.Core;assembly=AIC.Core     5   	   8convert$clr-namespace:AIC.PDAPage.Converters     5   	   9System&clr-namespace:System;assembly=mscorlib     5	   	   $��2q�5   	      AccentColorBrush#���   6     � Title$  主板参数复制��5   	   $	��600��6   $	��600��6%    � WindowStartupLocation$ CenterScreen=�5   	    � Icon$1 +/AIC.Resources;component/Images/user_go.png��6.    � Topmost .  � 	Resources 5      %  AIC.Core.IntVisibilityConverter  intVisibilityConverter6  0AIC.PDAPage.Converters.CodeSelectedItemConverter  codeSelectedItemConverter  ContentFontSize��%^   &	       &	      &	 &       5   
   6&     5   
   6,   X�5   
   16Q�64   5      .��5      �   AccentColorBrush2#���  6   .+�5   
   � $��20q�6    � ColumnDefinitions 5      �� 5      $��* �6#   �� 5      $��* �6#    � RowDefinitions 5      �� 5      $��* �6    �� 5      $
��auto �6    .+�5      &   System.Windows.Controls.DataGrid  -   5          Name$ table��5         AutoGenerateColumns .  	   CanUserAddRows	 .  
   FrozenColumnCount$
 1��6Y   $��2��6o   $��2q�6�   ��5      �� *Paras+ �� Mode$ TwoWay=�   Columns 5      4  .System.Windows.Controls.DataGridCheckBoxColumn  5       ,  &System.Windows.Controls.DataGridColumn   Header$ 是否复制��6-      MinWidth$	 100Q�6;   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6J   �� *CopyChecked+6l   0  *System.Windows.Controls.DataGridTextColumn  5!      $ 参数名称��6)   $	 100Q�67      
IsReadOnly .  6F   �� *Name+6s   4  .System.Windows.Controls.DataGridTemplateColumn  5"      $ 复制参数��6-   $	 100Q�6;      CellTemplate 5#      �� 5$      .R�5%   "   � {�.  .+�5&   &   �� $��Center=�6/    d� 	IsChecked 6J   �� *CopyBoolPara+y�6m   �� *
CopyType+ �� 	Converter# [  �� ConverterParameter$ 0��6�   �� 5'   &   $��0��6/   $��1��6<    �� DisplayMemberPath$
 Name��6m     VirtualisedMetroComboBox#���  6�   ��6L   �� *
Category+y�5(   +   �� *
CopyType+# [ $ 1�� �� SelectedItem 5)   *   `� 5*   .    `� 	Converter# [ 6;   .1�5+   2   ��  �� Path$ CopyIntPara�6:   �� 5,   2   $ Category�6:   $ OneWay=�6J   5-   /   5/   '   �� 50   &   $��Center=�6.   ��6I   �� *CopyStringPara+y�6j   �� *
CopyType+# [ $ 2��6�   51   #   52      54        55      $ 	旧参数��6-   $	 100Q�6:    56      �� 57      .R�58   "   � {�.  .+�59   &   �� $��Center=�6/    6J   �� *OldBoolPara+y�6l   �� *
CopyType+# [ $ 0��6�   �� 5:   &   $��0��6/   $��1��6<   $
 Name��6m   #���  6�   ��6L   �� *
Category+y�5;   +   �� *
CopyType+# [ $ 1�� 5<   *   `� 5=   .   # [ 6;   .1�5>   2   �� $ 
OldIntPara�6:   �� 5?   2   $ Category�6:   $ OneWay=�6J   5@   /   5B   '   �� 5C   &   $��Center=�6.   ��6I   �� *CopyStringPara+y�6j   �� *
CopyType+# [ $ 2��6�   5D   #   5E      5G        5H      $ 	新参数��6-   $	 100Q�6:    5I      �� 5J      .R�5K   "   �  J� IsHitTestVisible .  .+�5L   &   �� $��Center=�6/    6J   �� *NewBoolPara+y�6l   �� *
CopyType+# [ $ 0��6�   �� 5M   &   $��0��6/   $��1��6<   $
 Name��6m   #���  6�   ��6L   �� *
Category+y�5N   +   �� *
CopyType+# [ $ 1�� 5O   *   `� 5P   .   # [ 6;   .1�5Q   2   �� $ 
NewIntPara�6:   �� 5R   2   $ Category�6:   $ OneWay=�6J   5S   /   5U   '   �� 5V   &   $��Center=�6.   ��6I   �� *NewStringPara+$ TwoWay=�y�6u   �� *
CopyType+# [ $ 2��6�   5W   #   5X         CellEditingTemplate 5Z      �� 5[      .R�5\   "   � .+�5]   &   �� $��Center=�6/    6J   �� *NewBoolPara+y�6l   �� *
CopyType+# [ $ 0��6�   �� 5^   &   $��0��6/   $��1��6<   $
 Name��6m   #���  6�   ��6L   �� *
Category+y�5_   +   �� *
CopyType+# [ $ 1�� 5`   *   `� 5a   .   # [ 6;   .1�5b   2   �� $ 
NewIntPara�6:   �� 5c   2   $ Category�6:   $ OneWay=�6J   5d   /   5f   '   �� 5g   &   $��Center=�6.   ��6I   �� *NewStringPara+$ TwoWay=�y�6u   �� *
CopyType+# [ $ 2��6�   5h   #   5i      5k         ColumnHeaderStyle 5m      �� 5n        MetroDataGridColumnHeader �� BasedOn# [ 6    �� 
TargetType=	  7System.Windows.Controls.Primitives.DataGridColumnHeader" 	 6Q   .�5o      ��  �� Property � ��  AccentColorBrush3 �� Value# �  67   5p      ��5r      �� 5s        MetroDataGrid# [ 6   "  5t      .�5u      ��   �@ AlternatingRowBackground 	 AccentColorBrush4# � 	 5v      5w      5y      �� 5z      -   5          ��Name$  btnOK��5z   2   $��1��6   $��0��6"    
 AccentedSquareButtonStyle#��� 
 6A   $	��100��6v   ! �� 	IsDefault! . ��5{      �� 5|      " �� Orientation$" 
Horizontal=�6!   .+�5}      �� $
��None=�6    $0��*/AIC.Resources;component/Images/accept.png��6/   �� 5~       	 strOK#���  6$   $��Center=�6C   5      5�      �� 5�      -   5        $  btnClose��5�   2   $��1��6   $��1��6"   #��� 
 6D   $	��100��6y   # �� IsCancel# . ��5�      �� 5�      $" 
Horizontal=�6!   .+�5�      �� $
��None=�6    $0��*/AIC.Resources;component/Images/cancel.png��6/   �� 5�        	strCancel#���  6$   $��Center=�6G   5�      5�      5�      5�      5�      