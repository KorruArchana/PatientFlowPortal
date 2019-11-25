using System.Collections.Generic;
using System.Linq;
using System.Xml;
using EMIS.PatientFlow.Kiosk.Enum;
using EMIS.PatientFlow.Kiosk.Model;

namespace EMIS.PatientFlow.Kiosk.Helper
{
	public static class CurrentLanguageText
	{
		public static Dictionary<LanguageText, string> LoadLanguage(int langId)
		{
			var doc = new XmlDocument();
			doc.Load(@"Assets\LanguageFile\KioskLanguageFile.xml");

			return System.Enum.GetValues(typeof (LanguageText)).Cast<LanguageText>().ToDictionary(enumValue => enumValue, enumValue => GetLanguageText(doc, (int) enumValue, langId));
		}

		private static string GetLanguageText(XmlDocument doc, int wordId, int languageId)
		{
			XmlNode itemNode = doc.SelectSingleNode(string.Format("/Translation/Words/Word[@Id={0}]/Text[@LanguageId={1}]", wordId, languageId));

			if (itemNode != null)
				return itemNode.InnerText;
			XmlNode itemNode1 = doc.SelectSingleNode(string.Format("/Translation/Words/Word[@Id={0}]", wordId));
			if (itemNode1 != null && itemNode1.Attributes != null) 
				return itemNode1.Attributes["defaultText"].Value;

			return "";
		}

		public static List<LanguageModel> GetLanguageDetails()
		{
			var globalLanguageList = new List<LanguageModel>();
			var doc = new XmlDocument();
			doc.Load(@"Assets\LanguageFile\KioskLanguageFile.xml");

			for (var langId = 1; langId <= 40; langId++)
			{
				XmlNode itemNode = doc.SelectSingleNode(string.Format("/Translation/Languages/Language[@Id={0}]", langId));
				if (itemNode == null) continue;
				if (itemNode.Attributes == null) continue;
				string languageName = itemNode.Attributes["Name"].Value;
				var displayText = itemNode.Attributes["Text"].Value;

				if (languageName == null) continue;
				var model = new LanguageModel
				{
					LanguageId = langId,
					LanguageName = languageName,
					DisplayText = string.IsNullOrEmpty(displayText) ? languageName : displayText,
					LanguageFullSizePhotoPath = string.Format(
						"pack://application:,,,/Assets/Icons/Flags/FullSizeFlags/{0}.png",
						languageName),
					LanguageSquarePhotoPath = string.Format("pack://application:,,,/Assets/Icons/Flags/SquareFlags/{0}.png",
						languageName)
				};

				globalLanguageList.Add(model);
			}
			
			return globalLanguageList;
		}

	}
}