   M S B A M L   `   `   `  ���� M  IAIC.OnLineDataPage, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullC&clr-namespace:AIC.OnLineDataPage.ViewsAIC.OnLineDataPage.Views  C ?AIC.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5(clr-namespace:AIC.Core;assembly=AIC.CoreAIC.Core G CAIC.CoreType, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullA0clr-namespace:AIC.CoreType;assembly=AIC.CoreTypeAIC.CoreType T PWpf.PageNavigationControl, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullhJclr-namespace:Wpf.PageNavigationControl;assembly=Wpf.PageNavigationControlWpf.PageNavigationControl [;clr-namespace:AIC.Core.OrganizationModels;assembly=AIC.CoreAIC.Core.OrganizationModels I2clr-namespace:AIC.Core.ExCommand;assembly=AIC.CoreAIC.Core.ExCommand I)clr-namespace:AIC.OnLineDataPage.ControlsAIC.OnLineDataPage.Controls  K GWpf.GridSelected, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullM8clr-namespace:Wpf.GridSelected;assembly=Wpf.GridSelectedWpf.GridSelected T PWpf.VirtualizingWrapPanel, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullhJclr-namespace:Wpf.VirtualizingWrapPanel;assembly=Wpf.VirtualizingWrapPanelWpf.VirtualizingWrapPanel 1    +AIC.OnLineDataPage.Views.OnlineDataTileView   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R	 NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml    	 Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006    	 @d2http://schemas.microsoft.com/expression/blend/2008    	 8local&clr-namespace:AIC.OnLineDataPage.Views    	 c
 _System.Windows.Interactivity, Version=4.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity 
 3core(clr-namespace:AIC.Core;assembly=AIC.Core 
 ?coreType0clr-namespace:AIC.CoreType;assembly=AIC.CoreType 
 UpageJclr-namespace:Wpf.PageNavigationControl;assembly=Wpf.PageNavigationControl 
 Icontrol;clr-namespace:AIC.Core.OrganizationModels;assembly=AIC.Core 
 @command2clr-namespace:AIC.Core.ExCommand;assembly=AIC.Core 
 =localControls)clr-namespace:AIC.OnLineDataPage.Controls 
 P LPrism.Wpf, Version=6.2.0.0, Culture=neutral, PublicKeyToken=91a96d2a154366d8$prismhttp://prismlibrary.com/  X TXceed.Wpf.Toolkit, Version=3.2.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit  Emygrid8clr-namespace:Wpf.GridSelected;assembly=Wpf.GridSelected  fvirtualizingWrapPanelJclr-namespace:Wpf.VirtualizingWrapPanel;assembly=Wpf.VirtualizingWrapPanel  !  Prism.Mvvm.ViewModelLocator    AutoWireViewModel  .    AsyncWorkUserControlStyle#���    � 	Resources  �� MergedDictionaries �� Source)  #AIC.Core.ExCommand.CommandReference  AddCommandReference   Command(  "AIC.Core.EnumToVisibilityConverter  enumToVisibilityConverter   Reverse#  AIC.Core.EnumBooleanConverter  enumBooleanConverter  AIC.Core.BindingProxy 	 proxy   Data  ButtonStyle �� 
TargetType �� Property	 �� Value  AccentColorBrush
 �� 
TargetType J� SnapsToDevicePixels �� Triggers P� Property P� Value  SignalMonitorStyle ��Name �� Padding  ValueTemplate �� Triggers$  AIC.CoreType.SignalDisplayType   Value �� Value �� Binding �� Path   
TimeDomain 	 TimeDomainTemplate   FrequencyDomain 
 FrequencyDomainTemplate   Bode  BodeTemplate   Ortho  OrthoTemplate   Time3DSpectrum   Time3DSpectrumDomainTemplate   RMSTrend  AMSTrendTemplate   MultiDivFre  MultiDivFreTemplate   OrderAnalysis  OrderAnalysisTemplate   RPM3D  RPM3DSpectrumDomainTemplate   Nyquist  NyquistTemplate    PowerSpectrum  PowerSpectrumTemplate!   PowerSpectrumDensity   PowerSpectrumDensityTemplate"   Diagnose  DiagnoseTemplate  VirtualListBoxStyle5  /Wpf.VirtualizingWrapPanel.VirtualizingWrapPanel# �� RelativeSource$ �� AncestorType  FluidListBoxStyle  AIC.Core.FluidWrapPanel%  Name&   DragOpacity'   	DragScale(   ElementEasing-	  'System.Windows.Media.Animation.BackEase7
  1System.Windows.Media.Animation.EasingFunctionBase) 
  
EasingMode* 	  	Amplitude+   
DragEasing��  �� $5 //AIC.Resources;component/Themes/ColorStyle.xaml?��� $8 2/AIC.Resources;component/Themes/CheckBoxStyle.xaml?��� $F @/AIC.OnLineDataPage;component/Themes/OnlineDataTileTemplate.xaml?��� $4 ./AIC.Resources;component/Themes/ScrollBar.xaml?�%�  &	       &	      &	 )     &	 .     &	 <     &	 �    7  7  7 	 7 
 7  7  7  7  7  7  7  7  7  7  7  &	 <    &	 �       �� *
AddCommand+   .       �� �� " ��.���  � ��
	 ���� ��  � ��#	 �  ��  � ��$	 Center=���  � ��$	 Center=���  � ��$	 1q���  � ��	 �� "
 ��.R�� #��z��.+��� #��z��#��z����. # z #��z�� P�  �@J�IsMouseOver . . ���  � ��	 �	#7F243859P�  � {� .  . ���  � ��	 �#ADADADP�  � �� . . ���  � ��	 �	#FF20437E�� " ��.���  � ��$	 2q���  � ��$	 2q���  � ��	 �� "
 ��.R��� $ Bd��#��z��#��z��#��z��# z�� . ._��� #��z��# z #��z�� P�  � {� .  . ��� #	 � �� � ����  � ��8	    �� # Z  �� $ DisplayMode�.a���  � ��8	  �� # Z  �� $ DisplayMode�.a���  � ��8	  �� # Z  �� $ DisplayMode�.a���  � ��8	  �� # Z  �� $ DisplayMode�.a���  � ��8	  �� # Z  �� $ DisplayMode�.a���  � ��8	  �� # Z  �� $ DisplayMode�.a���  � ��8	  �� # Z  �� $ DisplayMode�.a���  � ��8	  �� # Z  �� $ DisplayMode�.a���  � ��8	  �� # Z  �� $ DisplayMode�.a���  � ��8	 	 �� # Z  �� $ DisplayMode�.a���  � ��8	 
 �� # Z  �� $ DisplayMode�.a���  � ��8	  �� # Z   �� $ DisplayMode�.a���  � ��8	  �� # Z!  �� $ DisplayMode�.a���  � ��8	  �� # Z"  �� $ DisplayMode�.a���  � ��8	  P�  �@J�IsKeyboardFocusWithin . . ���  �@��
IsSelected	 . �� " ��.���  � ��	 �� .R�  ���� *ActualWidth+# �� "$ ���� " ��.���  � ��	 �� .R�  $% fluidWrapPanel��$
��Left=�$	��Top=�}�.  $& 1Q�$' 1Q�( 	  $) EaseOut=�$
* 0.35Q�+ 	  $) EaseOut=�$
* 0.65Q�.��� -   $��grdWorkbench��, � RowDefinitions, �� $
��Auto ��� $��* ��� $
��Auto �- � ColumnDefinitions- �� $
��Auto ��� $
��Auto ��� $��* �.+�R� -   . R�Name$. treeview��$��0��$��3��  VirtualisedMetroTreeView#���  ���� *OrganizationTreeItems+ / �� DataType?  9AIC.Core.OrganizationModels.OrganizationTreeItemViewModel0 �� ItemsSource1 �� Orientation2 F�Name3 �� Stretch4 ��Name5 �� Mode6 �� Triggers7 �� 
TargetName9  3AIC.Core.OrganizationModels.DeviceTreeItemViewModel7  1AIC.Core.OrganizationModels.ItemTreeItemViewModel8 ��Name9  3AIC.Core.OrganizationModels.DivFreTreeItemViewModel��%�  (��       *, +)(�� 9    *, +)(�� ^    *, +)7  (�� @    *, +)�� "/  0 �� $ Children�.R��� $1 
Horizontal=�.+�F� $2 IconPath��$��18��$��18��$
3 Fill=�#���  ������Bnx�V��Bn4�Cќ�O�ob�L�!�!�4�C�71�ܽ�,���ܽ���K�����z@����H��р�e�!��8�obф�4�Cф�x�V��Bnx�V�4�C�Śј��d%ј�(�(��ǘ���@@(�(��ǘ���@@����08�����08�(�(����(�(�����d%�4�C�Ś� $��2,0,2,0q�.+��� $4 txtName��$
��Auto������ *Name+$5 TwoWay=�6 �� $ 0�� �� *T_Organization.Level+.a��� $7 IconPath�� � ���	 �� ��:�����:����pej� �pej� ��:���2��x��2��x����Ѥ����Ѥ��2�� ����є�����'t���������������E�'t�E�'t0P�є�0P�є��E� ��E� ��������EѤ����+�Tn5��+�����+�x����+�x���E����E����x��Tn5�x��Tn5�EѤ���E�$� �E�$� �x��$� l't�T�-���\��l't�\���x��\���EѬ<R�EѬ<R�x��Ԙ� �x��Ԙ� �E�$� �E���-���������%����,�U���-�P��V�� �h�V�� P�7���-����� $7 IconPath�� � ��$	 24���� $ Invalid�� �� *Alarm+.a��� $7 txtName�� � ��
	 ��i���� $ HighPreAlarm�� �� *Alarm+.a��� $7 txtName�� � ��
	 � ����� $ LowPreAlarm�� �� *Alarm+.a��� $7 txtName�� � ��
	 � ����� $ 	HighAlarm�� �� *Alarm+.a��� $7 txtName�� � ��
	 � ����� $ LowAlarm�� �� *Alarm+.a��� $7 txtName�� � ��
	 � ����� $ 
HighDanger�� �� *Alarm+.a��� $7 txtName�� � ��
	 �  ���� $ 	LowDanger�� �� *Alarm+.a��� $7 txtName�� � ��
	 �  ���� "/  0 �� $ Children�.R��� $1 
Horizontal=�.+�F� $��18��$��18��$
3 Fill=�#���  �����P�x��ݩ(�� ���� p׻\r� $�� �(4��0�	�^)�@@�5������@@�.>�A�G�!اQ���P�x�ز�ݩ������y@@�����p׻�:pΈ"@@$��pP�(4�Ql^�	�^)�@@SP=������@@���A(Ċ�!�L�c����2ز�S�U)�����y@@�� ����d�:pΈ"@@��7!P�Ѵ�� l^����� P=�d*� ��PJ� �Ê��L�c�(�� �2��� �U)\r� �� � �dѐ0�7!є5P�� Ӱ.>��� �Gd*� اQPJ� �P�x�� 6n -1Ӵ�L -1 -1��L -1 6n� -1L���L ?� 6n ?��L� ?� ?�L� ?� 6n� ?���LL� -1 6n -1� $��2,0,2,0q�.+��� $4 txtName��$
��Auto������ *Name+$5 TwoWay=�6 �� $ Invalid�� �� *Alarm+.a��� $7 txtName�� � ��
	 ��i���� $ HighPreAlarm�� �� *Alarm+.a��� $7 txtName�� � ��
	 � ����� $ LowPreAlarm�� �� *Alarm+.a��� $7 txtName�� � ��
	 � ����� $ 	HighAlarm�� �� *Alarm+.a��� $7 txtName�� � ��
	 � ����� $ LowAlarm�� �� *Alarm+.a��� $7 txtName�� � ��
	 � ����� $ 
HighDanger�� �� *Alarm+.a��� $7 txtName�� � ��
	 �  ���� $ 	LowDanger�� �� *Alarm+.a��� $7 txtName�� � ��
	 �  ���� "/  0 �� $ Children�.R��� $1 
Horizontal=�.+�F� $��18��$��18��$
3 Fill=�#���  ������ا  � �@]�  � �@]� ���@�4���@�4 Q%��ا  Q%��ا  � ���� @��р�� ���� -1��^�@������@�4@]� �@�4@fр������ -1���р�� @��� $��2,0,2,0q�.+��� $4 txtName��$
��Auto������ *Name+$5 TwoWay=��� $8 AddBtn��8��  $��Center=�$��24��$��24��$��0q����� .��F� $
3 Fill=�$��18��$��18��#���  a���� � ��O� � ���ѐ�����@o@@������@���@�4 6n������� �@o@��� ѐ���O� � ��OF� $2 DisconnectIcon��$y�	Collapsed=�$��18��$��18��$
3 Fill=�
������������< �K��H2(�T�)�:K)��;H2H2���;)�:K)(�T�2�K�< ����,@@�vW��K@�\�*����ф��@+��@�\��K�vW��,@@< �K�p��_)���@@���*̾;p40"�Ċ��_��T+U� 3���^p<��^�KT+U�+U��K��^p<��^� 3�+U��_�Ċ��"�p40h�;$�*��|����2,k��x
����l��쎂l����kx
�u]ӄ1�NO�#-�NO�J;�u]�` f@+���*�` fѨu]�J;ӜNO�#-�NO�1�u]�
S��k�ʡE��@@����ʡE��@@����
�,k��2Q��_)���@@F� $2 
UnBindIcon��$y�	Collapsed=�$��16��$��16��$
3 Fill=�
�������������HR|�+���|�+�|��<C��|��Āx�&�L�!l�CL�!�䫮L�!\7Āx\7<C���<C���������Trjl�CTrj��/Trj�������<C�ф��+Ѡ�5|�+��5��e��HR��e��HR|�+6 �� $ false�� �� *IsConnected+.a��� $7 DisconnectIcon�� � y�$	 Visible=��� $7 AddBtn�� � y�$	 	Collapsed=��� $ false�� �� *
IsPaired+.a��� $7 DisconnectIcon�� � y�$	 	Collapsed=��� $7 
UnBindIcon�� � y�$	 Visible=��� $ Invalid�� �� *Alarm+.a��� $7 txtName�� � ��
	 ��i���� $ HighPreAlarm�� �� *Alarm+.a��� $7 txtName�� � ��
	 � ����� $ LowPreAlarm�� �� *Alarm+.a��� $7 txtName�� � ��
	 � ����� $ 	HighAlarm�� �� *Alarm+.a��� $7 txtName�� � ��
	 � ����� $ LowAlarm�� �� *Alarm+.a��� $7 txtName�� � ��
	 � ����� $ 
HighDanger�� �� *Alarm+.a��� $7 txtName�� � ��
	 �  ���� $ 	LowDanger�� �� *Alarm+.a��� $7 txtName�� � ��
	 �  ���� "/  0 �� $ Children�.R��� $1 
Horizontal=�.+�F� $��18��$��18��$
3 Fill=�#���  ����� $�  Q%�v��  Q%�v�� ���р�� ���� H� $� � H� � р�C � р�C��р���� $�  H�� $�  Q%������ р� $� � Q% $� � Q%��� р���� � �  H�� � ��р�� ��р��  H�� �  H�� $��2,0,2,0q�.+��� $4 txtName��$
��Auto������ *Name+$5 TwoWay=�F� $2 DisconnectIcon��$y�	Collapsed=�$��18��$��18��$
3 Fill=�
������������< �K��H2(�T�)�:K)��;H2H2���;)�:K)(�T�2�K�< ����,@@�vW��K@�\�*����ф��@+��@�\��K�vW��,@@< �K�p��_)���@@���*̾;p40"�Ċ��_��T+U� 3���^p<��^�KT+U�+U��K��^p<��^� 3�+U��_�Ċ��"�p40h�;$�*��|����2,k��x
����l��쎂l����kx
�u]ӄ1�NO�#-�NO�J;�u]�` f@+���*�` fѨu]�J;ӜNO�#-�NO�1�u]�
S��k�ʡE��@@����ʡE��@@����
�,k��2Q��_)���@@6 �� $ false�� �� *IsConnected+.a��� $7 DisconnectIcon�� � y�$	 Visible=��� $ Invalid�� �� *Alarm+.a��� $7 txtName�� � ��
	 ��i���� $ HighPreAlarm�� �� *Alarm+.a��� $7 txtName�� � ��
	 � ����� $ LowPreAlarm�� �� *Alarm+.a��� $7 txtName�� � ��
	 � ����� $ 	HighAlarm�� �� *Alarm+.a��� $7 txtName�� � ��
	 � ����� $ LowAlarm�� �� *Alarm+.a��� $7 txtName�� � ��
	 � ����� $ 
HighDanger�� �� *Alarm+.a��� $7 txtName�� � ��
	 �  ���� $ 	LowDanger�� �� *Alarm+.a��� $7 txtName�� � ��
	 �  ������ " Q�  LineTreeViewItem9 �� BasedOn#9 [ .���  �@Q�
IsSelected	 �� *
IsSelected+$5 OneTime=���  �@Q�
IsExpanded	 �� *
IsExpanded+$5 OneTime=�. 
 (System.Windows.Interactivity.Interaction:   Triggers: / 
 )System.Windows.Interactivity.EventTrigger  ;   	EventName$; SelectedItemChanged��. 
 (System.Windows.Interactivity.TriggerBase<   Actions.< 6 
 0System.Windows.Interactivity.InvokeCommandAction  =   Command= �� *SelectedTreeChangedComamnd+>   CommandParameter> �� *SelectedItem+? �� ElementName$? treeview���� -   @ ��Name$@ gsSplitterr��$��0��$��1��$��3��$��3��#���  $��Center=�$��Stretch=�  GridSplitterStyleExpend#��[ �� $��0��$��3��$ 2q�  AccentColorBrush2#���  ._�� A � Orientation$A 
Horizontal=�.+��� $��显示模式��$��5,0,5,0q�$��Center=�	� $��数值��  RadioButtonStyle#��[ B d� 	IsCheckedB �� *CurrentSignalDisplayType+C �� 	Converter#C [ D �� ConverterParameter$D Value��:   $; Checked��.<   = �� * SelectSignalDisplayModeCommand+	� $��趋势��#��[ B �� *CurrentSignalDisplayType+#C [ $D RMSTrend��:   $; Checked��.<   = �� * SelectSignalDisplayModeCommand+	� $��时域��#��[ B �� *CurrentSignalDisplayType+#C [ $D 
TimeDomain��:   $; Checked��.<   = �� * SelectSignalDisplayModeCommand+	� $��频域��#��[ B �� *CurrentSignalDisplayType+#C [ $D FrequencyDomain��:   $; Checked��.<   = �� * SelectSignalDisplayModeCommand+	� $��	功率谱��#��[ B �� *CurrentSignalDisplayType+#C [ $D PowerSpectrum��:   $; Checked��.<   = �� * SelectSignalDisplayModeCommand+	� $��功率谱密度��#��[ B �� *CurrentSignalDisplayType+#C [ $D PowerSpectrumDensity��:   $; Checked��.<   = �� * SelectSignalDisplayModeCommand+	� $��时间三维��#��[ B �� *CurrentSignalDisplayType+#C [ $D Time3DSpectrum��:   $; Checked��.<   = �� * SelectSignalDisplayModeCommand+	� $��轴心轨迹��#��[ {�.  B �� *CurrentSignalDisplayType+#C [ $D Ortho��:   $; Checked��.<   = �� * SelectSignalDisplayModeCommand+	� $��伯德��#��[ {�.  B �� *CurrentSignalDisplayType+#C [ $
D Bode��:   $; Checked��.<   = �� * SelectSignalDisplayModeCommand+	� $��转速三维��#��[ {�.  B �� *CurrentSignalDisplayType+#C [ $D RPM3D��:   $; Checked��.<   = �� * SelectSignalDisplayModeCommand+	� $��诊断��#��[ B �� *CurrentSignalDisplayType+#C [ $D Diagnose��:   $; Checked��.<   = �� * SelectSignalDisplayModeCommand+�� $��Stretch=�$��2��#���  $��5,0,0,0q��� $��	预处理��$��5,0,5,0q�$��Center=��� -   E ��Name$E filterCheckBox��$	��5,0q�  CheckBoxStyle#��[ $��Center=�B �� *
IsFilter+&   Xceed.Wpf.Toolkit.DropDownButton  -   F  Name$F dropDownButton��
������ $��滤波��$��Center=�G   DropDownContentG � $	��320��, �� $
��auto ��� $��* �.+��� $1 
Horizontal=�.+�	� -   H 	�Name$H 
bandPassRb��#��[ 
���   �$��带通��$��Center=�B �� I �� Source#I [ $2 ,Data.SelectedSignalMonitor.Signal.FilterType�#C [ $D BandPass��	� -   $H 
highPassRb��#��[ 
���   �$��高通��$��Center=�B �� #I [ $2 ,Data.SelectedSignalMonitor.Signal.FilterType�#C [ $D HighPass��	� -   $H 	lowPassRb��#��[ 
���   �$��低通��$��Center=�B �� #I [ $2 ,Data.SelectedSignalMonitor.Signal.FilterType�#C [ $D LowPass��� $��1��$��5,5,25,0q�y��� #I [ $2 ,Data.SelectedSignalMonitor.Signal.FilterType�#C [   AIC.CoreType.FilterTypeJ   BandPass#D ZJ K �� FallbackValue$K 	Collapsed��, �� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto �- �� $
��auto ��� $��* �.+��� $��通带衰减��$  Xceed.Wpf.Toolkit.DoubleUpDown  $��1���  �Xceed.Wpf.Toolkit.Primitives.UpDownBase`1[[System.Nullable`1[[System.Double, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]L   Minimum$
L 0.01X�M   Maximum$M 3X��  �Xceed.Wpf.Toolkit.NumericUpDown`1[[System.Nullable`1[[System.Double, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]N   	Increment$	N 0.1X�O   ValueO �� #I [ $F @Data.SelectedSignalMonitor.Signal.BPFilter.PassbandAttenuationDB��� $��1��$��阻带衰减��  $��1��$��1��$L 40X�$	M 120X�O �� #I [ $F @Data.SelectedSignalMonitor.Signal.BPFilter.StopbandAttenuationDB��� #��[ 
���   �$��2��$��绑定转速��$��2��B �� #I [ $= 7Data.SelectedSignalMonitor.Signal.BPFilter.IsRPMBinding�:   $; Checked��.<   = �� #I [ $? 9Data.SelectedSignalMonitor.Signal.BPFilter.BindRPMCommand�> �� #I [ $+ %Data.SelectedSignalMonitor.Signal.RPM�  $; 	Unchecked��.<   = �� #I [ $A ;Data.SelectedSignalMonitor.Signal.BPFilter.UnBindRPMCommand�> �� #I [ $+ %Data.SelectedSignalMonitor.Signal.RPM��� $��3��$��低逼近带通��  $��3��$��1��$L 0X�O �� #I [ $A ;Data.SelectedSignalMonitor.Signal.BPFilter.BPPassBandFreLow��� $��4��$��高逼近带通��  $��4��$��1��$L 0X�O �� #I [ $B <Data.SelectedSignalMonitor.Signal.BPFilter.BPPassBandFreHigh��� $��5��$��过渡带宽��  $��5��$��1��$L 0X�O �� #I [ $D >Data.SelectedSignalMonitor.Signal.BPFilter.TransitionBandwidth�A� $��6��$��2��.+��� $��保存��$��5q����� #I [ $< 6Data.SelectedSignalMonitor.Signal.BPFilter.SaveCommand��� $��重置��$��5q����� #I [ $= 7Data.SelectedSignalMonitor.Signal.BPFilter.ResetCommand�� $��1��$��5,5,25,0q�y��� #I [ $2 ,Data.SelectedSignalMonitor.Signal.FilterType�#C [ P   HighPass#D ZP $K 	Collapsed��, �� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto �- �� $
��auto ��� $��* �.+��� $��通带衰减��  $��1��$
L 0.01X�$M 3X�$	N 0.1X�O �� #I [ $F @Data.SelectedSignalMonitor.Signal.HPFilter.PassbandAttenuationDB��� $��1��$��阻带衰减��  $��1��$��1��$L 40X�$	M 120X�O �� #I [ $F @Data.SelectedSignalMonitor.Signal.HPFilter.StopbandAttenuationDB��� #��[ 
���   �$��2��$��绑定转速��$��2��B �� #I [ $= 7Data.SelectedSignalMonitor.Signal.HPFilter.IsRPMBinding�$5 TwoWay=�:   $; Checked��.<   = �� #I [ $? 9Data.SelectedSignalMonitor.Signal.HPFilter.BindRPMCommand�> �� #I [ $+ %Data.SelectedSignalMonitor.Signal.RPM�  $; 	Unchecked��.<   = �� #I [ $A ;Data.SelectedSignalMonitor.Signal.HPFilter.UnBindRPMCommand�> �� #I [ $+ %Data.SelectedSignalMonitor.Signal.RPM��� $��3��$��通带��  $��3��$��1��$L 0X�O �� #I [ $< 6Data.SelectedSignalMonitor.Signal.HPFilter.PassbandFre��� $��4��$��阻带��  $��4��$��1��$L 0X�O �� #I [ $< 6Data.SelectedSignalMonitor.Signal.HPFilter.StopbandFre�A� $��6��$��2��.+��� $��保存��$��5q����� #I [ $< 6Data.SelectedSignalMonitor.Signal.HPFilter.SaveCommand��� $��重置��$��5q����� #I [ $= 7Data.SelectedSignalMonitor.Signal.HPFilter.ResetCommand�� $��1��$��5,5,25,0q�y��� #I [ $2 ,Data.SelectedSignalMonitor.Signal.FilterType�#C [ Q   LowPass#D ZQ $K 	Collapsed��, �� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto �- �� $
��auto ��� $��* �.+��� $��通带衰减��  $��1��$
L 0.01X�$M 3X�$	N 0.1X�O �� #I [ $F @Data.SelectedSignalMonitor.Signal.LPFilter.PassbandAttenuationDB��� $��1��$��阻带衰减��  $��1��$��1��$L 40X�$	M 120X�O �� #I [ $F @Data.SelectedSignalMonitor.Signal.LPFilter.StopbandAttenuationDB��� #��[ 
���   �$��2��$��绑定转速��$��2��B �� #I [ $= 7Data.SelectedSignalMonitor.Signal.LPFilter.IsRPMBinding�$5 TwoWay=�:   $; Checked��.<   = �� #I [ $? 9Data.SelectedSignalMonitor.Signal.LPFilter.BindRPMCommand�> �� #I [ $+ %Data.SelectedSignalMonitor.Signal.RPM�  $; 	Unchecked��.<   = �� #I [ $A ;Data.SelectedSignalMonitor.Signal.LPFilter.UnBindRPMCommand�> �� #I [ $+ %Data.SelectedSignalMonitor.Signal.RPM��� $��3��$��通带��  $��3��$��1��$L 0X�O �� #I [ $< 6Data.SelectedSignalMonitor.Signal.LPFilter.PassbandFre��� $��4��$��阻带��  $��4��$��1��$L 0X�O �� #I [ $< 6Data.SelectedSignalMonitor.Signal.LPFilter.StopbandFre�A� $��6��$��2��.+��� $��保存��$��5q����� #I [ $< 6Data.SelectedSignalMonitor.Signal.LPFilter.SaveCommand��� $��重置��$��5q����� #I [ $= 7Data.SelectedSignalMonitor.Signal.LPFilter.ResetCommand�	� $	��无��R 	� 	GroupName$R pregroup��#��[ $��Center=�B �� *SignalPreProccessType+#C [ $
D None��	� $��包络��$R pregroup��#��[ $��Center=�B �� *SignalPreProccessType+#C [ $D Envelope��	� $	��TFF��$R pregroup��#��[ $��Center=�B �� *SignalPreProccessType+#C [ $	D TFF��	� $��	倒频谱��$R pregroup��#��[ $��Center=�B �� *SignalPreProccessType+#C [ $D Cepstrum���� $��Stretch=�$��2��#���  $��5,0,5,0q���   AccentedSquareButtonStyle#���  ���� *
AddCommand+���� $? treeview��$ SelectedItem����� $1 
Horizontal=�.+��� $
��None=�$-��'/AIC.Resources;component/Images/add.png����   add#���  $��Center=��� #���  $��4,0,0,0q����� *ShowFunctionsCommand+���� $? treeview��$ SelectedItem����� $1 
Horizontal=�.+��� $
��None=�$1��+/AIC.Resources;component/Images/refresh.png���� $��模式��$��Center=��� #���  $��4,0,0,0q����� *ClearAllCommand+���� $1 
Horizontal=�.+��� $
��None=�$1��+/AIC.Resources;component/Images/delete3.png����  	  clear#���   $��Center=��� $��Stretch=�$��2��#���  $��5,0,5,0q��� $��2q� ! 
BlackBrush#��� ! #���  $ 0q�$��0,0,0,0q�._�  
������ ���� $1 
Horizontal=�.+��� $
��Fill=�$��16��$��16��$0��*/AIC.Resources;component/Images/layout.png���� $��排列��$��SemiBold"� " DefaultFont#��� " 
�������$��15(�$��Center=�G $  Wpf.GridSelected.RectangleGrid  -	   S  Name$S rectgrid��T   SelectT �� #I [ $ Data.Select�$5 TwoWay=�:   $; SelectRowColumnChanged��.< .  (AIC.Core.ExCommand.ExInvokeCommandAction  U   CommandU �� #I [ $( "Data.SelectRowColumnChangedComamnd�V   CommandParameterV �� $? rectgrid��� $��1��$��3��$��0,0,0,0q�, �� $
��Auto ��� $��* �- �� $��* ��� $
��auto �.+��� -
   W ��Name$W listview��$��1��$��0��$��Stretch=�$��Stretch=�#��[ ��Z� ��Z� ���� *SignalMonitors+$5 TwoWay=�X �� SelectedItemX �� *SelectedSignalMonitor+���� *HorizontalScrollBarVisibility+$5 TwoWay=����� *VerticalScrollBarVisibility+$5 TwoWay=����� .R�  $% fluidWrapPanel��$
��Left=�$	��Top=�}�.  $& 1Q�$' 1Q�Y   
ItemHeightY �� *
ItemHeight+Z   	ItemWidthZ �� *	ItemWidth+[   IsComposing[ �� *IsComposing+\   Orientation\ �� *Orientation+]   FluidElements] �� *FluidElements+$5 OneWayToSource=�( 	  $) EaseOut=�$
* 0.35Q�+ 	  $) EaseOut=�$
* 0.65Q�:   $; Loaded��.<   U �� *ListBoxLoadedComamnd++  %AIC.Core.ExCommand.RoutedEventTrigger  ^   RoutedEvent$ ^ ScrollViewer.ScrollChanged��.<   U �� *ScrollChangedComamnd+�� $��0��$��0�� # 
FlatSlider#��� # _ � Minimum$	_ 120Q�` � Maximum$
` 1550Q�a �� Orientation$a 
Horizontal=�b � Valueb �� *	ItemWidth+c �� Delay$	c 200���� $��1��$��1��#��� # $	_ 120Q�$	` 600Q�$a Vertical=�b �� *
ItemHeight+$	c 200���� $��0��$��1��d � ToolTip$d 允许拖拽��B �� *IsComposing+$5 TwoWay=���Z� 