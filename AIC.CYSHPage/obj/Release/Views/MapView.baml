   M S B A M L   `   `   `  ���� C  ?AIC.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null[;clr-namespace:AIC.Core.OrganizationModels;assembly=AIC.CoreAIC.Core.OrganizationModels  G CAIC.HomePage, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullA%clr-namespace:AIC.HomePage.ConvertersAIC.HomePage.Converters +clr-namespace:AIC.HomePageAIC.HomePage     AIC.HomePage.Views.MapView   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     c _System.Windows.Interactivity, Version=4.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity  Icontrol;clr-namespace:AIC.Core.OrganizationModels;assembly=AIC.Core  3convert%clr-namespace:AIC.HomePage.Converters  P LPrism.Wpf, Version=6.2.0.0, Culture=neutral, PublicKeyToken=91a96d2a154366d8$prismhttp://prismlibrary.com/  &localclr-namespace:AIC.HomePage  !  Prism.Mvvm.ViewModelLocator    AutoWireViewModel  .  � 	Resources    CategoryMenu  DefaultMenuItem �� Path m� Command m� CommandParameter �� RelativeSource �� Mode �� AncestorType m� Icon�� %�   &	        7  �� ���� *	MenuItems+���� .R�m� 8��  ���� $ Header� �� $ Command� ��  �� $ FindAncestor=�$ TreeViewL�$ SelectedItem� �� ���� $
 Icon�.��� 	 � ColumnDefinitions	 �� $
��Auto ��� $
��Auto ��� $��* �.+�R� -   $��treeview��$��0��  VirtualisedMetroTreeView#���  ���� *OrganizationTreeItems+ 
 �� DataType?   9AIC.Core.OrganizationModels.OrganizationTreeItemViewModel �� ItemsSource �� Orientation F�Name �� Stretch  AccentColorBrush ��Name �� Mode �� Triggers �� Value �� Binding �� 
TargetName �� Property �� Value9   3AIC.Core.OrganizationModels.DeviceTreeItemViewModel7   1AIC.Core.OrganizationModels.ItemTreeItemViewModel9   3AIC.Core.OrganizationModels.DivFreTreeItemViewModel��%Q  (��       *, +)(�� �    *, +)(�� 
    *, +)(�� L    *, +)�� "
   �� $ Children�.R��� $ 
Horizontal=�.+�F� $ IconPath��$��18��$��18��$
 Fill=�#���  ������Bnx�V��Bn4�Cќ�O�ob�L�!�!�4�C�71�ܽ�,���ܽ���K�����z@����H��р�e�!��8�obф�4�Cф�x�V��Bnx�V�4�C�Śј��d%ј�(�(��ǘ���@@(�(��ǘ���@@����08�����08�(�(����(�(�����d%�4�C�Ś� $��2,0,2,0q�.+��� $ txtName��$
��Auto������ *Name+$ TwoWay=� �� $ 0�� �� *T_Organization.Level+.a��� $ IconPath�� � ��� �� ��:�����:����pej� �pej� ��:���2��x��2��x����Ѥ����Ѥ��2�� ����є�����'t���������������E�'t�E�'t0P�є�0P�є��E� ��E� ��������EѤ����+�Tn5��+�����+�x����+�x���E����E����x��Tn5�x��Tn5�EѤ���E�$� �E�$� �x��$� l't�T�-���\��l't�\���x��\���EѬ<R�EѬ<R�x��Ԙ� �x��Ԙ� �E�$� �E���-���������%����,�U���-�P��V�� �h�V�� P�7���-����� $ IconPath�� � ��$ 24���� $ Invalid�� �� *Alarm+.a��� $ txtName�� � ��
 ������� $ 	HighAlert�� �� *Alarm+.a��� $ txtName�� � ��
 � ����� $ LowAlert�� �� *Alarm+.a��� $ txtName�� � ��
 � ����� $ 
HighDanger�� �� *Alarm+.a��� $ txtName�� � ��
 �  ���� $ 	LowDanger�� �� *Alarm+.a��� $ txtName�� � ��
 �  ���� "
   �� $ Children�.R��� $ 
Horizontal=�.+�F� $��18��$��18��$
 Fill=�#���  �����P�x��ݩ(�� ���� p׻\r� $�� �(4��0�	�^)�@@�5������@@�.>�A�G�!اQ���P�x�ز�ݩ������y@@�����p׻�:pΈ"@@$��pP�(4�Ql^�	�^)�@@SP=������@@���A(Ċ�!�L�c����2ز�S�U)�����y@@�� ����d�:pΈ"@@��7!P�Ѵ�� l^����� P=�d*� ��PJ� �Ê��L�c�(�� �2��� �U)\r� �� � �dѐ0�7!є5P�� Ӱ.>��� �Gd*� اQPJ� �P�x�� 6n -1Ӵ�L -1 -1��L -1 6n� -1L���L ?� 6n ?��L� ?� ?�L� ?� 6n� ?���LL� -1 6n -1� $��2,0,2,0q�.+��� $ txtName��$
��Auto������ *Name+$ TwoWay=� �� $ Invalid�� �� *Alarm+.a��� $ txtName�� � ��
 ������� $ 	HighAlert�� �� *Alarm+.a��� $ txtName�� � ��
 � ����� $ LowAlert�� �� *Alarm+.a��� $ txtName�� � ��
 � ����� $ 
HighDanger�� �� *Alarm+.a��� $ txtName�� � ��
 �  ���� $ 	LowDanger�� �� *Alarm+.a��� $ txtName�� � ��
 �  ���� "
   �� $ Children�.R��� $ 
Horizontal=�.+�F� $��18��$��18��$
 Fill=�#���  ������ا  � �@]�  � �@]� ���@�4���@�4 Q%��ا  Q%��ا  � ���� @��р�� ���� -1��^�@������@�4@]� �@�4@fр������ -1���р�� @��� $��2,0,2,0q�.+��� $ txtName��$
��Auto������ *Name+$ TwoWay=�F� $ DisconnectIcon��$y�	Collapsed=�$��18��$��18��$
 Fill=�
������������< �K��H2(�T�)�:K)��;H2H2���;)�:K)(�T�2�K�< ����,@@�vW��K@�\�*����ф��@+��@�\��K�vW��,@@< �K�p��_)���@@���*̾;p40"�Ċ��_��T+U� 3���^p<��^�KT+U�+U��K��^p<��^� 3�+U��_�Ċ��"�p40h�;$�*��|����2,k��x
����l��쎂l����kx
�u]ӄ1�NO�#-�NO�J;�u]�` f@+���*�` fѨu]�J;ӜNO�#-�NO�1�u]�
S��k�ʡE��@@����ʡE��@@����
�,k��2Q��_)���@@F� $ 
UnBindIcon��$y�	Collapsed=�$��16��$��16��$
 Fill=�
�������������HR|�+���|�+�|��<C��|��Āx�&�L�!l�CL�!�䫮L�!\7Āx\7<C���<C���������Trjl�CTrj��/Trj�������<C�ф��+Ѡ�5|�+��5��e��HR��e��HR|�+ �� $ false�� �� *IsConnected+.a��� $ DisconnectIcon�� � y�$ Visible=��� $ false�� �� *
IsPaired+.a��� $ DisconnectIcon�� � y�$ 	Collapsed=��� $ 
UnBindIcon�� � y�$ Visible=��� $ Invalid�� �� *Alarm+.a��� $ txtName�� � ��
 ������� $ 	HighAlert�� �� *Alarm+.a��� $ txtName�� � ��
 � ����� $ LowAlert�� �� *Alarm+.a��� $ txtName�� � ��
 � ����� $ 
HighDanger�� �� *Alarm+.a��� $ txtName�� � ��
 �  ���� $ 	LowDanger�� �� *Alarm+.a��� $ txtName�� � ��
 �  ���� "
   �� $ Children�.R��� $ 
Horizontal=�.+�F� $��18��$��18��$
 Fill=�#���  ����� $�  Q%�v��  Q%�v�� ���р�� ���� H� $� � H� � р�C � р�C��р���� $�  H�� $�  Q%������ р� $� � Q% $� � Q%��� р���� � �  H�� � ��р�� ��р��  H�� �  H�� $��2,0,2,0q�.+��� $ txtName��$
��Auto������ *Name+$ TwoWay=�F� $ DisconnectIcon��$y�	Collapsed=�$��18��$��18��$
 Fill=�
������������< �K��H2(�T�)�:K)��;H2H2���;)�:K)(�T�2�K�< ����,@@�vW��K@�\�*����ф��@+��@�\��K�vW��,@@< �K�p��_)���@@���*̾;p40"�Ċ��_��T+U� 3���^p<��^�KT+U�+U��K��^p<��^� 3�+U��_�Ċ��"�p40h�;$�*��|����2,k��x
����l��쎂l����kx
�u]ӄ1�NO�#-�NO�J;�u]�` f@+���*�` fѨu]�J;ӜNO�#-�NO�1�u]�
S��k�ʡE��@@����ʡE��@@����
�,k��2Q��_)���@@ �� $ false�� �� *IsConnected+.a��� $ DisconnectIcon�� � y�$ Visible=��� $ Invalid�� �� *Alarm+.a��� $ txtName�� � ��
 ������� $ 	HighAlert�� �� *Alarm+.a��� $ txtName�� � ��
 � ����� $ LowAlert�� �� *Alarm+.a��� $ txtName�� � ��
 � ����� $ 
HighDanger�� �� *Alarm+.a��� $ txtName�� � ��
 �  ���� $ 	LowDanger�� �� *Alarm+.a��� $ txtName�� � ��
 �  ������  �� 
TargetType" Q�  LineTreeViewItem �� BasedOn# [ .���  �@Q�
IsSelected �� *
IsSelected+$ TwoWay=���  �@Q�
IsExpanded �� *
IsExpanded+$ TwoWay=��� $��1��$��3��#���  $��Center=�$��Stretch=��� -    ��Name$ imageResource��$��2��(  "System.Windows.Controls.WebBrowser  -     Name$ 
webBrowser��$��2��