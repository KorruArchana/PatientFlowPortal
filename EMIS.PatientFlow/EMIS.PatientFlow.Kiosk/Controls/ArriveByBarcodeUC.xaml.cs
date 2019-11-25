using System.Windows.Controls;
using System.Text;
using System.Windows;
using System.Windows.Input;
using EMIS.PatientFlow.Kiosk.ViewModel;
using GalaSoft.MvvmLight.Messaging;

namespace EMIS.PatientFlow.Kiosk.Controls
{
	public partial class ArriveByBarcodeUC : UserControl
	{
		public ArriveByBarcodeUC()
		{
			InitializeComponent();
			this.Focus();
		}

		private void UserControl_Loaded(object sender, System.Windows.RoutedEventArgs e)
		{
			var window = Window.GetWindow(this);
			if (window != null)
				window.PreviewKeyDown += labelBarCode_PreviewKeyDown;
			else
			{
				Messenger.Default.Send(new BarCodeMessage
				{
					BarCode = "Error",
					Status = 0
				});
			}
		}


		private StringBuilder _inputvalue = new StringBuilder();
		private bool _isShiftKeyPressedBefore;

		private void labelBarCode_PreviewKeyDown(object sender, KeyEventArgs e)
		{
			var keyValue = IsNumeric(e.Key.ToString());
			if (e.Key == Key.LeftShift || e.Key == Key.RightShift)
			{
				_isShiftKeyPressedBefore = true;
				keyValue = string.Empty;
			}
			else
			{
				if (_isShiftKeyPressedBefore)
					keyValue = string.Empty;
				_isShiftKeyPressedBefore = false;
			}

			if (e.Key == Key.Return || e.Key == Key.Enter)
			{
				e.Handled = true;
				Messenger.Default.Send(new BarCodeMessage
				{
					BarCode = _inputvalue.ToString(),
					Status = 1
				});
				_inputvalue = new StringBuilder();
			}
			else if (!string.IsNullOrEmpty(keyValue))
				_inputvalue.Append(keyValue);
		}

		private static string IsNumeric(string value)
		{
			switch (value)
			{
				case "D0":
					return "0";
				case "D1":
					return "1";
				case "D2":
					return "2";
				case "D3":
					return "3";
				case "D4":
					return "4";
				case "D5":
					return "5";
				case "D6":
					return "6";
				case "D7":
					return "7";
				case "D8":
					return "8";
				case "D9":
					return "9";
				default:
					return string.Empty;
			}

		}
	}
}
