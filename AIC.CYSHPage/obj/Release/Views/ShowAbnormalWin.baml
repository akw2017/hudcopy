   M S B A M L   `   `   `  ���� G  CAIC.HomePage, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null7 clr-namespace:AIC.HomePage.ViewsAIC.HomePage.Views  U QMahApps.Metro, Version=1.5.0.23, Culture=neutral, PublicKeyToken=f4fb5a3c4d1e5b4fV;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.MetroMahApps.Metro.Controls =#clr-namespace:AIC.HomePage.ControlsAIC.HomePage.Controls  (    "AIC.HomePage.Views.ShowAbnormalWin   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     2local clr-namespace:AIC.HomePage.Views     PControls;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.Metro     1c#clr-namespace:AIC.HomePage.Controls     (  "MahApps.Metro.Controls.MetroWindow    ShowMinButton  .     ShowMaxRestoreButton .  $��2q�   AccentColorBrush#���    � Title$ 异常界面��$	��600��$	��800�� � WindowStartupLocation$ CenterScreen=� � 
ResizeMode$ NoResize=� � Icon$/ )/AIC.Resources;component/Images/water.png�� � Topmost .  � 	Resources   ExceptionListBoxItemStyle �� 
TargetType	 �� Property
 �� Value �� RelativeSource �� AncestorType �� 
TargetType ��Name �� CornerRadius �� Padding J� SnapsToDevicePixels �� Triggers P� Property P� Value �� 
TargetName��%�  &	       �� " ��.��� 	 � ��
 �� *HorizontalContentAlignment+ �� " ���� 	 � ��
 �� *VerticalContentAlignment+ �� " ���� 	 � ��$
 2,0,0,0q��� 	 � ��$
 2q��� 	 � ��
 �� " ��.R��� $ Bd��$ 5��#��z��#��z��#��z��# z�� . ._��� #��z��# z #��z�� P�  �@J�IsMouseOver . . ��� 	 � ��
 �	#7F486391P�  �@��
IsSelected . . ��� $ Bd��	 � ��
 �	#FF20437EP�  � {� .  . ��� #
 � ��	 � ��.���   AccentColorBrush2#���  .+�� -   $��mainGrid��$��20q� � ColumnDefinitions �� $��* ��� $��* ��� $��* ��� $��* � � RowDefinitions �� $��* ��� $��* ��� $
��Auto �.+��� -   $��0��$��2��#��[ ���� *ExceptionModels+��Z� ��Z� ���� .R���  �� Orientation$ 
Horizontal=�.+�F� $��25��$��25��$��5,0,0,0q� �� Stretch$
 Fill=�
��� ���������}p�M���	�MӬt� �J1�t� �f`�	����ȳ� � n5�e� Rc�e� $�ȳ� Ѡ�}4�Ӹ6��f�6�XK1��}p�M�do����@�% <��\b�;�ќ;�����do������C�x���%)�x���<������Ӕ��z��%)���C��$�^�'t�z�'t����'t<�$�^�x���C�x��� $��5,0,0,0q�$��Center=����� *Target+,� -   $��	docViewer��$��0��$��1��$��2��$��3��$ ,� HorizontalScrollBarVisibility$ Disabled=�" ,� VerticalScrollBarVisibility$
 Auto=��� -    ��Name$ btnClear��$��2��$��1��  AccentedSquareButtonStyle#���  $	��100������ $ 
Horizontal=�.+��� $
��None=�$0��*/AIC.Resources;component/Images/accept.png����  	 clear#���  $��Center=��� -   $ btnOK��$��2��$��2��#���  $	��100�� �� 	IsDefault . ���� $ 
Horizontal=�.+��� $
��None=�$0��*/AIC.Resources;component/Images/cancel.png����  	 strOK#���  $��Center=�