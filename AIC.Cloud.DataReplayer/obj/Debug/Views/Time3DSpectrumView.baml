   M S B A M L   `   `   `  ����  AIC.Cloud.DataReplayer5    /AIC.Cloud.DataReplayer.Views.Time3DSpectrumView   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5        � 	Resources  5         ButtonStyle �� 
TargetType �� Property �� Value �� 
TargetType �� CornerRadius J� SnapsToDevicePixels �� Triggers P� Property	 P� Value��%�  &	        �� 5	   
   " ��6$   .�5
      ��  � ��
 ���� 6+   �� 5       � �� �	#FF87CDEE6+   �� 5       � ��$ Center=�6;   �� 5       � ��$ Center=�69   �� 5       � ��$ 1q�6(   �� 5       � �� 5      �� 5      " ��6&   .R�5      � .+�5      �� #��z��6%   $ 7��6O   �� 5      #��z��6/   #��z��6r   ��. # z 6�   #��z��6�   5       5      P� 5       �@J�IsMouseOver	 . . �5   "   ��  � �� �	#7F2438596?   5      P� 5       � {�	 .  . �5   "   ��  � �� �#ADADAD6?   5      P� 5       � ��	 . . �5   "   ��  � �� �	#FF20437E6?   5       5"      5$      5%      5&      .��5(      � .+�5)   
   � $��2q�6   ���	#7F243859
 � RowDefinitions
 5*      �� 5+      $��25 �6    �� 5,      $��* �6    .+�5/      � .+�50      �� ���	#FF87CDEE$��Center=�6K   $
��Left=�6f   $��5,0,0,0q�6�   ��6   �� *Title+6�   �� 51       �� Orientation$ 
Horizontal=�6   $��Right=�66   $��0,2,2,2q�6R   .+�52      �� -   5        $��80��52   �   $��0,0,5,0q�6�   ��6   �� *
Channels+ �� SelectedItem 6@   �� *SelecetedChannel+��53      �� 54      .R�55   "   �� ��56   &   `� 57   *    �� StringFormat$! {0}.{1}.{2}.{3}.{4}.{5}.{6}��67   .1�58   .   ��  �� Path$ Channel.GroupCOName�66   �� 59   .   $ Channel.CorporationName�66   �� 5:   .   $ Channel.WorkShopName�66   �� 5;   .   $ Channel.DevName�66   �� 5<   .   $ Channel.DevSN�66   �� 5=   .   $ Channel.Name�66   �� 5>   .   $ Channel.MSSN�66   5?   +   5A   #   5B       � ToolTip 5D      �� 5E      ��5F   "   `� 5G   &   $! {0}.{1}.{2}.{3}.{4}.{5}.{6}��63   .1�5H   *   �� $* $SelecetedChannel.Cahnnel.GroupCOName�62   �� 5I   *   $. (SelecetedChannel.Cahnnel.CorporationName�62   �� 5J   *   $+ %SelecetedChannel.Cahnnel.WorkShopName�62   �� 5K   *   $&  SelecetedChannel.Cahnnel.DevName�62   �� 5L   *   $$ SelecetedChannel.Cahnnel.DevSN�62   �� 5M   *   $# SelecetedChannel.Cahnnel.Name�62   �� 5N   *   $# SelecetedChannel.Cahnnel.MSSN�62   5O   '   5Q      5S      �� 5T      -   5        $��clearChartBtn��5T   O   $��清除��6   #��[  6*   6d   5U      5V      � 5X      -   5        $��timeGrid3DChart��5X      $��1��6*   5Z      �� 5[      -   5        $��1��5[      #��[  6"   $��25��6G   $��25��6R   $��截图��6_   $��0,0,5,0q�6l   $��Right=�6}   $	��Top=�6�    5\      �� 5]      $��截图��6    5_      5`      �� 5b   
   -   5        $��
messageTxt��5b      $
��Left=�6&   $	��Top=�6A   
���  ��$
��Bold"�6j   $��1��6|   F� 5c   
   "����k"��pѰ��pѰ��H	$��2��6T   $
��Left=�6e   $	��Top=�6�   $��15��6�   $��15��6�    �� Stretch$
 Fill=�6�   ���	#FF87CDEE$��3��6�   F� 5d   
   "�������kч	��kч	�e�	$��2��6L   $��Right=�6]   $	��Top=�6y   $��15��6�   $��15��6�   $
 Fill=�6�   ���	#FF87CDEE$��3��6�   F� 5e   
   '���P�Ed7�@�@��E�� ����� 	$��2��6M   $
��Left=�6^   $��Bottom=�6y   $��15��6�   $��15��6�   $
 Fill=�6�   ���	#FF87CDEE$��3��6�   F� 5f   
   "����h�흤 �h���.р�7��.	$��2��6G   $��Right=�6X   $��Bottom=�6t   $��15��6�   $��15��6�   $
 Fill=�6�   ���	#FF87CDEE$��3��6�   5g      5h      