   M S B A M L   `   `   `  ����C  ?AIC.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null[;clr-namespace:AIC.Core.OrganizationModels;assembly=AIC.CoreAIC.Core.OrganizationModels  5      T PWpf.PageNavigationControl, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullhJclr-namespace:Wpf.PageNavigationControl;assembly=Wpf.PageNavigationControlWpf.PageNavigationControl 5
      I2clr-namespace:AIC.Core.ExCommand;assembly=AIC.CoreAIC.Core.ExCommand  5      5(clr-namespace:AIC.Core;assembly=AIC.CoreAIC.Core  5      G CAIC.CoreType, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullA0clr-namespace:AIC.CoreType;assembly=AIC.CoreTypeAIC.CoreType 5      F BLoya.Dameer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null>.clr-namespace:Loya.Dameer;assembly=Loya.DameerLoya.Dameer 5      N JAIC.HistoryDataPage, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullG(clr-namespace:AIC.HistoryDataPage.ModelsAIC.HistoryDataPage.Models 5      M IWpf.WrapBreakPanel, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullS<clr-namespace:Wpf.WrapBreakPanel;assembly=Wpf.WrapBreakPanelWpf.WrapBreakPanel 5      6   0AIC.HistoryDataPage.Views.HistoryDataDiagramView   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R	 NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml    	 5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006    	 5      @d2http://schemas.microsoft.com/expression/blend/2008    	 5      c
 _System.Windows.Interactivity, Version=4.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity 
 5      Icontrol;clr-namespace:AIC.Core.OrganizationModels;assembly=AIC.Core 
 5      P LPrism.Wpf, Version=6.2.0.0, Culture=neutral, PublicKeyToken=91a96d2a154366d8$prismhttp://prismlibrary.com/  5      UpageJclr-namespace:Wpf.PageNavigationControl;assembly=Wpf.PageNavigationControl  5
      @command2clr-namespace:AIC.Core.ExCommand;assembly=AIC.Core  5      F BItemsFilter, Version=1.1.0.1, Culture=neutral, PublicKeyToken=nullGbsFilter8http://schemas.bolapansoft.com/xaml/Controls/ItemsFilter  5      3core(clr-namespace:AIC.Core;assembly=AIC.Core  5      ?coreType0clr-namespace:AIC.CoreType;assembly=AIC.CoreType  5      =timeTool.clr-namespace:Loya.Dameer;assembly=Loya.Dameer  5      5models(clr-namespace:AIC.HistoryDataPage.Models  5      Gwrap<clr-namespace:Wpf.WrapBreakPanel;assembly=Wpf.WrapBreakPanel  5      !  Prism.Mvvm.ViewModelLocator    AutoWireViewModel  .    AsyncWorkUserControlStyle#���   5       � 	Resources 5       �� MergedDictionaries �� Source   AIC.Core.CommandReference  RemoveCommandReference   Command'   !AIC.Core.EnumDescriptionConverter  enumDescriptionConverter  enumComboItemTemplate �� Path �� 	Converter  enumCombobox �� 
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
TargetName7 F�Name8 �� Stretch9 J� Opacity: � UseLayoutRounding; J� RenderTransformOrigin< �� Angle  ButtonStyle  AccentColorBrush�� 5   
    5      �� 5      $K E/AIC.HistoryDataPage;component/Themes/HistoryDataDiagramTemplate.xaml?�6%   �� 5      $5 //AIC.Resources;component/Themes/ColorStyle.xaml?�6%   �� 5      $4 ./AIC.Resources;component/Themes/ScrollBar.xaml?�6%   %  &	       &	 2     &	 E     7  &	 �     7��7  &	 �     &	 �     &	 �    7  7 	 7 
 7  7  7  7  7  7  7  7  7  &	 �	    &	 b    7d�7  7  7  &	 �      5       6C   �� *RemoveDataCommand+  5      6,   �� 5      .R�5      �� ��6   �� $ .�8   5       �� 5!      " ��6)   8   .�5"      �� 	 � ��8
  61   5#      �� 5$      " ��6)    6H   Z� �� 5%      " ��6.   .�5&      �� 	 � ��$
 Disabled=�6O   �� 5'      	 � ��$

 Auto=�6M   �� 5(      	 � ��
 . 6B   �� 5)      	 �@��PanningMode$

 Both=�6=   �� 5*      	 � ��
 5+      �� 5,      " ��6*   .R�5-      �� #��z��6%   #��z��6Q   #��z��6�   # z��6�    . ._�5.   "   �� |�.  #��z��6A   .��5/   &   �� # z 65   50   #   51      52      54      55      �� 56      " ��6   .�57      �� 	 � ��
 58      �� 59      " ��6*   .R�5:      �� $ Bd��6%   #��z��61   #��z��6]   #��z��6�   # z��6�    . ._�5;   "   �� #��z��63   # z 6v   #��z��6�   5<       5=      P� 5>   "    � {� .  . �5?   &   �� #
 � ��6C   	 � ��5@   #   5B      5D       5F      �� 5G      # Z 6G    6"   �� $ DisplayMode�.a�5H      �� 	 � ��8
   6<   5I      �� 5J      # Z 6G    6"   �� $ DisplayMode�.a�5K      �� 	 � ��8
  6<   5L      �� 5M      # Z 6G    6"   �� $ DisplayMode�.a�5N      �� 	 � ��8
  6<   5O      �� 5P      # Z 6G    6"   �� $ DisplayMode�.a�5Q      �� 	 � ��8
  6<   5R      �� 5S      # Z 6G    6"   �� $ DisplayMode�.a�5T      �� 	 � ��8
  6<   5U      �� 5V      # Z 6G    6"   �� $ DisplayMode�.a�5W      �� 	 � ��8
  6<   5X      �� 5Y      # Z 6G    6"   �� $ DisplayMode�.a�5Z      �� 	 � ��8
  6<   5[      �� 5\      # Z 6G    6"   �� $ DisplayMode�.a�5]      �� 	 � ��8
  6<   5^      �� 5_      # Z 6G    6"   �� $ DisplayMode�.a�5`      �� 	 � ��8
  6<   5a      �� 5b      # Z 6G    6"   �� $ DisplayMode�.a�5c      �� 	 � ��8
 	 6<   5d      �� 5e      # Z 6G    6"   �� $ DisplayMode�.a�5f      �� 	 � ��8
 
 6<   5g      �� 5h      # Z  6G    6"   �� $ DisplayMode�.a�5i      �� 	 � ��8
  6<   5j      �� 5k       6"   �� *	IsVisible+	 false6@   .a�5l      �� 	 � y�$
 	Collapsed=�67   5m      P� 5n       �@J�IsKeyboardFocusWithin . . �5o      �� 	 �@��
IsSelected
 . 67   5p      5r      �� 5s      " ��6.   .�5t      �� 	 � ��
 5u      �� 5v      .R�5w        $! fluidWrapPanel��62   $
��Left=�6J   $	��Top=�6e   }�.  $" 1Q�6  $# 1Q�6,  $ 6�   �� *
ItemHeight+% 6�   �� *	ItemWidth+& 6�   �� *IsComposing+' 6�   �� *Orientation+( 5x   "     5y   &   $) EaseOut=�6/   $
* 0.35Q�6D   6W   + 5{   "     5|   &   $) EaseOut=�6/   $
* 0.65Q�6D   6W   5~      5      5�       5�      �� 5�       6"   �� $ IsMulticursor� true6I   .a�5�      �� 	 � ��
 5�      �� 5�   "   .R�5�   &   � ��60   �� *ActualWidth+, �� "- ��5�   #   5�      5�      5�      �� 5�      " d�64   8   .�5�      �� 	 �@�Cursor$

 Hand��6+   �� 5�      	 � ��8
  6/   �� 5�      	 � ��8
  6/   �� 5�      	 � ��8
  60   �� 5�      	 � ��$
 1q�64   �� 5�      	 � ��$
 16��6+   �� 5�      	 � ��$
 40��6*   �� 5�      	 � ��$
 1q�64   �� 5�      	 � ��
 5�      �� 5�      " d�6*   .R�5�      � . 5�   "   	  5�   &   CommonStates��/ 5�   *   
  5�   .   $0 0:0:0.1D�6?   .1 5�   *     
Normal��66     5�   *   	MouseOver��.2 5�   .   �� .��5�   2   W� $3 0D�6B   $4 1X�6O   $5 (UIElement.Opacity)�6V   $6 normal��6�   5�   /   5�   +     5�   *   Pressed��.2 5�   .   �� .��5�   2   W� $3 0D�6B   $4 1X�6O   $5 (UIElement.Opacity)�6V   $6 normal��6�   5�   /   5�   +     5�   *   Disabled��.2 5�   .   �� .��5�   2   W� $3 0D�6B   $	4 0.3X�6O   $5 (UIElement.Opacity)�6X   $6 normal��6�   5�   /   5�   +   5�   '   	  5�   &   CheckStates��.1 5�   *     Checked��.2 5�   .   �� .��5�   2   W� $3 0:0:0.3D�6B   $4 0X�6U   $5 Angle�6\   $6 symbolRotation��6~   5�   /   5�   +     5�   *   	Unchecked��.2 5�   .   �� .��5�   2   W� $3 0:0:0.3D�6B   $	4 180X�6U   $5 Angle�6^   $6 symbolRotation��6�   5�   /   5�   +     5�   *   Indeterminate��66   5�   '   	  5�   &   FocusStates��.1 5�   *     Focused��66     5�   *   	Unfocused��66   5�   '   .+�5�   "   �� $��2��6)   $��3��66   $��40��6J   $��20��6U   #��z��6a   #��z��6�   #��z��6�   F� 5�   "   $7 normal��6'   $��Center=�67   $��10��6T   $
8 Fill=�6_   $	9 0.6Q�6n   �����@B ��[ �@B ��[ �ا �ا �@B �ا �pr ����n@�pr ����n@S�pr ����n@��� �ا ��� �ا Ӏ�� �ا ��� ��[ ��� ��[ Ӏ�� ��[ �pr P�^����?�pr P�^����?S�pr P�^����?��[ ��[ 	#��z��6   : .  $��Center=�6^  $��7��6y  $; 0.5,0.5&�6�  z�5�   &   V� 5�   *   .~�5�   .   �� �� 5�   .   �� 5�   .   symbolRotation��$	< 180Q�6V   T� 5�   .   5�   +   5�   #   5�      5�      5�      5�      �� 5�      " ��6(   .�5�      �� 	 � ��

 ���� 6/   �� 5�      	 � ��#
 �  6/   �� 5�      	 � ��$
 Center=�6?   �� 5�      	 � ��$
 Center=�6=   �� 5�      	 � ��$
 1q�6,   �� 5�      	 � ��
 5�      �� 5�      " ��6*   .R�5�      � #��z��6#   .+�5�   "   �� #��z��63   #��z��6v   ��. # z 6�   #��z��6�   5�       5�      P� 5�   "    �@J�IsMouseOver . . �5�   &   �� 	 � ��
 �	#7F2438596C   5�   #   P� 5�   "    � {� .  . �5�   &   �� 	 � ��
 �#ADADAD6C   5�   #   P� 5�   "    � �� . . �5�   &   �� 	 � ��
 �	#FF20437E6C   5�   #   5�      5�      5�      5�      .��5�      � -   5        $��grdWorkbench��5�      = � RowDefinitions= 5�   
   �� 5�      $
��Auto �6   �� 5�      $��* �6   �� 5�      $��0 �6   > � ColumnDefinitions> 5�   
   �� 5�      $
��Auto �6   �� 5�      $
��Auto �6   �� 5�      $��* �6   .+�5�   
   � $��0��6   $��3��6   = 5�      �� 5      $��3* �6    �� 5     $
��Auto �6    �� 5     $��1* �6    .+�5     R� -   5        $��treeview��5       VirtualisedMetroTreeView#���  5     ��5     �� *OrganizationTreeItems+ 5     ? �� DataType?   9AIC.Core.OrganizationModels.OrganizationTreeItemViewModel@ �� ItemsSourceA �� OrientationB ��NameC �� ModeD �� TriggersE �� 
TargetName9   3AIC.Core.OrganizationModels.DeviceTreeItemViewModel7   1AIC.Core.OrganizationModels.ItemTreeItemViewModel9   3AIC.Core.OrganizationModels.DivFreTreeItemViewModel��%�  (��       5  /   *, +)(�� �    5  /   *, +)(�� H    5  /   *, +)(�� B    54  /   *, +)�� 5     "?  6/   @ 6i   �� $ Children�.R�5	     �� $A 
Horizontal=�6%   .+�5
     F� $7 IconPath��6#   $��18��65   $��18��6@   $
8 Fill=�6L   #���  6[   ������Bnx�V��Bn4�Cќ�O�ob�L�!�!�4�C�71�ܽ�,���ܽ���K�����z@����H��р�e�!��8�obф�4�Cф�x�V��Bnx�V�4�C�Śј��d%ј�(�(��ǘ���@@(�(��ǘ���@@����08�����08�(�(����(�(�����d%�4�C�Ś6�   � 5     $��2,0,2,0q�6#   .+�5  "   �� $B txtName��6,   $
��Auto��6`   ��6=   �� *Name+$C TwoWay=�6`   5     5     D 5     �� 5     $ 0��6S    6*   �� *T_Organization.Level+.a�5  "   �� $E IconPath��6)   	 � ���
 �� ��:�����:����pej� �pej� ��:���2��x��2��x����Ѥ����Ѥ��2�� ����є�����'t���������������E�'t�E�'t0P�є�0P�є��E� ��E� ��������EѤ����+�Tn5��+�����+�x����+�x���E����E����x��Tn5�x��Tn5�EѤ���E�$� �E�$� �x��$� l't�T�-���\��l't�\���x��\���EѬ<R�EѬ<R�x��Ԙ� �x��Ԙ� �E�$� �E���-���������%����,�U���-�P��V�� �h�V�� P�7���-���6O   �� 5  "   $E IconPath��6)   	 � ��$
 24��6P   5     5     �� 5     "?  6/   @ 6c   �� $ Children�.R�5     �� $A 
Horizontal=�6%   .+�5     F� $��18��6#   $��18��6.   $
8 Fill=�6:   #���  6I   �����P�x��ݩ(�� ���� p׻\r� $�� �(4��0�	�^)�@@�5������@@�.>�A�G�!اQ���P�x�ز�ݩ������y@@�����p׻�:pΈ"@@$��pP�(4�Ql^�	�^)�@@SP=������@@���A(Ċ�!�L�c����2ز�S�U)�����y@@�� ����d�:pΈ"@@��7!P�Ѵ�� l^����� P=�d*� ��PJ� �Ê��L�c�(�� �2��� �U)\r� �� � �dѐ0�7!є5P�� Ӱ.>��� �Gd*� اQPJ� �P�x�� 6n -1Ӵ�L -1 -1��L -1 6n� -1L���L ?� 6n ?��L� ?� ?�L� ?� 6n� ?���LL� -1 6n -16s   � 5     $��2,0,2,0q�6#   .+�5  "   �� $B txtName��6,   $
��Auto��6`   ��6=   �� *Name+$C TwoWay=�6`   5     5     5     �� 5     "?  6/   @ 6a   �� $ Children�.R�5     �� $A 
Horizontal=�6%   .+�5      F� $��18��6#   $��18��6.   $
8 Fill=�6:   #���  6I   ������ا  � �@]�  � �@]� ���@�4���@�4 Q%��ا  Q%��ا  � ���� @��р�� ���� -1��^�@������@�4@]� �@�4@fр������ -1���р�� @��6s   � 5!     $��2,0,2,0q�6#   .+�5"  "   �� $B txtName��6,   $
��Auto��6`   ��6=   �� *Name+$C TwoWay=�6`   5#     F� 5'     $7 DisconnectIcon��6#   $y�	Collapsed=�6;   $��18��6R   $��18��6]   $
8 Fill=�6i   
������������< �K��H2(�T�)�:K)��;H2H2���;)�:K)(�T�2�K�< ����,@@�vW��K@�\�*����ф��@+��@�\��K�vW��,@@< �K�p��_)���@@���*̾;p40"�Ċ��_��T+U� 3���^p<��^�KT+U�+U��K��^p<��^� 3�+U��_�Ċ��"�p40h�;$�*��|����2,k��x
����l��쎂l����kx
�u]ӄ1�NO�#-�NO�J;�u]�` f@+���*�` fѨu]�J;ӜNO�#-�NO�1�u]�
S��k�ʡE��@@����ʡE��@@����
�,k��2Q��_)���@@6�   F� 5(     $7 
UnBindIcon��6#   $y�	Collapsed=�67   $��16��6N   $��16��6Y   $
8 Fill=�6e   
�������������HR|�+���|�+�|��<C��|��Āx�&�L�!l�CL�!�䫮L�!\7Āx\7<C���<C���������Trjl�CTrj��/Trj�������<C�ф��+Ѡ�5|�+��5��e��HR��e��HR|�+6�   5)     D 5*     �� 5+     $ false��6J    6*   �� *IsConnected+.a�5,  "   �� $E DisconnectIcon��6)   	 � y�$
 Visible=�6[   5-     �� 5.     $ false��6G    6*   �� *
IsPaired+.a�5/  "   �� $E DisconnectIcon��6)   	 � y�$
 	Collapsed=�6[   �� 50  "   $E 
UnBindIcon��6)   	 � y�$
 Visible=�6W   51     53     �� 54     "?  6/   @ 6c   �� $ Children�.R�55     �� $A 
Horizontal=�6%   .+�56     F� $��18��6#   $��18��6.   $
8 Fill=�6:   #���  6I   ����� $�  Q%�v��  Q%�v�� ���р�� ���� H� $� � H� � р�C � р�C��р���� $�  H�� $�  Q%������ р� $� � Q% $� � Q%��� р���� � �  H�� � ��р�� ��р��  H�� �  H�6s   � 57     $��2,0,2,0q�6#   .+�58  "   �� $B txtName��6,   $
��Auto��6`   ��6=   �� *Name+$C TwoWay=�6`   59     F� 5:     $7 DisconnectIcon��6#   $y�	Collapsed=�6;   $��18��6R   $��18��6]   $
8 Fill=�6i   
������������< �K��H2(�T�)�:K)��;H2H2���;)�:K)(�T�2�K�< ����,@@�vW��K@�\�*����ф��@+��@�\��K�vW��,@@< �K�p��_)���@@���*̾;p40"�Ċ��_��T+U� 3���^p<��^�KT+U�+U��K��^p<��^� 3�+U��_�Ċ��"�p40h�;$�*��|����2,k��x
����l��쎂l����kx
�u]ӄ1�NO�#-�NO�J;�u]�` f@+���*�` fѨu]�J;ӜNO�#-�NO�1�u]�
S��k�ʡE��@@����ʡE��@@����
�,k��2Q��_)���@@6�   5;     D 5<     �� 5=     $ false��6J    6*   �� *IsConnected+.a�5>  "   �� $E DisconnectIcon��6)   	 � y�$
 Visible=�6[   5?     5A     5B     ��5D     �� 5E     " Q�6     LineTreeViewItem# [ 66   .�5F     �� 	 �@Q�
IsSelected
 67   �� *
IsSelected+$C OneTime=��� 5G     	 �@Q�
IsExpanded
 67   �� *
IsExpanded+$C OneTime=�5H     . 
 (System.Windows.Interactivity.InteractionF   TriggersF 5K     / 
 )System.Windows.Interactivity.EventTrigger  5L     G   	EventName$G SelectedItemChanged��6%   . 
 (System.Windows.Interactivity.TriggerBaseH   Actions.H 5M     6 
 0System.Windows.Interactivity.InvokeCommandAction  I   CommandI 60   �� *SelectedTreeChangedComamnd+J   CommandParameterJ 6_   �� *SelectedItem+K �� ElementName$K treeview��5N       5O     $G MouseDoubleClick��6%   .H 5P       I 60   �� *DoubleClickAddDataCommand+J 6^   �� *SelectedItem+$K treeview��5Q     5T     �� 5U     $��1��6   $��3��6(   #���  63   $��Stretch=�6c   $��Center=�6�   R� 5V     -   5        $��	treeview2��5V     $��2��6(   #���  5X     ��5W     �� *RecycledTreeItems+ 5Y     ��%�  (��       5Z  /   *, +)(�� �    5d  /   *, +)�� 5Z     "?  6/   @ 6i   �� $ Children�.R�5[     �� $A 
Horizontal=�6%   .+�5\     F� $7 IconPath��6#   $��18��65   $��18��6@   $
8 Fill=�6L   #���  6[   �����4��pej��pej�ltpej;t��;t��шopt��ӈopt���6upej4��pej�(�T�-��hT�-Ө�nT�-�;�H|?�;�(FӜ;���L��n@Q^�h@Q^�(�@Q^�X�@Q^ o��L o(F� oH|?X�T�-(�T�-��˦ ���� ��7� �8�D��8�H	�����n5���X�BP�|�ML��|�MѴ��|�MӰ��|�M�˦X�B�˦�n5�Ȣ�H	��Ȣ�D���s� ��˦ ����2��`��E��Z��EѸP�2����2��+2�Ѽ�+�E�D�[�E�D�[2�Ѽ�+2��HZ�2��0P��EѠ���E���2��HZ�2�6�   �� 5]     $A 
Horizontal=�6)   $��2,0,2,0q�6B   .+�5^  "   �� $
��Auto��6P   ��6,   �� *Name+$C TwoWay=�6P   �� 5_  "   $��:��6,   $
��Auto��65   �� 5`  "   $
��Auto��6T   ��6,   �� *
ServerIP+$C TwoWay=�6T   5a     5b     5c     �� 5d     "?  6/   @ 6a   �� $ Children�.R�5e     �� $A 
Horizontal=�6%   .+�5f     F� $��18��6#   $��18��6.   $
8 Fill=�6:   #���  6I   ������ا  � �@]�  � �@]� ���@�4���@�4 Q%��ا  Q%��ا  � ���� @��р�� ���� -1��^�@������@�4@]� �@�4@fр������ -1���р�� @��6s   � 5g     $��2,0,2,0q�6#   .+�5h  "   �� $
��Auto��6P   ��6,   �� *Name+$C TwoWay=�6P   5i     5j     5k     5l     ��5n     �� 5o     " Q�6   # [ 66   .�5p     �� 	 �@Q�
IsSelected
 67   �� *
IsSelected+$C OneTime=��� 5q     	 �@Q�
IsExpanded
 67   �� *
IsExpanded+$C OneTime=�5r     F 5u       5v     $G SelectedItemChanged��6%   .H 5w       I 60   �� *SelectedTreeChangedComamnd+J 6_   �� *SelectedItem+$K 	treeview2��5x       5y     $G MouseDoubleClick��6%   .H 5z       I 60   �� *DoubleClickAddDataCommand+J 6_   �� *SelectedItem+$K 	treeview2��5{     .@�5|  *      5~     5     �� 5�  
   -   5        L ��Name$L gsSplitterr��5�     $��0��6,   $��1��69   $��3��6I   $��3��6Z   #���  6d   $��Center=�6�   $��Stretch=�6�     GridSplitterStyleExpend#��[ 6�   �� 5�  
   $��0��6   $��2��6   $ 2q�6.     AccentColorBrush2#���  6:   ._�5�     '  !Wpf.WrapBreakPanel.WrapBreakPanel  .+�5�     �� $��
选择项:��6   $��Center=�6(   �� 5�     $��4,0,0,0q�6^     MetroTextBox#���  6o   $	��200��6�   $��Center=�6�   $��Center=�6�   M �� 
IsReadOnlyM . N � DataContextN 6   �� *SelectedTreeItem+��6C   �� $
 Name�6�   �� 5�     $��Stretch=�6   $��2��68   #���  6C   $	��5,0q�6o   �� 5�     $��数据上限:��6   $��Center=�6)   �� 5�     $	��100��6?   #���  6K   {�.  $��2,0,0,0q�6�   ��6   �� *ChannelRecordLength+6�   �� 5�     $��数据类型:��6   $��Center=�6)   $��2,0,0,0q�6D   �� 5�     $	��120��6      VirtualisedMetroComboBox#���   6j   O �� 
IsEditableO .  ��6'   �� *UnitCategory+P �� SelectedItemP 6L   �� *Unit+6�   �� 5�     $��触发条件:��6   $��Center=�6)   $��2,0,0,0q�6D   �� 5�     O .  $	��120��6.   #��[ 6:      AIC.Core.EnumHelperQ   Enum  AIC.CoreType.TriggerType"Q  6|   #���   6�   $��2,0,0,0q�6  P 6�   �� *TriggerFilter+6  �� 5�     R   LineBreakBeforeR . $��Stretch=�6G   $��2��6c   #���  6n   $	��5,0q�6�   $y�	Collapsed=�6�   �� 5�     $��转速��6    ! CheckBoxStyle#��[! 6(   $��Center=�6t   $y�	Collapsed=�6�   S d� 	IsCheckedS 6O   �� *AllowRPMFilter+6�   �� 5�     $��起始转速:��6   $��Center=�6)   $��2,0,0,0q�6D   $y�	Collapsed=�6U   �� 5�     $	��100��69   #���  6E   $��2,0,0,0q�6l   $y�	Collapsed=�6}   ��6   �� *DownRPMFilter+6}   �� 5�     $��终止转速:��6   $��Center=�6)   $��2,0,0,0q�6D   $y�	Collapsed=�6U   �� 5�     $	��100��6   #���  6C   $��2,0,0,0q�6j   $y�	Collapsed=�6{   ��6&   �� *UpRPMFilter+6{   �� 5�     R . $��Stretch=�6G   $��2��6c   #���  6m   $	��5,0q�6�   �� 5�     $��	正常值��6   #��[! 6)   $��Center=�6s   S 6P   �� *AllowNormal+6s   �� 5�     $��	预警值��6   #��[! 6)   $��Center=�6v   $��2,0,0,0q�6�   S 6P   �� *AllowPreWarning+6�   �� 5�     $��	警告值��6   #��[! 6)   $��Center=�6s   $��2,0,0,0q�6�   S 6P   �� *AllowWarning+6�   �� 5�     $��	危险值��6   #��[! 6)   $��Center=�6r   $��2,0,0,0q�6�   S 6P   �� *AllowDanger+6�   �� 5�     $��	无效值��6   #��[! 6)   $��Center=�6s   $��2,0,0,0q�6�   S 6P   �� *AllowInvalid+6�   �� 5�     $��Stretch=�6   $��2��68   #���  6C   $	��5,0q�6o   �� 5�     $��查询时间��6   $��Center=�6(   $��2,0,0,0q�6C     Loya.Dameer.Dameer  5�     T   Format$T Custom=�6"   U   CustomFormat$U yyyy-MM-dd HH:mm:ss��62   V   ShowCheckBoxV .  #���  6�   $��Center=�6�   W   ValueW 6j   �� *	StartTime+6�   �� 5�      " end#��� " 6   $��2,0,0,0q�69   $��Center=�6J     5�     $T Custom=�6"   $U yyyy-MM-dd HH:mm:ss��62   V .  #���  6�   $��Center=�6�   W 6j   �� *	EndTime+6�   �� 5�     $��Stretch=�6   $��2��68   #���  6B   $	��5,0q�6n   �� 5�      # AccentedSquareButtonStyle#��� # 6<   $��4,0,0,0q�6p   ��6   �� *AddDataCommand+��5�     �� 5�     $A 
Horizontal=�6%   .+�5�     �� $
��None=�6$   $-��'/AIC.Resources;component/Images/add.png��63   �� 5�      $ add#��� $ 6(   $��Center=�6E   5�     5�     �� 5�     #��� # 6@   $��4,0,0,0q�6t   ��6   �� *RefreshDataCommand+��5�     �� 5�     $A 
Horizontal=�6%   .+�5�     �� $
��None=�6$   $1��+/AIC.Resources;component/Images/refresh.png��63   �� 5�      % refresh#��� % 6(   $��Center=�6I   5�     5�     �� 5�     #��� # 6>   $��4,0,0,0q�6r   ��6   �� *ClearDataCommand+��5�     �� 5�     $A 
Horizontal=�6%   .+�5�     �� $
��None=�6$   $1��+/AIC.Resources;component/Images/delete3.png��63   �� 5�      	& clear#��� & 6(   $��Center=�6G   5�     5�     5�     5�     � 5�  
   $��1��6   $��2��6   $��0,1,0,0q�6,   = 5�     �� 5�     $
��Auto �6    �� 5�     $��* �6    > 5�     �� 5�     $��* �6#   �� 5�     $
��Auto �6#   �� 5�     $	��100 �6#   .+�5�     � $��0��6   $��0��6    > 5�     �� 5�     $��* �6'   �� 5�     $
��auto �6'   .+�5�     � $��Disabled=�6   .+�5�     �� $	��5,0q�6   $��趋势��6,   #��[! 6W   S 69   �� *	ShowAMS+6W   �� 5�     $	��5,0q�6   $��时域��6,   #��[! 6^   S 69   �� *ShowTimeDomain+6^   �� 5�     $	��5,0q�6   $��频域��6,   #��[! 6c   S 69   �� *ShowFrequencyDomain+6c   �� 5�     $	��5,0q�6   $��	功率谱��6,   #��[! 6b   S 6:   �� *ShowPowerSpectrum+6b   �� 5�     $	��5,0q�6   $��功率谱密度��6,   #��[! 6k   S 6<   �� *ShowPowerSpectrumDensity+6k   �� 5�     $	��5,0q�6   $��轴心轨迹��6,   #��[! 6[   {�.  S 6;   �� *	ShowOrtho+6�   �� 5�     $	��5,0q�6   $��	变工况��6,   #��[! 6a   {�.  S 6:   �� *ShowOffCondition+6�   �� 5�     $	��5,0q�6   $��	分布图��6,   #��[! 6d   S 6:   �� *ShowDistributionMap+6d   �� 5�     $	��5,0q�6   #��[! 6P   $��Center=�6w   $��Right=�6�   $��双光标模式��6�   S 6,   �� *IsMulticursor+6�   �� 5�     -   5        $��24��5�     $��24��6(   $��0,0,5,0q�6W   .��5�     �� $
��Fill=�6    $9��3/AIC.Resources;component/Images/HorizontalAlign.png��6/   X � ToolTipX 5�     _� 5�  "   $��水平对齐��6*   5�     5�     �� 5�     -   5        $��24��5�     $��24��6(   $��0,0,5,0q�6U   .��5�     �� $
��Fill=�6    $7��1/AIC.Resources;component/Images/VerticalAlign.png��6/   X 5�     _� 5�  "   $��垂直对齐��6*   5�     5�     �� 5�     -   5        $��24��5�     $��24��6(   $��0,0,5,0q�6P   .��5�     �� $
��Fill=�6    $/��)/AIC.Resources;component/Images/Align.png��6/   X 5�     _� 5�  "   $��平均对齐��6*   5�     5�     5�     5�     � 5�     $��1��6   $��0��6    $��0,0,0,0q�60   = 5�     �� 5�     $
��Auto �6$   �� 5�     $��* �6$   > 5�     �� 5      $��* �6'   �� 5     $
��auto �6'   .+�5     �� -   5        Y ��Name$Y scrollviewer��5     $��1��65   $��0��6B   $��Disabled=�6R   .��5     �� -	   5        Z ��Name$Z ReplayListBox��5     $��Stretch=�5  *   $��Stretch=�6H   $��Disabled=�6d   $��Disabled=�6�   #��[ 5  �   ��5  5   Z� ��6K   Z� ��5  *   �� *HistoricalDatas+P 6R   �� *SelectedHistoricalData+��5     �� 5     .R�5  "   � ��6,   �� *ActualWidth+, �� "- ��5     5     5     �� 5     $��0��6   $��0��6&    ' 
FlatSlider#��� ' 6\   [ � Minimum$	[ 120Q�6�   \ � Maximum$
\ 1550Q�6�   ] �� Orientation$] 
Horizontal=�6�   ^ � Value^ 66   �� *	ItemWidth+_ �� Delay$	_ 200��6�   �� 5     $��1��6   $��1��6&   #��� ' 6]   $	[ 120Q�6�   $	\ 600Q�6�   $] Vertical=�6�   ^ 66   �� *
ItemHeight+$	_ 200��6�   �� 5     $��0��6   $��1��6(   $X 允许拖拽��6g   $y�	Collapsed=�6�   S 68   �� *IsComposing+$C TwoWay=���6v   Z� 6�   51     �� 53     $��0��6   $��1��6(   $��2��68   $��3��6I   #���  6S   $��Center=�6�   $��Stretch=�6�   �� 55     $��0��6   $��2��6#   $��2��63   $��Stretch=�6q   $��Stretch=�6�   $��Disabled=�56  e   ��55  D   Z� ��6Z   Z� ��56     �� *AddedChannels+P 6<   �� *SelectedChannel+ 57     -  'AIC.HistoryDataPage.Models.ChannelToken ( DeepLinearAccentColorBrush��%  (��       58  #   *, +)7  �� 6   "?  6#   .R�59     �� $��1q�6!   #���  65   ._�5:     � $
��Auto��6#   $��80��60   #��� ( 6?   .+�5;  "   �� $��16(�6I   $
��Wrap=�6W   $��Center=�6k   $��Center=�6�   ��6,   �� *DisplayName+6�   �� 5<  "   8��  $
��Bold"�6y   $��24��6�   $��24��6�   $��Right=�6�   $	��Top=�6�   ��6[   �� ��5=  &   �� 5>  *   $A 
Horizontal=�65   .+�5?  .   �� $��X��68   #���  6A   $��Center=�6q   5@  +   5B  #   5C     5D     5E     5F     ��5G     �� 5H     .R�5I     � ` � Orientation$` Vertical=�6$   5J     5L     5M     5O     5P     