   M S B A M L   `   `   `  ����Q  MEMIS.PatientFlow.Kiosk, Version=5.0.0.0, Culture=neutral, PublicKeyToken=nullQ-clr-namespace:EMIS.PatientFlow.Kiosk.ControlsEMIS.PatientFlow.Kiosk.Controls  5      c _System.Windows.Interactivity, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35qPclr-namespace:System.Windows.Interactivity;assembly=System.Windows.InteractivitySystem.Windows.Interactivity 5      9    3EMIS.PatientFlow.Kiosk.Controls.SingleAppointmentUC   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      ?local-clr-namespace:EMIS.PatientFlow.Kiosk.Controls     5      ^iPclr-namespace:System.Windows.Interactivity;assembly=System.Windows.Interactivity     5        � DataContext  5
      �� *SingleAppointment+   Locator �� Source# [   � 	Resources 5       �� Source�� 5   
   $# ../Converters/Converters.xaml?�6   %    .��5      �  � RowDefinitions 5   
   �� 5      $
��2.5* �6   �� 5      $
��4.5* �6   �� 5      $��2* �6    � ColumnDefinitions 5   
   �� 5      $��0.25* �6   �� 5      $��3* �6   �� 5      $��0.25* �6   .+�5   
   �� $��0��6   $��1��6"    �� Orientation$ Vertical=�62   $��Bottom=�6I   .+�5      ]� $��0,0,0,30q�6   .+�5       �� $
��Wrap=�6   $
��Left=�60     LightGrayColor#���  5"      $��0,15,0,5q�6J   $
��Left=�5#      $	��Top=�67    �� TextAlignment$
 Left=�6O   ��5!      �� *53SizeHelperModel.ResolutionHelper.TextBlockLargeFont+# [  �� 5$      �6   �� *HiText+ �� Mode$ TwoWay=� �� 5%        HighLightMessageStyleRun#���  6H   �6   �� *PatientNameText+$ TwoWay=�6H   5&      �� 5'        NoneButtonStyle#��[ 6   $��55��6B   $
��10,3q�6N   $	��200��5(      $	��400��6(   $��Right=�67   ��5)      �� *!SingleAppointment.NotYouCommand+# [  .��5*      �� ��6    �� *
NotYouText+5+      5,      �� 5-      ���#DEDEDE$��2��6$   $��0,0,0,10q�68   $��Stretch=�5.      $��Center=�61   		 �� X2	 5/      �� *ActualWidth+
 �� RelativeSource
 �� *Self+50      �� 52   
   $��1��6   $��1��6"   $��0,20,0,0q�63   y�53      �� *AppointmentDetail.IsEnabled+   BoolToVisibleHiddenCollapsed �� 	Converter# [ .+�54      ��  �� Padding$ 10q�6   $
 Left=�6%   #���  6:   ��55      �� *53SizeHelperModel.ResolutionHelper.TextBlockLargeFont+# [  �� 56      �6   �� *AppointmentText+ �� 57      ��6   �� *(&AppointmentDetail.AppointmentTimeStyle+�6U   �� *AppointmentDetail.Time+ �� 58      �6   �� *DataContext.WithText+
 ��  �� Mode$ FindAncestor=� �� AncestorType" �� �� 59      $
��Bold"�6   �6(   �� *AppointmentDetail.Name+5:      �� 5<      $��0,20,0,0q�6   $	��100��6'     NextButtonStyle#��[ 5=      $��Center=�5?      $��Center=�65   ��5>      �� *" SingleAppointment.ConfirmCommand+# [  .��5@      �� $ 
Horizontal=�6   .+�5A      �� $
��Bold"�5B       $ 5q�62   ��5A   !   �� *75SizeHelperModel.ResolutionHelper.TextBlockXXLargeFont+# [  �� 5C      �6   �� *CheckInText+ �� 5D      $�>��6   5F      5G      5H      5I      �� 5K   
   -   5         ��Name$ ErrorButton��5K      $��1��6&   $��1��63   $
��Left=�5L      $��Stretch=�63   $��Stretch=�5M      $	��Top=�6/   $	��150��5N      $��0,20,0,0q�6!     ErrorAppointmentButtonStyle#��[ 5O      y�5P      �� *)'AppointmentDetail.IsErrorMessageVisible+# [ .��5Q      ]� $
��Left=�6   $	��Top=�63   .+�5R      �� $
��Left=�6   $��0,10,0,5q�5S      $��40��6*   $
��Left=�65   $	��Top=�6P   ��5R   .   �� *-+AppointmentDetail.ErrorAppointmentImagepath+5S   P   �� 5T      $
��Left=�6   $ 5,0,5,0q�62   ��5U      �� *53SizeHelperModel.ResolutionHelper.TextBlockLargeFont+# [  �� 5V      $��SemiBold"�6B   �6   �� *AppointmentDetail.Time+6B    �� 5W      �6   �� *DataContext.WithText+
 �� $ FindAncestor=�" �� �� 5X      $��SemiBold"�6B   �6   �� *AppointmentDetail.Name+6B   �� 5Y      �� 5Z      �6   �� * AppointmentDetail.ErrorMessage+5[      5\      5]      �� 5_   
   $��2��6+   $��1��68   $
��0,15q�5`      $��70��6     CloseButtonStyle#���  5a      $��Stretch=�5c      $��Center=�6/   ��5_      �� *Close+��5b      �� * SingleAppointment.CloseCommand+# [  y�5d      �� *)'AppointmentDetail.IsErrorMessageVisible+# [ 5f      5g      