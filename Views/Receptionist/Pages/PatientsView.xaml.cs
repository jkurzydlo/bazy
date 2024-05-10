using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using bazy1.ViewModels.Receptionist;
using bazy1.Repositories;

namespace bazy1.Views.Receptionist.Pages
{
    public partial class PatientsView : Window
    {
        public PatientsView()
        {
            //InitializeComponent(); ;
            // Ustawienie kontekstu danych na nową instancję ReceptionistViewModel
            DataContext = new ReceptionistViewModel();
            // Zaktualizowanie danych pacjentów w widoku po utworzeniu
            UpdatePatients();
        }

        // Metoda do aktualizacji danych pacjentów
        private void UpdatePatients()
        {
            // Sprawdzenie czy DataContext jest typu ReceptionistViewModel
            if (DataContext is ReceptionistViewModel viewModel)
            {
                // Pobranie listy pacjentów z ViewModel
               // viewModel.Patients = viewModel._patientRepository.GetPatients();
            }
        }

        private void ContextMenuButton_Click(object sender, RoutedEventArgs e)
        {
            // Pobierz przycisk, który został kliknięty
            var button = sender as Button;
            if (button != null)
            {
                // Pobierz wiersz, do którego należy ten przycisk
                var dataGridRow = FindVisualParent<DataGridRow>(button);
                if (dataGridRow != null)
                {
                    // Ustaw wiersz jako zaznaczony
                    dataGridRow.IsSelected = true;

                    // Wyświetl menu kontekstowe dla tego wiersza
                    var contextMenu = dataGridRow.ContextMenu;
                    if (contextMenu != null)
                    {
                        contextMenu.PlacementTarget = button;
                        contextMenu.IsOpen = true;
                    }
                }
            }
        }

        // Metoda pomocnicza do wyszukiwania rodzica określonego typu
        private T FindVisualParent<T>(UIElement element) where T : UIElement
        {
            var parent = VisualTreeHelper.GetParent(element) as UIElement;
            while (parent != null)
            {
                if (parent is T)
                {
                    return (T)parent;
                }
                parent = VisualTreeHelper.GetParent(parent) as UIElement;
            }
            return null;
        }
    }
}




