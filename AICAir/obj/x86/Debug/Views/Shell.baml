   M S B A M L   `   `   `  ����U  QMahApps.Metro, Version=1.5.0.23, Culture=neutral, PublicKeyToken=f4fb5a3c4d1e5b4fV;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.MetroMahApps.Metro.Controls  5   	   fCclr-namespace:MahApps.Metro.Controls.Dialogs;assembly=MahApps.MetroMahApps.Metro.Controls.Dialogs  5   	   O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem 5	   	   
 AICAir   AICAir.Views.Shell   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   P LPrism.Wpf, Version=6.2.0.0, Culture=neutral, PublicKeyToken=91a96d2a154366d8$prismhttp://prismlibrary.com/  5   	   JControls;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.Metro  5   	   PDialogCclr-namespace:MahApps.Metro.Controls.Dialogs;assembly=MahApps.Metro  5   	   :d2http://schemas.microsoft.com/expression/blend/2008  5   	   3System&clr-namespace:System;assembly=mscorlib  5	   	   -   5        !  Prism.Mvvm.ViewModelLocator    AutoWireViewModel  . (   "MahApps.Metro.Controls.MetroWindow   TitleAlignment$ Center=�5   !   $��0,2,0,0q�6:    � WindowStartupLocation$ CenterScreen=�5   	    � WindowState$ 	Maximized=�6.    � Icon$ /AICAir;component/20.ico��6d   8   2MahApps.Metro.Controls.Dialogs.DialogParticipation   Register 5   	   ��  � Title 5   	   �� *Title+ � 	Resources 5         WindowTitleFontSize  HeaderFontFamily  WindowButtonStyle �� 
TargetType+   %MahApps.Metro.Controls.ControlsHelper	 �� Property
 �� Value  DefaultFont  TransparentWhiteBrush  
BlackBrush  	TextBrush �� 
TargetType ��Name J� SnapsToDevicePixels J� Opacity �� Triggers P� Property P� Value �� 
TargetName �� RelativeSource �� Mode �� Path  GrayNormalBrush  
WhiteBrush��%d  &	        &	      &	 a     X�5   
   28Q�68   *�5   
   3/Segoe UI Semibold, Lucida Sans Unicode, Verdana)�6_   �� 5   
   " ��6*   .�5      �� 	 �@ ContentCharacterCasing$
 Lower=�5      �� 5      	 � ��$
 25��5      �� 5      	 � ��#
 �  5      �� 5      	 � ��$
 SemiBold"�5      �� 5      	 � ��#
 �  5      �� 5      	 � ��#
 �  5      �� 5      	 � ��#
 �  5      �� 5       	 � ��$	
 5,6q�5!      �� 5"      	 � ��$
 2q�5#      �� 5$      	 �@J�SnapsToDevicePixels
 . 5%      �� 5&      	 � ��
 5'      �� 5(      " ��6&   .R�5)      � #��z��6   .+�5*      �� $ contentPresenter��6/   #��z��5+      #��z��5,      # z 5-      ��. $	 0.5Q�5/      50       51      P� 52       �@ ContentCharacterCasing$ Normal=�53   "   . �54   "   �� $ contentPresenter��6)   
 55   %   ��  �� $ TemplatedParent=�$ Content�	 � ��56      P� 57       �@J�IsMouseOver . . �59   "   �� $ contentPresenter��6)   	 �@J�Opacity$
 1Q�5:   %   5;      P� 5<       �@J�IsMouseOver . . �5>   "   �� #
 �  5?   %   	 � ��5@      P� 5A       � �� . . �5C   "   �� #
 �  5D   %   	 � ���� 5E   "   #
 �  5F   %   	 � ��5G      5I      5K      5L      5M         RightWindowCommands 5N      +   %MahApps.Metro.Controls.WindowCommands  5O   
      ShowLastSeparator .  .@�5P      �� -   5        $��35��5P   2   $��30��6=   #���  6I    � ToolTip$ 最小化到托盘��6u   ��5Q      �� 5R       �� Orientation$ 
Horizontal=�6!   .+�5S      F� $��10��6   $��10��6*    �� Stretch$
 Fill=�66    	 IdealForegroundColorBrush#��� 	 6E   X������!�x��@�e�x��@�e�E���!�E���!�x����C�<R�@�el't���!l'tр�C�<R J� RenderTransformOrigin$ 0.5, 0.5&�6�   z�5T      V� 5U   "   .~�5V   &   ��  �� Angle$	 180Q�66   5W   #   5Y      5Z      5\      5]      .��5_      �  � RowDefinitions 5`   
   �� 5a      $��* �6   .+�5c   
   �� !  Prism.Regions.RegionManager   
RegionName$ MainRegionRegion��6   $��0��6K   5d      5e      