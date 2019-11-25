using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using EMIS.PatientFlow.Common.Enums;
using EMIS.PatientFlow.Common.Extensions;
using EMIS.PatientFlow.Entities;

namespace EMIS.PatientFlow.DatabaseAccess
{
	public partial class DbAccess
	{
		public Kiosk GetKioskDetails(int kioskId)
		{
			using (var connection = DbManager.GetNewConnection())
			{
				Kiosk kiosk = null;
				try
				{
					DbManager.Open(connection);
					var ds = new DataSet();

					SqlCommand spCommand = DbManager.GetSprocCommand("[PatientFlow].[GetKioskDetails]", connection);
					spCommand.Parameters.Add(DbManager.CreateParameter("@KioskId", kioskId));

					using (var da = new SqlDataAdapter())
					{
						da.SelectCommand = spCommand;
						da.Fill(ds);

						if (ds.Tables.Count > 0)
							kiosk = GetKioskDetailDataTable(ds.Tables);
					}
				}
				finally
				{
					DbManager.Close(connection);
				}

				return kiosk;
			}
		}

		private Kiosk GetKioskDetailDataTable(DataTableCollection dtTableCollection)
		{
            if (dtTableCollection[0].Rows.Count > 0)
            {
                Kiosk kiosk = ToKioskObject(dtTableCollection[0].Rows[0]);

                kiosk.LanguageList = new List<Language>();
                foreach (DataRow dr in dtTableCollection[1].Rows)
                    kiosk.LanguageList.Add(ToLanguageObject(dr));

                kiosk.Module = new List<Module>();

                foreach (DataRow dr in dtTableCollection[2].Rows)
                    kiosk.Module.Add(ToModuleObject(dr));

                kiosk.QuestionnaireList = new List<Questionnaire>();

                foreach (DataRow dr in dtTableCollection[3].Rows)
                    kiosk.QuestionnaireList.Add(ToQuestionnaireObject(dr));

                kiosk.OrganisationList = new List<Organisation>();

                foreach (DataRow dr in dtTableCollection[4].Rows)
                    kiosk.OrganisationList.Add(ToOrganisationObject(dr));

                kiosk.BranchList = new List<BrachModel>();
                foreach (DataRow dr in dtTableCollection[4].Rows)
                    kiosk.BranchList.Add(ToBranchModelObject(dr));

                List<Site> TempSiteList = new List<Site>();
                if (dtTableCollection[7].Rows.Count > 0)
                {
                    foreach (DataRow dr in dtTableCollection[7].Rows)
                        TempSiteList.Add(ToSiteObject(dr));
                }
                foreach (var item in kiosk.BranchList)
                {
                    var OrganisationSites = (from osite in TempSiteList
                                             where osite.OrganisationId == item.OrganisationId
                                             select new Site
                                             {
                                                 Id = osite.Id,
                                                 SiteName = osite.SiteName

                                             }).ToList();

                    if (OrganisationSites != null && OrganisationSites.Count > 0)
                    {
                        item.SiteList = OrganisationSites;
                    }
                }

                kiosk.PatientMatch =
                     dtTableCollection[5].Rows[0]["ScreenOrder"].ToString()
                       .Split(',')
                       .Select((itm, index) => new PatientMatch() { Order = index + 1, ScreenCode = itm })
                       .ToList();
                kiosk.AppointmentMatch =
                    dtTableCollection[6].Rows[0]["ScreenOrder"].ToString()
                      .Split(',')
                      .Select((itm, index) => new PatientMatch() { Order = index + 1, ScreenCode = itm })
                      .ToList();

                kiosk.SessionHolderList = new List<Member>();
                if (dtTableCollection[8].Rows.Count > 0)
                {
                    foreach (DataRow dr in dtTableCollection[8].Rows)
                        kiosk.SessionHolderList.Add(ToSessionHolderObject(dr));
                }

                kiosk.SelectedDemographicDetails = new List<int>();
                kiosk.SelectedDemographicDetailsList = new List<DemographicDetails>();

                foreach (DataRow dr in dtTableCollection[9].Rows)
                {
                    DemographicDetails demogrphicSelectedList = ToDemogrphicSelectedList(dr);

                    if (demogrphicSelectedList.DemographicDetailsTypeId == 2)
                        continue;
                    kiosk.SelectedDemographicDetails.Add(demogrphicSelectedList.DemographicDetailsTypeId);
                    kiosk.SelectedDemographicDetailsList.Add(demogrphicSelectedList);
                }

                List<SiteMap> siteMapList = (from DataRow row in dtTableCollection[10].Rows select ToSiteMapList(row)).ToList();
                kiosk.KioskSiteMapList = siteMapList.Where(a => a.SiteMapImage != null).ToList();

                if (dtTableCollection[11].Rows.Count > 0)
                {
                    kiosk.SyncGuid = dtTableCollection[11].Rows[0]["ProductKey"].ToString();
                }

                return kiosk;
            }

            return null;
		}

		private SiteMap ToSiteMapList(DataRow row)
		{
			var value = row["SiteMap"] == DBNull.Value ? null : row["SiteMap"] as byte[];
			return new SiteMap() { SiteMapName = row["SiteDescription"].ToString(), SiteMapImage = value };
		}

		private Member ToSessionHolderObject(DataRow dr)
		{
			return new Member()
			{
				SessionHolderId = Convert.ToInt32(dr["SessionHolderId"]),
				OrganisationId = Convert.ToInt32(dr["OrganisationId"])
			};
		}

		private DemographicDetails ToDemogrphicSelectedList(DataRow dr)
		{
			return new DemographicDetails()
			{
				DemographicDetailsType = dr["DemographicDetailsTypeName"].ToString(),
				DemographicDetailsTypeId = Convert.ToInt32(dr["DemographicDetailsTypeId"]),
				ModuleNameToDisplay = dr["DemographicDetailsTypeName"].ToString(),
			};
		}

		private BrachModel ToBranchModelObject(DataRow dr)
		{
			return new BrachModel()
			{
				BranchId = dr["SiteId"] == DBNull.Value ? 0 : Convert.ToInt32(dr["SiteId"]),
				OrganisationId = Convert.ToInt32(dr["OrganisationId"]),
				OrganisationName = dr["OrganisationName"].ToString(),
				IsMainLocation = dr["MainLocation"] != DBNull.Value && (bool)dr["MainLocation"]
			};
		}

		private Site ToSiteObject(DataRow dr)
		{
			return new Site()
			{
				Id = dr["SiteId"] == DBNull.Value ? 0 : Convert.ToInt32(dr["SiteId"]),
				OrganisationId = Convert.ToInt32(dr["OrganisationId"]),
				SiteName = Convert.ToString(dr["SiteName"])
			};
		}

		private Kiosk ToKioskObject(DataRow dr)
		{
			return new Kiosk()
			{
				Id = Convert.ToInt32(dr["KioskId"]),
				Status = Convert.ToInt32(dr["Status"]),
				KioskStatus = dr["StatusName"].ToString(),
				KioskName = dr["KioskName"].ToString(),
				PcName = dr["PCName"].ToString(),
				IpAddress = dr["IPAddress"].ToString(),
				Title = dr["Title"].ToString(),
				PatientMatchTitle = dr["PatientMatchTitle"].ToString(),
				AppointmentMatchTitle = dr["AppointmentMatchTitle"].ToString(),
				AppointmentMatchId = Convert.ToInt32(dr["AppointmentMatchId"]),
				ConnectionGuid = dr["ConnectionGuid"] == DBNull.Value ? string.Empty : new Guid(dr["ConnectionGuid"].ToString()).ToString(),
				PatientMatch =
				  dr["ScreenOrder"].ToString()
					.Split(',')
					.Select((itm, index) => new PatientMatch() { Order = index + 1, ScreenCode = itm })
					.ToList(),
				PatientMatchId = Convert.ToInt32(dr["PatientMatchId"]),
				KioskGuid = dr["KioskGuid"].ToString(),
				KioskLogoByte = dr["KioskLogo"] == DBNull.Value ? null : dr["KioskLogo"] as byte[],
				AutoConfirmArrival = dr["AutoConfirmArrival"] != DBNull.Value && Convert.ToBoolean(dr["AutoConfirmArrival"]),
				ShowDoctorDelay = dr["ShowDoctorDelay"] != DBNull.Value && Convert.ToBoolean(dr["ShowDoctorDelay"]),
				ForceSurvey = dr["ForceSurvey"] != DBNull.Value && Convert.ToBoolean(dr["ForceSurvey"]),
				SkipSurveyQuestion = dr["SkipSurveyQuestion"] != DBNull.Value && Convert.ToBoolean(dr["SkipSurveyQuestion"]),
				FileasKioskUser = dr["QOFKioskUser"] != DBNull.Value && Convert.ToBoolean(dr["QOFKioskUser"]),
				AppointmentReason = dr["AppointmentReason"] == DBNull.Value ? (bool?)null : Convert.ToBoolean(dr["AppointmentReason"]),
				EarlyArrival = dr["EarlyArrival"] == DBNull.Value ? 0 : Convert.ToInt32(dr["EarlyArrival"]),
				LateArrival = dr["LateArrival"] == DBNull.Value ? 0 : Convert.ToInt32(dr["LateArrival"]),
				ScreenTimeOut = dr["ScreenTimeOut"] == DBNull.Value ? 0 : Convert.ToInt32(dr["ScreenTimeOut"]),
				GeneralMessage = dr["GeneralMessage"] == DBNull.Value ? string.Empty : Convert.ToString(dr["GeneralMessage"]),
				GeneralMessageSpeed = dr["BannerSpeed"] == DBNull.Value ? 0 : Convert.ToInt32(dr["BannerSpeed"]),
				ShowDemographicDetails = dr["ShowDemographicDetails"] != DBNull.Value && Convert.ToBoolean(dr["ShowDemographicDetails"]),
				ScrambleDemographicDetails = dr["ScrambleDemographicDetails"] != DBNull.Value && Convert.ToBoolean(dr["ScrambleDemographicDetails"]),
				AutoConfirmMultipleArrival = dr["AutoConfirmMultipleArrival"] != DBNull.Value && Convert.ToBoolean(dr["AutoConfirmMultipleArrival"]),
				DemographicDetailsDuration = dr["DemographicDetailsDuration"] == DBNull.Value ? 180 : Convert.ToInt32(dr["DemographicDetailsDuration"]),
				AdminPassword = dr["AdminPassword"] == DBNull.Value ? String.Empty : Convert.ToString(dr["AdminPassword"]),
				KioskVersion = dr["Kioskversion"] == DBNull.Value ? string.Empty : Convert.ToString(dr["Kioskversion"]),
				LastStatusModified = dr["LastStatusUpdate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["LastStatusUpdate"]),
				AllowUntimed = dr["AllowUntimed"] != DBNull.Value && Convert.ToBoolean(dr["AllowUntimed"]),
			};
		}

		private Module ToModuleObject(DataRow dr)
		{
			var module = new Module
			{
				Id = Convert.ToInt32(dr["ModuleId"]),
				ModuleName = dr["ModuleName"].ToString().Replace(" ", string.Empty),
				ModuleNameToDisplay = dr["ModuleName"].ToString(),
				TranslationRefId = Convert.ToInt32(dr["TranslationRefId"])
			};
			return module;
		}

		private Language ToLanguageObject(DataRow dr)
		{
			return new Language
			{
				Id = Convert.ToInt32(dr["LanguageId"]),
				LanguageName = dr["LanguageName"].ToString(),
				TranslationRefId = dr["TranslationRefId"] == DBNull.Value ? 0 : Convert.ToInt32(dr["TranslationRefId"]),
				Order = Convert.ToInt32(dr["LanguageOrder"])
			};
		}

		private Questionnaire ToQuestionnaireObject(DataRow dr)
		{
			return new Questionnaire()
			{
				Id = Convert.ToInt32(dr["QuestionnaireId"]),
				Title = Convert.ToString(dr["Title"]),
				IsAnonymous = Convert.ToBoolean(dr["IsAnonymous"]),
				Frequency = (dr["Frequency"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["Frequency"])
			};
		}

		private Organisation ToOrganisationObject(DataRow dr)
		{
			return new Organisation()
			{
				Id = Convert.ToInt32(dr["OrganisationId"]),
				OrganisationName = dr["OrganisationName"].ToString(),
				DatabaseName = dr["OrganisationKey"].ToString(),
				SystemType = ConvertToSystemTypeEnum(Convert.ToInt32(dr["SystemTypeId"])),
				SiteId = dr["SiteDBID"] == DBNull.Value ? (long?)null : Convert.ToInt64(dr["SiteDBID"]),
				SiteName = dr["SiteName"].ToString(),
				MainLocation = dr["MainLocation"] != DBNull.Value && (bool)dr["MainLocation"],
			};
		}

		private static string ConvertToSystemTypeEnum(int systemTypeId)
		{
			string type;
			if (systemTypeId == Convert.ToInt32(SystemType.EmisWeb))
				type = SystemType.EmisWeb.GetDisplayName();
			else if (systemTypeId == Convert.ToInt32(SystemType.EmisPcs))
				type = SystemType.EmisPcs.GetDisplayName();
            else if (systemTypeId == Convert.ToInt32(SystemType.EmisPcsLan))
                type = SystemType.EmisPcsLan.GetDisplayName();
			else if (systemTypeId == Convert.ToInt32(SystemType.TPPSystmOne))
				type = SystemType.TPPSystmOne.GetDisplayName();
			else
				type = SystemType.None.GetDisplayName();

			return type;
		}

		public List<AppointmentSlotType> GetKioskSlotTypes(int kioskId)
		{
			using (var connection = DbManager.GetNewConnection())
			{
				var kioskSlotTypes = new List<AppointmentSlotType>();
				try
				{
					DbManager.Open(connection);

					SqlCommand spCommand = DbManager.GetSprocCommand("[PatientFlow].[GetKioskSlotType]", connection);
					spCommand.Parameters.Add(DbManager.CreateParameter("@KioskId", kioskId));

					using (SqlDataReader dr = spCommand.ExecuteReader())
					{
						while (dr.Read())
						{
							var slotType = new AppointmentSlotType
							{
								SlotTypeId = Convert.ToString(dr["SlotTypeId"]),
								Description = Convert.ToString(dr["Description"]),
								OrganisationId = dr["OrganisationId"] == DBNull.Value ? 0 : Convert.ToInt32(dr["OrganisationId"])
							};
							kioskSlotTypes.Add(slotType);
						}
					}
				}
				finally
				{
					DbManager.Close(connection);
				}

				return kioskSlotTypes;
			}
		}

		public DataTable CreateListDataTable(List<string> entitylist)
		{
			var dtlist = new DataTable("List");

			dtlist.Columns.Add("Id", typeof(int));
			if (entitylist != null && entitylist.Count > 0)
			{
				foreach (var item in entitylist)
				{
					DataRow dr = dtlist.NewRow();

					dr["Id"] = Convert.ToInt32(item);
					dtlist.Rows.Add(dr);
				}
			}

			return dtlist;
		}

		public DataTable CreateListDataTable(List<int> entitylist)
		{
			var dtlist = new DataTable("List");

			dtlist.Columns.Add("Id", typeof(int));
			if (entitylist != null && entitylist.Any())
			{
				foreach (var item in entitylist)
				{
					DataRow dr = dtlist.NewRow();
					dr["Id"] = item;
					dtlist.Rows.Add(dr);
				}
			}

			return dtlist;
		}

		public DataTable CreateListOrderDataTable(List<int> entitylist)
		{
			var dtlist = new DataTable("ListWithOrder");
			int i = 1;
			dtlist.Columns.Add("Id", typeof(int));
			dtlist.Columns.Add("ListOrder", typeof(int));
			if (entitylist != null && entitylist.Count > 0)
			{
				foreach (var item in entitylist)
				{
					DataRow dr = dtlist.NewRow();

					dr["Id"] = item;
					dr["ListOrder"] = i;
					dtlist.Rows.Add(dr);
					i++;
				}
			}

			return dtlist;
		}


		public DataTable CreateKioskLinkOrgSites(List<BrachModel> entitylist)
		{
			var dtlist = new DataTable("KioskLinkOrganisationSites");
			int i = 1;
			dtlist.Columns.Add("KioskLinkOrganisationSitesId", typeof(int));
			dtlist.Columns.Add("OrganisationId", typeof(int));
			dtlist.Columns.Add("SiteId", typeof(int));
			dtlist.Columns.Add("MainLocation", typeof(bool));

			if (entitylist != null && entitylist.Count > 0)
			{
				foreach (var item in entitylist)
				{
					DataRow dr = dtlist.NewRow();
					dr["KioskLinkOrganisationSitesId"] = i;
					dr["OrganisationId"] = item.OrganisationId;
					dr["SiteId"] = item.BranchId;
					dr["MainLocation"] = item.IsMainLocation;
					dtlist.Rows.Add(dr);
					i++;
				}
			}

			return dtlist;
		}


		private DataTable CreateKioskSiteMapList(List<SiteMap> kioskSiteMapList)
		{
			var dtlist = new DataTable("KioskSiteMapList");
			int i = 1;
			dtlist.Columns.Add("KioskSiteMapListId", typeof(int));
			dtlist.Columns.Add("SiteDescription", typeof(string));
			dtlist.Columns.Add("SiteMap", typeof(byte[]));


			foreach (SiteMap item in kioskSiteMapList)
			{
				DataRow dr = dtlist.NewRow();
				dr["KioskSiteMapListId"] = i;
				dr["SiteDescription"] = item.SiteMapName ?? "default";
				dr["SiteMap"] = item.SiteMapImage;
				dtlist.Rows.Add(dr);
				i++;
			}
			return dtlist;
		}

		public int KioskEdit(Kiosk kiosk, string user)
		{
			using (var connection = DbManager.GetNewConnection())
			{
				try
				{
					DataTable[] dtArray = PopulateKioskDatatable(kiosk);
					DbManager.Open(connection);
					SqlCommand spCommand = DbManager.GetSprocCommand("[PatientFlow].[UpdateKioskDetails]",connection);
					spCommand.Parameters.Add(DbManager.CreateParameter("@KioskId", kiosk.Id));
					spCommand.Parameters.Add(DbManager.CreateParameter("@PatientMatchId", kiosk.PatientMatchId));
					spCommand.Parameters.Add(DbManager.CreateParameter("@AppointmentMatchId", kiosk.AppointmentMatchId));
					spCommand.Parameters.Add(DbManager.CreateParameter("@ModuleIdList", dtArray[0], "PatientFlow.List"));
					spCommand.Parameters.Add(DbManager.CreateParameter("@Status", kiosk.Status));
					spCommand.Parameters.Add(DbManager.CreateParameter("@Languages", dtArray[1], "PatientFlow.ListWithOrder"));
					spCommand.Parameters.Add(DbManager.CreateParameter("@KioskName", kiosk.KioskName, 100));
					spCommand.Parameters.Add(DbManager.CreateParameter("@PcName", kiosk.PcName, 200));
					spCommand.Parameters.Add(DbManager.CreateParameter("@Organisations", dtArray[3], "PatientFlow.ListWithOrder"));
					spCommand.Parameters.Add(DbManager.CreateParameter("@Organisationsite", dtArray[4],
						"PatientFlow.KioskLinkOrganisationSites"));
					spCommand.Parameters.Add(DbManager.CreateParameter("@KioskLogo", kiosk.KioskLogoByte));
					spCommand.Parameters.Add(DbManager.CreateParameter("@AutoConfirmArrival", kiosk.AutoConfirmArrival));
					spCommand.Parameters.Add(DbManager.CreateParameter("@ShowDoctorDelay", kiosk.ShowDoctorDelay));
					spCommand.Parameters.Add(DbManager.CreateParameter("@AllowUntimed", kiosk.AllowUntimed));
					spCommand.Parameters.Add(DbManager.CreateParameter("@ForceSurvey", kiosk.ForceSurvey));
					spCommand.Parameters.Add(DbManager.CreateParameter("@SkipSurveyQuestion", kiosk.SkipSurveyQuestion));
					spCommand.Parameters.Add(DbManager.CreateParameter("@QOFKioskUser", kiosk.FileasKioskUser));
					if (kiosk.AppointmentReason != null)
						spCommand.Parameters.Add(DbManager.CreateParameter("@AppointmentReason", kiosk.AppointmentReason.Value));
					spCommand.Parameters.Add(DbManager.CreateParameter("@EarlyArrival", kiosk.EarlyArrival.Value));
					spCommand.Parameters.Add(DbManager.CreateParameter("@LateArrival  ", kiosk.LateArrival.Value));
					spCommand.Parameters.Add(DbManager.CreateParameter("@ScreenTimeOut  ", kiosk.ScreenTimeOut));
					spCommand.Parameters.Add(DbManager.CreateParameter("@Title", kiosk.Title, 200));
					spCommand.Parameters.Add(DbManager.CreateParameter("@AdminPassword", kiosk.AdminPassword, 50));
					spCommand.Parameters.Add(DbManager.CreateParameter("@ShowDemographicDetails", kiosk.ShowDemographicDetails));
					spCommand.Parameters.Add(DbManager.CreateParameter("@ScrambleDemographicDetails", kiosk.ScrambleDemographicDetails));
					spCommand.Parameters.Add(DbManager.CreateParameter("@AutoConfirmMultipleArrival", kiosk.AutoConfirmMultipleArrival));
					spCommand.Parameters.Add(DbManager.CreateParameter("@DemographicDetailsDuration", kiosk.DemographicDetailsDuration));
					spCommand.Parameters.Add(DbManager.CreateParameter("@SlotTypeList", dtArray[2], "PatientFlow.KioskLinkedFields"));
					spCommand.Parameters.Add(DbManager.CreateParameter("@ModifiedBy", user, 50));
					spCommand.Parameters.Add(DbManager.CreateParameter("@KioskSessionHolderList", dtArray[5], "PatientFlow.KioskLinkedFields"));
					spCommand.Parameters.Add(DbManager.CreateParameter("@DemographicDetailsList", dtArray[6], "PatientFlow.List"));
					spCommand.Parameters.Add(DbManager.CreateParameter("@SiteMapList", dtArray[7], "PatientFlow.KioskSiteMapList"));
					return Convert.ToInt32(spCommand.ExecuteScalar());
                }
				finally
				{
					DbManager.Close(connection);
				}
			}
		}

		public void DisconnectKiosk(string connectionId)
		{
			using (var connection = DbManager.GetNewConnection())
			{
				try
				{
					DbManager.Open(connection);

					SqlCommand spCommand = DbManager.GetSprocCommand("[PatientFlow].[DisconnectKiosk]", connection);

					spCommand.Parameters.Add(DbManager.CreateParameter("@ConnectionGuid", connectionId, 50));
					spCommand.ExecuteNonQuery();
				}
				finally
				{
					DbManager.Close(connection);
				}
			}
		}

		public void UpdateKioskStatus(int kioskId, int status)
		{
			using (var connection = DbManager.GetNewConnection())
			{
				try
				{
					DbManager.Open(connection);

					SqlCommand spCommand = DbManager.GetSprocCommand("[PatientFlow].[UpdateKioskStatus]",connection);
					spCommand.Parameters.Add(DbManager.CreateParameter("@KioskId", kioskId));
					spCommand.Parameters.Add(DbManager.CreateParameter("@Status", status));
					spCommand.ExecuteNonQuery();
				}
				finally
				{
					DbManager.Close(connection);
				}
			}
		}

		public int UpdateKioskConnection(string kioskAddress, string connectionId)
		{
			using (var connection = DbManager.GetNewConnection())
			{
				try
				{
					DbManager.Open(connection);

					SqlCommand spCommand = DbManager.GetSprocCommand("[PatientFlow].[UpdateKioskConnection]", connection);

					spCommand.Parameters.Add(DbManager.CreateParameter("@KioskAddress", kioskAddress, 50));
					spCommand.Parameters.Add(DbManager.CreateParameter("@ConnectionGuid", connectionId, 50));

					return Convert.ToInt32(spCommand.ExecuteScalar());
				}
				finally
				{
					DbManager.Close(connection);
				}
			}
		}

		public int GetKioskStatus(int kioskId)
		{
			using (var connection = DbManager.GetNewConnection())
			{
				try
				{
					int status = 0;
					DbManager.Open(connection);

					SqlCommand spCommand = DbManager.GetSprocCommand("[PatientFlow].[GetKioskStatus]", connection);

					spCommand.Parameters.Add(DbManager.CreateParameter("@KioskId", kioskId));
					using (SqlDataReader dr = spCommand.ExecuteReader())
					{
						while (dr.Read())
						{
							status = dr["KioskStatus"] == DBNull.Value ? 0 : Convert.ToInt32(dr["KioskStatus"]);
						}
					}
					return status;
				}
				finally
				{
					DbManager.Close(connection);
				}
			}
		}

		public int GetKioskConnectionStatus(int kioskId)
		{
			using (var connection = DbManager.GetNewConnection())
			{
				try
				{
					int status = 0;
					DbManager.Open(connection);

					SqlCommand spCommand = DbManager.GetSprocCommand("[PatientFlow].[GetKioskConnectionStatus]", connection);

					spCommand.Parameters.Add(DbManager.CreateParameter("@KioskId", kioskId));
					using (SqlDataReader dr = spCommand.ExecuteReader())
					{
						while (dr.Read())
						{
							status = dr["ConnectionStatus"] == DBNull.Value ? 0 : Convert.ToInt32(dr["ConnectionStatus"]);
						}
					}
					return status;
				}
				finally
				{
					DbManager.Close(connection);
				}
			}
		}

		public List<PatientMatch> GetPatientMatchList()
		{
			var patientMatchList = new List<PatientMatch>();
			using (var connection = DbManager.GetNewConnection())
			{
				try
				{
					DbManager.Open(connection);

					SqlCommand spCommand = DbManager.GetSprocCommand("[PatientFlow].[GetPatientMatchList]", connection);
					using (SqlDataReader dr = spCommand.ExecuteReader())
					{
						while (dr.Read())
						{
							var patientMatch = new PatientMatch
							{
								Id = Convert.ToInt32(dr["PatientMatchId"]),
								ScreenTitle = dr["PatientMatchTitle"].ToString(),
								ScreenCode = dr["ScreenOrder"].ToString()
							};
							patientMatchList.Add(patientMatch);
						}
					}
				}
				finally
				{
					DbManager.Close(connection);
				}
			}
			return patientMatchList;
		}

		public List<PatientMatch> GetAppointmentMatchList()
		{
			var patientMatchList = new List<PatientMatch>();
			using (var connection = DbManager.GetNewConnection())
			{
				try
				{
					DbManager.Open(connection);

					SqlCommand spCommand = DbManager.GetSprocCommand("[PatientFlow].[GetAppointmentMatchList]", connection);
					using (SqlDataReader dr = spCommand.ExecuteReader())
					{
						while (dr.Read())
						{
							var patientMatch = new PatientMatch
							{
								Id = Convert.ToInt32(dr["AppointmentMatchId"]),
								ScreenTitle = dr["AppointmentMatchTitle"].ToString(),
								ScreenCode = dr["ScreenOrder"].ToString()
							};
							patientMatchList.Add(patientMatch);
						}
					}
				}
				finally
				{
					DbManager.Close(connection);
				}
			}
			return patientMatchList;
		}

		public List<Module> GetModulesList()
		{
			var moduleList = new List<Module>();
			using (var connection = DbManager.GetNewConnection())
			{
				try
				{
					DbManager.Open(connection);

					SqlCommand spCommand = DbManager.GetSprocCommand("[PatientFlow].[GetModulesList]", connection);
					using (SqlDataReader dr = spCommand.ExecuteReader())
					{
						while (dr.Read())
						{
							var module = new Module
							{
								Id = Convert.ToInt32(dr["ModuleId"]),
								ModuleId = Convert.ToInt32(dr["ModuleId"]),
								ModuleName = dr["ModuleName"].ToString(),
								ModuleNameToDisplay = dr["ModuleName"].ToString()
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

		public List<DemographicDetails> GetDemographicDetailsList()
		{
			var moduleList = new List<DemographicDetails>();
			using (var connection = DbManager.GetNewConnection())
			{
				try
				{
					DbManager.Open(connection);

					SqlCommand spCommand = DbManager.GetSprocCommand("[PatientFlow].[GetDemographicDetailsList]", connection);
					using (SqlDataReader dr = spCommand.ExecuteReader())
					{
						while (dr.Read())
						{
							int demographicDetailsTypeId = Convert.ToInt32(dr["DemographicDetailsTypeId"]);
							if (demographicDetailsTypeId == 2)
								continue;
							var module = new DemographicDetails
							{
								DemographicDetailsTypeId = demographicDetailsTypeId,
								DemographicDetailsType = dr["DemographicDetailsTypeName"].ToString(),
								ModuleNameToDisplay = dr["DemographicDetailsTypeName"].ToString()
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

		public List<Module> GetModulesListForUser()
		{
			var moduleList = new List<Module>();
			using (var connection = DbManager.GetNewConnection())
			{
				try
				{
					DbManager.Open(connection);

					SqlCommand spCommand = DbManager.GetSprocCommand("[PatientFlow].[GetModulesListForUser]", connection);
					using (SqlDataReader dr = spCommand.ExecuteReader())
					{
						while (dr.Read())
						{
							var module = new Module
							{
								Id = Convert.ToInt32(dr["ModuleId"]),
								ModuleId = Convert.ToInt32(dr["ModuleId"]),
								ModuleName = dr["ModuleName"].ToString(),
								ModuleNameToDisplay = dr["ModuleName"].ToString()
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

		public KioskMasterDetails GetKioskMasterDetails()
		{
			var kioskMasterDetails = new KioskMasterDetails();
			using (var connection = DbManager.GetNewConnection())
			{
				try
				{
					DbManager.Open(connection);

					var ds = new DataSet();
					SqlCommand spCommand = DbManager.GetSprocCommand("[PatientFlow].[GetKioskMasterDetails]", connection);
					using (var da = new SqlDataAdapter())
					{
						da.SelectCommand = spCommand;
						da.Fill(ds);

						if(ds.Tables.Count > 0)
						{
							kioskMasterDetails = FillKioskMasterDetails(ds.Tables);
						}
					}
				}
				finally
				{
					DbManager.Close(connection);
				}
			}
			return kioskMasterDetails;
		}

		public KioskMasterDetails GetKioskMasterDetailsByUser()
		{
			var kioskMasterDetails = new KioskMasterDetails();
			using (var connection = DbManager.GetNewConnection())
			{
				try
				{
					DbManager.Open(connection);

					var ds = new DataSet();
					SqlCommand spCommand = DbManager.GetSprocCommand("[PatientFlow].[GetKioskMasterDetailsByUser]", connection);
					using (var da = new SqlDataAdapter())
					{
						da.SelectCommand = spCommand;
						da.Fill(ds);

						if (ds.Tables.Count > 0)
						{
							kioskMasterDetails = FillKioskMasterDetails(ds.Tables);
						}
					}
				}
				finally
				{
					DbManager.Close(connection);
				}
			}
			return kioskMasterDetails;
		}

		private KioskMasterDetails FillKioskMasterDetails(DataTableCollection dtTableCollection)
		{
			var kioskMasterDetails = new KioskMasterDetails();
			kioskMasterDetails.Modules = new List<Module>();

			foreach (DataRow dr in dtTableCollection[0].Rows)
			{
				var module = new Module
				{
					Id = Convert.ToInt32(dr["ModuleId"]),
					ModuleId = Convert.ToInt32(dr["ModuleId"]),
					ModuleName = dr["ModuleName"].ToString(),
					ModuleNameToDisplay = dr["ModuleName"].ToString()
				};
				kioskMasterDetails.Modules.Add(module);
			}

			kioskMasterDetails.Languages = new List<Language>();

			foreach (DataRow dr in dtTableCollection[1].Rows)
			{
				var language = new Language
				{
					Id = Convert.ToInt32(dr["LanguageId"]),
					LanguageName = dr["LanguageName"].ToString(),
					LanguageCode = dr["LanguageCode"].ToString()
				};
				kioskMasterDetails.Languages.Add(language);
			}

			kioskMasterDetails.PatientMatches = new List<PatientMatch>();

			foreach (DataRow dr in dtTableCollection[2].Rows)
			{
				var patientMatch = new PatientMatch
				{
					Id = Convert.ToInt32(dr["PatientMatchId"]),
					ScreenTitle = dr["PatientMatchTitle"].ToString(),
					ScreenCode = dr["ScreenOrder"].ToString()
				};
				kioskMasterDetails.PatientMatches.Add(patientMatch);
			}

			kioskMasterDetails.AppointmentMatches = new List<PatientMatch>();

			foreach (DataRow dr in dtTableCollection[3].Rows)
			{
				var appointmentMatch = new PatientMatch
				{
					Id = Convert.ToInt32(dr["AppointmentMatchId"]),
					ScreenTitle = dr["AppointmentMatchTitle"].ToString(),
					ScreenCode = dr["ScreenOrder"].ToString()
				};
				kioskMasterDetails.AppointmentMatches.Add(appointmentMatch);
			}

			kioskMasterDetails.DemographicDetails = new List<DemographicDetails>();

			foreach (DataRow dr in dtTableCollection[4].Rows)
			{
				int demographicDetailsTypeId = Convert.ToInt32(dr["DemographicDetailsTypeId"]);
				if (demographicDetailsTypeId == 2)
					continue;
				var demographicDetails = new DemographicDetails
				{
					DemographicDetailsTypeId = demographicDetailsTypeId,
					DemographicDetailsType = dr["DemographicDetailsTypeName"].ToString(),
					ModuleNameToDisplay = dr["DemographicDetailsTypeName"].ToString()
				};
				kioskMasterDetails.DemographicDetails.Add(demographicDetails);
			}

			return kioskMasterDetails;
		}

		public List<KioskDetails> GetKioskList()
		{
			var kiosklist = new List<KioskDetails>();
			using (var connection = DbManager.GetNewConnection())
			{
				try
				{
					DbManager.Open(connection);
					SqlCommand spCommand = DbManager.GetSprocCommand("[PatientFlow].[GetKioskList]", connection);

					using (SqlDataReader dr = spCommand.ExecuteReader())
					{
						while (dr.Read())
						{
							var kiosk = new KioskDetails
							{
								Id = Convert.ToInt32(dr["KioskId"]),
								KioskName = Convert.ToString(dr["KioskName"]),
								PcName = Convert.ToString(dr["PCName"]),
								IpAddress = Convert.ToString(dr["IPAddress"]),
								Status = Convert.ToInt32(dr["Status"]),
								Title = Convert.ToString(dr["Title"]),
								KioskStatus = Convert.ToString(dr["StatusName"]),
								ConnectionGuid = Convert.ToString(dr["ConnectionGuid"]),
								KioskGuid = Convert.ToString(dr["KioskGuid"]),
								KioskVersion = dr["Kioskversion"] == DBNull.Value ? string.Empty : Convert.ToString(dr["Kioskversion"]),
								OrganisationId = Convert.ToInt32(dr["OrganisationId"]),
								OrganisationKey = Convert.ToString(dr["OrganisationKey"]),
								OrganisationName = Convert.ToString(dr["OrganisationName"]),
								SystemTypeId = Convert.ToInt32(dr["SystemTypeId"]),
								SystemType = Convert.ToString(dr["SystemType"])
							};
							kiosklist.Add(kiosk);
						}
					}
				}
				finally
				{
					DbManager.Close(connection);
				}
			}
			return kiosklist;
		}

		public IEnumerable<Kiosk> GetKiosks()
		{
			var kioskList = new List<Kiosk>();
			using (var connection = DbManager.GetNewConnection())
			{
				try
				{
					DbManager.Open(connection);
					SqlCommand spCommand = DbManager.GetSprocCommand("[PatientFlow].[GetKiosks]", connection);
					GetKioskDetails(kioskList, spCommand);
				}
				finally
				{
					DbManager.Close(connection);
				}
			}
			return kioskList;
		}

		public IEnumerable<Kiosk> GetKiosksByUser(string user)
		{
			var kioskList = new List<Kiosk>();
			using (var connection = DbManager.GetNewConnection())
			{
				try
				{
					DbManager.Open(connection);
					SqlCommand spCommand = DbManager.GetSprocCommand("[PatientFlow].[GetKiosksByUser]", connection);
					spCommand.Parameters.Add(DbManager.CreateParameter("@User", user, 200));
					GetKioskDetails(kioskList, spCommand);
				}
				finally
				{
					DbManager.Close(connection);
				}
			}
			return kioskList;
		}

		private static void GetKioskDetails(List<Kiosk> kioskList, SqlCommand spCommand)
		{
			using (SqlDataReader dr = spCommand.ExecuteReader())
			{
				while (dr.Read())
				{
					var kiosk = new Kiosk
					{
						Id = Convert.ToInt32(dr["KioskId"]),
						KioskName = Convert.ToString(dr["KioskName"]),
						Title = Convert.ToString(dr["Title"]),
						KioskGuid = Convert.ToString(dr["KioskGuid"]),
						PcName = Convert.ToString(dr["PCName"]),
						IpAddress = Convert.ToString(dr["IPAddress"]),
						KioskVersion = dr["Kioskversion"] == DBNull.Value ? string.Empty : Convert.ToString(dr["Kioskversion"]),
						Status = Convert.ToInt32(dr["Status"]),
						KioskStatus = Convert.ToString(dr["StatusName"]),
						Usage = Convert.ToInt32(dr["Usage"]),
				        ConnectionGuid = dr["ConnectionGuid"] == DBNull.Value ? string.Empty : new Guid(dr["ConnectionGuid"].ToString()).ToString(),
                        OrganisationName = dr["OrganisationName"] == DBNull.Value ? string.Empty : Convert.ToString(dr["OrganisationName"]),
                    };
					kioskList.Add(kiosk);
				}
			}
		}

		public List<Kiosk> GetKioskListForOrganisation(int organisationId)
		{
			var kiosklist = new List<Kiosk>();
			using (var connection = DbManager.GetNewConnection())
			{
				try
				{
					DbManager.Open(connection);

					SqlCommand spCommand = DbManager.GetSprocCommand("[PatientFlow].[GetKioskListForOrganisation]", connection);
					spCommand.Parameters.Add(DbManager.CreateParameter("@OrganisationId", organisationId));
					using (SqlDataReader dr = spCommand.ExecuteReader())
					{
						while (dr.Read())
						{
                            var kiosk = new Kiosk
                            {
                                Id = Convert.ToInt32(dr["KioskId"]),
                                KioskName = Convert.ToString(dr["KioskName"]),
                                PcName = Convert.ToString(dr["PCName"]),
                                IpAddress = Convert.ToString(dr["IPAddress"]),
                                Status = Convert.ToInt32(dr["Status"]),
                                Title = Convert.ToString(dr["Title"]),
                                KioskStatus = Convert.ToString(dr["StatusName"]),
                                KioskVersion = dr["Kioskversion"] == DBNull.Value ? string.Empty : Convert.ToString(dr["Kioskversion"]),
                                KioskGuid = Convert.ToString(dr["KioskGuid"]),
                                ConnectionGuid= dr["ConnectionGuid"] == DBNull.Value
                                                ? string.Empty
                                                : new Guid(dr["ConnectionGuid"].ToString()).ToString()
                        };
							kiosklist.Add(kiosk);
						}
					}
				}
				finally
				{
					DbManager.Close(connection);
				}
			}
			return kiosklist;
		}

		public IEnumerable<Kiosk> GetKioskDetailListForOrganisation(int organisationId)
		{
			return GetKioskDetailListForOrganisation(organisationId, string.Empty);
		}

		public IEnumerable<Kiosk> GetKioskDetailListForOrganisation(int organisationId, string kioskName)
		{
			var kiosklist = new List<Kiosk>();
			using (var connection = DbManager.GetNewConnection())
			{
				try
				{
					DbManager.Open(connection);

					SqlCommand spCommand = DbManager.GetSprocCommand("[PatientFlow].[GetKioskDetailsForOrganisation]", connection);
					spCommand.Parameters.Add(DbManager.CreateParameter("@OrganisationId", organisationId));
					spCommand.Parameters.Add(DbManager.CreateParameter("@KioskName", kioskName, 50));
					using (SqlDataReader dr = spCommand.ExecuteReader())
					{
						while (dr.Read())
						{
							var kiosk = new Kiosk
							{
								Id = Convert.ToInt32(dr["KioskId"]),
								KioskName = Convert.ToString(dr["KioskName"]),
								KioskGuid = Convert.ToString(dr["KioskGuid"])
							};
							kiosklist.Add(kiosk);
						}
					}
				}
				finally
				{
					DbManager.Close(connection);
				}
			}
			return kiosklist;
		}
		
		public int AddKioskDetails(Kiosk kiosk, string user)
		{
            using (var connection = DbManager.GetNewConnection())
			{
				try
				{
					DataTable[] dtArray = PopulateKioskDatatable(kiosk);
					DbManager.Open(connection);
					SqlCommand spCommand = DbManager.GetSprocCommand("[PatientFlow].[AddKioskDetails]",connection);
					spCommand.Parameters.Add(DbManager.CreateParameter("@KioskName", kiosk.KioskName, 50));
					spCommand.Parameters.Add(DbManager.CreateParameter("@PCName", kiosk.PcName, 50));
					spCommand.Parameters.Add(DbManager.CreateParameter("@IPAddress", kiosk.IpAddress, 50));
					spCommand.Parameters.Add(DbManager.CreateParameter("@Title", kiosk.Title, 50));
					spCommand.Parameters.Add(DbManager.CreateParameter("@Organisations", dtArray[3], "PatientFlow.ListWithOrder"));
					spCommand.Parameters.Add(DbManager.CreateParameter("@Organisationsite", dtArray[4],
						"PatientFlow.KioskLinkOrganisationSites"));
					spCommand.Parameters.Add(DbManager.CreateParameter("@PatientMatchId", kiosk.PatientMatchId));
					spCommand.Parameters.Add(DbManager.CreateParameter("@AppointmentMatchId", kiosk.AppointmentMatchId));
					spCommand.Parameters.Add(DbManager.CreateParameter("@Languages", dtArray[1], "PatientFlow.ListWithOrder"));
					spCommand.Parameters.Add(DbManager.CreateParameter("@ModuleIdList", dtArray[0], "PatientFlow.List"));
					spCommand.Parameters.Add(DbManager.CreateParameter("@KioskLogo", kiosk.KioskLogoByte));
					if (kiosk.EarlyArrival != null)
						spCommand.Parameters.Add(DbManager.CreateParameter("@EarlyArrival", kiosk.EarlyArrival.Value));
					if (kiosk.LateArrival != null)
						spCommand.Parameters.Add(DbManager.CreateParameter("@LateArrival  ", kiosk.LateArrival.Value));
					spCommand.Parameters.Add(DbManager.CreateParameter("@ScreenTimeOut  ", kiosk.ScreenTimeOut));
					spCommand.Parameters.Add(DbManager.CreateParameter("@AutoConfirmArrival", kiosk.AutoConfirmArrival));
					spCommand.Parameters.Add(DbManager.CreateParameter("@AutoConfirmMultipleArrival", kiosk.AutoConfirmMultipleArrival));
					spCommand.Parameters.Add(DbManager.CreateParameter("@ShowDoctorDelay", kiosk.ShowDoctorDelay));
					spCommand.Parameters.Add(DbManager.CreateParameter("@AllowUntimed", kiosk.AllowUntimed));
					spCommand.Parameters.Add(DbManager.CreateParameter("@ForceSurvey", kiosk.ForceSurvey));
					spCommand.Parameters.Add(DbManager.CreateParameter("@SkipSurveyQuestion", kiosk.SkipSurveyQuestion));
					spCommand.Parameters.Add(DbManager.CreateParameter("@QOFKioskUser", kiosk.FileasKioskUser));
					spCommand.Parameters.Add(kiosk.AppointmentReason != null
						? DbManager.CreateParameter("@AppointmentReason", kiosk.AppointmentReason.Value)
						: DbManager.CreateNullParameter("@AppointmentReason ", SqlDbType.Bit));
					spCommand.Parameters.Add(DbManager.CreateParameter("@ModifiedBy", user, 50));
					spCommand.Parameters.Add(DbManager.CreateParameter("@GeneralMessage", kiosk.GeneralMessage, 400));
					spCommand.Parameters.Add(DbManager.CreateParameter("@BannerSpeed", kiosk.GeneralMessageSpeed));
					spCommand.Parameters.Add(DbManager.CreateParameter("@ShowDemographicDetails", kiosk.ShowDemographicDetails));
					spCommand.Parameters.Add(DbManager.CreateParameter("@ScrambleDemographicDetails", kiosk.ScrambleDemographicDetails));
					spCommand.Parameters.Add(DbManager.CreateParameter("@DemographicDetailsDuration", kiosk.DemographicDetailsDuration));
					spCommand.Parameters.Add(DbManager.CreateParameter("@AdminPassword", kiosk.AdminPassword, 50));
					spCommand.Parameters.Add(DbManager.CreateParameter("@SlotTypeList", dtArray[2], "PatientFlow.KioskLinkedFields"));
					spCommand.Parameters.Add(DbManager.CreateParameter("@KioskSessionHolderList", dtArray[5], "PatientFlow.KioskLinkedFields"));
					spCommand.Parameters.Add(DbManager.CreateParameter("@DemographicDetailsList", dtArray[6], "PatientFlow.List"));
					spCommand.Parameters.Add(DbManager.CreateParameter("@SiteMapList", dtArray[7], "PatientFlow.KioskSiteMapList"));
					return Convert.ToInt32(spCommand.ExecuteScalar());
				}
				finally
				{
					DbManager.Close(connection);
				}
			}
		}

		private DataTable[] PopulateKioskDatatable(Kiosk kiosk)
		{
			var dtLanguages = new DataTable();
			var dtOrganisations = new DataTable();
			var dtOrganisationBranch = new DataTable();
			var dtModules = new DataTable();
			var dtDepartments = new DataTable();
			var dtMembers = new DataTable();
			var dtKioskSiteMap = new DataTable();
			if (kiosk.SelectedModules != null && kiosk.SelectedModules.Count > 0)
				dtModules = CreateListDataTable(kiosk.SelectedModules);

			if (kiosk.SelectedLanguageList != null && kiosk.SelectedLanguageList.Count > 0)
				dtLanguages = CreateListOrderDataTable(kiosk.SelectedLanguageList);

			var dtSlotTypes = CreateKioskSlotListDataTable(kiosk.SlotTypes);
			
			if (kiosk.SelectedOrganisationList != null && kiosk.SelectedOrganisationList.Count > 0)
				dtOrganisations = CreateListOrderDataTable(kiosk.SelectedOrganisationList);
			if (kiosk.BranchList != null && kiosk.BranchList.Count > 0)
				dtOrganisationBranch = CreateKioskLinkOrgSites(kiosk.BranchList);

			var dtMembersList = CreateKioskSessionHolderListDataTable(kiosk.SessionHolderList);

			var dtDemogrpahicDetails = CreateListDataTable(kiosk.SelectedDemographicDetails);

			dtKioskSiteMap = CreateKioskSiteMapList(kiosk.KioskSiteMapList);

			return new[] { dtModules, dtLanguages, dtSlotTypes, dtOrganisations, dtOrganisationBranch, dtMembersList, dtDemogrpahicDetails, dtKioskSiteMap };
		}

		private DataTable CreateKioskSlotListDataTable(List<AppointmentSlotType> slotTypes)
		{
			var dtlist = new DataTable("KioskLinkedFields");
			int i = 1;
			dtlist.Columns.Add("KioskLinkedFieldsId", typeof(int));
			dtlist.Columns.Add("OrganisationId", typeof(int));
			dtlist.Columns.Add("FieldId", typeof(int));

			if(slotTypes != null && slotTypes.Any())
			{
				foreach(var item in slotTypes)
				{
					DataRow dr = dtlist.NewRow();
					dr["KioskLinkedFieldsId"] = i++;
					dr["OrganisationId"] = item.OrganisationId;
					dr["FieldId"] = item.SlotTypeId;
					dtlist.Rows.Add(dr);
				}
			}
			return dtlist;
		}

		private DataTable CreateKioskSessionHolderListDataTable(List<Member> sessionHolders)
		{
			var dtlist = new DataTable("KioskLinkedFields");
			int i = 1;
			dtlist.Columns.Add("KioskLinkedFieldsId", typeof(int));
			dtlist.Columns.Add("OrganisationId", typeof(int));
			dtlist.Columns.Add("FieldId", typeof(int));

			if (sessionHolders != null && sessionHolders.Any())
			{
				foreach (var item in sessionHolders)
				{
					DataRow dr = dtlist.NewRow();
					dr["KioskLinkedFieldsId"] = i++;
					dr["OrganisationId"] = item.OrganisationId;
					dr["FieldId"] = item.SessionHolderId;
					dtlist.Rows.Add(dr);
				}
			}
			return dtlist;
		}

		public string GetConnectionId(int kioskId)
		{
			using (var connection = DbManager.GetNewConnection())
			{
				try
				{
					DbManager.Open(connection);
					SqlCommand spCommand = DbManager.GetSprocCommand("[PatientFlow].[GetConnectionId]",connection);

					spCommand.Parameters.Add(DbManager.CreateParameter("@KioskId", kioskId));
					using (SqlDataReader dr = spCommand.ExecuteReader())
					{
						string connectionGuid = String.Empty;
						while (dr.Read())
						{
							connectionGuid = dr["ConnectionGuid"] == DBNull.Value
								? string.Empty
								: new Guid(dr["ConnectionGuid"].ToString()).ToString();
						}

						return connectionGuid;
					}
				}
				finally
				{
					DbManager.Close(connection);
				}
			}
		}

		public string GetConnectionIdFromKey(string key)
		{
			using (var connection = DbManager.GetNewConnection())
			{
				try
				{
					DbManager.Open(connection);
					SqlCommand spCommand = DbManager.GetSprocCommand("[PatientFlow].[GetConnectionIdFromKey]", connection);

					spCommand.Parameters.Add(DbManager.CreateParameter("@Key", key, 50));
					using (SqlDataReader dr = spCommand.ExecuteReader())
					{
						string connectionGuid = String.Empty;
						while (dr.Read())
						{
							connectionGuid = dr["ConnectionGuid"] == DBNull.Value ? string.Empty : new Guid(dr["ConnectionGuid"].ToString()).ToString();
						}

						return connectionGuid;
					}
				}
				finally
				{
					DbManager.Close(connection);
				}
			}
		}

		public int GetKioskIdFromKey(string key)
		{
			using (var connection = DbManager.GetNewConnection())
			{
				try
				{
					DbManager.Open(connection);
					SqlCommand spCommand = DbManager.GetSprocCommand("[PatientFlow].[GetKioskIdFromKey]", connection);

					spCommand.Parameters.Add(DbManager.CreateParameter("@Key", key, 50));
					using (SqlDataReader dr = spCommand.ExecuteReader())
					{
						int kioskId = 0;
						while (dr.Read())
						{
							kioskId = dr["KioskId"] == DBNull.Value ? 0 : Convert.ToInt32(dr["KioskId"]);
						}

						return kioskId;
					}
				}
				finally
				{
					DbManager.Close(connection);
				}
			}
		}

		public List<Member> GetLinkedMembersForKiosk(string registrationKey)
		{
			var memberList = new List<Member>();
			using (var connection = DbManager.GetNewConnection())
			{
				try
				{
					DbManager.Open(connection);

					SqlCommand spCommand = DbManager.GetSprocCommand("[PatientFlow].[GetLinkedMemberForKiosk]",connection);
					spCommand.Parameters.Add(DbManager.CreateParameter("@KioskAddress", registrationKey, 50));

					using (SqlDataReader dr = spCommand.ExecuteReader())
					{
						while (dr.Read())
						{
							var member = new Member
							{
								SessionHolderId =
									dr["SessionHolderId"] == DBNull.Value ? 0 : Convert.ToInt32(dr["SessionHolderId"]),
								OrganisationId =
									dr["OrganisationId"] == DBNull.Value ? 0 : Convert.ToInt32(dr["OrganisationId"])
							};
							memberList.Add(member);
						}
					}
				}
				finally
				{
					DbManager.Close(connection);
				}
			}

			return memberList;
		}

		public AdditionalInformation GetAdditionalInformation(string registrationKey)
		{
			var kioskId = GetKioskIdFromKey(registrationKey);
			if (kioskId > 0)
			{
				using (var connection = DbManager.GetNewConnection())
				{

					var additionalInfo = new AdditionalInformation();
					try
					{
						DbManager.Open(connection);
						var ds = new DataSet();

						SqlCommand spCommand = DbManager.GetSprocCommand("[PatientFlow].[GetAdditionalInformationForKiosk]", connection);
						spCommand.Parameters.Add(DbManager.CreateParameter("@KioskId", kioskId));

						using (var da = new SqlDataAdapter())
						{
							da.SelectCommand = spCommand;
							da.Fill(ds);

							if (ds.Tables.Count > 0)
								additionalInfo = ToAdditionalInformation(ds.Tables);
						}
					}
					finally
					{
						DbManager.Close(connection);
					}

					return additionalInfo;
				}
			}
			return new AdditionalInformation();
		}

		private AdditionalInformation ToAdditionalInformation(DataTableCollection dtTableCollection)
		{
			var additionalInfo = new AdditionalInformation();

			additionalInfo.MemberList = new List<Member>();
			foreach (DataRow dr in dtTableCollection[0].Rows)
			{
				var member = new Member
				{
					SessionHolderId =
						dr["SessionHolderId"] == DBNull.Value ? 0 : Convert.ToInt32(dr["SessionHolderId"]),
					OrganisationId =
						dr["OrganisationId"] == DBNull.Value ? 0 : Convert.ToInt32(dr["OrganisationId"]),
					LoginId= dr["LoginId"] == DBNull.Value ? string.Empty :dr["LoginId"].ToString(),
				};
				additionalInfo.MemberList.Add(member);
			}


			additionalInfo.PatientList = new List<Patient>();

			foreach (DataRow dr in dtTableCollection[1].Rows)
			{
                try
                {
                    var patient = new Patient
                    {
                        PatientId = Convert.ToInt32(dr["PatientId"]),
                        PatientMessageId = Convert.ToInt32(dr["PatientMessageId"]),
                        OrganisationId = Convert.ToInt32(dr["OrganisationId"]),
                        Message = Convert.ToString(dr["Message"]),
                        Firstname = string.Empty, //                                dr["Firstname"] == DBNull.Value ? string.Empty : Convert.ToString(dr["Firstname"]).DecryptAES256(),
                        Surname = string.Empty, //  dr["Surname"] == DBNull.Value ? string.Empty : Convert.ToString(dr["Surname"]).DecryptAES256(),
                        Dob = string.Empty, //  dr["DOB"] == DBNull.Value ? string.Empty : Convert.ToString(dr["DOB"]).DecryptAES256()
                    };
                    additionalInfo.PatientList.Add(patient);
                }
                catch 
                {
                    //igrnoring encryption issue as of now
                }
			}

			additionalInfo.AlertList = new List<Alert>();

			foreach (DataRow dr in dtTableCollection[2].Rows)
            {
                var alert = new Alert
                {
                    Id = Convert.ToInt32(dr["AlertId"]),
                    AlertText = Convert.ToString(dr["AlertText"]),
                    AlertType = Convert.ToInt32(dr["AlertType"]),
                    Gender = Convert.ToString(dr["Gender"]),
                    Age2 = Convert.ToInt32(dr["Age2"]),
                    Age1 = Convert.ToInt32(dr["Age1"]),
                    Operation = ConvertOperationForOlderKiosk(dr["Operation"].ToString()),
                    OrganisationIds = new List<int> { Convert.ToInt32(dr["OrganisationId"]) },
                    OrganisationId = Convert.ToInt32(dr["OrganisationId"]),
                    AlertsDisplayType = Convert.ToString(dr["AlertDisplayFormatTypeId"]),
                    LinkedKiosk = new List<string> { dr["KioskGuid"] == DBNull.Value ? "" : dr["KioskGuid"].ToString() }
                };
                additionalInfo.AlertList.Add(alert);
            }

            additionalInfo.DivertList = new List<Divert>();
			foreach (DataRow dr in dtTableCollection[3].Rows)
			{
				var divert = new Divert
				{
					OrganisationId = Convert.ToInt32(dr["OrganisationId"]),
					SessionHolderId = Convert.ToInt32(dr["SessionHolderId"]),
					LoginId = Convert.ToString(dr["LoginId"])
				};
				additionalInfo.DivertList.Add(divert);
			}

			return additionalInfo;
		}

        private static string ConvertOperationForOlderKiosk(string operation)
        {
            string result;

            switch(operation.ToUpper())
            {
                case "OVER":
                    result = "GreaterThan";
                    break;

               case "LESS THAN":
                    result = "LessThan";
                    break;

                default:
                    result = operation;
                    break;
            }
            return result;
        }

        public List<Patient> GetPatientsForKiosk(string registrationKey)
		{
			var patientList = new List<Patient>();
			using (var connection = DbManager.GetNewConnection())
			{
				try
				{
					DbManager.Open(connection);

					SqlCommand spCommand = DbManager.GetSprocCommand("[PatientFlow].[GetPatientsForKiosk]",connection);
					spCommand.Parameters.Add(DbManager.CreateParameter("@KioskAddress", registrationKey, 50));

					using (SqlDataReader dr = spCommand.ExecuteReader())
					{
						while (dr.Read())
						{
							var patient = new Patient
							{
								PatientId = Convert.ToInt32(dr["PatientId"]),
								PatientMessageId = Convert.ToInt32(dr["PatientMessageId"]),
								OrganisationId = Convert.ToInt32(dr["OrganisationId"]),
								Message = Convert.ToString(dr["Message"]),
								Firstname =
									dr["Firstname"] == DBNull.Value ? string.Empty : Convert.ToString(dr["Firstname"]).DecryptAES256(),
								Surname = dr["Surname"] == DBNull.Value ? string.Empty : Convert.ToString(dr["Surname"]).DecryptAES256(),
								Dob = dr["DOB"] == DBNull.Value ? string.Empty : Convert.ToString(dr["DOB"]).DecryptAES256()
							};
							patientList.Add(patient);
						}
					}
				}
				finally
				{
					DbManager.Close(connection);
				}
			}
			return patientList;
		}

		public void SaveAppointmentSlotType(List<AppointmentSlotType> appointmentSlotTypes)
		{
			using (var connection = DbManager.GetNewConnection())
			{
				try
				{
					DbManager.Open(connection);

					var dtSlotType = new DataTable();
					dtSlotType.Columns.Add("AppointmentSlotTypeId", typeof (int));
					dtSlotType.Columns.Add("OrganisationId", typeof (int));
					dtSlotType.Columns.Add("SlotTypeId", typeof (string));
					dtSlotType.Columns.Add("Description", typeof (string));
					int rowcount = 1;
					foreach (var item in appointmentSlotTypes)
					{
						dtSlotType.Rows.Add(rowcount++, item.OrganisationId, item.SlotTypeId, item.Description);
					}

					SqlCommand spCommand = DbManager.GetSprocCommand("[PatientFlow].[SaveSlotType]",connection);

					spCommand.Parameters.Add(DbManager.CreateParameter("@SlotTypeList", dtSlotType, "PatientFlow.AppointmentSlotType"));

					spCommand.ExecuteNonQuery();
				}
				finally
				{
					DbManager.Close(connection);
				}
			}
		}

		public bool ValidateKioskName(string kioskName, int kioskId, List<int> organisationList, out bool result)
		{
			using (var connection = DbManager.GetNewConnection())
			{
				try
				{
					DbManager.Open(connection);

					var dtOrganisations = new DataTable();
					if (organisationList != null && organisationList.Count > 0)
						dtOrganisations = CreateListDataTable(organisationList);

					SqlCommand spCommand = DbManager.GetSprocCommand("[PatientFlow].[ValidateKioskName]",connection);
					var outputParameter = DbManager.CreateOutputParameter("@Result", SqlDbType.Bit);
					spCommand.Parameters.Add(DbManager.CreateParameter("@KioskName", kioskName, 100));
					spCommand.Parameters.Add(DbManager.CreateParameter("@KioskId", kioskId));
					spCommand.Parameters.Add(DbManager.CreateParameter("@OrganisationList", dtOrganisations, "PatientFlow.List"));
					spCommand.Parameters.Add(outputParameter);
					spCommand.ExecuteScalar();
					result = Convert.ToBoolean(outputParameter.Value);
					return result;
				}
				finally
				{
					DbManager.Close(connection);
				}
			}
		}

		public List<AppointmentSlotType> GetAppointmentSlotTypes(int organisationId)
		{
			var appointmentSlotTypes = new List<AppointmentSlotType>();
			using (var connection = DbManager.GetNewConnection())
			{
				try
				{
					DbManager.Open(connection);

					SqlCommand spCommand = DbManager.GetSprocCommand("[PatientFlow].[GetAppointmentSlotTypes]",connection);
					spCommand.Parameters.Add(DbManager.CreateParameter("@OrganisationId", organisationId));

					using (SqlDataReader dr = spCommand.ExecuteReader())
					{
						while (dr.Read())
						{
                            var slotType = new AppointmentSlotType
                            {
                                SlotTypeId = Convert.ToString(dr["SlotTypeId"]),
                                Description = Convert.ToString(dr["Description"]),
                                OrganisationId = Convert.ToInt32(dr["OrganisationId"]),
                                OrganisationName = TryParseString(dr,"OrganisationName")
                            };

							appointmentSlotTypes.Add(slotType);
						}
					}
				}
				finally
				{
					DbManager.Close(connection);
				}
			}
			return appointmentSlotTypes;
		}

		public void DeleteKiosk(int kioskId)
		{
			using (var connection = DbManager.GetNewConnection())
			{
				try
				{
					DbManager.Open(connection);

					SqlCommand spCommand = DbManager.GetSprocCommand("[PatientFlow].[DeleteKiosk]",connection);
					spCommand.Parameters.Add(DbManager.CreateParameter("@KioskId", kioskId));
					spCommand.ExecuteNonQuery();
				}
				finally
				{
					DbManager.Close(connection);
				}
			}
		}

		public void SaveKioskDetails(string registrationKey, string pcName, string ipAddress)
		{
			using (var connection = DbManager.GetNewConnection())
			{
				try
				{
					DbManager.Open(connection);

					SqlCommand spCommand = DbManager.GetSprocCommand("[PatientFlow].[SaveKioskDetails]",connection);

					spCommand.Parameters.Add(DbManager.CreateParameter("@KioskGuid", new Guid(registrationKey)));
					spCommand.Parameters.Add(DbManager.CreateParameter("@PCName", pcName, 50));
					spCommand.Parameters.Add(DbManager.CreateParameter("@IpAddress", ipAddress, 50));
					spCommand.ExecuteNonQuery();
				}
				finally
				{
					DbManager.Close(connection);
				}
			}
		}

		public void SaveKioskDetails(string registrationKey, KioskSystemInformation systemInformation)
		{
			using (var connection = DbManager.GetNewConnection())
			{
				try
				{
					DbManager.Open(connection);

					SqlCommand spCommand = DbManager.GetSprocCommand("[PatientFlow].[SaveKioskDetails]",connection);

					spCommand.Parameters.Add(DbManager.CreateParameter("@KioskGuid", new Guid(registrationKey)));
					spCommand.Parameters.Add(DbManager.CreateParameter("@PCName", systemInformation.PcName, 50));
					spCommand.Parameters.Add(DbManager.CreateParameter("@IpAddress", systemInformation.IpAddress, 50));
					spCommand.Parameters.Add(DbManager.CreateParameter("@KioskVersion", systemInformation.Version, 50));
					spCommand.ExecuteNonQuery();
				}
				finally
				{
					DbManager.Close(connection);
				}
			}
		}

		public List<KioskUsageLog> GetKioskUsageLog(List<Guid> KioskGuids)
		{
			var kioskUsageLogs = new List<KioskUsageLog>();
			using (var connection = DbManager2.GetNewConnection())
			{
				try
				{
					var dtKioskGuids = new DataTable();
					dtKioskGuids = CreateKioskListTable(KioskGuids);

					DbManager2.Open(connection);

					SqlCommand spCommand = DbManager2.GetSprocCommand("[PatientFlow].[GetKioskUsageLog]", connection);
					spCommand.Parameters.Add(DbManager2.CreateParameter("@KiosksList", dtKioskGuids, "PatientFlow.KioskGuidList"));

					using (SqlDataReader dr = spCommand.ExecuteReader())
					{
						while (dr.Read())
						{
							var kioskUsageLog = new KioskUsageLog
							{
                                UsageLog = dr["Usage"] == DBNull.Value ? 0 : Convert.ToInt32(dr["Usage"]),
								KioskGuid = Convert.ToString(dr["KioskGuid"])
							};

							kioskUsageLogs.Add(kioskUsageLog);
						}
					}
				}
				finally
				{
					DbManager2.Close(connection);
				}
			}
			return kioskUsageLogs;
		}

		public DataTable CreateKioskListTable(List<Guid> entitylist)
		{
			var dtlist = new DataTable("List");

			dtlist.Columns.Add("Value", typeof(Guid));

			if (entitylist != null && entitylist.Count > 0)
			{
				foreach (var item in entitylist)
				{
					DataRow dr = dtlist.NewRow();
					dr["Value"] = item;
					dtlist.Rows.Add(dr);
				}
			}

			return dtlist;
		}

        public KioskSyncKeys GetKioskSyncKeys(int kioskId)
        {
            using (var connection = DbManager.GetNewConnection())
            {
				KioskSyncKeys kioskSynckeys = new KioskSyncKeys();

				try
                {
                    DbManager.Open(connection);
                    SqlCommand spcommand = DbManager.GetSprocCommand("[PatientFlow].[GetKioskSyncKeys]", connection);
                    spcommand.Parameters.Add(DbManager.CreateParameter("@KioskId", kioskId));
                    using (SqlDataReader dr = spcommand.ExecuteReader())
                    {
                        while (dr.Read())
                        {
							kioskSynckeys.OrganisationName = Convert.ToString(dr["OrganisationName"]);
							kioskSynckeys.KioskName = Convert.ToString(dr["KioskName"]);
							kioskSynckeys.KioskGuid = Convert.ToString(dr["KioskGuid"]);
							kioskSynckeys.SyncGuid = Convert.ToString(dr["ProductKey"]);
							kioskSynckeys.SyncConnectionGuid = Convert.ToString(dr["SyncConnectionId"]);
                        }
                    }
                }
                finally
                {
                    DbManager.Close();
                }
                return kioskSynckeys;
            }
        }
    }
}
