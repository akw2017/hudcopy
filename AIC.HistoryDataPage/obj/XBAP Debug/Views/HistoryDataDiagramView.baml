   M S B A M L   `   `   `  ���� C  ?AIC.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null[;clr-namespace:AIC.Core.OrganizationModels;assembly=AIC.CoreAIC.Core.OrganizationModels  T PWpf.PageNavigationControl, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullhJclr-namespace:Wpf.PageNavigationControl;assembly=Wpf.PageNavigationControlWpf.PageNavigationControl I2clr-namespace:AIC.Core.ExCommand;assembly=AIC.CoreAIC.Core.ExCommand  5(clr-namespace:AIC.Core;assembly=AIC.CoreAIC.Core  G CAIC.CoreType, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullA0clr-namespace:AIC.CoreType;assembly=AIC.CoreTypeAIC.CoreType F BLoya.Dameer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null>.clr-namespace:Loya.Dameer;assembly=Loya.DameerLoya.Dameer N JAIC.HistoryDataPage, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullG(clr-namespace:AIC.HistoryDataPage.ModelsAIC.HistoryDataPage.Models M IWpf.WrapBreakPanel, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullS<clr-namespace:Wpf.WrapBreakPanel;assembly=Wpf.WrapBreakPanelWpf.WrapBreakPanel 6   0AIC.HistoryDataPage.Views.HistoryDataDiagramView   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R	 NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml    	 Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006    	 @d2http://schemas.microsoft.com/expression/blend/2008    	 c
 _System.Windows.Interactivity, Version=4.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity 
 Icontrol;clr-namespace:AIC.Core.OrganizationModels;assembly=AIC.Core 
 P LPrism.Wpf, Version=6.2.0.0, Culture=neutral, PublicKeyToken=91a96d2a154366d8$prismhttp://prismlibrary.com/  UpageJclr-namespace:Wpf.PageNavigationControl;assembly=Wpf.PageNavigationControl  @command2clr-namespace:AIC.Core.ExCommand;assembly=AIC.Core  F BItemsFilter, Version=1.1.0.1, Culture=neutral, PublicKeyToken=nullGbsFilter8http://schemas.bolapansoft.com/xaml/Controls/ItemsFilter  3core(clr-namespace:AIC.Core;assembly=AIC.Core  ?coreType0clr-namespace:AIC.CoreType;assembly=AIC.CoreType  =timeTool.clr-namespace:Loya.Dameer;assembly=Loya.Dameer  5models(clr-namespace:AIC.HistoryDataPage.Models  Gwrap<clr-namespace:Wpf.WrapBreakPanel;assembly=Wpf.WrapBreakPanel  !  Prism.Mvvm.ViewModelLocator    AutoWireViewModel  .    AsyncWorkUserControlStyle#���    � 	Resources  �� MergedDictionaries �� Source   AIC.Core.CommandReference  RemoveCommandReference   Command'   !AIC.Core.EnumDescriptionConverter  enumDescriptionConverter  enumComboItemTemplate �� Path �� 	Converter  enumCombobox �� 
TargetType �� BasedOn	 �� Property
 �� Value  baseCheckBox  ItemsControlStyle �� 
TargetType �� Padding J� SnapsToDevicePixels  SignalMonitorStyle ��Name �� Triggers P� Property P� Value �� Triggers$  AIC.CoreType.SignalDisplayType   RMSTrend �� Value �� Binding  ReplayTemplate   
TimeDomain 	 TimeDomainTemplate   FrequencyDomain 
 FrequencyDomainTemplate   PowerSpectrum  PowerSpectrumTemplate   PowerSpectrumDensity   PowerSpectrumDensityTemplate   Ortho  OrthoTemplate   OffDesignCondition  OffDesignConditionTemplate   OrderAnalysis  OrderAnalysisTemplate   Time3DSpectrum  Time3DSpectrumTemplate   RPM3D  RPM3DSpectrumTemplate   DistributionMap  DistributionMapTemplate    AlarmPointTrend  AlarmPointTrendTemplate  FluidListBoxStyle   AIC.Core.FluidWrapPanel!  Name"   DragOpacity#   	DragScale$   
ItemHeight%   	ItemWidth&   IsComposing'   Orientation(   ElementEasing-  'System.Windows.Media.Animation.BackEase7  1System.Windows.Media.Animation.EasingFunctionBase)   
EasingMode*   	Amplitude+   
DragEasing, �� RelativeSource- �� AncestorType  ToggleExpandButtonStyle  NavigationForegroundBrush  NavigationBackgroundBrush  NavigationBorderBrush'  !System.Windows.VisualStateManager.   VisualStateGroups%	  System.Windows.VisualStateGroup/ 	  Transitions%
  System.Windows.VisualTransition0 
  GeneratedDuration1 	  States   System.Windows.VisualState2   
Storyboard3 g� Duration	4 W� To5 �� TargetProperty6 �� 
TargetName7 F�Name8 �� Stretch9 J� Opacity: � UseLayoutRounding; J� RenderTransformOrigin< �� Angle  ButtonStyle  AccentColorBrush��  �� $K E/AIC.HistoryDataPage;component/Themes/HistoryDataDiagramTemplate.xaml?��� $5 //AIC.Resources;component/Themes/ColorStyle.xaml?��� $4 ./AIC.Resources;component/Themes/ScrollBar.xaml?�%5  &	       &	 $     &	 )     7  &	 L     7��7  &	 p     &	 �     &	 g    7  7 	 7 
 7  7  7  7  7  7  7  7  7  &	 u    &	 �    7d�7  7  7  &	        �� *RemoveDataCommand+  �� .R��� ���� $ .�8   �� " ��8   .��� 	 � ��8
  �� " �� Z� �� " ��.��� 	 � ��$
 Disabled=��� 	 � ��$

 Auto=��� 	 � ��
 . �� 	 �@��PanningMode$

 Both=��� 	 � ��
 �� " ��.R��� #��z��#��z��#��z��# z�� . ._��� |�.  #��z��.���� # z �� " ��.��� 	 � ��
 �� " ��.R��� $ Bd��#��z��#��z��#��z��# z�� . ._��� #��z��# z #��z�� P�  � {� .  . ��� #
 � ��	 � �� �� # Z  �� $ DisplayMode�.a��� 	 � ��8
   �� # Z  �� $ DisplayMode�.a��� 	 � ��8
  �� # Z  �� $ DisplayMode�.a��� 	 � ��8
  �� # Z  �� $ DisplayMode�.a��� 	 � ��8
  �� # Z  �� $ DisplayMode�.a��� 	 � ��8
  �� # Z  �� $ DisplayMode�.a��� 	 � ��8
  �� # Z  �� $ DisplayMode�.a��� 	 � ��8
  �� # Z  �� $ DisplayMode�.a��� 	 � ��8
  �� # Z  �� $ DisplayMode�.a��� 	 � ��8
  �� # Z  �� $ DisplayMode�.a��� 	 � ��8
 	 �� # Z  �� $ DisplayMode�.a��� 	 � ��8
 
 �� # Z   �� $ DisplayMode�.a��� 	 � ��8
  ��  �� *	IsVisible+	 false.a��� 	 � y�$
 	Collapsed=�P�  �@J�IsKeyboardFocusWithin . . ��� 	 �@��
IsSelected
 . �� " ��.��� 	 � ��
 �� .R�  $! fluidWrapPanel��$
��Left=�$	��Top=�}�.  $" 1Q�$# 1Q�$ �� *
ItemHeight+% �� *	ItemWidth+& �� *IsComposing+' �� *Orientation+(   $) EaseOut=�$
* 0.35Q�+   $) EaseOut=�$
* 0.65Q� ��  �� $ IsMulticursor� true.a��� 	 � ��
 �� .R�� ���� *ActualWidth+, �� "- ���� " d�8   .��� 	 �@�Cursor$

 Hand���� 	 � ��8
  �� 	 � ��8
  �� 	 � ��8
  �� 	 � ��$
 1q��� 	 � ��$
 16���� 	 � ��$
 40���� 	 � ��$
 1q��� 	 � ��
 �� " d�.R�� . 	  CommonStates��/ 
  $0 0:0:0.1D�.1   
Normal��  	MouseOver��.2 �� .��W� $3 0D�$4 1X�$5 (UIElement.Opacity)�$6 normal��  Pressed��.2 �� .��W� $3 0D�$4 1X�$5 (UIElement.Opacity)�$6 normal��  Disabled��.2 �� .��W� $3 0D�$	4 0.3X�$5 (UIElement.Opacity)�$6 normal��	  CheckStates��.1   Checked��.2 �� .��W� $3 0:0:0.3D�$4 0X�$5 Angle�$6 symbolRotation��  	Unchecked��.2 �� .��W� $3 0:0:0.3D�$	4 180X�$5 Angle�$6 symbolRotation��  Indeterminate��	  FocusStates��.1   Focused��  	Unfocused��.+��� $��2��$��3��$��40��$��20��#��z��#��z��#��z��F� $7 normal��$��Center=�$��10��$
8 Fill=�$	9 0.6Q������@B ��[ �@B ��[ �ا �ا �@B �ا �pr ����n@�pr ����n@S�pr ����n@��� �ا ��� �ا Ӏ�� �ا ��� ��[ ��� ��[ Ӏ�� ��[ �pr P�^����?�pr P�^����?S�pr P�^����?��[ ��[ 	#��z��: .  $��Center=�$��7��$; 0.5,0.5&�z�V� .~��� �� �� symbolRotation��$	< 180Q�T� �� " ��.��� 	 � ��

 ���� �� 	 � ��#
 �  �� 	 � ��$
 Center=��� 	 � ��$
 Center=��� 	 � ��$
 1q��� 	 � ��
 �� " ��.R�� #��z��.+��� #��z��#��z����. # z #��z�� P�  �@J�IsMouseOver . . ��� 	 � ��
 �	#7F243859P�  � {� .  . ��� 	 � ��
 �#ADADADP�  � �� . . ��� 	 � ��
 �	#FF20437E.��� -   $��grdWorkbench��= � RowDefinitions= �� $
��Auto ��� $��* ��� $��0 �> � ColumnDefinitions> �� $
��Auto ��� $
��Auto ��� $��* �.+�� $��0��$��3��= �� $��3* ��� $
��Auto ��� $��1* �.+�R� -   $��treeview��  VirtualisedMetroTreeView#���  ���� *OrganizationTreeItems+ ? �� DataType?   9AIC.Core.OrganizationModels.OrganizationTreeItemViewModel@ �� ItemsSourceA �� OrientationB ��NameC �� ModeD �� TriggersE �� 
TargetName9   3AIC.Core.OrganizationModels.DeviceTreeItemViewModel7   1AIC.Core.OrganizationModels.ItemTreeItemViewModel9   3AIC.Core.OrganizationModels.DivFreTreeItemViewModel��%  (��       *, +)(�� �    *, +)(�� �    *, +)(�� �    *, +)�� "?  @ �� $ Children�.R��� $A 
Horizontal=�.+�F� $7 IconPath��$��18��$��18��$
8 Fill=�#���  ������Bnx�V��Bn4�Cќ�O�ob�L�!�!�4�C�71�ܽ�,���ܽ���K�����z@����H��р�e�!��8�obф�4�Cф�x�V��Bnx�V�4�C�Śј��d%ј�(�(��ǘ���@@(�(��ǘ���@@����08�����08�(�(����(�(�����d%�4�C�Ś� $��2,0,2,0q�.+��� $B txtName��$
��Auto������ *Name+$C TwoWay=�D �� $ 0�� �� *T_Organization.Level+.a��� $E IconPath��	 � ���
 �� ��:�����:����pej� �pej� ��:���2��x��2��x����Ѥ����Ѥ��2�� ����є�����'t���������������E�'t�E�'t0P�є�0P�є��E� ��E� ��������EѤ����+�Tn5��+�����+�x����+�x���E����E����x��Tn5�x��Tn5�EѤ���E�$� �E�$� �x��$� l't�T�-���\��l't�\���x��\���EѬ<R�EѬ<R�x��Ԙ� �x��Ԙ� �E�$� �E���-���������%����,�U���-�P��V�� �h�V�� P�7���-����� $E IconPath��	 � ��$
 24���� "?  @ �� $ Children�.R��� $A 
Horizontal=�.+�F� $��18��$��18��$
8 Fill=�#���  �����P�x��ݩ(�� ���� p׻\r� $�� �(4��0�	�^)�@@�5������@@�.>�A�G�!اQ���P�x�ز�ݩ������y@@�����p׻�:pΈ"@@$��pP�(4�Ql^�	�^)�@@SP=������@@���A(Ċ�!�L�c����2ز�S�U)�����y@@�� ����d�:pΈ"@@��7!P�Ѵ�� l^����� P=�d*� ��PJ� �Ê��L�c�(�� �2��� �U)\r� �� � �dѐ0�7!є5P�� Ӱ.>��� �Gd*� اQPJ� �P�x�� 6n -1Ӵ�L -1 -1��L -1 6n� -1L���L ?� 6n ?��L� ?� ?�L� ?� 6n� ?���LL� -1 6n -1� $��2,0,2,0q�.+��� $B txtName��$
��Auto������ *Name+$C TwoWay=��� "?  @ �� $ Children�.R��� $A 
Horizontal=�.+�F� $��18��$��18��$
8 Fill=�#���  ������ا  � �@]�  � �@]� ���@�4���@�4 Q%��ا  Q%��ا  � ���� @��р�� ���� -1��^�@������@�4@]� �@�4@fр������ -1���р�� @��� $��2,0,2,0q�.+��� $B txtName��$
��Auto������ *Name+$C TwoWay=�F� $7 DisconnectIcon��$y�	Collapsed=�$��18��$��18��$
8 Fill=�
������������< �K��H2(�T�)�:K)��;H2H2���;)�:K)(�T�2�K�< ����,@@�vW��K@�\�*����ф��@+��@�\��K�vW��,@@< �K�p��_)���@@���*̾;p40"�Ċ��_��T+U� 3���^p<��^�KT+U�+U��K��^p<��^� 3�+U��_�Ċ��"�p40h�;$�*��|����2,k��x
����l��쎂l����kx
�u]ӄ1�NO�#-�NO�J;�u]�` f@+���*�` fѨu]�J;ӜNO�#-�NO�1�u]�
S��k�ʡE��@@����ʡE��@@����
�,k��2Q��_)���@@F� $7 
UnBindIcon��$y�	Collapsed=�$��16��$��16��$
8 Fill=�
�������������HR|�+���|�+�|��<C��|��Āx�&�L�!l�CL�!�䫮L�!\7Āx\7<C���<C���������Trjl�CTrj��/Trj�������<C�ф��+Ѡ�5|�+��5��e��HR��e��HR|�+D �� $ false�� �� *IsConnected+.a��� $E DisconnectIcon��	 � y�$
 Visible=��� $ false�� �� *
IsPaired+.a��� $E DisconnectIcon��	 � y�$
 	Collapsed=��� $E 
UnBindIcon��	 � y�$
 Visible=��� "?  @ �� $ Children�.R��� $A 
Horizontal=�.+�F� $��18��$��18��$
8 Fill=�#���  ����� $�  Q%�v��  Q%�v�� ���р�� ���� H� $� � H� � р�C � р�C��р���� $�  H�� $�  Q%������ р� $� � Q% $� � Q%��� р���� � �  H�� � ��р�� ��р��  H�� �  H�� $��2,0,2,0q�.+��� $B txtName��$
��Auto������ *Name+$C TwoWay=�F� $7 DisconnectIcon��$y�	Collapsed=�$��18��$��18��$
8 Fill=�
������������< �K��H2(�T�)�:K)��;H2H2���;)�:K)(�T�2�K�< ����,@@�vW��K@�\�*����ф��@+��@�\��K�vW��,@@< �K�p��_)���@@���*̾;p40"�Ċ��_��T+U� 3���^p<��^�KT+U�+U��K��^p<��^� 3�+U��_�Ċ��"�p40h�;$�*��|����2,k��x
����l��쎂l����kx
�u]ӄ1�NO�#-�NO�J;�u]�` f@+���*�` fѨu]�J;ӜNO�#-�NO�1�u]�
S��k�ʡE��@@����ʡE��@@����
�,k��2Q��_)���@@D �� $ false�� �� *IsConnected+.a��� $E DisconnectIcon��	 � y�$
 Visible=����� " Q�  LineTreeViewItem# [ .��� 	 �@Q�
IsSelected
 �� *
IsSelected+$C OneTime=��� 	 �@Q�
IsExpanded
 �� *
IsExpanded+$C OneTime=�. 
 (System.Windows.Interactivity.InteractionF   TriggersF / 
 )System.Windows.Interactivity.EventTrigger  G   	EventName$G SelectedItemChanged��. 
 (System.Windows.Interactivity.TriggerBaseH   Actions.H 6 
 0System.Windows.Interactivity.InvokeCommandAction  I   CommandI �� *SelectedTreeChangedComamnd+J   CommandParameterJ �� *SelectedItem+K �� ElementName$K treeview��  $G MouseDoubleClick��.H   I �� *DoubleClickAddDataCommand+J �� *SelectedItem+$K treeview���� $��1��$��3��#���  $��Stretch=�$��Center=�R� -   $��	treeview2��$��2��#���  ���� *RecycledTreeItems+ ��%�  (��       *, +)(�� ,    *, +)�� "?  @ �� $ Children�.R��� $A 
Horizontal=�.+�F� $7 IconPath��$��18��$��18��$
8 Fill=�#���  �����4��pej��pej�ltpej;t��;t��шopt��ӈopt���6upej4��pej�(�T�-��hT�-Ө�nT�-�;�H|?�;�(FӜ;���L��n@Q^�h@Q^�(�@Q^�X�@Q^ o��L o(F� oH|?X�T�-(�T�-��˦ ���� ��7� �8�D��8�H	�����n5���X�BP�|�ML��|�MѴ��|�MӰ��|�M�˦X�B�˦�n5�Ȣ�H	��Ȣ�D���s� ��˦ ����2��`��E��Z��EѸP�2����2��+2�Ѽ�+�E�D�[�E�D�[2�Ѽ�+2��HZ�2��0P��EѠ���E���2��HZ�2��� $A 
Horizontal=�$��2,0,2,0q�.+��� $
��Auto������ *Name+$C TwoWay=��� $��:��$
��Auto���� $
��Auto������ *
ServerIP+$C TwoWay=��� "?  @ �� $ Children�.R��� $A 
Horizontal=�.+�F� $��18��$��18��$
8 Fill=�#���  ������ا  � �@]�  � �@]� ���@�4���@�4 Q%��ا  Q%��ا  � ���� @��р�� ���� -1��^�@������@�4@]� �@�4@fр������ -1���р�� @��� $��2,0,2,0q�.+��� $
��Auto������ *Name+$C TwoWay=����� " Q�# [ .��� 	 �@Q�
IsSelected
 �� *
IsSelected+$C OneTime=��� 	 �@Q�
IsExpanded
 �� *
IsExpanded+$C OneTime=�F   $G SelectedItemChanged��.H   I �� *SelectedTreeChangedComamnd+J �� *SelectedItem+$K 	treeview2��  $G MouseDoubleClick��.H   I �� *DoubleClickAddDataCommand+J �� *SelectedItem+$K 	treeview2��.@�   �� -   L ��Name$L gsSplitterr��$��0��$��1��$��3��$��3��#���  $��Center=�$��Stretch=�  GridSplitterStyleExpend#��[ �� $��0��$��2��$ 2q�  AccentColorBrush2#���  ._�'  !Wpf.WrapBreakPanel.WrapBreakPanel  .+��� $��测点:��$��Center=��� $��4,0,0,0q�  MetroTextBox#���  $	��200��$��Center=�$��Center=�M �� 
IsReadOnlyM . ���� $ SelectedTreeItem.Name��� $��Stretch=�$��2��#���  $	��5,0q��� $��数据上限:��$��Center=��� $	��100��#���  {�.  $��2,0,0,0q����� *ChannelRecordLength+�� $��数据类型:��$��Center=�$��2,0,0,0q��� $	��120��   VirtualisedMetroComboBox#���   N �� 
IsEditableN .  ���� *UnitCategory+O �� SelectedItemO �� *Unit+�� $��触发条件:��$��Center=�$��2,0,0,0q��� N .  $	��120��#��[    AIC.Core.EnumHelperP   Enum  AIC.CoreType.TriggerType"P  #���   $��2,0,0,0q�O �� *TriggerFilter+�� Q   LineBreakBeforeQ . $��Stretch=�$��2��#���  $	��5,0q�$y�	Collapsed=��� $��转速�� ! CheckBoxStyle#��[! $��Center=�$y�	Collapsed=�R d� 	IsCheckedR �� *AllowRPMFilter+�� $��起始转速:��$��Center=�$��2,0,0,0q�$y�	Collapsed=��� $	��100��#���  $��2,0,0,0q�$y�	Collapsed=����� *DownRPMFilter+�� $��终止转速:��$��Center=�$��2,0,0,0q�$y�	Collapsed=��� $	��100��#���  $��2,0,0,0q�$y�	Collapsed=����� *UpRPMFilter+�� Q . $��Stretch=�$��2��#���  $	��5,0q��� $��	正常值��#��[! $��Center=�R �� *AllowNormal+�� $��	预警值��#��[! $��Center=�$��2,0,0,0q�R �� *AllowPreWarning+�� $��	警告值��#��[! $��Center=�$��2,0,0,0q�R �� *AllowWarning+�� $��	危险值��#��[! $��Center=�$��2,0,0,0q�R �� *AllowDanger+�� $��	无效值��#��[! $��Center=�$��2,0,0,0q�R �� *AllowInvalid+�� $��Stretch=�$��2��#���  $	��5,0q��� $��查询时间��$��Center=�$��2,0,0,0q�  Loya.Dameer.Dameer  S   Format$S Custom=�T   CustomFormat$T yyyy-MM-dd HH:mm:ss��U   ShowCheckBoxU .  #���  $��Center=�V   ValueV �� *	StartTime+��  " end#��� " $��2,0,0,0q�$��Center=�  $S Custom=�$T yyyy-MM-dd HH:mm:ss��U .  #���  $��Center=�V �� *	EndTime+�� $��Stretch=�$��2��#���  $	��5,0q���  # AccentedSquareButtonStyle#��� # $��4,0,0,0q����� *AddDataCommand+���� $A 
Horizontal=�.+��� $
��None=�$-��'/AIC.Resources;component/Images/add.png����  $ add#��� $ $��Center=��� #��� # $��4,0,0,0q����� *RefreshDataCommand+���� $A 
Horizontal=�.+��� $
��None=�$1��+/AIC.Resources;component/Images/refresh.png����  % refresh#��� % $��Center=��� #��� # $��4,0,0,0q����� *ClearDataCommand+���� $A 
Horizontal=�.+��� $
��None=�$1��+/AIC.Resources;component/Images/delete3.png����  	& clear#��� & $��Center=��� $��Stretch=�$��2��#���  $	��5,0q��� $��诊断设备:��$��Center=��� $��4,0,0,0q�#���  $	��200��$��Center=�$��Center=�M . ���� $! SelectedDeviceTreeItem.Name��� #��� # $��4,0,0,0q����� *DeviceDiagnoseDataCommand+���� $A 
Horizontal=�.+��� $
��None=�$-��'/AIC.Resources;component/Images/add.png����  ' addData#��� ' $��Center=��� #��� # $��4,0,0,0q����� *DeviceDiagnoseCommand+���� $A 
Horizontal=�.+��� $
��None=�$3��-/AIC.Resources;component/Images/lightbulb.png����  ( menuDiagnose#��� ( $��Center=�� $��1��$��2��$��0,1,0,0q�= �� $
��Auto ��� $��* �> �� $��* ��� $
��Auto ��� $	��100 �.+�� $��0��$��0��> �� $��* ��� $
��auto �.+�� $��Disabled=�.+��� $	��5,0q�$��趋势��#��[! R �� *	ShowAMS+�� $	��5,0q�$��时域��#��[! R �� *ShowTimeDomain+�� $	��5,0q�$��频域��#��[! R �� *ShowFrequencyDomain+�� $	��5,0q�$��	功率谱��#��[! R �� *ShowPowerSpectrum+�� $	��5,0q�$��功率谱密度��#��[! R �� *ShowPowerSpectrumDensity+�� $	��5,0q�$��轴心轨迹��#��[! {�.  R �� *	ShowOrtho+�� $	��5,0q�$��	变工况��#��[! {�.  R �� *ShowOffCondition+�� $	��5,0q�$��	分布图��#��[! R �� *ShowDistributionMap+�� $	��5,0q�#��[! $��Center=�$��Right=�$��双光标模式��R �� *IsMulticursor+�� -   $��24��$��24��$��0,0,5,0q�.���� $
��Fill=�$9��3/AIC.Resources;component/Images/HorizontalAlign.png��W � ToolTipW _� $��水平对齐���� -   $��24��$��24��$��0,0,5,0q�.���� $
��Fill=�$7��1/AIC.Resources;component/Images/VerticalAlign.png��W _� $��垂直对齐���� -   $��24��$��24��$��0,0,5,0q�.���� $
��Fill=�$/��)/AIC.Resources;component/Images/Align.png��W _� $��平均对齐��� $��1��$��0��$��0,0,0,0q�= �� $
��Auto ��� $��* �> �� $��* ��� $
��auto �.+��� -   X ��Name$X scrollviewer��$��1��$��0��$��Disabled=�.���� -	   Y ��Name$Y ReplayListBox��$��Stretch=�$��Stretch=�$��Disabled=�$��Disabled=�#��[ ��Z� ��Z� ���� *HistoricalDatas+O �� *SelectedHistoricalData+���� .R�� ���� *ActualWidth+, �� "- ���� $��0��$��0�� ) 
FlatSlider#��� ) Z � Minimum$	Z 120Q�[ � Maximum$
[ 1550Q�\ �� Orientation$\ 
Horizontal=�] � Value] �� *	ItemWidth+^ �� Delay$	^ 200���� $��1��$��1��#��� ) $	Z 120Q�$	[ 600Q�$\ Vertical=�] �� *
ItemHeight+$	^ 200���� $��0��$��1��$W 允许拖拽��$y�	Collapsed=�R �� *IsComposing+$C TwoWay=���Z� �� $��0��$��1��$��2��$��3��#���  $��Center=�$��Stretch=��� $��0��$��2��$��2��$��Stretch=�$��Stretch=�$��Disabled=���Z� ��Z� ���� *AddedChannels+O �� *SelectedChannel+ -  'AIC.HistoryDataPage.Models.ChannelToken * DeepLinearAccentColorBrush��%+  (��       *, +)7  �� "?  .R��� $��1q�#���  ._�� $
��Auto��$��80��#��� * .+��� $��16(�$
��Wrap=�$��Center=�$��Center=����� *DisplayName+�� 8��  $
��Bold"�$��24��$��24��$��Right=�$	��Top=����� ���� $A 
Horizontal=�.+��� $��X��#���  $��Center=����� .R�� _ � Orientation$_ Vertical=�