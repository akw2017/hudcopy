   M S B A M L   `   `   `  ���� G  CAIC.HomePage, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null7 clr-namespace:AIC.HomePage.ViewsAIC.HomePage.Views  U QMahApps.Metro, Version=1.5.0.23, Culture=neutral, PublicKeyToken=f4fb5a3c4d1e5b4fV;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.MetroMahApps.Metro.Controls =#clr-namespace:AIC.HomePage.ControlsAIC.HomePage.Controls  %    AIC.HomePage.Views.ShowAlarmWin   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     2local clr-namespace:AIC.HomePage.Views     PControls;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.Metro     1c#clr-namespace:AIC.HomePage.Controls     P LPrism.Wpf, Version=6.2.0.0, Culture=neutral, PublicKeyToken=91a96d2a154366d8$prismhttp://prismlibrary.com/  !  Prism.Mvvm.ViewModelLocator    AutoWireViewModel  . (  "MahApps.Metro.Controls.MetroWindow   ShowMinButton .     ShowMaxRestoreButton .  $��2q�   AccentColorBrush#���    � Title$ 报警界面��$	��820��$	��800�� � WindowStartupLocation$ CenterScreen=� � 
ResizeMode$ NoResize=� � Icon$/ )/AIC.Resources;component/Images/water.png�� � Topmost .  � 	Resources   ColumnHeaderGripperStyle	 �� 
TargetType
 �� Property �� Value �� 
TargetType �� Padding  DataGridColumnHeaderStyle=  7System.Windows.Controls.Primitives.DataGridColumnHeader � ColumnDefinitions ��Name ��Name J� SnapsToDevicePixels n�Name �� Triggers P� Property P� Value �� 
TargetName  DataGridRowStyle)  #System.Windows.Controls.DataGridRow?  9System.Windows.Controls.Primitives.SelectiveScrollingGrid � RowDefinitions?  9System.Windows.Controls.Primitives.DataGridCellsPresenter   
ItemsPanelA  ;System.Windows.Controls.Primitives.DataGridDetailsPresenter   DetailsVisibility$   SelectiveScrollingOrientation;  5System.Windows.Controls.SelectiveScrollingOrientation   Vertical �� ConverterParameter&	   System.Windows.Controls.DataGrid# 	  RowDetailsScrollingConverter �� 	Converter �� RelativeSource  �� AncestorType:
  4System.Windows.Controls.Primitives.DataGridRowHeader7  1System.Windows.Controls.DataGridHeadersVisibility
!   Row!" 	  HeadersVisibilityConverter# �� Triggers$ �� Color  DataGridCellStyle*  $System.Windows.Controls.DataGridCell% �� ContentStringFormat& �� ContentStringFormat' ]� 
Conditions( �� Property) �� Value* ^� 
Conditions+ �� Binding  ButtonStyle��%�  &	       &	 �     7  7  &	 �    (�� �    *DataGrid.FocusBorderBrushKey+)&	 �    &	 �    �� "	 n�.��� 
 � ��$ 2���� 
 � ��
 ���� �� 
 �@�Cursor$ SizeWE���� 
 � �� �� " n�.R��� #��z��# z���� "	  .��� 
 � ��$ Center=��� 
 �@J�SnapsToDevicePixels . �� 
 � ��$ 0���� 
 � ��$ 25���� 
 � �� �	#FF85CBEB�� 
 �@�Cursor$
 Hand���� 
 � ��$ 5q��� 
 � ��$ 0,0,0,0q��� 
 � �� �	#7F243859�� 
 � �� �� "  .R��  �� $��* ��� $
��Auto �.+��� $ BackgroundBorder��#��z��$��2��#��z��#��z���� $ HeaderContent��#��z��#��z��#��z��#��z��#��z����. # z n� $ PART_LeftHeaderGripper��$
��Left=�$
��Left=�
������ 8��  n� $ PART_RightHeaderGripper��$��Right=�$��Right=�8�� $��1�� P�  �@J�IsMouseOver . . ��� $ BackgroundBorder��
 � �� �	#7F486391P�  �@ DisplayIndex$ 0��. ��� $ PART_LeftHeaderGripper��
 � y�$ 	Collapsed=�P�  � {� .  . ��� # � ��
 � ���� "	  .��� 
 � �� �	#FF85CBEB�� 
 � �� �	#7F243859�� 
 � ��$ 25���� 
 � ��$	 0,2q��� 
 �@J�SnapsToDevicePixels . �� 
 �@=�ErrorTemplate Z� �� 
 �@ ValidationErrorTemplate �� .R��� 
���  ��$��2,0,0,0q�$��!��$��Center=��� 
 � �� �� "  .R��� $ 
DGR_Border��#��z��#��z��#��z�� . ._�   �� $
��Auto ��� $��* � �� $��* ��� $
��Auto �.+�  $��1��#��z # z   $��1��$��1��#y�z  �� *AreRowDetailsFrozen+# Z # Z  �� "  	 
  $��2��$ Vertical=�y��� *HeadersVisibility+# Z! # Z"  �� "  	 # P�  �@J�IsMouseOver . . ��� 
 � �� �	#7F486391P�  �@ 
IsSelected . . ��� 
 � �� �	#FF20437E�� $$ 	#FF000000���� "	  .��� 
 � ��
 ���� �� 
 � ��
 ���� �� 
 � ��$ Stretch=��� 
 � ��$ Center=��� 
 �@J�SnapsToDevicePixels . �� 
 � ��$ 0q��� 
 � ��$ 0q��� 
 � �� �� "  .R��� #��z��#��z��#��z��# z ._��� #��z��#��z��#& z% #��z��# z #��z��#��z��# ]� ' �� ( �@ 
IsSelected) . �� ( �@��IsSelectionActive) .  ./��� 
 � �� �	#FF85CBEB^� * �� + �� *
IsSelected+ �� *Self+) True�� + �� *SelectionUnit+ �� "  	 ) Cell.0��� 
 � ��
 �����^� * �� + �� *
IsSelected+ �� *Self+) True�� + �� *
IsSelected+ �� "   	) False�� + �� *SelectionUnit+ �� "  	 ) CellOrRowHeader.0��� 
 � ��
 �����^� * �� + �� *IsMouseOver+ �� *Self+) True�� + �� *SelectionUnit+ �� "  	 ) Cell.0��� 
 � ��
 �����^� * �� + �� *IsMouseOver+ �� *Self+) True�� + �� *
IsSelected+ �� "   	) False�� + �� *SelectionUnit+ �� "  	 ) CellOrRowHeader.0��� 
 � ��
 �����P�  � {� .  . ��� 
 � ��
 �����]� ' �� ( � {�) .  �� ( �@ 
IsSelected) . ./��� 
 � ��
 ������� "	 ��.��� 
 � ��
 ���� �� 
 � �� �	#FF87CDEE�� 
 � ��$ Center=��� 
 � ��$ Center=��� 
 � ��$ 1q��� 
 � �� �� " ��.R�� #��z��.+��� #��z��#��z����. # z #��z�� P�  �@J�IsMouseOver . . ��� 
 � �� �	#7F243859P�  � {� .  . ��� 
 � �� �#ADADADP�  � �� . . ��� 
 � �� �	#FF20437E.���   AccentColorBrush2#���  .+�� $��20q� �� $��* ��� $��20 ��� $	��300 ��� $��35 �.+�	  -   , 	  GridLinesVisibility$, 
Horizontal=�- 	  AutoGenerateColumns- .  . 	  CanUserResizeRows. . / 	  CanUserAddRows/ .  
������ $��0q�0 	  RowHeaderWidth$0 0Q�1 	  ColumnHeaderStyle#1 [ 2 	  RowStyle#2 [ 3 	  	CellStyle#3 [ ���� *VibrationHistories+4 �� SelectedItem4 �� *SelectedVibrationHistory+��Z� 5 	  Columns5 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn6   Header$6 总厂��7   
IsReadOnly7 . 1  +System.Windows.Controls.DataGridBoundColumn8   Binding8 �� *GroupCOName+  $6 分厂��7 . 8 �� *CorporationName+  $6 车间��7 . 8 �� *WorkShopName+  $6 设备名称��7 . 8 �� *	DevName+  $6 设备编号��7 . 8 �� *DevSN+  $6 测点名称��7 . 8 �� *Name+  $6 测点编号��7 . 8 �� *MSSN+�� $��1��$��Stretch=�$��Center=�$��	10,0,10,0q�� $��2�� �� $��25 ��� $��* �.+�� $��0��$��3,3,3,0q�.+��� 9 �� Orientation$9 
Horizontal=�$
��Left=�.+��� ���	#FF87CDEE$��5,0,0,0q�$��Center=��� .�数据长度: �� ��� *HistoryCount+�� -   $��1��$��截图��#��[ $��Right=�$��Center=�$��0,0,5,0q�: � ToolTip: �� $��截图��� -   ; �Name$; 	gridChart��$��2��$��3,0,3,3q�F� -   < F�Name$< TopLeftPath��$��2��"����k"��pѰ��pѰ��H	$
��Left=�$	��Top=�$��15��$��15��= �� Stretch$
= Fill=����	#FF87CDEE$��3��F� -   $< TopRightPath��$��2��"�������kч	��kч	�e�	$��Right=�$	��Top=�$��15��$��15��$
= Fill=����	#FF87CDEE$��3��F� -   $< BottomLeftPath��$��2��'���P�Ed7�@�@��E�� ����� 	$
��Left=�$��Bottom=�$��15��$��15��$
= Fill=����	#FF87CDEE$��3��F� -   $< BottomRightPath��$��2��"����h�흤 �h���.р�7��.	$��Right=�$��Bottom=�$��15��$��15��$
= Fill=����	#FF87CDEE$��3���� $��刷新��$��3��$��14(�$��50��$��5q�$��Center=�#��[ ���� *RefreshHistoryCommand+