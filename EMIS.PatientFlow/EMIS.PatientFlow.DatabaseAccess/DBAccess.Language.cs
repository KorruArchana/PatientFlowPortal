using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using EMIS.PatientFlow.Entities;

namespace EMIS.PatientFlow.DatabaseAccess
{
	public partial class DbAccess
	{
		public IEnumerable<Language> GetLanguageList()
		{
			var languageList = new List<Language>();
			using (var connection = DbManager.GetNewConnection())
			{
				try
				{
					DbManager.Open(connection);

					SqlCommand spCommand = DbManager.GetSprocCommand("[PatientFlow].[GetLanguageList]", connection);
					using (SqlDataReader dr = spCommand.ExecuteReader())
					{
						while (dr.Read())
						{
							var language = new Language
							{
								Id = Convert.ToInt32(dr["LanguageId"]),
								LanguageName = dr["LanguageName"].ToString(),
								LanguageCode = dr["LanguageCode"].ToString()
							};
							languageList.Add(language);
						}
					}
				}
				finally
				{
					DbManager.Close(connection);
				}
			}

			return languageList;
		}

		public IEnumerable<Module> GetModules(int pageNo, int pageSize, out int recordCount)
		{
			var moduleList = new List<Module>();
			using (var connection = DbManager.GetNewConnection())
			{
				try
				{
					DbManager.Open(connection);
					SqlCommand spCommand = DbManager.GetSprocCommand("[PatientFlow].[GetModules]", connection);
					spCommand.Parameters.Add(DbManager.CreateParameter("@PageNo", pageNo));
					spCommand.Parameters.Add(DbManager.CreateParameter("@PageSize", pageSize));
					var outputParameter = DbManager.CreateOutputParameter("@TotalCount", SqlDbType.BigInt);
					spCommand.Parameters.Add(outputParameter);
					using (SqlDataReader dr = spCommand.ExecuteReader())
					{
						while (dr.Read())
						{
							var module = new Module
							{
								Id = Convert.ToInt32(dr["ModuleId"]),
								ModuleName = Convert.ToString(dr["ModuleName"]),
								TranslationTypeId = Convert.ToInt32(dr["TranslationTypeId"])
							};
							moduleList.Add(module);
						}
					}

					recordCount = Convert.ToInt32(outputParameter.Value);
				}
				finally
				{
					DbManager.Close(connection);
				}
			}
			return moduleList;
		}

		public IEnumerable<Module> GetModuleDetails(int moduleId, string languageCode)
		{
			var moduleList = new List<Module>();
			using (var connection = DbManager.GetNewConnection())
			{
				try
				{
					DbManager.Open(connection);
					SqlCommand spCommand = moduleId > 0
						? DbManager.GetSprocCommand("[PatientFlow].[GetScreenDetails]", connection)
						: DbManager.GetSprocCommand("[PatientFlow].[GetModuleDetails]", connection);
					if (moduleId > 0)
						spCommand.Parameters.Add(DbManager.CreateParameter("@ModuleId", moduleId));
					spCommand.Parameters.Add(DbManager.CreateParameter("@LanguageCode", languageCode, 100));
					using (SqlDataReader dr = spCommand.ExecuteReader())
					{
						while (dr.Read())
						{
							var module = new Module
							{
								Id = Convert.ToInt32(dr["ModuleId"]),
								ModuleName = Convert.ToString(dr["ModuleName"]),
								TranslatedText = Convert.ToString(dr["TranslationText"]),
								TranslationRefId = Convert.ToInt32(dr["TranslationRefId"])
							};
							moduleList.Add(module);
						}
					}
				}
				finally
				{
					DbManager.Close(connection);
				}
			}
			return moduleList;
		}

		public IEnumerable<Translation> GetTransation(
			int pageSize,
			DateTime syncedTranslationDate,
			out DateTime lastRowModifiedDate)
		{
			using (var connection = DbManager.GetNewConnection())
			{
				var translationList = new List<Translation>();
				try
				{
					DbManager.Open(connection);
					SqlCommand spCommand = DbManager.GetSprocCommand("[PatientFlow].[GetTranslations]", connection);

					var outputParameter = DbManager.CreateOutputParameter("@LastRowModifiedDate", SqlDbType.DateTime);

					spCommand.Parameters.Add(DbManager.CreateParameter("@PageSize", pageSize));
					spCommand.Parameters.Add(DbManager.CreateParameter("@SyncedTranslationDate", syncedTranslationDate));
					spCommand.Parameters.Add(outputParameter);
					using (SqlDataReader dr = spCommand.ExecuteReader())
					{
						while (dr.Read())
						{
							var translation = new Translation
							{
								LanguageId = Convert.ToInt32(dr["LanguageId"]),
								TranslationRefId = Convert.ToInt32(dr["TranslationRefId"]),
								TranslationText = Convert.ToString(dr["TranslationText"])
							};
							translationList.Add(translation);
						}
					}

					lastRowModifiedDate = Convert.ToDateTime(outputParameter.Value);
				}
				finally
				{
					DbManager.Close(connection);
				}

				return translationList;
			}
		}

		public IEnumerable<ScreenControl> GetScreenControls(
			int pageSize,
			DateTime lastSyncedDate,
			out DateTime lastRowModifiedDate)
		{
			using (var connection = DbManager.GetNewConnection())
			{
				var screenControlList = new List<ScreenControl>();
				try
				{
					DbManager.Open(connection);
					SqlCommand spCommand = DbManager.GetSprocCommand("[PatientFlow].[GetScreenControls]", connection);

					var outputParameter = DbManager.CreateOutputParameter("@LastRowModifiedDate", SqlDbType.DateTime);

					spCommand.Parameters.Add(DbManager.CreateParameter("@PageSize", pageSize));
					spCommand.Parameters.Add(DbManager.CreateParameter("@LastSyncedDate", lastSyncedDate));
					spCommand.Parameters.Add(outputParameter);
					using (SqlDataReader dr = spCommand.ExecuteReader())
					{
						while (dr.Read())
						{
							var screenControl = new ScreenControl
							{
								ControlId = Convert.ToInt32(dr["ControlId"]),
								TranslationRefId =
									dr["TranslationRefId"] == DBNull.Value ? 0 : Convert.ToInt32(dr["TranslationRefId"])
							};
							screenControlList.Add(screenControl);
						}
					}

					lastRowModifiedDate = Convert.ToDateTime(outputParameter.Value);
				}
				finally
				{
					DbManager.Close(connection);
				}

				return screenControlList;
			}
		}

		public int SaveModuleTranslations(DataTable dtModuleTranslationList, int translationTypeId)
		{
			int result;
			using (var connection = DbManager.GetNewConnection())
			{
				try
				{
					DbManager.Open(connection);
					SqlCommand spCommand = translationTypeId > 0
						? DbManager.GetSprocCommand("[PatientFlow].[SaveScreenControlTranslatedText]", connection)
						: DbManager.GetSprocCommand("[PatientFlow].[SaveModuleTranslatedText]", connection);
					spCommand.Parameters.Add(DbManager.CreateParameter("@ModuleTranslations", dtModuleTranslationList,
						"PatientFlow.ModuleTranslations"));
					result = Convert.ToInt32(spCommand.ExecuteScalar());
				}
				finally
				{
					DbManager.Close(connection);
				}
			}
			return result;
		}
	}
}
