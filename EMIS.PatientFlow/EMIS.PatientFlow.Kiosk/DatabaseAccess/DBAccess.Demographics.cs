using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using EMIS.PatientFlow.Common.Enums;
using EMIS.PatientFlow.Kiosk.Helper;

namespace EMIS.PatientFlow.Kiosk.DatabaseAccess
{
	public partial class DbAccess
	{
		public void SaveDemorgraphicFrequency(bool isValid)
		{
			const string spName = "[PatientFlow].[SaveDemographicDetailsFrequency]";
			try
			{
				DbManager.Open();
				SqlCommand spCommand = DbManager.GetSprocCommand(spName);
				spCommand.Parameters.Add(DbManager.CreateParameter("@PatientId", GlobalVariables.ArrivedPatientDetails.BookedPatient.Id));
				spCommand.Parameters.Add(DbManager.CreateParameter("@AccessedOn", DateTime.Today));
				spCommand.Parameters.Add(DbManager.CreateParameter("@OrganisationId", Convert.ToInt32(GlobalVariables.SelectedOrganisation.OrganisationId)));
				spCommand.Parameters.Add(DbManager.CreateParameter("@ValidDetails", isValid));
				spCommand.ExecuteNonQuery();
			}
			catch (Exception ex)
			{
				Logger.Instance.WriteLog(LogType.Error, ex.Message, ex, KioskId);
			}
			finally
			{
				//Close connection
				DbManager.Close();
			}
		}

		public DateTime? ShowDemographicDetailsForPatient()
		{
			const string spName = "[PatientFlow].[GetDemographicFrequencyByPatient]";
			try
			{
				DbManager.Open();
				SqlCommand spCommand = DbManager.GetSprocCommand(spName);
				spCommand.Parameters.Add(DbManager.CreateParameter("@PatientId", GlobalVariables.ArrivedPatientDetails.BookedPatient.Id));
				spCommand.Parameters.Add(DbManager.CreateParameter("@OrganisationId", Convert.ToInt32(GlobalVariables.SelectedOrganisation.OrganisationId)));

				var result = spCommand.ExecuteScalar();
				if (result != null)
					return Convert.ToDateTime(result.ToString());
			}
			catch (Exception ex)
			{
				Logger.Instance.WriteLog(LogType.Error, ex.Message, ex, KioskId);
			}
			finally
			{
				//Close connection
				DbManager.Close();
			}

			return null;
		}
	}
}
