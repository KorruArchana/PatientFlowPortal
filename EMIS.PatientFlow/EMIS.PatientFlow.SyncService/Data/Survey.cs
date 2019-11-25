using System;

namespace EMIS.PatientFlow.SyncService.Data
{
    public class Survey
    {
        public long AnswerId { get; set; }
        public string AnswerText { get; set; }
        public string KioskId { get; set; }
        public int QuestionnaireId { get; set; }
        public int QuestionId { get; set; }
        public string OptionId { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime Modified { get; set; }
        public string QuestionnaireTitle { get; set; }
        public string QuestionText { get; set; }
        public string ModifiedDate { get; set; }
    }
}
