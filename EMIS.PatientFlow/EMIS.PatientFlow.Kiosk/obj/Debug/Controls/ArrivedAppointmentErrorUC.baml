   M S B A M L   `   `   `  ����Q  MEMIS.PatientFlow.Kiosk, Version=5.0.0.0, Culture=neutral, PublicKeyToken=nullQ-clr-namespace:EMIS.PatientFlow.Kiosk.ControlsEMIS.PatientFlow.Kiosk.Controls  5      ?    9EMIS.PatientFlow.Kiosk.Controls.ArrivedAppointmentErrorUC   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      ?local-clr-namespace:EMIS.PatientFlow.Kiosk.Controls     5        � DataContext  5	      �� *ArrivedAppointmentError+   Locator �� Source# [   � 	Resources 5       �� Source�� 5   
   $# ../Converters/Converters.xaml?�6   %    .��5      �  � RowDefinitions 5   
   �� 5      $
��2.5* �6   �� 5      $
��4.5* �6   �� 5      $��2* �6    � ColumnDefinitions 5   
   �� 5      $��0.25* �6   �� 5      $��3* �6   �� 5      $��0.25* �6   .+�5   
   �� $��0��6   $��1��6"    �� Orientation$ Vertical=�62   $��Bottom=�6I   .+�5      ]� $��0,0,0,30q�6   .+�5      �� $
��Wrap=�6   $
��Left=�60     LightGrayColor#���  5       $��0,15,0,5q�6J   $
��Left=�5!      $	��Top=�67    �� TextAlignment$
 Left=�6O   ��5      �� *53SizeHelperModel.ResolutionHelper.TextBlockLargeFont+# [  �� 5"      �6   �� *HiText+ �� Mode$ TwoWay=� �� 5#        HighLightMessageStyleRun#���  6H   �6   �� *PatientNameText+$ TwoWay=�6H   5$      �� 5%        NoneButtonStyle#��[ 6   $��55��6B   $
��10,3q�6N   $	��200��5&      $	��400��6(   $��Right=�67   ��5'      �� *'%ArrivedAppointmentError.NotYouCommand+# [  .��5(      �� ��6    �� *
NotYouText+5)      5*      �� 5+      ���#DEDEDE$��2��6$   $��0,0,0,10q�68   $��Stretch=�5,      $��Center=�61   		 �� X2	 5-      �� *ActualWidth+
 �� RelativeSource
 �� *Self+5.      �� 50   
   -   5         ��Name$ ErrorButton��50      $��1��6&   $��1��63   $
��Left=�51      $��Stretch=�63   $��Stretch=�52      $	��Top=�6/   $	��150��53      $��0,20,0,0q�6!     ErrorAppointmentButtonStyle#��[ 54      .��55      ]� $
��Left=�6   $	��Top=�63   .+�56      �� $
��Left=�6   $��0,10,0,5q�57      $��40��6*   $
��Left=�65   $	��Top=�6P   ��56   .   �� *-+AppointmentDetail.ErrorAppointmentImagepath+57   P   �� 58      $
��Left=�6    �� Padding$ 5,0,5,0q�62   $
��Left=�59      $��Center=�67   ��5:      �� * AppointmentDetail.ErrorMessage+��5;      �� *53SizeHelperModel.ResolutionHelper.TextBlockLargeFont+# [  5<      5=      5>      �� 5@   
   $��2��6+   $��1��68   $
��0,15q�5A      $��70��6     CloseButtonStyle#���  5B      $��Stretch=�5D      $��Center=�6/   ��5@      �� *Close+��5C      �� *&$ArrivedAppointmentError.CloseCommand+# [  5D   /   5F      5G      