using System;
using System.Collections.Generic;
using EMIS.PatientFlow.Entities;
using EMIS.PatientFlow.Interfaces;
using EMIS.PatientFlow.Repositories.Base;

namespace EMIS.PatientFlow.Repositories
{
    public class SurveyRepository : BaseRepository, ISurveyRepository
    {
        public void SurveyUpdate(List<Survey> surveys)
        {
			try
			{
				DbAccess.SurveySave(surveys);
			}
			catch (Exception ex)
			{
				GenerateSqlException(ex);
				Logger.Instance.WriteLog(Entities.Enums.LogType.Fatal, ex.Message, ex, "TestUser");
			}
        }
    }
}
