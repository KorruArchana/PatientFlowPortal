using System.Collections.Generic;
using EMIS.PatientFlow.Entities;

namespace EMIS.PatientFlow.Interfaces
{
    public interface ISurveyRepository
    {
        void SurveyUpdate(List<Survey> surveys);
    }
}
