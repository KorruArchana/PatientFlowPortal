using System.Collections.Generic;
using EMIS.PatientFlow.Entities;

namespace EMIS.PatientFlow.Web.ViewModel
{
    public class KioskDetailsViewModel
    {
        public Kiosk Kiosk { get; set; }

        public string PatientMatchTitle { get; set; }
        public List<Language> LanguageList { get; set; }

        public List<Module> Module { get; set; }

        public List<PatientMatch> PatientMatch { get; set; }
        public int OrganisationId { get; set; }

        public SiteMenu SiteMenu { get; set; }
    }
}