using bazy1.Models;
using bazy1.Repositories;
using bazy1.ViewModels.Receptionist.Pages;
using System;
using System.Windows.Input;

namespace bazy1.ViewModels.Receptionist
{
    public class ReceptionistViewModel : ViewModelBase
    {
        private ViewModelBase _currentViewModel;
        private string _caption;
        private User _currentUser;
        private IUserRepository _userRepository;

        public ICommand ShowPatientRegistrationCommand { get; }
        public ICommand ShowAppointmentManagementCommand { get; }

        public ReceptionistViewModel()
        {
            _userRepository = new UserRepository();
            CurrentUser = new User();
            LoadCurrentUser();
            ShowPatientRegistrationCommand = new BasicCommand(ExecuteShowPatientRegistrationCommand);
            ShowAppointmentManagementCommand = new BasicCommand(ExecuteShowAppointmentManagementCommand);
            ExecuteShowPatientRegistrationCommand(null); // Wyświetlanie domyślnego widoku po uruchomieniu aplikacji
        }

        public User CurrentUser
        {
            get => _currentUser;
            set
            {
                _currentUser = value;
                OnPropertyChanged(nameof(CurrentUser));
            }
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

        private void ExecuteShowPatientRegistrationCommand(object obj)
        {
            // Ustawiamy widok rejestracji pacjenta
            CurrentViewModel = new PatientRegistrationViewModel();
            Caption = "Rejestracja pacjenta";
        }

        private void ExecuteShowAppointmentManagementCommand(object obj)
        {
            // Ustawiamy widok zarządzania wizytami
            CurrentViewModel = new AppointmentManagementViewModel();
            Caption = "Zarządzanie wizytami";
        }

        private void LoadCurrentUser()
        {
            // Tutaj wczytujemy dane użytkownika
        }
    }
}





