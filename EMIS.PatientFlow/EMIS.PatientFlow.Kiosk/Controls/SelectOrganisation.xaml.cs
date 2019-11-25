using System.Windows.Controls;
using EMIS.PatientFlow.Kiosk.Helper;
using EMIS.PatientFlow.Kiosk.View;

namespace EMIS.PatientFlow.Kiosk.Controls
{
    public partial class SelectOrganisation : UserControl
    {
        public SelectOrganisation()
        {
            InitializeComponent();

			if (!GlobalVariables.ConnectionStatus && !GlobalVariables.IsDataAvailable)
			{
				var indexWindow = App.Current.MainWindow as IndexWindow;
				if (indexWindow != null)
					indexWindow.BindData();
			}
		}
	}
}
