   M S B A M L   `   `   `  ����d  `GalaSoft.MvvmLight.Extras, Version=5.0.2.32240, Culture=neutral, PublicKeyToken=f46ff315b1088208jKclr-namespace:GalaSoft.MvvmLight.Command;assembly=GalaSoft.MvvmLight.ExtrasGalaSoft.MvvmLight.Command  5       EMIS.PatientFlow.Kiosk:   4EMIS.PatientFlow.Kiosk.Controls.DemographicDetailsUc   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      c _System.Windows.Interactivity, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity  5      YcommandKclr-namespace:GalaSoft.MvvmLight.Command;assembly=GalaSoft.MvvmLight.Extras  5        � DataContext  5
      �� *PatientDemographicDetails+   Locator �� Source# [   � 	Resources 5       �� Source�� 5   
   $# ../Converters/Converters.xaml?�6   %    .  (System.Windows.Interactivity.Interaction   Triggers 5      /  )System.Windows.Interactivity.EventTrigger  5   
      	EventName$ Loaded��6   .  (System.Windows.Interactivity.TriggerBase   Actions. 5      6  0System.Windows.Interactivity.InvokeCommandAction     Command 6$   �� *LoadedCommand+5      .��5      �  � RowDefinitions 5   
   �� 5      $��2* �6   �� 5      $��8* �6   	 � ColumnDefinitions	 5   
   �� 5      $��0.25* �6   �� 5      $��2* �6   �� 5      $��0.25* �6   .+�5"   
   �� $��0��6   $��1��6"   
 �� Orientation$
 Vertical=�62   .+�5#      ]� $��	0,20,0,30q�6   .+�5$      �� $
��Left=�6   $
��Left=�62   $	��Top=�5%       �� TextAlignment$
 Left=�64     LightGrayColor#���  5'      $��0,15,0,5q�6J   ��5&      �� *53SizeHelperModel.ResolutionHelper.TextBlockLargeFont+# [  �� 5(      �6   �� *
ThanksText+ �� Mode$ TwoWay=� �� 5)        HighLightMessageStyleRun#���  6H   �6   �� *PatientNameText+$ TwoWay=�6H   5*      51      �� 53      ���#DEDEDE$��2��6$   $��0,0,0,10q�68   $��Stretch=�54      $��Center=�61   	 �� X2 55      �� *ActualWidth+ �� RelativeSource �� *Self+56      �� 58   
   $��1��6   $��1��6"   $
 Vertical=�62   .+�59      �� $
 Vertical=�6   $��30��60   .+�5:      �� $��0,0,0,10q�6   $
��Left=�6.   $	��Top=�6I   #���  6a   ��6�   �� *MessagesTitleText+��5;      �� *64SizeHelperModel.ResolutionHelper.TextBlockMediumFont+# [  �� 5=      
�������$��0,0,0,20q�6M   $
��Left=�5>      $	��Top=�63   ��5=      �� *PageTitleText+��5?      �� *53SizeHelperModel.ResolutionHelper.TextBlockSmallFont+# [  y�5@      �� *	MaskTitle+   BoolToVisibleHiddenCollapsed �� 	Converter# [ 5A      �� 5C      
�������$��1q�6(   $��0,0,0,20q�6<   ���#F5F5F5._�5D      �� -   5         ��Name$  DemographicDetailsListView��5D      $
��0,10q�6?   $��Stretch=�5E      ��5F      �� *DemographicListCollection+$ TwoWay=���5H      �� 5I      .R�5J      �� 5K      ��5N      �� 5O      .R�5P      � $	��0,5q�6#   ��60   �� *ActualWidth+ �� ElementName$  DemographicDetailsListView��	 5R   "   �� 5S   &   $��5* �67   �� 5T   &   $��5* �67   .+�5W   "   �� $��0��6,   $��CharacterEllipsis=�6<    �� Padding$ 5q�6]   $��Center=�5X   ,   $��Right=�6G   $ Right=�5Y   ,   
���������6B   �� *Name+��5Z   ,   �� *64SizeHelperModel.ResolutionHelper.TextBlockMediumFont+# [  �� 5[   "   $��1��6,   $��CharacterEllipsis=�6<   $��Center=�5\   ,   $
��Left=�6G   $ 5q�6b   $
 Left=�5]   ,   #���  5^   ,   $��SemiBold"�6Z   ��5]   A   �� *
DetailText+��5_   ,   �� *64SizeHelperModel.ResolutionHelper.TextBlockMediumFont+# [  5`      5a      5c      5d      �� 5f      $��0,5,0,20q�5g      $��Center=�5h      $��Center=�65   #���  5i      $
��Bold"�6F   ��5f      �� *53SizeHelperModel.ResolutionHelper.TextBlockLargeFont+# [  ��5g   *   �� *DetailsUptodateText+5i   F   �� 5k      $��Center=�6   $
 
Horizontal=�64   $��Center=�6M   .+�5l      �� $��10,10,20,10q�6   $	��380��6.   $��65��6:     DefaultButtonStyle#���  5m      ���#086460��5n   .   �� *NextButtonText+��5o      �� *'%PatientDemographicDetails.NextCommand+# [  �� 5q      $��10,10,10,10q�6   $	��380��6/   $��65��6;   #���  5r      ��6F   �� *CancelButtonText+��5s      �� *)'PatientDemographicDetails.CancelCommand+# [  5t      5v      5w      5y      