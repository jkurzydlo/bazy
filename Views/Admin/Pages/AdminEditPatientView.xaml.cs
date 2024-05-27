using bazy1.ViewModels.Admin.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace bazy1.Views.Admin.Pages
{
    public partial class AdminEditPatientView : System.Windows.Controls.UserControl
    {
        public AdminEditPatientView()
        {
            InitializeComponent();
        }
        public AdminEditPatientView(AdminEditPatientViewModel viewModel) 
        {
            InitializeComponent();
            DataContext = viewModel;

        }
    }
}
