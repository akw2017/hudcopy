   M S B A M L   `   `   `  ����G  CAIC.HomePage, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null7 clr-namespace:AIC.HomePage.ViewsAIC.HomePage.Views  5   	   U QMahApps.Metro, Version=1.5.0.23, Culture=neutral, PublicKeyToken=f4fb5a3c4d1e5b4fV;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.MetroMahApps.Metro.Controls 5   	   %    AIC.HomePage.Views.ServerSetWin   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   2local clr-namespace:AIC.HomePage.Views     5   	   PControls;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.Metro     5   	   (  "MahApps.Metro.Controls.MetroWindow    ShowMinButton  .     ShowMaxRestoreButton .  $��2q�5
   	      AccentColorBrush#���   6    � Title$ 设置��5   	   $��341.143��6   $��452.834��6%    � WindowStartupLocation$ CenterScreen=�65    � 
ResizeMode$ NoResize=�6Z    � Icon$6 0/AIC.HomePage;component/Images/wrench_orange.png��6p    � 	Resources 5       �� MergedDictionaries �� Source	 �� 
TargetType
 �� Property �� Value �� Orientation �� Triggers P� Property P� Value �� Self �� RelativeSource �� Path�� 5   
    5      �� 5      $1 +/AIC.HomePage;component/Themes/Generic.xaml?�6%   �� 5      �� 5      "	 ��6   .�5      �� 
 �@=�ErrorTemplate 5      �� 5   "   .R�5   &   �� $ 
Horizontal=�61   .+�5   *   �� 
���  ��$��Center=�6E   $��18(�6`   !6q   �� 5   *   5   '   5   #   5       5      P� 5       �@=�HasError . . �5   "   �� 
 � ��
 �  ��6@   �� 5   "   
 �@�ToolTip 6<   �� # Z $2 ,(Validation.Errors).CurrentItem.ErrorContent�5       5"      5#      %
   '��       5%      .��5'      � -   5         �Name$ mygrid��5'        AccentColorBrush2#���  6   .+�5(   
   �� $��服务器名：��6   $
��Left=�6    $��	34,19,0,0q�6;   $	��Top=�6N    J� RenderTransformOrigin$ 
0.586,-0.5&�6f   �� 5)   
   -   5         ��Name$ txtName��5)      $
��Left=�6#   $��23��6>   $��
109,19,0,0q�6J   $	��Top=�6_   $	��297��6w   ��5*      �� *Name+ �� Mode$ TwoWay=� �� UpdateSourceTrigger$ PropertyChanged=� �� ValidatesOnExceptions .  �� ValidatesOnDataErrors .  �� NotifyOnValidationError . .��6�   5+      �� 5,   
   $��服务器IP：��6   $
��Left=�6!   $��	34,49,0,0q�6<   $	��Top=�6O   $ 
0.586,-0.5&�6g   �� 5-   
   -   5        $ txtIP��5-      $
��Left=�6!   $��23��6<   $��
109,49,0,0q�6H   $	��Top=�6\   $	��297��6t   ��5.      �� *IP+$ TwoWay=�$ PropertyChanged=� .  .  . .��6�   5/      �� 50   
   $��服务器端口：��6   $
��Left=�6!   $��	34,79,0,0q�6<   $	��Top=�6O   $ 
0.586,-0.5&�6g   �� 51   
   -   5        $ txtPort��51      $
��Left=�6#   $��23��6>   $��
109,79,0,0q�6J   $	��Top=�6^   $	��297��6v   ��52      �� *Port+$ TwoWay=�$ PropertyChanged=� .  .  . .��6�   53      �� 54   
   $��机构名称：��6   $
��Left=�6    $��
34,109,0,0q�6;   $	��Top=�6O   $ 
0.586,-0.5&�6g   �� 55   
   -   5        $ 
txtFactory��55      $
��Left=�6&   $��23��6A   $��109,109,0,0q�6M   $	��Top=�6b   $	��297��6z   ��56      �� *	Factory+$ TwoWay=�$ PropertyChanged=� .  .  . .��6�   57      �� 58   
   $��	经度：��6   $
��Left=�6   $��
34,139,0,0q�69   $	��Top=�6M   $ 
0.586,-0.5&�6e   �� 59   
   -   5        $ txtLongitude��59      $
��Left=�6(   $��23��6C   $��109,139,0,0q�6O   $	��Top=�6d   $	��297��6|   ��5:      �� *	Longitude+$ TwoWay=�$ PropertyChanged=� .  .  . .��6�   5;      �� 5<   
   $��	纬度：��6   $
��Left=�6   $��
34,169,0,0q�69   $	��Top=�6M   $ 
0.586,-0.5&�6e   �� 5=   
   -   5        $ txtLatitude��5=      $
��Left=�6'   $��23��6B   $��109,169,0,0q�6N   $	��Top=�6c   $	��297��6{   ��5>      �� *
Latitude+$ TwoWay=�$ PropertyChanged=� .  .  . .��6�   5?      �� 5@   
   $��默认登录：��6   $
��Left=�6    $��
34,199,0,0q�6;   $	��Top=�6O   $ 
0.586,-0.5&�6g   /  )MahApps.Metro.Controls.ToggleSwitchButton  5A   
   -   5          Name$ 
btnIsLogin��5A   &   $��110,194,37,79q�6:   $
��Auto��6Q   �� 5B   
   $��设为云服务：��6   $
��Left=�6!   $��
33,229,0,0q�6<   $	��Top=�6P   $ 
0.586,-0.5&�6h     5C   
   -	   5        $ 
btnIsCloud��5C   &   $��110,223,37,50q�6:   $
��Auto��6Q   �� 5D   
   -
   5        $��btnOK��5D      $��确定��6   $
��Left=�6+   $��130,263,0,0q�6F   $	��Top=�6[   $��77��6s   $ 
0.493,1.03&�6   6�   �� 5E   
   -   5         ��Name$ btnClose��5E      $��取消��6#   $
��Left=�60   $��228,263,0,0q�6K   $	��Top=�6`   $��77��6x   $ 
0.493,1.03&�6�   6�   5G      5H      