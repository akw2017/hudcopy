   M S B A M L   `   `   `  ���� [  WSystem.Windows.Forms, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089Y@clr-namespace:System.Windows.Forms;assembly=System.Windows.FormsSystem.Windows.Forms   AIC.Cloud.DataReplayer4   .AIC.Cloud.DataReplayer.Views.VideoReplayerView   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     Q MAIC.Cloud.Presentation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9presentation&http://www.aicmonitor.com/Presentation  Iwf@clr-namespace:System.Windows.Forms;assembly=System.Windows.Forms  X TXceed.Wpf.Toolkit, Version=2.5.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit    � 	Resources     SplitterBackgroundBrush �� Color  NavigationForegroundBrush  NavigationBackgroundBrush  NavigationBorderBrush  TogglePlayPauseStyle �� 
TargetType �� BasedOn �� Property �� Value �� 
TargetType'  !System.Windows.VisualStateManager   VisualStateGroups%  System.Windows.VisualStateGroup   Transitions%  System.Windows.VisualTransition	   GeneratedDuration
   States   System.Windows.VisualState �� CornerRadius J� Clip F�Name �� Stretch
 �� Top �� Left �� Triggers P� Property P� Value �� 
TargetName  ToggleExpandButtonStyle   
Storyboard g� Duration	 W� To �� TargetProperty �� 
TargetName J� Opacity � UseLayoutRounding J� RenderTransformOrigin �� Angle  LeftPanelExpanded W� EasingFunction/  )System.Windows.Media.Animation.CircleEase7  1System.Windows.Media.Animation.EasingFunctionBase   
EasingMode  LeftPanelCollapsed��%)	  &	        &	      &	 *     &	 ?     &	 T     7d�&	     7d�7  7  7  &	     &	 r    �� $ 	#FFE7E7E7���� $ 	#FF3F3F3F���� $ 	#FFE7E7E7���� $ 	#00E7E7E7���� " d�8   .���  �@�Cursor$
 Hand����  � ��$ 1q���  � ��$ 1q���  � �� �� " d�.R��    CommonStates��   $	 0:0:0.1D�.
   
Normal��  Indeterminate��  FocusStates��.
   Focused��  	Unfocused��.+��� 
�������$ 2��$��1q�
�������._��� $��30��$��30��4 ��        р��    р������    ����        $��Center=�$��Center=�.+�F� $ playPath��$
 Fill=�$ 5��$ 8��
���   �4������!��� ќG��ejќG���u���!�����!���  P�  �@d�	IsChecked$
 trueY�. ��� $ playPath�� � ��a ��@f��� �@o@��� �@o@@���@f@���@f��� ������� ������ ���@������@��������� �� " d�8   .���  �@�Cursor$
 Hand����  � ��8  ��  � ��8  ��  � ��8  ��  � ��$ 1q���  � ��$ 16����  � ��$ 40����  � ��$ 1q���  � �� �� " d�.R��    CommonStates��   $	 0:0:0.1D�.
   
Normal��  	MouseOver��. �� .��W� $ 0D�$ 1X�$ (UIElement.Opacity)�$ normal��  Pressed��. �� .��W� $ 0D�$ 1X�$ (UIElement.Opacity)�$ normal��  Disabled��. �� .��W� $ 0D�$	 0.3X�$ (UIElement.Opacity)�$ normal��  CheckStates��.
   Checked��. �� .��W� $ 0:0:0.3D�$ 0X�$ Angle�$ symbolRotation��  	Unchecked��. �� .��W� $ 0:0:0.3D�$	 180X�$ Angle�$ symbolRotation��  Indeterminate��  FocusStates��.
   Focused��  	Unfocused��.+��� $��2��$��3��$��40��$��20��#��z��#��z��#��z��F� $ normal��$��Center=�$��10��$
 Fill=�$	 0.6Q������@B ��[ �@B ��[ �ا �ا �@B �ا �pr ����n@�pr ����n@S�pr ����n@��� �ا ��� �ا Ӏ�� �ا ��� ��[ ��� ��[ Ӏ�� ��[ �pr P�^����?�pr P�^����?S�pr P�^����?��[ ��[ 	#��z�� .  $��Center=�$��7��$ 0.5,0.5&�z�V� .~��� �� �� symbolRotation��$	 180Q�T� �� .��W� $ LeftPanelColumn��$	 200X�$ 
00:00:00.6D�   $ EaseOut=��� .��W� $ LeftPanelColumn��$ 0X�$ 
00:00:00.6D�   $ EaseOut=�.���   � ColumnDefinitions  �� -   ! ��Name$! LeftPanelColumn��$��* �*  $AIC.Cloud.Presentation.GridExtension"   ColumnWidth$	" 200Q��� $��3 ��� $��2* �.+�� # � RowDefinitions# �� $
��auto ��� $��* �.+��   �� $
��auto ��� $��* �# �� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto ��� $��* �.+��� $��0��$��2��$��0,0,40,0q����� *VideoChannels+$ �� SelectedItem$ �� *SelectedVideoChannel+���� .R��� 
���������`� % �� StringFormat$% {0}.{1}.{2}.{3}.{4}.{5}��.1��� & �� Path$& GroupCOName��� $& CorporationName��� $& WorkShopName��� $& DevName��� $& DevSN��� $
& Name��� $
& MSSN��� $��0��$��2��$��Right=�$��35��$��查询������ *VideoQueryCommand+�� $��起始时间:��$��1��$��0��
�������&   Xceed.Wpf.Toolkit.DateTimePicker  $��1��$��1��'   
TimeFormat$' LongTime=��	  �Xceed.Wpf.Toolkit.Primitives.UpDownBase`1[[System.Nullable`1[[System.DateTime, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]( 	  Value( �� *VideoStartTime+�� $��结束时间:��$��2��$��0��
�������  $��2��$��1��$' LongTime=�( �� *VideoEndTime+�� -   ) ��Name$) 	videoList��$��1��* �� DisplayMemberPath$
* Name������ *
VedioInfos+$ �� *SelectedVideo+� $��2��# �� $��* ��� $��18 ��� $��30 �.+��� ���#303030$��1q�$ 0��._��� -   + ��Name$+ 
InfoCanvas��$��0��$��0��.+�7
  1System.Windows.Forms.Integration.WindowsFormsHost
  -   $��flowLayoutHost��, 
  
Background
, ��������� *ActualWidth+- �� ElementName$- 
InfoCanvas������ *ActualHeight+$- 
InfoCanvas��. 
  Child.. %   System.Windows.Forms.PictureBox  -   pbReplay���� -   / ��Name$/ replayerSlider��$��1��0 � Minimum$0 0Q�1 � Maximum$	1 100Q�2 � Value$2 0Q�3 � LargeChange$3 10Q�4 � SmallChange$4 1Q�$��0,0,0,0q�$
��Auto��� $��2��.+��� 5 �� Orientation$5 
Horizontal=�$
��Left=�.+��� -   6 ��Name$6 TBpassedTime��$��Center=�$��5,0,0,0q��� $��/��$��Center=��� -   $6 TBTotalTime��$��Center=��� $5 
Horizontal=�$��Center=�.+��� -	   7 ��Name$7 btnStop��$��2,0,2,0q�8 � ToolTip8 �� $��停止��.���� $��30��$��30��4 ��        р��    р������    ����        $��Center=�$��Center=�.+�F� $
 Fill=�$ 3��$ 3��
������������ -1 � � ?� � � Q% -1� Q% ?�� ?� Q%� -1 Q%� �  ?�� �  -1� -1 � ���)@o@���)��O�@o@��O�@o@���р�O���р�O��O���f��O���f@o@���)@o@�5n$�^� 6n@x}� 6n��� 6n�Б�Ks���Lbx���4�����������h����������(���� ?��Б ?���� ?�@x}� ?���^� ?���T ?���J�(���EӴ�@o@h�@o@���@o@�4��@o@Lbx@o@�Ks��E� 6n��J 6n��T�5n$�^��`��@x}Lbx@x}�	d4����Y�(����OtБ��Oh���Y����
d���Lbxh�����tБ���(������`��@��@o@�@�����р�����р��@x}���@x}�Լ�@x}��&S@@@x}<F�LbxӰ��XLs@�� i�Z���^�@����T�����J<F���E���&S@@@o@Լ�@o@��@o@�@��@o@��T&MӐ���Y���	d�_� i��� 6n05� 6n��� 6nр����O�05���O����O��T&M���@o@� $� @o@�� @o@��� @o@�l� ��A���� l�B��� 4�E��� `H���� ��^���� �	d���  i�l� ԪkӴ��  6n�  6n $�  6n�B>�٬�0@ 6n4| 6n���KsӀ�Lbx��4��������4|���B>�٬�0@�����&SU0@8Ҋ� $� �� $� `� $� �H����� �H����� ������� tБ��� ����� �q��� �����&SU0@���@f���ӌ����t�������q����!����!tБ��!������! 6n���! i��!
d�_�~a�����^t���^����^�4|��^B>�٬�0@��^��&SU0@�h\� $� ��Y $� t�T��&SU0@�<R�B>�٬�0@��O4|��O����PӀ��<R��t�T���RW���!�RW���!��O���!��J��!4�E�_l�B���@o@t�@o@��@o@�� -
   $��2,0,2,0q�8 �� $��慢进��.���� $��30��$��30��4 ��        р��    р������    ����         .  .+�F� $
 Fill=�$ 9��$ 2��
�������|���� Q%`� Q%�d����� 6n� Q%`�@x}`�@x}�d�� ��  6n�@x}`���� ������ ���� � ���� � ������ ��d� -   9 d�Name$9 btnPlayPause��#��[ $��2,0,2,0q�8 �� $��播放/暂停���� -   $7 btnFast��$��2,0,2,0q�8 �� $��快进��.���� $��30��$��30��4 ��        р��    р������    ����        .+�F� $
 Fill=�$ 9��$ 2��
�������|���� � `� �8 6n� � �d�� � `���^`��� 6n���^�d����^`�@����� Q%��� Q%����@������@������ -   $7 
btnSnaphot��8 �� $��截图��.���� $��30��$��30��4 ��        р��    р������    ����        $��Center=�$��Center=�.+�F� $
 Fill=�$ 1��$ 4��
�������������ا @]� �@�4@]� �@�4���ا ���ا @]� �@]� ��� �@]� @����@������� �@]� ��� ����  H����! ?��@o@����@����O� H�@x}р���^р� H�����  H����!@f���!�%�@o@��� @o@��� @o@�� ��?��� �>р��  $� ѐ� $� S�?!6�;N�Q0@��!�Pk��0@��!@f�� -   $6 txbReplaySpeed��$��1��$��Center=��� $��x��$��Center=��� $5 
Horizontal=�$��Right=�.+��� -   : ��Name$: checkBoxPlayLocalFile���� -   ; ��Name$; txbVideoPath��$	��100���� -   $��Right=�8 �� $��打开文件��.���� $��30��$��30��4 ��        р��    р������    ����        .+�F� $
 Fill=�$ 5��$ 2��
������������	��g��0@ -1�@]� `c��@]�  $� ���^ $� ���^��� ���^�� h�l@]� @x}@]� � H�@]� ���e�c�@@@]� �(I� � $� �� -1�	��g��0@ -1�� H�� ?� H��@]�  H���L@o@�D�[@o@�� H��� $��1��$��Stretch=�$��Stretch=�
���   �$	��0,1q�