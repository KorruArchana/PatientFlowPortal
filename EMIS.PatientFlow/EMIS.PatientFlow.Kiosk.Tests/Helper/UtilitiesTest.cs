using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using PatientFlowKiosk = EMIS.PatientFlow.Kiosk;

namespace EMIS.PatientFlow.Kiosk.Tests.Helper
{
	public class UtilitiesTest
	{
		
		[Fact]
		public void MatchPatientTest()
		{
			PatientFlowKiosk.Helper.Utilities.MatchPatient();
		}

		[Fact]
		public void ClearPatientMatchingAppValuesTest()
		{
			PatientFlowKiosk.Helper.Utilities.ClearPatientMatchingAppValues();
		}

		[Theory]
		[InlineData("http://localhost:52704/", "ServerURI")]
		[InlineData("PASS", "FileRecordAPIPwd")]
		public void GetAppSettingValueTest(string expected, string key)
		{
			/* It returns null... It's always better to write GetAppSettingValue using dependency injection
			which makes the code more easily testable */
			string actual = PatientFlowKiosk.Helper.Utilities.GetAppSettingValue(key);
			Assert.Equal(actual, expected);
		}

		[Theory]
		[InlineData("P_SURNAME")]
		[InlineData("P_GENDER")]
		[InlineData("P_YEAR")]
		[InlineData("P_DAY")]
		[InlineData("P_MONTH")]
		[InlineData("DateofBirth")]
		public void PatientMatchScreenNavigationTest(string screenCode)
		{
			PatientFlowKiosk.Helper.Utilities.PatientMatchScreenNavigation(screenCode);
			// Has to cover the exception case.. Catch code in this
		}

		[Theory]
		[InlineData("PatientMatchArrival")]
		[InlineData("PatientMatchBooking")]
		public void SetPatientMatchFirstPageTest(string configType)
		{
			//Has to see and mock the kioskconfig so that we can get data and do this
			PatientFlowKiosk.Helper.Utilities.SetPatientMatchFirstPage(configType);
		}

	}
}
