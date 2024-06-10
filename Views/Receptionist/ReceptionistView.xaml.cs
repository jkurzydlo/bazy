using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace bazy1.Views.Receptionist
{
    /// <summary>
    /// Logika interakcji dla klasy ReceptionistView.xaml
    /// </summary>
    public partial class ReceptionistView : Window
    {
        public ReceptionistView()
        {
            InitializeComponent();
			Closing += (s, e) =>
			{
				foreach (var item in Directory.EnumerateFiles(Directory.GetCurrentDirectory()).ToList().Where(f => f.Contains("pdf")))
				{
					File.Delete(item);
				}
			};
		}
    }
}
