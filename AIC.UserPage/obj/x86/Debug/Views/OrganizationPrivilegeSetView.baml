   M S B A M L   `   `   `  ����G  CAIC.UserPage, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:AIC.UserPageAIC.UserPage  5      T PWpf.PageNavigationControl, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullhJclr-namespace:Wpf.PageNavigationControl;assembly=Wpf.PageNavigationControlWpf.PageNavigationControl 5	      C ?AIC.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullI2clr-namespace:AIC.Core.ExCommand;assembly=AIC.CoreAIC.Core.ExCommand 5
      [;clr-namespace:AIC.Core.OrganizationModels;assembly=AIC.CoreAIC.Core.OrganizationModels 5      5    /AIC.UserPage.Views.OrganizationPrivilegeSetView   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      ,localclr-namespace:AIC.UserPage     5      c _System.Windows.Interactivity, Version=4.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity  5      P LPrism.Wpf, Version=6.2.0.0, Culture=neutral, PublicKeyToken=91a96d2a154366d8$prismhttp://prismlibrary.com/  5      UpageJclr-namespace:Wpf.PageNavigationControl;assembly=Wpf.PageNavigationControl  5	      @command2clr-namespace:AIC.Core.ExCommand;assembly=AIC.Core  5
      Icontrol;clr-namespace:AIC.Core.OrganizationModels;assembly=AIC.Core  5      !  Prism.Mvvm.ViewModelLocator    AutoWireViewModel  .    AsyncWorkUserControlStyle#���   5       � 	Resources 5       �� MergedDictionaries �� Source�� 5   
    5      �� 5      $4 ./AIC.Resources;component/Themes/ScrollBar.xaml?�6%   %    5      .��5      � -   5         �Name$
 grid��5       � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��* �6   �� 5      $
��Auto �6   .+�5   
   �� $��0��6   $��2��6    �� Padding$ 2q�6.     AccentColorBrush2#���  6:   ._�5      ��  �� Orientation$ 
Horizontal=�6   .+�5       ��   serverIP#���  6   $��4,0,0,0q�6>   $��Center=�6O   �� 5!      $	��200��6     VirtualisedMetroComboBox#���  6r   $��Center=�6�    �� 
IsEditable .  $��4,0,0,0q�6�   ��6'   �� *ServerIPCategory+	 �� SelectedItem	 6P   �� *
ServerIP+6�   �� 5"        name#���  6   $��4,0,0,0q�6:   $��Center=�6K   �� 5#      $��4,0,0,0q�6K     MetroTextBox#���  6\   $	��200��6�   $��Center=�6�   ��6   �� *
SearchName+
 �� Mode$
 OneWayToSource=�6�   �� 5$        AccentedSquareButtonStyle#���  6;   $��4,0,0,0q�6o   ��6   �� *QueryCommand+��5%      �� 5&      $ 
Horizontal=�6%   .+�5'      �� $
��None=�6$   $0��*/AIC.Resources;component/Images/search.png��63   �� 5(       
 search#���  6(   $��Center=�6H   5)      5+      �� 5,        menuOrganizationManage#���  6   $��Center=�6L   $��20,0,0,0q�6g   �� 5-      $��4,0,0,0q�6u   #���  6�   $	��200��6�   $��Center=�6�    �� 
IsReadOnly .  � DataContext 6   ��  �� ElementName$ datagrid�� �� Path$ SelectedItem���6Z   �� $
 Name�6�   �� 5.      #���  6x   $��4,0,0,0q�6�   ��6   �� *
AddCommand+��68   �� *SelectedItem+$ datagrid����5/      �� 50      $ 
Horizontal=�6%   .+�51      �� $
��None=�6$   $-��'/AIC.Resources;component/Images/add.png��63   �� 52       	 add#��� 	 6(   $��Center=�6E   53      55      �� 56      #���  6y   $��4,0,0,0q�6�   ��6   �� *EditCommand+��69   �� *SelectedItem+$ datagrid����57      �� 58      $ 
Horizontal=�6%   .+�59      �� $
��None=�6$   $0��*/AIC.Resources;component/Images/pencil.png��63   �� 5:       
 edit#��� 
 6(   $��Center=�6F   5;      5=      �� 5>      #���  6{   $��4,0,0,0q�6�   ��6   �� *DeleteCommand+��6;   �� *SelectedItem+$ datagrid����5?      �� 5@      $ 
Horizontal=�6%   .+�5A      �� $
��None=�6$   $1��+/AIC.Resources;component/Images/delete3.png��63   �� 5B       
 delete#���  6(   $��Center=�6H   5C      5E      5H      5I      &   System.Windows.Controls.DataGrid  5K   
   -   5          Name$ datagrid��5K      $��1��6%      AutoGenerateColumns .     FrozenColumnCount$ 1��6N      
IsReadOnly .    ColumnWidth5  /System.Windows.Controls.DataGridLengthConverter$ * 6v   $��0,1,0,0q�6�      RowDetailsVisibilityMode$ VisibleWhenSelected=�5L   G   ��6   �� *MyOrganizationPrivilegeView+   Columns 5M      0  *System.Windows.Controls.DataGridTextColumn  5O      ,  &System.Windows.Controls.DataGridColumn   Header# �  6%      MinWidth$	 100Q�6^   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6E   �� *Name+6p   4  .System.Windows.Controls.DataGridTemplateColumn  5P        checkedExpanded# �  6)      CellTemplate 5Q      �� 5R      .R�5S      �� -   5        5S   @   5T      5V         RowDetailsTemplate 5X      �� 5Y      .R�5Z      �� #���  6   $��2q�6O    �� CornerRadius$ 2��6c   $��2q�6t   ._�5[      R� $��treeview��6#     VirtualisedMetroTreeView#���  5]      ��5\      �� *OrganizationTreeItems+$
 TwoWay=� 5^       �� DataType?  9AIC.Core.OrganizationModels.OrganizationTreeItemViewModel �� ItemsSource F�Name �� Stretch  AccentColorBrush  �� Triggers! �� Value" �� Binding# �� 
TargetName$ �� Property% �� Value9	  3AIC.Core.OrganizationModels.DeviceTreeItemViewModel��%Y  (��       5_   ;   *, +)(�� �    5m   ;   *,	 +)�� 5_   "   "  6;    6u   �� $ Children�.R�5`   &   �� $ 
Horizontal=�61   .+�5a   *   F� $ IconPath��6/   $��18��6A   $��18��6L   $
 Fill=�6X   #���  6g   ������Bnx�V��Bn4�Cќ�O�ob�L�!�!�4�C�71�ܽ�,���ܽ���K�����z@����H��р�e�!��8�obф�4�Cф�x�V��Bnx�V�4�C�Śј��d%ј�(�(��ǘ���@@(�(��ǘ���@@����08�����08�(�(����(�(�����d%�4�C�Ś6�   � 5b   *   $��2,0,2,0q�6/   .+�5c   .   �� $
��Auto��6[   ��68   �� *Name+$
 TwoWay=�6[   5d   +   5e   '     5f   &   �� 5g   *   $! 0��6_   " 66   �� *T_Organization.Level+.a�5h   .   �� $# IconPath��65   $ � ���% �� ��:�����:����pej� �pej� ��:���2��x��2��x����Ѥ����Ѥ��2�� ����є�����'t���������������E�'t�E�'t0P�є�0P�є��E� ��E� ��������EѤ����+�Tn5��+�����+�x����+�x���E����E����x��Tn5�x��Tn5�EѤ���E�$� �E�$� �x��$� l't�T�-���\��l't�\���x��\���EѬ<R�EѬ<R�x��Ԙ� �x��Ԙ� �E�$� �E���-���������%����,�U���-�P��V�� �h�V�� P�7���-���6[   �� 5i   .   $# IconPath��65   $ � ��$% 24��6\   5j   +   5l   #   �� 5m   "   " 	 6;    6o   �� $ Children�.R�5n   &   �� $ 
Horizontal=�61   .+�5o   *   F� $��18��6/   $��18��6:   $
 Fill=�6F   ���	#FF87CDEE�����P�x��ݩ(�� ���� p׻\r� $�� �(4��0�	�^)�@@�5������@@�.>�A�G�!اQ���P�x�ز�ݩ������y@@�����p׻�:pΈ"@@$��pP�(4�Ql^�	�^)�@@SP=������@@���A(Ċ�!�L�c����2ز�S�U)�����y@@�� ����d�:pΈ"@@��7!P�Ѵ�� l^����� P=�d*� ��PJ� �Ê��L�c�(�� �2��� �U)\r� �� � �dѐ0�7!є5P�� Ӱ.>��� �Gd*� اQPJ� �P�x�� 6n -1Ӵ�L -1 -1��L -1 6n� -1L���L ?� 6n ?��L� ?� ?�L� ?� 6n� ?���LL� -1 6n -16f   � 5p   *   $��2,0,2,0q�6/   .+�5q   .   �� $
��Auto��6[   ��68   �� *Name+$
 TwoWay=�6[   5r   +   5s   '   5t   #   5u      ��5v      �� 5w   "   & �� 
TargetType"& Q�6(     LineTreeViewItem' �� BasedOn#' [ 6B   .�5x   &   �� $ �@Q�
IsSelected% 6C   �� *
IsSelected+$
 OneTime=��� 5y   &   $ �@Q�
IsExpanded% 6C   �� *
IsExpanded+$
 OneTime=�5z   #   5}      5~      5      (   ColumnHeaderStyle( 5�      �� 5�        MetroDataGridColumnHeader#' [ 6   =
  7System.Windows.Controls.Primitives.DataGridColumnHeader"& 
 6M   .�5�      �� $ � ��  AccentColorBrush3#% �  63   5�      )   	CellStyle) 5�      �� 5�        MetroDataGridCell#' [ 6   *  $System.Windows.Controls.DataGridCell"&  5�      5�      *   RowStyle* 5�      �� 5�        MetroDataGridRow#' [ 6   )  #System.Windows.Controls.DataGridRow"&  5�      5�      ��5�      �� 5�        MetroDataGrid#' [ 6   "&  5�      .�5�      ��  $ �@ AlternatingRowBackground  AccentColorBrush4#% �  5�      5�      .  (System.Windows.Interactivity.Interaction+   Triggers+ 5�      /  )System.Windows.Interactivity.EventTrigger  5�      ,   	EventName$, MouseDoubleClick��6!   .  (System.Windows.Interactivity.TriggerBase-   Actions.- 5�      6  0System.Windows.Interactivity.InvokeCommandAction  .   Command. 6,   �� *EditCommand+/   CommandParameter/ 6L   �� *SelectedItem+$ datagrid��5�      5�      5�      5�      