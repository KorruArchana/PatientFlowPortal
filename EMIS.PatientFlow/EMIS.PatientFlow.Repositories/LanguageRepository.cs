using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using EMIS.PatientFlow.Entities;
using EMIS.PatientFlow.Interfaces;
using EMIS.PatientFlow.Repositories.Base;

namespace EMIS.PatientFlow.Repositories
{
    public class LanguageRepository : BaseRepository, ILanguageRepository
    {    
        public IEnumerable<Language> GetLanguageList()
        {
			try
			{
				return DbAccess.GetLanguageList();
			}
           catch(Exception ex)
			{
				GenerateSqlException(ex);
				Logger.Instance.WriteLog(Entities.Enums.LogType.Fatal, ex.Message, ex, "TestUser");
				return new List<Language>();
			}
        } 

        public IEnumerable<Module> GetModules(int pageNo, int pageSize, out int recordCount)
        {
			try
			{
				return DbAccess.GetModules(pageNo, pageSize, out recordCount);
			}
			catch(Exception ex)
			{
				recordCount = -1;
				GenerateSqlException(ex);
				Logger.Instance.WriteLog(Entities.Enums.LogType.Fatal, ex.Message, ex, "TestUser");
				return new List<Module>();
			}
            
        }

        public IEnumerable<Module> GetModuleDetails(int moduleId, string languageCode)
        {
			try
			{
				return DbAccess.GetModuleDetails(moduleId, languageCode);
			}
			catch(Exception ex)
			{
				GenerateSqlException(ex);
				Logger.Instance.WriteLog(Entities.Enums.LogType.Fatal, ex.Message, ex, "TestUser");
				return new List<Module>();
			}
          
        }

        public int SaveModuleTranslations(List<Module> model)
        {
			try
			{
				var dtModuleTranslationList = new DataTable();
				if (model != null && model.Count > 0)
					dtModuleTranslationList = CreateListDataTable(model);

				int translationTypeId = (from transTypeId in model
										 select transTypeId.TranslationTypeId).FirstOrDefault();
				return DbAccess.SaveModuleTranslations(dtModuleTranslationList, translationTypeId);
			}
			catch(Exception ex)
			{
				GenerateSqlException(ex);
				Logger.Instance.WriteLog(Entities.Enums.LogType.Fatal, ex.Message, ex, "TestUser");
				return -1;
			}
           
        }

        public IEnumerable<Translation> GetTranslation(int pageSize, DateTime syncedTranslationDate, out DateTime lastRowModifedDate)
        {
			try
			{
				return DbAccess.GetTransation(pageSize, syncedTranslationDate, out lastRowModifedDate);
			}
			catch(Exception ex)
			{
			    lastRowModifedDate = DateTime.Now;
				GenerateSqlException(ex);
				Logger.Instance.WriteLog(Entities.Enums.LogType.Fatal, ex.Message, ex, "TestUser");
				return new List<Translation>();
			}
         
        }

        public IEnumerable<ScreenControl> GetScreenControl(int pageSize, DateTime lastSyncedDate, out DateTime lastRowModifedDate)
        {
			try
			{
				return DbAccess.GetScreenControls(pageSize, lastSyncedDate, out lastRowModifedDate);
			}
			catch(Exception ex)
			{
				lastRowModifedDate = DateTime.Now;
				GenerateSqlException(ex);
				Logger.Instance.WriteLog(Entities.Enums.LogType.Fatal, ex.Message, ex, "TestUser");
				return new List<ScreenControl>();
			}
           
        }

        public DataTable CreateListDataTable(List<Module> entitylist)
        {
			try
			{
				var dtlist = new DataTable("ModuleTranslations");
				dtlist.Columns.Add("Id", typeof(int));
				dtlist.Columns.Add("LanguageCode", typeof(string));
				dtlist.Columns.Add("TranslatedText", typeof(string));
				dtlist.Columns.Add("TranslationRefId", typeof(int));
				dtlist.Columns.Add("ModifiedBy", typeof(string));
				foreach (var item in entitylist)
				{
					var dr = dtlist.NewRow();
					dr["Id"] = Convert.ToInt32(item.Id);
					dr["LanguageCode"] = Convert.ToString(item.Language.LanguageCode);
					dr["TranslatedText"] = Convert.ToString(item.TranslatedText);
					dr["TranslationRefId"] = Convert.ToInt32(item.TranslationRefId);
					dr["ModifiedBy"] = CurrentUser;
					dtlist.Rows.Add(dr);
				}

				return dtlist;
			}
			catch(Exception ex)
			{
				GenerateSqlException(ex);
				Logger.Instance.WriteLog(Entities.Enums.LogType.Fatal, ex.Message, ex, "TestUser");
				return null;
			}
          
        }
    }
}
