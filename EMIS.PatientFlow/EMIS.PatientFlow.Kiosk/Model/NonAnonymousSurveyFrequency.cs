using System;

namespace EMIS.PatientFlow.Kiosk.Model
{
    public class NonAnonymousSurveyFrequency : Entity
    {
        public int PatientId { get; set; }
        public int QuestionnaireId { get; set; }
        public DateTime AccessedOn { get; set; }
    }
}
