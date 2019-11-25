using System.Windows.Controls;
using System.Windows.Input;

namespace EMIS.PatientFlow.Kiosk.Controls
{
    public partial class DemographicDetailsUc : UserControl
    {
		public DemographicDetailsUc()
        {
            InitializeComponent();
        }

	    private void UIElement_OnPreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
	    {
		    e.Handled = true;
	    }
    }
}