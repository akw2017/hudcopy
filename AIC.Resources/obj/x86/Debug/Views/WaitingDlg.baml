   M S B A M L   `   `   `  ����H  DAIC.Resources, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullC&clr-namespace:AIC.Resources.ConvertersAIC.Resources.Converters  5   	   $    AIC.Resources.Views.WaitingDlg   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   K GXamlAnimatedGif, Version=1.1.10.0, Culture=neutral, PublicKeyToken=null<gif2https://github.com/XamlAnimatedGif/XamlAnimatedGif  5   	   4convert&clr-namespace:AIC.Resources.Converters  5   	   $��30��5   	   $	��120��6   
������ &   System.Windows.Media.TextOptions    TextFormattingMode$  Display=�5   	    � 	Resources 5      8   2AIC.Resources.Converters.BoolToVisibilityConverter   boolToVisibilityConverter��%   &	          5	   
   6,   5
      .��5      � .+�5   
   ��  �� CornerRadius$ 5��6   $��30��6"   
���   �$��1q�6B   
������� J� Effect 5      3  -System.Windows.Media.Effects.DropShadowEffect  5         Color$ Black��6#   63   ._�5      ��  �� Orientation$ 
Horizontal=�6   .+�5      �� '  !XamlAnimatedGif.AnimationBehavior   	SourceUri$1 +/AIC.Resources;component/Images/loading.gif?�6      	AutoStart . $��28��6�   $��28��6�   $��5,0,5,0q�6�   �� 5      -   5        $��tbPrompt��5      $��Center=�6,   $��12(�6G   
���   ���6h   �� *
WaitInfo+6�   �� 5      -   5        $��	txtCancel��5      $��Center=�6-   �� 6J    �� Command 6T   �� *CancelCommmad+.�6v   .�6x   �� $��12(�6�   $��取消��6�   6�   ��5      �� 5      	 �� 
TargetType"	 ��6    .�5      �� 
 �� Property
 � y� �� Value$ 	Collapsed=�6;    �� Triggers 5      �� 5   "    �� Binding 6.   �� *	CanCancel+ �� Value True6L   .a�5   &   �� 
 � y�$ Visible=�6D   5   #   5      5      5      5       5!      5"      