using System;

namespace EMIS.PatientFlow.Entities
{
    public class QuestionnaireReport : Entity
    {
        public string QuestionnaireTitle { get; set; }
        public string QuestionText { get; set; }
        public string AnswerText { get; set; }
        public DateTime Modified { get; set; }
        public string ModifiedDateToDisplay { get; set; }
    }
}
