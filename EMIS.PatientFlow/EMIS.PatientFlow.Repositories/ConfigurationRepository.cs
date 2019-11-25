using EMIS.PatientFlow.Entities;
using EMIS.PatientFlow.Interfaces;
using EMIS.PatientFlow.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMIS.PatientFlow.Repositories
{
    public class ConfigurationRepository: BaseRepository, IConfigurationRepository
    {
      public WebUser GetPatientFlowUser()
      {
          return new WebUser();
      }
    }
}
