using bazy1.ViewModels.Admin.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace bazy1.Views.Admin.Pages
{
    public partial class AdminPatientListControl : System.Windows.Controls.UserControl
    {
        public AdminPatientListControl()
        {
            InitializeComponent();
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

		private void UserControl_Loaded(object sender, System.Windows.RoutedEventArgs e) {

		}
	}
}
