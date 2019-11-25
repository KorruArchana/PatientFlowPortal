using System.Collections.Generic;
using System.Web.Mvc;
using EMIS.PatientFlow.Entities;

namespace EMIS.PatientFlow.Web.ViewModel
{
    public class TranslationListViewModel
    {
        public List<Module> TranslationList { get; set; }
        public List<SelectListItem> LanguageList { get; set; }
        public int LanguageId { get; set; }
        public int TotalCount { get; set; }
    }
}