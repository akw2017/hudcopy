   M S B A M L   `   `   `  ���� U  QMahApps.Metro, Version=1.5.0.23, Culture=neutral, PublicKeyToken=f4fb5a3c4d1e5b4fV;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.MetroMahApps.Metro.Controls  fCclr-namespace:MahApps.Metro.Controls.Dialogs;assembly=MahApps.MetroMahApps.Metro.Controls.Dialogs  O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem 
 AICAir   AICAir.Views.Shell   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     P LPrism.Wpf, Version=6.2.0.0, Culture=neutral, PublicKeyToken=91a96d2a154366d8$prismhttp://prismlibrary.com/  JControls;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.Metro  PDialogCclr-namespace:MahApps.Metro.Controls.Dialogs;assembly=MahApps.Metro  :d2http://schemas.microsoft.com/expression/blend/2008  3System&clr-namespace:System;assembly=mscorlib  -   !  Prism.Mvvm.ViewModelLocator    AutoWireViewModel  . (   "MahApps.Metro.Controls.MetroWindow   TitleAlignment$ Center=�$��0,2,0,0q� � WindowStartupLocation$ CenterScreen=� � WindowState$ 	Maximized=� � Icon$ /AICAir;component/20.ico��8   2MahApps.Metro.Controls.Dialogs.DialogParticipation   Register ��  � Title �� *Title+ � 	Resources    WindowTitleFontSize  HeaderFontFamily  WindowButtonStyle �� 
TargetType+   %MahApps.Metro.Controls.ControlsHelper	 �� Property
 �� Value  DefaultFont  TransparentWhiteBrush  
BlackBrush  	TextBrush �� 
TargetType ��Name J� SnapsToDevicePixels J� Opacity �� Triggers P� Property P� Value �� 
TargetName �� RelativeSource �� Mode �� Path  GrayNormalBrush  
WhiteBrush��%D  &	        &	      &	 E     X�28Q�*�3/Segoe UI Semibold, Lucida Sans Unicode, Verdana)��� " ��.��� 	 �@ ContentCharacterCasing$
 Lower=��� 	 � ��$
 25���� 	 � ��#
 �  �� 	 � ��$
 SemiBold"��� 	 � ��#
 �  �� 	 � ��#
 �  �� 	 � ��#
 �  �� 	 � ��$	
 5,6q��� 	 � ��$
 2q��� 	 �@J�SnapsToDevicePixels
 . �� 	 � ��
 �� " ��.R�� #��z��.+��� $ contentPresenter��#��z��#��z��# z ��. $	 0.5Q� P�  �@ ContentCharacterCasing$ Normal=�. ��� $ contentPresenter��
 ��  �� $ TemplatedParent=�$ Content�	 � ��P�  �@J�IsMouseOver . . ��� $ contentPresenter��	 �@J�Opacity$
 1Q�P�  �@J�IsMouseOver . . ��� #
 �  	 � ��P�  � �� . . ��� #
 �  	 � ���� #
 �  	 � ��   RightWindowCommands +   %MahApps.Metro.Controls.WindowCommands     ShowLastSeparator .  .@��� -   $��35��$��30��#���   � ToolTip$ 最小化到托盘������  �� Orientation$ 
Horizontal=�.+�F� $��10��$��10�� �� Stretch$
 Fill=� 	 IdealForegroundColorBrush#��� 	 X������!�x��@�e�x��@�e�E���!�E���!�x����C�<R�@�el't���!l'tр�C�<R J� RenderTransformOrigin$ 0.5, 0.5&�z�V� .~���  �� Angle$	 180Q�.���  � RowDefinitions �� $��* �.+��� !  Prism.Regions.RegionManager   
RegionName$ MainRegionRegion��$��0��