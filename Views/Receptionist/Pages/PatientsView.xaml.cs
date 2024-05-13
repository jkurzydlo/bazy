using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using bazy1.ViewModels.Receptionist;
using bazy1.Repositories;

namespace bazy1.Views.Receptionist.Pages
{
    public partial class PatientsView : UserControl
    {
        public PatientsView()
        {
            InitializeComponent();
            DataContext = new ReceptionistViewModel();
        }
        // Metoda do aktualizacji danych pacjentów
        //
        //
        public ReceptionistViewModel DataContext { get; }

        private void ContextMenuButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button != null)
            {
                var dataGridRow = FindVisualParent<DataGridRow>(button);
                if (dataGridRow != null)
                {
                    dataGridRow.IsSelected = true;
                    var contextMenu = dataGridRow.ContextMenu;
                    if (contextMenu != null)
                    {
                        contextMenu.PlacementTarget = button;
                        contextMenu.IsOpen = true;
                    }
                }
            }
        }

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
        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}





