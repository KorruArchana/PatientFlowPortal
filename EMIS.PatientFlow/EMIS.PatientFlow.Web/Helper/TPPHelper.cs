using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EMIS.PatientFlow.API;
using EMIS.PatientFlow.API.Data;
using EMIS.PatientFlow.API.Utilities;
using EMIS.PatientFlow.Common.Enums;
using EMIS.PatientFlow.Common.Extensions;
using EMIS.PatientFlow.Entities;
using EMIS.PatientFlow.Web.Repository;
using EMIS.PatientFlow.Web.Repository.Interfaces;
using EMIS.PatientFlow.Web.ViewModel;

namespace EMIS.PatientFlow.Web.Helper
{
	public class TPPHelper
	{
		public string GetOrganisation(Credential credential)
		{
			GetOrganisation.ClientIntegrationRequest request = new GetOrganisation.ClientIntegrationRequest
			{
				APIKey = credential.UserName,
				DeviceID = credential.Password,
				DeviceVersion = "1.0",
				RequestUID = "@RequestUID",
				FunctionParameters = new GetOrganisation.ClientIntegrationRequestFunctionParameters()
			};
			string requeststring = Extension.Serialize(request);
			return requeststring;
		}
	}
}