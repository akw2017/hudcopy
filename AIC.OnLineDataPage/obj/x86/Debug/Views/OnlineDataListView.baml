   M S B A M L   `   `   `  ����C  ?AIC.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null[;clr-namespace:AIC.Core.OrganizationModels;assembly=AIC.CoreAIC.Core.OrganizationModels  5      T PWpf.PageNavigationControl, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullhJclr-namespace:Wpf.PageNavigationControl;assembly=Wpf.PageNavigationControlWpf.PageNavigationControl 5      I2clr-namespace:AIC.Core.ExCommand;assembly=AIC.CoreAIC.Core.ExCommand  5      M IAIC.OnLineDataPage, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullM+clr-namespace:AIC.OnLineDataPage.ConvertersAIC.OnLineDataPage.Converters 5      O5clr-namespace:AIC.Core.SignalModels;assembly=AIC.CoreAIC.Core.SignalModels  5      5(clr-namespace:AIC.Core;assembly=AIC.CoreAIC.Core  5      G CAIC.CoreType, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullA0clr-namespace:AIC.CoreType;assembly=AIC.CoreTypeAIC.CoreType 5      C&clr-namespace:AIC.OnLineDataPage.ViewsAIC.OnLineDataPage.Views 5      1   +AIC.OnLineDataPage.Views.OnlineDataListView   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      c _System.Windows.Interactivity, Version=4.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity  5      Icontrol;clr-namespace:AIC.Core.OrganizationModels;assembly=AIC.Core  5      U	 QMahApps.Metro, Version=1.5.0.23, Culture=neutral, PublicKeyToken=f4fb5a3c4d1e5b4f;Controls,http://metro.mahapps.com/winfx/xaml/controls 	 5      P
 LPrism.Wpf, Version=6.2.0.0, Culture=neutral, PublicKeyToken=91a96d2a154366d8$prismhttp://prismlibrary.com/ 
 5	      UpageJclr-namespace:Wpf.PageNavigationControl;assembly=Wpf.PageNavigationControl 
 5      @command2clr-namespace:AIC.Core.ExCommand;assembly=AIC.Core 
 5      ;	converter+clr-namespace:AIC.OnLineDataPage.Converters 
 5      F BItemsFilter, Version=1.1.0.1, Culture=neutral, PublicKeyToken=nullGbsFilter8http://schemas.bolapansoft.com/xaml/Controls/ItemsFilter  5      Bsignal5clr-namespace:AIC.Core.SignalModels;assembly=AIC.Core  5      3core(clr-namespace:AIC.Core;assembly=AIC.Core  5      ?coreType0clr-namespace:AIC.CoreType;assembly=AIC.CoreType  5      1view&clr-namespace:AIC.OnLineDataPage.Views  5      ! 
 Prism.Mvvm.ViewModelLocator    AutoWireViewModel  .  � 	Resources 5       �� MergedDictionaries �� Source>  8AIC.OnLineDataPage.Converters.IntToAlarmTypeStrConverter   intToAlarmTypeStrConverter$   AIC.Core.EqualBindingConverter  equalBindingConverter%   AIC.Core.GradeToStringConverter  gradeToStringConverter*   $AIC.Core.AlarmLimitToStringConverter  alarmLimitToStringConverter   AIC.Core.TypeOfConverter  typeOfConverter    AIC.Core.IsDivfreConverter  isDivfreConverter(   "AIC.Core.VisibilityToBoolConverter  visibilityToBoolConverter(	   "AIC.Core.BoolToVisibilityConverter  boolToVisibilityConverter'
   !AIC.Core.EnumDescriptionConverter  enumDescriptionConverter 	 expandToggleStyle �� 
TargetType �� BasedOn �� Property �� Value 
 DarkAccentColorBrush �� 
TargetType'  !System.Windows.VisualStateManager	   VisualStateGroups%  System.Windows.VisualStateGroup
   Transitions%  System.Windows.VisualTransition   GeneratedDuration   States   System.Windows.VisualState   
Storyboard g� Duration	 W� To �� TargetProperty �� 
TargetName F�Name �� Stretch J� Opacity � UseLayoutRounding J� RenderTransformOrigin �� Angle �� Triggers P� Property P� Value  HeiachyTemplate&   System.Windows.Controls.DataGrid   
IsReadOnly   AutoGenerateColumns   GridLinesVisibility   RowHeaderWidth   Columns4  .System.Windows.Controls.DataGridTemplateColumn,  &System.Windows.Controls.DataGridColumn    Header!   SortMemberPath"   
IsReadOnly#   CellTemplate$ A�Name% �� Triggers& �� Value' �� Binding( �� Path) �� 
TargetName0  *System.Windows.Controls.DataGridTextColumn1  +System.Windows.Controls.DataGridBoundColumn*   Binding+ �� StringFormat  enumComboItemTemplate, �� 	Converter�� 5   
    5      �� 5      $4 ./AIC.Resources;component/Themes/ScrollBar.xaml?�6%   %  &	        &	      &	 &     &	 9     &	 L     &	 _     &	 r     &	 �     &	 �     &		 �     7d�&	 �    &	 @    7    5      63     5      6)     5      6*     5      6/     5      6#     5      6%     5       6-   	  5!      6-   
  5"      6,   �� 5#      " d�6.   8   .�5$      ��  �@�Cursor$
 Hand��6+   �� 5%       � ��# � 
 6/   �� 5&       � ��
 ���� 6/   �� 5'       � �� 5(      �� 5)      " d�6*   .R�5*      � 	 5+   "     5,   &   CommonStates��
 5-   *     5.   .   $ 0:0:0.1D�6?   . 50   *     
Normal��66     51   *   	MouseOver��. 52   .   �� .��53   2   W� $ 0D�6B   $ 1X�6O   $ (UIElement.Opacity)�6V   $ normal��6�   54   /   55   +     56   *   Pressed��. 57   .   �� .��58   2   W� $ 0D�6B   $ 1X�6O   $ (UIElement.Opacity)�6V   $ normal��6�   59   /   5:   +     5;   *   Disabled��. 5<   .   �� .��5=   2   W� $ 0D�6B   $	 0.3X�6O   $ (UIElement.Opacity)�6X   $ normal��6�   5>   /   5?   +   5@   '     5A   &   CheckStates��. 5B   *     Checked��. 5C   .   �� .��5D   2   W� $ 0:0:0.3D�6B   $	 180X�6U   $ Angle�6^   $ symbolRotation��6�   5E   /   5F   +     5G   *   	Unchecked��. 5H   .   �� .��5I   2   W� $ 0:0:0.3D�6B   $ 90X�6U   $ Angle�6]   $ symbolRotation��6   5J   /   5K   +     5L   *   Indeterminate��66   5M   '     5N   &   FocusStates��. 5O   *     Focused��66     5P   *   	Unfocused��66   5Q   '   .+�5S   "   �� $��2��6)   $��3��66   #��z��6K   #��z��6u   #��z��6�   F� 5T   "   $ normal��6'   $��Center=�67   $��10��6T   $
 Fill=�6_   $	 0.6Q�6n   �����@B ��[ �@B ��[ �ا �ا �@B �ا �pr ����n@�pr ����n@S�pr ����n@��� �ا ��� �ا Ӏ�� �ا ��� ��[ ��� ��[ Ӏ�� ��[ �pr P�^����?�pr P�^����?S�pr P�^����?��[ ��[ 	#��z��6    .  $��Center=�6^  $��7��6y  $ 0.5,0.5&�6�  z�5U   &   V� 5W   *   .~�5X   .   �� �� 5Y   .   �� 5Z   .   symbolRotation��$ 90Q�6V   T� 5[   .   5\   +   5^   #   5_       5`      P� 5a   "    �@d�	IsChecked$
 trueY�6?   . �5b   &   ��  � ��$	 Top=�6J   5c   #   5e      5g      5h      �� 5i      .R�5j        $��0q�6;   
������ $��0q�6_    .  .  $
 None=�6�   $ 0Q�5k      ��5j      �� *	DivFres+��6s   Z�  5l        5m      $  报警级别��61   $! DelayAlarmGrade��6?   " . # 5n      �� 5o   "   .R�5p   &   A� $$ ellipse��6.   ���	#FF85CBEB$��Center=�6P   $��12��6m   $��0q�6y   $��12��6�   $��Center=�6�   % 5q   &   �� 5r   *   $& Invalid��6_   ' 66   �� $( DelayAlarmGrade�.a�5s   .   �� $) ellipse��65    � ��
 ��i��6Z   5t   +   �� 5u   *   $& 	HighAlarm��6_   ' 66   �� $( DelayAlarmGrade�.a�5v   .   �� $) ellipse��65    � ��
 � ���6Z   5w   +   �� 5x   *   $& LowAlarm��6_   ' 66   �� $( DelayAlarmGrade�.a�5y   .   �� $) ellipse��65    � ��
 � ���6Z   5z   +   �� 5{   *   $& 
HighDanger��6_   ' 66   �� $( DelayAlarmGrade�.a�5|   .   �� $) ellipse��65    � ��
 �  ��6Z   5}   +   �� 5~   *   $& 	LowDanger��6_   ' 66   �� $( DelayAlarmGrade�.a�5   .   �� $) ellipse��65    � ��
 �  ��6Z   5�   +   5�   #   5�        5�      $  分频名称��6-   " . * 6;   �� *DisPlayName+6[     5�      $  采集时间��6-   " . * 6<   �� *ACQDatetime+$+ yyyy-MM-dd HH:mm:ss��6~     5�      $  频率��6-   " . * 6:   �� *FreV+$+ F3��6c     5�      $  幅值��6-   " . * 6:   �� *FreMV+$+ F3��6d     5�      $  相位��6-   " . * 6:   �� *Phase+$+ F1��6d     5�      $  高危��6-   " . * 6:   �� *
HighDanger+$+ F3��6i     5�      $  高警��6-   " . * 6:   �� *	HighAlarm+$+ F3��6h     5�      $  正常(高)��6-   " . * 6=   �� *
HighNormal+$+ F3��6l     5�      $  正常(低)��6-   " . * 6=   �� *	LowNormal+$+ F3��6k     5�      $  低警��6-   " . * 6:   �� *
LowAlarm+$+ F3��6g     5�      $  低危��6-   " . * 6:   �� *	LowDanger+$+ F3��6h     5�      $  转速��6-   " . * 6:   �� *RPM+$+ F1��6b     5�      $  默认转速��6-   " . * 6<   �� *
DefaultR+6Y     5�      $  基础频率1��6-   " . * 6=   �� *
Base1Fre+6Z     5�      $  基础频率2��6-   " . * 6=   �� *
Base2Fre+6Z     5�      $  最大频率数��6-   " . * 6<   �� *	MaxFreNum+$+ F3��6j     5�      $  倍频��6-   " . * 6:   �� *
MultiFre+6W     5�      $  特征频率��6-   " . * 6<   �� *CharacteristicFre+6b     5�      $  浮动百分比��6-   " . * 6=   �� *Base1FrePercent+$+ F3��6r   5�      5�      �� 5�      .R�5�      �� ��6   �� $( .�8,   5�      5�      .��5�      � -   5        $��grdWorkbench��5�      - � RowDefinitions- 5�   
   �� 5�      $
��Auto �6   �� 5�      $��3* �6   �� 5�      $
��Auto �6   . � ColumnDefinitions. 5�   
   �� 5�      $
��Auto �6   �� 5�      $
��Auto �6   �� 5�      $��* �6   .+�5�   
   R� -   5        / R�Name$/ treeview��5�   1   $��0��6   $��3��6      VirtualisedMetroTreeView#���  5�      ��5�      �� *OrganizationTreeItems+ 5�      0 �� DataType?   9AIC.Core.OrganizationModels.OrganizationTreeItemViewModel1 �� ItemsSource2 �� Orientation  AccentColorBrush3 ��Name4 �� Mode9   3AIC.Core.OrganizationModels.DeviceTreeItemViewModel��%�  (��       5�   +   *, +)(�� [    5�   +   *, +)�� 5�      "0  6+   1 6e   �� $( Children�.R�5�      �� $2 
Horizontal=�6!   .+�5�      F� $ IconPath��6   $��18��61   $��18��6<   $
 Fill=�6H   #���  6W   ������Bnx�V��Bn4�Cќ�O�ob�L�!�!�4�C�71�ܽ�,���ܽ���K�����z@����H��р�e�!��8�obф�4�Cф�x�V��Bnx�V�4�C�Śј��d%ј�(�(��ǘ���@@(�(��ǘ���@@����08�����08�(�(����(�(�����d%�4�C�Ś6�   � 5�      $��2,0,2,0q�6   .+�5�      �� $3 txtName��6(   $
��Auto��6\   ��69   �� *Name+$4 TwoWay=�6\   5�      5�      % 5�      �� 5�      $& 0��6O   ' 6&   �� *T_Organization.Level+.a�5�      �� $) IconPath��6%    � ��� �� ��:�����:����pej� �pej� ��:���2��x��2��x����Ѥ����Ѥ��2�� ����є�����'t���������������E�'t�E�'t0P�є�0P�є��E� ��E� ��������EѤ����+�Tn5��+�����+�x����+�x���E����E����x��Tn5�x��Tn5�EѤ���E�$� �E�$� �x��$� l't�T�-���\��l't�\���x��\���EѬ<R�EѬ<R�x��Ԙ� �x��Ԙ� �E�$� �E���-���������%����,�U���-�P��V�� �h�V�� P�7���-���6K   �� 5�      $) IconPath��6%    � ��$ 24��6L   5�      �� 5�      $& Invalid��6@   ' 6&   �� *Alarm+.a�5�      �� $) txtName��6%    � ��
 ��i��6P   5�      �� 5�      $& HighPreAlarm��6@   ' 6&   �� *Alarm+.a�5�      �� $) txtName��6%    � ��
 � ���6P   5�      �� 5�      $& LowPreAlarm��6@   ' 6&   �� *Alarm+.a�5�      �� $) txtName��6%    � ��
 � ���6P   5�      �� 5�      $& 	HighAlarm��6@   ' 6&   �� *Alarm+.a�5�      �� $) txtName��6%    � ��
 � ���6P   5�      �� 5�      $& LowAlarm��6@   ' 6&   �� *Alarm+.a�5�      �� $) txtName��6%    � ��
 � ���6P   5�      �� 5�      $& 
HighDanger��6@   ' 6&   �� *Alarm+.a�5�      �� $) txtName��6%    � ��
 �  ��6P   5�      �� 5�      $& 	LowDanger��6@   ' 6&   �� *Alarm+.a�5�      �� $) txtName��6%    � ��
 �  ��6P   5�      5�      �� 5�      "0  6+   .R�5�      �� $2 
Horizontal=�6!   .+�5�      F� $��18��6   $��18��6*   $
 Fill=�66   #���  6E   �����P�x��ݩ(�� ���� p׻\r� $�� �(4��0�	�^)�@@�5������@@�.>�A�G�!اQ���P�x�ز�ݩ������y@@�����p׻�:pΈ"@@$��pP�(4�Ql^�	�^)�@@SP=������@@���A(Ċ�!�L�c����2ز�S�U)�����y@@�� ����d�:pΈ"@@��7!P�Ѵ�� l^����� P=�d*� ��PJ� �Ê��L�c�(�� �2��� �U)\r� �� � �dѐ0�7!є5P�� Ӱ.>��� �Gd*� اQPJ� �P�x�� 6n -1Ӵ�L -1 -1��L -1 6n� -1L���L ?� 6n ?��L� ?� ?�L� ?� 6n� ?���LL� -1 6n -16o   �� 5�      $��2,0,2,0q�6%   $2 
Horizontal=�66   .+�5�      �� $3 txtName��6(   $
��Auto��6\   ��69   �� *Name+$4 TwoWay=�6\   �� 5�      $3 txtRun��6(   
��� � �$��:运行��6K   �� 5�      $3 txtStop��6(   
���  ��$��:停止��6J   $y�	Collapsed=�6U   5�      5�      % 5�      �� 5�      $& Invalid��6@   ' 6&   �� *Alarm+.a�5�      �� $) txtName��6%    � ��
 ��i��6P   5�      �� 5�      $& false��6D   ' 6&   �� *	IsRunning+.a�5�      �� $) txtRun��6%    � y�$ 	Collapsed=�6O   �� 5�      $) txtStop��6%    � y�$ Visible=�6P   5�      �� 5�      $& HighPreAlarm��6@   ' 6&   �� *Alarm+.a�5�      �� $) txtName��6%    � ��
 � ���6P   5�      �� 5�      $& LowPreAlarm��6@   ' 6&   �� *Alarm+.a�5�      �� $) txtName��6%    � ��
 � ���6P   5�      �� 5�      $& 	HighAlarm��6@   ' 6&   �� *Alarm+.a�5�      �� $) txtName��6%    � ��
 � ���6P   5�      �� 5�      $& LowAlarm��6@   ' 6&   �� *Alarm+.a�5�      �� $) txtName��6%    � ��
 � ���6P   5�      �� 5�      $& 
HighDanger��6@   ' 6&   �� *Alarm+.a�5�      �� $) txtName��6%    � ��
 �  ��6P   5�      �� 5�      $& 	LowDanger��6@   ' 6&   �� *Alarm+.a�5�      �� $) txtName��6%    � ��
 �  ��6P   5�      5�      5      ��5     �� 5     " Q�6     LineTreeViewItem# [ 62   .�5     ��  �@Q�
IsSelected 63   �� *
IsSelected+$4 OneTime=��� 5      �@Q�
IsExpanded 63   �� *
IsExpanded+$4 OneTime=�5     .  (System.Windows.Interactivity.Interaction5   Triggers5 5	     /  )System.Windows.Interactivity.EventTrigger  5
     6   	EventName$6 SelectedItemChanged��6!   .  (System.Windows.Interactivity.TriggerBase7   Actions.7 5     6  0System.Windows.Interactivity.InvokeCommandAction  8   Command8 6,   �� *SelectedTreeChangedComamnd+9   CommandParameter9 6[   �� *SelectedItem+: �� ElementName$: treeview��5     5     �� 5  
   -   5        ; ��Name$; gsSplitterr��5     $��0��6,   $��3��69   $��1��6J   $��3��6Z   #���  6d   $��Center=�6�   $��Stretch=�6�     GridSplitterStyleExpend#��[ 6�   �� 5  
   $��0��6   $��2��6   < �� Padding$< 5q�6.     AccentColorBrush2#���  6:   ._�5     � .+�5     �� $2 
Horizontal=�6   .+�5     �� $��报警级别:��6    $��Center=�6-   $��2,0,2,0q�6H   �� 5     $��5,2,5,2q�6!   $2 
Horizontal=�62   .+�5     �� $��无效��6$   $��Center=�6.   �� 5     $��Center=�6C   $��2,0,2,0q�6^   ��6$   �� *AbnormalCount+6^   �� 5     $��Center=�6#   
����i��  CheckBoxStyle#��[ 6�   = d� 	IsChecked= 6T   �� *IsInvalidSignal+$4 TwoWay=�6�   5     �� 5     $��5,2,5,2q�6!   $2 
Horizontal=�62   .+�5     �� $��正常��6$   $��Center=�6.   �� 5      $��Center=�6A   $��2,0,2,0q�6\   ��6$   �� *NormalCount+6\   �� 5!     $��Center=�6#     	TextBrush#���  6>   #��[ 6�   = 6h   �� *IsNormalSignal+$4 TwoWay=�6�   5"     �� 5#     $��5,2,5,2q�6!   $2 
Horizontal=�62   .+�5%     �� $��预警��6$   $��Center=�6.   �� 5&     $��Center=�6C   $��2,0,2,0q�6^   ��6$   �� *PreAlertCount+6^   �� 5'     $��Center=�6#   
��� ���#��[ 6�   = 6S   �� *IsPreAlarmSignal+$4 TwoWay=�6�   5(     �� 5)     $��5,2,5,2q�6!   $2 
Horizontal=�62   .+�5+     �� $��警告��6$   $��Center=�6.   �� 5,     $��Center=�6@   $��2,0,2,0q�6[   ��6$   �� *
AlertCount+6[   �� 5-     $��Center=�6#   
��� ���#��[ 6�   = 6W   �� *IsAlarmSignal+$4 TwoWay=�6�   5.     �� 5/     $��5,2,5,2q�6!   $2 
Horizontal=�62   .+�51     �� $��危险��6$   $��Center=�6.   �� 52     $��Center=�6A   $��2,0,2,0q�6\   ��6$   �� *DangerCount+6\   �� 53     $��Center=�6#   
���  ��#��[ 6�   = 6P   �� *IsDangerSignal+$4 TwoWay=�6�   54     �� 55     $��5,2,5,2q�6!   $2 
Horizontal=�62   .+�57     �� $��断线��6$   $��Center=�6.   �� 58     $��Center=�6D   $��2,0,2,0q�6_   ��6$   �� *UnConnectCount+6_   �� 59     $��Center=�6#   
���  ��#��[ 6�   = 6T   �� *DisConnectSignal+$4 TwoWay=�6�   5:     5;     �� 5=     $2 
Horizontal=�6   $��Right=�66   .+�5>     �� $��Center=�6   $
= TrueY�6:   $��分组显示��6K   #��[ 6[   5 5?       5@     $6 Checked��6-   .7 5A  "     8 68   �� *GroupViewCommand+5B       5C     $6 	Unchecked��6-   .7 5D  "     8 68   �� *UnGroupViewCommand+5E     5G     5H     5I     5J       5L  
   -   5        >  Name$> gridview��5L     $��1��6%   $��2��62   $��0,1,0,0q�6�   ? %� IsVirtualizing? . @ %� VirtualizationMode$@ 	Recycling=�5M  Q   !A %� IsVirtualizingWhenGroupingA . ��. $
 None=�6�   B   EnableRowVirtualizationB .  .  C   CanUserAddRowsC .  ��5L  B   �� *SignalsView+D �� SelectedItemD 6f   �� *SelectedSignal+ 5N       defaultGroup��%<   &	       �� 5O     " ��6"   .R�5P     �� 5Q     5R      5S       5V     $  报警级别��6%   " . * 63   �� *DelayAlarmGrade+#, [ 6�     5W     $  测点名称��6%   " . * 64   �� *
ItemName+6Q     5X     $  采集时间��6%   " . * 64   �� *ACQDatetime+$+ yyyy-MM-dd HH:mm:ss��6v     5Y     $  数值��6%   " . * 62   �� *Result+$+ F3��6]     5Z     $  单位��6%   " . * 62   �� *Unit+6K     5[     $  传感器地址��6%   " . * 65   �� *ChannelHDID+6U     5\     $  报警信息��6%   " . * 64   �� *AlarmLimitString+6Y     5x     $  传感器电量��6)   # 5y     �� 5z     .R�5{     � .+�5|  "   � E �Name$E ProgressBar��6.   
��� � �$��20��6v   $��40��6�   $	��5 2q�6�   F � ValueF 6C   �� *BatteryEnergy+6�   �� 5}  "   G �� TextAlignment$G Center=�6\   
�������$��Center=�6�   $��Center=�6�   $��16(�6�   ��6,   �� *BatteryEnergy+$+ F0��6�   5~     5     5�     H �� 
GroupStyleH 5�     (  "System.Windows.Controls.GroupStyle  5�     I   ContainerStyleI 5�     �� 5�     " ��6    .�5�     ��  � �� 5�  "   �� 5�  &   " ��66   .R�5�  *   9� $��0,0,0,0q�63   ��5�  .   �� 5�  2   $2 
Horizontal=�6=   $
��Auto��6V   $��24��6c   .+�5�  6   �� $
��Bold"�6@   $��5,0,5,0q�6m   ��6R   �� $
( Name�6m   �� 5�  6   $
��Bold"�6@   $��:��6R   �� 5�  6   $
��Bold"�6@   $��5,0,5,0q�6r   ��6R   �� $( 	ItemCount�6r   5�  3   ��5�  .   �� 5�  2   #���  69   $��2q�6k   ._�5�  6   �� 5�  3   ��5�  .   �� 5�  2   " 9�68   .�5�  6   ��  �@9�
IsExpanded .  6S   J �� TriggersJ 5�  6   �� 5�  :   & True6F   ' 5�  >   `� 5�  B   K `� 	Converter#K [ 6O   .1�5�  F   �� $
( Name�6N   �� 5�  F   $( DataContext.FirstName�6N   L �� RelativeSourceL 6k   �� M �� Mode$M FindAncestor=�N �� AncestorType$N UserControlL�5�  C   .a�5�  >   ��  �@9�
IsExpanded . 6[   5�  ;   5�  3   5�  +   5�  '   5�     J 5�     �� 5�  "   ' 6.   �� $
( Name�&  6L   .a�5�  &   ��  � ��# [ 6A   5�  #   5�     O   PanelO 5     �� 5     .R�5     >  8System.Windows.Controls.Primitives.DataGridRowsPresenter  5     5     P   ColumnHeaderStyleP 5     �� 5	       MetroDataGridColumnHeader# [ 6   =  7System.Windows.Controls.Primitives.DataGridColumnHeader"  6M   .�5
     ��  � ��  AccentColorBrush3# �  63   5     Q   	CellStyleQ 5     �� 5       MetroDataGridCell# [ 6   *  $System.Windows.Controls.DataGridCell"  5     .�5     ��  � ��$ 0,0,20,0q�6/   5     R   RowStyleR 5     �� 5       MetroDataGridRow# [ 6   )  #System.Windows.Controls.DataGridRow"  5     J 5     �� 5     ' 6&   �� $( DelayAlarmGrade�& Invalid6O   .a�5     ��  � ��
 ��i��6<   5     �� 5     ' 6&   �� $( DelayAlarmGrade�& HighPreAlarm6O   .a�5     ��  � ��
 � ���6;   5     �� 5     ' 6&   �� $( DelayAlarmGrade�& LowPreAlarm6O   .a�5     ��  � ��
 � ���6;   5      �� 5!     ' 6&   �� $( DelayAlarmGrade�& 	HighAlarm6O   .a�5"     ��  � ��
 � ���6;   5#     �� 5$     ' 6&   �� $( DelayAlarmGrade�& LowAlarm6O   .a�5%     ��  � ��
 � ���6;   5&     �� 5'     ' 6&   �� $( DelayAlarmGrade�& 
HighDanger6O   .a�5(     ��  � ��
 �  ��6;   5)     �� 5*     ' 6&   �� $( DelayAlarmGrade�& 	LowDanger6O   .a�5+     ��  � ��
 �  ��6;   5,     �� 5-     ' 6&   �� $( DelayAlarmGrade�& 
DisConnect6O   .a�5.     ��  � ��
 �  ��6;   5/     51     ��53     �� 54       MetroDataGrid# [ 6   "  55     .�56     ��   �@ AlternatingRowBackground  AccentColorBrush4# �  57     58     5 5:       5;     $6 SelectionChanged��6!   .7 5<       8 6,   �� * SelectedDataGridChangedComamnd+9 6_   �� *SelectedItem+$: gridview��5=     5?     � 5A  
   $��2��6   $��2��6   .+�5B     9� ��6   �� *DiagnosticInfo+��69   �� *DiagnosticAdvice+6`   5C     5D     5E     