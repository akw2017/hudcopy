   M S B A M L   `   `   `  ���� G  CAIC.CoreType, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullA0clr-namespace:AIC.CoreType;assembly=AIC.CoreTypeAIC.CoreType  Q MAIC.Cloud.DataReplayer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullK*clr-namespace:AIC.Cloud.DataReplayer.ViewsAIC.Cloud.DataReplayer.Views U/clr-namespace:AIC.Cloud.DataReplayer.ViewModels!AIC.Cloud.DataReplayer.ViewModels E AAIC.Themes, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullM5clr-namespace:AIC.Themes.Controls;assembly=AIC.ThemesAIC.Themes.Controls ?$clr-namespace:AIC.Cloud.DataReplayerAIC.Cloud.DataReplayer K GAIC.Cloud.Domain, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullM8clr-namespace:AIC.Cloud.Domain;assembly=AIC.Cloud.DomainAIC.Cloud.Domain O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem 3   -AIC.Cloud.DataReplayer.Views.DataReplayerView   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R	 NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml    	 Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006    	 @d2http://schemas.microsoft.com/expression/blend/2008    	 c
 _System.Windows.Interactivity, Version=4.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity 
 h dMicrosoft.Expression.Interactions, Version=4.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bei9http://schemas.microsoft.com/expression/2010/interactions  ?coreType0clr-namespace:AIC.CoreType;assembly=AIC.CoreType  6views*clr-namespace:AIC.Cloud.DataReplayer.Views  @
viewModels/clr-namespace:AIC.Cloud.DataReplayer.ViewModels  DControls5clr-namespace:AIC.Themes.Controls;assembly=AIC.Themes  0local$clr-namespace:AIC.Cloud.DataReplayer  Edomain8clr-namespace:AIC.Cloud.Domain;assembly=AIC.Cloud.Domain  0sys&clr-namespace:System;assembly=mscorlib  F BItemsFilter, Version=1.1.0.1, Culture=neutral, PublicKeyToken=nullGbsFilter8http://schemas.bolapansoft.com/xaml/Controls/ItemsFilter  Q MAIC.Cloud.Presentation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9presentation&http://www.aicmonitor.com/Presentation    � 	Resources   �� MergedDictionaries �� Source?  9AIC.Cloud.Presentation.Converters.DateTimeDoubleConverter   dtdConverterA  ;AIC.Cloud.Presentation.Converters.TimeSpanToDoubleConverter  timespanToDoubleConverter5  /AIC.Cloud.Presentation.Converters.MathConverter  mathConverter=  7BolapanControl.ItemsFilter.View.DateTimeRangeFilterView �� 
TargetType �� Property �� Value �� 
TargetType � RowDefinitions �� Padding	 �� TextAlignment
 �� RelativeSource �� StringFormat1  +AIC.Cloud.Presentation.Controls.RangeSlider   Orientation   AutoToolTipTextConverter � Minimum �� 	Converter � Maximum   
LowerValue �� Delay   
UpperValue  SplitterBackgroundBrush �� Color  NavigationForegroundBrush  NavigationBackgroundBrush  NavigationBorderBrush<  6AIC.Cloud.Presentation.Converters.IfTrueValueConverter  boolToIntConverter   	TrueValue   
FalseValue@  :AIC.Cloud.Presentation.Converters.EnumDescriptionConverter  enumDescriptionConverter-  'AIC.Cloud.Presentation.CommandReference 	 RemoveCommandReference   Command 
 enumComboItemTemplate �� Path  enumCombobox �� BasedOn  baseCheckBox  ItemsControlStyle �� Padding J� SnapsToDevicePixels  SignalMonitorStyle ��Name �� Triggers P� Property P� Value  �� Triggers$	   AIC.CoreType.SignalDisplayType! 	  AMSTrend" �� Value# �� Binding  ReplayTemplate$ 	  
TimeDomain  TimeDomainTemplate% 	  FrequencyDomain  FrequencyDomainTemplate& 	  PowerSpectrum  PowerSpectrumTemplate' 	  PowerSpectrumDensity   PowerSpectrumDensityTemplate( 	  Ortho  OrthoTemplate) 	  OffDesignCondition  OffDesignConditionTemplate* 	  OrderAnalysis  OrderAnalysisTemplate+ 	  Time3DSpectrum  Time3DSpectrumTemplate, 	  RPM3D  RPM3DSpectrumTemplate- 	  AlarmPointTrend  AlarmPointTrendTemplate  ToggleExpandButtonStyle'
  !System.Windows.VisualStateManager. 
  VisualStateGroups%  System.Windows.VisualStateGroup/   Transitions%  System.Windows.VisualTransition0   GeneratedDuration1   States   System.Windows.VisualState2   
Storyboard3 g� Duration	4 W� To5 �� TargetProperty6 �� 
TargetName7 F�Name8 �� Stretch9 J� Opacity: � UseLayoutRounding; J� RenderTransformOrigin< �� Angle  TreeViewItemFocusVisual . *TreeViewItem.TreeArrow.Static.Checked.Fill 0 ,TreeViewItem.TreeArrow.Static.Checked.Stroke + 'TreeViewItem.TreeArrow.MouseOver.Stroke ) %TreeViewItem.TreeArrow.MouseOver.Fill 3  /TreeViewItem.TreeArrow.MouseOver.Checked.Stroke 1! -TreeViewItem.TreeArrow.MouseOver.Checked.Fill " 	TreeArrow &# "TreeViewItem.TreeArrow.Static.Fill ($ $TreeViewItem.TreeArrow.Static.Stroke % ExpandCollapseToggleStyle= �� CenterY> �� CenterX? �� 
TargetName@ ]� 
ConditionsA �� PropertyB �� Value & TreeViewItemStyleC �� AncestorTypeD �� ModeE � ColumnDefinitionsF d�NameG �� 	ClickModeH d� 	IsCheckedI ��NameJ ��NameK :� RoutedEventL W� FromM W� EasingFunction/  )System.Windows.Media.Animation.CircleEase7  1System.Windows.Media.Animation.EasingFunctionBaseN   
EasingMode ' CheckBoxStyleO �� 
BackgroundP �� Bullet7  1AIC.Cloud.Presentation.Controls.AnimatedRectangle ( RadioButtonStyleQ � 	Resources ) Key9  3System.Windows.Media.Animation.EasingDoubleKeyFrameR O� KeyTimeS O� ValueT �NameU A�Name * 	OnLoaded1 + DisabledBackgroundBrush , ButtonStyle��  �� $U O/AIC.Cloud.DataReplayer;component/Resources/SignalMonitorValueDataTemplate.xaml?�%�  &	        &	      &	 
     '       7   7   7   7   7   &	 �    &	 �    &	 �    &	 �    &	 �    &	 �    &		 �    &	
     7  &	 :    7��7 
 &	 ^    &	 q    &	 U    7  7  7  7  7  7  7  7  7  7  7  &	 �    7d�7  7  7  &	 �    &	     &	     &	 /    &	 D    &	  [    &	! p    &	" �    &	# �    &	$ �    &	% �    7 " 7 # 7 $ 7  7  7  7  7   7 ! &	& `    7  7 % &	' ^    &	( x    7 * &	, �          �� "  .���  � �� �� "  .R��� $
��Auto��#��z��#��z��#��z��
�������._��  �� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto �.+��� $��1��$��1,1,1,1q�$
��Left=�
���   �$ 1,1,1,1q�$
	 Left=��� .�终止时间: �� ��� *Model.MaxDateTime+
 �� *TemplatedParent+$ yyyy/MM/dd HH:mm:ss��  $
��Left=�$ Vertical=�$��2��$��2��$	��150��8    �� *Model.MinDateTime+8  
 �� *TemplatedParent+ �� *Model.MaxDateTime+8  
 �� *TemplatedParent+ �� *Model.CompareFrom+8  
 �� *TemplatedParent+$	 300�� �� *Model.CompareTo+8  
 �� *TemplatedParent+$	 300���� $��3��$��0��$��1,1,1,1q�$ 1,1,1,1q�
���   ��� .�起始时间: �� ��� *Model.MinDateTime+
 �� *TemplatedParent+$ yyyy/MM/dd HH:mm:ss���� $ 	#FFE7E7E7���� $ 	#FF3F3F3F���� $ 	#FFE7E7E7���� $ 	#00E7E7E7��  $ 0��$ 1��     �� *RemoveChannelsCommand+�� .R��� ���� $ .�8   �� " ��8   .���  � ��8  �� " �� Z� �� " ��.���  � ��$ Disabled=���  � ��$
 Auto=���  � �� . ��  �@��PanningMode$
 Both=���  � �� �� " ��.R��� #��z��#��z��#��z��# z�� . ._��� |�.  #��z��.���� # z �� " ��.���  � �� �� " ��.R��� $ Bd��#��z��#��z��#��z��# z�� . ._��� #��z��# z #��z�� P�  � {� .  . ��� # � �� � ��  �� #" Z! # �� $ DisplayMode�.a���  � ��8   �� #" Z$ # �� $ DisplayMode�.a���  � ��8  �� #" Z% # �� $ DisplayMode�.a���  � ��8  �� #" Z& # �� $ DisplayMode�.a���  � ��8  �� #" Z' # �� $ DisplayMode�.a���  � ��8  �� #" Z( # �� $ DisplayMode�.a���  � ��8  �� #" Z) # �� $ DisplayMode�.a���  � ��8  �� #" Z* # �� $ DisplayMode�.a���  � ��8  �� #" Z+ # �� $ DisplayMode�.a���  � ��8  �� #" Z, # �� $ DisplayMode�.a���  � ��8 	 �� #" Z- # �� $ DisplayMode�.a���  � ��8 
 �� # �� *	IsVisible+	" false.a���  � y�$ 	Collapsed=��� " d�8   .���  �@�Cursor$
 Hand����  � ��8  ��  � ��8  ��  � ��8  ��  � ��$ 1q���  � ��$ 16����  � ��$ 40����  � ��$ 1q���  � �� �� " d�.R�� .   CommonStates��/   $0 0:0:0.1D�.1   
Normal��  	MouseOver��.2 �� .��W� $3 0D�$4 1X�$5 (UIElement.Opacity)�$6 normal��  Pressed��.2 �� .��W� $3 0D�$4 1X�$5 (UIElement.Opacity)�$6 normal��  Disabled��.2 �� .��W� $3 0D�$	4 0.3X�$5 (UIElement.Opacity)�$6 normal��  CheckStates��.1   Checked��.2 �� .��W� $3 0:0:0.3D�$4 0X�$5 Angle�$6 symbolRotation��  	Unchecked��.2 �� .��W� $3 0:0:0.3D�$	4 180X�$5 Angle�$6 symbolRotation��  Indeterminate��  FocusStates��.1   Focused��  	Unfocused��.+��� $��2��$��3��$��40��$��20��#��z��#��z��#��z��F� $7 normal��$��Center=�$��10��$
8 Fill=�$	9 0.6Q������@B ��[ �@B ��[ �ا �ا �@B �ا �pr ����n@�pr ����n@S�pr ����n@��� �ا ��� �ا Ӏ�� �ا ��� ��[ ��� ��[ Ӏ�� ��[ �pr P�^����?�pr P�^����?S�pr P�^����?��[ ��[ 	#��z��: .  $��Center=�$��7��$; 0.5,0.5&�z�V� .~��� �� �� symbolRotation��$	< 180Q�T� �� .���  � �� �� .R��� �� $ 	#FF595959���� $ 	#FF262626���� $ 	#FF1BBBFA���� $ Transparent���� $ 	#FF262626���� $ 	#FF595959��B� $��M0,0 L0,6 L6,0 zC��� $ Transparent���� $ 	#FF989898���� " d�.���  � |� .  ��  � ��$ 16����  � ��$ 16����  � �� �� " d�.R��� 
������ $��16��$ 5,5,5,5q�$��16��._�F� $7 
ExpandPath��8��  8�� 8�� z��� $	< 135Q�$= 3Q�$> 3Q� P�  �@d�	IsChecked$
 TrueY�. ��� $? 
ExpandPath�� � z� �� $	< 180Q�$= 3Q�$> 3Q��� $? 
ExpandPath��8   � ���� $? 
ExpandPath��8   � ��P�  �@J�IsMouseOver . . ��� $? 
ExpandPath��8   � ���� $? 
ExpandPath��8   � ��]� @ �� A �@J�IsMouseOverB . �� A �@d�	IsChecked$
B TrueY�./��� $? 
ExpandPath��8   � ���� $? 
ExpandPath��8   � ���� " Q�.���  � �� �	#FF87CDEE��  � �� �	#7F243859��  � �� �� *HorizontalContentAlignment+
 �� "C ����  � �� �� *VerticalContentAlignment+
 �� "C ����  � ��$ 1,0,0,0q���  � ��$	 0,2q���  � ��$ 25����  �@�FocusVisualStyle8   ��  �@Q�
IsExpanded �� *
IsExpanded+$D TwoWay=���  � z� T� ��  � �� �� " Q�.R�� E �� $��19��$
��Auto ��� $
��Auto ��� $��* � �� $
��Auto ��� .+�d� $F Expander��$G Press=�8�� H �� *
IsExpanded+
 �� *TemplatedParent+�� $ Bd��$��1��#��z��#��z��#��z��# z�� . ._��� $I PART_Header��$��Header��#��z��# z �� $J 	ItemsHost��$��2��$��1��$��1�� P�  �@Q�
IsExpanded .  . ��� $? 	ItemsHost�� � y�$ 	Collapsed=�P�  �@��HasItems .  . ��� $? Expander�� � y�$ Hidden=�P�  �@Q�
IsSelected . . ��� $? Bd�� � �� �	#FF20437E�� # � �� � ��P�  � {� .  . ��� # � �� � ��  :� $K Loaded��[��� .���� .��W� $85 2(UIElement.RenderTransform).(TranslateTransform.X)�$	L -50X�$4 0X�$3 0:0:1D�M   $N EaseOut=�P�  �@%�IsVirtualizing . . ���  � �� �� .R�$� �� " ��.���  � �� �	#FF87CDEE��  � ��$ 1q���  � ��$ Center=���  � ��$ 15(���  � �� �� " ��.R��� 
O ����  . P � $��25��$��25��.+�  F� $
7 path��$��22��$��20��$
8 Fill=�#��z��F�����ej�Z�����x��,E=R�ܿe������Tn5�$�!��C��ej�Z�._��� #��z��#��z����. # z #��z�� P�  � �� . . ���  � ��$ 4,0,0,0q�P�  � {� .  . ��� # � �� � ��P�  �@d�	IsChecked$ FalseY�. ��� $
? path�� � y�$ Hidden=��� " 	�.���  � �� �	#FF87CDEE��  � ��
 ���� ��  � �� �	#FF87CDEE��  � ��$ 1q���  � ��$ Center=���  � ��$ Center=���  � �� �� " 	�Q ���� 	OnLoaded1) .��U� $V5 P(UIElement.RenderTransform).(TransformGroup.Children)[0].(ScaleTransform.ScaleX)�$
6 grid��.\�  $R 0��$S 0Q�  $R 0:0:0.5��$S 1Q�U� $V5 P(UIElement.RenderTransform).(TransformGroup.Children)[0].(ScaleTransform.ScaleY)�$
6 grid��.\�  $R 0��$S 0Q�  $R 0:0:0.5��$S 1Q�.R��� 
O ���� P � $
T grid��$��25��$��25��$; 0.5,0.5&�z�V� .~��� �� �� T� .+�A� $U Ellipse_Border��#��z��#��z��$��3��A� $U 	CheckMark��$��5q����	#FF87CDEE._��� #��z��#��z��#��z����.  :� $K FrameworkElement.Loaded��.[��� 8��  P�  �@d�	IsChecked$ falseY�. ��� $? 	CheckMark�� � y�$ 	Collapsed=�P�  � {� .  . ��� # � + $? Ellipse_Border�� � ���� $? Ellipse_Border�� � �� �	#40000000��  � �� �	#80000000�� " ��.���  � ��
 ���� ��  � �� �	#FF87CDEE��  � ��$ Center=���  � ��$ Center=���  � ��$ 1q���  � �� �� " ��.R�� #��z��.+��� #��z��#��z����. # z #��z�� P�  �@J�IsMouseOver . . ���  � �� �	#7F243859P�  � {� .  . ���  � �� �#ADADADP�  � �� . . ���  � �� �	#FF20437E.��� . 
 (System.Windows.Interactivity.InteractionV   TriggersV 9  3Microsoft.Expression.Interactivity.Core.DataTrigger  W   Value$
W Data��D  >Microsoft.Expression.Interactivity.Core.PropertyChangedTriggerX   BindingX �� *
ViewMode+. 
 (System.Windows.Interactivity.TriggerBaseY   Actions.Y =  7Microsoft.Expression.Interactivity.Core.GoToStateAction  Z   	StateName$Z ShowData��  $W Video��X �� *
ViewMode+.Y   $Z 	ShowVideo��.   -   VisualizationStates��.1   -   ShowData��.2 �� .��T� $5 (UIElement.Visibility)�$6 dataReplayGrid��$3 0D�..�i� [ R� KeyTime$[ 0:0:0��  System.Windows.Visibility\   Visible] R� Value#] Z\ T� $5 (UIElement.Visibility)�$6 videoReplayGrid��$3 0D�..�i� $[ 0:0:0��^   	Collapsed#] Z^   -   	ShowVideo��.2 �� .��T� $5 (UIElement.Visibility)�$6 dataReplayGrid��$3 0D�..�i� $[ 0:0:0��#] Z^ T� $5 (UIElement.Visibility)�$6 videoReplayGrid��$3 0D�..�i� $[ 0:0:0��#] Z\ .+��� .@��� $��数据查询��.��� -   $��dataReplayGrid��V   $W Chart��X �� *DataViewMode+.Y   $Z ShowDataChart��  $
W List��X �� *DataViewMode+.Y   $Z ShowDataList��.   -   VisualStates��.1   -   ShowDataChart��.2 �� .��T� $5 (UIElement.Visibility)�$6 dataChartGrid��$3 0D�..�i� $[ 0:0:0��#] Z\ T� $5 (UIElement.Visibility)�$6 dataListGrid��$3 0D�..�i� $[ 0:0:0��#] Z^   -   ShowDataList��.2 �� .��T� $5 (UIElement.Visibility)�$6 dataChartGrid��$3 0D�..�i� $[ 0:0:0��#] Z^ T� $5 (UIElement.Visibility)�$6 dataListGrid��$3 0D�..�i� $[ 0:0:0��#] Z\  �� $
��auto ��� $��* �.+�� .+�� $��Disabled=�$��5q�.+�	� $��图表��$
H TrueY�$��16(�#��[( $��Chart������ *DataViewModeSwitchCommand+	� $��列表��#��[( $��16(�$
��List��$��5,0,0,0q����� *DataViewModeSwitchCommand+�� $��Stretch=�$��2�����	#FF87CDEE$	��5,0q��� $��数据上限:�����	#FF87CDEE$��Center=�$��16(��� $��50��$��25��$��Center=�$��Center=����� *ChannelRecordLength+�� $��数据类型:�����	#FF87CDEE$��Center=�$��16(�$��5,0,0,0q��� _ �� 
IsEditable_ .  $��75��$��25��#��[
 '  !AIC.Cloud.Presentation.EnumHelper`   Enum   AIC.CoreType.Unit"`  a �� SelectedItema �� *
UnitFilter+�� $��触发条件:�����	#FF87CDEE$��Center=�$��16(�$��5,0,0,0q��� _ .  $��75��$��25��#��[
    AIC.CoreType.TriggerType"`  a �� *TriggerFilter+�� $��Stretch=�$��2�����	#FF87CDEE$	��5,0q��� $��转速��#��[' $��Center=�H �� *AllowRPMFilter+�� $��起始转速:�����	#FF87CDEE$��Center=�$��16(�$��5,0,0,0q��� $��50��$��25��$��Center=�$��Center=����� *DownRPMFilter+�� $��终止转速:�����	#FF87CDEE$��Center=�$��16(�$��5,0,0,0q��� $��50��$��25��$��Center=�$��Center=����� *UpRPMFilter+�� $��Stretch=�$��2�����	#FF87CDEE$	��5,0q��� $��	正常值��#��[' $��Center=�H �� *AllowNormal+�� $��	警告值��#��[' $��Center=�$��2,0,0,0q�H �� *AllowWarning+�� $��	危险值��#��[' $��Center=�$��2,0,0,0q�H �� *AllowDanger+�� $��Stretch=�$��2�����	#FF87CDEE$	��5,0q��� $��	IP地址:�����	#FF87CDEE$��Center=�$��16(��� $	��100��$��25��$��Center=�$��Center=����� *ListenAddress+�� $��端口:�����	#FF87CDEE$��Center=�$��5,0,0,0q�$��16(��� $��50��$��25��$��Center=����� *
ListenPort+$ F0���� $��Stretch=�$��2�����	#FF87CDEE$	��5,0q��� $��刷新通道��#��[, $��25��$��5,0,0,0q�$��16(����� *RefreshCommand+�� $��Stretch=�$��2�����	#FF87CDEE$	��5,0q��� $��3��$��刷新数据��$��5,0,5,0q�#��[, $��16(����� *RefreshChannelDataCommand+  � $��1��E �� $	��200 ��� $��12 ��� $��* ��� $��12 ��� $	��120 �.+�� $��	5,5,2.5,5q� �� $
��auto ��� $
��auto ��� $
��auto ��� $��* �.+�R� -   b R�Name$b treeView��$��3��#��[& $��0q�$��Hidden=�$��Hidden=����� *Groups+��Z� ��Z�   c �� DataType%  AIC.Cloud.Domain.GroupTreeModeld �� ItemsSourcee �� Orientationf ��Name+  %AIC.Cloud.Domain.CorporationTreeModel(  "AIC.Cloud.Domain.WorkShopTreeModelg ��Nameh �� Triggers)  #AIC.Cloud.Domain.EquipmentTreeModel)  #AIC.Cloud.Domain.TestPointTreeModel&    AIC.Cloud.Domain.DivFreTreeModel��%m  (��       *, +)(�� �    *, +)(��     *, +)(�� �    *, +)(�� 0    *, +)(�� �    *,  +)�� "c  d �� *
Children+.R��� $e 
Horizontal=�$	��0,1q�.+�F� $��35��$��22��$8 Uniform=����	#FF87CDEE�������@]� р��@]� р����� р���� р�@]� ��� -1�@�q -1�@�q��O�����O��� -1��� $� р�O $� р�� $� р�� $� р��@��р��@��р�����р�O���р�O@��р�@��р� $� ���@�������^р���^р�C��^�@�q��^�@�q@��р�C@��р�C ?�р� ?�р�@�����@�����j @�����j  ?����j ���� �� ��O�@f����@f ?��@f@��р�� @��р��  ?�р��  ?�р�� @�����j @��� �� 0�@f0#[�@f��y� �� ��<�v�[ `��v�[ ��f� �� 0�� $f CorporationText��$��Center=����� *Name+�� "c  d �� *
Children+.R��� $e 
Horizontal=�$	��0,1q�.+�F� $��35��$��22��$8 Uniform=����	#FF87CDEE�������@]� р��@]� р����� р���� р�@]� ��� -1�@�q -1�@�q��O�����O��� -1��� $� р�O $� р�� $� р�� $� р��@��р��@��р�����р�O���р�O@��р�@��р� $� ���@�������^р���^р�C��^�@�q��^�@�q@��р�C@��р�C ?�р� ?�р�@�����@�����j @�����j  ?����j ���� �� ��O�@f����@f ?��@f@��р�� @��р��  ?�р��  ?�р�� @�����j @��� �� 0�@f0#[�@f��y� �� ��<�v�[ `��v�[ ��f� �� 0�� $f CorporationText��$��Center=����� *Name+�� "c  d �� *
Children+.R��� $e 
Horizontal=�$	��0,1q�.+�F� $��22��$��22��$8 Uniform=����	#FF87CDEE�������� �!Q��� �ǘ���@@���� �Pq�P�� �}Ѥ!� ��O��BnHd� Q����ǘ���0@����P�� ь�0� ь�Ԃшd%��O����}��ǘ���@@�Pq�ǘ���@@�ǘ���@@��ǘ���@@�!���� �!��Bn�oѠo��XQ�o�ǘ���@@Q�z@�ǘ���@@єz@��ј���Q���ǘ���@@Q���ǘ���@@ь���X��Bn�o�� $f WorkShopText��$��Center=����� *Name+�� $g AddBtn��$y�	Collapsed=�$��Center=�$��22��$��22��$��0q����� $- 'DataContext.LoadingWorkshoptDataCommand�
 �� "C R����� .��F� $
8 Fill=�$��20��$��20��
�������a���� � ��O� � ���ѐ�����@o@@������@���@�4 6n������� �@o@��� ѐ���O� � ��Oh �� $
" List��# �� $ DataContext.DataViewMode�
 �� "C R�.a��� $? AddBtn�� � y�$ Visible=��� "c  d �� *
Children+.R��� $e 
Horizontal=�$	��0,1q�.+�F� $��22��$��22��$8 Uniform=����	#FF87CDEE�����P�x��ݩ(�� ���� p׻\r� $�� �(4��0�	�^)�@@�5������@@�.>�A�G�!اQ���P�x�ز�ݩ������y@@�����p׻�:pΈ"@@$��pP�(4�Ql^�	�^)�@@SP=������@@���A(Ċ�!�L�c����2ز�S�U)�����y@@�� ����d�:pΈ"@@��7!P�Ѵ�� l^����� P=�d*� ��PJ� �Ê��L�c�(�� �2��� �U)\r� �� � �dѐ0�7!є5P�� Ӱ.>��� �Gd*� اQPJ� �P�x�� 6n -1Ӵ�L -1 -1��L -1 6n� -1L���L ?� 6n ?��L� ?� ?�L� ?� 6n� ?���LL� -1 6n -1�� $f EquipmentText��$��Center=���`� $ {0}{1}��.1��� $
 Name��� $
 MSSN��� $g AddBtn��$y�	Collapsed=�$��Center=�$��22��$��22��$��0q����� $- 'DataContext.LoadingEquipmentDataCommand�
 �� "C R����� .��F� $
8 Fill=�$��20��$��20��
�������a���� � ��O� � ���ѐ�����@o@@������@���@�4 6n������� �@o@��� ѐ���O� � ��Oh �� $
" List��# �� $ DataContext.DataViewMode�
 �� "C R�.a��� $? AddBtn�� � y�$ Visible=��� "c  d �� *
Children+.R�� .+��� $e 
Horizontal=�$	��0,1q�.+�F� $��22��$��22��$8 Uniform=����	#FF87CDEE������ا  � �@]�  � �@]� ���@�4���@�4 Q%��ا  Q%��ا  � ���� @��р�� ���� -1��^�@������@�4@]� �@�4@fр������ -1���р�� @���� $f TestPointText��$��Center=���`� $ {0}{1}��.1��� $
 Name��� $
 MSSN��� $��
vDataImage��$��../Images/VData.Png��$��2,0,2,0q�$��4��$y�	Collapsed=�$��26��$��25��$
��None=��� $g AddBtn��$��Center=�$��22��$��22��$��0q����� $$ DataContext.LoadingDataCommand�
 �� "C R����� .��F� $
8 Fill=�$��20��$��20��
�������a���� � ��O� � ���ѐ�����@o@@������@���@�4 6n������� �@o@��� ѐ���O� � ��Oh �� $
" true��# �� *	HasData+.a��� $? 
vDataImage�� � y�$ Visible=��� "c   .R��� $e 
Horizontal=�$	��0,1q�.+�F� $��22��$��22��$8 Uniform=����	#FF87CDEE����� $�  Q%�v��  Q%�v�� ���р�� ���� H� $� � H� � р�C � р�C��р���� $�  H�� $�  Q%������ р� $� � Q% $� � Q%��� р���� � �  H�� � ��р�� ��р��  H�� �  H��� $f 
DivFreText��$��1��$��Center=�$��5,0,0,0q����� *Name+�� $g AddBtn��$��Center=�$��22��$��22��$��0q����� $# DataContext.LoadDivFreCommand�
 �� "C R����� .��F� $
8 Fill=�$��20��$��20��
�������a���� � ��O� � ���ѐ�����@o@@������@���@�4 6n������� �@o@��� ѐ���O� � ��OA!  ;AIC.Cloud.Presentation.Controls.AnimatedVertialGridSplitter!  $��1��$��Stretch=�$
��0,30q�� $��2�� �� $��* ��� $
��auto �.+�� -	   $��dataChartGrid��$��2.5,5,2.5,5q� �� $
��Auto ��� $��* ��� $��27 �.+�� $��0,5,0,0q�E �� $��* ��� $
��auto �.+�� $��Disabled=�$��5q�.+��� $	��5,0q�$��通频��#��[' H �� *	ShowAMS+�� $	��5,0q�$��报警点趋势��#��[' H �� *ShowAlarmPointTrend+�� $	��5,0q�$��时域��#��[' H �� *ShowTimeDomain+�� $	��5,0q�$��频域��#��[' H �� *ShowFrequencyDomain+�� $	��5,0q�$��	功率谱��#��[' H �� *ShowPowerSpectrum+�� $	��5,0q�$��功率谱密度��#��[' H �� *ShowPowerSpectrumDensity+�� $	��5,0q�$��轴心轨迹��#��[' H �� *	ShowOrtho+�� $	��5,0q�$��	变工况��#��[' H �� *ShowOffCondition+�� $e 
Horizontal=�$	��Top=�$��Right=�$��1��$��5q�.+��� -
   $��0,0,5,0q�.���� $#��../Images/HorizontalAlign.png��$��1q�i � ToolTipi _� $��水平对齐���� -   $��0,0,5,0q�.���� $!��../Images/VerticalAlign.png��$��1q�i _� $��垂直对齐���� -   $��0,0,5,0q�.���� $��../Images/Align.png��$��1q�i _� $��平均对齐��� $��1��.+��� -   j ��Name$j verticalScrollViewer��$
��Auto=�.���� -   k ��Name$k ReplayListBox��$��Stretch=�$��Stretch=�$��Disabled=�$
��Auto=�#��[ ��Z� ��Z� ���� *HistoricalDatas+���� .R�("  "AIC.Themes.Controls.FluidWrapPanel"  l " Name$l fluidWrapPanel��$
��Left=�$	��Top=�}�.  m "  Orientation$m 
Horizontal=�n "  DragOpacity$n 1Q�o "  	DragScale$o 1Q�p "  
ItemHeightp �� *
ItemHeight+q "  	ItemWidthq �� *	ItemWidth+r "  IsComposingr �� *IsComposing+s "  ElementEasings -#  'System.Windows.Media.Animation.BackEase#  $N EaseOut=�t #  	Amplitude$
t 0.35Q�u "  
DragEasingu #  $N EaseOut=�$
t 0.65Q��� #��[' $��Right=�$	��Top=�$��0,0,0,0q�H �� *IsComposing+$D TwoWay=��� $��1�� - SimpleSlider#��� - $	 120Q�$
 1000Q�v �� Orientation$v 
Horizontal=�$	��200��$��Right=�$	��Top=�$��0,0,30,0q�w � Valuew �� *	ItemWidth+$	 200���� $��1��#��� - $	 120Q�$
 1000Q�$v Vertical=�$	��200��$��Right=�$	��Top=�$��0,30,0,0q�w �� *
ItemHeight+$	 200��� -   $��dataListGrid��$��2.5,5,2.5,5q�$y�	Collapsed=�.+��� .@��� $��振动通道��.��� .+�/$  )BolapanControl.ItemsFilter.FilterDataGrid$  -   &%   System.Windows.Controls.DataGridx %  GridLinesVisibility$
x None=�y %  EnableRowVirtualizationy . z %  AutoGenerateColumnsz .  { %  CanUserAddRows{ .  ��Z� ���� *VInfoObjects+| %  RowHeaderTemplate| �� .R��� $��Center=�$��2q����� } %  Columns} 0&  *System.Windows.Controls.DataGridTextColumn&  ,'  &System.Windows.Controls.DataGridColumn~ '  Header$~ 总厂�� '  
IsReadOnly . 1(  +System.Windows.Controls.DataGridBoundColumn� (  Binding� �� *GroupCOName+&  $~ 分厂�� . � �� *CorporationName+&  $~ 车间�� . � �� *WorkShopName+&  $~ 设备名称�� . � �� *	DevName+&  $~ 设备编号�� . � �� *DevSN+&  $~ 测点名称�� . � �� *CHName+&  $~ 测点编号�� . � �� *CHMSSN+&  $~ 上传时间�� . � �� *Date+$ yyyy/MM/dd HH:mm:ss��-)  'BolapanControl.ItemsFilter.ColumnFilter� )  Initializers� F*  @BolapanControl.ItemsFilter.Initializer.FilterInitializersManager*  E+  ?BolapanControl.ItemsFilter.Model.DateTimeRangeFilterInitializer+  &  $~ 	有效值�� . � �� *Value+$ F3��� *  I,  CBolapanControl.ItemsFilter.Initializer.LessOrEqualFilterInitializer,  L-  FBolapanControl.ItemsFilter.Initializer.GreaterOrEqualFilterInitializer-  C.  =BolapanControl.ItemsFilter.Initializer.RangeFilterInitializer.  &  $~ 单位�� . � �� *Unit+�� $��清除列表��$��	Vibration��$��Right=�$	��Top=�$��5q�#��[, $��16(����� *ClearListCommand+�� $��模拟通道��.��� .+�$  -   $
x None=�y . z .  { .  ��Z� ���� *AnInfoObjects+| �� .R��� $��Center=�$��2q����� } &  $~ 总厂�� . � �� *GroupCOName+&  $~ 分厂�� . � �� *CorporationName+&  $~ 车间�� . � �� *WorkShopName+&  $~ 设备名称�� . � �� *	DevName+&  $~ 设备编号�� . � �� *DevSN+&  $~ 测点名称�� . � �� *CHName+&  $~ 测点编号�� . � �� *CHMSSN+&  $~ 上传时间�� . � �� *Date+$ yyyy/MM/dd HH:mm:ss��� *  +  &  $~ 	有效值�� . � �� *Value+$ F3��� *  ,  -  .  &  $~ 单位�� . � �� *Unit+�� $��清除列表��$��Analog��$��Right=�$	��Top=�$��5q�#��[, $��16(����� *ClearListCommand+�� $��分频��.��� .+�$  -   $
x None=�y . z .  { .  ��Z� ���� *DivFreObjects+| �� .R��� $��Center=�$��2q����� } &  $~ 总厂�� . � �� *GroupCOName+&  $~ 分厂�� . � �� *CorporationName+&  $~ 车间�� . � �� *WorkShopName+&  $~ 设备名称�� . � �� *	DevName+&  $~ 设备编号�� . � �� *DevSN+&  $~ 测点名称�� . � �� *CHName+&  $~ 测点编号�� . � �� *CHMSSN+&  $~ 分频�� . � �� *FreDescription+&  $~ 上传时间�� . � �� *Date+$ yyyy/MM/dd HH:mm:ss��� *  +  &  $~ 频率�� . � �� *FreV+$ F3��� *  ,  -  .  &  $~ 幅值�� . � �� *FreMV+$ F3��� *  ,  -  .  &  $~ 相位�� . � �� *Phase+$ F3��� *  ,  -  .  &  $~ 单位�� . � �� *Unit+�� $��清除列表��$��DivFre��$��Right=�$	��Top=�$��5q�#��[, $��16(����� *ClearListCommand+� $��1��E �� $
��auto ��� $��* ��� $
��auto ��� $
��auto �.+�4/  .AIC.Cloud.Presentation.Controls.DateTimePicker/  $��0��$��25��� /  
DateFormat$� yyyy/MM/%d HH:mm:ss��� /  SelectedDate� �� *	StartTime+$D TwoWay=�50  /AIC.Cloud.Presentation.Controls.DateRangeSlider0  ���#2F4B67$	��1,0q�$��1��� 0  Minimum� �� *	StartTime+$D TwoWay=�� 0  Maximum� �� *	EndTime+$D TwoWay=�� 0  
UpperValue� �� *	UpperTime+$D TwoWay=�� 0  
LowerValue� �� *	LowerTime+$D TwoWay=�/  $��2��$��25��$� yyyy/MM/%d HH:mm:ss��� �� *	EndTime+$D TwoWay=�!  $��3��$��Stretch=�$
��0,30q�� $��4��$��	2.5,5,5,5q� �� $��* ��� $��3 ��� $��50 �.+��� $��0��$��Disabled=���Z� ���� *AddedChannels+a �� *SelectedChannel+��Z�   =1  7AIC.Cloud.DataReplayer.ViewModels.VibrationChannelToken:2  4AIC.Cloud.DataReplayer.ViewModels.AnalogChannelToken:3  4AIC.Cloud.DataReplayer.ViewModels.DivFreChannelToken��%�  (��       *,1 +)7 	 (�� �    *,2 +)7 	 (��     *,3 +)7 	 �� "c 1 .R�� $��90��$��60�����	#7F243859.+��� $
��Wrap=�
���  ��$��Center=�$��Center=���`� $ {0}{1}.{2}{3}��.1��� $ Channel.DevName��� $ Channel.DevSN��� $ Channel.Name��� $ Channel.MSSN�i �� �� ��� *Channel.GroupCOName+ �� .�. �� ��� *Channel.CorporationName+ �� .�. �� ��� *Channel.WorkShopName+ �� .�. �� ��� *Channel.DevName+ �� ��� *Channel.DevSN+ �� .�. �� ��� *Channel.Name+ �� ��� *Channel.MSSN+�� $��X��$��Right=�$	��Top=�$��15��$��15��8��  ���� �� "c 2 .R�� $��90��$��60�����	#7F243859.+��� $
��Wrap=�
��� � �$��Center=�$��Center=���`� $ {0}{1}.{2}{3}��.1��� $ Channel.DevName��� $ Channel.DevSN��� $ Channel.Name��� $ Channel.MSSN�i �� �� ��� *Channel.GroupCOName+ �� .�. �� ��� *Channel.CorporationName+ �� .�. �� ��� *Channel.WorkShopName+ �� .�. �� ��� *Channel.DevName+ �� ��� *Channel.DevSN+ �� .�. �� ��� *Channel.Name+ �� ��� *Channel.MSSN+�� $��X��$��Right=�$	��Top=�$��15��$��15��8��  ���� �� "c 3 .R�� $��90��$��60�����	#7F243859.+��� $
��Wrap=�
��� ���$��Center=�$��Center=���`� $	 {0}��.1��� $ Channel.FreDescription�i �� �� ��� *GroupCOName+ �� .�. �� ��� *CorporationName+ �� .�. �� ��� *WorkShopName+ �� .�. �� ��� *	DevName+ �� ��� *DevSN+ �� .�. �� ��� *Name+ �� ��� *MSSN+ �� .�. �� ��� *Channel.FreDescription+�� $��X��$��Right=�$	��Top=�$��15��$��15��8��  ���� ���� " ��.���  � ��$ Center=���  � ��$ Center=���  � �� �� " ��.R��� $ Bd��#��z��#��z��#��z��# z�� . ._��� #��z��# z #��z�� P�  � {� .  . ��� # � �� � ����  � ��$ 5q�  P�  �@J�IsMouseOver . . ���  � �� �	#7F486391P�  �@��
IsSelected . . ���  � �� �	#FF20437E���� .R�� �� $��诊断��$��2��$��20(�$��5q����� *DiagnoseCommand+�� $��视频查询��.��� -   $��videoReplayGrid��.+�44  .AIC.Cloud.DataReplayer.Views.VideoReplayerView4  