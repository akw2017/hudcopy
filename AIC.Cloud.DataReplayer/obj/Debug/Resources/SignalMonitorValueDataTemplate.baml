   M S B A M L   `   `   `  ����c  _System.Windows.Interactivity, Version=4.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35qPclr-namespace:System.Windows.Interactivity;assembly=System.Windows.InteractivitySystem.Windows.Interactivity  5      G CAIC.CoreType, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullA0clr-namespace:AIC.CoreType;assembly=AIC.CoreTypeAIC.CoreType 5      E AAIC.Themes, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullM5clr-namespace:AIC.Themes.Controls;assembly=AIC.ThemesAIC.Themes.Controls 5      Q MAIC.Cloud.Presentation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nulluOclr-namespace:AIC.Cloud.Presentation.Converters;assembly=AIC.Cloud.Presentation!AIC.Cloud.Presentation.Converters 5      Q MAIC.Cloud.DataReplayer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullU/clr-namespace:AIC.Cloud.DataReplayer.ViewModels!AIC.Cloud.DataReplayer.ViewModels 5      K*clr-namespace:AIC.Cloud.DataReplayer.ViewsAIC.Cloud.DataReplayer.Views 5	      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R	 NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089    booleanToVisibilityConverter@   :AIC.Cloud.Presentation.Converters.EnumDescriptionConverter  enumDescriptionConverter:  4AIC.Cloud.Presentation.Converters.IntToUnitConverter  intToUnitConverter  enumComboItemTemplate  �� Path �� 	Converter " VInfoContractComboItemTemplate � DataContext �� StringFormat  NoBorder �� 
TargetType �� Property �� Value  ReplayTemplate �� DataType>  8AIC.Cloud.DataReplayer.ViewModels.AMSReplayDataViewModel.   (System.Windows.Interactivity.Interaction   	Behaviors0  *AIC.Themes.Controls.FluidMouseDragBehavior	   
DragButton4  .AIC.Cloud.DataReplayer.Views.AMSReplayDataView  TimeDomainTemplate?  9AIC.Cloud.DataReplayer.ViewModels.TimeDomainDataViewModel5  /AIC.Cloud.DataReplayer.Views.TimeDomainDataView  FrequencyDomainTemplateD  >AIC.Cloud.DataReplayer.ViewModels.FrequencyDomainDataViewModel:	  4AIC.Cloud.DataReplayer.Views.FrequencyDomainDataView 	 PowerSpectrumTemplateB
  <AIC.Cloud.DataReplayer.ViewModels.PowerSpectrumDataViewModel8  2AIC.Cloud.DataReplayer.Views.PowerSpectrumDataView  
 PowerSpectrumDensityTemplateI  CAIC.Cloud.DataReplayer.ViewModels.PowerSpectrumDensityDataViewModel?  9AIC.Cloud.DataReplayer.Views.PowerSpectrumDensityDataView  OrthoTemplate:  4AIC.Cloud.DataReplayer.ViewModels.OrthoDataViewModel0  *AIC.Cloud.DataReplayer.Views.OrthoDataView  OffDesignConditionTemplateG  AAIC.Cloud.DataReplayer.ViewModels.OffDesignConditionDataViewModel=  7AIC.Cloud.DataReplayer.Views.OffDesignConditionDataView  OrderAnalysisTemplateB  <AIC.Cloud.DataReplayer.ViewModels.OrderAnalysisDataViewModel8  2AIC.Cloud.DataReplayer.Views.OrderAnalysisDataView  Time3DSpectrumTemplateC  =AIC.Cloud.DataReplayer.ViewModels.Time3DSpectrumDataViewModel5  /AIC.Cloud.DataReplayer.Views.Time3DSpectrumView  RPM3DSpectrumTemplateB  <AIC.Cloud.DataReplayer.ViewModels.RPM3DSpectrumDataViewModel8  2AIC.Cloud.DataReplayer.Views.RPM3DSpectrumDataView  AlarmPointTrendTemplateD  >AIC.Cloud.DataReplayer.ViewModels.AlarmPointTrendDataViewModel:  4AIC.Cloud.DataReplayer.Views.AlarmPointTrendDataView�� 5      F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     :x,http://schemas.microsoft.com/winfx/2006/xaml    	 5      ViPclr-namespace:System.Windows.Interactivity;assembly=System.Windows.Interactivity  5      =coreType0clr-namespace:AIC.CoreType;assembly=AIC.CoreType  5      BControls5clr-namespace:AIC.Themes.Controls;assembly=AIC.Themes  5      abaseConverterOclr-namespace:AIC.Cloud.Presentation.Converters;assembly=AIC.Cloud.Presentation  5      9presentation&http://www.aicmonitor.com/Presentation  5      =	viewModel/clr-namespace:AIC.Cloud.DataReplayer.ViewModels  5      4views*clr-namespace:AIC.Cloud.DataReplayer.Views  5	      %  &	        &	      &	 &     &	 9     7  &	 |     &	 R    &	 �    &	 �    &	 w    7   &		 t    &	
 P    &	 ,    &	     &	 �    &	 �    &	 �	    &	 x
    �� 5      6#      5      6-     5      6'   �� 5      .R�5   
   �� ��6   �� $  .�8   5      �� 5      .R�5   
   ��  6   �� ��5      `� 5      $ 	{0}{1}{2}��6   .1�5      �� $  DevSN�6   �� 5      $
  Name�6   �� 5      $
  MSSN�6   5      5      5      �� 5       " ��6   .�5!   
   ��  � ��$ 0q�6,   �� 5"   
    � ��
 �   �6(   5#      �� 5%      "  6*   .R�5&   
   � ��6   �� *	ItemWidth+��6+   �� *
ItemHeight+ 5'        5(      $
	 Left=�62   6F   .+�5*      � $��3q�6   .+�5+        5,      5-      5.      �� 50      "  6.   .R�51   
   � ��6   �� *	ItemWidth+��6+   �� *
ItemHeight+ 52        53      $
	 Left=�62   6F   .+�55      � $��3q�6   .+�56        57      58      59      �� 5;      "  63   .R�5<   
   � ��6   �� *	ItemWidth+��6+   �� *
ItemHeight+ 5=        5>      $
	 Left=�62   6F   .+�5@      � $��3q�6   .+�5A      	  y�60   �� *	IsVisible+8   5B      5C      5D      �� 5F      " 
 61   .R�5G   
   � ��6   �� *	ItemWidth+��6+   �� *
ItemHeight+ 5H        5I      $
	 Left=�62   6F   .+�5K      � $��3q�6   .+�5L        5M      5N      5O      �� 5Q      "  68   .R�5R   
   � ��6   �� *	ItemWidth+��6+   �� *
ItemHeight+ 5S        5T      $
	 Left=�62   6F   .+�5V      � $��3q�6   .+�5W        5X      5Y      5Z      �� 5\      "  6)   .R�5]   
   � ��6   �� *	ItemWidth+��6+   �� *
ItemHeight+ 5^        5_      $
	 Left=�62   6F   .+�5a      � $��3q�6   .+�5b        5c      5d      5e      �� 5g      "  66   .R�5h   
   � ��6   �� *	ItemWidth+��6+   �� *
ItemHeight+ 5i        5j      $
	 Left=�62   6F   .+�5l      � $��3q�6   .+�5m        5n      5o      5p      �� 5r      "  61   .R�5s   
   � ��6   �� *	ItemWidth+��6+   �� *
ItemHeight+ 5t        5u      $
	 Left=�62   6F   .+�5w      � $��3q�6   .+�5x        5y      5z      5{      �� 5}      "  62   .R�5~   
   � ��6   �� *	ItemWidth+��6+   �� *
ItemHeight+ 5        5�      $
	 Left=�62   6F   .+�5�      � $��3q�6   .+�5�        5�      5�      5�      �� 5�      "  61   .R�5�   
   � ��6   �� *	ItemWidth+��6+   �� *
ItemHeight+ 5�        5�      $
	 Left=�62   6F   .+�5�      � $��3q�6   .+�5�        5�      5�      5�      �� 5�      "  63   .R�5�   
   � ��6   �� *	ItemWidth+��6+   �� *
ItemHeight+ 5�        5�      $
	 Left=�62   6F   .+�5�      � $��3q�6   .+�5�        5�      5�      5�      5�      