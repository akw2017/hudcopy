   M S B A M L   `   `   `  ����M  IAIC.OnLineDataPage, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullC&clr-namespace:AIC.OnLineDataPage.ViewsAIC.OnLineDataPage.Views  5      I)clr-namespace:AIC.OnLineDataPage.ControlsAIC.OnLineDataPage.Controls  5      J FDiagramDesigner, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullJ6clr-namespace:DiagramDesigner;assembly=DiagramDesignerDiagramDesigner 5
      C ?AIC.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5(clr-namespace:AIC.Core;assembly=AIC.CoreAIC.Core 5      G CAIC.CoreType, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullA0clr-namespace:AIC.CoreType;assembly=AIC.CoreTypeAIC.CoreType 5      [;clr-namespace:AIC.Core.OrganizationModels;assembly=AIC.CoreAIC.Core.OrganizationModels 5      E AAIC.Domain, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;,clr-namespace:AIC.Domain;assembly=AIC.Domain
AIC.Domain 5      I2clr-namespace:AIC.Core.ExCommand;assembly=AIC.CoreAIC.Core.ExCommand 5      E'clr-namespace:AIC.OnLineDataPage.ModelsAIC.OnLineDataPage.Models  5      4    .AIC.OnLineDataPage.Views.OnlineDataDiagramView   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      8local&clr-namespace:AIC.OnLineDataPage.Views     5      c	 _System.Windows.Interactivity, Version=4.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity 	 5      =localControls)clr-namespace:AIC.OnLineDataPage.Controls 	 5      h
 dMicrosoft.Expression.Interactions, Version=4.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bei9http://schemas.microsoft.com/expression/2010/interactions 
 5	      ?dd6clr-namespace:DiagramDesigner;assembly=DiagramDesigner 
 5
      3core(clr-namespace:AIC.Core;assembly=AIC.Core 
 5      ?coreType0clr-namespace:AIC.CoreType;assembly=AIC.CoreType 
 5      Icontrol;clr-namespace:AIC.Core.OrganizationModels;assembly=AIC.Core 
 5      9domain,clr-namespace:AIC.Domain;assembly=AIC.Domain 
 5      P LPrism.Wpf, Version=6.2.0.0, Culture=neutral, PublicKeyToken=91a96d2a154366d8$prismhttp://prismlibrary.com/  5      @command2clr-namespace:AIC.Core.ExCommand;assembly=AIC.Core  5      X TXceed.Wpf.Toolkit, Version=3.2.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit  5      4models'clr-namespace:AIC.OnLineDataPage.Models  5      !  Prism.Mvvm.ViewModelLocator    AutoWireViewModel  .  � 	Resources 5       �� MergedDictionaries �� Source)  #AIC.Core.ExCommand.CommandReference   AddCommandReference   Command9   3AIC.OnLineDataPage.Models.ValueTypeTemplateSelector  valueTypeTemplateSelector  defaultValueTempalte   DefaultTemplate  imageValueTemplate   ImageTemplate#  AIC.Core.EnumBooleanConverter  enumBooleanConverter(  "AIC.Core.EnumToVisibilityConverter  enumToVisibilityConverter   Reverse  AIC.Core.NegateConverter  negateConverter  AIC.Core.BindingProxy 	 proxy   Data  DragGripperStyle	 �� 
TargetType
 �� Property �� Value �� 
TargetType �� Padding 	 ItemContainerStyle/   )AIC.OnLineDataPage.Controls.ResizableItem �Name ��Name J� SnapsToDevicePixels n�Name �� Triggers P� Property P� Value 
 ItemsControlStyle  DefaultErrorTemplate �� Orientation �� CornerRadius �Name K� ZIndex � Points J� IsHitTestVisible � Points �� StrokeEndLineCap �� StrokeStartLineCap ��Name ^� InitialShowDelay  � ToolTip! �� ElementName" �� Path$	  Xceed.Wpf.Toolkit.DoubleUpDown�� 5   
    5      �� 5      $5 //AIC.Resources;component/Themes/ColorStyle.xaml?�6%   �� 5      $8 2/AIC.Resources;component/Themes/CheckBoxStyle.xaml?�6%   �� 5      $I C/AIC.OnLineDataPage;component/Themes/OnlineDataDiagramTemplate.xaml?�6%   �� 5      $4 ./AIC.Resources;component/Themes/ScrollBar.xaml?�6%   %#
  &	        &	 +     7  7  &	 L     &	 _     &	 y     &	 �     &	 �     &		 �    7  &	
 [    &	     '	  S	    7    5       6C   �� *
AddCommand+  5      8   8  5!   3     5"      6(     5#       .  6O     5$      6#     5%       6.   �� �� 5&      "	 n�6-   .�5'      �� 
 � ��$ 8��6+   �� 5(      
 � ��
 ���� 6/   �� 5)      
 �@�Cursor$ SizeNS��6+   �� 5*      
 � �� 5+      �� 5,      " n�6*   .R�5-      �� #��z��6%   # z��6O   5.      50      51      �� 52      "	  6/   .�53      �� 
 � ��$ 0,2,0,0q�6+   �� 54      
 � ��$ 20��6.   �� 56      
 � ��$	 228��6+   �� 57      
 � ��$ Stretch=�6?   �� 58      
 � ��$ Stretch=�6=   �� 59      
 � �� 5:      �� 5;      "  6*   .R�5<      � $ rootGrid��6#   .+�5=   "   �� $ Bd��6)   #��z��65   #��z��6a   #��z��6�   # z��6�    . ._�5>   &   �� #��z��67   # z 6z   #��z��6�   5?   #   n� 5@   "   $ PART_DragThumb��6(   8��  $��Stretch=�6j   $��Bottom=�6�   5A       5B      P� 5C   "    � {� .  . �5D   &   �� # � ��6C   
 � ��5E   #   5G      5I      5J      �� 5K      "	 ��6.   .�5L      �� 
 � ��$
 Auto=�6O   �� 5M      
 � ��$
 Auto=�6M   �� 5N      
 � �� . 6B   �� 5O      
 �@��PanningMode$
 Both=�6=   �� 5P      
 � �� 5Q      �� 5R      " ��6*   .R�5S      �� #��z��6%   #��z��6Q   #��z��6�   # z��6�    . ._�5T   "   �� |�.  #��z��6A   .��5U   &   �� # z 65   5V   #   5W      5X      5Z      5[      �� 5\      .R�5]      �� $ 
Horizontal=�6   .+�5^      �� $��1q�6   ���	#FFdc000c$	 0.7��6I   $	��Top=�6\   ._�5_      � .+�5`      � $ toolTipCorner��6&   $ 2��6=   $��-1q�6M    �   ��[ ��[ ��[ ���	#FFdc000c$��Right=�5a   .   $	��Top=�6J    . 6b   � 5b      $ 3��6'    �   ��j ��j $��-1q�6H   $��Right=�6T   $	��Top=�6p    . $	��1.5��5c   /   $ Round=�6E   $ Round=�6^   
�������6y   �� 5d      $ placeholder��68   5e      5f      �� 5g      
���  ��$��3q�6.   $ 10��69   $��20��6K   $��20��6V   $	��Top=�6b   $ 0��5i   X     5h      �� $! placeholder��$8" 2AdornedElement.(Validation.Errors)[0].ErrorContent�._�5j      �� $��!��6$   
�������$��Center=�6@   $��Center=�6]   5k      5l      5m      �� 5n      "	 	 6   .�5o      �� 
 �@=�ErrorTemplate8   6=   5p      5q      .��5s      � -   5        $��grdWorkbench��5s      # � RowDefinitions# 5t   
   �� 5u      $
��Auto �6   �� 5v      $��* �6   $ � ColumnDefinitions$ 5x   
   �� 5y      $
��Auto �6   �� 5z      $
��Auto �6   �� 5{      $��* �6   �� 5|      $
��Auto �6   �� 5}      $��* �6   .+�5�   
   R� -   5        % R�Name$% treeview��5�   1   $��0��6   $��2��6      VirtualisedMetroTreeView#���  5�      ��5�      �� *OrganizationTreeItems+ 5�      & �� DataType?
  9AIC.Core.OrganizationModels.OrganizationTreeItemViewModel' �� ItemsSource( F�Name) �� Stretch  AccentColorBrush* ��Name+ �� Mode, �� Triggers- �� Value. �� Binding/ �� 
TargetName9  3AIC.Core.OrganizationModels.DeviceTreeItemViewModel0 ��Name7  1AIC.Core.OrganizationModels.ItemTreeItemViewModel9  3AIC.Core.OrganizationModels.DivFreTreeItemViewModel��%�#  (��       5�   +   *,
 +)(�� [    5�   +   *, +)7   (�� g    5�   +   *, +)(�� �    5�   +   *, +)�� 5�      "& 
 6+   ' 6e   �� $" Children�.R�5�      �� $ 
Horizontal=�6!   .+�5�      F� $( IconPath��6   $��18��61   $��18��6<   $
) Fill=�6H   #���  6W   ������Bnx�V��Bn4�Cќ�O�ob�L�!�!�4�C�71�ܽ�,���ܽ���K�����z@����H��р�e�!��8�obф�4�Cф�x�V��Bnx�V�4�C�Śј��d%ј�(�(��ǘ���@@(�(��ǘ���@@����08�����08�(�(����(�(�����d%�4�C�Ś6�   � 5�      $��2,0,2,0q�6    .+�5�      �� $* txtName��6(   $
��Auto��6\   ��69   �� *Name+$+ TwoWay=�6\   5�      5�      , 5�      �� 5�      $- 0��6O   . 6&   �� *T_Organization.Level+.a�5�      �� $/ IconPath��6%   
 � ��� �� ��:�����:����pej� �pej� ��:���2��x��2��x����Ѥ����Ѥ��2�� ����є�����'t���������������E�'t�E�'t0P�є�0P�є��E� ��E� ��������EѤ����+�Tn5��+�����+�x����+�x���E����E����x��Tn5�x��Tn5�EѤ���E�$� �E�$� �x��$� l't�T�-���\��l't�\���x��\���EѬ<R�EѬ<R�x��Ԙ� �x��Ԙ� �E�$� �E���-���������%����,�U���-�P��V�� �h�V�� P�7���-���6K   �� 5�      $/ IconPath��6%   
 � ��$ 24��6L   5�      �� 5�      $- Invalid��6@   . 6&   �� *Alarm+.a�5�      �� $/ txtName��6%   
 � ��
 ��i��6P   5�      �� 5�      $- HighPreAlarm��6@   . 6&   �� *Alarm+.a�5�      �� $/ txtName��6%   
 � ��
 � ���6P   5�      �� 5�      $- LowPreAlarm��6@   . 6&   �� *Alarm+.a�5�      �� $/ txtName��6%   
 � ��
 � ���6P   5�      �� 5�      $- 	HighAlarm��6@   . 6&   �� *Alarm+.a�5�      �� $/ txtName��6%   
 � ��
 � ���6P   5�      �� 5�      $- LowAlarm��6@   . 6&   �� *Alarm+.a�5�      �� $/ txtName��6%   
 � ��
 � ���6P   5�      �� 5�      $- 
HighDanger��6@   . 6&   �� *Alarm+.a�5�      �� $/ txtName��6%   
 � ��
 �  ��6P   5�      �� 5�      $- 	LowDanger��6@   . 6&   �� *Alarm+.a�5�      �� $/ txtName��6%   
 � ��
 �  ��6P   5�      5�      �� 5�      "&  6+   ' 6_   �� $" Children�.R�5�      �� $ 
Horizontal=�6!   .+�5�      F� $��18��6   $��18��6*   $
) Fill=�66   #���  6E   �����P�x��ݩ(�� ���� p׻\r� $�� �(4��0�	�^)�@@�5������@@�.>�A�G�!اQ���P�x�ز�ݩ������y@@�����p׻�:pΈ"@@$��pP�(4�Ql^�	�^)�@@SP=������@@���A(Ċ�!�L�c����2ز�S�U)�����y@@�� ����d�:pΈ"@@��7!P�Ѵ�� l^����� P=�d*� ��PJ� �Ê��L�c�(�� �2��� �U)\r� �� � �dѐ0�7!є5P�� Ӱ.>��� �Gd*� اQPJ� �P�x�� 6n -1Ӵ�L -1 -1��L -1 6n� -1L���L ?� 6n ?��L� ?� ?�L� ?� 6n� ?���LL� -1 6n -16o   � 5�      $��2,0,2,0q�6    .+�5�      �� $* txtName��6(   $
��Auto��6\   ��69   �� *Name+$+ TwoWay=�6\   5�      �� 5�      $0 AddBtn��6!   8��  $��Center=�6}   $��24��6�   $��24��6�   $��0q�6�   ��6`   �� .��5�      F� $
) Fill=�6#   $��18��62   $��18��6=   #���  6I   F����F���C�ଔTn5���7Tn5�Tn5��C���7�<R�ଔ�<R�F���C6s   5�      5�      , 5�      �� 5�      $- Invalid��6@   . 6&   �� *Alarm+.a�5�      �� $/ txtName��6%   
 � ��
 ��i��6P   5�      �� 5�      $- HighPreAlarm��6@   . 6&   �� *Alarm+.a�5�      �� $/ txtName��6%   
 � ��
 � ���6P   5�      �� 5�      $- LowPreAlarm��6@   . 6&   �� *Alarm+.a�5�      �� $/ txtName��6%   
 � ��
 � ���6P   5�      �� 5�      $- 	HighAlarm��6@   . 6&   �� *Alarm+.a�5�      �� $/ txtName��6%   
 � ��
 � ���6P   5�      �� 5�      $- LowAlarm��6@   . 6&   �� *Alarm+.a�5�      �� $/ txtName��6%   
 � ��
 � ���6P   5�      �� 5�      $- 
HighDanger��6@   . 6&   �� *Alarm+.a�5�      �� $/ txtName��6%   
 � ��
 �  ��6P   5�      �� 5�      $- 	LowDanger��6@   . 6&   �� *Alarm+.a�5�      �� $/ txtName��6%   
 � ��
 �  ��6P   5�      5�      �� 5�      "&  6+   ' 6]   �� $" Children�.R�5�      �� $ 
Horizontal=�6!   .+�5�      F� $��18��6   $��18��6*   $
) Fill=�66   #���  6E   ������ا  � �@]�  � �@]� ���@�4���@�4 Q%��ا  Q%��ا  � ���� @��р�� ���� -1��^�@������@�4@]� �@�4@fр������ -1���р�� @��6o   � 5�      $��2,0,2,0q�6   .+�5�      �� $* txtName��6(   $
��Auto��6\   ��69   �� *Name+$+ TwoWay=�6\   5�      F� 5�      $( DisconnectIcon��6   $y�	Collapsed=�67   $��18��6N   $��18��6Y   $
) Fill=�6f   
������������< �K��H2(�T�)�:K)��;H2H2���;)�:K)(�T�2�K�< ����,@@�vW��K@�\�*����ф��@+��@�\��K�vW��,@@< �K�p��_)���@@���*̾;p40"�Ċ��_��T+U� 3���^p<��^�KT+U�+U��K��^p<��^� 3�+U��_�Ċ��"�p40h�;$�*��|����2,k��x
����l��쎂l����kx
�u]ӄ1�NO�#-�NO�J;�u]�` f@+���*�` fѨu]�J;ӜNO�#-�NO�1�u]�
S��k�ʡE��@@����ʡE��@@����
�,k��2Q��_)���@@6�   F� 5�      $( 
UnBindIcon��6   $y�	Collapsed=�63   $��16��6J   $��16��6U   $
) Fill=�6a   
�������������HR|�+���|�+�|��<C��|��Āx�&�L�!l�CL�!�䫮L�!\7Āx\7<C���<C���������Trjl�CTrj��/Trj�������<C�ф��+Ѡ�5|�+��5��e��HR��e��HR|�+6|   5�      , 5�      �� 5�      $- false��6F   . 6&   �� *IsConnected+.a�5�      �� $/ DisconnectIcon��6%   
 � y�$ Visible=�6W   5�      �� 5�      $- false��6C   . 6&   �� *
IsPaired+.a�5�      �� $/ DisconnectIcon��6%   
 � y�$ 	Collapsed=�6W   �� 5�      $/ 
UnBindIcon��6%   
 � y�$ Visible=�6S   5�      �� 5�      $- Invalid��6@   . 6&   �� *Alarm+.a�5�      �� $/ txtName��6%   
 � ��
 ��i��6P   5�      �� 5�      $- HighPreAlarm��6@   . 6&   �� *Alarm+.a�5�      �� $/ txtName��6%   
 � ��
 � ���6P   5�      �� 5�      $- LowPreAlarm��6@   . 6&   �� *Alarm+.a�5�      �� $/ txtName��6%   
 � ��
 � ���6P   5�      �� 5�      $- 	HighAlarm��6@   . 6&   �� *Alarm+.a�5�      �� $/ txtName��6%   
 � ��
 � ���6P   5�      �� 5�      $- LowAlarm��6@   . 6&   �� *Alarm+.a�5�      �� $/ txtName��6%   
 � ��
 � ���6P   5�      �� 5�      $- 
HighDanger��6@   . 6&   �� *Alarm+.a�5�      �� $/ txtName��6%   
 � ��
 �  ��6P   5�      �� 5�      $- 	LowDanger��6@   . 6&   �� *Alarm+.a�5�      �� $/ txtName��6%   
 � ��
 �  ��6P   5�      5�      �� 5�      "&  6+   ' 6_   �� $" Children�.R�5�      �� $ 
Horizontal=�6!   .+�5�      F� $��18��6   $��18��6*   $
) Fill=�66   #���  6E   ����� $�  Q%�v��  Q%�v�� ���р�� ���� H� $� � H� � р�C � р�C��р���� $�  H�� $�  Q%������ р� $� � Q% $� � Q%��� р���� � �  H�� � ��р�� ��р��  H�� �  H�6o   � 5�      $��2,0,2,0q�6    .+�5�      �� $* txtName��6(   $
��Auto��6\   ��69   �� *Name+$+ TwoWay=�6\   5�      F� 5�      $( DisconnectIcon��6   $y�	Collapsed=�67   $��18��6N   $��18��6Y   $
) Fill=�6e   
������������< �K��H2(�T�)�:K)��;H2H2���;)�:K)(�T�2�K�< ����,@@�vW��K@�\�*����ф��@+��@�\��K�vW��,@@< �K�p��_)���@@���*̾;p40"�Ċ��_��T+U� 3���^p<��^�KT+U�+U��K��^p<��^� 3�+U��_�Ċ��"�p40h�;$�*��|����2,k��x
����l��쎂l����kx
�u]ӄ1�NO�#-�NO�J;�u]�` f@+���*�` fѨu]�J;ӜNO�#-�NO�1�u]�
S��k�ʡE��@@����ʡE��@@����
�,k��2Q��_)���@@6�   5�      , 5�      �� 5�      $- false��6F   . 6&   �� *IsConnected+.a�5�      �� $/ DisconnectIcon��6%   
 � y�$ Visible=�6W   5�      �� 5�      $- Invalid��6@   . 6&   �� *Alarm+.a�5�      �� $/ txtName��6%   
 � ��
 ��i��6P   5�      �� 5      $- HighPreAlarm��6@   . 6&   �� *Alarm+.a�5     �� $/ txtName��6%   
 � ��
 � ���6P   5     �� 5     $- LowPreAlarm��6@   . 6&   �� *Alarm+.a�5     �� $/ txtName��6%   
 � ��
 � ���6P   5     �� 5     $- 	HighAlarm��6@   . 6&   �� *Alarm+.a�5     �� $/ txtName��6%   
 � ��
 � ���6P   5     �� 5	     $- LowAlarm��6@   . 6&   �� *Alarm+.a�5
     �� $/ txtName��6%   
 � ��
 � ���6P   5     �� 5     $- 
HighDanger��6@   . 6&   �� *Alarm+.a�5     �� $/ txtName��6%   
 � ��
 �  ��6P   5     �� 5     $- 	LowDanger��6@   . 6&   �� *Alarm+.a�5     �� $/ txtName��6%   
 � ��
 �  ��6P   5     5     5     ��5     �� 5     "	 Q�6     LineTreeViewItem1 �� BasedOn#1 [ 62   .�5     �� 
 �@Q�
IsSelected 63   �� *
IsSelected+$+ OneTime=��� 5     
 �@Q�
IsExpanded 63   �� *
IsExpanded+$+ OneTime=�5     . 	 (System.Windows.Interactivity.Interaction2   Triggers2 5     / 	 )System.Windows.Interactivity.EventTrigger  5     3   	EventName$3 SelectedItemChanged��6!   . 	 (System.Windows.Interactivity.TriggerBase4   Actions.4 5     6 	 0System.Windows.Interactivity.InvokeCommandAction  5   Command5 6,   �� *SelectedTreeChangedComamnd+6   CommandParameter6 6[   �� *SelectedItem+$! treeview��5      5#     �� 5%  
   -   5        7 ��Name$7 gsSplitterr��5%     $��0��6,   $��2��69   $��1��6J   $��3��6Z   #���  6d   $��Center=�6�   $��Stretch=�6�     GridSplitterStyleExpend#��[ 6�   �� 5'  
   $��0��6   $��2��6   $��3��6.   $ 2q�6B     AccentColorBrush2#���  6N   ._�5(     � .+�5)     �� $ 
Horizontal=�6   .+�5*     �� $��设备名称��6    $��Center=�6,   $	��5,0q�6G   �� 5+     $��Center=�6:   $	��5,0q�6U   8 �� 
IsReadOnly8 . $	��200��6t   ��6   �� *
DeviceName+6t   �� 5,     -   5        9 ��Name$	9 chk��5,     $��锁定布局��6M     CheckBoxStyle#��[ 6\   $��Center=�6�   $	��5,0q�6�   : d� 	IsChecked: 6,   �� *
IsReadOnly+{�6�   �� *CanOperated+6�   �� 5-       AccentedSquareButtonStyle#���  6C   $��Center=�6x   $	��5,0q�6�   ��6   �� *SaveLayoutCommand+��5.     �� 5/     $ 
Horizontal=�6)   .+�50  "   �� $
��None=�6(   $3��-/AIC.Resources;component/Images/page_save.png��67   �� 51  "   $��保存布局��6,   $��Center=�68   52     54     55     56     57     �� 59  
   $��1��6   $��2��6   $��1q�6.   #���  6B   ._�5:     � .+�5;     $  DiagramDesigner.DiagramControl  ;   ItemTemplateSelector#; [ 6M   $��3q�6�   
������ < � DataContext< 6$   �� *DiagramViewModel+6�   5<     5=     �� 5?  
   -   5        $7 gsSecondSplitterr��5?     $��1��62   $��3��6?   $��3��6O   #���  6Y   $��Center=�6�   $��Stretch=�6�   9� 5A  
   -   5        = 9�Name$= expander��5A     $��1��6%   $��4��62   > 9� ExpandDirection$
> Left=�6B   ? 9� 
IsExpanded? 6Y   �� *ItemIsExpanded+.��5B     � $��0��6   $��4��6    # 5C     �� 5D     $
��auto �6$   �� 5E     $��* �6$   .+�5G     � $��0,5,0,0q�6   .+�5H     �� $��5q�6!   .+�5I     �� $ 
Horizontal=�6%   .+�5J     �� -   5        $9 filterCheckBox��5J  '   $	��5,0q�6?   #��[ 6L   $��Center=�6�   : 6s   �� *
IsFilter+5S     &   Xceed.Wpf.Toolkit.DropDownButton  5T     -   5        @  Name$@ dropDownButton��5T  2   
������ $��滤波��6c   $��Center=�6p   A   DropDownContentA 5U  "   � 5V  &   $	��320��6+   # 5W  *   �� 5X  .   $
��auto �6<   �� 5Y  .   $��* �6<   .+�5\  *   �� $ 
Horizontal=�65   .+�5^  .   	� -	   5        B 	�Name$B 
bandPassRb��5^  :     RadioButtonStyle#��[ 6N   
���   �$��带通��6�   $��Center=�6=  : 6�   �� C �� Source#C [ $$" Data.SelectedSignal.FilterType�D �� 	Converter#D [ E �� ConverterParameter$E BandPass��6=  	� 5_  .   -
   5        $B 
highPassRb��5_  :   #��[ 6N   
���   �$��高通��6�   $��Center=�6=  : 6�   �� #C [ $$" Data.SelectedSignal.FilterType�#D [ $E HighPass��6=  	� 5`  .   -   5        $B 	lowPassRb��5`  :   #��[ 6M   
���   �$��低通��6�   $��Center=�6;  : 6�   �� #C [ $$" Data.SelectedSignal.FilterType�#D [ $E LowPass��6;  5a  +   � 5b  *   $��1��6/   $��5,5,25,0q�6<   y�6N   �� #C [ $$" Data.SelectedSignal.FilterType�#D [   AIC.CoreType.FilterTypeF   BandPass#E ZF G �� FallbackValue$G 	Collapsed��# 5c  .   �� 5d  2   $
��auto �6@   �� 5e  2   $
��auto �6@   �� 5f  2   $
��auto �6@   �� 5g  2   $
��auto �6@   �� 5h  2   $
��auto �6@   �� 5i  2   $
��auto �6@   �� 5j  2   $
��auto �6@   $ 5l  .   �� 5m  2   $
��auto �6C   �� 5n  2   $��* �6C   .+�5p  .   �� $��通带衰减��64   	  5q  .   $��1��6@   �  �Xceed.Wpf.Toolkit.Primitives.UpDownBase`1[[System.Nullable`1[[System.Double, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]H   Minimum$
H 0.01X�6P   I   Maximum$I 3X�6_   �  �Xceed.Wpf.Toolkit.NumericUpDown`1[[System.Nullable`1[[System.Double, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]J   	Increment$	J 0.1X�6k   K   ValueK 6{   �� #C [ $8" 2Data.SelectedSignal.BPFilter.PassbandAttenuationDB��� 5r  .   $��1��64   $��阻带衰减��6A   	  5s  .   $��1��6@   $��1��6M   $H 40X�6]   $	I 120X�6j   K 6x   �� #C [ $8" 2Data.SelectedSignal.BPFilter.StopbandAttenuationDB��� 5t  .   #��[ 67   
���   �$��2��6�   $��绑定转速��6�   $��2��6�   : 6^   �� #C [ $/" )Data.SelectedSignal.BPFilter.IsRPMBinding�2 5u  2     5v  6   $3 Checked��6E   .4 5w  :     5 6P   �� #C [ $1" +Data.SelectedSignal.BPFilter.BindRPMCommand�6 6�   �� #C [ $" Data.SelectedSignal.RPM�5x  7     5y  6   $3 	Unchecked��6E   .4 5z  :     5 6P   �� #C [ $3" -Data.SelectedSignal.BPFilter.UnBindRPMCommand�6 6�   �� #C [ $" Data.SelectedSignal.RPM�5{  7   5}  /   �� 5~  .   $��3��64   $��低逼近带通��6A   	  5  .   $��3��6@   $��1��6M   $H 0X�6]   K 6i   �� #C [ $3" -Data.SelectedSignal.BPFilter.BPPassBandFreLow��� 5�  .   $��4��64   $��高逼近带通��6A   	  5�  .   $��4��6@   $��1��6M   $H 0X�6]   K 6i   �� #C [ $4" .Data.SelectedSignal.BPFilter.BPPassBandFreHigh��� 5�  .   $��5��64   $��过渡带宽��6A   	  5�  .   $��5��6A   $��1��6N   $H 0X�6^   K 6j   �� #C [ $6" 0Data.SelectedSignal.BPFilter.TransitionBandwidth�A� 5�  .   $��6��6:   $��2��6G   .+�5�  2   �� $��保存��69   $��5q�6F   ��6Q   �� #C [ $." (Data.SelectedSignal.BPFilter.SaveCommand��� 5�  2   $��重置��69   $��5q�6F   ��6Q   �� #C [ $/" )Data.SelectedSignal.BPFilter.ResetCommand�5�  /   5�  +   � 5�  *   $��1��6/   $��5,5,25,0q�6<   y�6N   �� #C [ $$" Data.SelectedSignal.FilterType�#D [ L   HighPass#E ZL $G 	Collapsed��# 5�  .   �� 5�  2   $
��auto �6@   �� 5�  2   $
��auto �6@   �� 5�  2   $
��auto �6@   �� 5�  2   $
��auto �6@   �� 5�  2   $
��auto �6@   �� 5�  2   $
��auto �6@   $ 5�  .   �� 5�  2   $
��auto �6C   �� 5�  2   $��* �6C   .+�5�  .   �� $��通带衰减��64   	  5�  .   $��1��6@   $
H 0.01X�6P   $I 3X�6_   $	J 0.1X�6k   K 6{   �� #C [ $8" 2Data.SelectedSignal.HPFilter.PassbandAttenuationDB��� 5�  .   $��1��64   $��阻带衰减��6A   	  5�  .   $��1��6@   $��1��6M   $H 40X�6]   $	I 120X�6j   K 6x   �� #C [ $8" 2Data.SelectedSignal.HPFilter.StopbandAttenuationDB��� 5�  .   #��[ 67   
���   �$��2��6�   $��绑定转速��6�   $��2��6�   : 6^   �� #C [ $/" )Data.SelectedSignal.HPFilter.IsRPMBinding�$+ TwoWay=�2 5�  2     5�  6   $3 Checked��6E   .4 5�  :     5 6P   �� #C [ $1" +Data.SelectedSignal.HPFilter.BindRPMCommand�6 6�   �� #C [ $" Data.SelectedSignal.RPM�5�  7     5�  6   $3 	Unchecked��6E   .4 5�  :     5 6P   �� #C [ $3" -Data.SelectedSignal.HPFilter.UnBindRPMCommand�6 6�   �� #C [ $" Data.SelectedSignal.RPM�5�  7   5�  /   �� 5�  .   $��3��64   $��通带��6A   	  5�  .   $��3��6@   $��1��6M   $H 0X�6]   K 6i   �� #C [ $." (Data.SelectedSignal.HPFilter.PassbandFre��� 5�  .   $��4��64   $��阻带��6A   	  5�  .   $��4��6@   $��1��6M   $H 0X�6]   K 6i   �� #C [ $." (Data.SelectedSignal.HPFilter.StopbandFre�A� 5�  .   $��6��6:   $��2��6G   .+�5�  2   �� $��保存��69   $��5q�6F   ��6Q   �� #C [ $." (Data.SelectedSignal.HPFilter.SaveCommand��� 5�  2   $��重置��69   $��5q�6F   ��6Q   �� #C [ $/" )Data.SelectedSignal.HPFilter.ResetCommand�5�  /   5�  +   � 5�  *   $��1��6/   $��5,5,25,0q�6<   y�6N   �� #C [ $$" Data.SelectedSignal.FilterType�#D [ M   LowPass#E ZM $G 	Collapsed��# 5�  .   �� 5�  2   $
��auto �6@   �� 5�  2   $
��auto �6@   �� 5�  2   $
��auto �6@   �� 5�  2   $
��auto �6@   �� 5�  2   $
��auto �6@   �� 5�  2   $
��auto �6@   $ 5�  .   �� 5�  2   $
��auto �6C   �� 5�  2   $��* �6C   .+�5�  .   �� $��通带衰减��64   	  5�  .   $��1��6@   $
H 0.01X�6P   $I 3X�6_   $	J 0.1X�6k   K 6{   �� #C [ $8" 2Data.SelectedSignal.LPFilter.PassbandAttenuationDB��� 5�  .   $��1��64   $��阻带衰减��6A   	  5�  .   $��1��6@   $��1��6M   $H 40X�6]   $	I 120X�6j   K 6x   �� #C [ $8" 2Data.SelectedSignal.LPFilter.StopbandAttenuationDB��� 5�  .   #��[ 67   
���   �$��2��6�   $��绑定转速��6�   $��2��6�   : 6^   �� #C [ $/" )Data.SelectedSignal.LPFilter.IsRPMBinding�$+ TwoWay=�2 5�  2     5�  6   $3 Checked��6E   .4 5�  :     5 6P   �� #C [ $1" +Data.SelectedSignal.LPFilter.BindRPMCommand�6 6�   �� #C [ $" Data.SelectedSignal.RPM�5�  7     5�  6   $3 	Unchecked��6E   .4 5�  :     5 6P   �� #C [ $3" -Data.SelectedSignal.LPFilter.UnBindRPMCommand�6 6�   �� #C [ $" Data.SelectedSignal.RPM�5�  7   5�  /   �� 5�  .   $��3��64   $��通带��6A   	  5�  .   $��3��6@   $��1��6M   $H 0X�6]   K 6i   �� #C [ $." (Data.SelectedSignal.LPFilter.PassbandFre��� 5�  .   $��4��64   $��阻带��6A   	  5�  .   $��4��6@   $��1��6M   $H 0X�6]   K 6i   �� #C [ $." (Data.SelectedSignal.LPFilter.StopbandFre�A� 5�  .   $��6��6:   $��2��6G   .+�5�  2   �� $��保存��69   $��5q�6F   ��6Q   �� #C [ $." (Data.SelectedSignal.LPFilter.SaveCommand��� 5�  2   $��重置��69   $��5q�6F   ��6Q   �� #C [ $/" )Data.SelectedSignal.LPFilter.ResetCommand�5�  /   5�  +   5�  '   5�     	� 5�     $	��无��6*   N 	� 	GroupName$N pregroup��66   #��[ 6K   $��Center=�6�   : 6u   �� *SignalPreProccessType+#D [ $
E None��6�   	� 5�     $��包络��6*   $N pregroup��67   #��[ 6L   $��Center=�6�   : 6v   �� *SignalPreProccessType+#D [ $E Envelope��6�   	� 5�     $	��TFF��6*   $N pregroup��68   #��[ 6M   $��Center=�6�   : 6w   �� *SignalPreProccessType+#D [ $	E TFF��6�   	� 5�     $��	倒频谱��6*   $N pregroup��68   #��[ 6M   $��Center=�6�   : 6w   �� *SignalPreProccessType+#D [ $E Cepstrum��6�   5�     � 5�     $��Disabled=�6$   .+�5�     �� $	��5,0q�6'   $��时域��64   #��[ 6k   : 6A   �� *IsTimeDomainChecked+2 5�  "     5�  &   $3 Checked��65   .4 5�  *     $6 
TimeDomain��6h   5 6@   �� *AddGraphFuncCommand+6h   5�  '     5�  &   $3 	Unchecked��65   .4 5�  *     $6 
TimeDomain��6k   5 6@   �� *RemoveGraphFuncCommand+6k   5�  '   5     �� 5     $	��5,0q�6'   $��频域��64   #��[ 6p   : 6A   �� *IsFrequencyDomainChecked+2 5  "     5  &   $3 Checked��65   .4 5  *     $6 FrequencyDomain��6h   5 6@   �� *AddGraphFuncCommand+6h   5  '     5  &   $3 	Unchecked��65   .4 5  *     $6 FrequencyDomain��6k   5 6@   �� *RemoveGraphFuncCommand+6k   5	  '   5     �� 5     $	��5,0q�6'   $��	趋势图��64   #��[ 6j   : 6B   �� *IsRMSTrendChecked+2 5  "     5  &   $3 Checked��65   .4 5  *     $6 RMSTrend��6h   5 6@   �� *AddGraphFuncCommand+6h   5  '     5  &   $3 	Unchecked��65   .4 5  *     $6 RMSTrend��6k   5 6@   �� *RemoveGraphFuncCommand+6k   5  '   5     �� 5     $	��5,0q�6'   $��	功率谱��64   #��[ 6o   : 6B   �� *IsPowerSpectrumChecked+2 5  "     5  &   $3 Checked��65   .4 5  *     $6 PowerSpectrum��6h   5 6@   �� *AddGraphFuncCommand+6h   5  '     5  &   $3 	Unchecked��65   .4 5  *     $6 PowerSpectrum��6k   5 6@   �� *RemoveGraphFuncCommand+6k   5  '   5     �� 5      $	��5,0q�6'   $��功率谱密度��64   #��[ 6x   : 6D   �� *IsPowerSpectrumDensityChecked+2 5!  "     5"  &   $3 Checked��65   .4 5#  *     $6 PowerSpectrumDensity��6h   5 6@   �� *AddGraphFuncCommand+6h   5$  '     5%  &   $3 	Unchecked��65   .4 5&  *     $6 PowerSpectrumDensity��6k   5 6@   �� *RemoveGraphFuncCommand+6k   5'  '   5)     �� 5*     $	��5,0q�6'   $��时间三维��64   #��[ 6i   : 6C   �� *IsTime3DChecked+2 5+  "     5,  &   $3 Checked��65   .4 5-  *     $6 Time3D��6h   5 6@   �� *AddGraphFuncCommand+6h   5.  '     5/  &   $3 	Unchecked��65   .4 50  *     $6 Time3D��6k   5 6@   �� *RemoveGraphFuncCommand+6k   51  '   53     �� 54     $	��5,0q�6'   $��分频��64   #��[ 6l   {�.  $y�	Collapsed=�6�   : 6A   �� *IsMultiDivFreChecked+2 55  "     56  &   $3 Checked��65   .4 57  *     $6 MultiDivFre��6h   5 6@   �� *AddGraphFuncCommand+6h   58  '     59  &   $3 	Unchecked��65   .4 5:  *     $6 MultiDivFre��6k   5 6@   �� *RemoveGraphFuncCommand+6k   5;  '   5=     �� 5>     $	��5,0q�6'   $��轴心轨迹��64   #��[ 6h   {�.  : 6C   �� *IsOrthoChecked+2 5?  "     5@  &   $3 Checked��65   .4 5A  *     $6 Ortho��6h   5 6@   �� *AddGraphFuncCommand+6h   5B  '     5C  &   $3 	Unchecked��65   .4 5D  *     $6 Ortho��6k   5 6@   �� *RemoveGraphFuncCommand+6k   5E  '   5G     �� 5H     $	��5,0q�6'   $��伯德��64   #��[ 6e   {�.  : 6A   �� *IsBodeChecked+2 5I  "     5J  &   $3 Checked��65   .4 5K  *     $
6 Bode��6h   5 6@   �� *AddGraphFuncCommand+6h   5L  '     5M  &   $3 	Unchecked��65   .4 5N  *     $
6 Bode��6k   5 6@   �� *RemoveGraphFuncCommand+6k   5O  '   5Q     �� 5R     $	��5,0q�6'   $��阶次分析��64   #��[ 6h   {�.  : 6C   �� *IsOrderChecked+2 5S  "     5T  &   $3 Checked��65   .4 5U  *     $6 Order��6h   5 6@   �� *AddGraphFuncCommand+6h   5V  '     5W  &   $3 	Unchecked��65   .4 5X  *     $6 Order��6k   5 6@   �� *RemoveGraphFuncCommand+6k   5Y  '   5[     �� 5\     $	��5,0q�6'   $��奈奎斯特��64   #��[ 6j   {�.  : 6C   �� *IsNyquistChecked+2 5]  "     5^  &   $3 Checked��65   .4 5_  *     $6 Nyquist��6h   5 6@   �� *AddGraphFuncCommand+6h   5`  '     5a  &   $3 	Unchecked��65   .4 5b  *     $6 Nyquist��6k   5 6@   �� *RemoveGraphFuncCommand+6k   5c  '   5e     �� 5g     $	��5,0q�6'   $��转速三维��64   #��[ 6h   {�.  : 6C   �� *IsRPM3DChecked+2 5h  "     5i  &   $3 Checked��65   .4 5j  *     $6 RPM3D��6h   5 6@   �� *AddGraphFuncCommand+6h   5k  '     5l  &   $3 	Unchecked��65   .4 5m  *     $6 RPM3D��6k   5 6@   �� *RemoveGraphFuncCommand+6k   5n  '   5p     5r     5s     5u     � 5w     $��1��6   $��0,5,0,0q�6$   .+�5x     �� $
��Auto=�6#   .��5y     7   1AIC.OnLineDataPage.Controls.ResizableItemsControl  $��Stretch=�6e   $��Stretch=�6�   $��3q�6�   #��[	 5z  6   #��[
 6o   ��5y  >   �� *GraphFunctions+��5{  6   Z� ��6L   Z� 5|     5}     5~     5     5�     5�     5�     