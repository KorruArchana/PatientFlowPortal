using System;
using System.Windows;
using System.Windows.Controls;
using EMIS.PatientFlow.Common.Enums;
using EMIS.PatientFlow.Kiosk.Model;

namespace EMIS.PatientFlow.Kiosk.TemplateSelector
{
    public class QuestionnaireDataTemplateSelector : DataTemplateSelector
    {
        public DataTemplate TextBoxTemplate { get; set; }
        public DataTemplate CheckBoxTemplate { get; set; }
        public DataTemplate RadioButtonTemplate { get; set; }

        public DataTemplate NumericTextBoxTemplate { get; set; }
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            Questions questions = (Questions)item;
            if (questions != null)
            {
                String controlType = questions.AnswerControlType;

                if (controlType == AnswerControlType.Textbox.ToString())
                    return TextBoxTemplate;
                else if (controlType == AnswerControlType.CheckBox.ToString())
                    return CheckBoxTemplate;
                else if (controlType == AnswerControlType.RadioButton.ToString())
                    return RadioButtonTemplate;
                else if (controlType == AnswerControlType.NumericTextBox.ToString())
                    return NumericTextBoxTemplate;
                else
                    return TextBoxTemplate;
            }
            else
                return null;
        }
    }
}
