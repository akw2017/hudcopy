   M S B A M L   `   `   `  ����G  CAIC.UserPage, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:AIC.UserPageAIC.UserPage  5      T PWpf.PageNavigationControl, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullhJclr-namespace:Wpf.PageNavigationControl;assembly=Wpf.PageNavigationControlWpf.PageNavigationControl 5	      C ?AIC.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullI2clr-namespace:AIC.Core.ExCommand;assembly=AIC.CoreAIC.Core.ExCommand 5
      $    AIC.UserPage.Views.RoleSetView   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      ,localclr-namespace:AIC.UserPage     5      c _System.Windows.Interactivity, Version=4.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity  5      P LPrism.Wpf, Version=6.2.0.0, Culture=neutral, PublicKeyToken=91a96d2a154366d8$prismhttp://prismlibrary.com/  5      UpageJclr-namespace:Wpf.PageNavigationControl;assembly=Wpf.PageNavigationControl  5	      @command2clr-namespace:AIC.Core.ExCommand;assembly=AIC.Core  5
      !  Prism.Mvvm.ViewModelLocator    AutoWireViewModel  .    AsyncWorkUserControlStyle#���   5       � 	Resources 5       �� MergedDictionaries �� Source�� 5   
    5      �� 5      $4 ./AIC.Resources;component/Themes/ScrollBar.xaml?�6%   %    5      .��5      �  � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��* �6   �� 5      $
��Auto �6   .+�5   
   �� $��0��6    �� Padding$ 2q�6     AccentColorBrush2#���  6*   ._�5      ��  �� Orientation$ 
Horizontal=�6   .+�5      ��   serverIP#���  6   $��4,0,0,0q�6>   $��Center=�6O   �� 5      $	��200��6     VirtualisedMetroComboBox#���  6r   $��Center=�6�    �� 
IsEditable .  $��4,0,0,0q�6�   ��6'   �� *ServerIPCategory+ �� SelectedItem 6P   �� *
ServerIP+6�   �� 5         name#���  6   $��4,0,0,0q�6:   $��Center=�6K   �� 5!      $��4,0,0,0q�6K     MetroTextBox#���  6\   $	��200��6�   $��Center=�6�   ��6   �� *
SearchName+	 �� Mode$	 OneWayToSource=�6�   �� 5"        AccentedSquareButtonStyle#���  6;   $��4,0,0,0q�6o   ��6   �� *QueryCommand+��5#      �� 5$      $ 
Horizontal=�6%   .+�5%      �� $
��None=�6$   $0��*/AIC.Resources;component/Images/search.png��63   �� 5&       
 search#���  6(   $��Center=�6H   5'      5)      �� 5*        menuRoleManage#���  6   $��Center=�6D   $��20,0,0,0q�6_   �� 5+      $��4,0,0,0q�6r   #���  6�   $	��200��6�   $��Center=�6�   
 �� 
IsReadOnly
 .  � DataContext 6   ��  �� ElementName$ table�� �� Path$ SelectedItem���6W   �� $
 Name�6�   �� 5,      #���  6u   $��4,0,0,0q�6�   ��6   �� *
AddCommand+��68   �� *SelectedItem+$ table����5-      �� 5.      $ 
Horizontal=�6%   .+�5/      �� $
��None=�6$   $-��'/AIC.Resources;component/Images/add.png��63   �� 50       	 add#��� 	 6(   $��Center=�6E   51      53      �� 54      #���  6v   $��4,0,0,0q�6�   ��6   �� *EditCommand+��69   �� *SelectedItem+$ table����55      �� 56      $ 
Horizontal=�6%   .+�57      �� $
��None=�6$   $0��*/AIC.Resources;component/Images/pencil.png��63   �� 58       
 edit#��� 
 6(   $��Center=�6F   59      5;      �� 5<      #���  6x   $��4,0,0,0q�6�   ��6   �� *DeleteCommand+��6;   �� *SelectedItem+$ table����5=      �� 5>      $ 
Horizontal=�6%   .+�5?      �� $
��None=�6$   $1��+/AIC.Resources;component/Images/delete3.png��63   �� 5@       
 delete#���  6(   $��Center=�6H   5A      5C      5D      5E      &   System.Windows.Controls.DataGrid  5G   
   -   5          Name$ table��5G      $��1��6"      AutoGenerateColumns .     FrozenColumnCount$ 1��6K      
IsReadOnly .    ColumnWidth5  /System.Windows.Controls.DataGridLengthConverter$ * 6s   $��0,1,0,0q�6�   ��5H      �� *
T_RoleView+   Columns 5I      0  *System.Windows.Controls.DataGridTextColumn  5J      ,  &System.Windows.Controls.DataGridColumn   Header$ ID��6%      MinWidth$	 100Q�6I   1  +System.Windows.Controls.DataGridBoundColumn   Binding 61   �� *id+6Z     5K      # �  6%   $	 100Q�6^    6E   �� *Name+6o     5M       
 sortNo# �  6%   $	 100Q�6c    6G   �� *	Sort_No+6t   4  .System.Windows.Controls.DataGridTemplateColumn  5N        isAdmin# �  6)   $	 100Q�6L      CellTemplate 5O      �� 5P      .R�5Q      �� {�.   d� 	IsChecked 6'   �� *
Is_Admin+6F   5R      5T        5U        isSuperAdmin# �  6)   $	 100Q�6Q    5V      �� 5W      .R�5X      �� {�.   6'   �� *Is_SuperAdmin+6K   5Y      5[         ColumnHeaderStyle 5]      �� 5^        MetroDataGridColumnHeader �� BasedOn# [ 6    �� 
TargetType=  7System.Windows.Controls.Primitives.DataGridColumnHeader"  6M   .�5_      ��  �� Property � ��  AccentColorBrush3 �� Value# �  63   5`         	CellStyle 5b      �� 5c        MetroDataGridCell# [ 6   *	  $System.Windows.Controls.DataGridCell" 	 5d      5f         RowStyle 5h      �� 5i        MetroDataGridRow# [ 6   )
  #System.Windows.Controls.DataGridRow" 
 5j      5l      ��5n      �� 5o        MetroDataGrid# [ 6   "  5p      .�5q      ��   �@ AlternatingRowBackground  AccentColorBrush4# �  5r      5s      .  (System.Windows.Interactivity.Interaction    Triggers  5u      /  )System.Windows.Interactivity.EventTrigger  5v      !   	EventName$! MouseDoubleClick��6!   .  (System.Windows.Interactivity.TriggerBase"   Actions." 5w      6  0System.Windows.Interactivity.InvokeCommandAction  #   Command# 6,   �� *EditCommand+$   CommandParameter$ 6L   �� *SelectedItem+$ table��5x      5z      .  (Wpf.PageNavigationControl.PageNavigation  5|   
   -   5        $��pager��5|      $��2��5}   s   %   PageSizeList% 5|   +   �� *PageSizeList+&   PageSize& 6Q   �� *
PageSize+$	 TwoWay=�'   
TotalItems' 5}      �� *
TotalItems+$	 TwoWay=�(   CurrentPage( 6;   �� *CurrentPage+$	 OneWayToSource=�  5~        5      $! CurrentPageChanged��6!   ." 5�      .  (AIC.Core.ExCommand.ExInvokeCommandAction  )   Command) 64   �� *CurrentPageChangedComamnd+*   CommandParameter* 6b   �� $ pager��5�      5�      5�      5�      