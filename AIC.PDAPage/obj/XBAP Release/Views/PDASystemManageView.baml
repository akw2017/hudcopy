   M S B A M L   `   `   `  ���� C  ?AIC.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null[;clr-namespace:AIC.Core.OrganizationModels;assembly=AIC.CoreAIC.Core.OrganizationModels  F BAIC.PDAPage, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null7 clr-namespace:AIC.PDAPage.ModelsAIC.PDAPage.Models 5clr-namespace:AIC.PDAPage.ViewsAIC.PDAPage.Views ?$clr-namespace:AIC.PDAPage.ConvertersAIC.PDAPage.Converters I2clr-namespace:AIC.Core.ExCommand;assembly=AIC.CoreAIC.Core.ExCommand  +   %AIC.PDAPage.Views.PDASystemManageView   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     c _System.Windows.Interactivity, Version=4.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity  Icontrol;clr-namespace:AIC.Core.OrganizationModels;assembly=AIC.Core  ,model clr-namespace:AIC.PDAPage.Models  +localclr-namespace:AIC.PDAPage.Views  2convert$clr-namespace:AIC.PDAPage.Converters  P LPrism.Wpf, Version=6.2.0.0, Culture=neutral, PublicKeyToken=91a96d2a154366d8$prismhttp://prismlibrary.com/  @command2clr-namespace:AIC.Core.ExCommand;assembly=AIC.Core  !  Prism.Mvvm.ViewModelLocator    AutoWireViewModel  .    AsyncWorkUserControlStyle#���    � 	Resources  �� MergedDictionaries4  .AIC.PDAPage.Converters.BoolVisibilityConverter  Key-  'AIC.PDAPage.Converters.IntAdd1Converter)   #AIC.Core.ExCommand.CommandReference   Command �� 
TargetType �� Property �� Value  AccentColorBrush �� 
TargetType � 	Resources	 �� TargetProperty
 �� 
TargetName9  3System.Windows.Media.Animation.EasingDoubleKeyFrame O� KeyTime O� Value �� 
Background �� Bullet �Name J� RenderTransformOrigin A�Name �� Triggers :� RoutedEvent  	OnLoaded1 P� Property P� Value �� 
TargetName  DisabledBackgroundBrush �� Source��  ��   boolVisibilityConverter   intAdd1Converter   AddCommandReference  �� *
AddCommand+  AddRootCommandReference  �� *AddRootCommand+  SortCommandReference  �� *SortCommand+  AddOrganizationReference  �� *AddOrganizationCommand+  DeleteCommandReference  �� *DeleteCommand+  CopyCommandReference  �� *CopyCommand+  PasteCommandReference  �� *PasteCommand+  PasteRootCommandReference  �� *PasteRootCommand+  RenameCommandReference  �� *RenameCommand+  DeleteCardCommandReference  �� *DeleteCardCommand+  #ForceDeleteCardCommandReference  �� *ForceDeleteCardCommand+  !ClearRecycledCommandReference  �� *ClearRecycledCommand+  !RecoveredItemCommandReference  �� *RecoveredItemCommand+  DebugCommandReference  �� *DebugCommand+  EditDeviceCommandReference  �� *EditDeviceCommand+�� RadioButtonStyle " 	�.���  � ��
 ���� ��  � ��# �  ��  � ��$ 1q���  � ��$ Center=���  � ��$ Center=���  � �� �� " 	� ���� 	OnLoaded1 .��U� $V	 P(UIElement.RenderTransform).(TransformGroup.Children)[0].(ScaleTransform.ScaleX)�$

 grid��.\�  $ 0��$ 0Q�  $ 0:0:0.5��$ 1Q�U� $V	 P(UIElement.RenderTransform).(TransformGroup.Children)[0].(ScaleTransform.ScaleY)�$

 grid��.\�  $ 0��$ 0Q�  $ 0:0:0.5��$ 1Q�.R��� 
 ����  � $
 grid��$��24��$��24��$ 0.5,0.5&�z�V� .~��� �� �� T� .+�A� $ Ellipse_Border��#��z��#��z��$��3��A� $ 	CheckMark��$��5q�#���  ._��� #��z��#��z��#��z����.  :� $ FrameworkElement.Loaded��.[��� #��[ P�  �@d�	IsChecked$ falseY�. ��� $ 	CheckMark�� � y�$ 	Collapsed=�P�  � {� .  . ��� # �  $ Ellipse_Border�� � ���� $ Ellipse_Border�� � �� �	#40000000��  � �� �	#80000000�� $@ :/AIC.PDAPage;component/Themes/PDASystemManageTemplate.xaml?��� $4 ./AIC.Resources;component/Themes/ScrollBar.xaml?�%    .��� -   $��grdWorkbench�� � RowDefinitions �� $
��Auto ��� $��* � � ColumnDefinitions �� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $��8* ��� $��3* ��� $��5* �.+��� $��0��$��7�� �� Padding$ 2q�  AccentColorBrush2#���  ._�� .+���  �� Orientation$ 
Horizontal=�.+���   serverIP#���  $��4,0,0,0q�$��Center=��� $	��200��  VirtualisedMetroComboBox#���  $��Center=� �� 
IsEditable .  $��4,0,0,0q����� *ServerIPCategory+ �� SelectedItem �� *
ServerIP+��  	 pdaip#���  $��4,0,0,0q�$��Center=��� -    ��Name$ txtMainCardIP�� 	 MetroTextBox#��� 	 $��4,0,0,0q�$	��200��$��Center=����� *MainControlCardIPEdit+ �� Mode$ TwoWay=�  �� UpdateSourceTrigger$  PropertyChanged=�! �� ValidatesOnExceptions! . " �� ValidatesOnDataErrors" . # �� NotifyOnValidationError# . .����  
 AccentedSquareButtonStyle#��� 
 $��4,0,0,0q����� *AddCardCommand+���� $ 
Horizontal=�.+��� $
��None=�$-��'/AIC.Resources;component/Images/add.png����   add#���  $��Center=��� #��� 
 $��4,0,0,0q����� *DeleteCardCommand+���� $ 
Horizontal=�.+��� $
��None=�$1��+/AIC.Resources;component/Images/delete3.png����  
 delete#���  $��Center=��� #��� 
 $��4,0,0,0q����� *DownLoadCardCommand+���� $ 
Horizontal=�.+��� $
��None=�$.��(/AIC.Resources;component/Images/tick.png����   senddown#���  $��Center=��� #��� 
 $��4,0,0,0q����� *SaveCardCommand+���� $ 
Horizontal=�.+��� $
��None=�$2��,/AIC.Resources;component/Images/filesave.png����   save#���  $��Center=���   transmissionIdentifier#���  $��4,0,0,0q�$��Center=��� #��� 	 $��4,0,0,0q�$	��200��$��Center=����� *TransmissionCardIdentifier+�� #��� 
 $��4,0,0,0q����� *AddTransmissionCardCommand+���� $ 
Horizontal=�.+��� $
��None=�$-��'/AIC.Resources;component/Images/add.png���� #���  $��Center=��� #��� 
 $��4,0,0,0q����� *DeleteTransmissionCardCommand+���� $ 
Horizontal=�.+��� $
��None=�$1��+/AIC.Resources;component/Images/delete3.png���� #���  $��Center=��� $ 
Horizontal=�$��Right=�.+�	� $	��5,0q�$��单槽显示��$ d� 	IsChecked$
$ TrueY�  RadioButtonStyle#��[ 	� $��多槽显示��#��[ $ �� *IsMultiSelected+� -   $ grid1��$��0��$��1��$	��0,5q�.+��  �� $��3* ��� $
��Auto ��� $��1* �.+�R� -   $��treeview��  VirtualisedMetroTreeView#���  ���� *OrganizationTreeItems+% J� 	AllowDrop% �� *AllowDropTree+& � ContextMenu& �� .@�m� $��新建机构��  AddRootCommandReference' m� Command#' [ ( m� CommandParameter$( -1��m� $��粘贴机构��  PasteRootCommandReference#' [ $( -1�� ) �� DataType?   9AIC.Core.OrganizationModels.OrganizationTreeItemViewModel* �� ItemsSource+ �� Path  AddOrganizationReference  AddCommandReference  SortCommandReference  DeleteCommandReference  CopyCommandReference  PasteCommandReference  RenameCommandReference  DebugCommandReference, F�Name- �� Stretch  boolVisibilityConverter. �� 	Converter/ �� ConverterParameter0 J� InputBindings
1 �� Key2 �� CommandParameter3 �� Command4 �� RelativeSource5 �� Mode6 �� AncestorType7 �� Triggers8 �� Binding9 �� ElementName: �� Value.  (System.Windows.Interactivity.Interaction;   Triggers/  )System.Windows.Interactivity.EventTrigger<   	EventName.	  (System.Windows.Interactivity.TriggerBase= 	  Actions6
  0System.Windows.Interactivity.InvokeCommandAction> 
  CommandParameter? 
  Command@ �� Triggers9   3AIC.Core.OrganizationModels.DeviceTreeItemViewModel  EditDeviceCommandReference7   1AIC.Core.OrganizationModels.ItemTreeItemViewModel9   3AIC.Core.OrganizationModels.DivFreTreeItemViewModel��%,  (��       *, +)7  7  7  7  7  7  7  7  7  7  (�� �    *, +)7  7  7  7  7  7  7  7  7  7  (�� �    *, +)7  7  7  7  7  7  7  7  7  (�� M    *, +)7  7  7  7  7  7  7  7  �� ")  * �� $+ Children�.R��� $ 
Horizontal=�& �� .@�m� $��添加机构��8'   ( �� m� $��添加设备��8'  ( �� m� $��排序��8'  ( �� m� $��删除机构��8'  ( �� m� $��复制��8'  ( �� m� $��粘贴��8'  ( �� m� $��	重命名��8'  ( �� m� $��调试信息��8'  $y�Visible=�( �� .+�F� $, IconPath��$��18��$��18��$
- Fill=�#���  ������Bnx�V��Bn4�Cќ�O�ob�L�!�!�4�C�71�ܽ�,���ܽ���K�����z@����H��р�e�!��8�obф�4�Cф�x�V��Bnx�V�4�C�Śј��d%ј�(�(��ǘ���@@(�(��ǘ���@@����08�����08�(�(����(�(�����d%�4�C�Ś� $��2,0,2,0q�.+��� $
��Auto������ *Name+$ TwoWay=�y��� *
EditMode+8.  $/ false���� $ renametextbox��
������ 
�������$
��Auto������ *Name+$ TwoWay=�y��� *
EditMode+8. 	 $
/ true��.��0 �� $1 Enter��$2 Enter��3 �� *DataContext.EndRenameCommand+4 �� $5 FindAncestor=�$6 UserControlL��� $	1 Esc��$	2 Esc��3 �� *DataContext.EndRenameCommand+4 �� $5 FindAncestor=�$6 UserControlL�.������ " ��7 �� 8 �� *
Visibility+$9 renametextbox��: Visible.a���  �@+�FocusedElement �� $9 renametextbox��.��;   $< 	LostFocus��.= 
  $> 	LostFocus��? �� *DataContext.EndRenameCommand+4 �� $5 FindAncestor=�$6 UserControlL�.��@ �� $: 0��8 �� *T_Organization.Level+.a��� $ IconPath�� � ��� �� ��:�����:����pej� �pej� ��:���2��x��2��x����Ѥ����Ѥ��2�� ����є�����'t���������������E�'t�E�'t0P�є�0P�є��E� ��E� ��������EѤ����+�Tn5��+�����+�x����+�x���E����E����x��Tn5�x��Tn5�EѤ���E�$� �E�$� �x��$� l't�T�-���\��l't�\���x��\���EѬ<R�EѬ<R�x��Ԙ� �x��Ԙ� �E�$� �E���-���������%����,�U���-�P��V�� �h�V�� P�7���-����� $ IconPath�� � ��$ 24���� ")  * �� $+ Children�.R��� $ 
Horizontal=�& �� .@�m� $��添加测点��8'   ( �� m� $��排序��8'  ( �� m� $��删除设备��8'  ( �� m� $��编辑设备��8'  ( �� m� $��复制��8'  ( �� m� $��粘贴��8'  ( �� m� $��	重命名��8'  ( �� m� $��调试信息��8'  $y�Visible=�( �� .+�F� $��18��$��18��$
- Fill=�#���  �����P�x��ݩ(�� ���� p׻\r� $�� �(4��0�	�^)�@@�5������@@�.>�A�G�!اQ���P�x�ز�ݩ������y@@�����p׻�:pΈ"@@$��pP�(4�Ql^�	�^)�@@SP=������@@���A(Ċ�!�L�c����2ز�S�U)�����y@@�� ����d�:pΈ"@@��7!P�Ѵ�� l^����� P=�d*� ��PJ� �Ê��L�c�(�� �2��� �U)\r� �� � �dѐ0�7!є5P�� Ӱ.>��� �Gd*� اQPJ� �P�x�� 6n -1Ӵ�L -1 -1��L -1 6n� -1L���L ?� 6n ?��L� ?� ?�L� ?� 6n� ?���LL� -1 6n -1� $��2,0,2,0q�.+��� $
��Auto������ *Name+$ TwoWay=�y��� *
EditMode+8.  $/ false���� $ renametextbox��
������ 
�������$
��Auto������ *Name+$ TwoWay=�y��� *
EditMode+8. 	 $
/ true��.��0 �� $1 Enter��$2 Enter��3 �� *DataContext.EndRenameCommand+4 �� $5 FindAncestor=�$6 UserControlL��� $	1 Esc��$	2 Esc��3 �� *DataContext.EndRenameCommand+4 �� $5 FindAncestor=�$6 UserControlL�.������ " ��7 �� 8 �� *
Visibility+$9 renametextbox��: Visible.a���  �@+�FocusedElement �� $9 renametextbox��.��;   $< 	LostFocus��.= 
  $> 	LostFocus��? �� *DataContext.EndRenameCommand+4 �� $5 FindAncestor=�$6 UserControlL�.���� ")  * �� $+ Children�.R��� $ 
Horizontal=�& �� .@�m� $��添加分频��8'   ( �� m� $��排序��8'  ( �� m� $��删除测点��8'  ( �� m� $��复制��8'  ( �� m� $��粘贴��8'  ( �� m� $��	重命名��8'  ( �� m� $��调试信息��8'  $y�Visible=�( �� .+�F� $��18��$��18��$
- Fill=�#���  ������ا  � �@]�  � �@]� ���@�4���@�4 Q%��ا  Q%��ا  � ���� @��р�� ���� -1��^�@������@�4@]� �@�4@fр������ -1���р�� @��� $��2,0,2,0q�.+��� $
��Auto������ *Name+$ TwoWay=�y��� *
EditMode+8.  $/ false���� $ renametextbox��
������ 
�������$
��Auto������ *Name+$ TwoWay=�y��� *
EditMode+8.  $
/ true��.��0 �� $1 Enter��$2 Enter��3 �� *DataContext.EndRenameCommand+4 �� $5 FindAncestor=�$6 UserControlL��� $	1 Esc��$	2 Esc��3 �� *DataContext.EndRenameCommand+4 �� $5 FindAncestor=�$6 UserControlL�.������ " ��7 �� 8 �� *
Visibility+$9 renametextbox��: Visible.a���  �@+�FocusedElement �� $9 renametextbox��.��;   $< 	LostFocus��.= 
  $> 	LostFocus��? �� *DataContext.EndRenameCommand+4 �� $5 FindAncestor=�$6 UserControlL�.���� $��2,0,2,0q����� *Error+F� $, 
StatusPath��$��2��$��16��$��16��$
- Fill=�
��� � ������P�� ��^�TR'�� 0@��^�TR'�� 0@�71�TR'�� 0@��� ,�*�!� �Bn�!� �ԯ��!� �X���� �X��71�|���71�|����f���� �Bn��� �8D��� ��!���!�71ф�!��^�����^����d%�P�� �d%�P�� ��^@ �� $
: True��8 �� *
IsPaired+.a��� $ 
StatusPath�� � ��� ��� ��^��-� 0@��^��-� 0@�71��-� 0@��� ��*�!� �Bn�!� �8���!� �Y���� �Y��71єY���^Ѥ���^Ѥ��d%Ѵ�� �d%Ѵ�� ��^�����71������g���� �Bn��� ӜD��� ��!���!�71���!��^������^�����71�� $ 
StatusPath�� � ��
 �  ���� ")  * �� $+ Children�.R��� $ 
Horizontal=�& �� .@�m� $��排序��8'   ( �� m� $��删除分频��8'  ( �� m� $��复制��8'  ( �� m� $��粘贴��8'  ( �� m� $��	重命名��8'  ( �� m� $��调试信息��8'  $y�Visible=�( �� .+�F� $��18��$��18��$
- Fill=�#���  ����� $�  Q%�v��  Q%�v�� ���р�� ���� H� $� � H� � р�C � р�C��р���� $�  H�� $�  Q%������ р� $� � Q% $� � Q%��� р���� � �  H�� � ��р�� ��р��  H�� �  H�� $��2,0,2,0q�.+��� $
��Auto������ *Name+$ TwoWay=�y��� *
EditMode+8.  $/ false���� $ renametextbox��
������ 
�������$
��Auto������ *Name+$ TwoWay=�y��� *
EditMode+8.  $
/ true��.��0 �� $1 Enter��$2 Enter��3 �� *DataContext.EndRenameCommand+4 �� $5 FindAncestor=�$6 UserControlL��� $	1 Esc��$	2 Esc��3 �� *DataContext.EndRenameCommand+4 �� $5 FindAncestor=�$6 UserControlL�.������ " ��7 �� 8 �� *
Visibility+$9 renametextbox��: Visible.a���  �@+�FocusedElement �� $9 renametextbox��.��;   $< 	LostFocus��.= 
  $> 	LostFocus��? �� *DataContext.EndRenameCommand+4 �� $5 FindAncestor=�$6 UserControlL�.������ " Q�  MetroTreeViewItemA �� BasedOn#A [ .���  �@Q�
IsSelected �� *
IsSelected+$ TwoWay=���  �@Q�
IsExpanded �� *
IsExpanded+$ TwoWay=�;   $< SelectedItemChanged��.= 
  ? �� *SelectedTreeChangedComamnd+> �� *SelectedItem+$9 treeview��  $!< PreviewMouseRightButtonDown��.= .   (AIC.Core.ExCommand.ExInvokeCommandAction  B   CommandB �� *PreviewMouseRightComamnd+C   CommandParameterC �� *SelectedItem+$9 treeview���� $��1��$��3��#���  $��Stretch=�$��Center=�R� -   $��	treeview2��$��2��#���  ���� *RecycledTreeItems+  ! RecoveredItemCommandReference��%~  (��       *, +)(�� 9    *, +)7  7  �� ")  * �� $+ Children�.R��� $ 
Horizontal=�.+�F� $, IconPath��$��18��$��18��$
- Fill=�#���  �����4��pej��pej�ltpej;t��;t��шopt��ӈopt���6upej4��pej�(�T�-��hT�-Ө�nT�-�;�H|?�;�(FӜ;���L��n@Q^�h@Q^�(�@Q^�X�@Q^ o��L o(F� oH|?X�T�-(�T�-��˦ ���� ��7� �8�D��8�H	�����n5���X�BP�|�ML��|�MѴ��|�MӰ��|�M�˦X�B�˦�n5�Ȣ�H	��Ȣ�D���s� ��˦ ����2��`��E��Z��EѸP�2����2��+2�Ѽ�+�E�D�[�E�D�[2�Ѽ�+2��HZ�2��0P��EѠ���E���2��HZ�2�� $��2,0,2,0q�.+��� $
��Auto������ *Name+$ TwoWay=��� $ renametextbox��
������ 
�������$
��Auto��$y�	Collapsed=����� *Name+$ TwoWay=��� ")  * �� $+ Children�.R��� $ 
Horizontal=�& �� .@�m� $��恢复到所选测点��8'   ( �� m� $��调试信息��8'  $y�Visible=�( �� .+�F� $��18��$��18��$
- Fill=�#���  ������ا  � �@]�  � �@]� ���@�4���@�4 Q%��ا  Q%��ا  � ���� @��р�� ���� -1��^�@������@�4@]� �@�4@fр������ -1���р�� @��� $��2,0,2,0q�.+��� $
��Auto������ *Name+$ TwoWay=��� $ renametextbox��
������ 
�������$
��Auto��$y�	Collapsed=����� *Name+$ TwoWay=����� " Q�#A [ .���  �@Q�
IsSelected �� *
IsSelected+$ TwoWay=���  �@Q�
IsExpanded �� *
IsExpanded+$ TwoWay=�;   $< SelectedItemChanged��.= 
  ? �� *SelectedTreeChangedComamnd+> �� *SelectedItem+$9 treeview��  $!< PreviewMouseRightButtonDown��.=   B �� *PreviewMouseRightComamnd+C �� $9 treeview��.@�   �� -   D ��Name$D gsSplitterr��$��1��$��1��$��3��#���  $��Center=�$��Stretch=�   GridSplitterStyleExpend#��[  � $��2��$��1��.+�R� -   E R�Name$E 
serverview��#���  ���� *ServerTreeItems+ 9   3AIC.Core.OrganizationModels.ServerTreeItemViewModel;   5AIC.Core.OrganizationModels.MainCardTreeItemViewModel ! DeleteCardCommandReference #" ForceDeleteCardCommandReferenceC   =AIC.Core.OrganizationModels.WireMatchingCardTreeItemViewModel # intAdd1ConverterF   @AIC.Core.OrganizationModels.WirelessReceiveCardTreeItemViewModelC   =AIC.Core.OrganizationModels.TransmissionCardTreeItemViewModel7   1AIC.Core.OrganizationModels.SlotTreeItemViewModel:   4AIC.Core.OrganizationModels.ChannelTreeItemViewModelF ��Name��%)  (��       *, +)(��     *, +)7 ! 7 " (�� �    *, +)7 # (�� �    *, +)(�� 
    *, +)(��     *, +)7 # (�� �    *, +)7 # �� ")  * �� $+ Children�.R��� $ 
Horizontal=�.+�F� $��18��$��18��$
- Fill=�#���  �������C��!Ӏ���!�;G��@�;G��f��;G`���;G���������C���Ӏ�����@o@��@o@`��@o@��f�@o@��@�����!��C��!��;G !��;GTG��@�e��C@�eӀ��@�e@o@TG@o@ !�@o@����@o@�t�����2
��C�2
����2
�G}��;Gܗ���;G !��;G�����;G���� l���C l�Ӏ�� l�@o@��@o@����@o@`4<�@o@� b����ހ��C�ހ����ހ�G�(b�;G8C<��;G�����;G�m��;G�Y4���S��C�SӀ���S@o@�Y4@o@�m�@o@ ��@o@T�����@����C@�����@���G����;G����;G�m�� $��	服务器��$��Center=�$��2,0,0,0q��� $��:��$��Center=��� $��Center=����� *
ServerIP+�� ")  * �� $+ Children�.R��� $ 
Horizontal=�& �� .@�m� $��删除数采器��8'   ( �� m� $��强制删除数采器��8'  ( �� .+�F� $��18��$��18��$
- Fill=�#���  ����� E@f����@fӐ��@f l�H l�`<� l�`�x� l�ك������������Ѡ�������7����@��(q�@���ү�@���\��@��PQП��2
 ���2
� E�2
�p:�2
 -1PQ -1�\�� -1`<� -1�Gp:@f E@f�fffff�@@@����B@��Ӑ�M@�� ~V�b� ~V`W�� ~V�np� ~VPc{��M�D��B�D��fffff�@@�D��p)��D� H�Pc{ H��np� H�`W�� H��b�p)�@��fffff�@@@���� $��主板:��$��Center=�$��2,0,0,0q��� $��Center=����� *MainControlCardIP+$ TwoWay=��� ")  * �� $+ Children�.R��� $ 
Horizontal=�.+�F� $��18��$��18��$
- Fill=�#���  �������C8�� ��}pej��}��р�C��C���	��ј�	pejр�C8�� ��}�x���}�Eр�C,����	�E���	�x��t�H��р�C�Eь?����}�x���}�Z���}l'tр�C������	l't���	�Z��t�H��р�Cl't�(?����}�Z��� $��板卡��$��Center=�$��2,0,0,0q��� $��Center=����� *	CardNum+8.   �� $��:��$��Center=��� $��Center=����� *Name+�� ")  * �� $+ Children�.R��� $ 
Horizontal=�.+�F� $��18��$��18��$
- Fill=�#���  �������CW���;�W��S��z�G@@�S���C��S��;��;��S���C�S���z�G@@�S�W��;�W���CSW��z�G@@�z�G@@W���CW����Ch�� �,��h�� ���Գ������CӘ��,��,�������C����Գ����h�� ,��h�� ��C�h�� Գ�Գ�h�� ��Ch�� ���C�:�H��:�:H��:��Cӄ:���H�|�M��C|�M�T��|�M|�M���|�M��C�|�MH�����:��C�:�� $��无线接收板��$��Center=�$��2,0,0,0q��� ")  * �� $+ Children�.R��� $ 
Horizontal=�.+�F� $��18��$��18��$
- Fill=�#���  �����+��!��£��!�|sx�2���$��л�B���Ѽ�+@�e�0Y���D�T�<R$���<R2�Ӭ<R|sx(����!��+��!�+�˦��P
�˦<F�4��<F�2��<F����P
(���+(��0M(��h���h2���h4��0M�˦��+�˦�� $��	传输卡��$��Center=�$��2,0,0,0q��� $��:��$��Center=��� $��Center=����� *SlaveIdentifier+�� ")  * �� $+ Children�.R��� $ 
Horizontal=�.+�F� $��16��$��16��$
- Fill=�#���  �������!��^������^���������!�����!��^���!�������������ހ���!�ހ���!�������^�@�e��^�@�e������������^���!@������@������@�(���!@�(���!@��������@�e���@�e�ހ����ހ��������@���@�e@���@�e@�(���@�(���@���� $	��槽��$��Center=�$��2,0,0,0q��� $��Center=����� *	SlotNum+8.   �� ")  * �� $+ Children�.R��� $ 
Horizontal=�.+�F� $��18��$��18��$
- Fill=�#���  ������¦,P� Q45DH�}8@@Q��@H�}8@@ѬZ�,P� �8 LAI�гY���@@	 �гY���@@�j#Q�w� ��+e*@@��w� ��ј= t�h�\�x#l�8 L�?јA� ɕ� ?X���BX��X� ɕѤ�:�?Ѽ�H�mѼ�Drjф����Q�����+e*@@рƄ�j#рƄ	 Ѥ�:@�Gр¦,P� �� $��通道��$��Center=�$��2,0,0,0q��� $��Center=����� *CHNum+8.   �� $F BindBtn��$��24��$��24������ 4 �� *FindAncestor+"6 R�$$+ DataContext.BindChannelCommand����� .��F� $, 
StatusPath��$��18��$��18��$
- Fill=�
��� � ������P�� ��^�TR'�� 0@��^�TR'�� 0@�71�TR'�� 0@��� ,�*�!� �Bn�!� �ԯ��!� �X���� �X��71�|���71�|����f���� �Bn��� �8D��� ��!���!�71ф�!��^�����^����d%�P�� �d%�P�� ��^@ �� $
: True��8 �� *
IsPaired+.a��� $ 
StatusPath�� � ��� ��� ��^��-� 0@��^��-� 0@�71��-� 0@��� ��*�!� �Bn�!� �8���!� �Y���� �Y��71єY���^Ѥ���^Ѥ��d%Ѵ�� �d%Ѵ�� ��^�����71������g���� �Bn��� ӜD��� ��!���!�71���!��^������^�����71�� $ 
StatusPath�� � ��
 �  ������ " Q�#A [ .���  �@Q�
IsSelected �� *
IsSelected+$ TwoWay=���  �@Q�
IsExpanded �� *
IsExpanded+$ TwoWay=�;   $< SelectedItemChanged��.= 
  ? �� *SelectedServerChangedComamnd+> �� *SelectedItem+$9 
serverview��  $!< PreviewMouseRightButtonDown��.=   B �� *PreviewMouseRightComamnd+C �� $9 treeview���� $��3��$��1��$��3��#���  $��Center=�$��Stretch=�� $��4��$��1�� �� $
��Auto ��� $��* � �� $
��Auto ��� $��* �.+��� -   G ��Name$G listMainCard��$��0�� $ UnSelectedListViewItem#��[$ $��2������ *
MainCard+���� .R�� ���� *ActualWidth+4 �� "6 ��;   $< MouseDoubleClick��.= 
  ? �� *CardEditCommand+> �� *SelectedItem+$9 listMainCard��  $< SelectionChanged��.= 
  ? �� *CardChangedCommand+> �� *SelectedItem+$9 listMainCard���� -	   $G listWirelessReceiveCard��$��0��$��1��#��[$ ���� *WirelessReceiveCard+���� .R�� ���� *ActualWidth+4 �� "6 ��;   $< MouseDoubleClick��.= 
  ? �� *CardEditCommand+> �� *SelectedItem+$9 listWirelessReceiveCard��  $< SelectionChanged��.= 
  ? �� *CardChangedCommand+> �� *SelectedItem+$9 listWirelessReceiveCard���� -
   $G listCard��$��1��$��0��$��2�� % ListViewItemStyle#��[% $��Disabled=� �� *SelectedCard+���� *Cards+���� .R�� ���� *ActualWidth+4 �� "6 ��;   $< MouseDoubleClick��.= 
  ? �� *CardEditCommand+> �� *SelectedItem+$9 listCard��  $< SelectionChanged��.= 
  ? �� *CardChangedCommand+> �� *SelectedItem+$9 listCard���� -   $G listSlot��$��5��$��1��#��[%  �� *SelectedSlot+���� *Slots+;   $< MouseDoubleClick��.= 
  ? �� *SlotEditCommand+> �� *SelectedItem+$9 listSlot��  $< SelectionChanged��.= 
  ? �� *SlotChangedComamnd+> �� *SelectedItem+$9 listSlot���� -   $G listChannel��$��6��$��1��H �� SelectionMode$H Single=�#��[%  �� *SelectedChannel+���� *
Channels+% �� *AllowDropList+;   $< MouseDoubleClick��.= 
  ? �� *ChannelEditCommand+> �� *SelectedItem+$9 listChannel��  $< SelectionChanged��.= 
  ? �� *ChannelChangedCommand+> �� *SelectedItem+$9 listChannel��