   M S B A M L   `   `   `  ���� U  QMahApps.Metro, Version=1.5.0.23, Culture=neutral, PublicKeyToken=f4fb5a3c4d1e5b4fV;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.MetroMahApps.Metro.Controls  C ?AIC.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5(clr-namespace:AIC.Core;assembly=AIC.CoreAIC.Core F BAIC.PDAPage, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null?$clr-namespace:AIC.PDAPage.ConvertersAIC.PDAPage.Converters O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem $   AIC.PDAPage.Views.CardCopyView   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     PControls;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.Metro     9core(clr-namespace:AIC.Core;assembly=AIC.Core     8convert$clr-namespace:AIC.PDAPage.Converters     9System&clr-namespace:System;assembly=mscorlib     $��2q�   AccentColorBrush#���     � Title$  主板参数复制��$	��600��$	��600�� � WindowStartupLocation$ CenterScreen=� � Icon$1 +/AIC.Resources;component/Images/user_go.png�� � Topmost .  � 	Resources %  AIC.Core.IntVisibilityConverter  intVisibilityConverter6  0AIC.PDAPage.Converters.CodeSelectedItemConverter  codeSelectedItemConverter  ContentFontSize��%4   &	       &	      &	 
         X�16Q�.���   AccentColorBrush2#���  .+�� $��20q� � ColumnDefinitions �� $��* ��� $��* � � RowDefinitions �� $��* ��� $
��auto �.+�&   System.Windows.Controls.DataGrid  -     Name$ table��   AutoGenerateColumns .  	   CanUserAddRows	 .  
   FrozenColumnCount$
 1��$��2��$��2q����� *Paras+ �� Mode$ TwoWay=�   Columns 4  .System.Windows.Controls.DataGridCheckBoxColumn  ,  &System.Windows.Controls.DataGridColumn   Header$ 是否复制��   MinWidth$	 100Q�1  +System.Windows.Controls.DataGridBoundColumn   Binding �� *CopyChecked+0  *System.Windows.Controls.DataGridTextColumn  $ 参数名称��$	 100Q�   
IsReadOnly .  �� *Name+4  .System.Windows.Controls.DataGridTemplateColumn  $ 复制参数��$	 100Q�   CellTemplate �� .R�� {�.  .+��� $��Center=� d� 	IsChecked �� *CopyBoolPara+y��� *
CopyType+ �� 	Converter# [  �� ConverterParameter$ 0���� $��0��$��1�� �� DisplayMemberPath$
 Name��  VirtualisedMetroComboBox#���  ���� *
Category+y��� *
CopyType+# [ $ 1�� �� SelectedItem `�  `� 	Converter# [ .1���  �� Path$ CopyIntPara��� $ Category�$ OneWay=��� $��Center=����� *CopyStringPara+y��� *
CopyType+# [ $ 2��  $ 	旧参数��$	 100Q� �� .R�� {�.  .+��� $��Center=� �� *OldBoolPara+y��� *
CopyType+# [ $ 0���� $��0��$��1��$
 Name��#���  ���� *
Category+y��� *
CopyType+# [ $ 1�� `� # [ .1��� $ 
OldIntPara��� $ Category�$ OneWay=��� $��Center=����� *CopyStringPara+y��� *
CopyType+# [ $ 2��  $ 	新参数��$	 100Q� �� .R��  J� IsHitTestVisible .  .+��� $��Center=� �� *NewBoolPara+y��� *
CopyType+# [ $ 0���� $��0��$��1��$
 Name��#���  ���� *
Category+y��� *
CopyType+# [ $ 1�� `� # [ .1��� $ 
NewIntPara��� $ Category�$ OneWay=��� $��Center=����� *NewStringPara+$ TwoWay=�y��� *
CopyType+# [ $ 2��   CellEditingTemplate �� .R�� .+��� $��Center=� �� *NewBoolPara+y��� *
CopyType+# [ $ 0���� $��0��$��1��$
 Name��#���  ���� *
Category+y��� *
CopyType+# [ $ 1�� `� # [ .1��� $ 
NewIntPara��� $ Category�$ OneWay=��� $��Center=����� *NewStringPara+$ TwoWay=�y��� *
CopyType+# [ $ 2��   ColumnHeaderStyle ��   MetroDataGridColumnHeader �� BasedOn# [  �� 
TargetType=	  7System.Windows.Controls.Primitives.DataGridColumnHeader" 	 .���  �� Property � ��  AccentColorBrush3 �� Value# �  ����   MetroDataGrid# [ "  .���   �@ AlternatingRowBackground 	 AccentColorBrush4# � 	 �� -     ��Name$  btnOK��$��1��$��0�� 
 AccentedSquareButtonStyle#��� 
 $	��100��! �� 	IsDefault! . ���� " �� Orientation$" 
Horizontal=�.+��� $
��None=�$0��*/AIC.Resources;component/Images/accept.png����  	 strOK#���  $��Center=��� -   $  btnClose��$��1��$��1��#��� 
 $	��100��# �� IsCancel# . ���� $" 
Horizontal=�.+��� $
��None=�$0��*/AIC.Resources;component/Images/cancel.png����   	strCancel#���  $��Center=�