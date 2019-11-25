using System;
using System.Windows;
using EMIS.PatientFlow.Common.Enums;
using EMIS.PatientFlow.Common.Extensions;
using EMIS.PatientFlow.Kiosk.DatabaseAccess.Repository.Interfaces;
using EMIS.PatientFlow.Kiosk.Enum;
using EMIS.PatientFlow.Kiosk.Helper;
using EMIS.PatientFlow.Kiosk.Model;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;

namespace EMIS.PatientFlow.Kiosk.ViewModel
{
	public class PatientDemographicDetailsViewModel : ViewModelBase
	{
		private string _thanksText;
		private string _patientNameText;
		private string _notYouText;
		private string _pageTitleText;
		private string _messagesTitleText;
		private DemographicListCollection _demographicListCollection;
		private string _detailsUptodateText;
		private string _nextButtonText;
		private string _cancelButtonButtonText;
		private bool? _maskTitle;

		private RelayCommand<AppPages> _nextCommand;
		private RelayCommand<AppPages> _cancelCommand;
		private RelayCommand<string> _loadedCommand;


		public bool? MaskTitle
		{
			get
			{
				return _maskTitle;
			}

			set
			{
				_maskTitle = value;
				RaisePropertyChanged("MaskTitle");
			}
		}

		public string ThanksText
		{
			get
			{
				return _thanksText;
			}

			set
			{
				_thanksText = value;
				RaisePropertyChanged("ThanksText");
			}
		}

		public string PatientNameText
		{
			get
			{
				return _patientNameText;
			}

			set
			{
				_patientNameText = value;
				RaisePropertyChanged("PatientNameText");
			}
		}

		public string NotYouText
		{
			get
			{
				return _notYouText;
			}

			set
			{
				_notYouText = value;
				RaisePropertyChanged("NotYouText");
			}
		}

		public string PageTitleText
		{
			get
			{
				return _pageTitleText;
			}

			set
			{
				_pageTitleText = value;
				RaisePropertyChanged("PageTitleText");
			}
		}

		public string MessagesTitleText
		{
			get
			{
				return _messagesTitleText;
			}

			set
			{
				_messagesTitleText = value;
				RaisePropertyChanged("MessagesTitleText");
			}
		}

		public DemographicListCollection DemographicListCollection
		{
			get
			{
				return _demographicListCollection;
			}

			set
			{
				_demographicListCollection = value;
				RaisePropertyChanged("DemographicListCollection");
			}
		}

		public string DetailsUptodateText
		{
			get
			{
				return _detailsUptodateText;
			}

			set
			{
				_detailsUptodateText = value;
				RaisePropertyChanged("DetailsUptodateText");
			}
		}

		public string NextButtonText
		{
			get { return _nextButtonText; }
			set
			{
				_nextButtonText = value;
				RaisePropertyChanged("NextButtonText");
			}
		}

		public string CancelButtonText
		{
			get { return _cancelButtonButtonText; }
			set
			{
				_cancelButtonButtonText = value;
				RaisePropertyChanged("CancelButtonText");
			}
		}

		public RelayCommand<AppPages> NextCommand
		{
			get
			{
				return _nextCommand
					   ?? (_nextCommand = new RelayCommand<AppPages>(
						   p =>
						   {
							   SaveDemographicDetailsFrequency(true);
							   Messenger.Default.Send(AppPages.FinishRouting);
						   }));
			}
		}

		public RelayCommand<AppPages> CancelCommand
		{
			get
			{
				return _cancelCommand
					   ?? (_cancelCommand = new RelayCommand<AppPages>(
						   p =>
						   {
							   SaveDemographicDetailsFrequency(false);
							   Messenger.Default.Send(AppPages.ShowGpDemographicMessages);
						   }));
			}
		}

		public RelayCommand<string> LoadedCommand
		{
			get
			{
				return _loadedCommand
					   ?? (_loadedCommand = new RelayCommand<string>(
						   p =>
						   {
							   if (!GlobalVariables.UserDemographicDetailsSettings.ShowDetails)
							   {
								   Messenger.Default.Send(AppPages.Finish);
							   }
							   else if (!GlobalVariables.IsDbConnected)
							   {
								   Messenger.Default.Send(AppPages.ExceptionDivert);
							   }
						   }));
			}
		}

		public PatientDemographicDetailsViewModel()
		{
			InitializeControls();
		}

		private void InitializeControls()
		{
			ThanksText = GlobalVariables.SelectedLanguageIdText[LanguageText.ThanksText];
			PatientNameText = GlobalVariables.ArrivedPatientName;
			NotYouText = GlobalVariables.SelectedLanguageIdText[LanguageText.NotYou];
			PageTitleText = GlobalVariables.SelectedLanguageIdText[LanguageText.UpToDateInfoMessage];
			MessagesTitleText = GlobalVariables.SelectedLanguageIdText[LanguageText.PleaseCheckDetails];
			DetailsUptodateText = GlobalVariables.SelectedLanguageIdText[LanguageText.DetailsUptodateText];
			NextButtonText = GlobalVariables.SelectedLanguageIdText[LanguageText.YesText];
			CancelButtonText = GlobalVariables.SelectedLanguageIdText[LanguageText.NoText];

			var demolist = GlobalVariables.UserDemographicDetailsSettings.UserDemographicLists;
			if (GlobalVariables.ArrivedPatientDetails != null && GlobalVariables.ArrivedPatientDetails.BookedPatient != null)
			{
				bool maskValue = GlobalVariables.UserDemographicDetailsSettings.ScrambleDemographicDetails;
				MaskTitle = maskValue ? true : (bool?)null;
				Patient bookedPatient = GlobalVariables.ArrivedPatientDetails.BookedPatient;
				DemographicListCollection = new DemographicListCollection();
				const string colon = ": ";
				foreach (var list in demolist)
				{
					try
					{
						switch (list.DemographicDetailsType.ToUpper())
						{
							case "NAME":
								DemographicListCollection.Add(new DemographicDetail { IsEnabled = true, Name = GlobalVariables.SelectedLanguageIdText[LanguageText.Name] + colon, DetailText = GetName() });
								break;
							case "POSTCODE":
								DemographicListCollection.Add(new DemographicDetail { IsEnabled = true, Name = GlobalVariables.SelectedLanguageIdText[maskValue ? LanguageText.PostCode : LanguageText.PostCodeUnMasked] + colon, DetailText = GetPostCode(bookedPatient, maskValue) });
								break;
							case "EMAIL ADDRESS":
								if (GlobalVariables.ArrivedPatientDetails.DisplayTime != "Appointment" || !string.IsNullOrWhiteSpace(bookedPatient.Email))
									DemographicListCollection.Add(new DemographicDetail { IsEnabled = true, Name = GlobalVariables.SelectedLanguageIdText[maskValue ? LanguageText.EmailAddress : LanguageText.EmailAddressUnMasked] + colon, DetailText = GetEmail(bookedPatient, maskValue) });
								break;
							case "MOBILE":
								if (GlobalVariables.ArrivedPatientDetails.DisplayTime != "Appointment" || !string.IsNullOrWhiteSpace(bookedPatient.Mobile))
									DemographicListCollection.Add(new DemographicDetail { IsEnabled = true, Name = GlobalVariables.SelectedLanguageIdText[maskValue ? LanguageText.MobileNumber : LanguageText.MobileNumberUnMasked] + colon, DetailText = GetMobile(bookedPatient, maskValue) });
								break;
							case "TELEPHONE":
								DemographicListCollection.Add(new DemographicDetail { IsEnabled = true, Name = GlobalVariables.SelectedLanguageIdText[maskValue ? LanguageText.TelephoneNumber : LanguageText.TelephoneNumberUnMasked] + colon, DetailText = GetTelephone(bookedPatient, maskValue) });
								break;
							case "GP PRACTICE NAME":
								DemographicListCollection.Add(new DemographicDetail { IsEnabled = true, Name = GlobalVariables.SelectedLanguageIdText[LanguageText.GpName] + colon, DetailText = GlobalVariables.ArrivedPatientDetails.SessionHolder.PracticeName });
								break;
						}
					}
					catch (Exception expException)
					{
						Logger.Instance.WriteLog(LogType.Error, expException.Message, expException, "Kiosk");
					}
				}
			}
			else
			{
				Logger.Instance.WriteLog(LogType.Error, "No Demographic details present", null, KioskId);
				Messenger.Default.Send(AppPages.Finish);
			}
		}

		private void SaveDemographicDetailsFrequency(bool isValid)
		{
            string displayName = isValid ? ActionType.ValidDemographicDetails.GetDisplayName() : ActionType.InValidDemographicDetails.GetDisplayName();
            displayName += " - " + GlobalVariables.ArrivedPatientDetails.BookedPatient.Id;
            Logger.Instance.WriteLog(LogType.Info, displayName, null, KioskId);
			var demographicRepository = DiResolver.CurrentInstance.Reslove<IDemographicRepository>();
			demographicRepository.SaveDemorgraphicFrequency(isValid);
		}

		private static string GetTelephone(Patient bookedPatient, bool maskValue)
		{
			return MaskMobileNumber(bookedPatient.HomeTelephone, maskValue, 4);
		}

		private static string GetMobile(Patient bookedPatient, bool maskValue)
		{
			return MaskMobileNumber(bookedPatient.Mobile, maskValue, 4);
		}

		private static string MaskMobileNumber(string value, bool isMask, int length)
		{
			if (string.IsNullOrEmpty(value))
				return GlobalVariables.SelectedLanguageIdText[LanguageText.NoInfoMessage];

			return isMask
				? (string.IsNullOrEmpty(value) || value.Length < length ? value : value.Substring(value.Length - length, length).PadLeft(value.Length, '*'))
				: value;
		}

		private static string GetEmail(Patient bookedPatient, bool maskValue)
		{
			var email = bookedPatient.Email;
			if (string.IsNullOrEmpty(email))
				return GlobalVariables.SelectedLanguageIdText[LanguageText.NoInfoMessage];

			return !maskValue || string.IsNullOrEmpty(email) ? email : System.Text.RegularExpressions.Regex.Replace(email, @"(?<=[\w]{1})[\w-\._\+%]*(?=[\w]{1}@)", m => new string('*', m.Length));
		}

		private static string GetPostCode(Patient bookedPatient, bool maskValue)
		{
			return MaskMobileNumber(bookedPatient.PostCode, maskValue, 3);
		}

		private static string GetName()
		{
			Patient bookedPatient = GlobalVariables.ArrivedPatientDetails.BookedPatient;
			string title = string.IsNullOrEmpty(bookedPatient.Title) ? string.Empty : bookedPatient.Title + " ";
			return title + bookedPatient.FirstName + " " + bookedPatient.FamilyName;
		}

	}
}