   M S B A M L   `   `   `  ���� G  CAIC.UserPage, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:AIC.UserPageAIC.UserPage  T PWpf.PageNavigationControl, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullhJclr-namespace:Wpf.PageNavigationControl;assembly=Wpf.PageNavigationControlWpf.PageNavigationControl C ?AIC.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullI2clr-namespace:AIC.Core.ExCommand;assembly=AIC.CoreAIC.Core.ExCommand $    AIC.UserPage.Views.RoleSetView   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     ,localclr-namespace:AIC.UserPage     c _System.Windows.Interactivity, Version=4.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity  P LPrism.Wpf, Version=6.2.0.0, Culture=neutral, PublicKeyToken=91a96d2a154366d8$prismhttp://prismlibrary.com/  UpageJclr-namespace:Wpf.PageNavigationControl;assembly=Wpf.PageNavigationControl  @command2clr-namespace:AIC.Core.ExCommand;assembly=AIC.Core  !  Prism.Mvvm.ViewModelLocator    AutoWireViewModel  .    AsyncWorkUserControlStyle#���    � 	Resources  �� MergedDictionaries �� Source��  �� $4 ./AIC.Resources;component/Themes/ScrollBar.xaml?�%    .���  � RowDefinitions �� $
��Auto ��� $��* ��� $
��Auto �.+��� $��0�� �� Padding$ 2q�  AccentColorBrush2#���  ._���  �� Orientation$ 
Horizontal=�.+���   serverIP#���  $��4,0,0,0q�$��Center=��� $	��200��  VirtualisedMetroComboBox#���  $��Center=� �� 
IsEditable .  $��4,0,0,0q����� *ServerIPCategory+ �� SelectedItem �� *
ServerIP+��   name#���  $��4,0,0,0q�$��Center=��� $��4,0,0,0q�  MetroTextBox#���  $	��200��$��Center=����� *
SearchName+	 �� Mode$	 OneWayToSource=���   AccentedSquareButtonStyle#���  $��4,0,0,0q����� *QueryCommand+���� $ 
Horizontal=�.+��� $
��None=�$0��*/AIC.Resources;component/Images/search.png����  
 search#���  $��Center=���   menuRoleManage#���  $��Center=�$��20,0,0,0q��� $��4,0,0,0q�#���  $	��200��$��Center=�
 �� 
IsReadOnly
 .  � DataContext ��  �� ElementName$ table�� �� Path$ SelectedItem����� $
 Name��� #���  $��4,0,0,0q����� *
AddCommand+���� *SelectedItem+$ table������ $ 
Horizontal=�.+��� $
��None=�$-��'/AIC.Resources;component/Images/add.png����  	 add#��� 	 $��Center=��� #���  $��4,0,0,0q����� *EditCommand+���� *SelectedItem+$ table������ $ 
Horizontal=�.+��� $
��None=�$0��*/AIC.Resources;component/Images/pencil.png����  
 edit#��� 
 $��Center=��� #���  $��4,0,0,0q����� *DeleteCommand+���� *SelectedItem+$ table������ $ 
Horizontal=�.+��� $
��None=�$1��+/AIC.Resources;component/Images/delete3.png����  
 delete#���  $��Center=�&   System.Windows.Controls.DataGrid  -     Name$ table��$��1��   AutoGenerateColumns .     FrozenColumnCount$ 1��   
IsReadOnly .    ColumnWidth5  /System.Windows.Controls.DataGridLengthConverter$ * $��0,1,0,0q����� *
T_RoleView+   Columns 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn   Header$ ID��   MinWidth$	 100Q�1  +System.Windows.Controls.DataGridBoundColumn   Binding �� *id+  # �  $	 100Q� �� *Name+   
 sortNo# �  $	 100Q� �� *	Sort_No+4  .System.Windows.Controls.DataGridTemplateColumn    isAdmin# �  $	 100Q�   CellTemplate �� .R��� {�.   d� 	IsChecked �� *
Is_Admin+    isSuperAdmin# �  $	 100Q� �� .R��� {�.   �� *Is_SuperAdmin+   ColumnHeaderStyle ��   MetroDataGridColumnHeader �� BasedOn# [  �� 
TargetType=  7System.Windows.Controls.Primitives.DataGridColumnHeader"  .���  �� Property � ��  AccentColorBrush3 �� Value# �     	CellStyle ��   MetroDataGridCell# [ *	  $System.Windows.Controls.DataGridCell" 	    RowStyle ��   MetroDataGridRow# [ )
  #System.Windows.Controls.DataGridRow" 
 ����   MetroDataGrid# [ "  .���   �@ AlternatingRowBackground  AccentColorBrush4# �  .  (System.Windows.Interactivity.Interaction    Triggers  /  )System.Windows.Interactivity.EventTrigger  !   	EventName$! MouseDoubleClick��.  (System.Windows.Interactivity.TriggerBase"   Actions." 6  0System.Windows.Interactivity.InvokeCommandAction  #   Command# �� *EditCommand+$   CommandParameter$ �� *SelectedItem+$ table��.  (Wpf.PageNavigationControl.PageNavigation  -   $��pager��$��2��%   PageSizeList% �� *PageSizeList+&   PageSize& �� *
PageSize+$	 TwoWay=�'   
TotalItems' �� *
TotalItems+$	 TwoWay=�(   CurrentPage( �� *CurrentPage+$	 OneWayToSource=�    $! CurrentPageChanged��." .  (AIC.Core.ExCommand.ExInvokeCommandAction  )   Command) �� *CurrentPageChangedComamnd+*   CommandParameter* �� $ pager��