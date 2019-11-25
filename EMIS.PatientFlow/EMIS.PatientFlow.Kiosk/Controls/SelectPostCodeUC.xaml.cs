using System.Windows.Controls;

namespace EMIS.PatientFlow.Kiosk.Controls
{
    public partial class SelectPostCodeUC : UserControl
    {
		public SelectPostCodeUC()
        {
            InitializeComponent();
        }

		private void UIElement_OnPreviewMouseRightButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			e.Handled = true;
		}
	}
}
