using bazy1.ViewModels.Receptionist;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace bazy1.Views.Receptionist
{
    public partial class ReceptionistView : Window
    {
        public ReceptionistView()
        {
            InitializeComponent();
            Console.WriteLine(DataContext.ToString());
        }
    }
}
