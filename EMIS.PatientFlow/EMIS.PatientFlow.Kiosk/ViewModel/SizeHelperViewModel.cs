using System;
using System.Windows;
using System.Windows.Forms;
using EMIS.PatientFlow.Kiosk.Helper.ResolutionSizeHelper;
using GalaSoft.MvvmLight;

namespace EMIS.PatientFlow.Kiosk.ViewModel
{
	public class SizeHelperViewModel : ViewModelBase
	{
		public SizeHelper ResolutionHelper { get; set; }

		public SizeHelperViewModel()
		{
			ResolutionHelper = GetResolutionBasedSizeHelper();
		}

		public SizeHelper GetResolutionBasedSizeHelper()
		{
			if (Screen.PrimaryScreen.Bounds.Height == 1024 && Screen.PrimaryScreen.Bounds.Width == 1280)
			{
				SizeHelper sSeriesKioskSize = new SSeriesKioskSize();
				return sSeriesKioskSize;
			}
			else if (Screen.PrimaryScreen.Bounds.Height == 768 && Screen.PrimaryScreen.Bounds.Width == 1024)
			{
				SizeHelper ibmKioskSize = new IBMKioskSize();
				return ibmKioskSize;
			}
			else if (Screen.PrimaryScreen.Bounds.Height == 800 && Screen.PrimaryScreen.Bounds.Width == 1280)
			{
				SizeHelper tabletKioskSize = new IBMKioskSize();
				tabletKioskSize.LanguagePopUpWidth = 850;
                tabletKioskSize.DoctorSelectionButtonWidth = 320;
				return tabletKioskSize;
			}
			else if (Screen.PrimaryScreen.Bounds.Height == 768 && Screen.PrimaryScreen.Bounds.Width == 1366)
			{
				SizeHelper tabletKioskSize = new IBMKioskSize();
				tabletKioskSize.LanguagePopUpWidth = 850;
                tabletKioskSize.DoctorSelectionButtonWidth = 340;
                return tabletKioskSize;
			}
			else if (Screen.PrimaryScreen.Bounds.Height == 1080 && Screen.PrimaryScreen.Bounds.Width == 1920)
			{
				SizeHelper elephantKioskSize = new ElephantKioskSize();
				return elephantKioskSize;
			}
			else
			{
				if (Screen.PrimaryScreen.Bounds.Height > 950)
				{
					SizeHelper sSeriesKioskSize = new SSeriesKioskSize();
					return sSeriesKioskSize;
				}
				else
				{
					SizeHelper tabletKioskSize = new IBMKioskSize();
					return tabletKioskSize;
				}
			}
		}
	}
}