   M S B A M L   `   `   `  ����T  PWpf.PageNavigationControl, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullE'clr-namespace:Wpf.PageNavigationControlWpf.PageNavigationControl  5      .    (Wpf.PageNavigationControl.PageNavigation   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      9local'clr-namespace:Wpf.PageNavigationControl     5      -   5        $��pageNavigation��5   6     � 	Resources  5	       �� 
TargetType �� Property �� Value �� Orientation �� Triggers P� Property P� Value �� Self	 �� RelativeSource
 �� Path   PathBtnStyle*   $Wpf.PageNavigationControl.PathButton �� 
TargetType ��Name �� CornerRadius � ColumnDefinitions �� Stretch �� AncestorType �� TextAlignment  AccentColorBrush  AccentColorBrush2  AccentColorBrush3��%�  '��       &	  �    �� 5
   
   " ��6   .�5      ��  �@=�ErrorTemplate 5      �� 5      .R�5      �� $ 
Horizontal=�6%   .+�5      �� 
���  ��$��Center=�69   $��18(�6T   !6e   �� 5      5      5      5       5      P� 5       �@=�HasError . . �5      ��  � ��
 �  ��64   �� 5       �@�ToolTip 60   �� #	 Z $2
 ,(Validation.Errors).CurrentItem.ErrorContent�5      5      �� 5   
   "  6%   .�5      ��  � �� 5      �� 5       "  6&   .R�5!      �� $ border��6!   $��1q�61   #��z��6E   
�������$ 2��6�   ._�5"      � $��2q�6#    5#   "   �� 5$   &   $��* �67   �� 5%   &   $
��Auto �67   .+�5'   "   F� $ Uniform=�6w   ��6'   �� 	 �� *TemplatedParent+$
 PathData���6�   �� *
Foreground+	 �� *FindAncestor+"  �� 5(   "   $��1��6,   #��z��6<   $��Center=�6]   $
��Left=�6x   $ Center=�6  ��6�   �� *
Foreground+	 �� *FindAncestor+"  6  5)      5*      5+      5-       5.      P� 5/       � {� .  . �50      ��  � ��
 �����63   51      P� 52       � {� . . �53      ��  � ��# �  63   54      P� 55       �@J�IsMouseOver . . �56      ��  � ��# �  63   57      P� 58       � �� . . �59      ��  � ��# �  63   5:      5<      5=      .��5?      �� $��1q�6   #���  6!   ._�5@   
   � .+�5A   
   �� $ 
Horizontal=�6   .+�5B      �� $��0,0,5,0q�6   ��6(   ��  �� ElementName$ pageNavigation��$
 PageSizeList� �� SelectedValue 6m   �� $ pageNavigation��$
 PageSize�5D        5E      $��5,0,0,0q�6   .   (Wpf.PageNavigationControl.PageNavigation   PageFirstCommand#��Z 60   $��24��6k   $��24��6w   #���   6�      PathData$� �M 53.8335,22.1665L 33.1281,38L 53.8335,53.8335L 53.8335,22.1665 Z M 29.4742,23.3844L 22.1665,23.3844L 22.1665,52.6155L 29.4742,52.6155L 29.4742,23.3844 Z ��6�     5F      $��5,0,0,0q�6      PageUpCommand#��Z 60   $��24��6h   $��24��6t   #���   6   $a [F1 M 30.0833,22.1667L 50.6665,37.6043L 50.6665,38.7918L 30.0833,53.8333L 30.0833,22.1667 Z ��6�    J� RenderTransformOrigin$ 0.5, 0.5&�5G      z�5H      �� 5I       �� Angle$	 180Q�6&   5K      �� 5L      $��5,0,0,0q�6   $	��第��6)   $��Center=�62   �� 5M      $��Center=�6�   $��30��6�   ��6   �� $ pageNavigation��$
 CurrentPage� �� Mode$ TwoWay=� �� UpdateSourceTrigger$ PropertyChanged=� �� ValidatesOnExceptions .  �� ValidatesOnDataErrors .  �� NotifyOnValidationError . .��6   J� InputBindings 5N      �� 5O      
 �� Key$ Enter��6!       PageGotoCommand! �� Command#! Z  6-   " �� CommandParameter" 6g   �� $

 Text�	 �� " ��.��5P   )   5Q      �� 5R      $��Center=�6   ��5S      `� 5T      # �� StringFormat$# 页 / 共{0}页��6#   .1�5U      �� $ pageNavigation��6"   $
 
TotalPages�6?   5V      5X        5Y      $��5,0,0,0q�6   $   PageDownCommand#��Z$ 60   $��24��6j   $��24��6v   #���   6�   $a [F1 M 30.0833,22.1667L 50.6665,37.6043L 50.6665,38.7918L 30.0833,53.8333L 30.0833,22.1667 Z ��6�     5Z      $��5,0,0,0q�6   %   PageLastCommand#��Z% 60   $��24��6j   $��24��6v   #���   6�   $� �M 22.1665,22.1665L 42.8718,38L 22.1665,53.8335L 22.1665,22.1665 Z M 46.5257,23.3844L 53.8335,23.3844L 53.8335,52.6155L 46.5257,52.6155L 46.5257,23.3844 Z ��6�     5[      $��5,0,0,0q�6   &   PageRefreshCommand#��Z& 60   $��24��6m   $��24��6y   #���   6�   $� �F1 M 38,20.5833C 42.9908,20.5833 47.4912,22.6825 50.6667,26.046L 50.6667,17.4167L 55.4166,22.1667L 55.4167,34.8333L 42.75,34.8333L 38,30.0833L 46.8512,30.0833C 44.6768,27.6539 41.517,26.125 38,26.125C 31.9785,26.125 27.0037,30.6068 26.2296,36.4167L 20.6543,36.4167C 21.4543,27.5397 28.9148,20.5833 38,20.5833 Z M 38,49.875C 44.0215,49.875 48.9963,45.3932 49.7703,39.5833L 55.3457,39.5833C 54.5457,48.4603 47.0852,55.4167 38,55.4167C 33.0092,55.4167 28.5088,53.3175 25.3333,49.954L 25.3333,58.5833L 20.5833,53.8333L 20.5833,41.1667L 33.25,41.1667L 38,45.9167L 29.1487,45.9167C 31.3231,48.3461 34.483,49.875 38,49.875 Z ��6�   5\      �� 5]   
   $ 
Horizontal=�6   $��Right=�6.   .+�5^      �� $��Center=�6   ��5_      `� 5`      $"# 显示{0}到{1},共{2}记录��6#   .1�5a      �� $ pageNavigation��6"   $
 	FirstItem�6?   �� 5b      $ pageNavigation��6"   $
 
SecondItem�6?   �� 5c      $ pageNavigation��6"   $
 
TotalItems�6?   5d      5f      5g      5h      5i      5j      