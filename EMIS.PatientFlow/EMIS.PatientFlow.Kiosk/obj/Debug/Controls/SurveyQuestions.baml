   M S B A M L   `   `   `  ����Q  MEMIS.PatientFlow.Kiosk, Version=5.0.0.0, Culture=neutral, PublicKeyToken=nullQ-clr-namespace:EMIS.PatientFlow.Kiosk.KeyBoardEMIS.PatientFlow.Kiosk.KeyBoard  5      c _System.Windows.Interactivity, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35qPclr-namespace:System.Windows.Interactivity;assembly=System.Windows.InteractivitySystem.Windows.Interactivity 5      d `GalaSoft.MvvmLight.Extras, Version=5.0.2.32240, Culture=neutral, PublicKeyToken=f46ff315b1088208jKclr-namespace:GalaSoft.MvvmLight.Command;assembly=GalaSoft.MvvmLight.ExtrasGalaSoft.MvvmLight.Command 5      a5clr-namespace:EMIS.PatientFlow.Kiosk.TemplateSelector'EMIS.PatientFlow.Kiosk.TemplateSelector  5	      Q-clr-namespace:EMIS.PatientFlow.Kiosk.ControlsEMIS.PatientFlow.Kiosk.Controls  5
      5    /EMIS.PatientFlow.Kiosk.Controls.SurveyQuestions   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      ;k-clr-namespace:EMIS.PatientFlow.Kiosk.KeyBoard     5      ^iPclr-namespace:System.Windows.Interactivity;assembly=System.Windows.Interactivity     5      [cmdKclr-namespace:GalaSoft.MvvmLight.Command;assembly=GalaSoft.MvvmLight.Extras     5      Glocal5clr-namespace:EMIS.PatientFlow.Kiosk.TemplateSelector     5	      GlocalControls-clr-namespace:EMIS.PatientFlow.Kiosk.Controls     5
        � DataContext  5      �� *SurveyQuestions+   Locator �� Source# [   � 	Resources 5       �� MergedDictionaries �� Source  NumericTextBoxTemplate �� Orientation �� Mode �� TextAlignment  HighLightMessageStyle �� ElementName # ActualWidthMinusMarginConverter	 �� 	Converter
 ��Name �� TextWrapping5   /EMIS.PatientFlow.Kiosk.KeyBoard.NumericKeyboard   NumericKeyboard $  SurveyTextBoxBaseControlTemplate �� UpdateSourceTrigger  TextBoxTemplate9   3EMIS.PatientFlow.Kiosk.KeyBoard.TouchScreenKeyboard   TouchScreenKeyboard  CheckBoxTemplate  FavsScrollViewer  ListboxStyle �� SelectionMode � IsSharedSizeScope � ColumnDefinitions z� SharedSizeGroup ��Name 	 DefaultButtonStyle d� 	IsChecked � LayoutTransform �� ScaleX �� ScaleY �� Padding 
 RadioButtonTemplate ��Name  PassThroughConvert `� 	Converter �� PathO   IEMIS.PatientFlow.Kiosk.TemplateSelector.QuestionnaireDataTemplateSelector   TextBoxTemplate   NumericTextBoxTemplate   CheckBoxTemplate   RadioButtonTemplate  ListItemTemplateSelector�� 5   
    5      �� 5      $ SurveyQuestionsStyle.xaml?�6%   �� 5      $- '../Converters/ActualWidthConverter.xaml?�6%   %d  &	       7   7  7  7  7   &	      7   7  7  7  7   &	 C    7   7  7   7  7  7   7  7   7   &	
 '    7   7  7   7  7  7   7  7   7   7  &	 �    7  7  7  7 
 �� 5      .R�5      �� 
�������$ Vertical=�5      $
��Left=�65   $	��Top=�6P   y�5   1   �� *IsQuestionVisible+$ TwoWay=�.+�5      �� $
 Left=�6    $
��Left=�65   $
��0,20q�6P   $
��Wrap=�6^   #���  5       ��5       �� *53SizeHelperModel.ResolutionHelper.TextBlockLargeFont+8   ��5   P   �� *QuestionText+��5       �� *ActualWidth+$ QuestionStkPnl��8	  �� 5      $
 
tbxNumeric��6[   $
 Wrap=�6    . 8�� $	��150��5       $��2q�5!      ���#626262$	��Top=�5"      $
��Left=�6=   ��5      �� *ActualWidth+$ QuestionStkPnl��8	  ��5#      �� *53SizeHelperModel.ResolutionHelper.TextBlockLargeFont+8  ��5$      �� *SelectedAnswer+$ TwoWay=�$ PropertyChanged=�5%      5&      �� 5(      .R�5)      �� 
�������$ Vertical=�5*      $
��Left=�65   $	��Top=�6P   y�5)   1   �� *IsQuestionVisible+$ TwoWay=�.+�5+      �� $
 Left=�6    $
��Left=�65   $
��0,20q�6P   $
��Wrap=�6^   #���  5-       ��5,       �� *53SizeHelperModel.ResolutionHelper.TextBlockLargeFont+8   ��5-   P   �� *QuestionText+��5.       �� *ActualWidth+$ QuestionStkPnl��8	  �� 5/      $
 tbkAnswerText��6c   $
 Wrap=�6    . 8�� $	��150��52      $��2q�53      ���#626262$	��Top=�54      $
��Left=�6=   ��51      �� *ActualWidth+$ QuestionStkPnl��8	  ��55      �� *53SizeHelperModel.ResolutionHelper.TextBlockLargeFont+8  ��56      �� *SelectedAnswer+$ TwoWay=�$ PropertyChanged=�57      58      �� 5:      .R�5;      �� 
�������$ Vertical=�5<      $
��Left=�65   $	��Top=�6P   y�5;   0   �� *IsQuestionVisible+$ TwoWay=�.+�5=      �� $
 Left=�6    $
��Left=�65   $
��0,10q�6P   $
��Wrap=�6^   #���  5?       ��5>       �� *53SizeHelperModel.ResolutionHelper.TextBlockLargeFont+8   ��5?   P   �� *QuestionText+��5@       �� *ActualWidth+$ QuestionStkPnl���� 5A      $
��Auto=�6#   8�� ��5B   #   �� *CASizeHelperModel.ResolutionHelper.RadioButtonCheckBoxMaximumHeight+8  .��5C      �� $��Hidden=�6#   $��Hidden=�6U   $��0,10,0,0q�6�   8�� $
��Left=�5D   I   $
��Left=�6k   $��0q�6�   $ Single=�5E   I    . ��6#   �� *AnswerOptions+��5F   #   �� *ActualWidth+$ QuestionStkPnl��8	  ��5H      �� 5I   "   .R�5J   &   �� $ 
Horizontal=�61   .+�5K   *   �  5L   .   �� 5M   2   $ B��6C   .+�5P   .   �� $ CheckBoxButton��6E   $��0��65   #��� 	 6]   $��0,10,0,5q�5Q   5   $��0q�6G   $
��Left=�5U   5   $��Center=�6W   ���#04BCA6��5R   5   �� *64SizeHelperModel.ResolutionHelper.ButtonsMinimumWidth+8  ��5S   5   �� *ActualWidth+$ QuestionStkPnl��8	  ��5T   5   �� *75SizeHelperModel.ResolutionHelper.ButtonsMinimumHeight+8  ��5V   5   �� *!SurveyQuestions.CheckBoxCommand+8  ��6�   �� *AnswerOptionId+.��5W   2   ]� .+�5X   6   �� $
��Left=�6?   $��	10,0,10,0q�6u    6U   �� *	IsChecked+ 5Y   :   �� 5Z   >   $ 1Q�6M   $ 1Q�6X   5\   7   �� 5]   6   $
��Left=�6@   $
��Left=�5^   @   $��Center=�6[   $
 20,2q�5_   <   ��5]   V   �� *AnswerOptionText+5`   7   5a   3   5b   /   5d   +   5e   '   5f   #   5i      5k      5l      5m      �� 5o      .R�5p      �� 
�������$ Vertical=�6e   $��Center=�6|   $	��Top=�6�   y�60   �� *IsQuestionVisible+$ TwoWay=�.+�5q      �� $ Center=�6    $
��Left=�67   $
��0,10q�6R   $
��Wrap=�6`   #���  5t       ��5r       �� *ActualWidth+$ QuestionStkPnl����5s       �� *53SizeHelperModel.ResolutionHelper.TextBlockLargeFont+8   ��5t   P   �� *QuestionText+�� 5u      $
��Auto=�6#   8�� ��5v   #   �� *CASizeHelperModel.ResolutionHelper.RadioButtonCheckBoxMaximumHeight+8  .��5w      �� $ 
LayoutRoot��6#   $��Hidden=�5x   #   $��Hidden=�5y   #   $��	0,10,0,10q�5z   #   $��0q�66   8�� $��Center=�5|   #   $��Center=�6@   $ Single=�5~   I    . ��5}   #   �� *ActualWidth+$ QuestionStkPnl��8	  ��5~   #   �� *AnswerOptions+��5      �� 5�   "   .R�5�   &   �  5�   *   �� 5�   .   $
��Auto �6?   $ A��6L   .+�5�   *   �� $��0��61   #��� 	 6A   $��Center=�5�   1   $
��Left=�5�   1   $��0,10,0,5q�5�   1   ��5�   1   �� *64SizeHelperModel.ResolutionHelper.ButtonsMinimumWidth+8  ��5�   1   �� *ActualWidth+$ QuestionStkPnl��8	  ��5�   1   �� *75SizeHelperModel.ResolutionHelper.ButtonsMinimumHeight+8  ��5�   1   �� *$"SurveyQuestions.RadioButtonCommand+8  ��5�   .   `� 5�   2   8 	 .1�5�   6   �� $ QuestionsList��6>   �� 5�   6   $ AnswerOptionId�6>   5�   3   .��5�   .   �� $
 20,2q�5�   8   ��5�   8   �� *AnswerOptionText+$ TwoWay=�$ PropertyChanged=�5�   /   5�   +   5�   '   5�   #   5�      5�      5�      5�        5�      8   8  8  8  5�   y   5�      .  (System.Windows.Interactivity.Interaction    Triggers  5�      /  )System.Windows.Interactivity.EventTrigger  5�   
   !   	EventName$! Loaded��6   .  (System.Windows.Interactivity.TriggerBase"   Actions." 5�      6  0System.Windows.Interactivity.InvokeCommandAction  #   Command# 6$   �� *LoadedCommand+5�      .��5�      � $ � RowDefinitions$ 5�   
   �� 5�      $
��0.8* �6   �� 5�      $	��10* �6    5�   
   �� 5�      $
��0.5* �6   �� 5�      $��9* �6   �� 5�      $
��0.5* �6   .+�5�   
   �� $��0��6   $��0��6   $��3��6.     HeadingButtonStyle#���  6B   .��5�      �� $
��Wrap=�6>   ��6   �� *SurgeryLikeToAsktext+6>   5�      �� 5�   
   $��1��6   $��1��6"   .+�5�      (� $��50��6   $��50��6!   $��Center=�5�      $	��Top=�63   $
��0,10q�6K   y�5�      �� *IsProgressBarVisible+$ PropertyChanged=�   BoolToVisibleHiddenCollapsed#	 [ .��5�      3   -EMIS.PatientFlow.Kiosk.Controls.ProgressBarUc  5�      �� 5�      -   5        % ��Name$% QuestionStkPnl��5�      $ Vertical=�61   $��0,10,0,0q�6H   .+�5�      �� -   5        $��QuestionsList��5�   Q   $��Hidden=�5�      $��Hidden=�6M   
������ $��0q�5�   0   $��0q�6<   #��[ 5�   9     LvItemStyle#��[ 5�      #��[ 6I    . ��5�      �� *	Questions+  5�        5�      $! Loaded��6)   ." 5�      /	  )GalaSoft.MvvmLight.Command.EventToCommand	  & 	  PassEventArgsToCommand& . ' 	  Command' 61   �� *
ListLoaded+6P   5�        5�      $! SelectionChanged��6)   ." 5�      	  & . ' 61   �� *ListSelectionChanged+6[   5�      5�      �� 5�      $ 
Horizontal=�6   $��0,18,0,5q�66   ��5�      �� *ActualWidth+$ 
LayoutRoot����5�      �� *VerticalAlignmentValue+��5�      �� *HorizontalAlignmentValue+.+�5�      �� $	��Top=�5�      $
��Left=�65   $	��200��5�      $��	0,10,40,0q�6,   
( � Tag$( Confirm��5�        NextButtonStyle#��[ 6+   ��5�      �� *ContinueCommand+��5�      �� $ QuestionsList����5�      �� *B@SizeHelperModel.ResolutionHelper.SurveyQuestionnaireButtonHeight+# [  y�5�      �� *IsContinueVisible+#	 [ .��5�      �� $ 10q�6H   ��6$   �� *ContinueButtonText+6H   5�      �� 5�      $	��240��5�   �   $��0,10,0,0q�6�   $
( Skip��5�        BookAndSurveyButtonStyle#��[ 6(   ��5�      �� *SkipCommand+��5�      �� $ QuestionsList����5�      �� *VerticalAlignmentValue+��6R   �� *HorizontalAlignmentValue+��5�   !   �� *B@SizeHelperModel.ResolutionHelper.SurveyQuestionnaireButtonHeight+# [  y�5�      �� *IsSkipVisible+#	 [ .��5�      �� $ 10q�6D   ��6$   �� *SkipButtonText+6D   5�      5�      5�      5�      5�      5�      