using EMIS.PatientFlow.Kiosk.ViewModel;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using System.Windows.Media.Effects;

namespace EMIS.PatientFlow.Kiosk.Controls
{
	public partial class FinishUc : UserControl
	{
		private bool alertsFlag = false;
		private bool appointmentsFlag = false;
		
        public FinishUc()
		{
			InitializeComponent();
            ShowAllButton.Visibility = Visibility.Collapsed;
		}
     
		private void AlertsListViewScrollChanged(object sender, RoutedEventArgs e)
		{
			if (!alertsFlag)
			{
				List<ScrollBar> scrollBarList = GetVisualChildCollection<ScrollBar>(AlertsListView);
				foreach (ScrollBar scrollBar in scrollBarList)
				{
					if (scrollBar.Orientation == Orientation.Vertical)
					{
						if (scrollBar.Visibility == Visibility.Visible)
						{
							ShowAllButton.Visibility = Visibility.Visible;
							scrollBar.Visibility = Visibility.Collapsed;
						}
						else
						{
							ShowAllButton.Visibility = Visibility.Collapsed;
							scrollBar.Visibility = Visibility.Collapsed;
						}

						alertsFlag = true;
					}
				}
			}
		}

		private void ArrivedAppointmentListViewScrollChanged(object sender, RoutedEventArgs e)
		{
			if (!appointmentsFlag)
			{
				List<ScrollBar> scrollBarList = GetVisualChildCollection<ScrollBar>(ArrivedAppointmentListView);
				foreach (ScrollBar scrollBar in scrollBarList)
				{
					if (scrollBar.Orientation == Orientation.Vertical && scrollBar.Visibility == Visibility.Visible)
					{
							scrollBar.Visibility = Visibility.Collapsed;
							appointmentsFlag = true;
					}
				}
			}

		}

		public static List<T> GetVisualChildCollection<T>(object parent) where T : Visual
		{
			List<T> visualCollection = new List<T>();
			GetVisualChildCollection(parent as DependencyObject, visualCollection);
			return visualCollection;
		}

		private static void GetVisualChildCollection<T>(DependencyObject parent, List<T> visualCollection) where T : Visual
		{
			int count = VisualTreeHelper.GetChildrenCount(parent);
			for (int i = 0; i < count; i++)
			{
				DependencyObject child = VisualTreeHelper.GetChild(parent, i);
				if (child is T)
				{
					visualCollection.Add(child as T);
				}
				else if (child != null)
				{
					GetVisualChildCollection(child, visualCollection);
				}
			}
		}
    }
}