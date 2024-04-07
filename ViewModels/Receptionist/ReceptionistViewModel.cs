using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using bazy1.Models;
using bazy1.ViewModels.Admin;

namespace bazy1.ViewModels.Receptionist
{
    public class ReceptionistViewModel : ViewModelBase
    {
        private ViewModelBase _currentViewModel;
        private string _caption;

        public ReceptionistViewModel()
        {
            // Inicjalizacja widoku
            // Tutaj można dodać logikę inicjalizacji, jeśli to konieczne
        }

        public ViewModelBase CurrentViewModel
        {
            get => _currentViewModel;
            set
            {
                _currentViewModel = value;
                OnPropertyChanged(nameof(CurrentViewModel));
            }
        }

        public string Caption
        {
            get => _caption;
            set
            {
                _caption = value;
                OnPropertyChanged(nameof(Caption));
            }
        }
    }
}

