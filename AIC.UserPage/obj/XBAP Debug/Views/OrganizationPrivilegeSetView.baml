   M S B A M L   `   `   `  ���� G  CAIC.UserPage, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:AIC.UserPageAIC.UserPage  T PWpf.PageNavigationControl, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullhJclr-namespace:Wpf.PageNavigationControl;assembly=Wpf.PageNavigationControlWpf.PageNavigationControl C ?AIC.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullI2clr-namespace:AIC.Core.ExCommand;assembly=AIC.CoreAIC.Core.ExCommand [;clr-namespace:AIC.Core.OrganizationModels;assembly=AIC.CoreAIC.Core.OrganizationModels 5    /AIC.UserPage.Views.OrganizationPrivilegeSetView   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     ,localclr-namespace:AIC.UserPage     c _System.Windows.Interactivity, Version=4.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity  P LPrism.Wpf, Version=6.2.0.0, Culture=neutral, PublicKeyToken=91a96d2a154366d8$prismhttp://prismlibrary.com/  UpageJclr-namespace:Wpf.PageNavigationControl;assembly=Wpf.PageNavigationControl  @command2clr-namespace:AIC.Core.ExCommand;assembly=AIC.Core  Icontrol;clr-namespace:AIC.Core.OrganizationModels;assembly=AIC.Core  !  Prism.Mvvm.ViewModelLocator    AutoWireViewModel  .    AsyncWorkUserControlStyle#���    � 	Resources  �� MergedDictionaries �� Source��  �� $4 ./AIC.Resources;component/Themes/ScrollBar.xaml?�%    .��� -    �Name$
 grid�� � RowDefinitions �� $
��Auto ��� $��* ��� $
��Auto �.+��� $��0��$��2�� �� Padding$ 2q�  AccentColorBrush2#���  ._���  �� Orientation$ 
Horizontal=�.+���   serverIP#���  $��4,0,0,0q�$��Center=��� $	��200��  VirtualisedMetroComboBox#���  $��Center=� �� 
IsEditable .  $��4,0,0,0q����� *ServerIPCategory+	 �� SelectedItem	 �� *
ServerIP+��   name#���  $��4,0,0,0q�$��Center=��� $��4,0,0,0q�  MetroTextBox#���  $	��200��$��Center=����� *
SearchName+
 �� Mode$
 OneWayToSource=���   AccentedSquareButtonStyle#���  $��4,0,0,0q����� *QueryCommand+���� $ 
Horizontal=�.+��� $
��None=�$0��*/AIC.Resources;component/Images/search.png����  
 search#���  $��Center=���   menuOrganizationManage#���  $��Center=�$��20,0,0,0q��� $��4,0,0,0q�#���  $	��200��$��Center=� �� 
IsReadOnly .  � DataContext ��  �� ElementName$ datagrid�� �� Path$ SelectedItem����� $
 Name��� #���  $��4,0,0,0q����� *
AddCommand+���� *SelectedItem+$ datagrid������ $ 
Horizontal=�.+��� $
��None=�$-��'/AIC.Resources;component/Images/add.png����  	 add#��� 	 $��Center=��� #���  $��4,0,0,0q����� *EditCommand+���� *SelectedItem+$ datagrid������ $ 
Horizontal=�.+��� $
��None=�$0��*/AIC.Resources;component/Images/pencil.png����  
 edit#��� 
 $��Center=��� #���  $��4,0,0,0q����� *DeleteCommand+���� *SelectedItem+$ datagrid������ $ 
Horizontal=�.+��� $
��None=�$1��+/AIC.Resources;component/Images/delete3.png����  
 delete#���  $��Center=�&   System.Windows.Controls.DataGrid  -     Name$ datagrid��$��1��   AutoGenerateColumns .     FrozenColumnCount$ 1��   
IsReadOnly .    ColumnWidth5  /System.Windows.Controls.DataGridLengthConverter$ * $��0,1,0,0q�   RowDetailsVisibilityMode$ VisibleWhenSelected=����� *MyOrganizationPrivilegeView+   Columns 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn   Header# �     MinWidth$	 100Q�1  +System.Windows.Controls.DataGridBoundColumn   Binding �� *Name+4  .System.Windows.Controls.DataGridTemplateColumn    checkedExpanded# �     CellTemplate �� .R��� -      RowDetailsTemplate �� .R��� #���  $��2q� �� CornerRadius$ 2��$��2q�._�R� $��treeview��  VirtualisedMetroTreeView#���  ���� *OrganizationTreeItems+$
 TwoWay=�  �� DataType?  9AIC.Core.OrganizationModels.OrganizationTreeItemViewModel �� ItemsSource F�Name �� Stretch  AccentColorBrush  �� Triggers! �� Value" �� Binding# �� 
TargetName$ �� Property% �� Value9	  3AIC.Core.OrganizationModels.DeviceTreeItemViewModel��%�  (��       *, +)(�� �    *,	 +)�� "   �� $ Children�.R��� $ 
Horizontal=�.+�F� $ IconPath��$��18��$��18��$
 Fill=�#���  ������Bnx�V��Bn4�Cќ�O�ob�L�!�!�4�C�71�ܽ�,���ܽ���K�����z@����H��р�e�!��8�obф�4�Cф�x�V��Bnx�V�4�C�Śј��d%ј�(�(��ǘ���@@(�(��ǘ���@@����08�����08�(�(����(�(�����d%�4�C�Ś� $��2,0,2,0q�.+��� $
��Auto������ *Name+$
 TwoWay=�  �� $! 0��" �� *T_Organization.Level+.a��� $# IconPath��$ � ���% �� ��:�����:����pej� �pej� ��:���2��x��2��x����Ѥ����Ѥ��2�� ����є�����'t���������������E�'t�E�'t0P�є�0P�є��E� ��E� ��������EѤ����+�Tn5��+�����+�x����+�x���E����E����x��Tn5�x��Tn5�EѤ���E�$� �E�$� �x��$� l't�T�-���\��l't�\���x��\���EѬ<R�EѬ<R�x��Ԙ� �x��Ԙ� �E�$� �E���-���������%����,�U���-�P��V�� �h�V�� P�7���-����� $# IconPath��$ � ��$% 24���� " 	  �� $ Children�.R��� $ 
Horizontal=�.+�F� $��18��$��18��$
 Fill=����	#FF87CDEE�����P�x��ݩ(�� ���� p׻\r� $�� �(4��0�	�^)�@@�5������@@�.>�A�G�!اQ���P�x�ز�ݩ������y@@�����p׻�:pΈ"@@$��pP�(4�Ql^�	�^)�@@SP=������@@���A(Ċ�!�L�c����2ز�S�U)�����y@@�� ����d�:pΈ"@@��7!P�Ѵ�� l^����� P=�d*� ��PJ� �Ê��L�c�(�� �2��� �U)\r� �� � �dѐ0�7!є5P�� Ӱ.>��� �Gd*� اQPJ� �P�x�� 6n -1Ӵ�L -1 -1��L -1 6n� -1L���L ?� 6n ?��L� ?� ?�L� ?� 6n� ?���LL� -1 6n -1� $��2,0,2,0q�.+��� $
��Auto������ *Name+$
 TwoWay=����� & �� 
TargetType"& Q�  LineTreeViewItem' �� BasedOn#' [ .��� $ �@Q�
IsSelected% �� *
IsSelected+$
 OneTime=��� $ �@Q�
IsExpanded% �� *
IsExpanded+$
 OneTime=�(   ColumnHeaderStyle( ��   MetroDataGridColumnHeader#' [ =
  7System.Windows.Controls.Primitives.DataGridColumnHeader"& 
 .��� $ � ��  AccentColorBrush3#% �  )   	CellStyle) ��   MetroDataGridCell#' [ *  $System.Windows.Controls.DataGridCell"&  *   RowStyle* ��   MetroDataGridRow#' [ )  #System.Windows.Controls.DataGridRow"&  ����   MetroDataGrid#' [ "&  .���  $ �@ AlternatingRowBackground  AccentColorBrush4#% �  .  (System.Windows.Interactivity.Interaction+   Triggers+ /  )System.Windows.Interactivity.EventTrigger  ,   	EventName$, MouseDoubleClick��.  (System.Windows.Interactivity.TriggerBase-   Actions.- 6  0System.Windows.Interactivity.InvokeCommandAction  .   Command. �� *EditCommand+/   CommandParameter/ �� *SelectedItem+$ datagrid��