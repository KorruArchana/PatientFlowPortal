   M S B A M L   `   `   `  ����c  _System.Windows.Interactivity, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35qPclr-namespace:System.Windows.Interactivity;assembly=System.Windows.InteractivitySystem.Windows.Interactivity  5      Q MEMIS.PatientFlow.Kiosk, Version=5.0.0.0, Culture=neutral, PublicKeyToken=nullQ-clr-namespace:EMIS.PatientFlow.Kiosk.ControlsEMIS.PatientFlow.Kiosk.Controls 5      C   =EMIS.PatientFlow.Kiosk.Controls.MultiplePatientsExceptionPage   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      ^iPclr-namespace:System.Windows.Interactivity;assembly=System.Windows.Interactivity     5      ?local-clr-namespace:EMIS.PatientFlow.Kiosk.Controls     5        � DataContext  5
      �� *MultiplePatientsExceptionPage+   Locator �� Source# [   � 	Resources 5       �� Source�� 5   
   $# ../Converters/Converters.xaml?�6   %    .   (System.Windows.Interactivity.Interaction   Triggers 5      /   )System.Windows.Interactivity.EventTrigger  5   
      	EventName$ Loaded��6   .   (System.Windows.Interactivity.TriggerBase   Actions. 5      6   0System.Windows.Interactivity.InvokeCommandAction     Command 6$   �� *LoadedCommand+5      .��5      �  � RowDefinitions 5   
   �� 5      $��3* �6   �� 5      $��7* �6   	 � ColumnDefinitions	 5   
   �� 5      $
��0.5* �6   �� 5      $��3* �6   �� 5      $
��0.5* �6   .+�5"   
   �� -   5        
 ��Name$%
 CannotFindAppTextWithLineStkPnl��5"      $��0��6>   $��1��6K   $��Bottom=�5#       �� Orientation$ Vertical=�60   .+�5$      ]� .+�5%      �� $
��Left=�6     ErrorCloseImageSource#���  6.   $��60��5&      $��
0,10,20,10q�6"   �� 5(      $
��Left=�6   $��	5,10,0,10q�62     HighLightMessageStyle#��[ 5)      $
��Wrap=�6K   ��5(   E   �� *CantFindText+��5*      �� *64SizeHelperModel.ResolutionHelper.TextBlockXLargeFont+# [  5+      5,      �� 5.      ���#DEDEDE$��2��6$   $��0,30,0,0q�68   $��Stretch=�5/      $��Center=�61   	 �� X2 50      �� *ActualWidth+ �� ElementName$% CannotFindAppTextWithLineStkPnl��52      �� 54   
   $��1��6   $��1��6%   $
��0,30q�62   $��Stretch=�6@   .+�55      � 	 56      �� 57      $��2* �6'   �� 58      $��2* �6'   .+�5;      �� $��0��6    �� Padding$ 0,0,15,0q�6S    �� TextAlignment$
 Left=�5<      
���������5;   ,   �� *
ColumnSpan+��5<   1   �� *CheckInInfoText+��5=      �� *53SizeHelperModel.ResolutionHelper.TextBlockLargeFont+# [  �� 5?      $��1��6   $��Right=�6-   y�5@      �� * IsPatientEnteredDetailsVisible+   BoolToVisibleHiddenCollapsed �� 	Converter# [ .+�5A      �� $��5q�6   ���#F1F1F1$	��100��6?   $	��400��6O   $��Hidden=�5B      $��Hidden=�6S   $��0q�5C      $
��Left=�63   ��6N   �� *PatientMatchList+ �� Mode$ TwoWay=���5E      �� 5F      .R�5G   "   �  � Orientation$ Vertical=�6,   5H      ��5K      �� 5L      .R�5N   "   �� $��20,10,20,10q�6,   $
��Left=�6A   $��Center=�6\   
�������$��DemiBold"�5P   >   ��5O   ,   �� *64SizeHelperModel.ResolutionHelper.TextBlockMediumFont+# [  �� 5Q   &   �6*   ��  �� Path$	 Key�$ OneWay=� �� 5R   &   ���#087A6F�6*   �� $ Value�$ OneWay=�6S   5S   #   5U      5W      5X      5Y      �� 5[      $
��0,20q�6   $ 
Horizontal=�6'   $
��Left=�6@   $��Center=�6[   .+�5\      �� $��50��6   $	��350��6(   $
��0,20q�67     DefaultButtonStyle#���  5^      ��5]      �� *Close+��5_      �� *,*MultiplePatientsExceptionPage.CloseCommand+# [  �� 5a      $��50��6   $	��350��6(   $��
30,20,0,20q�68   #���  5c      ��5b      �� *
TryAgain+��5d      �� */-MultiplePatientsExceptionPage.TryAgainCommand+# [  5e      5f      5g      5i      