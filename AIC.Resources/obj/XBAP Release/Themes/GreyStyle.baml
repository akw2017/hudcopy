   M S B A M L   `   `   `  ���� H  DAIC.Resources, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null?$clr-namespace:AIC.Resources.ControlsAIC.Resources.Controls  W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089U QMahApps.Metro, Version=1.5.0.23, Culture=neutral, PublicKeyToken=f4fb5a3c4d1e5b4f   DisabledBorderBrush  �� Color  SolidBorderBrush  LightBorderBrush  
GlyphBrush  LightColorBrush  DisabledForegroundBrush  DisabledBackgroundBrush  WindowBackgroundBrush  SelectedBackgroundBrush 	 NormalBrush �� 
StartPoint �� EndPoint � Color � Offset 
 HorizontalNormalBrush  
LightBrush  HorizontalLightBrush  	DarkBrush  PressedBrush  NormalBorderBrush  HorizontalNormalBorderBrush  DefaultedBorderBrush  PressedBorderBrush �� 
TargetType �� BasedOn �� Property �� Value	 �� 
TargetType
 � 	Resources  Key g� Duration �� 
TargetName �� TargetProperty	 W� To g� 	BeginTime O� KeyTime O� Value �Name ��Name �� CornerRadius J� Opacity J� IsHitTestVisible ��Name ��Name K� ZIndex �� Triggers P� Property P� Value �� 
TargetName N� ExitActions ��Name  
FocusedOff  N� EnterActions  	FocusedOn  HoverOff  HoverOn  
PressedOff  	PressedOn! �� Padding  myDataGridColumnHeaderStyle=   7System.Windows.Controls.Primitives.DataGridColumnHeader" � ColumnDefinitions# ��Name$ �� Stretch% J� SnapsToDevicePixels& � Cursor' F�Name( J� RenderTransformOrigin) �� ScaleX* �� ScaleY+ n�Name, �� Angle  myDataGridStyle&   System.Windows.Controls.DataGrid  myDataGridCellStyle*  $System.Windows.Controls.DataGridCell- �� RelativeSource  SelectedOff  
SelectedOn)  #System.Windows.Controls.DataGridRow. �� Triggers/ d�Name0 �� 	ClickMode1 d� 	IsChecked2 �� Path3 �� Mode4 �� RadiusX5 �� RadiusY6 � ActualWidth7 �� SelectionBoxItem8 �� SelectionBoxItemTemplate9 ��Name: �� 
IsReadOnly; �� 
IsReadOnly< �Name= �� IsDropDownOpen> � AllowsTransparency? �� MaxDropDownHeight@ K� IsItemsHost-  'System.Windows.Input.KeyboardNavigationA   DirectionalNavigationB P� 
SourceNameC � FocusVisualStyle  ScrollBarLineButton   ScrollBarPageButton ! ScrollBarThumbD ]� DecreaseRepeatButtonE ]� ThumbF ]� IncreaseRepeatButtonG � RowDefinitions " ExpanderStyle
H �� Top # ToggleButtonStyleI �� LeftJ �� ElementNameK ��NameL �� Right	M �� X1	N �� Y1	O �� X2	P �� Y2Q �� StrokeDashArrayR �� Bottom $ ExpanderToggleButton % AccentColorBrushS � UseLayoutRounding & PauseToggleButtonT ��NameU �� Orientation 	' pause ( play ) ToggleTextButton * ToggleArrowButton + AccentColorBrush2 , ToggleArrowButtonStyle)  #MahApps.Metro.Controls.ButtonHelper - GrayBrush10 . TextBoxBorderBrush / 	TextBrush 0 DefaultFont 1 UpperCaseContentFontSize 2 
WhiteBrush 3 
GrayBrush8 4 SoundToggleButton 5 mute 	6 alarm 7 ExpanderStyle1 8 AccentColorBrush3V � OverridesDefaultStyle 9 ExpanderToggleButton2 : ExpanderStyle2W �Name ; TransparentExpander+  %MahApps.Metro.Controls.ControlsHelper < 
BlackBrush = ContentFontSize+  %MahApps.Metro.Controls.GroupBoxHelper+  %MahApps.Metro.Controls.ExpanderHelper > ExpanderUpHeaderStyle ? ExpanderDownHeaderStyle @ ExpanderLeftHeaderStyle A ExpanderRightHeaderStyle'	  !System.Windows.VisualStateManagerX 	  VisualStateGroups%
  System.Windows.VisualStateGroupY 
  States   System.Windows.VisualStateZ   
Storyboard9  3System.Windows.Media.Animation.EasingDoubleKeyFrame B ControlsDisabledBrush[ ]�Name\ �� HeaderStringFormat] �� ContentStringFormat^   HeaderFontSize_   HeaderFontStretch`   HeaderFontWeighta �� Mode C 	TextStyleb ��Namec �� Self D DefaultMenu&   System.Windows.Media.TextOptions E DefaultMenuItemd �� AncestorTypeO Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem  F DefaultMenuItemTemplatee m� Iconf �� HeaderStringFormatg �� ContentStringFormath m� InputGestureTexti �� Memberj ]� 
Conditionsk �� Propertyl �� Value G CheckBoxPopupStylem � 	StaysOpen*   $AIC.Resources.Controls.PopupBehaviorn   ClosesOnInput H RBStyle1 I TextButtonStyle�� D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    :x,http://schemas.microsoft.com/winfx/2006/xaml     ;Controls,http://metro.mahapps.com/winfx/xaml/controls  -ctrl$clr-namespace:AIC.Resources.Controls  %Mt  &	        &	      &	       &	 0     &	 @     &	 P     &	 `     &	 p     &	 �     &		 �     &	
 �     &	 <    &	 �    &	 �    &	 >    &	 �    &	     &	 t    &	 �    '��      7  7  7  7  7  7  'n�     &	 �    7  7  &	 �    &	     7  7  '  �    '�� �     7  7  7  7  7  7  '�� �3    7  7  7  7  &	 �9    7 	 7  7  7  7  &	  D;    &	! �;    '�� �<    7  7   7 ! 7 	 7  7   7  7  7   7 ! 7 
 7  7   7  &	" tA    &	# *D    &	$ �H    &	& 8J    7  &	) K    &	* �K    &	, SM    &	4 �O    7  &	7 }T    7 $ &	9 �V    7  7  7  7  7   7  7  &	: �X    7 9 7 	 7  7   7  &	; [    7 > 7 ? 7 @ 7 A &	C id    &	D 1f    7 E &	E Bg    &	F �h    &	G �n    7 H 7 H 7 H 7 H &	I �o    �� $
  #AAA���� $
  #888���� $
  #AAA���� $
  #444���� $
  #DDD���� $
  #888���� $
  #EEE���� $
  #FFF���� $
  #DDD���� $	 0,0&�$	 0,1&���� � $
 #FFF��$	 0.0Q�� $
 #CCC��$	 1.0Q��� $	 0,0&�$	 1,0&���� � $
 #FFF��$	 0.0Q�� $
 #CCC��$	 1.0Q��� $	 0,0&�$	 0,1&���� � $
 #FFF��$	 0.0Q�� $
 #EEE��$	 1.0Q��� $	 0,0&�$	 1,0&���� � $
 #FFF��$	 0.0Q�� $
 #EEE��$	 1.0Q��� $	 0,0&�$	 0,1&���� � $
 #FFF��$	 0.0Q�� $
 #AAA��$	 1.0Q��� $	 0,0&�$	 0,1&���� � $
 #BBB��$	 0.0Q�� $
 #EEE��$	 0.1Q�� $
 #EEE��$	 0.9Q�� $
 #FFF��$	 1.0Q��� $	 0,0&�$	 0,1&���� � $
 #CCC��$	 0.0Q�� $
 #444��$	 1.0Q��� $	 0,0&�$	 1,0&���� � $
 #CCC��$	 0.0Q�� $
 #444��$	 1.0Q��� $	 0,0&�$	 0,1&���� � $
 #777��$	 0.0Q�� $
 #000��$	 1.0Q��� $	 0,0&�$	 0,1&���� � $
 #444��$	 0.0Q�� $
 #888��$	 1.0Q��� " �� Z� .���  �@�FocusVisualStyle Z� ��  � ��$ 3q���  � �� �� "	 ��
 ���� HoverOn .��W� $ 00:00:00.1000000D�$ BackgroundOver_Highlight��$ Opacity�$
 0.65X��� HoverOff .��W� $ 00:00:00.4000000D�$ BackgroundOver_Highlight��$ Opacity�$ 0X��� 	PressedOn .��W� $ 00:00:00.1000000D�$ BackgroundPressed��$ Opacity�$
 0.84X�W� $ 00:00:00.1000000D�$  BackgoundPressed_Highlight��$ Opacity�$
 0.65X��� 
PressedOff .��W� $ 00:00:00.4000000D�$ BackgroundPressed��$ Opacity�$ 0X�W� $ 00:00:00.4000000D�$  BackgoundPressed_Highlight��$ Opacity�$ 0X��� 	FocusedOn .��U� $ 00:00:00X�$ FocusVisualElement��$ (UIElement.Opacity)�.\��� $ 00:00:00.1000000��$ 1Q��� 
FocusedOff .��U� $ 00:00:00X�$ FocusVisualElement��$ (UIElement.Opacity)�.\��� $ 00:00:00.3000000��$ 0Q�.R�� $
 Grid��.+��� $ BackgroundNorm��$��1q����	#FF8E8F8F$ 3������ $ 0.5,1&�$ 0.5,0&�.��� $ 	#FFFFFFFF��$ 0Q�� $ 	#FFEDEDED��$ 0.171Q�� $ 	#FFE7E7E7��$	 0.5Q�� $ 	#FFDCDCDC��$
 0.51Q�� $ 	#FFEAEBEC��$ 1Q��� $ BackgroundOver_Highlight��$��1q����	#FFD3D4D5$��1,0,1,1q�$ 3��$ 0Q����� $ 1,0.5&�$ 0,0.5&�.��� $ 	#99898C8E��$ 0Q�� $ 	#0095D9FF��$ 0.201Q�� $ 	#0095D9FF��$ 0.799Q�� $ 	#99898C8E��$ 1Q��� $ BackgroundPressed��$��1q�$ 3��$ 0Q����� $ 0.5,1&�$ 0.5,0&�.��� $ 	#FFA8AAAB��$ 0Q�� $ 	#FFD5D7D8��$ 0.152Q�� $ 	#FFE3E9F0��$	 0.5Q�� $ 	#FFDBDEDF��$ 0.509Q����� $ 0.5,1&�$ 0.5,0&�.��� $ 	#FF6C6D6E��$ 0Q�� $ 	#FF97999B��$ 1Q��� $  BackgoundPressed_Highlight��$��1q�$	 2.5��$ 0Q����� $ 1,0.5&�$ 0,0.5&�.��� $ 	#FFA1A4A5��$ 0Q�� $ 	#A5D1D1D1��$ 0.037Q�� $ 	#0095D9FF��$ 0.143Q�� $ 	#0095D9FF��$ 0.857Q�� $ 	#A5D1D1D1��$ 0.963Q�� $ 	#FFA1A4A5��$ 1Q��� $ Background_overlay��$��1,1,1,0q�$��1q�$ 3,3,0,0������ $ 0.5,1&�$ 0.5,0&�.��� $ 	#00FFFFFF��$ 0Q�� $ 	#72FFFFFF��$ 0.157Q�� $ 	#72FFFFFF��$	 0.5Q�� $ 	#00FFFFFF��$
 0.51Q��� $ DisabledVisualElement�����	#FFF1F1F1���	#FFB2B2B2$��1q�$ 3�� .  $y�	Collapsed=��� $ contentPresenter��#��z��#��z��#��z��#��z��#��z���� $ DisabledOverlay��$ 1��#��z�����	#FF969696#��z��#��z��#��z��$y�	Collapsed=��� $ FocusVisualElement��$��1q����	#FF737474$��1q�$ 2�� .  $ 0Q��� $ DefaultBorder��$��1q����	#FF737474$��1q�$ 2�� .  $ 0Q� P�  �@��	IsDefault . . ��� $ DefaultBorder�� �@J�Opacity$ 1Q�P�  �@J�IsKeyboardFocused .  �� $  FocusedOff_BeginStoryboard��8��    �� $ FocusedOn_BeginStoryboard��8�� P�  �@J�IsMouseOver .  �� $ HoverOff_BeginStoryboard��8��   �� 8�� P�  � �� .  �� 8��   �� 8�� P�  � {� . P�  � {� .  . ��� # �   � ���� $ DisabledVisualElement�� � y�$ Visible=��� $ DisabledOverlay�� � y�$ Visible=��� $ contentPresenter�� � y�$ 	Collapsed=��� " n�.���  � ��$ 5����  � ��
 ���� ��  �@�Cursor$ SizeWE����  � �� �� "	 n�.R��� #��z��#! z���� "   .���  � �� �	#FF152937��  � ��$
 Left=���  � ��$ Center=���  � ��$ 12(���  � �� .  ��  � ��$ 4,4,5,4q���  � �� �� "	   
 ���� HoverOn .��U� $ 00:00:00X�$ BackgroundRectangle��$ (UIElement.Opacity)�.\��� $ 00:00:00.2000000��$	 0.8Q��� HoverOff .��U� $ 00:00:00X�$ BackgroundRectangle��$ (UIElement.Opacity)�.\��� $ 00:00:00.3000000��$	 0.5Q�.R�� " �� $
��Auto ��� $
��Auto ��� $��* ��� $
��Auto �.+��� $# BackgroundRectangle��$��4��$��2��$
$ Fill=�$��1��$	 0.5Q����	#FFDCDCDC���� $ 0.5,1&�$ 0.5,0&�.��� $ 	#FFBDC2C4��$ 0Q�� $ 	#FFA3A3A3��$ 0.171Q�� $ 	#FFB4B4B4��$	 0.5Q�� $ 	#FFB4B4B4��$
 0.51Q�� $ 	#FFCCCFD1��$ 1Q��� #% z% #��z��#��z��#��z��#& z& F� $' 	SortArrow��$��Right=�$��Center=�$��2��$( .5,.5&����	#FF000000$��8��$ 0Q�$$ Uniform=�+�����l��8] ��O 8] �        ��l��8] $��2��z�V� .~��� $) .9Q�$* .9Q�SortIconTransform��n� $+ PART_LeftHeaderGripper��$��0��$
��Left=�n� $+ PART_RightHeaderGripper��$��3��$��Right=� P�  �@  SortDirection$ 	AscendingX�. ��� $ 	SortArrow�� �@J�Opacity$ 1Q��� $ 	SortArrow�� � z� �� $	, 180Q��� $ BackgroundRectangle�� �@J�Opacity$ 1Q�P�  �@  SortDirection$ 
DescendingX�. ��� $ 	SortArrow�� �@J�Opacity$ 1Q��� $ BackgroundRectangle�� �@J�Opacity$ 1Q�P�  �@J�IsMouseOver .   �� 8��   �� 8�� �� "  .���  �@ RowBackground
 ���� ��  � ��$ 13(���  � �� Z� ��  � ��$ Center=���  �@ HeadersVisibility$ Column=��� % �@ HorizontalScrollBarVisibility$
 Auto=���   �@ HorizontalGridLinesBrush �	#FF7D8080�� # �@ VerticalScrollBarVisibility$
 Auto=���  �@ VerticalGridLinesBrush �	#FF7D8080��  �@ SelectionMode$ Single=���  �@ CanUserReorderColumns . ��  �@ CanUserResizeColumns . ��  �@ CanUserSortColumns .  ��  �@ AutoGenerateColumns .  ��  �@ 
IsReadOnly . ��  � |� .  ��   �@ RowDetailsVisibilityMode$ VisibleWhenSelected=���  � ��$ 0q���  �@ GridLinesVisibility$	 All=���  � �� . ��  �@ SelectionUnit$ FullRow=��� "  .���  � ��$ Center=���  �@�FocusVisualStyle Z� ��  � ��
 ���� ��  � ��$ 0q���  �@�ToolTip �� *Content.Text+- �� *Self+��  � �� �� "	  
 ���� 
SelectedOn .��U� $ 00:00:00X�$ select_gradient��$ (UIElement.Opacity)�.\��� $ 00:00:00.1000000��$ 1Q��� SelectedOff .��U� $ 00:00:00X�$ select_gradient��$ (UIElement.Opacity)�.\��� $ 00:00:00.3000000��$ 0Q�.R��� #��z��#��z��#��z��% . ._�� .+��� $# select_gradient��$��0q�$��1��$��0��$ 0Q� .  ���� $ 0.5,1&�$ 0.5,0&�.��� $ 	#FFBDC2C4��$ 0Q�� $ 	#FFA3A3A3��$ 0.171Q�� $ 	#FFB4B4B4��$	 0.5Q�� $ 	#FFB4B4B4��$
 0.51Q�� $ 	#FFCCCFD1��$ 1Q��� $��2,1,2,1q�#% z% #��z��#��z�� P�  �@ 
IsSelected .  �� $! SelectedOff_BeginStoryboard��8��    �� 8�� �� "  .���  � ��$ 25��. P�  �@J�IsMouseOver . . ���  � �� �� $ 0.5,1&�$ 0.5,0&�.��� $ 	#AAFFFFFF��$ 0Q�� $ 	#AAEDEDED��$ 0.171Q�� $ 	#AAE7E7E7��$	 0.5Q�� $ 	#AADCDCDC��$
 0.51Q�� $ 	#AAEAEBEC��$ 1Q�P�  �@ 
IsSelected . . ���  � �� �� $ 0.5,1&�$ 0.5,0&�.��� $ 	#FFBDC2C4��$ 0Q�� $ 	#FFA3A3A3��$ 0.171Q�� $ 	#FFB4B4B4��$	 0.5Q�� $ 	#FFB4B4B4��$
 0.51Q�� $ 	#FFCCCFD1��$ 1Q��� " ��.���  �@J�SnapsToDevicePixels . ��  � |� .  ��  � �� �� "	 ��
 ���� 	FocusedOn .��U� $ 00:00:00X�$ FocusVisualElement��$ (UIElement.Opacity)�.\��� $ 00:00:00.1000000��$ 1Q��� 
FocusedOff .��U� $ 00:00:00X�$ FocusVisualElement��$ (UIElement.Opacity)�.\��� $ 00:00:00.3000000��$ 0Q�.R�� .+�d� $/ ToggleButton��|�.  $0 Press=�1 �� $2 IsDropDownOpen�$3 TwoWay=�- �� *TemplatedParent+���� "	 d�
 ���� HoverOn .��W� $ 00:00:00.1000000D�$ BtnOver��$ Opacity�$	 0.8X�W� $ 00:00:00.1000000D�$ Background_over��$ Opacity�$ 1X��� HoverOff .��W� $ 00:00:00.4000000D�$ BtnOver��$ Opacity�$ 0X�W� $ 00:00:00.4000000D�$ Background_over��$ Opacity�$ 0X��� 	PressedOn .��W� $ 00:00:00.1000000D�$ BtnPress��$ Opacity�$	 0.8X�W� $ 00:00:00.1000000D�$ Background_press��$ Opacity�$ 1X�W� $ 00:00:00.1000000D�$ BtnPress_highlight��$ Opacity�$ 1X��� 
PressedOff .��W� $ 00:00:00.4000000D�$ BtnPress��$ Opacity�$ 0X�W� $ 00:00:00.4000000D�$ Background_press��$ Opacity�$ 0X�W� $ 00:00:00.4000000D�$ BtnPress_highlight��$ Opacity�$ 0X�.R�� .+��� $# 
Background�����	#FFABAEB3$4 3��$5 3�� .  ���� $ 0.5,1&�$ 0.5,0&�.��� $ 	#FFFFFFFF��$ 0Q�� $ 	#FFEDEDED��$ 0.171Q�� $ 	#FFE7E7E7��$	 0.5Q�� $ 	#FFDCDCDC��$
 0.51Q�� $ 	#FFEAEBEC��$ 1Q��� $# Background_over�����	#FFABACAD$4 3��$5 3��$ 0Q� .  �� $# Background_press�����	#FFABABAB$4 3��$5 3��$ 0Q� .  �� $ BtnOver��#��z6 ���	#FFABABAB$��1q�$ 3��$ 0Q�$��Right=����� $ 0.5,1&�$ 0.5,0&�.��� $ 	#FFE1E1E1��$ 0Q�� $ 	#FFEDEDED��$ 0.171Q�� $ 	#FFDDDDDD��$	 0.5Q�� $ 	#FFC9C9C9��$
 0.51Q�� $ 	#FFEDEDED��$ 1Q��� $ BtnPress��#��z6 ���	#FF595A5B$��1q�$ 3��$ 0Q�$��Right=����� $ 0.5,1&�$ 0.5,0&�.��� $ 	#FFA8AAAB��$ 0Q�� $ 	#FFD5D7D8��$ 0.152Q�� $ 	#FFE3E9F0��$	 0.5Q�� $ 	#FFDBDEDF��$ 0.509Q��� $ BtnPress_highlight��$��1q�$ 2��$ 0Q�$��Right=����� $ 1,0.5&�$ 0,0.5&�.��� $ 	#FF919191��$ 0Q�� $ 	#A5CED1D2��$ 0.088Q�� $ 	#0095D9FF��$ 0.221Q�� $ 	#0095D9FF��$ 0.779Q�� $ 	#A5CED1D2��$ 0.912Q�� $ 	#FF919191��$ 1Q��� $ 
BtnOverlay��$��1q�$ 2��$��Right=����� $ 0.5,1&�$ 0.5,0&�.��� $ 	#00FFFFFF��� $ 	#72FFFFFF��$ 0.189Q�� $ 	#72FFFFFF��$	 0.5Q�� $ 	#00FFFFFF��$
 0.51Q�F� $' BtnArrow��$��0,0,7,0q�$��6��+���� ��v���P.��v���8�BІ�� ��v��$��Right=�$$ Uniform=����� $ 0.5,1&�$ 0.5,0&�.��� $ 	#FF616368��$ 0Q�� $ 	#FF000000��$ 1Q��� $# DisabledVisualElement��$��1q����	#00FFFFFF$4 3��$5 3�� .  $y�	Collapsed=� P�  �@J�IsMouseOver .  �� $ HoverOff_BeginStoryboard��8��    �� 8�� P�  �@d�	IsChecked$
 trueY�P�  � �� .  �� $  PressedOff_BeginStoryboard��8��   �� $ PressedOn_BeginStoryboard��8�� P�  � {� .  . ��� # �   � ���� $ DisabledVisualElement�� � y�$ Visible=��� $ ContentSite��$
��Left=�$��3,3,23,3q�$��Center=�#��z7 #��z8 #��z�� .  �� $9 PART_EditableTextBox��$y�Hidden=�$
��Left=�$��3,3,23,3q�$��Center=�|�. 
������ #; z: ��Z� .������ "	 ��.R��� $ PART_ContentHost��|�.  #��z��.���� $# DisabledVisualElement�����	#A5FFFFFF$4 4��$5 4�� .  $y�	Collapsed=��� $# FocusVisualElement��$��-1q����	#FFCECFD0$��1��$4 4��$5 4�� .  $ 0Q�� $< Popup��#��z= $��Bottom=�|�.  > . $��Slide=�.��� $ DropDown��#��z? #��z6 % . .+��� $ DropDownBorder�����	#FFFFFFFF$��1q�$ 1,1,3,3������ $ 0.5,1&�$ 0.5,0&�.��� $ 	#FFABAEB3��$ 0Q�� $ 	#FFD4D7D9��$ 0.355Q�._��� $��4,6,4,6q�% . $
��Auto=�$
��Auto=���. .���� @ . $A 	Contained=� P�  �@J�	IsFocused .  �� $  FocusedOff_BeginStoryboard��8��   �� 8�� P�  �@��HasItems .  . ��� $ DropDownBorder�� � ��$ 95��P�  � {� .  . ���  � ��
 �   ��� $ DisabledVisualElement�� � y�$ Visible=�P�  �@��
IsGrouping . . ���  � �� .  P� $B Popup�� �@�AllowsTransparency . . ��� $ DropDownBorder�� �@��CornerRadius$ 4���� $ DropDownBorder�� � ��$ 0,2,0,0q�P�  �@��
IsEditable . . ���  � �� .  �� $ PART_EditableTextBox�� � y�$ Visible=��� $ ContentSite�� � y�$ Hidden=��� " ��.���  �@J�SnapsToDevicePixels . ��  � ��$ Stretch=���  � ��$ Center=���  � ��$ 3q���  � |� .  ��  � �� �� "	 ��
 ���� HoverOn .��W� $ 00:00:00.1000000D�$ BackgroundGradientOver��$ Opacity�$
 0.73X��� HoverOff .��W� $ 00:00:00.4000000D�$ BackgroundGradientOver��$ Opacity�$ 0X��� 
SelectedOn .��W� $ 00:00:00.1000000D�$  BackgroundGradientSelected��$ Opacity�$
 0.84X��� SelectedOff .��W� $ 00:00:00.4000000D�$  BackgroundGradientSelected��$ Opacity�$ 0X�.R�� % . C Z� .+��� $# BackgroundGradientOver�����	#FFCBCDCE$4 2��$5 2��$ 0Q�C Z� ���� $ 0.5,1&�$ 0.5,0&�.��� $ 	#FFE0E3E4��$	 0.5Q�� $ 	#FFC2C5C6��$	 0.5Q�� $ 	#FFB7BABC��$ 1Q��� $ # BackgroundGradientSelected�����	#FFBCBEBF$4 2��$5 2��$ 0Q�C Z� ���� $ 0.5,1&�$ 0.5,0&�.��� $ 	#FFFFFFFF��$ 0Q�� $ 	#FFEDEDED��$ 0.171Q�� $ 	#FFE7E7E7��$	 0.5Q�� $ 	#FFDCDCDC��$
 0.51Q�� $ 	#FFEAEBEC��$ 1Q��� $# BackgroundHighlight��$��1q����	#A0FFFFFF$4 1��$5 1��C Z� �� $ contentPresenter��#��z��#��z��#% z% #��z��C Z�  P�  �@J�IsMouseOver .  �� $ HoverOff_BeginStoryboard��8��    �� $ HoverOn_BeginStoryboard��8�� P�  �@��IsHighlighted . P�  �@��
IsSelected .  �� $" SelectedOff_BeginStoryboard1��8��   �� $! SelectedOn_BeginStoryboard1��8�� P�  � {� .  . ��� # �   � ���� " ��.���  �@J�SnapsToDevicePixels . ��  �@�OverridesDefaultStyle . ��  � |� .  ��  � �� �� "	 ��.R��� $��Border��$��1q�$ 2��8��  8�� $��1q�._�F� $��Center=�$��Center=�8�� ���� $2 Content�- �� *TemplatedParent+ P�  � �� . . ��� $ Border��8   � ��P�  � {� .  . ��� 8   � ���� " ��.���  �@J�SnapsToDevicePixels . ��  �@�OverridesDefaultStyle . ��  � �� .  ��  � |� .  ��  � �� �� "	 ��.R��� 
������ �� " n�.���  �@J�SnapsToDevicePixels . ��  �@�OverridesDefaultStyle . ��  � �� .  ��  � |� .  ��  � �� �� "	 n�.R��� $ 2��#��z��#��z��$��1q��� " ��.���  �@J�SnapsToDevicePixels . ��  �@�OverridesDefaultStyle . . P�  �@��Orientation$ 
Horizontal=�. ���  � ��$
 Auto����  � ��$ 18����  � �� �� "	 ��.R�� " �� $��18���� $��0.00001* ��� $��18��.+��� $��3��$ 2�����#F0F0F0�� $��0��8��  $��18��$��ScrollBar.LineLeftCommand��$��M 4 0 L 4 8 L 0 4 Z��]� $��
PART_Track��$��1����.  D �� 8�� $��ScrollBar.PageLeftCommand��E n� 8�� 8�� 8�� $��0,1,0,1q�F �� 8�� $ ��ScrollBar.PageRightCommand���� $��3��8�� $��18��$ ��ScrollBar.LineRightCommand��$��M 0 0 L 4 4 L 0 8 Z��P�  �@��Orientation$ Vertical=�. ���  � ��$ 18����  � ��$
 Auto����  � �� �� "	 ��.R�� G �� $��18���� $��0.00001* ��� $��18��.+��� $��3��$ 2�����#F0F0F0�� $��0��8�� $��18��$��ScrollBar.LineUpCommand��$��M 0 4 L 8 4 L 4 0 Z��]� $��
PART_Track��$��1����. D �� 8�� $��ScrollBar.PageUpCommand��E n� 8��	 8��
 8�� $��1,0,1,0q�F �� 8�� $��ScrollBar.PageDownCommand���� $��3��8�� $��18��$��ScrollBar.LineDownCommand��$��M 0 0 L 4 4 L 8 0 Z���� " 9�.���  � �� �	#FF217cb5��  � ��$ 宋体)���  � ��$ 14(���  � ��
 ���� ��  � ��$ Stretch=���  � ��$ Stretch=���  � ��
 ���� ��  � ��$ 0q���  � �� �� "	 9�.R��� #��z��% . .+�d� $/ 
HeaderSite��$H 0��#��z��#��z��#��z��#��� # 1 �� *
IsExpanded+$3 TwoWay=�- �� *TemplatedParent+�� $ 
ExpandSite��#��z��#��z��$y�	Collapsed=�$H 35��$I 0��|�.  #��z��#��z�����	#7Ae9f4fa���� $J 
HeaderSite��$2 Width�._��� $��10 10q�#��z��#��z�� P�  �@9�
IsExpanded . . ��� $ 
ExpandSite�� � y�$ Visible=�P�  � {� .  . ��� # � �� � ���� " d�.���  �@�FocusVisualStyle Z� ��  � ��$	 245����  � ��$ 35����  � �� �	#7Ad2e7f4��  � �� �	#FF217cb5��  � ��$ 14(���  � ��$ 宋体)���  � ��$
 Bold"���  � ��$ 0q���  � ��$
 Left=���  � ��$ Center=���  � ��$ 10 10q���  � �� �� "	 d�.R��� #��z��#��z��#��z��% . .+��� $K 
canvNormal��$y�Visible=�#��z��#��z��#��z��% . $L 0��.+��� $��15��$��35�����	#FFc7ebff$L 0���� $M 0��$N 0��$O 0��$P 15�����#297eb4$��3��$
��6 10q�$Q 1R�$L 0���� $K canvChecked��$y�Hidden=�#��z��#��z��#��z��% . $L 0��.+�F� U������� (9� QȊ� ���Sc@Ѡ.c (9� Q0c -��� @QȊ� |?5^��@ш�� �z~ ���� (9� 	���#297eb4$L 5��$H 8��F� U������� (9� QȊ� ���Sc@Ѡ.c (9� Q0c -��� @QȊ� |?5^��@ш�� �z~ ���� (9� 	���#297eb4$L 5��$H 15���� #��z��$��1��$R 0�����	#FFbacfda�� #��z��#��z����. #% z% #��z�� P�  � �� . . ���  � �� �	#FFd2e7f4P�  �@d�	IsChecked$
 trueY�. ��� $ canvChecked�� � y�$ Visible=��� $ 
canvNormal�� � y�$ Hidden=�P�  � {� .  . ��� # � �� � ���� "	 d�.R��� $��border��#��� % $��8��$	��119��$��Right=�$��Stretch=�._��� $��image��$1��+/AIC.Resources;component/Images/ss_left.png��$��8��$��7��$��Center=�$��Center=�S .  P�  �@d�	IsChecked$
 TrueY�. ��� $ image�� � ��$2 ,/AIC.Resources;component/Images/ss_right.png���� "	 d�.R��� $T stack��$U 
Horizontal=�.+��� $	 txt��#��� ' $��Center=� P�  �@d�	IsChecked$
 TrueY�. ��� $	 txt��# � (  � ��P�  �@J�IsMouseOver . . ��� $ stack��8    � ���� "	 d�.R��� $	 txt��$��Center=�#��z��#��z��#��z��$��	Underline{� P�  �@d�	IsChecked$
 TrueY�. ��� $	 txt�� � ��
 �  ���� "	 d�.R��� #��� % $��1q�._�� $
 grid��$��20��$��16�� . .+�F� $' IconPath��$��2q�$
$ Fill=�#��� % X����БA(���?�O��!hP���!h�b�БA�|���b��P��X�q�O�БA(� P�  �@d�	IsChecked$
 TrueY�. ��� $ IconPath�� � ��F ��БAD�[���|����БAP���!h���!h�|�БAD�[P� $
B grid�� �@J�IsMouseOver . . ��� # � +  � ���� " d�.���  �@ CornerRadius$ 3����  � ��# � - ��  � ��# � . ��  � ��# � / ��  � ��# � 0 ��  � ��# � 1 ��  � ��$
 Bold"���  � ��$ 0q���  � ��$ 1q���  � ��$ 12����  � �� �� "	 d�.R�� $
 grid��#��� 2 .+��� #��� % $ 2��$��1q�._�F� $' IconPath��$��2q�$
$ Fill=�#��� % X����БA(���?�O��!hP���!h�b�БA�|���b��P��X�q�O�БA(� P�  �@d�	IsChecked$
 TrueY�. ��� $ IconPath�� � ��F ��БAD�[���|����БAP���!h���!h�|�БAD�[P�  �@J�IsMouseOver . . ��� $
 grid��# � 3  � ���� "	 d�.R��� $T stack��$U 
Horizontal=�.+�F� $
' path��$��18��$��18��$
$ Fill=�
��� � �������:,m�Q�Ś�ǘ���@@�D�+D���D�+b��Ś�g���:�g��ln�g�\�	��v\�	$\�\�	��+�\�	�ln,m��:,m���e4�C��eP�H����D������8���`4�U���5����54�CӠ�5|9,�W���D���������E�p��eg��e4�C�$84�C�$8��xT����H��H���X��������ć�����pk���4�CӨ��<d��I��&@@X��B��H�� ���T��\��$8�$84�C�,��4�C�,���!d����p zr����$\$\�(li$\�=t�BQ�=t4�CӰ=t̑6(li��+$\��+Q zr�2ı.�@@Ӵ���c,����#,��4�C�� $	 txt��#��� 5 $��Center=� P�  �@d�	IsChecked$
 TrueY�. ��� $
 path�� � ��� ���:,m�Q�Ś�ǘ���@@�D�+D���D�+b��Ś�g���:�g��ln�g�\�	��v\�	$\�\�	��+�\�	�ln,m��:,m��(tp�l������OQ(tp�e��a�@@����N���#���|�N`O�Q\Ӂ� �rh�@@�8f,��O�$ԁ�l����N�����#��@��������(tp�l��� $
 path�� � ��
 �  ���� $	 txt��# � 6  � ��P�  �@J�IsMouseOver . . ��� $ stack��8    � ���� " 9�.���  � �� �� "	 9�.R�� #��� 8 " �� $
��Auto ��� $��8 �.+��� $ Content��$��0,0,0,0q�$ 0,0,0,0��$y�	Collapsed=�._��� $��0q��� $ 	BtnBorder��$��1��$��0q�$ 0,0,0,0��._�� .+�d� V . 8��  1 �� *
IsExpanded+$3 TwoWay=�- �� *TemplatedParent+�� $��2��$��0q�$��Header����.  P�  �@9�
IsExpanded .  . ��� $ Content�� � y�$ Visible=�P�  �@9�ExpandDirection$
 Left=�. ��� $ Content�� � ��$
 Left=��� $ 	BtnBorder�� � ��$ Right=�P�  � {� .  . ��� # � �� � ���� "	 d�.R��� $��Border��$ 2,0,0,0��
������ 8��  $��0,0,1,0q�._�F� $��Arrow��8�� $��Center=�$��Center=�"����        � 	=  	= � z     	 P�  �@J�IsMouseOver . . ��� $ Border��8   � ��P�  �@d�	IsChecked$
 trueY�. ��� $ Arrow�� � ��" ��     	= � 	=     � z  	= 	P�  � {� .  . ��� $ Border��8   � ���� $ Border��8   � ���� 8   � ���� $ Arrow��8   � ���� " 9�.���  � �� �� "	 9�.R�� G �� $
��Auto ��� $W 
ContentRow��$��0 �.+��� $��Border��$��0��$��1q�$ 2,2,0,0��._�� " �� $��20 ��� $��* �.+�d� V . 8��  8�� 1 �� $2 
IsExpanded�$3 TwoWay=�- �� *TemplatedParent+�� $��1��$��4q�$��Header����. �� $��Content��$��1��$��1,0,1,1q�$ 0,0,2,2��._��� $��4q� P�  �@9�
IsExpanded . . ��� $ 
ContentRow�� �� $J Content��$2 DesiredHeight� � ��P�  � {� .  . ��� $ Border��8   � ���� $ Border��8   � ���� 8   � ���� " 9�.���  �@ ContentCharacterCasing$ Upper=���  � ��$ 5q���  �@J�SnapsToDevicePixels . ��  � ��# � < ��  � ��# � % ��  � ��
 ���� ��  � ��$ 1q���  �@ HeaderFontSize# � = ��  �@ HeaderForeground Z� ��  �@ HeaderUpStyle8   ��  �@ HeaderDownStyle8  ��  �@ HeaderLeftStyle8  ��  �@ HeaderRightStyle8  ��  � �� �� "	 9�.R�� 
������ X 
  CommonStates��.Y   
Normal��  	MouseOver��  Disabled��.Z �� .��U� $ Opacity�$ DisabledVisualElement��.\��� $ 0��$	 0.7Q�U� $ (UIElement.Opacity)�$ ExpanderRoot��.\�  $ 0��$	 0.3Q�.+��� $# DisabledVisualElement��#��� B  .  $ 0Q�]� $[ ExpanderRoot��
������ .+��� $ 
HeaderSite��#��z��#��z��#% z% S . #��z��$	��Top=�._�d� $/ 
ToggleSite��S .  #��z��#��z��#��z��#] z\ #��z^ #��z_ #��z` 1 �� *
IsExpanded+$3 TwoWay=�- �� *TemplatedParent+���� - �� *TemplatedParent+$/2 )(Controls:ExpanderHelper.HeaderDownStyle)��� $ 
ExpandSite��#��z��#��z��#% z% S . 
������ #��z��|�.  $ 0Q�$y�	Collapsed=�._��� S .  #��z��#��z��#��z��#& z&  P�  �@ ContentCharacterCasing$ Normal=�. ��� $ 
ToggleSite�� �� - �� $a TemplatedParent=�$2 Header� � ��P�  �@9�
IsExpanded . . ��� $ 
ExpandSite�� � y�$ Visible=�  �� .���� .��W� $ 0:0:0.25D�$ 
ExpandSite��$ (UIElement.Opacity)�$ 1X� �� .���� .��W� $ 0:0:0.25D�$ 
ExpandSite��$ (UIElement.Opacity)�$ 0X�P�  �@9�ExpandDirection$ Right=�. ��� $ 
ExpandSite�� � ��$ Right=��� $ 
HeaderSite�� � ��$
 Left=��� $ 
ToggleSite�� �� - �� *TemplatedParent+$02 *(Controls:ExpanderHelper.HeaderRightStyle)� � ��P�  �@9�ExpandDirection$ Up=�. ��� $ 
ExpandSite�� � ��$	 Top=��� $ 
HeaderSite�� � ��$ Bottom=��� $ 
ToggleSite�� �� - �� *TemplatedParent+$-2 '(Controls:ExpanderHelper.HeaderUpStyle)� � ��P�  �@9�ExpandDirection$
 Left=�. ��� $ 
ExpandSite�� � ��$
 Left=��� $ 
HeaderSite�� � ��$ Right=��� $ 
ToggleSite�� �� - �� *TemplatedParent+$/2 )(Controls:ExpanderHelper.HeaderLeftStyle)� � ��P�  � {� .  . ��� # � �� � ���� " ��.���  � �� �� "	 ��.R��� $ border��$
��Auto��$
��Auto��$��1q����	#FF7F9DB9._�� $
 grid�����#FFFFFF.+��� $b PART_ContentHost��$��Center=�$
��Left=���  �@=�ErrorTemplate �� .R��� $U 
Horizontal=�.+��� 
���  ��$��Center=�$��18(�!�� . P�  �@=�HasError . . ���  � ��
 �  ����  �@�ToolTip �� #- Zc $22 ,(Validation.Errors).CurrentItem.ErrorContent��� " o�.���  �@J�SnapsToDevicePixels . ��  �@��ClearTypeHint$ Enabled=���  �@ TextFormattingMode$ Ideal=���  � ��
 ���� ��  � ��8   ��  � �� �� "	 o�.R��� #��z��#��z��#��z��#! z��#% z% ._��� $��0q�#% z% �� " m�.���  � ��$
 Auto����  � ��$
 Auto����  � ��$ -28,0,-28,0q���  � ��$ 0q���  � �� �� .R��� $ 	txtHeader��$��Stretch=�$��0,1,0,1q�$��Center=����� *
FontSize+- �� "d m�$a FindAncestor=����� *Header+- �� "d m�$a FindAncestor=����� *
Foreground+- �� "d m�$a FindAncestor=��� D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    :x,http://schemas.microsoft.com/winfx/2006/xaml     ,s&clr-namespace:System;assembly=mscorlib  "	 m�.R��� $��templateRoot��#��z��#��z��#��z��% . $��0,0,0,0q�._�� $��-1,-1,-1,-1q�" �� $
��Auto �$��0���� $��0 ��� $��* ��� $��0 ��� $��0 ��� $��0 �.+��� $
��Icon��#��ze $
��Icon��$��16��$��16��$��3,0,3,0q�$
��Left=�$��Center=�#% z% �� $��
GlyphPanel��$��1,1,1,1q����	#FF26A0DA���	#3D26A0DA$��22��$��22��$��-1,0,0,0q�$��Center=�$��Center=�$y�Hidden=�}�.  ._�F� $��Glyph��T������� �O �`�G �ڊ � �D �ڊ �    �XO � � �g5 ���A  ] ������� @    р�� �O ���	#FF212121$��10��$��11��$��LeftToRight=��� $��menuHeaderContainer����. #��z��#��z��#g zf $��Header��#��z��$
��Left=�$��Center=�#% z% $��2���� $��menuGestureText��#��zh #��z��$��Center=�$	 0.7Q�$��4�� P�  �@m�Icon. ��� $
 Icon�� � y� �� $i Visibility.Collapsed�� Z� P�  �@m�	IsChecked. ��� $ 
GlyphPanel�� � y� �� $i Visibility.Visible���� $
 Icon�� � y� �� $i Visibility.Collapsed�� ��True��P�  � {�. ��� $ templateRoot�� � �� ��	#FF707070���� $ Glyph�� � �� ��	#FF707070�� ��	False��]� j �� k �@m�IsHighlightedl ��True���� k � {�l ��	False��./��� $ templateRoot�� � �� ��	#0A000000���� $ templateRoot�� � �� ��	#21000000���� " ��.���  � �� �� "	 ��.R�� .+��� #��z��#��z��#��z��� m .  $��Slide=�n . ���� - �� *TemplatedParent+$2 	IsChecked�$3 TwoWay=�.���� .+�	� $��Option 1��$
1 TrueY�8��  	� $��Option 2��8�� 	� $��Option 3��8�� 	� $��Option 4��8�� �� " ��.���  � ��
 ���� ��  � ��$ Center=���  � ��$ Center=���  � ��$ 1q���  � �� �� "	 ��.R�� #��z��.+��� #��z��#��z����. #% z% #��z�� P�  �@J�IsMouseOver . . ���  � �� �	#7F243859P�  � {� .  . ���  � �� �#ADADADP�  � �� . . ���  � �� �	#FF20437E