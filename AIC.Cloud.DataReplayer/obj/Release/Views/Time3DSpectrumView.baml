   M S B A M L   `   `   `  ����   AIC.Cloud.DataReplayer5    /AIC.Cloud.DataReplayer.Views.Time3DSpectrumView   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008       � 	Resources     ButtonStyle �� 
TargetType �� Property �� Value �� 
TargetType �� CornerRadius J� SnapsToDevicePixels �� Triggers P� Property	 P� Value��%�  &	        �� " ��.���  � ��
 ���� ��  � �� �	#FF87CDEE��  � ��$ Center=���  � ��$ Center=���  � ��$ 1q���  � �� �� " ��.R�� .+��� #��z��$ 7���� #��z��#��z����. # z #��z�� P�  �@J�IsMouseOver	 . . ���  � �� �	#7F243859P�  � {�	 .  . ���  � �� �#ADADADP�  � ��	 . . ���  � �� �	#FF20437E.��� .+�� $��2q����	#7F243859
 � RowDefinitions
 �� $��25 ��� $��* �.+�� .+��� ���	#FF87CDEE$��Center=�$
��Left=�$��5,0,0,0q����� *Title+��  �� Orientation$ 
Horizontal=�$��Right=�$��0,2,2,2q�.+��� -   $��80��$��0,0,5,0q����� *
Channels+ �� SelectedItem �� *SelecetedChannel+���� .R��� ��`�  �� StringFormat$! {0}.{1}.{2}.{3}.{4}.{5}.{6}��.1���  �� Path$ Channel.GroupCOName��� $ Channel.CorporationName��� $ Channel.WorkShopName��� $ Channel.DevName��� $ Channel.DevSN��� $ Channel.Name��� $ Channel.MSSN� � ToolTip �� ��`� $! {0}.{1}.{2}.{3}.{4}.{5}.{6}��.1��� $* $SelecetedChannel.Cahnnel.GroupCOName��� $. (SelecetedChannel.Cahnnel.CorporationName��� $+ %SelecetedChannel.Cahnnel.WorkShopName��� $&  SelecetedChannel.Cahnnel.DevName��� $$ SelecetedChannel.Cahnnel.DevSN��� $# SelecetedChannel.Cahnnel.Name��� $# SelecetedChannel.Cahnnel.MSSN��� -   $��clearChartBtn��$��清除��#��[  � -   $��timeGrid3DChart��$��1���� -   $��1��#��[  $��25��$��25��$��截图��$��0,0,5,0q�$��Right=�$	��Top=� �� $��截图���� -   $��
messageTxt��$
��Left=�$	��Top=�
���  ��$
��Bold"�$��1��F� "����k"��pѰ��pѰ��H	$��2��$
��Left=�$	��Top=�$��15��$��15�� �� Stretch$
 Fill=����	#FF87CDEE$��3��F� "�������kч	��kч	�e�	$��2��$��Right=�$	��Top=�$��15��$��15��$
 Fill=����	#FF87CDEE$��3��F� '���P�Ed7�@�@��E�� ����� 	$��2��$
��Left=�$��Bottom=�$��15��$��15��$
 Fill=����	#FF87CDEE$��3��F� "����h�흤 �h���.р�7��.	$��2��$��Right=�$��Bottom=�$��15��$��15��$
 Fill=����	#FF87CDEE$��3��