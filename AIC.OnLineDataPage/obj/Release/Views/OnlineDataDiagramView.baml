   M S B A M L   `   `   `  ���� M  IAIC.OnLineDataPage, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullC&clr-namespace:AIC.OnLineDataPage.ViewsAIC.OnLineDataPage.Views  I)clr-namespace:AIC.OnLineDataPage.ControlsAIC.OnLineDataPage.Controls  J FDiagramDesigner, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullJ6clr-namespace:DiagramDesigner;assembly=DiagramDesignerDiagramDesigner C ?AIC.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5(clr-namespace:AIC.Core;assembly=AIC.CoreAIC.Core G CAIC.CoreType, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullA0clr-namespace:AIC.CoreType;assembly=AIC.CoreTypeAIC.CoreType [;clr-namespace:AIC.Core.OrganizationModels;assembly=AIC.CoreAIC.Core.OrganizationModels E AAIC.Domain, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;,clr-namespace:AIC.Domain;assembly=AIC.Domain
AIC.Domain I2clr-namespace:AIC.Core.ExCommand;assembly=AIC.CoreAIC.Core.ExCommand E'clr-namespace:AIC.OnLineDataPage.ModelsAIC.OnLineDataPage.Models  4    .AIC.OnLineDataPage.Views.OnlineDataDiagramView   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     8local&clr-namespace:AIC.OnLineDataPage.Views     c	 _System.Windows.Interactivity, Version=4.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity 	 =localControls)clr-namespace:AIC.OnLineDataPage.Controls 	 h
 dMicrosoft.Expression.Interactions, Version=4.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bei9http://schemas.microsoft.com/expression/2010/interactions 
 ?dd6clr-namespace:DiagramDesigner;assembly=DiagramDesigner 
 3core(clr-namespace:AIC.Core;assembly=AIC.Core 
 ?coreType0clr-namespace:AIC.CoreType;assembly=AIC.CoreType 
 Icontrol;clr-namespace:AIC.Core.OrganizationModels;assembly=AIC.Core 
 9domain,clr-namespace:AIC.Domain;assembly=AIC.Domain 
 P LPrism.Wpf, Version=6.2.0.0, Culture=neutral, PublicKeyToken=91a96d2a154366d8$prismhttp://prismlibrary.com/  @command2clr-namespace:AIC.Core.ExCommand;assembly=AIC.Core  X TXceed.Wpf.Toolkit, Version=3.2.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit  4models'clr-namespace:AIC.OnLineDataPage.Models  !  Prism.Mvvm.ViewModelLocator    AutoWireViewModel  .  � 	Resources  �� MergedDictionaries �� Source)  #AIC.Core.ExCommand.CommandReference   AddCommandReference   Command9   3AIC.OnLineDataPage.Models.ValueTypeTemplateSelector  valueTypeTemplateSelector  defaultValueTempalte   DefaultTemplate  imageValueTemplate   ImageTemplate#  AIC.Core.EnumBooleanConverter  enumBooleanConverter(  "AIC.Core.EnumToVisibilityConverter  enumToVisibilityConverter   Reverse  AIC.Core.NegateConverter  negateConverter  AIC.Core.BindingProxy 	 proxy   Data  DragGripperStyle	 �� 
TargetType
 �� Property �� Value �� 
TargetType �� Padding 	 ItemContainerStyle/   )AIC.OnLineDataPage.Controls.ResizableItem �Name ��Name J� SnapsToDevicePixels n�Name �� Triggers P� Property P� Value 
 ItemsControlStyle  DefaultErrorTemplate �� Orientation �� CornerRadius �Name K� ZIndex � Points J� IsHitTestVisible � Points �� StrokeEndLineCap �� StrokeStartLineCap ��Name ^� InitialShowDelay  � ToolTip! �� ElementName" �� Path$	  Xceed.Wpf.Toolkit.DoubleUpDown��  �� $5 //AIC.Resources;component/Themes/ColorStyle.xaml?��� $8 2/AIC.Resources;component/Themes/CheckBoxStyle.xaml?��� $I C/AIC.OnLineDataPage;component/Themes/OnlineDataDiagramTemplate.xaml?��� $4 ./AIC.Resources;component/Themes/ScrollBar.xaml?�%  &	        &	      7  7  &	 ,     &	 1     &	 =     &	 B     &	 P     &		 �     7  &	
 S    &	 3    '	  `    7     �� *
AddCommand+  8   8       .       �� �� "	 n�.��� 
 � ��$ 8���� 
 � ��
 ���� �� 
 �@�Cursor$ SizeNS���� 
 � �� �� " n�.R��� #��z��# z���� "	  .��� 
 � ��$ 0,2,0,0q��� 
 � ��$ 20���� 
 � ��$	 228���� 
 � ��$ Stretch=��� 
 � ��$ Stretch=��� 
 � �� �� "  .R�� $ rootGrid��.+��� $ Bd��#��z��#��z��#��z��# z�� . ._��� #��z��# z #��z��n� $ PART_DragThumb��8��  $��Stretch=�$��Bottom=� P�  � {� .  . ��� # � ��
 � ���� "	 ��.��� 
 � ��$
 Auto=��� 
 � ��$
 Auto=��� 
 � �� . �� 
 �@��PanningMode$
 Both=��� 
 � �� �� " ��.R��� #��z��#��z��#��z��# z�� . ._��� |�.  #��z��.���� # z �� .R��� $ 
Horizontal=�.+��� $��1q����	#FFdc000c$	 0.7��$	��Top=�._�� .+�� $ toolTipCorner��$ 2��$��-1q� �   ��[ ��[ ��[ ���	#FFdc000c$��Right=�$	��Top=� . � $ 3�� �   ��j ��j $��-1q�$��Right=�$	��Top=� . $	��1.5��$ Round=�$ Round=�
��������� $ placeholder���� 
���  ��$��3q�$ 10��$��20��$��20��$	��Top=�$ 0��  �� $! placeholder��$8" 2AdornedElement.(Validation.Errors)[0].ErrorContent�._��� $��!��
�������$��Center=�$��Center=��� "	 	 .��� 
 �@=�ErrorTemplate8   .��� -   $��grdWorkbench��# � RowDefinitions# �� $
��Auto ��� $��* �$ � ColumnDefinitions$ �� $
��Auto ��� $
��Auto ��� $��* ��� $
��Auto ��� $��* �.+�R� -   % R�Name$% treeview��$��0��$��2��  VirtualisedMetroTreeView#���  ���� *OrganizationTreeItems+ & �� DataType?
  9AIC.Core.OrganizationModels.OrganizationTreeItemViewModel' �� ItemsSource( F�Name) �� Stretch  AccentColorBrush* ��Name+ �� Mode, �� Triggers- �� Value. �� Binding/ �� 
TargetName9  3AIC.Core.OrganizationModels.DeviceTreeItemViewModel0 ��Name7  1AIC.Core.OrganizationModels.ItemTreeItemViewModel9  3AIC.Core.OrganizationModels.DivFreTreeItemViewModel��%�  (��       *,
 +)(�� 9    *, +)7   (��     *, +)(�� �    *, +)�� "& 
 ' �� $" Children�.R��� $ 
Horizontal=�.+�F� $( IconPath��$��18��$��18��$
) Fill=�#���  ������Bnx�V��Bn4�Cќ�O�ob�L�!�!�4�C�71�ܽ�,���ܽ���K�����z@����H��р�e�!��8�obф�4�Cф�x�V��Bnx�V�4�C�Śј��d%ј�(�(��ǘ���@@(�(��ǘ���@@����08�����08�(�(����(�(�����d%�4�C�Ś� $��2,0,2,0q�.+��� $* txtName��$
��Auto������ *Name+$+ TwoWay=�, �� $- 0��. �� *T_Organization.Level+.a��� $/ IconPath��
 � ��� �� ��:�����:����pej� �pej� ��:���2��x��2��x����Ѥ����Ѥ��2�� ����є�����'t���������������E�'t�E�'t0P�є�0P�є��E� ��E� ��������EѤ����+�Tn5��+�����+�x����+�x���E����E����x��Tn5�x��Tn5�EѤ���E�$� �E�$� �x��$� l't�T�-���\��l't�\���x��\���EѬ<R�EѬ<R�x��Ԙ� �x��Ԙ� �E�$� �E���-���������%����,�U���-�P��V�� �h�V�� P�7���-����� $/ IconPath��
 � ��$ 24���� $- Invalid��. �� *Alarm+.a��� $/ txtName��
 � ��
 ��i���� $- HighPreAlarm��. �� *Alarm+.a��� $/ txtName��
 � ��
 � ����� $- LowPreAlarm��. �� *Alarm+.a��� $/ txtName��
 � ��
 � ����� $- 	HighAlarm��. �� *Alarm+.a��� $/ txtName��
 � ��
 � ����� $- LowAlarm��. �� *Alarm+.a��� $/ txtName��
 � ��
 � ����� $- 
HighDanger��. �� *Alarm+.a��� $/ txtName��
 � ��
 �  ���� $- 	LowDanger��. �� *Alarm+.a��� $/ txtName��
 � ��
 �  ���� "&  ' �� $" Children�.R��� $ 
Horizontal=�.+�F� $��18��$��18��$
) Fill=�#���  �����P�x��ݩ(�� ���� p׻\r� $�� �(4��0�	�^)�@@�5������@@�.>�A�G�!اQ���P�x�ز�ݩ������y@@�����p׻�:pΈ"@@$��pP�(4�Ql^�	�^)�@@SP=������@@���A(Ċ�!�L�c����2ز�S�U)�����y@@�� ����d�:pΈ"@@��7!P�Ѵ�� l^����� P=�d*� ��PJ� �Ê��L�c�(�� �2��� �U)\r� �� � �dѐ0�7!є5P�� Ӱ.>��� �Gd*� اQPJ� �P�x�� 6n -1Ӵ�L -1 -1��L -1 6n� -1L���L ?� 6n ?��L� ?� ?�L� ?� 6n� ?���LL� -1 6n -1� $��2,0,2,0q�.+��� $* txtName��$
��Auto������ *Name+$+ TwoWay=��� $0 AddBtn��8��  $��Center=�$��24��$��24��$��0q����� .��F� $
) Fill=�$��18��$��18��#���  F����F���C�ଔTn5���7Tn5�Tn5��C���7�<R�ଔ�<R�F���C, �� $- Invalid��. �� *Alarm+.a��� $/ txtName��
 � ��
 ��i���� $- HighPreAlarm��. �� *Alarm+.a��� $/ txtName��
 � ��
 � ����� $- LowPreAlarm��. �� *Alarm+.a��� $/ txtName��
 � ��
 � ����� $- 	HighAlarm��. �� *Alarm+.a��� $/ txtName��
 � ��
 � ����� $- LowAlarm��. �� *Alarm+.a��� $/ txtName��
 � ��
 � ����� $- 
HighDanger��. �� *Alarm+.a��� $/ txtName��
 � ��
 �  ���� $- 	LowDanger��. �� *Alarm+.a��� $/ txtName��
 � ��
 �  ���� "&  ' �� $" Children�.R��� $ 
Horizontal=�.+�F� $��18��$��18��$
) Fill=�#���  ������ا  � �@]�  � �@]� ���@�4���@�4 Q%��ا  Q%��ا  � ���� @��р�� ���� -1��^�@������@�4@]� �@�4@fр������ -1���р�� @��� $��2,0,2,0q�.+��� $* txtName��$
��Auto������ *Name+$+ TwoWay=�F� $( DisconnectIcon��$y�	Collapsed=�$��18��$��18��$
) Fill=�
������������< �K��H2(�T�)�:K)��;H2H2���;)�:K)(�T�2�K�< ����,@@�vW��K@�\�*����ф��@+��@�\��K�vW��,@@< �K�p��_)���@@���*̾;p40"�Ċ��_��T+U� 3���^p<��^�KT+U�+U��K��^p<��^� 3�+U��_�Ċ��"�p40h�;$�*��|����2,k��x
����l��쎂l����kx
�u]ӄ1�NO�#-�NO�J;�u]�` f@+���*�` fѨu]�J;ӜNO�#-�NO�1�u]�
S��k�ʡE��@@����ʡE��@@����
�,k��2Q��_)���@@F� $( 
UnBindIcon��$y�	Collapsed=�$��16��$��16��$
) Fill=�
�������������HR|�+���|�+�|��<C��|��Āx�&�L�!l�CL�!�䫮L�!\7Āx\7<C���<C���������Trjl�CTrj��/Trj�������<C�ф��+Ѡ�5|�+��5��e��HR��e��HR|�+, �� $- false��. �� *IsConnected+.a��� $/ DisconnectIcon��
 � y�$ Visible=��� $- false��. �� *
IsPaired+.a��� $/ DisconnectIcon��
 � y�$ 	Collapsed=��� $/ 
UnBindIcon��
 � y�$ Visible=��� $- Invalid��. �� *Alarm+.a��� $/ txtName��
 � ��
 ��i���� $- HighPreAlarm��. �� *Alarm+.a��� $/ txtName��
 � ��
 � ����� $- LowPreAlarm��. �� *Alarm+.a��� $/ txtName��
 � ��
 � ����� $- 	HighAlarm��. �� *Alarm+.a��� $/ txtName��
 � ��
 � ����� $- LowAlarm��. �� *Alarm+.a��� $/ txtName��
 � ��
 � ����� $- 
HighDanger��. �� *Alarm+.a��� $/ txtName��
 � ��
 �  ���� $- 	LowDanger��. �� *Alarm+.a��� $/ txtName��
 � ��
 �  ���� "&  ' �� $" Children�.R��� $ 
Horizontal=�.+�F� $��18��$��18��$
) Fill=�#���  ����� $�  Q%�v��  Q%�v�� ���р�� ���� H� $� � H� � р�C � р�C��р���� $�  H�� $�  Q%������ р� $� � Q% $� � Q%��� р���� � �  H�� � ��р�� ��р��  H�� �  H�� $��2,0,2,0q�.+��� $* txtName��$
��Auto������ *Name+$+ TwoWay=�F� $( DisconnectIcon��$y�	Collapsed=�$��18��$��18��$
) Fill=�
������������< �K��H2(�T�)�:K)��;H2H2���;)�:K)(�T�2�K�< ����,@@�vW��K@�\�*����ф��@+��@�\��K�vW��,@@< �K�p��_)���@@���*̾;p40"�Ċ��_��T+U� 3���^p<��^�KT+U�+U��K��^p<��^� 3�+U��_�Ċ��"�p40h�;$�*��|����2,k��x
����l��쎂l����kx
�u]ӄ1�NO�#-�NO�J;�u]�` f@+���*�` fѨu]�J;ӜNO�#-�NO�1�u]�
S��k�ʡE��@@����ʡE��@@����
�,k��2Q��_)���@@, �� $- false��. �� *IsConnected+.a��� $/ DisconnectIcon��
 � y�$ Visible=��� $- Invalid��. �� *Alarm+.a��� $/ txtName��
 � ��
 ��i���� $- HighPreAlarm��. �� *Alarm+.a��� $/ txtName��
 � ��
 � ����� $- LowPreAlarm��. �� *Alarm+.a��� $/ txtName��
 � ��
 � ����� $- 	HighAlarm��. �� *Alarm+.a��� $/ txtName��
 � ��
 � ����� $- LowAlarm��. �� *Alarm+.a��� $/ txtName��
 � ��
 � ����� $- 
HighDanger��. �� *Alarm+.a��� $/ txtName��
 � ��
 �  ���� $- 	LowDanger��. �� *Alarm+.a��� $/ txtName��
 � ��
 �  ������ "	 Q�  LineTreeViewItem1 �� BasedOn#1 [ .��� 
 �@Q�
IsSelected �� *
IsSelected+$+ OneTime=��� 
 �@Q�
IsExpanded �� *
IsExpanded+$+ OneTime=�. 	 (System.Windows.Interactivity.Interaction2   Triggers2 / 	 )System.Windows.Interactivity.EventTrigger  3   	EventName$3 SelectedItemChanged��. 	 (System.Windows.Interactivity.TriggerBase4   Actions.4 6 	 0System.Windows.Interactivity.InvokeCommandAction  5   Command5 �� *SelectedTreeChangedComamnd+6   CommandParameter6 �� *SelectedItem+$! treeview���� -   7 ��Name$7 gsSplitterr��$��0��$��2��$��1��$��3��#���  $��Center=�$��Stretch=�  GridSplitterStyleExpend#��[ �� $��0��$��2��$��3��$ 2q�  AccentColorBrush2#���  ._�� .+��� $ 
Horizontal=�.+��� $��设备名称��$��Center=�$	��5,0q��� $��Center=�$	��5,0q�8 �� 
IsReadOnly8 . $	��200������ *
DeviceName+�� -   9 ��Name$	9 chk��$��锁定布局��  CheckBoxStyle#��[ $��Center=�$	��5,0q�: d� 	IsChecked: �� *
IsReadOnly+{��� *CanOperated+��   AccentedSquareButtonStyle#���  $��Center=�$	��5,0q����� *SaveLayoutCommand+���� $ 
Horizontal=�.+��� $
��None=�$3��-/AIC.Resources;component/Images/page_save.png���� $��保存布局��$��Center=��� $��1��$��2��$��1q�#���  ._�� .+�$  DiagramDesigner.DiagramControl  ;   ItemTemplateSelector#; [ $��3q�
������ < � DataContext< �� *DiagramViewModel+�� -   $7 gsSecondSplitterr��$��1��$��3��$��3��#���  $��Center=�$��Stretch=�9� -   = 9�Name$= expander��$��1��$��4��> 9� ExpandDirection$
> Left=�? 9� 
IsExpanded? �� *ItemIsExpanded+.��� $��0��$��4��# �� $
��auto ��� $��* �.+�� $��0,5,0,0q�.+��� $��5q�.+��� $ 
Horizontal=�.+��� -   $9 filterCheckBox��$	��5,0q�#��[ $��Center=�: �� *
IsFilter+&   Xceed.Wpf.Toolkit.DropDownButton  -   @  Name$@ dropDownButton��
������ $��滤波��$��Center=�A   DropDownContentA � $	��320��# �� $
��auto ��� $��* �.+��� $ 
Horizontal=�.+�	� -	   B 	�Name$B 
bandPassRb��  RadioButtonStyle#��[ 
���   �$��带通��$��Center=�: �� C �� Source#C [ $$" Data.SelectedSignal.FilterType�D �� 	Converter#D [ E �� ConverterParameter$E BandPass��	� -
   $B 
highPassRb��#��[ 
���   �$��高通��$��Center=�: �� #C [ $$" Data.SelectedSignal.FilterType�#D [ $E HighPass��	� -   $B 	lowPassRb��#��[ 
���   �$��低通��$��Center=�: �� #C [ $$" Data.SelectedSignal.FilterType�#D [ $E LowPass��� $��1��$��5,5,25,0q�y��� #C [ $$" Data.SelectedSignal.FilterType�#D [   AIC.CoreType.FilterTypeF   BandPass#E ZF G �� FallbackValue$G 	Collapsed��# �� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto �$ �� $
��auto ��� $��* �.+��� $��通带衰减��	  $��1���  �Xceed.Wpf.Toolkit.Primitives.UpDownBase`1[[System.Nullable`1[[System.Double, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]H   Minimum$
H 0.01X�I   Maximum$I 3X��  �Xceed.Wpf.Toolkit.NumericUpDown`1[[System.Nullable`1[[System.Double, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]J   	Increment$	J 0.1X�K   ValueK �� #C [ $8" 2Data.SelectedSignal.BPFilter.PassbandAttenuationDB��� $��1��$��阻带衰减��	  $��1��$��1��$H 40X�$	I 120X�K �� #C [ $8" 2Data.SelectedSignal.BPFilter.StopbandAttenuationDB��� #��[ 
���   �$��2��$��绑定转速��$��2��: �� #C [ $/" )Data.SelectedSignal.BPFilter.IsRPMBinding�2   $3 Checked��.4   5 �� #C [ $1" +Data.SelectedSignal.BPFilter.BindRPMCommand�6 �� #C [ $" Data.SelectedSignal.RPM�  $3 	Unchecked��.4   5 �� #C [ $3" -Data.SelectedSignal.BPFilter.UnBindRPMCommand�6 �� #C [ $" Data.SelectedSignal.RPM��� $��3��$��低逼近带通��	  $��3��$��1��$H 0X�K �� #C [ $3" -Data.SelectedSignal.BPFilter.BPPassBandFreLow��� $��4��$��高逼近带通��	  $��4��$��1��$H 0X�K �� #C [ $4" .Data.SelectedSignal.BPFilter.BPPassBandFreHigh��� $��5��$��过渡带宽��	  $��5��$��1��$H 0X�K �� #C [ $6" 0Data.SelectedSignal.BPFilter.TransitionBandwidth�A� $��6��$��2��.+��� $��保存��$��5q����� #C [ $." (Data.SelectedSignal.BPFilter.SaveCommand��� $��重置��$��5q����� #C [ $/" )Data.SelectedSignal.BPFilter.ResetCommand�� $��1��$��5,5,25,0q�y��� #C [ $$" Data.SelectedSignal.FilterType�#D [ L   HighPass#E ZL $G 	Collapsed��# �� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto �$ �� $
��auto ��� $��* �.+��� $��通带衰减��	  $��1��$
H 0.01X�$I 3X�$	J 0.1X�K �� #C [ $8" 2Data.SelectedSignal.HPFilter.PassbandAttenuationDB��� $��1��$��阻带衰减��	  $��1��$��1��$H 40X�$	I 120X�K �� #C [ $8" 2Data.SelectedSignal.HPFilter.StopbandAttenuationDB��� #��[ 
���   �$��2��$��绑定转速��$��2��: �� #C [ $/" )Data.SelectedSignal.HPFilter.IsRPMBinding�$+ TwoWay=�2   $3 Checked��.4   5 �� #C [ $1" +Data.SelectedSignal.HPFilter.BindRPMCommand�6 �� #C [ $" Data.SelectedSignal.RPM�  $3 	Unchecked��.4   5 �� #C [ $3" -Data.SelectedSignal.HPFilter.UnBindRPMCommand�6 �� #C [ $" Data.SelectedSignal.RPM��� $��3��$��通带��	  $��3��$��1��$H 0X�K �� #C [ $." (Data.SelectedSignal.HPFilter.PassbandFre��� $��4��$��阻带��	  $��4��$��1��$H 0X�K �� #C [ $." (Data.SelectedSignal.HPFilter.StopbandFre�A� $��6��$��2��.+��� $��保存��$��5q����� #C [ $." (Data.SelectedSignal.HPFilter.SaveCommand��� $��重置��$��5q����� #C [ $/" )Data.SelectedSignal.HPFilter.ResetCommand�� $��1��$��5,5,25,0q�y��� #C [ $$" Data.SelectedSignal.FilterType�#D [ M   LowPass#E ZM $G 	Collapsed��# �� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto �$ �� $
��auto ��� $��* �.+��� $��通带衰减��	  $��1��$
H 0.01X�$I 3X�$	J 0.1X�K �� #C [ $8" 2Data.SelectedSignal.LPFilter.PassbandAttenuationDB��� $��1��$��阻带衰减��	  $��1��$��1��$H 40X�$	I 120X�K �� #C [ $8" 2Data.SelectedSignal.LPFilter.StopbandAttenuationDB��� #��[ 
���   �$��2��$��绑定转速��$��2��: �� #C [ $/" )Data.SelectedSignal.LPFilter.IsRPMBinding�$+ TwoWay=�2   $3 Checked��.4   5 �� #C [ $1" +Data.SelectedSignal.LPFilter.BindRPMCommand�6 �� #C [ $" Data.SelectedSignal.RPM�  $3 	Unchecked��.4   5 �� #C [ $3" -Data.SelectedSignal.LPFilter.UnBindRPMCommand�6 �� #C [ $" Data.SelectedSignal.RPM��� $��3��$��通带��	  $��3��$��1��$H 0X�K �� #C [ $." (Data.SelectedSignal.LPFilter.PassbandFre��� $��4��$��阻带��	  $��4��$��1��$H 0X�K �� #C [ $." (Data.SelectedSignal.LPFilter.StopbandFre�A� $��6��$��2��.+��� $��保存��$��5q����� #C [ $." (Data.SelectedSignal.LPFilter.SaveCommand��� $��重置��$��5q����� #C [ $/" )Data.SelectedSignal.LPFilter.ResetCommand�	� $	��无��N 	� 	GroupName$N pregroup��#��[ $��Center=�: �� *SignalPreProccessType+#D [ $
E None��	� $��包络��$N pregroup��#��[ $��Center=�: �� *SignalPreProccessType+#D [ $E Envelope��	� $	��TFF��$N pregroup��#��[ $��Center=�: �� *SignalPreProccessType+#D [ $	E TFF��	� $��	倒频谱��$N pregroup��#��[ $��Center=�: �� *SignalPreProccessType+#D [ $E Cepstrum��� $��Disabled=�.+��� $	��5,0q�$��时域��#��[ : �� *IsTimeDomainChecked+2   $3 Checked��.4   $6 
TimeDomain��5 �� *AddGraphFuncCommand+  $3 	Unchecked��.4   $6 
TimeDomain��5 �� *RemoveGraphFuncCommand+�� $	��5,0q�$��频域��#��[ : �� *IsFrequencyDomainChecked+2   $3 Checked��.4   $6 FrequencyDomain��5 �� *AddGraphFuncCommand+  $3 	Unchecked��.4   $6 FrequencyDomain��5 �� *RemoveGraphFuncCommand+�� $	��5,0q�$��	趋势图��#��[ : �� *IsRMSTrendChecked+2   $3 Checked��.4   $6 RMSTrend��5 �� *AddGraphFuncCommand+  $3 	Unchecked��.4   $6 RMSTrend��5 �� *RemoveGraphFuncCommand+�� $	��5,0q�$��	功率谱��#��[ : �� *IsPowerSpectrumChecked+2   $3 Checked��.4   $6 PowerSpectrum��5 �� *AddGraphFuncCommand+  $3 	Unchecked��.4   $6 PowerSpectrum��5 �� *RemoveGraphFuncCommand+�� $	��5,0q�$��功率谱密度��#��[ : �� *IsPowerSpectrumDensityChecked+2   $3 Checked��.4   $6 PowerSpectrumDensity��5 �� *AddGraphFuncCommand+  $3 	Unchecked��.4   $6 PowerSpectrumDensity��5 �� *RemoveGraphFuncCommand+�� $	��5,0q�$��时间三维��#��[ : �� *IsTime3DChecked+2   $3 Checked��.4   $6 Time3D��5 �� *AddGraphFuncCommand+  $3 	Unchecked��.4   $6 Time3D��5 �� *RemoveGraphFuncCommand+�� $	��5,0q�$��分频��#��[ {�.  $y�	Collapsed=�: �� *IsMultiDivFreChecked+2   $3 Checked��.4   $6 MultiDivFre��5 �� *AddGraphFuncCommand+  $3 	Unchecked��.4   $6 MultiDivFre��5 �� *RemoveGraphFuncCommand+�� $	��5,0q�$��轴心轨迹��#��[ {�.  : �� *IsOrthoChecked+2   $3 Checked��.4   $6 Ortho��5 �� *AddGraphFuncCommand+  $3 	Unchecked��.4   $6 Ortho��5 �� *RemoveGraphFuncCommand+�� $	��5,0q�$��伯德��#��[ {�.  : �� *IsBodeChecked+2   $3 Checked��.4   $
6 Bode��5 �� *AddGraphFuncCommand+  $3 	Unchecked��.4   $
6 Bode��5 �� *RemoveGraphFuncCommand+�� $	��5,0q�$��阶次分析��#��[ {�.  : �� *IsOrderChecked+2   $3 Checked��.4   $6 Order��5 �� *AddGraphFuncCommand+  $3 	Unchecked��.4   $6 Order��5 �� *RemoveGraphFuncCommand+�� $	��5,0q�$��奈奎斯特��#��[ {�.  : �� *IsNyquistChecked+2   $3 Checked��.4   $6 Nyquist��5 �� *AddGraphFuncCommand+  $3 	Unchecked��.4   $6 Nyquist��5 �� *RemoveGraphFuncCommand+�� $	��5,0q�$��转速三维��#��[ {�.  : �� *IsRPM3DChecked+2   $3 Checked��.4   $6 RPM3D��5 �� *AddGraphFuncCommand+  $3 	Unchecked��.4   $6 RPM3D��5 �� *RemoveGraphFuncCommand+� $��1��$��0,5,0,0q�.+��� $
��Auto=�.��7   1AIC.OnLineDataPage.Controls.ResizableItemsControl  $��Stretch=�$��Stretch=�$��3q�#��[	 #��[
 ���� *GraphFunctions+��Z� ��Z� 