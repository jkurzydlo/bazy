using bazy1.ViewModels.Receptionist.Pages;
using System.Windows;

namespace bazy1.Views.Receptionist.Pages
{
    public partial class AddPatientWindow : Window
    {
        public AddPatientWindow()
        {
			InitializeComponent(); ;
            DataContext = new AddPatientViewModel();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            // np. ((AddPatientViewModel)DataContext).AddPatientCommand.Execute(null);
        }

        private void PhoneNumberTextBox_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            // Mozesz dodac logikę weryfikacji wprowadzanych danych,
            // na przyklad, czy wprowadzane znaki są cyframi itp.
        }
    }
}
