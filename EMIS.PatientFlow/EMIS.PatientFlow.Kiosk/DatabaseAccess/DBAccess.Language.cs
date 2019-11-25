using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using EMIS.PatientFlow.Common.Enums;
using EMIS.PatientFlow.Kiosk.Enum;
using EMIS.PatientFlow.Kiosk.Helper;
using EMIS.PatientFlow.Kiosk.Model;

namespace EMIS.PatientFlow.Kiosk.DatabaseAccess
{
	public partial class DbAccess
	{
		public List<LanguageModel> GetLanguageList()
		{
			var languageList = new List<LanguageModel>();
			LanguagesList languages = GetKioskConfiguration<LanguagesList>(KioskConfigType.Language.ToString());
			try
			{
				if (languages != null)
				{
					foreach (var lang in languages.Languages)
					{
						if (lang.Id > 0)
						{
							if (GlobalVariables.GlobalLanguageList.FirstOrDefault(a => a.LanguageId == lang.Id) != null)
							{
								languageList.Add(GlobalVariables.GlobalLanguageList.FirstOrDefault(a => a.LanguageId == lang.Id));
							}
						}
					}
				}
				if(languageList.Count==0)
					languageList.Add(GlobalVariables.GlobalLanguageList.FirstOrDefault());
			}
			catch (Exception ex)
			{
				Logger.Instance.WriteLog(LogType.Error, ex.Message, ex, KioskId);
			}
			return languageList;
		}


		/// <summary>
		/// Get text of UI controls based on selected language
		/// </summary>
		/// <param name="screenCode">screen code</param>
		/// <param name="controlUniqueId">control id</param>
		/// <returns>translation text</returns>
		public string GetControlText(string screenCode, string controlUniqueId)
		{
			const string spScreencontrolText = "[PatientFlow].[GetScreenControlText]";
			string controlText = default(string);
			try
			{
				DbManager.Open();
				GlobalVariables.IsDbConnected = true;

				SqlCommand spCommand = DbManager.GetSprocCommand(spScreencontrolText);

				spCommand.Parameters.Add(DbManager.CreateParameter("@ScreenCode", screenCode, 20));
				spCommand.Parameters.Add(DbManager.CreateParameter("@ControlUniqueId", controlUniqueId, 50));
				spCommand.Parameters.Add(DbManager.CreateParameter("@LanguageId", GlobalVariables.SelectedLanguageId));

				using (SqlDataReader dr = spCommand.ExecuteReader())
				{
					while (dr.Read())
					{
						controlText = dr["ControlLabel"] == DBNull.Value ? string.Empty : Convert.ToString(dr["ControlLabel"]);
					}
				}
			}
			catch (Exception ex)
			{
				var exception = ex as SqlException;
				if (exception != null && exception.ErrorCode == Constants.DbConnectionErrorCode)
				{
					GlobalVariables.IsDbConnected = false;
				}
				else
				{
					Logger.Instance.WriteLog(LogType.Error, ex.Message, ex, KioskId);
				}
			}
			finally
			{
				//Close connection
				DbManager.Close();
			}

			return controlText;
		}
	}
}
