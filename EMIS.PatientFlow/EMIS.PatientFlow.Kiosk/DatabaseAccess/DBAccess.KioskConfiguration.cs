using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Windows.Media.Imaging;
using EMIS.PatientFlow.API;
using EMIS.PatientFlow.Common.Enums;
using EMIS.PatientFlow.Common.Extensions;
using EMIS.PatientFlow.Kiosk.Helper;
using EMIS.PatientFlow.Kiosk.Model;

namespace EMIS.PatientFlow.Kiosk.DatabaseAccess
{
	public partial class DbAccess
	{
		/// <summary>
		/// Fetch config value from database based on config type
		/// </summary>
		/// <typeparam name="T">return class object</typeparam>
		/// <param name="configType">config type</param>
		/// <returns>configuration value</returns>
		public T GetKioskConfiguration<T>(string configType)
		{
			T resultObj = default(T);
			const string spKioskConfig = "[PatientFlow].[GetKioskConfiguration]";

			using (var connection = DbManager.GetNewConnection())
			{
				try
				{
					DbManager.Open(connection);
					GlobalVariables.IsDbConnected = true;

					using (SqlCommand spCommand = DbManager.GetSprocCommand(spKioskConfig, connection))
					{
						spCommand.Parameters.Add(DbManager.CreateParameter("@ConfigType", configType, 100));
						spCommand.Parameters.Add(DbManager.CreateParameter("@KioskId", KioskId, 50));

						using (SqlDataReader dr = spCommand.ExecuteReader())
						{
							while (dr.Read())
							{
								if (dr["Value"] != DBNull.Value)
								{
									string value = Convert.ToString(dr["Value"]);
									resultObj = value.ConvertFromJsonString<T>();
								}
								else
								{
									string value = string.Empty;
									resultObj = value.ConvertFromJsonString<T>();
								}
							}
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
					DbManager.Close(connection);
				}
			}

			return resultObj;
		}

		/// <summary>
		/// Get web client/pcs credentials based on organisation
		/// </summary>
		/// <returns>credential object</returns>
		public Credential GetPatientFlowUser()
		{
			Credential credential = default(Credential);
			const string spApiConfig = "[PatientFlow].[GetPatientFlowUserByOrganisationId]";
			try
			{
				DbManager.Open();
				GlobalVariables.IsDbConnected = true;
				SqlCommand spCommand = DbManager.GetSprocCommand(spApiConfig);
				spCommand.Parameters.Add(DbManager.CreateParameter("@OrganisationId", Convert.ToInt32(GlobalVariables.SelectedOrganisation.OrganisationId)));

				using (SqlDataReader dr = spCommand.ExecuteReader())
				{
					while (dr.Read())
					{
						credential = new Credential()
						{
							IpAddress = dr["IPAddress"] == DBNull.Value ? string.Empty : Convert.ToString(dr["IPAddress"]),
							OrganisationKey = dr["OrganisationKey"] == DBNull.Value ? string.Empty : Convert.ToString(dr["OrganisationKey"]),
							SupplierId = dr["SupplierId"] == DBNull.Value ? string.Empty : Convert.ToString(dr["SupplierId"]).DecryptAES256(),
							UserName = dr["UserName"] == DBNull.Value ? string.Empty : Convert.ToString(dr["UserName"]).DecryptAES256(),
							Password = dr["Password"] == DBNull.Value ? string.Empty : Convert.ToString(dr["Password"]).DecryptAES256(),
							WebServiceUrl = dr["WebServiceUrl"] == DBNull.Value ? string.Empty : Convert.ToString(dr["WebServiceUrl"]),
							SystemType = dr["SystemType"] == DBNull.Value ? SystemType.None : ConvertToSystemType(Convert.ToString(dr["SystemType"])),
						};
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
			GlobalVariables.InvalidCredentials = credential == null;
			return credential;
		}

		private static SystemType ConvertToSystemType(string toString)
		{
			switch (toString.ToUpper())
			{
				case "EMIS - WEB":
					return SystemType.EmisWeb;

				case "EMIS - PCS":
					return SystemType.EmisPcs;

				case "TOPAS":
					return SystemType.Topas;

				case "TPP - SYSTMONE":
					return SystemType.TPPSystmOne;

				default:
					return SystemType.None;

			}
		}

		public Options GetModule(List<Options> moduleOptions, int moduleId)
		{
			const string spModuleList = "[PatientFlow].[GetModules]";
			Options module = null;
			try
			{
				DbManager.Open();
				GlobalVariables.IsDbConnected = true;
				DataTable dtModules = GetModuleDataTable(moduleOptions);

				SqlCommand spCommand = DbManager.GetSprocCommand(spModuleList);
				spCommand.Parameters.Add(DbManager.CreateParameter("@Module", dtModules, "PatientFlow.KioskModule"));
				spCommand.Parameters.Add(DbManager.CreateParameter("@LanguageId", GlobalVariables.SelectedLanguageId));
				spCommand.Parameters.Add(DbManager.CreateParameter("@ModuleId", moduleId));
				using (SqlDataReader dr = spCommand.ExecuteReader())
				{
					while (dr.Read())
					{
						module = new Options
						{
							ModuleName = dr["ModuleName"] == DBNull.Value ? string.Empty : Convert.ToString(dr["ModuleName"]),
							ModuleNameToDisplay = dr["ModuleNameToDisplay"] == DBNull.Value ? string.Empty : Convert.ToString(dr["ModuleNameToDisplay"])
						};
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

			return module;
		}

		/// <summary>
		/// Get the module details as a data table
		/// </summary>
		/// <param name="modules">module list</param>
		/// <returns>data table</returns>
		public DataTable GetModuleDataTable(List<Options> modules)
		{
			var dtModules = new DataTable();
			dtModules.Columns.Add("ModuleId", typeof(int));
			dtModules.Columns.Add("ModuleName", typeof(string));
			dtModules.Columns.Add("ModuleNameToDisplay", typeof(string));
			dtModules.Columns.Add("TranslationRefId", typeof(Int64));

			foreach (var item in modules)
			{
				dtModules.Rows.Add(
					item.Id,
					item.ModuleName,
					item.ModuleNameToDisplay,
					item.TranslationRefId);
			}

			return dtModules;
		}

		public List<SiteMapImage> GetSiteMapImage()
		{
			const string spKioskLogo = "[PatientFlow].[GetKioskSiteMap]";
			try
			{
				var kioskSiteMapList = new List<SiteMapImage>();
				DbManager.Open();

				SqlCommand spCommand = DbManager.GetSprocCommand(spKioskLogo);
				spCommand.Parameters.Add(DbManager.CreateParameter("@KioskId", KioskId, 50));

				using (SqlDataReader dr = spCommand.ExecuteReader())
				{
					while (dr.Read())
					{
						byte[] imageData = dr["SiteMap"] == DBNull.Value ? null : (byte[])dr["SiteMap"];
						if (imageData == null || imageData.Length == 0)
						{
							continue;
						}
						var siteMap = new SiteMapImage
						{
							SiteMapName = dr["SiteDescription"] == DBNull.Value ? string.Empty : Convert.ToString(dr["SiteDescription"]),
							SiteMapImage = imageData,
							Image = GetBitmapImage(imageData)
						};
						kioskSiteMapList.Add(siteMap);
					}
				}

				return kioskSiteMapList;
			}
			catch (Exception ex)
			{
				Logger.Instance.WriteLog(LogType.Error, ex.Message, ex, KioskId);
				return null;
			}
			finally
			{
				DbManager.Close();
			}
		}

		public BitmapImage GetLogoImage()
		{
			const string spKioskLogo = "[PatientFlow].[GetKioskLogo]";
			try
			{

				byte[] imageData = null;

				DbManager.Open();

				SqlCommand spCommand = DbManager.GetSprocCommand(spKioskLogo);

				spCommand.Parameters.Add(DbManager.CreateParameter("@KioskId", KioskId, 50));

				using (SqlDataReader dr = spCommand.ExecuteReader())
				{
					while (dr.Read())
					{
						imageData = dr["Logo"] != DBNull.Value ? (byte[])dr["Logo"] : null;
					}
				}

				if (imageData == null || imageData.Length == 0)
				{
					GlobalVariables.IsKioskLogoPresent = false;
					return null;
				}
				else
				{
					GlobalVariables.IsKioskLogoPresent = true;
				}

				var image = GetBitmapImage(imageData);
				return image;
			}
			catch (Exception ex)
			{
				GlobalVariables.IsKioskLogoPresent = false;
				return null;
			}
			finally
			{
				//Close connection
				DbManager.Close();
			}
		}

		private  BitmapImage GetBitmapImage(byte[] imageData)
		{
			try
			{
				BitmapImage image = new BitmapImage();
				using (var mem = new MemoryStream(imageData))
				{
					mem.Position = 0;
					image.BeginInit();
					image.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
					image.CacheOption = BitmapCacheOption.OnLoad;
					image.UriSource = null;
					image.StreamSource = mem;
					image.EndInit();
				}

				image.Freeze();
				return image;
			}
			catch (Exception ex)
			{
				Logger.Instance.WriteLog(LogType.Error, ex.Message, ex, KioskId);
				return null;
			}
		}

		public void SaveWebClientConfiguration(ApiUser user)
		{
			try
			{
				DbManager.Open();

				SqlCommand spCommand = DbManager.GetSprocCommand("[PatientFlow].[SavePatientFlowUser]");

				spCommand.Parameters.Add(DbManager.CreateParameter("@UserName", user.UserName.Decrypt().EncryptAES256(), 150));
				spCommand.Parameters.Add(DbManager.CreateParameter("@Password", user.Password.Decrypt().EncryptAES256(), 150));
				spCommand.Parameters.Add(DbManager.CreateParameter("@SupplierId", user.SupplierId.Decrypt().EncryptAES256(), 150));
				spCommand.Parameters.Add(DbManager.CreateParameter("@DatabaseName", user.DatabaseName, 50));
				spCommand.Parameters.Add(DbManager.CreateParameter("@IPAddress", user.IpAddress, 50));
				spCommand.Parameters.Add(DbManager.CreateParameter("@Type", user.Type, 15));
				spCommand.Parameters.Add(DbManager.CreateParameter("@WebServiceUrl", user.WebServiceUrl, 500));
				spCommand.Parameters.Add(DbManager.CreateParameter("@OrganisationId", user.OrganisationId));

				spCommand.ExecuteNonQuery();
			}
			finally
			{
				//Close connection
				DbManager.Close();
			}
		}
	}
}
