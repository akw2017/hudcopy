   M S B A M L   `   `   `  ���� J  FDiagramDesigner, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:DiagramDesignerDiagramDesigner  $    DiagramDesigner.DiagramControl   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     +sclr-namespace:DiagramDesigner     .��� .+��� -   $��DesignerScrollViewer��$
��Auto=�$
��Auto=���Z� .���� ;   5DiagramDesigner.DesignerItemsControlItemStyleSelector    Instance#��Z  ���� *Items+����  �� Path$ ItemTemplateSelector� �� RelativeSource ��  �� AncestorType$   DiagramDesigner.DiagramControl"   � 	Resources    designerItemStyle �� 
TargetType �� Property �� Value �� Mode$   DiagramDesigner.SelectionProps  connectorItemStyle	 ��Name
 � ContextMenu m� Command m� CommandParameter �Name � Points-   'DiagramDesigner.ConnectionPathConverter   Instance �� 	Converter F�Name �� Stretch J� RenderTransformOrigin/   )DiagramDesigner.BoolToVisibilityConverter   Instance �� Left
 �� Top �� Triggers �� Value �� Binding �� 
TargetName �� Angle��%  &	        &	 L    �� " ��.���  � ��$ 50����  � ��$ 50����  �@��Top �� *Top+$ TwoWay=���  �@��Left �� *Left+$ TwoWay=���  � �� �� *	ItemWidth+$ TwoWay=���  � �� �� *
ItemHeight+$ TwoWay=���  �@ EnabledForSelection . ��  �@J�SnapsToDevicePixels . �� " ��.���  � �� �� *
Area.Width+��  � �� �� *Area.Height+��  �@��Top �� *
Area.Top+��  �@��Left �� *	Area.Left+��  �@ EnabledForSelection . ��  � �� �� .R��� $	 selectedGrid��$��0q�$��Stretch=�$��Stretch=�
 �� .@�m� $��删除�� �� *DeleteConnectionCommand+ �� .+�� $
 poly�����	#FF87CDEE$��2�� �� $ ConnectionPoints�# Z F� $ arrow��$��10��$��10�����	#FF87CDEE"����    ��� �@KL     р�� ��� 	$
��Left=�$	��Top=�$
 Fill=����	#FF87CDEE$ 0.5,0.5&�y��� $ IsFullConnection�# Z  �� *
EndPoint.X+ �� *
EndPoint.Y+z��� rot�� �� $
 True�� �� *
IsSelected+.a��� $
 poly�� � ��
 � ����� $ arrow�� � ��
 � ����� $ arrow�� � ��
 � ����� $
 Left�� �� $# SinkConnectorInfo.Orientation�.a��� $ arrow�� � ��$ 
-10,-5,0,0q��� $ arrow�� � z� �� $ 90Q��� $	 Top�� �� $# SinkConnectorInfo.Orientation�.a��� $ arrow�� � ��$ 
-5,-10,0,0q��� $ arrow�� � z� �� $	 180Q��� $ Right�� �� $# SinkConnectorInfo.Orientation�.a��� $ arrow�� � ��$ 0,-5,0,0q��� $ arrow�� � z� �� $	 -90Q��� $ Bottom�� �� $# SinkConnectorInfo.Orientation�.a��� $ arrow�� � ��$ -5,0,0,0q��� $ arrow�� � z� �� $ 0Q����� .R�$   DiagramDesigner.DesignerCanvas  
������  J� 	AllowDrop . 