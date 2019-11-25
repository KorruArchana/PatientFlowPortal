using System.Collections.Generic;

namespace EMIS.PatientFlow.SyncService.Data
{
    public class Question
    {
        public int QuestionId { get; set; }
        public int QuestionnaireId { get; set; }
        public string QuestionText { get; set; }
        public int QuestionType { get; set; }
        public int OptionCharLimit { get; set; }
        public string Gender { get; set; }
        public int Age1 { get; set; }
        public int Age2 { get; set; }
        public int Order { get; set; }
        public string Operation { get; set; }
        public List<QuestionOptionList> QuestionOptions { get; set; }
    }

    public class QuestionOptionList
    {
        public string QuestionOption { get; set; }
        public string QuestionOptionCode { get; set; }
        public int NestedQuestionId { get; set; }
        public int OptionId { get; set; }
        public int QuestionId { get; set; }
        public long QuestionSnomedOptionCode { get; set; }
    }
}
