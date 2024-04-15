using bazy1.ViewModels.Receptionist;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace bazy1.Views.Receptionist.Pages
{
    public partial class PatientsView : Window
    {
        public PatientsView()
        {
            InitializeComponent();
            DataContext = new ReceptionistViewModel();
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


