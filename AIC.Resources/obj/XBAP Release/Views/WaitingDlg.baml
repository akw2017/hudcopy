   M S B A M L   `   `   `  ���� H  DAIC.Resources, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullC&clr-namespace:AIC.Resources.ConvertersAIC.Resources.Converters  $    AIC.Resources.Views.WaitingDlg   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     K GXamlAnimatedGif, Version=1.1.10.0, Culture=neutral, PublicKeyToken=null<gif2https://github.com/XamlAnimatedGif/XamlAnimatedGif  4convert&clr-namespace:AIC.Resources.Converters  $��30��$	��120��
������ &   System.Windows.Media.TextOptions    TextFormattingMode$  Display=� � 	Resources 8   2AIC.Resources.Converters.BoolToVisibilityConverter   boolToVisibilityConverter��%   &	          .��� .+���  �� CornerRadius$ 5��$��30��
���   �$��1q�
������� J� Effect 3  -System.Windows.Media.Effects.DropShadowEffect     Color$ Black��._���  �� Orientation$ 
Horizontal=�.+��� '  !XamlAnimatedGif.AnimationBehavior   	SourceUri$1 +/AIC.Resources;component/Images/loading.gif?�   	AutoStart . $��28��$��28��$��5,0,5,0q��� -   $��tbPrompt��$��Center=�$��12(�
���   ����� *
WaitInfo+�� -   $��	txtCancel��$��Center=���  �� Command �� *CancelCommmad+.�.��� $��12(�$��取消������ 	 �� 
TargetType"	 ��.��� 
 �� Property
 � y� �� Value$ 	Collapsed=� �� Triggers ��  �� Binding �� *	CanCancel+ �� Value True.a��� 
 � y�$ Visible=�