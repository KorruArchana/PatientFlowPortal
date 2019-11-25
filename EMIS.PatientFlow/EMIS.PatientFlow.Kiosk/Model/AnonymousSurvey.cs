namespace EMIS.PatientFlow.Kiosk.Model
{
    public class AnonymousSurvey : Entity
    {
        public string KioskId { get; set; }
        public int QuestionnaireId { get; set; }
        public string QuestionnaireTitle { get; set; }
        public int QuestionId { get; set; }
        public string QuestionText { get; set; }
        public string OptionId { get; set; }
        public string AnswerText { get; set; }
    }
}
