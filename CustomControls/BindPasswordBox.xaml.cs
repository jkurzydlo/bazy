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

namespace bazy1.CustomControls
{
    /// <summary>
    /// Logika interakcji dla klasy BindBindPasswordBox.xaml
    /// </summary>
    public partial class BindPasswordBox : UserControl
    {
        public static readonly DependencyProperty PasswordProperty = DependencyProperty.Register("Password", typeof(string), typeof(BindPasswordBox));
        public String Password {
            get {
                return (string)GetValue(PasswordProperty);
            }
            set {
                SetValue(PasswordProperty, value);
            }
        }
        public BindPasswordBox()
        {
            InitializeComponent();
            txtPassword.PasswordChanged += OnPasswordChanged;
        }

		private void OnPasswordChanged(object sender, RoutedEventArgs e) {
			Password = txtPassword.Password;
		}
	}
}
