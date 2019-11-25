using System;
using System.Collections.Generic;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using EMIS.PatientFlow.Kiosk.DatabaseAccess.Repository.Interfaces;
using EMIS.PatientFlow.Kiosk.Enum;
using EMIS.PatientFlow.Kiosk.Helper;
using EMIS.PatientFlow.Kiosk.Model;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;

namespace EMIS.PatientFlow.Kiosk.ViewModel
{
	public class SiteMapViewModel : ViewModelBase
	{
		private string _hospitalNameText;
		private List<SiteMapImage> _kioskSiteMapList;

		private RelayCommand<string> _loadedCommand;
		private RelayCommand<string> _clearScreenCommand;


		public string HospitalNameText
		{
			get
			{
				return _hospitalNameText;
			}
			set
			{
				_hospitalNameText = value;
				RaisePropertyChanged("HospitalNameText");
			}
		}

		public List<SiteMapImage> KioskSiteMapList
		{
			get
			{
				return _kioskSiteMapList;
			}
			set
			{
				_kioskSiteMapList = value;
				RaisePropertyChanged("KioskSiteMapList");
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
							if (!GlobalVariables.IsDbConnected)
							{
								Messenger.Default.Send(AppPages.ExceptionDivert);
							}

						}));
			}
		}

		public RelayCommand<string> ClearScreenCommand
		{
			get
			{
				return _clearScreenCommand
					?? (_clearScreenCommand = new RelayCommand<string>(
						p =>
						{
							Messenger.Default.Send(AppPages.HomePage);
						}));
			}
		}

		public SiteMapViewModel()
		{
			InitializeControls();
		}

		private void InitializeControls()
		{

			HospitalNameText = (GlobalVariables.KioskSettings != null) ? GlobalVariables.KioskSettings.Title : "";
			HospitalNameText = HospitalNameText + " Site Map";

			if (GlobalVariables.KioskSiteMapsList != null && GlobalVariables.KioskSiteMapsList.Count > 0)
			{
				KioskSiteMapList = GlobalVariables.KioskSiteMapsList;
			}
		}
	}
}