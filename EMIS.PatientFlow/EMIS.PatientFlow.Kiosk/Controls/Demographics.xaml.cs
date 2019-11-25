using EMIS.PatientFlow.Kiosk.ViewModel;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;


namespace EMIS.PatientFlow.Kiosk.Controls
{
    /// <summary>
    /// Interaction logic for Demographics.xaml
    /// </summary>
    public partial class Demographics : UserControl
    {
        public Demographics()
        {
            InitializeComponent();
        } 
        private void tbkPopupValueText_GotFocus(object sender, RoutedEventArgs e)
        {
           
            System.Diagnostics.Process.Start("osk");
        }

        private void tbkPopupValueText_LostFocus(object sender, RoutedEventArgs e)
        {

        }
    }
}
