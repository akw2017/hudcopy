   M S B A M L   `   `   `  ����G  CAIC.HomePage, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:AIC.HomePageAIC.HomePage  5      &     AIC.HomePage.Views.ServerSetView   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      ,localclr-namespace:AIC.HomePage     5      c _System.Windows.Interactivity, Version=4.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity  5      P LPrism.Wpf, Version=6.2.0.0, Culture=neutral, PublicKeyToken=91a96d2a154366d8$prismhttp://prismlibrary.com/  5      !  Prism.Mvvm.ViewModelLocator    AutoWireViewModel  .  � 	Resources 5       �� MergedDictionaries �� Source�� 5   
    5      �� 5      $1 +/AIC.HomePage;component/Themes/Generic.xaml?�6%   %    5      .��5      �  � RowDefinitions 5   
   �� 5      $��40 �6   �� 5      $��* �6   �� 5      $
��Auto �6    � ColumnDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��* �6   .+�5   
   �� $��0��6   $��1��6"    �� Orientation$ 
Horizontal=�62   .+�5      �� $��服务器名:��6   $��Center=�6%   �� 5         MetroTextBox#���   6H   $	��100��6o   $��Center=�6{   ��6   �� *GroupNameTxt+ �� Mode$ OneWayToSource=�6{   �� 5         AccentedSquareButtonStyle#���  67   $��4,0,0,0q�6k   $��28��6|   ��6   �� *QueryCommand+��5!      �� 5"      $ 
Horizontal=�6!   .+�5#      �� $
��None=�6    $/��)/AIC.HomePage;component/Images/search.png��6/   �� 5$      $��搜索��6$   $��Center=�6.   5%      5'      �� 5(      $��服务器管理:��6   $��Center=�6&   $��20,0,0,0q�6A   �� 5)      #���   6m   $	��100��6�   $��Center=�6�    �� 
IsReadOnly . 	 � DataContext	 6   �� 
 �� ElementName$
 table�� �� Path$ SelectedItem���6T   �� $ IP�6�   �� 5*      #���  6q   $��4,0,0,0q�6�   $��28��6�   ��6   �� *
AddCommand+��64   �� *SelectedItem+$
 table����5+      �� 5,      $ 
Horizontal=�6!   .+�5-      �� $
��None=�6    $,��&/AIC.HomePage;component/Images/add.png��6/   �� 5.      $��添加��6$   $��Center=�6.   5/      51      �� 52      #���  6r   $��4,0,0,0q�6�   $��28��6�   ��6   �� *EditCommand+��65   �� *SelectedItem+$
 table����53      �� 54      $ 
Horizontal=�6!   .+�55      �� $
��None=�6    $/��)/AIC.HomePage;component/Images/pencil.png��6/   �� 56      $��修改��6$   $��Center=�6.   57      59      �� 5:      #���  6t   $��4,0,0,0q�6�   $��28��6�   ��6   �� *DeleteCommand+��67   �� *SelectedItem+$
 table����5;      �� 5<      $ 
Horizontal=�6!   .+�5=      �� $
��None=�6    $0��*/AIC.HomePage;component/Images/delete3.png��6/   �� 5>      $��删除��6$   $��Center=�6.   5?      5A      �� 5B      #���  6u   $��4,0,0,0q�6�   $��28��6�   ��6   �� *DefaultCommand+��68   �� *SelectedItem+$
 table����5C      �� 5D      $ 
Horizontal=�6!   .+�5E      �� $
��None=�6    $-��'/AIC.HomePage;component/Images/tick.png��6/   �� 5F      $��默认��6$   $��Center=�6.   5G      5I      5J      &   System.Windows.Controls.DataGrid  5K   
   -   5          Name$ table��5K      $��1��6"   $��1��6/      AutoGenerateColumns .     FrozenColumnCount$ 1��6[      
IsReadOnly . ��5L      �� *
InfoList+   Columns 5M      0  *System.Windows.Controls.DataGridTextColumn  5N      ,  &System.Windows.Controls.DataGridColumn   Header$ ID��6%   1  +System.Windows.Controls.DataGridBoundColumn   Binding 61   �� *ID+6J     5O      $ 服务器名��6%    63   �� *Name+6N     5P      $ 服务器IP��6%    64   �� *IP+6M     5Q      $ 服务器端口号��6%    65   �� *Port+6P     5R      $ 机构名称��6%    63   �� *	Factory+6Q     5S      $ 经度��6%    61   �� *	Longitude+6Q     5T      $ 纬度��6%    61   �� *
Latitude+6P   4  .System.Windows.Controls.DataGridTemplateColumn  5U      $ 默认登录��6)      CellTemplate 5V      �� 5W      .R�5X      � .+�5Y   "   �� $��Center=�6(   $��16��6^   $��16��6i   ��6C   �� *	IsLogin+6w   5Z      5[      5]        5^      $ 服务器模式��6)    5_      �� 5`      .R�5a      � .+�5b   "   �� $��Center=�6(   $��16��6^   $��16��6i   ��6C   �� *	IsCloud+6w   5c      5d      5f        5g      $ 登录结果��6)    5h      �� 5i      .R�5j      � .+�5k   "   �� $��Center=�6(   $��16��6b   $��16��6m   ��6C   �� *LoginResult+6{   5l      5m      5o        5p      $ 权限��6%    61   �� *
Permission+6R      ColumnHeaderStyle 5r      �� 5s        MetroDataGridColumnHeader �� BasedOn# [ 6    �� 
TargetType=  7System.Windows.Controls.Primitives.DataGridColumnHeader"  6M   .�5t      ��  �� Property � ��  AccentColorBrush3 �� Value# �  63   5u      ��5w      �� 5x        MetroDataGrid# [ 6   "  5y      .�5z      ��   �@ AlternatingRowBackground  GrayBrush10# �  5{      5|      .  (System.Windows.Interactivity.Interaction   Triggers 5~      /	  )System.Windows.Interactivity.EventTrigger	  5       	  	EventName$ MouseDoubleClick��6!   .
  (System.Windows.Interactivity.TriggerBase 
  Actions. 5�      6  0System.Windows.Interactivity.InvokeCommandAction     Command 6,   �� *EditCommand+   CommandParameter 6L   �� *SelectedItem+$
 table��5�      5�      5�      5�      