using EMIS.PatientFlow.Kiosk.Helper;
/* 
 * This is a modified version of the on-screen key board taken from http://www.codeproject.com/Articles/32568/A-Touch-Screen-Keyboard-Control-in-WPF
 * The code is under Code Project Open Licence. Please see http://www.codeproject.com/info/cpol10.aspx
 * Original version was written by FoxholeWilly. 
 * 
 * 
 */
using System.Windows;
namespace EMIS.PatientFlow.Kiosk.KeyBoard
{
    public class NumericKeyboard : KioskKeyBoard
    {
        #region Initialising KeyBoard
        static NumericKeyboard()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(NumericKeyboard), new System.Windows.FrameworkPropertyMetadata(typeof(NumericKeyboard)));

            SetCommandBinding();
        }

        public static bool GetNumericKeyboard(System.Windows.DependencyObject obj)
        {
            return (bool)obj.GetValue(NumericKeyboardProperty);
        }

        public static void SetNumericKeyboard(System.Windows.DependencyObject obj, bool value)
        {
            obj.SetValue(NumericKeyboardProperty, value);
        }

        public static readonly System.Windows.DependencyProperty NumericKeyboardProperty =
            System.Windows.DependencyProperty.RegisterAttached("NumericKeyboard", typeof(bool), typeof(NumericKeyboard), new System.Windows.UIPropertyMetadata(default(bool), NumericKeyboardPropertyChanged));

        #endregion

        #region Touch screen Property Change
        static void NumericKeyboardPropertyChanged(System.Windows.DependencyObject sender, System.Windows.DependencyPropertyChangedEventArgs e)
        {
            System.Windows.FrameworkElement host = sender as System.Windows.FrameworkElement;
            if (host != null)
            {
                host.GotFocus += OnGotFocus;
                host.LostFocus += OnLostFocus;
                host.Initialized += Initialised;
                host.IsVisibleChanged += OnVisibilityChanged;
            }
        }

        private static void OnVisibilityChanged(object sender, System.Windows.DependencyPropertyChangedEventArgs e)
        {
            System.Windows.Controls.Control host = sender as System.Windows.Controls.Control;
            try
            {
                if (host != null && host.IsVisible)
                {
                    DisplayKeyboard(host);
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

        private static void Initialised(object sender, System.EventArgs e)
        {
            System.Windows.Controls.Control host = sender as System.Windows.Controls.Control;
            try
            {
                if (host != null && (Helper.GlobalVariables.IsAppLaunched && host.IsVisible))
                {
                    Helper.GlobalVariables.IsKeyboardInitialised = true;
                    DisplayKeyboard(host);
                }
            }
            catch (System.Exception ex)
            {
                Helper.Logger.Instance.WriteLog(Common.Enums.LogType.Error, ex.Message, ex, System.Configuration.ConfigurationManager.AppSettings["RegistrationKey"]);
            }
        }

        private static void DisplayKeyboard(System.Windows.Controls.Control host)
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
                InstanceObject = new NumericKeyboard();
                InstanceObject.AllowsTransparency = true;
                InstanceObject.WindowStyle = System.Windows.WindowStyle.None;
                InstanceObject.ShowInTaskbar = false;
                InstanceObject.ShowInTaskbar = false;
                InstanceObject.Topmost = true;
				InstanceObject.Show();
				Syncchild();
			}
        }

        static void OnGotFocus(object sender, System.Windows.RoutedEventArgs e)
        {
            System.Windows.Controls.Control host = sender as System.Windows.Controls.Control;
            try
            {
                DisplayKeyboard(host);
            }
            catch (System.Exception ex)
            {
                Helper.Logger.Instance.WriteLog(Common.Enums.LogType.Error, ex.Message, ex, System.Configuration.ConfigurationManager.AppSettings["RegistrationKey"]);
            }
        }

        private static void SetDimensions()
        {
            _widthTouchKeyboard = SystemParameters.PrimaryScreenWidth;
			string message = System.String.Format("{0}: {1}, {2}: {3}, {4}: {5}", "Screen Width Virtual", SystemParameters.VirtualScreenWidth, "Primary", _widthTouchKeyboard, "MaximumWindowTrackWidth", SystemParameters.MaximumWindowTrackWidth);
			Helper.Logger.Instance.WriteLog(Common.Enums.LogType.Info, message, null, System.Configuration.ConfigurationManager.AppSettings["RegistrationKey"]);
        }

        static void OnLostFocus(object sender, System.Windows.RoutedEventArgs e)
        {
            try
            {
                var host = sender as System.Windows.Controls.Control;
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
            }
        }

        public static void Syncchild()
        {
            try
            {
                if (_CurrentControl != null && InstanceObject != null)
                {
                  
					Point actualpoint = new Point(0, SystemParameters.VirtualScreenHeight - _heightTouchKeyboard);

					if (_widthTouchKeyboard + actualpoint.X > SystemParameters.VirtualScreenWidth)
                    {
                        double difference = _widthTouchKeyboard + actualpoint.X
                                            - SystemParameters.VirtualScreenWidth;
                        InstanceObject.Left = actualpoint.X - difference;
                    }
                    else if (actualpoint.X <= 1)
                    {
                        InstanceObject.Left = 0;
                    }
                    else
                        InstanceObject.Left = actualpoint.X;
                    InstanceObject.Top = actualpoint.Y;
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
