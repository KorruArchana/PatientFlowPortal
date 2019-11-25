using EMIS.PatientFlow.Common.Enums;
using System;
using System.Reflection;
using Xunit;
using PatientFlowWeb = EMIS.PatientFlow.Web;

namespace EMIS.PatientFlow.Web.Tests.Helper
{
	public class APIHelperTest
	{
		PatientFlowWeb.Helper.ApiHelper apiHelper;
		Type type;
		BindingFlags bindingFlags;

		public APIHelperTest()
		{
			apiHelper = new PatientFlowWeb.Helper.ApiHelper();
			type = typeof(PatientFlowWeb.Helper.ApiHelper);
			bindingFlags = BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static;
		}

		[Theory]
		[InlineData("6001", "D1")]
		[InlineData("6002", "D2")]
		[InlineData("6003", "D3")]
		[InlineData("6004", "D4")]
		[InlineData("6005", "D5")]
		[InlineData("6006", "D6")]
		[InlineData("6007", "6007")]
		public void SwitchPCSSlotTypeTest(string expected, string typeId)
		{
			var myMethod = type.GetMethod("SwitchPCSSlotType", bindingFlags);
			object[] parameterValues = new object[] { typeId };

			var actualResult = myMethod.Invoke(apiHelper, parameterValues);
			Assert.Equal(expected, actualResult);
		}

		[Theory]
		[InlineData(SystemType.EmisWeb, "EMIS - WEB")]
		[InlineData(SystemType.EmisPcs, "EMIS - PCS")]
		[InlineData(SystemType.None, "None")]
		public void ConvertToSystemTypeTest(SystemType expected, string toString)
		{
			var myMethod = type.GetMethod("ConvertToSystemType", bindingFlags);
			object[] parameterValues = new object[] { toString };

			var actualResult = myMethod.Invoke(apiHelper, parameterValues);
			Assert.Equal(expected, actualResult);
		}
	}
}
