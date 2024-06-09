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

namespace bazy1.Views.Doctor
{
    /// <summary>
    /// Logika interakcji dla klasy MainView.xaml
    /// </summary>
    public partial class DoctorView : Window
    {
        public DoctorView()
        {
            InitializeComponent();
            Console.WriteLine(DataContext.ToString());
            Closing += (s, e) =>
            {
                foreach (var item in Directory.EnumerateFiles(Directory.GetCurrentDirectory()).ToList().Where(f=> f.Contains("pdf")))
                {
                    File.Delete(item);
                }
            };
        }
    }
}
