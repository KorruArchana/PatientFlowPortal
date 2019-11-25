using System;
using System.Collections.Generic;
using System.Web.Http;
using EMIS.PatientFlow.Entities;
using EMIS.PatientFlow.Interfaces;
using EMIS.PatientFlow.Services.Helper;

namespace EMIS.PatientFlow.Services.Controllers
{
    
    public class LanguageController : ApiController
    {
        private readonly ILanguageRepository _repository;
        private readonly ILoggerRepository _logger;
        public LanguageController(ILanguageRepository languageRepository, ILoggerRepository loggerRepository)
        {
            _repository = languageRepository;
            _logger = loggerRepository;
        }

        public IEnumerable<Language> GetLanguageList()
        {
            try
            {
                return _repository.GetLanguageList();
            }
            catch (Exception ex)
            {
                _logger.WriteLog(Entities.Enums.LogType.Error, ex.Message);
                throw;
            }
        }
        [Authorize(Roles = "EMIS Super User")]
        public dynamic GetModules(int pageNo, int pageSize)
        {
            try
            {
                int count;
                return new
                {
                    ModuleList = _repository.GetModules(pageNo, pageSize, out count),
                    TotalCount = count
                };
            }
            catch (Exception ex)
            {
                _logger.WriteLog(Entities.Enums.LogType.Error, ex.Message);
                throw;
            }
        }
        [Authorize(Roles = "EMIS Super User")]
        public IEnumerable<Module> GetModuleDetails(int moduleId, string languageCode)
        {
            try
            {
                return _repository.GetModuleDetails(moduleId, languageCode);
            }
            catch (Exception ex)
            {
                _logger.WriteLog(Entities.Enums.LogType.Error, ex.Message);
                throw;
            }
        }

        [Authorize(Roles = "EMIS Super User")]
        public int SaveModuleTranslations([FromBody]string value)
        {
            List<Module> model = JSONHelper.Deserialize<List<Module>>(value);
            int result;
            try
            {
                result = _repository.SaveModuleTranslations(model);
            }
            catch (Exception ex)
            {
                _logger.WriteLog(Entities.Enums.LogType.Error, ex.Message);
                throw;
            }
            return result;
        }
    }
}
