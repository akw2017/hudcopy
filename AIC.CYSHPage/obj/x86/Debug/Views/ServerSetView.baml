   M S B A M L   `   `   `  ����G  CAIC.HomePage, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:AIC.HomePageAIC.HomePage  5      T PWpf.PageNavigationControl, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullhJclr-namespace:Wpf.PageNavigationControl;assembly=Wpf.PageNavigationControlWpf.PageNavigationControl 5	      C ?AIC.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullI2clr-namespace:AIC.Core.ExCommand;assembly=AIC.CoreAIC.Core.ExCommand 5
      &     AIC.HomePage.Views.ServerSetView   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      ,localclr-namespace:AIC.HomePage     5      c _System.Windows.Interactivity, Version=4.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity  5      P LPrism.Wpf, Version=6.2.0.0, Culture=neutral, PublicKeyToken=91a96d2a154366d8$prismhttp://prismlibrary.com/  5      UpageJclr-namespace:Wpf.PageNavigationControl;assembly=Wpf.PageNavigationControl  5	      @command2clr-namespace:AIC.Core.ExCommand;assembly=AIC.Core  5
      !  Prism.Mvvm.ViewModelLocator    AutoWireViewModel  . .��5      �  � RowDefinitions 5   
   �� 5      $��40 �6   �� 5      $��* �6   �� 5      $
��Auto �6   .+�5   
   �� $��0��6    �� Orientation$ 
Horizontal=�6"   .+�5      ��    
serverName#���   6   $��4,0,0,0q�6<   $��Center=�6M   �� 5      $��4,0,0,0q�6G     MetroTextBox#���  6X   $	��100��6   $��Center=�6�   ��6   �� *
SearchName+ �� Mode$ OneWayToSource=�6�   �� 5        AccentedSquareButtonStyle#���  67   $��4,0,0,0q�6k   $��28��6|   ��6   �� *QueryCommand+��5      �� 5      $ 
Horizontal=�6!   .+�5      �� $
��None=�6    $0��*/AIC.Resources;component/Images/search.png��6/   �� 5       
 search#���  6$   $��Center=�6D   5      5      �� 5        serverManagement#���  6   $��Center=�6B   $��20,0,0,0q�6]   �� 5       $��4,0,0,0q�6l   #���  6}   $	��100��6�   $��Center=�6�    �� 
IsReadOnly .  � DataContext 6   ��  �� ElementName$ table�� �� Path$ SelectedItem���6S   �� $ IP�6�   �� 5!      #���  6q   $��4,0,0,0q�6�   $��28��6�   ��6   �� *
AddCommand+��64   �� *SelectedItem+$ table����5"      �� 5#      $ 
Horizontal=�6!   .+�5$      �� $
��None=�6    $-��'/AIC.Resources;component/Images/add.png��6/   �� 5%        add#���  6$   $��Center=�6A   5&      5(      �� 5)      #���  6r   $��4,0,0,0q�6�   $��28��6�   ��6   �� *EditCommand+��65   �� *SelectedItem+$ table����5*      �� 5+      $ 
Horizontal=�6!   .+�5,      �� $
��None=�6    $0��*/AIC.Resources;component/Images/pencil.png��6/   �� 5-        edit#���  6$   $��Center=�6B   5.      50      �� 51      #���  6t   $��4,0,0,0q�6�   $��28��6�   ��6   �� *DeleteCommand+��67   �� *SelectedItem+$ table����52      �� 53      $ 
Horizontal=�6!   .+�54      �� $
��None=�6    $1��+/AIC.Resources;component/Images/delete3.png��6/   �� 55       
 delete#���  6$   $��Center=�6D   56      58      �� 59      #���  6u   $��4,0,0,0q�6�   $��28��6�   ��6   �� *DefaultCommand+��68   �� *SelectedItem+$ table����5:      �� 5;      $ 
Horizontal=�6!   .+�5<      �� $
��None=�6    $.��(/AIC.Resources;component/Images/tick.png��6/   �� 5=        default#���  6$   $��Center=�6E   5>      5@      5A      &   System.Windows.Controls.DataGrid  5B   
   -   5          Name$ table��5B      $��1��6"   	   AutoGenerateColumns	 .  
   FrozenColumnCount$
 1��6K      
IsReadOnly .    ColumnWidth5  /System.Windows.Controls.DataGridLengthConverter$ * 6s   ��5C      �� *ServerInfoShow+   Columns 5D      0  *System.Windows.Controls.DataGridTextColumn  5E      ,  &System.Windows.Controls.DataGridColumn   Header$ ID��6%      MinWidth$ 60Q�6H   1  +System.Windows.Controls.DataGridBoundColumn   Binding 61   �� *ID+6X     5F      # �   6%   $ 60Q�6d    6K   �� *Name+6t     5G       	 serverIP# � 	 6%   $ 60Q�6`    6I   �� *IP+6p     5I       
 organizationName# � 
 6%   $ 60Q�6m    6Q   �� *	Factory+6}     5J        	longitude# �  6%   $ 60Q�6h    6J   �� *	Longitude+6x     5K        latitude# �  6%   $ 60Q�6f    6I   �� *
Latitude+6v   4  .System.Windows.Controls.DataGridTemplateColumn  5L        defaultlogin# �  6)   $ 60Q�6Q      CellTemplate 5M      �� 5N      .R�5O      ��  ��Name$ image��6$   $��Center=�63   $6��0/AIC.Resources;component/Images/bullet_minus.png��6N   $��16��6�   $��16��6�   6�    �� Triggers 5P      �� 5Q   "    �� Value$
 True��6J    �� Binding 6.   �� *	IsLogin+.a�5R   &   ��  �� 
TargetName$ image��6-    �� Property � �� �� Value$. (/AIC.Resources;component/Images/tick.png��6R   5S   #   5U      5W        5X        
servermode# �  6)   $ 60Q�6O    5Y      �� 5Z      .R�5[      �� $ image��6$   $��Center=�63   $2��,/AIC.Resources;component/Images/computer.png��6N   $��16��6�   $��16��6�   6�    5\      �� 5]   "   $
 True��6J    6.   �� *	IsCloud+.a�5^   &   �� $ image��6-    � ��$7 1/AIC.Resources;component/Images/weather_cloud.png��6R   5_   #   5a      5c        5d        loginResult# �  6)   $ 60Q�6P    5e      �� 5f      .R�5g      �� $ image��6$   $��Center=�63   $6��0/AIC.Resources;component/Images/bullet_minus.png��6N   $��16��6�   $��16��6�   6�    5h      �� 5i   "   $
 True��6N    6.   �� *LoginResult+.a�5j   &   �� $ image��6-    � ��$. (/AIC.Resources;component/Images/tick.png��6R   5k   #   5m      5o        5p        permissions# �  6%   $ 60Q�6k    6L   �� *
Permission+6{      ColumnHeaderStyle 5r      �� 5s        MetroDataGridColumnHeader �� BasedOn# [ 6    �� 
TargetType=  7System.Windows.Controls.Primitives.DataGridColumnHeader"  6M   .�5t      ��  � ��  AccentColorBrush3# �  63   5u      ��5w      �� 5x        MetroDataGrid# [ 6   "  5y      .�5z      ��   �@ AlternatingRowBackground  GrayBrush10# �  5{      5|      .	  (System.Windows.Interactivity.Interaction 	  Triggers 5~      /
  )System.Windows.Interactivity.EventTrigger
  5       
  	EventName$ MouseDoubleClick��6!   .  (System.Windows.Interactivity.TriggerBase   Actions. 5�      6  0System.Windows.Interactivity.InvokeCommandAction     Command 6,   �� *EditCommand+    CommandParameter  6L   �� *SelectedItem+$ table��5�      5�      .  (Wpf.PageNavigationControl.PageNavigation  5�   
   -   5        $��pager��5�      $��2��5�   k   !   PageSizeList! 5�   +   �� *PageSizeList+"   PageSize" 6Q   �� *
PageSize+#   
TotalItems# 5�      �� *
TotalItems+$ TwoWay=�$   CurrentPage$ 6;   �� *CurrentPage+$ TwoWay=� 5�      
  5�      $ CurrentPageChanged��6!   . 5�      .  (AIC.Core.ExCommand.ExInvokeCommandAction  %   Command% 64   �� *CurrentPageChangedComamnd+&   CommandParameter& 6b   �� $ pager��5�      5�      5�      5�      