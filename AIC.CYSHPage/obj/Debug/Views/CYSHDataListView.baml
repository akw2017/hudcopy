   M S B A M L   `   `   `  ����C  ?AIC.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null[;clr-namespace:AIC.Core.OrganizationModels;assembly=AIC.CoreAIC.Core.OrganizationModels  5      T PWpf.PageNavigationControl, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullhJclr-namespace:Wpf.PageNavigationControl;assembly=Wpf.PageNavigationControlWpf.PageNavigationControl 5      I2clr-namespace:AIC.Core.ExCommand;assembly=AIC.CoreAIC.Core.ExCommand  5      G CAIC.CYSHPage, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullA%clr-namespace:AIC.CYSHPage.ConvertersAIC.CYSHPage.Converters 5      O5clr-namespace:AIC.Core.SignalModels;assembly=AIC.CoreAIC.Core.SignalModels  5      5(clr-namespace:AIC.Core;assembly=AIC.CoreAIC.Core  5      G CAIC.CoreType, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullA0clr-namespace:AIC.CoreType;assembly=AIC.CoreTypeAIC.CoreType 5      7 clr-namespace:AIC.CYSHPage.ViewsAIC.CYSHPage.Views 5      )   #AIC.CYSHPage.Views.CYSHDataListView   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      c _System.Windows.Interactivity, Version=4.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity  5      Icontrol;clr-namespace:AIC.Core.OrganizationModels;assembly=AIC.Core  5      U	 QMahApps.Metro, Version=1.5.0.23, Culture=neutral, PublicKeyToken=f4fb5a3c4d1e5b4f;Controls,http://metro.mahapps.com/winfx/xaml/controls 	 5      P
 LPrism.Wpf, Version=6.2.0.0, Culture=neutral, PublicKeyToken=91a96d2a154366d8$prismhttp://prismlibrary.com/ 
 5	      UpageJclr-namespace:Wpf.PageNavigationControl;assembly=Wpf.PageNavigationControl 
 5      @command2clr-namespace:AIC.Core.ExCommand;assembly=AIC.Core 
 5      4conveter%clr-namespace:AIC.CYSHPage.Converters 
 5      GbsFilter8http://schemas.bolapansoft.com/xaml/Controls/ItemsFilter 
 5      Bsignal5clr-namespace:AIC.Core.SignalModels;assembly=AIC.Core 
 5      3core(clr-namespace:AIC.Core;assembly=AIC.Core 
 5      ?coreType0clr-namespace:AIC.CoreType;assembly=AIC.CoreType 
 5      +view clr-namespace:AIC.CYSHPage.Views 
 5      ! 
 Prism.Mvvm.ViewModelLocator    AutoWireViewModel  .  � 	Resources 5      8  2AIC.CYSHPage.Converters.IntToAlarmTypeStrConverter   intToAlarmTypeStrConverter   AIC.Core.TypeOfConverter  typeOfConverter    AIC.Core.IsDivfreConverter  isDivfreConverter(   "AIC.Core.VisibilityToBoolConverter  visibilityToBoolConverter(   "AIC.Core.BoolToVisibilityConverter  boolToVisibilityConverter'   !AIC.Core.EnumDescriptionConverter  enumDescriptionConverter  CheckBoxStyle �� 
TargetType �� Property �� Value �� 
TargetType �� 
Background J� SnapsToDevicePixels �� Bullet	 ��Name  AccentColorBrush
 �� RadiusX �� RadiusY F�Name �� Stretch �� Triggers P� Property P� Value �� 
TargetName  expandToggleStyle �� BasedOn 	 DarkAccentColorBrush'  !System.Windows.VisualStateManager   VisualStateGroups%	  System.Windows.VisualStateGroup 	  Transitions%
  System.Windows.VisualTransition 
  GeneratedDuration 	  States   System.Windows.VisualState   
Storyboard g� Duration	 W� To �� TargetProperty �� 
TargetName J� Opacity � UseLayoutRounding J� RenderTransformOrigin �� Angle 
 HeiachyTemplate&   System.Windows.Controls.DataGrid    
IsReadOnly!   AutoGenerateColumns"   GridLinesVisibility#   RowHeaderWidth$   Columns4  .System.Windows.Controls.DataGridTemplateColumn,  &System.Windows.Controls.DataGridColumn%   Header&   SortMemberPath'   
IsReadOnly(   CellTemplate) A�Name* �� Triggers+ �� Value, �� Binding- �� Path0  *System.Windows.Controls.DataGridTextColumn1  +System.Windows.Controls.DataGridBoundColumn.   Binding/ �� StringFormat  enumComboItemTemplate0 �� 	Converter�� 5   
   %e  &	        &	      &	 &     &	 9     &	 L     &	 _     &	 r     &	 �    7d�&	
 �    &	 �    7    5      62     5      6#     5      6%     5      6-     5      6-     5      6,   �� 5      " ��6*   .�5      ��  � ��$ 1q�64   �� 5        � ��$ Center=�6=   �� 5"       � �� 5#      �� 5$      " ��6*   .R�5%      �� 
 ����  .  5&   "   � 5'   &   $��24��6+   $��24��67   .+�5(   *   �� $	 	PART_Rect��64   #���  6G   $��3��6s   $
 5��6�   $ 5��6�   F� 5)   *   $
 path��6/   $��18��6=   $��18��6H   $
 Fill=�6T   #��z��6c   F�����ej�Z�����x��,E=R�ܿe������Tn5�$�!��C��ej�Z�6�   5*   '   ._�5,   "   �� #��z��63   #��z��6v   ��. # z 6�   #��z��6�   5-       5.      P� 5/   "    � �� . . �50   &   ��  � ��$ 4,0,0,0q�6@   51   #   P� 52   "    � {� .  . �53   &   �� # � ��6C    � ��54   #   P� 55   "    �@d�	IsChecked$ FalseY�6?   . �56   &   �� $
 path��6C    � y�$ Hidden=�6U   57   #   59      5;      5<      �� 5=      " d�6.   8   .�5>      ��  �@�Cursor$
 Hand��6+   �� 5?       � ��# � 	 6/   �� 5@       � ��
 ���� 6/   �� 5A       � �� 5B      �� 5C      " d�6*   .R�5D      �  5E   "   	  5F   &   CommonStates�� 5G   *   
  5H   .   $ 0:0:0.1D�6?   . 5J   *     
Normal��66     5K   *   	MouseOver��. 5L   .   �� .��5M   2   W� $ 0D�6B   $ 1X�6O   $ (UIElement.Opacity)�6V   $ normal��6�   5N   /   5O   +     5P   *   Pressed��. 5Q   .   �� .��5R   2   W� $ 0D�6B   $ 1X�6O   $ (UIElement.Opacity)�6V   $ normal��6�   5S   /   5T   +     5U   *   Disabled��. 5V   .   �� .��5W   2   W� $ 0D�6B   $	 0.3X�6O   $ (UIElement.Opacity)�6X   $ normal��6�   5X   /   5Y   +   5Z   '   	  5[   &   CheckStates��. 5\   *     Checked��. 5]   .   �� .��5^   2   W� $ 0:0:0.3D�6B   $	 180X�6U   $ Angle�6^   $ symbolRotation��6�   5_   /   5`   +     5a   *   	Unchecked��. 5b   .   �� .��5c   2   W� $ 0:0:0.3D�6B   $ 90X�6U   $ Angle�6]   $ symbolRotation��6   5d   /   5e   +     5f   *   Indeterminate��66   5g   '   	  5h   &   FocusStates��. 5i   *     Focused��66     5j   *   	Unfocused��66   5k   '   .+�5m   "   �� $��2��6)   $��3��66   #��z��6K   #��z��6u   #��z��6�   F� 5n   "   $ normal��6'   $��Center=�67   $��10��6T   $
 Fill=�6_   $	 0.6Q�6n   �����@B ��[ �@B ��[ �ا �ا �@B �ا �pr ����n@�pr ����n@S�pr ����n@��� �ا ��� �ا Ӏ�� �ا ��� ��[ ��� ��[ Ӏ�� ��[ �pr P�^����?�pr P�^����?S�pr P�^����?��[ ��[ 	#��z��6    .  $��Center=�6^  $��7��6y  $ 0.5,0.5&�6�  z�5o   &   V� 5q   *   .~�5r   .   �� �� 5s   .   �� 5t   .   symbolRotation��$ 90Q�6V   T� 5u   .   5v   +   5x   #   5y       5z      P� 5{   "    �@d�	IsChecked$
 trueY�6?   . �5|   &   ��  � ��$	 Top=�6J   5}   #   5      5�      5�      �� 5�      .R�5�        $��0q�6;   
������ $��0q�6_     . ! .  $
" None=�6�   $# 0Q�5�      ��5�      �� *	DivFres+��6s   Z� $ 5�        5�      $% 报警级别��61   $& DelayAlarmGrade��6?   ' . ( 5�      �� 5�   "   .R�5�   &   A� $) ellipse��6.   ���	#FF85CBEB$��Center=�6P   $��12��6m   $��0q�6y   $��12��6�   $��Center=�6�   * 5�   &   �� 5�   *   $+ Invalid��6_   , 66   �� $- DelayAlarmGrade�.a�5�   .   �� $ ellipse��65    � ��
 �����6Z   5�   +   �� 5�   *   $+ 
HighNormal��6_   , 66   �� $- DelayAlarmGrade�.a�5�   .   �� $ ellipse��65    � �� �	#FF85CBEB6Z   5�   +   �� 5�   *   $+ 	LowNormal��6_   , 66   �� $- DelayAlarmGrade�.a�5�   .   �� $ ellipse��65    � �� �	#FF85CBEB6Z   5�   +   �� 5�   *   $+ 	HighAlert��6_   , 66   �� $- DelayAlarmGrade�.a�5�   .   �� $ ellipse��65    � ��
 � ���6Z   5�   +   �� 5�   *   $+ LowAlert��6_   , 66   �� $- DelayAlarmGrade�.a�5�   .   �� $ ellipse��65    � ��
 � ���6Z   5�   +   �� 5�   *   $+ 
HighDanger��6_   , 66   �� $- DelayAlarmGrade�.a�5�   .   �� $ ellipse��65    � ��
 �  ��6Z   5�   +   �� 5�   *   $+ 	LowDanger��6_   , 66   �� $- DelayAlarmGrade�.a�5�   .   �� $ ellipse��65    � ��
 �  ��6Z   5�   +   5�   #   5�        5�      $% 分频名称��6-   ' . . 6;   �� *DisPlayName+6[     5�      $% 采集时间��6-   ' . . 6<   �� *ACQDatetime+$/ yyyy/MM/dd HH:mm:ss��6~     5�      $% 频率��6-   ' . . 6:   �� *FreV+$/ F3��6c     5�      $% 幅值��6-   ' . . 6:   �� *FreMV+$/ F3��6d     5�      $% 相位��6-   ' . . 6:   �� *Phase+$/ F1��6d     5�      $% 高危��6-   ' . . 6:   �� *
HighDanger+$/ F3��6i     5�      $% 高警��6-   ' . . 6:   �� *	HighAlert+$/ F3��6h     5�      $% 正常(高)��6-   ' . . 6=   �� *
HighNormal+$/ F3��6l     5�      $% 正常(低)��6-   ' . . 6=   �� *	LowNormal+$/ F3��6k     5�      $% 低警��6-   ' . . 6:   �� *
LowAlert+$/ F3��6g     5�      $% 低危��6-   ' . . 6:   �� *	LowDanger+$/ F3��6h     5�      $% 转速��6-   ' . . 6:   �� *RPM+$/ F1��6b     5�      $% 默认转速��6-   ' . . 6<   �� *
DefaultR+6Y     5�      $% 基础频率1��6-   ' . . 6=   �� *
Base1Fre+6Z     5�      $% 基础频率2��6-   ' . . 6=   �� *
Base2Fre+6Z     5�      $% 最大频率数��6-   ' . . 6<   �� *	MaxFreNum+$/ F3��6j     5�      $% 倍频��6-   ' . . 6:   �� *
MultiFre+6W     5�      $% 特征频率��6-   ' . . 6<   �� *CharacteristicFre+6b     5�      $% 浮动百分比��6-   ' . . 6=   �� *Base1FrePercent+$/ F3��6r   5�      5�      �� 5�      .R�5�      �� ��6   �� $- .�80   5�      5�      .��5�      � 1 � RowDefinitions1 5�   
   �� 5�      $��0 �6   �� 5�      $��* �6   �� 5�      $��0 �6   2 � ColumnDefinitions2 5�   
   �� 5�      $��0 �6   �� 5�      $
��Auto �6   �� 5�      $��* �6   .+�5�   
   R� -   5        $��treeview��5�   1   $��0��6   $��3��6      VirtualisedMetroTreeView#���  5�      ��5�      �� *OrganizationTreeItems+ 5�      3 �� DataType?   9AIC.Core.OrganizationModels.OrganizationTreeItemViewModel4 �� ItemsSource5 �� Orientation6 ��Name7 �� Mode9   3AIC.Core.OrganizationModels.DeviceTreeItemViewModel��%�  (��       5�   +   *, +)(�� [    5�   +   *, +)�� 5�      "3  6+   4 6e   �� $- Children�.R�5�      �� $5 
Horizontal=�6!   .+�5�      F� $ IconPath��6   $��18��61   $��18��6<   $
 Fill=�6H   #���  6W   ������Bnx�V��Bn4�Cќ�O�ob�L�!�!�4�C�71�ܽ�,���ܽ���K�����z@����H��р�e�!��8�obф�4�Cф�x�V��Bnx�V�4�C�Śј��d%ј�(�(��ǘ���@@(�(��ǘ���@@����08�����08�(�(����(�(�����d%�4�C�Ś6�   � 5�      $��2,0,2,0q�6   .+�5�      �� $6 txtName��6(   $
��Auto��6\   ��69   �� *Name+$7 TwoWay=�6\   5�      5�      * 5�      �� 5�      $+ 0��6O   , 6&   �� *T_Organization.Level+.a�5�      �� $ IconPath��6%    � ��� �� ��:�����:����pej� �pej� ��:���2��x��2��x����Ѥ����Ѥ��2�� ����є�����'t���������������E�'t�E�'t0P�є�0P�є��E� ��E� ��������EѤ����+�Tn5��+�����+�x����+�x���E����E����x��Tn5�x��Tn5�EѤ���E�$� �E�$� �x��$� l't�T�-���\��l't�\���x��\���EѬ<R�EѬ<R�x��Ԙ� �x��Ԙ� �E�$� �E���-���������%����,�U���-�P��V�� �h�V�� P�7���-���6K   �� 5�      $ IconPath��6%    � ��$ 24��6L   5�      �� 5�      $+ Invalid��6@   , 6&   �� *Alarm+.a�5�      �� $ txtName��6%    � ��
 �����6P   5�      �� 5�      $+ HighPreAlert��6@   , 6&   �� *Alarm+.a�5�      �� $ txtName��6%    � ��
 � ���6P   5�      �� 5�      $+ LowPreAlert��6@   , 6&   �� *Alarm+.a�5�      �� $ txtName��6%    � ��
 � ���6P   5�      �� 5�      $+ 	HighAlert��6@   , 6&   �� *Alarm+.a�5�      �� $ txtName��6%    � ��
 � ���6P   5�      �� 5�      $+ LowAlert��6@   , 6&   �� *Alarm+.a�5�      �� $ txtName��6%    � ��
 � ���6P   5�      �� 5�      $+ 
HighDanger��6@   , 6&   �� *Alarm+.a�5�      �� $ txtName��6%    � ��
 �  ��6P   5�      �� 5�      $+ 	LowDanger��6@   , 6&   �� *Alarm+.a�5�      �� $ txtName��6%    � ��
 �  ��6P   5�      5�      �� 5�      "3  6+   .R�5�      �� $5 
Horizontal=�6!   .+�5�      F� $��18��6   $��18��6*   $
 Fill=�66   #���  6E   �����P�x��ݩ(�� ���� p׻\r� $�� �(4��0�	�^)�@@�5������@@�.>�A�G�!اQ���P�x�ز�ݩ������y@@�����p׻�:pΈ"@@$��pP�(4�Ql^�	�^)�@@SP=������@@���A(Ċ�!�L�c����2ز�S�U)�����y@@�� ����d�:pΈ"@@��7!P�Ѵ�� l^����� P=�d*� ��PJ� �Ê��L�c�(�� �2��� �U)\r� �� � �dѐ0�7!є5P�� Ӱ.>��� �Gd*� اQPJ� �P�x�� 6n -1Ӵ�L -1 -1��L -1 6n� -1L���L ?� 6n ?��L� ?� ?�L� ?� 6n� ?���LL� -1 6n -16o   �� 5�      $��2,0,2,0q�6%   $5 
Horizontal=�66   .+�5�      �� $6 txtName��6(   $
��Auto��6\   ��69   �� *Name+$7 TwoWay=�6\   �� 5�      $6 txtRun��6(   
��� �|�$��:运行��6O   �� 5�      $6 txtStop��6(   
���  ��$��:停止��6J   $y�	Collapsed=�6U   5�      5      * 5     �� 5     $+ Invalid��6@   , 6&   �� *Alarm+.a�5     �� $ txtName��6%    � ��
 �����6P   5     �� 5     $+ false��6D   , 6&   �� *	IsRunning+.a�5     �� $ txtRun��6%    � y�$ 	Collapsed=�6O   �� 5     $ txtStop��6%    � y�$ Visible=�6P   5     �� 5	     $+ HighPreAlert��6@   , 6&   �� *Alarm+.a�5
     �� $ txtName��6%    � ��
 � ���6P   5     �� 5     $+ LowPreAlert��6@   , 6&   �� *Alarm+.a�5     �� $ txtName��6%    � ��
 � ���6P   5     �� 5     $+ 	HighAlert��6@   , 6&   �� *Alarm+.a�5     �� $ txtName��6%    � ��
 � ���6P   5     �� 5     $+ LowAlert��6@   , 6&   �� *Alarm+.a�5     �� $ txtName��6%    � ��
 � ���6P   5     �� 5     $+ 
HighDanger��6@   , 6&   �� *Alarm+.a�5     �� $ txtName��6%    � ��
 �  ��6P   5     �� 5     $+ 	LowDanger��6@   , 6&   �� *Alarm+.a�5     �� $ txtName��6%    � ��
 �  ��6P   5     5     5     ��5     �� 5      " Q�6     LineTreeViewItem# [ 62   .�5!     ��  �@Q�
IsSelected 63   �� *
IsSelected+$7 OneTime=��� 5"      �@Q�
IsExpanded 63   �� *
IsExpanded+$7 OneTime=�5#     .  (System.Windows.Interactivity.Interaction8   Triggers8 5&     /  )System.Windows.Interactivity.EventTrigger  5'     9   	EventName$9 SelectedItemChanged��6!   .  (System.Windows.Interactivity.TriggerBase:   Actions.: 5(     6  0System.Windows.Interactivity.InvokeCommandAction  ;   Command; 6,   �� *SelectedTreeChangedComamnd+<   CommandParameter< 6[   �� *SelectedItem+= �� ElementName$= treeview��5)     5,     �� 5.  
   $��0��6   $��3��6$   $��1��65   $��3��6E   #���  6O   $��Center=�6   $��Stretch=�6�   �� 50  
   $��0��6   $��2��6"   $5 
Horizontal=�62   .+�52     �� $��报警级别:��6   $��Center=�6%   $��2,0,2,0q�6@   �� 53     $��5,2,5,2q�6   $5 
Horizontal=�6*   .+�54     A� 
�������$��Center=�6&   $��24��6A   $��24��6M   �� 55     $��无效��6   $��Center=�6&   �� 56     $��Center=�6   #��[ 6i   > d� 	IsChecked> 66   �� *IsInvalidSignal+$7 TwoWay=�6i   57     �� 58     $��5,2,5,2q�6   $5 
Horizontal=�6*   .+�59     A� ���	#FF85CBEB$��Center=�6+   $��24��6F   $��24��6R   �� 5:     $��	高正常��6   $��Center=�6'   �� 5;     $��Center=�6   #��[ 6l   > 66   �� *IsHighNormalSignal+$7 TwoWay=�6l   5<     �� 5=     $��5,2,5,2q�6   $5 
Horizontal=�6*   .+�5>     A� 
��� ���$��Center=�6(   $��24��6C   $��24��6O   �� 5?     $��	高预警��6   $��Center=�6'   �� 5@     $��Center=�6   #��[ 6n   > 66   �� *IsHighPreAlertSignal+$7 TwoWay=�6n   5A     �� 5B     $��5,2,5,2q�6   $5 
Horizontal=�6*   .+�5C     A� 
��� ���$��Center=�6,   $��24��6G   $��24��6S   �� 5D     $��高警��6   $��Center=�6&   �� 5E     $��Center=�6   #��[ 6k   > 66   �� *IsHighAlertSignal+$7 TwoWay=�6k   5F     �� 5G     $��5,2,5,2q�6   $5 
Horizontal=�6*   .+�5H     A� 
���  ��$��Center=�6%   $��24��6@   $��24��6L   �� 5I     $��高危��6   $��Center=�6&   �� 5J     $��Center=�6   #��[ 6l   > 66   �� *IsHighDangerSignal+$7 TwoWay=�6l   5K     �� 5L     $��5,2,5,2q�6   $5 
Horizontal=�6*   .+�5M     A� 
���  ��$��Center=�6%   $��24��6@   $��24��6L   �� 5N     $��低危��6   $��Center=�6&   �� 5O     $��Center=�6   #��[ 6k   > 66   �� *IsLowDangerSignal+$7 TwoWay=�6k   5P     �� 5Q     $��5,2,5,2q�6   $5 
Horizontal=�6*   .+�5R     A� 
��� ���$��Center=�6,   $��24��6G   $��24��6S   �� 5S     $��低警��6   $��Center=�6&   �� 5T     $��Center=�6   #��[ 6j   > 66   �� *IsLowAlertSignal+$7 TwoWay=�6j   5U     �� 5V     $��5,2,5,2q�6   $5 
Horizontal=�6*   .+�5W     A� 
��� ���$��Center=�6(   $��24��6C   $��24��6O   �� 5X     $��	低预警��6   $��Center=�6'   �� 5Y     $��Center=�6   #��[ 6m   > 66   �� *IsLowPreAlertSignal+$7 TwoWay=�6m   5Z     �� 5[     $��5,2,5,2q�6   $5 
Horizontal=�6*   .+�5\     A� ���	#FF85CBEB$��Center=�6+   $��24��6F   $��24��6R   �� 5]     $��	低正常��6   $��Center=�6'   �� 5^     $��Center=�6   #��[ 6k   > 66   �� *IsLowNormalSignal+$7 TwoWay=�6k   5_     5`       5b  
   $��1��6   $��2��6    $
" None=�60   $��0q�6K   ?   SelectionMode$? Single=�5c  n   @   CanUserAddRows@ .  A   ColumnWidth5  /System.Windows.Controls.DataGridLengthConverter$A * 5d  9   B %� VirtualizationMode$B 	Recycling=�6I   C %� IsVirtualizingC . !D %� IsVirtualizingWhenGroupingD . ! .  E   EnableRowVirtualizationE . !F   EnableColumnVirtualizationF . G   RowDetailsVisibilityMode$G 	Collapsed=�5e     ��. ��5c  "   �� *SignalsView+H �� SelectedItemH 6F   �� *SelectedSignal+$ 5f       5g     $% 报警级别��6)   ' . ( 5h     �� 5i     .R�5j     A� $) ellipse��6&   ���	#FF85CBEB$��Center=�6H   $��16��6e   $��0q�6q   $��16��6|   $��Center=�6�   * 5k     �� 5l  "   $+ Invalid��6W   , 6.   �� $- DelayAlarmGrade�.a�5m  &   �� $ ellipse��6-    � ��
 �����6R   5n  #   �� 5o  "   $+ 
HighNormal��6W   , 6.   �� $- DelayAlarmGrade�.a�5p  &   �� $ ellipse��6-    � �� �	#FF85CBEB6R   5q  #   �� 5r  "   $+ 	LowNormal��6W   , 6.   �� $- DelayAlarmGrade�.a�5s  &   �� $ ellipse��6-    � �� �	#FF85CBEB6R   5t  #   �� 5u  "   $+ 	HighAlert��6W   , 6.   �� $- DelayAlarmGrade�.a�5v  &   �� $ ellipse��6-    � ��
 � ���6R   5w  #   �� 5x  "   $+ LowAlert��6W   , 6.   �� $- DelayAlarmGrade�.a�5y  &   �� $ ellipse��6-    � ��
 � ���6R   5z  #   �� 5{  "   $+ 
HighDanger��6W   , 6.   �� $- DelayAlarmGrade�.a�5|  &   �� $ ellipse��6-    � ��
 �  ��6R   5}  #   �� 5~  "   $+ 	LowDanger��6W   , 6.   �� $- DelayAlarmGrade�.a�5  &   �� $ ellipse��6-    � ��
 �  ��6R   5�  #   5�     5�       5�     $% 测点名称��6%   I   MinWidth$	I 180Q�6Q   ' . . 64   �� *
ItemName+6`     5�     $% 采集时间��6%   $	I 210Q�6v   ' . . 64   �� *ACQDatetime+$/ yyyy/MM/dd HH:mm:ss��6�     5�     $% 	有效值��6%   ' . . 63   �� *Result+$/ F3��6^     5�     $% 单位��6%   ' . . 62   �� *Unit+6K     5�     $% 通道信息��6%   ' . . 64   �� *ChannelHDID+6T     5�     $% 电量��6)   ( 5�     �� 5�     .R�5�     � $��20��6#   .+�5�  "   � J �Name$J ProgressBar��6.   
��� � �$��20��6v   $��40��6�   $	��5 2q�6�   K � ValueK 6C   �� *BatteryEnergy+6�   �� 5�  "   L �� TextAlignment$L Center=�6\   
�������$��Center=�6�   $��Center=�6�   $��16(�6�   ��6,   �� *BatteryEnergy+$/ F0��6�   5�     5�     5�     M �� 
GroupStyleM 5�     (  "System.Windows.Controls.GroupStyle  5�     N   ContainerStyleN 5�     �� 5�     " ��6    .�5�     ��  � �� 5�  "   �� 5�  &   " ��66   .R�5�  *   9� O 9� 
IsExpandedO . $��0,0,0,0q�6E   ��5�  .   �� 5�  2   $5 
Horizontal=�6=   $
��Auto��6V   $��16��6c   .+�5�  6   �� $
��Bold"�6@   $��5,0,0,0q�6m   ��6R   �� $
- Name�6m   5�  3   ��5   .   �� 5  2     AccentColorBrush2#���  69   $��2q�6k   ._�5  6   �� 5  3   5  +   5  '   5     5	     P   PanelP 5     �� 5     .R�5     >  8System.Windows.Controls.Primitives.DataGridRowsPresenter  5     5     Q   ColumnHeaderStyleQ 5     �� 5       MetroDataGridColumnHeader# [ 6   =  7System.Windows.Controls.Primitives.DataGridColumnHeader"  6M   .�5     ��  � ��  AccentColorBrush3# �  63   5     R   	CellStyleR 5     �� 5       MetroDataGridCell# [ 6   *  $System.Windows.Controls.DataGridCell"  5     5     S   RowStyleS 5     �� 5       MetroDataGridRow# [ 6   )  #System.Windows.Controls.DataGridRow"  5      5"     ��5$     �� 5%       MetroDataGrid# [ 6   "  5&     5)     5+     54     55     