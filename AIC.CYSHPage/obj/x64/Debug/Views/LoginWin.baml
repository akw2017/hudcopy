   M S B A M L   `   `   `  ����G  CAIC.HomePage, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null7 clr-namespace:AIC.HomePage.ViewsAIC.HomePage.Views  5   	   U QMahApps.Metro, Version=1.5.0.23, Culture=neutral, PublicKeyToken=f4fb5a3c4d1e5b4fV;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.MetroMahApps.Metro.Controls 5   	   =#clr-namespace:AIC.HomePage.ControlsAIC.HomePage.Controls  5   	   !    AIC.HomePage.Views.LoginWin   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   2local clr-namespace:AIC.HomePage.Views     5   	   PControls;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.Metro     5   	   1c#clr-namespace:AIC.HomePage.Controls     5   	   (  "MahApps.Metro.Controls.MetroWindow    ShowMinButton  .     ShowMaxRestoreButton .  $��2q�5   	      AccentColorBrush#���   6    � Title$ 登录��5   	   $��246.181��6   $��301.967��6%    � WindowStartupLocation$ CenterScreen=�65    � 
ResizeMode$ NoResize=�6Z    � Icon$6 0/AIC.HomePage;component/Images/wrench_orange.png��6p    � 	Resources 5       �� MergedDictionaries �� Source	 �� 
TargetType
 �� Property �� Value �� Orientation �� Triggers P� Property P� Value �� Self �� RelativeSource �� Path�� 5   
    5      �� 5      $1 +/AIC.HomePage;component/Themes/Generic.xaml?�6%   �� 5      �� 5      "	 ��6   .�5      �� 
 �@=�ErrorTemplate 5      �� 5   "   .R�5   &   �� $ 
Horizontal=�61   .+�5   *   �� 
���  ��$��Center=�6E   $��18(�6`   !6q   �� 5   *   5   '   5   #   5       5      P� 5       �@=�HasError . . �5   "   �� 
 � ��
 �  ��6@   �� 5    "   
 �@�ToolTip 6<   �� # Z $2 ,(Validation.Errors).CurrentItem.ErrorContent�5!      5#      5$      %
   '��       5&      .��5(      � -   5         �Name$ mygrid��5(        AccentColorBrush2#���  6   .+�5)   
   �� $��	用户：��6   $
��Left=�6   $��	38,59,0,0q�69   $	��Top=�6L    J� RenderTransformOrigin$ 
0.586,-0.5&�6d   �� 5*   
   -   5         ��Name$ txtIP��5*      $
��Left=�6!   $��23��6<   $��
113,59,0,0q�6H   $	��Top=�6\   $	��139��6t   ��5+      �� *User+ �� Mode$ TwoWay=� �� UpdateSourceTrigger$ PropertyChanged=� �� ValidatesOnExceptions .  �� ValidatesOnDataErrors .  �� NotifyOnValidationError . .��6�   5,      �� 5-   
   $��	密码：��6   $
��Left=�6   $��
38,104,0,0q�69   $	��Top=�6M   $ 
0.586,-0.5&�6e   G� 5.   
   -   5         G�Name$ txtPort��5.      $
��Left=�6'   $��23��6B   $��113,104,0,0q�6N   $	��Top=�6c   $	��139��6{   -   'AIC.HomePage.Controls.PasswordBoxHelper   Attach .    Password 50      �� *
Password+$ TwoWay=�$ PropertyChanged=�51      �� 52   
   -   5        $��btnOK��52      $��登录��6   $
��Left=�6+   $��
55,154,0,0q�6F   $	��Top=�6Z   $��77��6r   $ 
0.493,1.03&�6~    �� 	IsDefault . 6�   �� 53   
   -   5         ��Name$ btnClose��53      $��取消��6#   $
��Left=�60   $��153,154,0,0q�6K   $	��Top=�6`   $��77��6x   $ 
0.493,1.03&�6�     �� IsCancel  . 6�   �� 54   
   $
��Left=�60   $��	55,18,0,0q�6K   $	��Top=�6^   $ 
0.586,-0.5&�6v   ���	#FFFF0000��6   �� *Information+6�   56      57      