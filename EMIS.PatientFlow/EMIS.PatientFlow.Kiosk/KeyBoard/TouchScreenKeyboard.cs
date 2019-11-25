/* 
 * This is a modified version of the on-screen key board taken from http://www.codeproject.com/Articles/32568/A-Touch-Screen-Keyboard-Control-in-WPF
 * The code is under Code Project Open Licence. Please see http://www.codeproject.com/info/cpol10.aspx
 * Original version was written by FoxholeWilly. 
 * 
 * 
 */
using EMIS.PatientFlow.Kiosk.Helper;
namespace EMIS.PatientFlow.Kiosk.KeyBoard
{
	public class TouchScreenKeyboard : KioskKeyBoard
	{
		#region Initialising KeyBoard
		static TouchScreenKeyboard()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof(TouchScreenKeyboard), new System.Windows.FrameworkPropertyMetadata(typeof(TouchScreenKeyboard)));

			SetCommandBinding();
		}

		public static bool GetTouchScreenKeyboard(System.Windows.DependencyObject obj)
		{
			return (bool)obj.GetValue(TouchScreenKeyboardProperty);
		}

		public static void SetTouchScreenKeyboard(System.Windows.DependencyObject obj, bool value)
		{
			obj.SetValue(TouchScreenKeyboardProperty, value);
		}

		public static readonly System.Windows.DependencyProperty TouchScreenKeyboardProperty =
			System.Windows.DependencyProperty.RegisterAttached("TouchScreenKeyboard", typeof(bool), typeof(TouchScreenKeyboard), new System.Windows.UIPropertyMetadata(default(bool), TouchScreenKeyboardPropertyChanged));

		#endregion

		#region Touch screen Property Change
		static void TouchScreenKeyboardPropertyChanged(System.Windows.DependencyObject sender, System.Windows.DependencyPropertyChangedEventArgs e)
		{
			System.Windows.FrameworkElement host = sender as System.Windows.FrameworkElement;
			if (host != null)
			{
				host.GotFocus += OnGotFocus;
				host.Initialized += Initialised;
				host.LostFocus += OnLostFocus;
				host.IsVisibleChanged += OnVisibilityChanged;
			}
		}

		private static void Initialised(object sender, System.EventArgs e)
		{
			System.Windows.Controls.Control host = sender as System.Windows.Controls.Control;
			try
			{
				if (host != null && (Helper.GlobalVariables.IsAppLaunched && host.IsVisible) && !GlobalVariables.IsInFinishArrival && !GlobalVariables.IsInFinishBooking)
				{
					Helper.GlobalVariables.IsKeyboardInitialised = true;
					ShowKeyboard(host);
				}
			}
			catch (System.Exception ex)
			{
				Helper.Logger.Instance.WriteLog(Common.Enums.LogType.Error, ex.Message, ex, System.Configuration.ConfigurationManager.AppSettings["RegistrationKey"]);
			}
		}

		private static void OnVisibilityChanged(object sender, System.Windows.DependencyPropertyChangedEventArgs e)
		{
			System.Windows.Controls.Control host = sender as System.Windows.Controls.Control;
			try
			{
				if (host != null && host.IsVisible && !GlobalVariables.IsInFinishArrival && !GlobalVariables.IsInFinishBooking)
				{
					ShowKeyboard(host);
				}
				else
				{
					if (InstanceObject != null)
					{
						InstanceObject.Close();
						InstanceObject = null;
					}
				}
			}
			catch (System.Exception ex)
			{
				Helper.Logger.Instance.WriteLog(Common.Enums.LogType.Error, ex.Message, ex, System.Configuration.ConfigurationManager.AppSettings["RegistrationKey"]);
			}
		}

		static void OnGotFocus(object sender, System.Windows.RoutedEventArgs e)
		{
			System.Windows.Controls.Control host = sender as System.Windows.Controls.Control;
			try
			{
				ShowKeyboard(host);
			}
			catch (System.Exception ex)
			{
				Helper.Logger.Instance.WriteLog(Common.Enums.LogType.Error, ex.Message, ex, System.Configuration.ConfigurationManager.AppSettings["RegistrationKey"]);
			}
		}

		static void OnLostFocus(object sender, System.Windows.RoutedEventArgs e)
		{
			try
			{
				System.Windows.Controls.Control host = sender as System.Windows.Controls.Control;
				if (host != null)
				{
					host.Background = _PreviousTextBoxBackgroundBrush;
					host.BorderBrush = _PreviousTextBoxBorderBrush;
					host.BorderThickness = _PreviousTextBoxBorderThickness;
				}

				if (InstanceObject != null)
				{
					InstanceObject.Close();
					InstanceObject = null;
				}
			}
			catch (System.Exception ex)
			{
				Helper.Logger.Instance.WriteLog(Common.Enums.LogType.Error, ex.Message, ex, System.Configuration.ConfigurationManager.AppSettings["RegistrationKey"]);
				throw;
			}
		}

		private static void ShowKeyboard(System.Windows.Controls.Control host)
		{
			SetDimensions();
			_PreviousTextBoxBackgroundBrush = host.Background;
			_PreviousTextBoxBorderBrush = host.BorderBrush;
			_PreviousTextBoxBorderThickness = host.BorderThickness;

			host.Background = System.Windows.Media.Brushes.White;
			host.BorderBrush = System.Windows.Media.Brushes.Red;
			host.BorderThickness = new System.Windows.Thickness(2);

			_CurrentControl = host;

			if (InstanceObject == null)
			{
				InstanceObject = new TouchScreenKeyboard();
				InstanceObject.AllowsTransparency = true;
				InstanceObject.WindowStyle = System.Windows.WindowStyle.None;
				InstanceObject.ShowInTaskbar = false;
				InstanceObject.ShowInTaskbar = false;
				InstanceObject.Topmost = true;
				InstanceObject.Show();
				syncchild();

			}
		}

		private static void SetDimensions()
		{
			_widthTouchKeyboard = System.Windows.SystemParameters.PrimaryScreenWidth;
			string message = System.String.Format("{0}: {1}, {2}: {3}", "Screen Width Virtual", System.Windows.SystemParameters.VirtualScreenWidth, "Primary", _widthTouchKeyboard);
			Helper.Logger.Instance.WriteLog(Common.Enums.LogType.Info, message, null, System.Configuration.ConfigurationManager.AppSettings["RegistrationKey"]);
		}

		private static void syncchild()
		{
			try
			{
				if (_CurrentControl != null && InstanceObject != null)
				{
					System.Windows.Point Actualpoint = new System.Windows.Point(0, System.Windows.SystemParameters.VirtualScreenHeight - _heightTouchKeyboard);

					if (_widthTouchKeyboard + Actualpoint.X > System.Windows.SystemParameters.VirtualScreenWidth)
					{
						double difference = _widthTouchKeyboard + Actualpoint.X - System.Windows.SystemParameters.VirtualScreenWidth;
						InstanceObject.Left = Actualpoint.X - difference;
					}
					else if (Actualpoint.X <= 1)
					{
						InstanceObject.Left = 0;
					}
					else
						InstanceObject.Left = Actualpoint.X;
					InstanceObject.Top = Actualpoint.Y;
					InstanceObject.Show();
				}
			}
			catch (System.Exception ex)
			{
				Helper.Logger.Instance.WriteLog(Common.Enums.LogType.Error, ex.Message, ex, System.Configuration.ConfigurationManager.AppSettings["RegistrationKey"]);
			}
		}

		#endregion
	}
}
