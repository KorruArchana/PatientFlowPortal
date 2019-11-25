using System;
using System.Windows.Threading;
using EMIS.PatientFlow.Kiosk.Enum;
using EMIS.PatientFlow.Kiosk.Helper;
using EMIS.PatientFlow.Kiosk.View;
using GalaSoft.MvvmLight.Messaging;
using System.Windows.Controls;

namespace EMIS.PatientFlow.Kiosk.Controls
{
	public partial class ExceptionDivert : UserControl
	{
		private readonly DispatcherTimer _timer;

		public ExceptionDivert()
		{
			InitializeComponent();

			if (GlobalVariables.IsKioskDataError)
			{
				BtnClearScreen.IsEnabled = false;
				_timer = new DispatcherTimer {Interval = TimeSpan.FromMilliseconds(8000)};
				_timer.Tick += timer_Tick;
				_timer.Start();
			}
		}

		private void timer_Tick(object sender, EventArgs e)
		{
			BtnClearScreen.IsEnabled = true;
			_timer.Stop();
		}
	}
}
