   M S B A M L   `   `   `  ���� c  _System.Windows.Interactivity, Version=4.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35qPclr-namespace:System.Windows.Interactivity;assembly=System.Windows.InteractivitySystem.Windows.Interactivity  G CAIC.CoreType, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullA0clr-namespace:AIC.CoreType;assembly=AIC.CoreTypeAIC.CoreType C ?AIC.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5(clr-namespace:AIC.Core;assembly=AIC.CoreAIC.Core M IAIC.OnLineDataPage, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullC&clr-namespace:AIC.OnLineDataPage.ViewsAIC.OnLineDataPage.Views U/clr-namespace:AIC.OnLineDataPage.Views.SubViews!AIC.OnLineDataPage.Views.SubViews M+clr-namespace:AIC.OnLineDataPage.ViewModelsAIC.OnLineDataPage.ViewModels R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089U QMahApps.Metro, Version=1.5.0.23, Culture=neutral, PublicKeyToken=f4fb5a3c4d1e5b4f'   !AIC.Core.EnumDescriptionConverter   enumDescriptionConverter(  "AIC.Core.BoolToVisibilityConverter  boolToVisibilityConverter   booleanToVisibilityConverter  ValueTemplate.   (System.Windows.Interactivity.Interaction    	Behaviors%  AIC.Core.FluidMouseDragBehavior   
DragButton  AccentColorBrush � RowDefinitions � ColumnDefinitions �Name � ToolTip �� StringFormat �� Path �� Orientation	 ��Name
 F�Name �� Stretch � 	Resources �� 
TargetType �� Property �� Value �� Mode �� Triggers �� Value �� Binding �� 
TargetName  BarTemplate �Name � Value � Maximum�� D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    :x,http://schemas.microsoft.com/winfx/2006/xaml     ViPclr-namespace:System.Windows.Interactivity;assembly=System.Windows.Interactivity  =coreType0clr-namespace:AIC.CoreType;assembly=AIC.CoreType  1core(clr-namespace:AIC.Core;assembly=AIC.Core  ;Controls,http://metro.mahapps.com/winfx/xaml/controls  0views&clr-namespace:AIC.OnLineDataPage.Views  <subviews/clr-namespace:AIC.OnLineDataPage.Views.SubViews  9	viewModel+clr-namespace:AIC.OnLineDataPage.ViewModels  %  &	        &	      &	 
     &	      &	          �� �� .R�� ���� *	ItemWidth+���� *
ItemHeight+    $
 Left=�.+�� $��2q�#���   �� $��20 ��� $��* � �� $
��Auto ��� $��* �.+�� $ 
gridheader��
��� � �$��0,0,0,0q�$��2�� �� $
��Bold"���`� $	 {0}��.1��� $ Device.Name�.+��� $��5,0,0,0q�$ 
Horizontal=�.+��� $	 txtName��$��Center=���`� $	 {0}��.1��� $ Device.Name��� $��1��$��0��$��5, 0, 5, 20q�$��Center=�.+��� $	 txtRun��
��� � �$��运行��$��Center=��� $	 txtStop��
���  ��$��停止��$y�	Collapsed=�$��Center=�F� $
 pathIcon��$��28��$��28��$
 Fill=�
���   �
��� � �$��2q�������DuuӬ�L��q�S6p(�Y6p���_6pp;e��qp�imt�in�<w8rl�zp?ul�ќ�{����0~�)���0~����!|8@�xx\H���s�P#H-n0=@��f[��_D�u��V�����L;��C<ݻ�:8 ����,d���!d�����@��Ӭ
��U'�8b5l���A�輲��m\Z~�ۍd�C\&��d	�ۍص���m����AӘ���#/����d�lH$��p�Y�b��5I�n�H�:4G��8�+��� ~\t��`1����C�y	`���y	�)��D�t�єC\�Ӡz�+{�y��wT@8�t��$�!r�q)@�p�/@�p�|�5@�p�<�Ur`�Buu� �R��|p7a�с�{nф���{LЇT	��O��V��O�Ӥb��O�0�h�T�p������&~,���x�����qѴ����[�d~�E�(�)��4���1�>0ț:T�-d�CT�-�'MT�-��U�>0�&^��4�|tv�E�R���[�4#���q�l��x���&~��p��Ӱ�h�<���O�0���O��(���O�\&LЇSф�H&�с��4��|��Duu� $��1��$��1��$��0,0,0,0q�.+��� $��0,2,0,0q�
������ 
������ ���� *Device.Children+ ���� '��       " ��.���  � ��
 ���� ��  � ��$ 16(����� .R�� .+��� $ 
Horizontal=�.+��� $
��Auto��$��16(����� *Name+$ TwoWay=��� $ 
Horizontal=�$��Right=�.+��� $	 AMSText��$��16(�$
��Auto��$��5,0,0,0q����� *BaseAlarmSignal.Result+$ F3��$ TwoWay=��� $��16(�$��5,0,0,0q����� *BaseAlarmSignal.Unit+ �� $ Invalid�� �� *!BaseAlarmSignal.DelayAlarmGrade+.a��� $ AMSText�� � ��
 ��i���� $ HighPreAlarm�� �� *!BaseAlarmSignal.DelayAlarmGrade+.a��� $ AMSText�� � ��
 � ����� $ LowPreAlarm�� �� *!BaseAlarmSignal.DelayAlarmGrade+.a��� $ AMSText�� � ��
 � ����� $ 	HighAlarm�� �� *!BaseAlarmSignal.DelayAlarmGrade+.a��� $ AMSText�� � ��
 � ����� $ LowAlarm�� �� *!BaseAlarmSignal.DelayAlarmGrade+.a��� $ AMSText�� � ��
 � ����� $ 
HighDanger�� �� *!BaseAlarmSignal.DelayAlarmGrade+.a��� $ AMSText�� � ��
 �  ���� $ 	LowDanger�� �� *!BaseAlarmSignal.DelayAlarmGrade+.a��� $ AMSText�� � ��
 �  �� �� $ Invalid�� �� *Device.Alarm+.a��� $ txtName�� � ��
 ��i���� $ pathIcon�� � ��
 ��i���� $ HighPreAlarm�� �� *Device.Alarm+.a��� $ txtName�� � ��
 � ����� $ pathIcon�� � ��
 � ����� $ LowPreAlarm�� �� *Device.Alarm+.a��� $ txtName�� � ��
 � ����� $ pathIcon�� � ��
 � ����� $ 	HighAlarm�� �� *Device.Alarm+.a��� $ txtName�� � ��
 � ����� $ pathIcon�� � ��
 � ����� $ LowAlarm�� �� *Device.Alarm+.a��� $ txtName�� � ��
 � ����� $ pathIcon�� � ��
 � ����� $ 
HighDanger�� �� *Device.Alarm+.a��� $ txtName�� � ��
 �  ���� $ pathIcon�� � ��
 �  ���� $ 	LowDanger�� �� *Device.Alarm+.a��� $ txtName�� � ��
 �  ���� $ pathIcon�� � ��
 �  ���� $ false�� �� *Device.IsRunning+.a��� $ txtRun�� � y�$ 	Collapsed=��� $ txtStop�� � y�$ Visible=��� $ 
gridheader�� � ��
 �  ���� $ pathIcon�� � ��
 �  ���� .R�� ���� *	ItemWidth+���� *
ItemHeight+    $
 Left=�.+�� $��2q�#���   �� $��20 ��� $��* � �� $
��Auto ��� $��* �.+�� $ 
gridheader��
��� � �$��0,0,0,0q�$��2�� �� $
��Bold"���`� $	 {0}��.1��� $ Device.Name�.+��� $��5,0,0,0q�$ 
Horizontal=�.+��� $	 txtName��$��Center=���`� $	 {0}��.1��� $ Device.Name��� $��1��$��0��$��5, 0, 5, 20q�$��Center=�.+��� $	 txtRun��
��� � �$��运行��$��Center=��� $	 txtStop��
���  ��$��停止��$y�	Collapsed=�$��Center=�F� $
 pathIcon��$��28��$��28��$
 Fill=�
���   �
��� � �$��2q�������DuuӬ�L��q�S6p(�Y6p���_6pp;e��qp�imt�in�<w8rl�zp?ul�ќ�{����0~�)���0~����!|8@�xx\H���s�P#H-n0=@��f[��_D�u��V�����L;��C<ݻ�:8 ����,d���!d�����@��Ӭ
��U'�8b5l���A�輲��m\Z~�ۍd�C\&��d	�ۍص���m����AӘ���#/����d�lH$��p�Y�b��5I�n�H�:4G��8�+��� ~\t��`1����C�y	`���y	�)��D�t�єC\�Ӡz�+{�y��wT@8�t��$�!r�q)@�p�/@�p�|�5@�p�<�Ur`�Buu� �R��|p7a�с�{nф���{LЇT	��O��V��O�Ӥb��O�0�h�T�p������&~,���x�����qѴ����[�d~�E�(�)��4���1�>0ț:T�-d�CT�-�'MT�-��U�>0�&^��4�|tv�E�R���[�4#���q�l��x���&~��p��Ӱ�h�<���O�0���O��(���O�\&LЇSф�H&�с��4��|��Duu� $��1��$��1��$��0,0,0,0q�.+��� $��0,2,0,0q�
������ 
������ ���� *Device.Children+ ���� '��       " ��.���  � ��
 ���� ��  � ��$ 16(����� .R�� .+��� $ 
Horizontal=�.+��� $
��Auto��$��16(����� *Name+$ TwoWay=��� $ 
Horizontal=�$��Right=�.+�� $ ProgressBar��
��� � �$��60��$��5,0,5,0q���Z�  �� *BaseAlarmSignal.Result+ �� *BaseAlarmSignal.AlarmMax+ �� $ Invalid�� �� *!BaseAlarmSignal.DelayAlarmGrade+.a��� $ ProgressBar�� � ��
 ��i���� $ HighPreAlarm�� �� *!BaseAlarmSignal.DelayAlarmGrade+.a��� $ ProgressBar�� � ��
 � ����� $ LowPreAlarm�� �� *!BaseAlarmSignal.DelayAlarmGrade+.a��� $ ProgressBar�� � ��
 � ����� $ 	HighAlarm�� �� *!BaseAlarmSignal.DelayAlarmGrade+.a��� $ ProgressBar�� � ��
 � ����� $ LowAlarm�� �� *!BaseAlarmSignal.DelayAlarmGrade+.a��� $ ProgressBar�� � ��
 � ����� $ 
HighDanger�� �� *!BaseAlarmSignal.DelayAlarmGrade+.a��� $ ProgressBar�� � ��
 �  ���� $ 	LowDanger�� �� *!BaseAlarmSignal.DelayAlarmGrade+.a��� $ ProgressBar�� � ��
 �  �� �� $ Invalid�� �� *Device.Alarm+.a��� $ txtName�� � ��
 ��i���� $ pathIcon�� � ��
 ��i���� $ HighPreAlarm�� �� *Device.Alarm+.a��� $ txtName�� � ��
 � ����� $ pathIcon�� � ��
 � ����� $ LowPreAlarm�� �� *Device.Alarm+.a��� $ txtName�� � ��
 � ����� $ pathIcon�� � ��
 � ����� $ 	HighAlarm�� �� *Device.Alarm+.a��� $ txtName�� � ��
 � ����� $ pathIcon�� � ��
 � ����� $ LowAlarm�� �� *Device.Alarm+.a��� $ txtName�� � ��
 � ����� $ pathIcon�� � ��
 � ����� $ 
HighDanger�� �� *Device.Alarm+.a��� $ txtName�� � ��
 �  ���� $ pathIcon�� � ��
 �  ���� $ 	LowDanger�� �� *Device.Alarm+.a��� $ txtName�� � ��
 �  ���� $ pathIcon�� � ��
 �  ���� $ false�� �� *Device.IsRunning+.a��� $ txtRun�� � y�$ 	Collapsed=��� $ txtStop�� � y�$ Visible=��� $ 
gridheader�� � ��
 �  ���� $ pathIcon�� � ��
 �  ��