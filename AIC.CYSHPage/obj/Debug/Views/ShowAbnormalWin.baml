   M S B A M L   `   `   `  ����G  CAIC.HomePage, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null7 clr-namespace:AIC.HomePage.ViewsAIC.HomePage.Views  5   	   U QMahApps.Metro, Version=1.5.0.23, Culture=neutral, PublicKeyToken=f4fb5a3c4d1e5b4fV;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.MetroMahApps.Metro.Controls 5   	   =#clr-namespace:AIC.HomePage.ControlsAIC.HomePage.Controls  5   	   (    "AIC.HomePage.Views.ShowAbnormalWin   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   2local clr-namespace:AIC.HomePage.Views     5   	   PControls;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.Metro     5   	   1c#clr-namespace:AIC.HomePage.Controls     5   	   (  "MahApps.Metro.Controls.MetroWindow    ShowMinButton  .     ShowMaxRestoreButton .  $��2q�5   	      AccentColorBrush#���   6    � Title$ 异常界面��5   	   $	��600��6   $	��800��6#    � WindowStartupLocation$ CenterScreen=�6/    � 
ResizeMode$ NoResize=�6T    � Icon$/ )/AIC.Resources;component/Images/water.png��6j    � Topmost .  � 	Resources 5        ExceptionListBoxItemStyle �� 
TargetType	 �� Property
 �� Value �� RelativeSource �� AncestorType �� 
TargetType ��Name �� CornerRadius �� Padding J� SnapsToDevicePixels �� Triggers P� Property P� Value �� 
TargetName��%/  &	       �� 5   
   " ��62   .�5      �� 	 � ��
 6;   �� *HorizontalContentAlignment+ �� " ���� 5      	 � ��
 69   �� *VerticalContentAlignment+ �� " ���� 5      	 � ��$
 2,0,0,0q�6(   �� 5      	 � ��$
 2q�6'   �� 5      	 � ��
 5      �� 5      " ��6&   .R�5      �� $ Bd��6!   $ 5��6-   #��z��6>   #��z��6j   #��z��6�   # z��6�    . ._�5      �� #��z��6/   # z 6r   #��z��6�   5       5      P� 5       �@J�IsMouseOver . . �5   "   �� 	 � ��
 �	#7F4863916?   5      P� 5       �@��
IsSelected . . �5   "   �� $ Bd��6?   	 � ��
 �	#FF20437E6O   5      P� 5        � {� .  . �5!   "   �� #
 � ��6?   	 � ��5"      5$      5&      5'      5(      .��5*      �   AccentColorBrush2#���  6   .+�5+   
   � -   5        $��mainGrid��5+      $��20q�6    � ColumnDefinitions 5,      �� 5-      $��* �6#   �� 5.      $��* �6#   �� 5/      $��* �6#   �� 50      $��* �6#    � RowDefinitions 52      �� 53      $��* �6    �� 54      $��* �6    �� 55      $
��Auto �6    .+�57      �� -   5        $��0��57      $��2��6#   #��[ 6\   ��64   �� *ExceptionModels+��6�   Z� ��6�   Z� ��58      �� 59      .R�5:      ��  �� Orientation$ 
Horizontal=�6%   .+�5;      F� $��25��6#   $��25��6.   $��5,0,0,0q�6:    �� Stretch$
 Fill=�6K   
��� ���������}p�M���	�MӬt� �J1�t� �f`�	����ȳ� � n5�e� Rc�e� $�ȳ� Ѡ�}4�Ӹ6��f�6�XK1��}p�M�do����@�% <��\b�;�ќ;�����do������C�x���%)�x���<������Ӕ��z��%)���C��$�^�'t�z�'t����'t<�$�^�x���C�x�6h   �� 5<      $��5,0,0,0q�6@   $��Center=�6Q   ��6(   �� *Target+6Q   5=      5>      5@      ,� 5A      -   5        $��	docViewer��5A   '   $��0��68   $��1��6E   $��2��6U   $��3��6f   $ ,� HorizontalScrollBarVisibility$ Disabled=�6z   " ,� VerticalScrollBarVisibility$
 Auto=�6�   �� 5C      -   5         ��Name$ btnClear��5C   2   $��2��6   $��1��6"     AccentedSquareButtonStyle#���  6D   $	��100��6y   ��5D      �� 5E      $ 
Horizontal=�6!   .+�5F      �� $
��None=�6    $0��*/AIC.Resources;component/Images/accept.png��6/   �� 5G       	 clear#���  6$   $��Center=�6C   5H      5J      �� 5K      -   5        $ btnOK��5K   2   $��2��6   $��2��6"   #���  6A   $	��100��6v    �� 	IsDefault . ��5L      �� 5M      $ 
Horizontal=�6!   .+�5N      �� $
��None=�6    $0��*/AIC.Resources;component/Images/cancel.png��6/   �� 5O       	 strOK#���  6$   $��Center=�6C   5P      5R      5S      5X      5Y      