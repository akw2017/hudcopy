   M S B A M L   `   `   `  ����M  IAIC.OnLineDataPage, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullM+clr-namespace:AIC.OnLineDataPage.ConvertersAIC.OnLineDataPage.Converters  5       C ?AIC.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5(clr-namespace:AIC.Core;assembly=AIC.CoreAIC.Core 5       3    -AIC.OnLineDataPage.Views.AlarmGradeFilterView   5       R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   #   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5       F BItemsFilter, Version=1.1.0.1, Culture=neutral, PublicKeyToken=nullGbsFilter8http://schemas.bolapansoft.com/xaml/Controls/ItemsFilter  5       Dmc;http://schemas.openxmlformats.org/markup-compatibility/2006  5       :d2http://schemas.microsoft.com/expression/blend/2008  5       <
converters+clr-namespace:AIC.OnLineDataPage.Converters  5       3core(clr-namespace:AIC.Core;assembly=AIC.Core  5       -   5          � 	Resources  5         CategoryItemsPanelTemplate K� IsItemsHost �� Orientation'  !AIC.Core.EnumDescriptionConverter  enumDescriptionConverter>   8AIC.OnLineDataPage.Converters.AlarmGradeToImageConverter  alarmGradeToImageConverter  CheckBoxStyle �� 
TargetType �� Property �� Value �� 
TargetType �� 
Background J� SnapsToDevicePixels	 �� Bullet
 ��Name  AccentColorBrush �� RadiusX �� RadiusY F�Name �� Stretch �� Triggers P� Property P� Value �� 
TargetName��%  &	        &	 E     &	 X     &	 k     �� 5   
   .R�5      ��  . $ 
Horizontal=�6,   5        5   
   6(     5   
   60   �� 5   
   " ��6&   .�5      ��  � ��$ 1q�60   �� 5       � ��$ Center=�69   �� 5       � �� 5      �� 5      " ��6&   .R�5      �� 
 ����  . 	 5      � 5   "   $��24��6'   $��24��63   .+�5   &   �� $
 	PART_Rect��60   #���  6C   $��3��6o   $ 5��6�   $ 5��6�   F� 5   &   $
 path��6+   $��18��69   $��18��6D   $
 Fill=�6P   #��z��6_   F�����ej�Z�����x��,E=R�ܿe������Tn5�$�!��C��ej�Z�6�   5   #   ._�5!      �� #��z��6/   #��z��6r   ��. # z 6�   #��z��6�   5"       5#      P� 5$       � �� . . �5%   "   ��  � ��$ 4,0,0,0q�6<   5&      P� 5'       � {� .  . �5(   "   �� # � ��6?    � ��5)      P� 5*       �@d�	IsChecked$ FalseY�6;   . �5+   "   �� $
 path��6?    � y�$ Hidden=�6Q   5,      5.      50      51      52      ��53      �� 54   
   :  4BolapanControl.ItemsFilter.View.MultiValueFilterView"  6   .�55      ��  � �� 56      �� 57      "  6&   .R�58      � .+�59      � �  �BolapanControl.ItemsFilter.View.FilterViewBase`1[[BolapanControl.ItemsFilter.Model.IMultiValueFilter, ItemsFilter, Version=1.1.0.1, Culture=neutral, PublicKeyToken=null]]   Model � DataContext# z 6#   $
��Auto=�6I    � ColumnDefinitions 5:   "   �� 5;   &   $
��Auto �67   �� 5<   &   $��* �67   .+�5>   "   �� $��0��6,   $��报警级别：��6<   $��Center=�6I   $��2,0,2,0q�6d   �� 5?   "    ��Name$ 
PART_Items��6*   $��1��5@   *   #��[  5A   *    �� SelectionMode$ Multiple=�5C   *   ��5B   *   �� *AvailableValues+��5C   C   Z� ��6Y   Z� ��5D   &   �� 5E   *   " ��60   .�5F   .   ��  � �� 6K   �� *
Background+ �� RelativeSource �� *FindAncestor+ �� AncestorLevel$ 1�� �� AncestorType" ���� 5G   .    � �� 5H   2   �� 5I   6   " ��6F   .R�5J   :   �� $��5,2,5,2q�6E   $ 
Horizontal=�6V   .+�5K   >   A�  A�Name$ ellipse��6F   ���	#FF85CBEB$��Center=�6h   $��24��6�   $��24��6�   �� 5L   >   #���  6H   $��Center=�6�   ��6x   ��  �� 	Converter# [ 6�   �� 5M   >   $��Center=�6G   #��[ 6�    d� 	IsChecked 6b   �� *
IsSelected+ �� Mode$ TwoWay=� �� *TemplatedParent+6�   5N   ;    5O   :   �� 5P   >    �� Value$ Invalid��6e     �� Binding  6J   �� ! �� Path$! .�.a�5Q   B   �� $ ellipse��6I    � ��
 �����6n   5R   ?   �� 5S   >   $ 	HighAlarm��6e     6J   �� $! .�.a�5T   B   �� $ ellipse��6I    � ��
 � ���6n   5U   ?   �� 5V   >   $ LowAlarm��6e     6J   �� $! .�.a�5W   B   �� $ ellipse��6I    � ��
 � ���6n   5X   ?   �� 5Y   >   $ 
HighDanger��6e     6J   �� $! .�.a�5Z   B   �� $ ellipse��6I    � ��
 �  ��6n   5[   ?   �� 5\   >   $ 	LowDanger��6e     6J   �� $! .�.a�5]   B   �� $ ellipse��6I    � ��
 �  ��6n   5^   ?   5`   7   5b   /   5c   +   5e   #   5f      5g      5h      5j      5k      5m      