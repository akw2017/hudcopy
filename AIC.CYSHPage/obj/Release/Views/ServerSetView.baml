   M S B A M L   `   `   `  ���� G  CAIC.HomePage, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:AIC.HomePageAIC.HomePage  T PWpf.PageNavigationControl, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullhJclr-namespace:Wpf.PageNavigationControl;assembly=Wpf.PageNavigationControlWpf.PageNavigationControl C ?AIC.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullI2clr-namespace:AIC.Core.ExCommand;assembly=AIC.CoreAIC.Core.ExCommand &     AIC.HomePage.Views.ServerSetView   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     ,localclr-namespace:AIC.HomePage     c _System.Windows.Interactivity, Version=4.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity  P LPrism.Wpf, Version=6.2.0.0, Culture=neutral, PublicKeyToken=91a96d2a154366d8$prismhttp://prismlibrary.com/  UpageJclr-namespace:Wpf.PageNavigationControl;assembly=Wpf.PageNavigationControl  @command2clr-namespace:AIC.Core.ExCommand;assembly=AIC.Core  !  Prism.Mvvm.ViewModelLocator    AutoWireViewModel  . .���  � RowDefinitions �� $��40 ��� $��* ��� $
��Auto �.+��� $��0�� �� Orientation$ 
Horizontal=�.+���    
serverName#���   $��4,0,0,0q�$��Center=��� $��4,0,0,0q�  MetroTextBox#���  $	��100��$��Center=����� *
SearchName+ �� Mode$ OneWayToSource=���   AccentedSquareButtonStyle#���  $��4,0,0,0q����� *QueryCommand+���� $ 
Horizontal=�.+��� $
��None=�$0��*/AIC.Resources;component/Images/search.png����  
 search#���  $��Center=���   serverManagement#���  $��Center=�$��20,0,0,0q��� $��4,0,0,0q�#���  $	��100��$��Center=� �� 
IsReadOnly .  � DataContext ��  �� ElementName$ table�� �� Path$ SelectedItem����� $ IP��� #���  $��4,0,0,0q����� *
AddCommand+���� *SelectedItem+$ table������ $ 
Horizontal=�.+��� $
��None=�$-��'/AIC.Resources;component/Images/add.png����   add#���  $��Center=��� #���  $��4,0,0,0q����� *EditCommand+���� *SelectedItem+$ table������ $ 
Horizontal=�.+��� $
��None=�$0��*/AIC.Resources;component/Images/pencil.png����   edit#���  $��Center=��� #���  $��4,0,0,0q����� *DeleteCommand+���� *SelectedItem+$ table������ $ 
Horizontal=�.+��� $
��None=�$1��+/AIC.Resources;component/Images/delete3.png����  
 delete#���  $��Center=��� #���  $��4,0,0,0q����� *DefaultCommand+���� *SelectedItem+$ table������ $ 
Horizontal=�.+��� $
��None=�$.��(/AIC.Resources;component/Images/tick.png����   default#���  $��Center=�&   System.Windows.Controls.DataGrid  -     Name$ table��$��1��	   AutoGenerateColumns	 .  
   FrozenColumnCount$
 1��   
IsReadOnly .    ColumnWidth5  /System.Windows.Controls.DataGridLengthConverter$ * ���� *ServerInfoShow+   Columns 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn   Header$ ID��   MinWidth$ 60Q�1  +System.Windows.Controls.DataGridBoundColumn   Binding �� *ID+  # �   $ 60Q� �� *Name+   	 serverIP# � 	 $ 60Q� �� *IP+   
 organizationName# � 
 $ 60Q� �� *	Factory+    	longitude# �  $ 60Q� �� *	Longitude+    latitude# �  $ 60Q� �� *
Latitude+4  .System.Windows.Controls.DataGridTemplateColumn    defaultlogin# �  $ 60Q�   CellTemplate �� .R���  ��Name$ image��$��Center=�$6��0/AIC.Resources;component/Images/bullet_minus.png��$��16��$��16�� �� Triggers ��  �� Value$
 True�� �� Binding �� *	IsLogin+.a���  �� 
TargetName$ image�� �� Property � �� �� Value$. (/AIC.Resources;component/Images/tick.png��    
servermode# �  $ 60Q� �� .R��� $ image��$��Center=�$2��,/AIC.Resources;component/Images/computer.png��$��16��$��16�� �� $
 True�� �� *	IsCloud+.a��� $ image�� � ��$7 1/AIC.Resources;component/Images/weather_cloud.png��    loginResult# �  $ 60Q� �� .R��� $ image��$��Center=�$6��0/AIC.Resources;component/Images/bullet_minus.png��$��16��$��16�� �� $
 True�� �� *LoginResult+.a��� $ image�� � ��$. (/AIC.Resources;component/Images/tick.png��    permissions# �  $ 60Q� �� *
Permission+   ColumnHeaderStyle ��   MetroDataGridColumnHeader �� BasedOn# [  �� 
TargetType=  7System.Windows.Controls.Primitives.DataGridColumnHeader"  .���  � ��  AccentColorBrush3# �  ����   MetroDataGrid# [ "  .���   �@ AlternatingRowBackground  GrayBrush10# �  .	  (System.Windows.Interactivity.Interaction 	  Triggers /
  )System.Windows.Interactivity.EventTrigger
   
  	EventName$ MouseDoubleClick��.  (System.Windows.Interactivity.TriggerBase   Actions. 6  0System.Windows.Interactivity.InvokeCommandAction     Command �� *EditCommand+    CommandParameter  �� *SelectedItem+$ table��.  (Wpf.PageNavigationControl.PageNavigation  -   $��pager��$��2��!   PageSizeList! �� *PageSizeList+"   PageSize" �� *
PageSize+#   
TotalItems# �� *
TotalItems+$ TwoWay=�$   CurrentPage$ �� *CurrentPage+$ TwoWay=� 
  $ CurrentPageChanged��. .  (AIC.Core.ExCommand.ExInvokeCommandAction  %   Command% �� *CurrentPageChangedComamnd+&   CommandParameter& �� $ pager��