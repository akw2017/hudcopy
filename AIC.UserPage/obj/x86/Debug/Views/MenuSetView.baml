   M S B A M L   `   `   `  ����G  CAIC.UserPage, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:AIC.UserPageAIC.UserPage  5      T PWpf.PageNavigationControl, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullhJclr-namespace:Wpf.PageNavigationControl;assembly=Wpf.PageNavigationControlWpf.PageNavigationControl 5	      C ?AIC.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullI2clr-namespace:AIC.Core.ExCommand;assembly=AIC.CoreAIC.Core.ExCommand 5
      A%clr-namespace:AIC.UserPage.ConvertersAIC.UserPage.Converters  5      $    AIC.UserPage.Views.MenuSetView   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      ,localclr-namespace:AIC.UserPage     5      c _System.Windows.Interactivity, Version=4.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity  5      P LPrism.Wpf, Version=6.2.0.0, Culture=neutral, PublicKeyToken=91a96d2a154366d8$prismhttp://prismlibrary.com/  5      UpageJclr-namespace:Wpf.PageNavigationControl;assembly=Wpf.PageNavigationControl  5	      @command2clr-namespace:AIC.Core.ExCommand;assembly=AIC.Core  5
      3convert%clr-namespace:AIC.UserPage.Converters  5      !  Prism.Mvvm.ViewModelLocator    AutoWireViewModel  .    AsyncWorkUserControlStyle#���   5       � 	Resources 5       �� MergedDictionaries �� Source0   *AIC.UserPage.Converters.IntStringConverter  intStringConverter.   (AIC.UserPage.Converters.IntIconConverter  intIconConverter�� 5   
    5      �� 5      $4 ./AIC.Resources;component/Themes/ScrollBar.xaml?�6%   %:   &	       &	        5      6)     5      6'   5      .��5      �  � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��* �6   �� 5      $
��Auto �6   .+�5    
   �� $��0��6    �� Padding$ 2q�6     AccentColorBrush2#���  6+   ._�5!      ��  �� Orientation$ 
Horizontal=�6   .+�5"      ��   serverIP#���  6   $��4,0,0,0q�6>   $��Center=�6O   �� 5#      $	��200��6     VirtualisedMetroComboBox#���  6r   $��Center=�6�    �� 
IsEditable .  $��4,0,0,0q�6�   ��6'   �� *ServerIPCategory+ �� SelectedItem 6P   �� *
ServerIP+6�   �� 5$        name#���  6   $��4,0,0,0q�6:   $��Center=�6K   �� 5%      $��4,0,0,0q�6K     MetroTextBox#���  6\   $	��200��6�   $��Center=�6�   ��6   �� *
SearchName+	 �� Mode$	 OneWayToSource=�6�   �� 5&        AccentedSquareButtonStyle#���  6;   $��4,0,0,0q�6o   ��6   �� *QueryCommand+��5'      �� 5(      $ 
Horizontal=�6%   .+�5)      �� $
��None=�6$   $0��*/AIC.Resources;component/Images/search.png��63   �� 5*       
	 search#��� 	 6(   $��Center=�6H   5+      5-      �� 5.       
 menuMenuManage#��� 
 6   $��Center=�6D   $��20,0,0,0q�6_   �� 5/      $��4,0,0,0q�6r   #���  6�   $	��200��6�   $��Center=�6�   
 �� 
IsReadOnly
 .  � DataContext 6   ��  �� ElementName$ table�� �� Path$ SelectedItem���6W   �� $
 Name�6�   �� 50      #���  6u   $��4,0,0,0q�6�   ��6   �� *
AddCommand+��68   �� *SelectedItem+$ table����51      �� 52      $ 
Horizontal=�6%   .+�53      �� $
��None=�6$   $-��'/AIC.Resources;component/Images/add.png��63   �� 54        add#���  6(   $��Center=�6E   55      57      �� 58      #���  6v   $��4,0,0,0q�6�   ��6   �� *EditCommand+��69   �� *SelectedItem+$ table����59      �� 5:      $ 
Horizontal=�6%   .+�5;      �� $
��None=�6$   $0��*/AIC.Resources;component/Images/pencil.png��63   �� 5<        edit#���  6(   $��Center=�6F   5=      5?      �� 5@      #���  6x   $��4,0,0,0q�6�   ��6   �� *DeleteCommand+��6;   �� *SelectedItem+$ table����5A      �� 5B      $ 
Horizontal=�6%   .+�5C      �� $
��None=�6$   $1��+/AIC.Resources;component/Images/delete3.png��63   �� 5D       
 delete#���  6(   $��Center=�6H   5E      5G      5H      5I      &   System.Windows.Controls.DataGrid  5K   
   -   5          Name$ table��5K      $��1��6"      AutoGenerateColumns .     FrozenColumnCount$ 1��6K      
IsReadOnly .    ColumnWidth5  /System.Windows.Controls.DataGridLengthConverter$ * 6s   $��0,1,0,0q�6�   $   HorizontalScrollBarVisibility$ Disabled=�6�   ��5L      �� *
T_MenuView+   Columns 5M      0  *System.Windows.Controls.DataGridTextColumn  5N      ,  &System.Windows.Controls.DataGridColumn   Header$ ID��6%      MinWidth$	 100Q�6H   1  +System.Windows.Controls.DataGridBoundColumn   Binding 61   �� *id+6Z     5O      # �  6%   $	 100Q�6^    6E   �� *Name+6p   4	  .System.Windows.Controls.DataGridTemplateColumn	  5R        showText# �  6)   $	 100Q�6M    	  CellTemplate 5S      �� 5T      .R�5U      �� $ 
Horizontal=�6)   .+�5V   "   �� $��16��6(   $��16��63   $
��Fill=�6?   ��6N   �� *
IconPath+�� 5W   "   $��4,0,0,0q�6F   ��6,   �� *
ShowText+6F   5X      5Y      5[       �� 
GroupStyle 5]      (
  "System.Windows.Controls.GroupStyle
  5^       
  ContainerStyle 5_      �� 5`       �� 
TargetType" ��6    .�5a      ��  �� Property � �� �� Value 5b   "   �� 5c   &    �� 
TargetType" ��66   .R�5d   *   9�  9� 
IsExpanded . $��0,0,0,0q�6E   ��5e   .   �� 5f   2   $ 
Horizontal=�6=   $
��Auto��6V   .+�5g   6   �� $
��Bold"�6@   $��5,0,0,0q�6m   ��6R   �� $
 Name�6m   �� 5h   6   $
��Bold"�6@   $��:��6R   �� 5i   6   $
��Bold"�6@   ��6R   �� $ 	ItemCount��� 5j   6   $
��Bold"�6@   $	��条��6R   5k   3   ��5m   .   �� 5n   2   #���  69   $��2q�6k   ._�5o   6   �� 5p   3   5r   +   5s   '   5u      5v        
  Panel  5x      �� 5y      .R�5z      >  8System.Windows.Controls.Primitives.DataGridRowsPresenter  5{      5}      !   ColumnHeaderStyle! 5      �� 5�        MetroDataGridColumnHeader" �� BasedOn#" [ 6   =  7System.Windows.Controls.Primitives.DataGridColumnHeader"  6M   .�5�      ��  � ��  AccentColorBrush3# �  63   5�      #   	CellStyle# 5�      �� 5�        MetroDataGridCell#" [ 6   *  $System.Windows.Controls.DataGridCell"  5�      5�      $   RowStyle$ 5�      �� 5�        MetroDataGridRow#" [ 6   )  #System.Windows.Controls.DataGridRow"  5�      5�      ��5�      �� 5�        MetroDataGrid#" [ 6   "  5�      .�5�      ��   �@ AlternatingRowBackground  AccentColorBrush4# �  5�      5�      .  (System.Windows.Interactivity.Interaction%   Triggers% 5�      /  )System.Windows.Interactivity.EventTrigger  5�      &   	EventName$& MouseDoubleClick��6!   .  (System.Windows.Interactivity.TriggerBase'   Actions.' 5�      6  0System.Windows.Interactivity.InvokeCommandAction  (   Command( 6,   �� *EditCommand+)   CommandParameter) 6L   �� *SelectedItem+$ table��5�      5�      .  (Wpf.PageNavigationControl.PageNavigation  5�   
   -   5        $��pager��5�      $��2��5�   s   *   PageSizeList* 5�   +   �� *PageSizeList++   PageSize+ 6Q   �� *
PageSize+$	 TwoWay=�,   
TotalItems, 5�      �� *
TotalItems+$	 TwoWay=�-   CurrentPage- 6;   �� *CurrentPage+$	 OneWayToSource=�% 5�        5�      $& CurrentPageChanged��6!   .' 5�      .  (AIC.Core.ExCommand.ExInvokeCommandAction  .   Command. 64   �� *CurrentPageChangedComamnd+/   CommandParameter/ 6b   �� $ pager��5�      5�      5�      5�      