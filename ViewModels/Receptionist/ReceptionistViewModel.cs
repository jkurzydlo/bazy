using bazy1.Models;
using bazy1.Repositories;
using bazy1.ViewModels.Receptionist.Pages;
using bazy1.ViewModels;
using System;
using System.Windows.Input;
using System.Collections.Generic;
using bazy1.Views.Receptionist.Pages;
using System.Threading;

namespace bazy1.ViewModels.Receptionist
{
    public class ReceptionistViewModel : ViewModelBase
    {
        private ViewModelBase _currentViewModel;
        private string _caption;
        private User _currentUser;
        private IUserRepository _userRepository;
        private List<Patient> _patients;
        public PatientRepository _patientRepository;

		public ICommand ShowPatientRegistrationCommand { get; }
		public ICommand ShowAppointmentManagementCommand { get; }
		public ICommand ShowDocScheduleViewCommand { get; }
		public ICommand ShowAppointmentsCommand { get; }
		public ICommand ShowPatientAppointmentsViewCommand { get; }


		//komendy dla wszystkich widoków w oknie
		public ICommand ShowDashboardViewCommand { get; }

		private void ExecuteShowDashboardViewCommand(object obj) {
			//Jeżeli użytkownik loguje się po raz pierwszy wyświetl widok zmiany hasła, jeśli nie - ekran główny

			if (_currentUser.FirstLogin)
			{

				CurrentViewModel = new Pages.FirstLoginViewModel(_currentUser);
			}
			else
			{

				CurrentViewModel = new Pages.DashboardViewModel();
			}
			Caption2 = "Ekran główny";
		}


        public ReceptionistViewModel()
        {
            _userRepository = new UserRepository();
            _patientRepository = new PatientRepository();
            CurrentUser = new User();
            LoadCurrentUser();
            ShowPatientRegistrationCommand = new BasicCommand(ExecuteShowPatientRegistrationCommand);
            ShowAppointmentManagementCommand = new BasicCommand(ExecuteShowAppointmentManagementCommand);
            ExecuteShowPatientRegistrationCommand(null); // Wyświetlanie domyślnego widoku po uruchomieniu aplikacji
        }

			if (!_currentUser.FirstLogin || !_firstLogin) //Jeśli użytkownik nie loguje się pierwszy raz -> zmienił hasło -> daj dostęp do przycisków
			{

				//Ustawiamy viewmodel dla widoku listy użytkowników
				CurrentViewModel = new Pages.PatientListViewModel(this);
				Caption2 = "Lista pacjentów";
				Console.WriteLine("dasdas");
			}

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

        public List<Patient> Patients // Dodaj właściwość do przechowywania listy pacjentów
        {
            get => _patients;
            set
            {
                _patients = value;
                OnPropertyChanged(nameof(Patients));
            }
        }

        private void ExecuteShowPatientRegistrationCommand(object obj)
        {
            // Ustawiamy widok rejestracji pacjenta
            CurrentViewModel = new AddPatientViewModel();
            Caption = "Rejestracja pacjenta";
        }

        private void ExecuteShowAppointmentManagementCommand(object obj)
        {
            // Ustawiamy widok zarządzania wizytami
            CurrentViewModel = new AddAppointmentViewModel(CurrentViewModel);
            Caption = "Zarządzanie wizytami";
        }

		public ReceptionistViewModel() {
			//AddAppointmentCommand = new BasicCommand((object obj) => { CurrentViewModel = new AddAppointmentViewModel(this); });
			ShowReferralViewCommand = new BasicCommand(ExecuteShowReferralViewCommand);
			ShowPrescriptionViewCommand = new BasicCommand(ExecuteShowPrescriptionViewCommand);
			ShowPatientAppointmentsViewCommand = new BasicCommand((object obj) =>
			{
				//CurrentViewModel = new PatientAppointmentsViewModel(SelectedP);
			});

			_userRepository = new UserRepository();
			CurrentUser = new User();
			loadCurrentUser();
			ExecuteShowDashboardViewCommand(null);
			ShowDashboardViewCommand = new BasicCommand(ExecuteShowDashboardViewCommand);
			ShowPatientListViewCommand = new BasicCommand(ExecuteShowPatientListViewCommand);
			ShowScheduleViewCommand = new BasicCommand(ExecuteShowScheduleViewCommand);
			ShowDocScheduleViewCommand = new BasicCommand((object obj) =>
			{
				if (!_currentUser.FirstLogin || !_firstLogin) //Jeśli użytkownik nie loguje się pierwszy raz -> zmienił hasło -> daj dostęp do przycisków
				{
					CurrentViewModel = new DocScheduleViewModel();
				}
			});

			ShowDashboardLoggedInCommand = new BasicCommand((object obj) => {


				FirstLoginViewModel = (Pages.FirstLoginViewModel)CurrentViewModel;
				if (!string.IsNullOrEmpty(FirstLoginViewModel.Password) && !string.IsNullOrEmpty(FirstLoginViewModel.PasswordRepeat))
				{
					if (FirstLoginViewModel.Password.Equals(FirstLoginViewModel.PasswordRepeat))
					{
						CurrentViewModel = new Pages.DashboardViewModel();
						_firstLogin = false;
					}
				}
			});
		}

		//Znajdź w bazie użytkownika o danych podanych w polu logowania i ustaw jako właściwość CurrentUser
		private void loadCurrentUser() {
			User? user = _userRepository.findByUsername(Thread.CurrentPrincipal.Identity.Name);
			if (user != null)
			{
				CurrentUser.Id = user.Id;
				CurrentUser.Name = user.Name;
				CurrentUser.Surname = user.Surname;
				CurrentUser.Login = user.Login;
				CurrentUser.Type = user.Type;
				CurrentUser.Password = user.Password;
				CurrentUser.FirstLogin = user.FirstLogin;
				Console.Write("da: " + CurrentUser.Name + CurrentUser.Surname);

                    Patients = _patientRepository.GetPatients();
                }
            }
            catch (Exception ex)
            {
                // Obsługa wyjątku - możesz zalogować błąd lub podjąć inne działania
                Console.WriteLine("Błąd podczas ładowania bieżącego użytkownika: " + ex.Message);
            }
        }

        private BasicCommand showPatientsCommand;
        public ICommand ShowPatientsCommand => showPatientsCommand ??= new BasicCommand(ShowPatients1);

        private void ShowPatients1(object commandParameter)
        {
            CurrentViewModel = new PatientListViewModel();
            Caption = "Lista pacjentów";
        }
    }
}


