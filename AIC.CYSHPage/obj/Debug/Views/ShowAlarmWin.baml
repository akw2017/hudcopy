   M S B A M L   `   `   `  ����G  CAIC.HomePage, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null7 clr-namespace:AIC.HomePage.ViewsAIC.HomePage.Views  5   	   U QMahApps.Metro, Version=1.5.0.23, Culture=neutral, PublicKeyToken=f4fb5a3c4d1e5b4fV;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.MetroMahApps.Metro.Controls 5   	   =#clr-namespace:AIC.HomePage.ControlsAIC.HomePage.Controls  5   	   %    AIC.HomePage.Views.ShowAlarmWin   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   2local clr-namespace:AIC.HomePage.Views     5   	   PControls;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.Metro     5   	   1c#clr-namespace:AIC.HomePage.Controls     5   	   P LPrism.Wpf, Version=6.2.0.0, Culture=neutral, PublicKeyToken=91a96d2a154366d8$prismhttp://prismlibrary.com/  5	   	   !  Prism.Mvvm.ViewModelLocator    AutoWireViewModel  . (  "MahApps.Metro.Controls.MetroWindow   ShowMinButton .     ShowMaxRestoreButton .  $��2q�5   	      AccentColorBrush#���   6    � Title$ 报警界面��5   	   $	��820��6   $	��800��6#    � WindowStartupLocation$ CenterScreen=�6/    � 
ResizeMode$ NoResize=�6T    � Icon$/ )/AIC.Resources;component/Images/water.png��6j    � Topmost .  � 	Resources 5        ColumnHeaderGripperStyle	 �� 
TargetType
 �� Property �� Value �� 
TargetType �� Padding  DataGridColumnHeaderStyle=  7System.Windows.Controls.Primitives.DataGridColumnHeader � ColumnDefinitions ��Name ��Name J� SnapsToDevicePixels n�Name �� Triggers P� Property P� Value �� 
TargetName  DataGridRowStyle)  #System.Windows.Controls.DataGridRow?  9System.Windows.Controls.Primitives.SelectiveScrollingGrid � RowDefinitions?  9System.Windows.Controls.Primitives.DataGridCellsPresenter   
ItemsPanelA  ;System.Windows.Controls.Primitives.DataGridDetailsPresenter   DetailsVisibility$   SelectiveScrollingOrientation;  5System.Windows.Controls.SelectiveScrollingOrientation   Vertical �� ConverterParameter&	   System.Windows.Controls.DataGrid# 	  RowDetailsScrollingConverter �� 	Converter �� RelativeSource  �� AncestorType:
  4System.Windows.Controls.Primitives.DataGridRowHeader7  1System.Windows.Controls.DataGridHeadersVisibility
!   Row!" 	  HeadersVisibilityConverter# �� Triggers$ �� Color  DataGridCellStyle*  $System.Windows.Controls.DataGridCell% �� ContentStringFormat& �� ContentStringFormat' ]� 
Conditions( �� Property) �� Value* ^� 
Conditions+ �� Binding  ButtonStyle��%1  &	       &	     7  7  &	 �    (�� �    5�      *DataGrid.FocusBorderBrushKey+)&	 �    &	     �� 5   
   "	 n�61   .�5      �� 
 � ��$ 2��6&   �� 5      
 � ��
 ���� 6+   �� 5      
 �@�Cursor$ SizeWE��6'   �� 5      
 � �� 5      �� 5      " n�6&   .R�5      �� #��z��6!   # z��6K   5      5      5      �� 5   
   "	  62   .�5      �� 
 � ��$ Center=�69   �� 5      
 �@J�SnapsToDevicePixels . 64   �� 5      
 � ��$ 0��6*   �� 5       
 � ��$ 25��6+   �� 5!      
 � �� �	#FF85CBEB6+   �� 5"      
 �@�Cursor$
 Hand��6'   �� 5#      
 � ��$ 5q�6)   �� 5$      
 � ��$ 0,0,0,0q�6(   �� 5%      
 � �� �	#7F2438596+   �� 5&      
 � �� 5'      �� 5(      "  6&   .R�5)      �  5*      �� 5+   "   $��* �63   �� 5,   "   $
��Auto �63   .+�5.      �� $ BackgroundBorder��6%   #��z��5/   	   $��2��50   	   #��z��51   	   #��z��52   	   �� 54      $ HeaderContent��6/   #��z��55   	   #��z��56   	   #��z��57   	   #��z��58   	   #��z��59   	   ��. # z 5;   	   n� 5=      $ PART_LeftHeaderGripper��6$   $
��Left=�5>   	   $
��Left=�5?   	   
������ 8��  5A   	   n� 5C      $ PART_RightHeaderGripper��6$   $��Right=�5D   	   $��Right=�5E   	   8�� $��1��5G   	   5H       5I      P� 5J       �@J�IsMouseOver . . �5K   "   �� $ BackgroundBorder��6)   
 � �� �	#7F4863916]   5L      P� 5M       �@ DisplayIndex$ 0��6>   . �5N   "   �� $ PART_LeftHeaderGripper��6Q   
 � y�$ 	Collapsed=�6?   6Q   5O      P� 5P       � {� .  . �5Q   "   �� # � ��6?   
 � ��5R      5T      5V      5W      �� 5X   
   "	  6)   .�5Y      �� 
 � �� �	#FF85CBEB6+   �� 5Z      
 � �� �	#7F2438596+   �� 5[      
 � ��$ 25��6*   �� 5\      
 � ��$	 0,2q�6'   �� 5]      
 �@J�SnapsToDevicePixels . 64   �� 5^      
 �@=�ErrorTemplate 69   Z� �� 5_      
 �@ ValidationErrorTemplate 5`      �� 5a      .R�5b      �� 
���  ��$��2,0,0,0q�65   $��!��6F   $��Center=�6O   5c      5e      �� 5f      
 � �� 5g      �� 5h      "  6&   .R�5i      �� $ 
DGR_Border��6!   #��z��65   #��z��6a   #��z��6�    . ._�5j         5k   "   �� 5l   &   $
��Auto �67   �� 5m   &   $��* �67    5o   "   �� 5p   &   $��* �64   �� 5q   &   $
��Auto �64   .+�5s   "     $��1��69   #��z 6I   # z 6s     5t   "   $��1��6;   $��1��6K   #y�z 6o   6X   �� *AreRowDetailsFrozen+# Z # Z  �� "  	 6o  
  5u   "   $��2��64   $ Vertical=�6E   y�6�   �� *HeadersVisibility+# Z! # Z"  �� "  	 5v      5w      5x      5z      # 5{      P� 5|       �@J�IsMouseOver . . �5}      �� 
 � �� �	#7F48639163   5~      P� 5       �@ 
IsSelected . . �5�      �� 
 � �� �	#FF20437E63   5�      5�      �� 5�   
   $$ 	#FF000000��6J   �� 5�   
   "	  6*   .�5�      �� 
 � ��
 ���� 6+   �� 5�      
 � ��
 ���� 6,   �� 5�      
 � ��$ Stretch=�6;   �� 5�      
 � ��$ Center=�69   �� 5�      
 �@J�SnapsToDevicePixels . 64   �� 5�      
 � ��$ 0q�6(   �� 5�      
 � ��$ 0q�6'   �� 5�      
 � �� 5�      �� 5�      "  6&   .R�5�      �� #��z��6!   #��z��5�      #��z��5�      # z 5�      ._�5�      �� #��z��6/   #��z��5�   	   #& z% 5�   	   #��z��5�   	   # z 5�   	   #��z��5�   	   #��z��5�   	   5�      5�      5�      # 5�      ]� 5�      ' 5�      �� 5�      ( �@ 
IsSelected) . 6:   �� 5�      ( �@��IsSelectionActive) .  6J   ./�5�      �� 
 � �� �	#FF85CBEB63   5�      ^� 5�      * 5�      �� 5�      + 6$   �� *
IsSelected+ �� *Self+) True6i   �� 5�      + 6$   �� *SelectionUnit+ �� "  	 ) Cell6�   .0�5�      �� 
 � ��
 �����63   5�      ^� 5�      * 5�      �� 5�      + 6$   �� *
IsSelected+ �� *Self+) True6i   �� 5�      + 6$   �� *
IsSelected+ �� "   	) False6�   �� 5�      + 6$   �� *SelectionUnit+ �� "  	 ) CellOrRowHeader6�   .0�5�      �� 
 � ��
 �����63   5�      ^� 5�      * 5�      �� 5�      + 6$   �� *IsMouseOver+ �� *Self+) True6j   �� 5�      + 6$   �� *SelectionUnit+ �� "  	 ) Cell6�   .0�5�      �� 
 � ��
 �����63   5�      ^� 5�      * 5�      �� 5�      + 6$   �� *IsMouseOver+ �� *Self+) True6j   �� 5�      + 6$   �� *
IsSelected+ �� "   	) False6�   �� 5�      + 6$   �� *SelectionUnit+ �� "  	 ) CellOrRowHeader6�   .0�5�      �� 
 � ��
 �����63   5�      P� 5�       � {� .  . �5�      �� 
 � ��
 �����63   5�      ]� 5�      ' 5�      �� 5�      ( � {�) .  69   �� 5�      ( �@ 
IsSelected) . 6:   ./�5�      �� 
 � ��
 �����63   5�      5�      �� 5�   
   "	 ��6$   .�5�      �� 
 � ��
 ���� 6+   �� 5�      
 � �� �	#FF87CDEE6+   �� 5�      
 � ��$ Center=�6;   �� 5�      
 � ��$ Center=�69   �� 5�      
 � ��$ 1q�6(   �� 5�      
 � �� 5�      �� 5�      " ��6&   .R�5�      � #��z��6   .+�5�      �� #��z��6/   #��z��6r   ��. # z 6�   #��z��6�   5�       5�      P� 5�       �@J�IsMouseOver . . �5�   "   �� 
 � �� �	#7F2438596?   5�      P� 5�       � {� .  . �5�   "   �� 
 � �� �#ADADAD6?   5�      P� 5�       � �� . . �5�   "   �� 
 � �� �	#FF20437E6?   5�      5�      5�      5�      5�      .��5�      �   AccentColorBrush2#���  6   .+�5�   
   � $��20q�6    5�      �� 5�      $��* �6    �� 5�      $��20 �6    �� 5�      $	��300 �6    �� 5�      $��35 �6    .+�5�      	  -   5        , 	  GridLinesVisibility$, 
Horizontal=�5�      - 	  AutoGenerateColumns- .  . 	  CanUserResizeRows. . / 	  CanUserAddRows/ .  
������ $��0q�5�   ,   0 	  RowHeaderWidth$0 0Q�6W   1 	  ColumnHeaderStyle#1 [ 5�      2 	  RowStyle#2 [ 6R   3 	  	CellStyle#3 [ 6   ��5�   8   �� *VibrationHistories+4 �� SelectedItem4 6�   �� *SelectedVibrationHistory+��5�   @   Z� 5 	  Columns5 5�      0  *System.Windows.Controls.DataGridTextColumn  5�      ,  &System.Windows.Controls.DataGridColumn6   Header$6 总厂��6)   7   
IsReadOnly7 . 1  +System.Windows.Controls.DataGridBoundColumn8   Binding8 66   �� *GroupCOName+6V     5�      $6 分厂��6)   7 . 8 66   �� *CorporationName+6Z     5�      $6 车间��6)   7 . 8 66   �� *WorkShopName+6W     5�      $6 设备名称��6)   7 . 8 68   �� *	DevName+6T     5�      $6 设备编号��6)   7 . 8 68   �� *DevSN+6R     5�      $6 测点名称��6)   7 . 8 68   �� *Name+6Q     5      $6 测点编号��6)   7 . 8 68   �� *MSSN+6Q   5     �� 5     $��1��6   $��Stretch=�6(   $��Center=�6F   $��	10,0,10,0q�6a   � 5     $��2��6    5     �� 5     $��25 �6$   �� 5	     $��* �6$   .+�5     � $��0��6   $��3,3,3,0q�6$   .+�5     �� 9 �� Orientation$9 
Horizontal=�6!   $
��Left=�6:   .+�5     �� ���	#FF87CDEE$��5,0,0,0q�6;   $��Center=�6L   �� 5     .�6   数据长度:6!    �� 5     �6   �� *HistoryCount+5     5     �� 5     -   5        $��1��5     $��截图��6*   #��[ 67   $��Right=�6\   $��Center=�6x   $��0,0,5,0q�6�   : � ToolTip: 5     �� 5     $��截图��6(   5     5     � 5     -   5        ; �Name$; 	gridChart��5     $��2��6*   $��3,0,3,3q�67   5     F� 5     -   5        < F�Name$< TopLeftPath��5     $��2��6,   "����k"��pѰ��pѰ��H	$
��Left=�6�   $	��Top=�6�   $��15��6�   $��15��6�   = �� Stretch$
= Fill=�6�   ���	#FF87CDEE$��3��6�   F� 5      -   5        $< TopRightPath��5      $��2��6-   "�������kч	��kч	�e�	$��Right=�6{   $	��Top=�6�   $��15��6�   $��15��6�   $
= Fill=�6�   ���	#FF87CDEE$��3��6�   F� 5!     -   5        $< BottomLeftPath��5!     $��2��6/   '���P�Ed7�@�@��E�� ����� 	$
��Left=�6~   $��Bottom=�6�   $��15��6�   $��15��6�   $
= Fill=�6�   ���	#FF87CDEE$��3��6�   F� 5"     -   5        $< BottomRightPath��5"     $��2��60   "����h�흤 �h���.р�7��.	$��Right=�6y   $��Bottom=�6�   $��15��6�   $��15��6�   $
= Fill=�6�   ���	#FF87CDEE$��3��6�   5#     �� 5%     $��刷新��6   $��3��6"   $��14(�6/   $��50��6=   $��5q�6H   $��Center=�6S   #��[ 6p   ��6�   �� *RefreshHistoryCommand+5&     5'     5(     