   M S B A M L   `   `   `  ����J  FDiagramDesigner, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:DiagramDesignerDiagramDesigner  5      $    DiagramDesigner.DiagramControl   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      +sclr-namespace:DiagramDesigner     5      .��5      � .+�5   
   �� -   5        $��DesignerScrollViewer��5      $
��Auto=�5      $
��Auto=�5      ��5      Z� .��5      �� ;   5DiagramDesigner.DesignerItemsControlItemStyleSelector    Instance#��Z  5      ��5      �� *Items+��69   ��  �� Path$ ItemTemplateSelector� �� RelativeSource ��  �� AncestorType$   DiagramDesigner.DiagramControl"   � 	Resources 5         designerItemStyle �� 
TargetType �� Property �� Value �� Mode$   DiagramDesigner.SelectionProps  connectorItemStyle	 ��Name
 � ContextMenu m� Command m� CommandParameter �Name � Points-   'DiagramDesigner.ConnectionPathConverter   Instance �� 	Converter F�Name �� Stretch J� RenderTransformOrigin/   )DiagramDesigner.BoolToVisibilityConverter   Instance �� Left
 �� Top �� Triggers �� Value �� Binding �� 
TargetName �� Angle��%�
  &	        &	 �    �� 5      " ��66   .�5      ��  � ��$ 50��65   �� 5       � ��$ 50��66   �� 5       �@��Top 67   �� *Top+$ TwoWay=��� 5       �@��Left 68   �� *Left+$ TwoWay=��� 5       � �� 63   �� *	ItemWidth+$ TwoWay=��� 5       � �� 63   �� *
ItemHeight+$ TwoWay=��� 5       �@ EnabledForSelection . 6Q   �� 5       �@J�SnapsToDevicePixels . 6A   5^      �� 5`      " ��67   .�5a      ��  � �� 62   �� *
Area.Width+�� 5b       � �� 63   �� *Area.Height+�� 5c       �@��Top 67   �� *
Area.Top+�� 5d       �@��Left 69   �� *	Area.Left+�� 5e       �@ EnabledForSelection . 6R   �� 5f       � �� 5g      �� 5h   "   .R�5i   &   �� $	 selectedGrid��68   $��0q�6-   $��Stretch=�6N   $��Stretch=�6l   
 5j   *   �� 5k   .   .@�5l   2   m� $��删除��6;    6G   �� *DeleteConnectionCommand+ 6s   �� 5m   /   .+�5o   *   � $
 poly��63   ���	#FF87CDEE$��2��6T    5p   3   �� $ ConnectionPoints�# Z F� 5r   *   $ arrow��6/   $��10��6>   $��10��6J   ���	#FF87CDEE"����    ��� �@KL     р�� ��� 	$
��Left=�5u   5   $	��Top=�6P   $
 Fill=�5x   5   ���	#FF87CDEE$ 0.5,0.5&�5y   5   y�5t   5   �� $ IsFullConnection�# Z  5v   5   �� *
EndPoint.X+ 5w   5   �� *
EndPoint.Y+z�5z   .   �� 5{   2   rot��6B   5}   +   5~   '    5   &   �� 5�   *   $
 True��66    6C   �� *
IsSelected+.a�5�   .   �� $
 poly��65    � ��
 � ���6Y   �� 5�   .   $ arrow��65    � ��
 � ���6[   �� 5�   .   $ arrow��65    � ��
 � ���6X   5�   +   �� 5�   *   $
 Left��6n    66   �� $# SinkConnectorInfo.Orientation�.a�5�   .   �� $ arrow��65    � ��$ 
-10,-5,0,0q�6[   �� 5�   .   $ arrow��65    � z� 5�   2   �� 5�   6   $ 90Q�6F   5�   /   5�   +   �� 5�   *   $	 Top��6n    66   �� $# SinkConnectorInfo.Orientation�.a�5�   .   �� $ arrow��65    � ��$ 
-5,-10,0,0q�6Z   �� 5�   .   $ arrow��65    � z� 5�   2   �� 5�   6   $	 180Q�6F   5�   /   5�   +   �� 5�   *   $ Right��6n    66   �� $# SinkConnectorInfo.Orientation�.a�5�   .   �� $ arrow��65    � ��$ 0,-5,0,0q�6Z   �� 5�   .   $ arrow��65    � z� 5�   2   �� 5�   6   $	 -90Q�6F   5�   /   5�   +   �� 5�   *   $ Bottom��6m    66   �� $# SinkConnectorInfo.Orientation�.a�5�   .   �� $ arrow��65    � ��$ -5,0,0,0q�6Z   �� 5�   .   $ arrow��65    � z� 5�   2   �� 5�   6   $ 0Q�6F   5�   /   5�   +   5�   #   5�      5�      5�      ��5�      �� 5�      .R�5�      $   DiagramDesigner.DesignerCanvas  
������  J� 	AllowDrop . 5�      5�      5�      5�      5�      5�      