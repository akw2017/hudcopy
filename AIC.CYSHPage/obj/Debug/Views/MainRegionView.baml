   M S B A M L   `   `   `  ����G  CAIC.CYSHPage, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null7 clr-namespace:AIC.CYSHPage.ViewsAIC.CYSHPage.Views  5      C ?AIC.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullQ6clr-namespace:AIC.Core.ControlModels;assembly=AIC.CoreAIC.Core.ControlModels 5	      5(clr-namespace:AIC.Core;assembly=AIC.CoreAIC.Core 5      9!clr-namespace:AIC.CYSHPage.ModelsAIC.CYSHPage.Models  5      '    !AIC.CYSHPage.Views.MainRegionView   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      2local clr-namespace:AIC.CYSHPage.Views     5      P LPrism.Wpf, Version=6.2.0.0, Culture=neutral, PublicKeyToken=91a96d2a154366d8$prismhttp://prismlibrary.com/  5      U QMahApps.Metro, Version=1.5.0.23, Culture=neutral, PublicKeyToken=f4fb5a3c4d1e5b4f;Controls,http://metro.mahapps.com/winfx/xaml/controls  5      Amenu6clr-namespace:AIC.Core.ControlModels;assembly=AIC.Core  5	      c _System.Windows.Interactivity, Version=4.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity  5      3core(clr-namespace:AIC.Core;assembly=AIC.Core  5      -model!clr-namespace:AIC.CYSHPage.Models  5      -   5          >�Name$  usercontrol��5      !  Prism.Mvvm.ViewModelLocator   AutoWireViewModel .    AsyncWorkUserControlStyle#���   5       � 	Resources 5      (  "AIC.Core.BoolToVisibilityConverter  boolToVisibilityConverter1  +MahApps.Metro.Controls.WindowButtonCommands �� 
TargetType �� Property �� Value  AppThemeMenuIcon �� Mode  AccentMenuIcon  AppThemeMenuItemStyle  MetroMenuItem �� BasedOn  AccentColorMenuItemStyle � Cursor	 �� Path
 �� Orientation  AccentColorBrush �� RelativeSource �� Mode �� AncestorType �� Self�� 5   
   %�  &	       '       &	 �    &	 -   &	 �    7  7  &	 i    7  7  '�� E      5      6-   �� 5      "  6   .�5      ��  � ��$	 Top=�5      �� 5       � ��$ 24��5      5      A� 5      $��16��5      $��16��5      $��1��5      ��5       �� *BorderColorBrush+$ OneWay=���5!      �� *
ColorBrush+$ OneWay=�A� 5"      $��16��5$      $��16��5%      ��5&      �� *
ColorBrush+$ OneWay=��� 5'      " m�5(      8   .�5*      ��  �@m�Command 5+      �� *ChangeAccentCommand+�� 5,       �@m�Icon8  5-      �� 5.       � �� 5/      �� *Name+$ OneWay=�50      �� 51      " m�52      8   .�54      ��  �@m�Command 55      �� *ChangeAccentCommand+�� 56       �@m�Icon8  57      �� 58       � �� 59      �� *Name+$ OneWay=�5:      �� 5;      " ��6   .�5<      ��  � �� 5=      �� 5>      .R�5?      ]� .+�5@   "   �� $
��None=�6(   $5��//AIC.Resources;component/Images/application.png��67   $
��Left=�6p   $��0,0,6,0q�6�   � 5A   "   $��Right=�6'   $��6,0,0,0q�6>   .+�5B   &   �� $
 Hand��6S   $
��Bold"�6a   $��24��6s   $��24��6~   $��Center=�6�   $��Center=�6�   ��6-   �� $	 CloseCommand�y�6�   �� $	 
Visibility���5C   *   �� 5D   .   $
 
Horizontal=�69   .+�5E   2   �� $��X��6<   #���  6E   $��Center=�6u   5F   /   5H   '   �� 5I   &   $
 Hand��6-   $��24��6;   $��24��6F   y�5J   -   �� $	 LockVisibility���5K   -   �� *DataContext.UnLockCommand+ �� $ FindAncestor=�$ UserControlL�.��5M   *   �� $.��(/AIC.Resources;component/Images/lock.png��60   $��-1,-1,-1,-1q�6b   $
��Fill=�6w   5N   '   5O   #   �� 5P   "   $��Center=�6R   ��63   �� $	 Title�6R   5Q      5R      5T      �� 5U       � �� 6+   �� $	 Content.Closer�# Z 6�   5V      5W      .��5Y      � -   5         �Name$
 grid��5Y       � ColumnDefinitions 5Z   
   �� 5[      $
��Auto �6   �� 5\      $��* �6    � RowDefinitions 5^   
   �� 5_      $
��Auto �6   �� 5`      $
��Auto �6   �� 5a      $
��Auto �6   �� 5b      $��* �6   �� 5c      $
��Auto �6   .+�5e   
   �� $��2��6   $��2��6$   $-��'/AIC.CYSHPage;component/Images/CYSH.jpg��65   6h   �� 5f   
   $��2��6   $��1��6"   $��Center=�6M   $��Right=�6h   ��62   �� *	LoginUser+6h   9� 5h   
   -   5         9�Name$ expanderMenu��5h      $��3��6)    9� ExpandDirection$
 Left=�66     ExpanderStyle1#���  6M   ��5i      R� 5j      -   5        $��treeview��5j      $��4��6+    	 VirtualisedMetroTreeView#��� 	 6e   $	��200��6�   ��6<   �� *CatalogTreeItems+��5k      �� 5m       �� DataType*   $AIC.CYSHPage.Models.PropertyNodeItem"  63    �� ItemsSource 6^   �� $	 Children�.R�5o      �� $
 
Horizontal=�6)   .+�5p   "   �� $��Center=�6,   ��6G   �� *Name+$ TwoWay=� � ToolTip 5q   "   �� 5r   &   $��Center=�60   $
��Wrap=�6a   $	��200��6u   ��6K   �� *Name+6u   5t      5v      ��5z      �� 5{      " Q�6     
 LineTreeViewItem# [
 6:   .�5|      ��  �@Q�
IsSelected 6;   �� *
IsSelected+�� 5}       �@Q�
IsExpanded 6;   �� *
IsExpanded+5~      .  (System.Windows.Interactivity.Interaction   Triggers 5�      /  )System.Windows.Interactivity.EventTrigger  5�         	EventName$ SelectedItemChanged��6)   .  (System.Windows.Interactivity.TriggerBase   Actions. 5�      6  0System.Windows.Interactivity.InvokeCommandAction     Command 64   �� *SelectTreeChangeComamnd+   CommandParameter 6`   �� *SelectedItem+ �� ElementName$ treeview��5�      5�      5�      �� 5�   
   !	  Prism.Regions.RegionManager 	  
RegionName$ MainTabRegion��6   $��3��6H   $��1��6U   � 5�   
   $��4��6   #���  6   $��2��6L   .+�5�      �� ��5�      �� 5�      .R�5�      �  5�      �� 5�   "   $
��Auto �63   �� 5�   "   $
��Auto �63   �� 5�   "   $��* �63   �� 5�   "   $
��Auto �63   �� 5�   "   $
��Auto �63   �� 5�   "   $
��Auto �63   5�      5�      .@�5�      �� $��0��6   ��6,   Z� .@�5�      �� $
 
Horizontal=�6!   $��Right=�6:   .+�5�      F� �����4���ejѼ߲�ej� ��Z�Q4��d]�F�@@�4���ej���������#���@@�����Z���[�����[� �l't� ���4����4��l't�pej��[ф:�[� :����ej����ej��Ѡyv���P������������������������Xy���Xy�����є�H	�є�\����M\����M����'t����l't�ш���Ѹ��Tn5��Z�Tn5�do�р�C�р�C��������� �� Stretch$
 Fill=�6�  
��� � �$��20��6  $��20��6  $��Center=�6*  $��Center=�6G  y�6   �� *IsConnected+ �� 	Converter# [ 6G  �� 5�      $�� 在线��6$   
��� � �$��Center=�6B   y�6]   �� *IsConnected+# [ F� 5�      �����?�D�[�$Z)���,tu��[���D�|���IL���,���ѐtuTn5�$Z)�S�Ѹ?�Tn5�����ш�L�������|Ѹ?�D�[�ej�ej�����ej�����Z�Qpejd]�F�@@�ej�ej�߲���Ѩ1����Ѩ1��[� ���[����l't������ej��pejl't�H<R��[�\�!�[�\�!����H<R����H<R����P^\�ь˦��� ���Ѽ߲���� �D��� �Xy��`�Xy��T?�L����*�D���D�[����D�[��`d����x��n5�2��n5�<F���X�+��X�+���� �D�����H	���Z�H	���Z�\���Tn5\����m5�p��Y)D�|Ѥ���5@Ѥ��H	�$
 Fill=�6U  
���  ��$��20��6o  $��20��6z  $��Center=�6�  $��Center=�6�  y�6   �� *IsConnected+# [   �� ConverterParameter$  invert��6�  �� 5�      $�� 离线��6$   
���  ��$��Center=�6@   y�6[   �� *IsConnected+# [ $  invert��5�      �� 5�      
��������� 5�      .�6   测点总数:6%    �� 5�      �6   �� *Count+5�      �� 5�      ���	#FF87CDEE�� 5�      .�6   	正常:6#    �� 5�      �6   �� *NormalCount+5�      �� 5�      
��� ����� 5�      .�6   	预警:6#    �� 5�      �6   �� *PreWarningCount+5�      �� 5�      
��� ����� 5�      .�6   	警告:6#    �� 5�      �6   �� *WarningCount+5�      �� 5�      
���  ���� 5�      .�6   	危险:6#    �� 5�      �6   �� *DangerCount+5�      �� 5�      
��������� 5�      .�6   	异常:6#    �� 5�      �6   �� *AbnormalCount+5�      �� 5�      
���  ���� 5�      .�6   	断线:6#    �� 5�      �6   �� *UnConnectCount+5�      d� 5�      -   5        ! d�Name$! mutebtn��5�   #     SoundToggleButton#��[ 6O   " d� 	IsChecked" 64   �� *Mute+6O   5�      �� 5�      $��1��6     MetroStatusBarSeparator#��[ 6-   �� 5�      $��3��6    $y�	Collapsed=�60   .��5�      �� $
 
Horizontal=�6!   .+�5�      A� -   5        # A�Name$# ellipse��5�   "   $��12��63   $��12��6>   
���  ��$ � Triggers$ 5�      :� 5�   "   % :� RoutedEvent$% Window.Loaded��6/   .[�5�   &   �� .��5�   *   �� & g� RepeatBehavior$& Forever��65   .��5�   .   W� ' g� 	BeginTime$' 0X�6>   ( �� 
TargetName$( ellipse��6L   ) �� TargetProperty$) (UIElement.Opacity)�6l   * W� From$* 1X�6�   	+ W� To$+ 0X�6�   , g� Duration$, 00:00:01D�6�   5�   +   5�   '   5�   #   5�      �� 5�      $��Center=�6%   �� 6B   - �� Command- 6L   �� *AlarmListCommmad+.�6q   .�6s   �� $��报警列表��6}   6�   6�   5�      5�      �� 5�      $��4��6    y�60   �� *ExceptListVisibility+.��5�      �� $
 
Horizontal=�6!   .+�5�      A� -   5        $# ellipse2��5�   "   $��12��64   $��12��6?   
��� ���$ 5�      :� 5�   "   $% Window.Loaded��6/   .[�5�   &   �� .��5�   *   �� $& Forever��65   .��5�   .   W� $' 0X�6>   $( ellipse2��6L   $) (UIElement.Opacity)�6m   $* 1X�6�   $+ 0X�6�   $, 00:00:01D�6�   5�   +   5�   '   5   #   5     �� 5     $��Center=�6%   �� 6B   - 6L   �� *ExceptListCommmad+.�6r   .�6t   �� $��异常列表��6~   6�   6�   5     5     �� 5     $��5��6!   $��Right=�6M   ��61   �� *	NowTime+6M   5     5	     5
     5     