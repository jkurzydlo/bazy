using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace bazy1.Views
{
    /// <summary>
    /// Logika interakcji dla klasy UserControl1.xaml
    /// </summary>
    public partial class startPage : UserControl
    {
        public startPage()
        {
			InitializeComponent();
        }
		private void BtnMinimizeClick(object sender, RoutedEventArgs e)
		{
			Application.Current.MainWindow.WindowState = WindowState.Minimized;
		}

		private void Window_MouseDown(object sender, MouseButtonEventArgs e)
		{
			//if (e.LeftButton.Equals(MouseButtonState.Pressed))
		}

		private void BtnLoginClick(object sender, RoutedEventArgs e)
		{

		}


		private void BtnCloseClick(object sender, RoutedEventArgs e)
		{
			Application.Current.Shutdown();
		}

		private void BtnDoctorClick(object sender, RoutedEventArgs e)
		{
			//redite
		}
	}
}
