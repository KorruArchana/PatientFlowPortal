   M S B A M L   `   `   `  ����c  _System.Windows.Interactivity, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35qPclr-namespace:System.Windows.Interactivity;assembly=System.Windows.InteractivitySystem.Windows.Interactivity  5      Q MEMIS.PatientFlow.Kiosk, Version=5.0.0.0, Culture=neutral, PublicKeyToken=nullQ-clr-namespace:EMIS.PatientFlow.Kiosk.KeyBoardEMIS.PatientFlow.Kiosk.KeyBoard 5      U/clr-namespace:EMIS.PatientFlow.Kiosk.Converters!EMIS.PatientFlow.Kiosk.Converters 5      .   (EMIS.PatientFlow.Kiosk.Controls.FinishUc   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      ^iPclr-namespace:System.Windows.Interactivity;assembly=System.Windows.Interactivity     5      ;k-clr-namespace:EMIS.PatientFlow.Kiosk.KeyBoard     5      F
converters/clr-namespace:EMIS.PatientFlow.Kiosk.Converters     5        � DataContext  5      �� *Finish+   Locator �� Source# [   � 	Resources 5       �� MergedDictionaries �� Source�� 5   
    5      �� 5      $# ../Converters/Converters.xaml?�6%   �� 5      $- '../Converters/ActualWidthConverter.xaml?�6%   �� 5      $8 2../Converters/AppointmentButtonStyleConverter.xaml?�6%   %    5      .   (System.Windows.Interactivity.Interaction   Triggers 5      /   )System.Windows.Interactivity.EventTrigger  5   
      	EventName$ Loaded��6   .   (System.Windows.Interactivity.TriggerBase   Actions. 5      6   0System.Windows.Interactivity.InvokeCommandAction     Command 6$   �� *LoadedCommand+5      .��5      � -   5        	 �Name$	 MainGrid��5      .+�5   
   � y�6   �� *IsGridVisible+   BoolToVisibleHiddenCollapsed
 �� 	Converter#
 [  � RowDefinitions 5       �� 5!      $
��2.2* �6    �� 5"      $
��6.6* �6    �� 5#      $
��1.2* �6     � ColumnDefinitions 5&      �� 5'      $
��0.3* �6#   �� 5(      $��7* �6#   �� 5)      $
��0.3* �6#   �� 5*      $��3* �6#   �� 5+      $
��0.2* �6#   .+�5.      ]� -   5         ]�Name$ ThanksStkPnl��5.      $��0��6.   $��1��6;   ��6K   �� *AppointmentsColumnSpan+.+�50      �� $��Bottom=�6   ���#DEDEDE$��2��6@   $��0,0,0,5q�6T   $��Stretch=�51      $��Center=�61   	 �� X2 52      �� *ActualWidth+ �� ElementName$ ThanksStkPnl���� 54      $	��Top=�6     LightGrayColor#���  61   $��0,20,0,8q�6_   ��55      �� *53SizeHelperModel.ResolutionHelper.TextBlockLargeFont+# [  �� 56      �6   �� *
ThanksText+ �� 57      �6   �� *PatientNameText+�� 6=   $�.��6A   58      ]� 5:      $	��Top=�6   $��0,0,0,0q�62   .+�5;      �� $
��Left=�6     CheckImageSource#��[ 62   $��0,0,20,0q�5<      $��50��6*   �� 5>      $
��Left=�6    $
��Bold"�66   #���  6H   $��Center=�5@        �� TextAlignment$
 Left=�6;   ��5?       �� *53SizeHelperModel.ResolutionHelper.TextBlockLargeFont+# [  ��5A       �� *CheckedAppointmentsText+5B      5D      ]� 5F      -   5        $. (DirectionalAlertAndAppointmentsDockPanel��5F      $��1��6J   $��2��6W   $��1��6h   ��6x   �� *AppointmentsColumnSpan+.+�5G      �� -   5         ��Name$ DirectionalAlertsListView��5G   0   $	��Top=�6   $��0q�5H   -   $��Hidden=�6A   $��	0,10,0,10q�6u   $��Center=�5J      $��Center=�64   $��Center=�6X   ��5G   S   �� *DirectionalAlertList+��5H      Z� y�5I      �� *IsDirectionalAlertsPresent+#
 [ ��5K      �� 5L      .R�5M      �  � Orientation$ Vertical=�6(   5N      ��5Q      �� 5R      .R�5S      �� $��Center=�6(   $ Center=�6E   #���  6\    �� Padding$	 0,5q�6�   $
��Bold"�5V   C   $
��Wrap=�6U   $��0,0,0,6q�6i   ��5T   (   �� *ActualWidth+$. (DirectionalAlertAndAppointmentsDockPanel����5U   (   �� *53SizeHelperModel.ResolutionHelper.TextBlockLargeFont+# [  ��5V   (   �� *	AlertText+6i   5W      5Y      �� 5[      -   5        $  ArrivedAppointmentListView��5[   0   $	��Top=�6   
������ $��0q�6m   $��Stretch=�5]      $	��Top=�65   $��Stretch=�5^      $	��Top=�6<   $��Disabled=�5`      $
��Auto=�6M   ��5\      �� *(&DisplayListOfArrivedAppointmentDetails+ �� Mode$ TwoWay=���5_      �� *ActualWidth+$. (DirectionalAlertAndAppointmentsDockPanel����5c      �� 5d      .R�5e      � $ 
Horizontal=�6(   5f      ��5i      �� 5j      .R�5k      ��  ��Name$ AppointmentButton��6%   $��15,10q�6@   $��65��6O   $
��Left=�6^   ��5l       �� *#!Finish.ViewAllAppointmentsCommand+# [  ��5n   "   `� 5o   &     ActualWidthConverter `� 	Converter# [ 63   1�5p   *   �� 5q   .    �� Path$ ActualWidth�66   $  ArrivedAppointmentListView��6I   �� 5r   .   $* $DataContext.ArrivedAppointmentsCount�66    �� RelativeSource 6b   ��  �� Mode$ FindAncestor=� �� AncestorType" ��5t   '   ��5w   "   `� 5x   &     AppointmentButtonStyle# [ 63   .1�5y   *   �� $ IsViewAllAppointments�62   �� 5z   *     DefaultButtonStyle# [ 62   �� 5{   *     ArrivalFinishButtonStyle# [ 62   5|   '   .��5   "   �� .+�5�   &   ��  �� Orientation$ Vertical=�61   y�5�   1   �� *IsNormalAppointment+#
 [ .+�5�   *   �� $
��10,5q�64   $��CharacterEllipsis=�6B   $��NoWrap=�6c   $
��Left=�5�   4   $��Center=�6O   �� 5�   2   �66   �� *DisplayTime+$ OneWay=���6`   �� *AppointmentTimeStyle+$ OneWay=� �� 5�   2   �66   �� *DataContext.WithText+ �� $ FindAncestor=�" �� �� 5�   2   $��SemiBold"�6m   �66   �� *SessionHolder.DisplayName+$ OneWay=�6m   5�   +   �� 5�   *   $
��10,5q�64   $��CharacterEllipsis=�6B   $��NoWrap=�6c   $
��Left=�5�   4   $��Center=�6O   y�5�   4   �� *IsDelayTextVisible+#
 [ �� 5�   2   �66   �� *	DelayText+$ OneWay=�5�   +   5�   '   �� 5�   &   $
 10,5q�60   $��CharacterEllipsis=�6?   $��NoWrap=�6`   $��Center=�5�   ,   $��Center=�6I   $ Center=�6d   ��5�   ,   �� *ActualWidth+$ AppointmentButton��y�5�   ,   �� *IsViewAllAppointments+#
 [ �� 5�   &   �6*   �� *%#DataContext.ViewAllAppointmentsText+ �� $ FindAncestor=�" ���� 5�   &   �� 5�   &   �6*   �� *)'DataContext.MoreArrivedAppointmentsText+ �� $ FindAncestor=�" ��5�   '   5�   #   5�      5�      5�      �� 5�      $	��Top=�6   $��70��5�        NextButtonStyle#��[ 5�      $	��Top=�6:   $��	0,20,0,10q�6R   $��Stretch=�5�      $��Center=�6/   $
��Bold"�6S   ��5�      �� *ContinueOrDoneText+��5�      �� *64SizeHelperModel.ResolutionHelper.TextBlockXLargeFont+# [  ��5�      �� *Finish.ContinueOrDoneCommand+# [  5�      5�      � 5�      $��RelativePoint=�5�   E   $
��Fade=�5�       � AllowsTransparency . ��5�      �� *IsAllAppointmentsPopUpVisible+ � PlacementTarget 5�      �� $ MainGrid��.��5�      �� $��2q�6    �� CornerRadius$ 10��6-    	 Button.Background#��[	 6?   
������� �� Padding$ 5q�5�   ,   $
��50,0q�68   ._�5�      ]� -   5        $ AppointmentsStackPanel��5�   $   .+�5�      �� $	��Top=�6(   $
��Left=�6=   $	��Top=�6X   $��
10,15,0,10q�6p    
 HighLightMessageStyle#��[
 5�      ��5�      �� *ActualWidth+$ AppointmentsStackPanel����5�      �� *53SizeHelperModel.ResolutionHelper.TextBlockLargeFont+# [  ��5�      �� *#!AllAppointmentsDisplayHeadingText+�� 5�      $	��Top=�6#   ���#DEDEDE$��3��6I   $��0,5,0,20q�6]   $��Stretch=�5�      $
��Left=�6/    5�      �� *ActualWidth+$ AppointmentsStackPanel���� 5�      $��Bottom=�6%   $��Stretch=�6=   $��Bottom=�6[   $��5q�6v   $��Center=�5�      $��Center=�69   #��[ 5�      $��60��6>   ��5�      �� *	CloseText+��63   �� *Finish.CloseCommand+# [  �� 5�      -   5        $# AllArrivedAppointmentListView��5�   =   $	��Top=�6'   
������ $��0q�6}   $��Stretch=�5�      $	��Top=�65   $��0,0,0,20q�6M   $��Stretch=�5�      $	��Top=�6<   $��Disabled=�5�      $
��Auto=�6M   ��5�      �� *!ListOfArrivedAppointmentDetails+��5�      �� *ActualWidth+$ AppointmentsStackPanel����5�   "   �� 5�   &   .R�5�   *   � $ 
Horizontal=�64   5�   '   ��5�   "   �� 5�   &   .R�5�   *   �� $��15,10q�61   $��65��6@   $
��Left=�5�   1   #��[ 6S   ��5�   1   �� $ ActualWidth�$# AllArrivedAppointmentListView�� # ActualWidthToHalfWidthConverter#
 [ .��5�   .   ]� .+�5�   2   �� $
��10,5q�6<   $��CharacterEllipsis=�6J   $��NoWrap=�6k   �� 5�   6   $��SemiBold"�6d   �6:   �� *DisplayTime+$ OneWay=�6d    �� 5�   6   �6:   �� *DataContext.WithText+ �� $ FindAncestor=�" �� �� 5�   6   $��SemiBold"�6q   �6:   �� *SessionHolder.DisplayName+$ OneWay=�6q   �� 5�   6   �� 5�   6   �6:   �� *	DelayText+$ OneWay=�5�   3   5�   /   5�   +   5�   '   5�      5�      5�      5�      �� 5�      -   5          ��Name$$  AlertsSectionBorderColourPanel��5�   X   $��0��6   $��3��6"   $��3��63   $��2��6C   ���#FDFAFA���#F0F0F0$��6,0,0,0q�5�   <   y�5�      �� *IsAlertsPresent+#
 [ �� 5�      -	   5        $  AlertsSectionBorderPanel��5�   D   $��0��6   $��2��6"   $��3��63   y�5�      �� *IsAlertsPresent+#
 [ ._�5�      ]� -
   5        $ AlertsSectionDockPanel��5�      $	��Top=�6<   $��0,0,20,5q�6Q   .+�5�      �� $	��Top=�6    $
��Left=�65   $	��Top=�6P   $��
10,15,0,10q�6h   #���  5�   ;   $
��Bold"�6i   ��5�       �� *ActualWidth+$ AlertsSectionDockPanel����5�       �� *64SizeHelperModel.ResolutionHelper.TextBlockMediumFont+# [  ��5�       �� *	AlertText+6i   �� 5�      -   5        $ AlertsListView��5�      $	��Top=�67   $��0q�5�   S   $��Hidden=�5�      $
��Auto=�6O   ��5�      �� *	AlertList+��6=   Z� ��5�      �� 5�      .R�5�   "   � $ Vertical=�6,   5�      ��5�      �� 5�      .R�5�   "   �� ! ��Name$! StkPnl��6Y   $ 
Horizontal=�6-   $��	-3,5,0,10q�6F   .+�5   &   �� $��8,0,0,0q�6-   ��6G   �� *InsetBorderFontColor+._�5  *   �� $
��Left=�64   $	��Top=�6O   $
 Left=�6g   $��10,0,0,0q�5  ,   $ 0,0,15,0q�6>   ��5  ,   �� *64SizeHelperModel.ResolutionHelper.TextBlockMediumFont+# [  ��5  ,   �� *ActualWidth+$ AlertsListView���� 5  .   �62   �� *ImportantText+$ OneWay=���6^   �� *
FontWeight+��6�   �� *	FontColor+ �� 5  .   �62   �� *	AlertText+��6M   �� *
FontWeight+��6o   �� *	FontColor+5  +   5  '   5
  #   5     5     5     5     �� 5     $��2��6   $��3��6"   y�62   �� *IsAlertsPresent+#
 [ ._�5     �� -   5        $ ShowAllButton��5     $��60��60   $	��160��6?   $	��190��6N     ShowAllButtonStyle#��[ 5     $��0,5,0,10q�6=   $��Center=�5     $��Center=�6.   $	��5,0q�6I   $��Center=�5     $��Center=�65   ��5     �� *ShowAllAlertsCommand+.��5     �� $
��Wrap=�6=   ��6    �� *ShowAllText+6=   5     5     � 5     $��RelativePoint=�5  E   $
��Fade=�5      . ��5     �� *IsAllAlertsPopUpVisible+ 5     �� $ MainGrid��.��5      �� $��2q�6   $ 10��6-   #��[	 6?   
�������$ 15q�5!  ,   $
��50,0q�69   ._�5#     ]� -   5        $ AlertsStackPanel��5#  $   .+�5$     �� $	��Top=�6(   $
��Left=�6=   $	��Top=�6X   $��
10,15,0,10q�6p   #��[
 5'     ��5%     �� *ActualWidth+$ AlertsStackPanel����5&     �� *53SizeHelperModel.ResolutionHelper.TextBlockLargeFont+# [  ��5(     �� *AllAlertsDisplayHeadingText+�� 5*     $	��Top=�6#   ���#DEDEDE$��3��6I   $��0,5,0,20q�6]   $��Stretch=�5+     $
��Left=�6/    5,     �� *ActualWidth+$ AlertsStackPanel���� 5.     $��Bottom=�6!   $��Stretch=�69   $��Bottom=�6W   $��5q�6r   $��Center=�5/     $��Center=�69   #��[ 50     $��60��6>   ��51     �� *	GotItText+��63   �� *Finish.GotItCommand+# [  �� 53     -   5        $ AllAlertsListView��53  8   $	��Top=�6#   $��0,0,0,20q�6S   $��0q�54  S   $��Hidden=�55     $
��Auto=�6O   ��54     �� *	AlertList+��6=   Z� ��56  "   �� 57  &   .R�58  *   � $ Vertical=�64   59  '   ��5<  "   �� 5=  &   .R�5>  *   ]� $ StkPnl��6F   $��0,10,0,0q�64   .+�5?  .   �� $
��Left=�6>   $��0,0,15,0q�6T   $	��Top=�6f   " �� Bullet" 5@  2   A� 5A  6   #���  6>   $��5��6f   $��5��6p   ._�5C  2   �� $
��Left=�6<   $
��Left=�6R   $	��Top=�6m   $
 Left=�6�   $��15,0,0,5q�5F  <   $ 0,0,75,0q�6N   ��5D  <   �� *64SizeHelperModel.ResolutionHelper.TextBlockMediumFont+# [  ��5E  <   �� *ActualWidth+$ AllAlertsListView���� 5G  6   �6:   �� *ImportantText+$ OneWay=���6f   �� *
FontWeight+��6�   �� *	FontColor+ �� 5H  6   
��������6:   �� *	AlertText+6U   5I  3   5J  /   5K  +   5L  '   5N     5P     5Q     5S     5U     � 5W  
   y�6   �� *IsSurveyPromptVisible+#
 [  5Y     �� 5Z     $��3* �6    �� 5[     $��7* �6     5^     �� 5_     $��3* �6#   �� 5`     $��4* �6#   �� 5a     $
��0.2* �6#   �� 5b     $��4* �6#   �� 5c     $��3* �6#   .+�5f     �� $��0��6   $��1��6%   $��3��65   $��Center=�5g     $��Bottom=�69   $ Center=�5h  J   $
��Wrap=�6a   #��� 
 5j     $��0,0,0,30q�5k     ��5h     �� *WishToAnswerQuestionaireText+��5i     �� *53SizeHelperModel.ResolutionHelper.TextBlockLargeFont+# [  5k     �� 5m     $��1��6   $��1��6"   $��0,30,0,0q�5n     #���  6'   $��Stretch=�5o     $	��Top=�63   $��70��5p     $��10q�6$   
# � Tag$# Confirm��5q  \   ��6   �� *Finish.YesCommand+# [  .��5r     ]� .+�5s     �� $ Center=�6H   ��6    �� *WishToAnswerButtonText+6H   5t     5u     �� 5w     $��1��6   $��3��6"   $��0,30,0,0q�62   #���  5x     $��Stretch=�5y     $	��Top=�63   $��70��5z     $��10q�6$   ��5{     �� *Finish.NoCommand+# [  .��5|     ]� .+�5}     �� $ Center=�6N   ��6    �� *DoNotWishToAnswerButtonText+6N   5~     5     5�     5�     5�     