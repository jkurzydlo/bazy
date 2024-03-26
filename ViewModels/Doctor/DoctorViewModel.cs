using bazy1.Models;
using bazy1.Models.Repositories;
using bazy1.Repositories;
using bazy1.ViewModels.Admin.Pages;
using bazy1.ViewModels.Doctor.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace bazy1.ViewModels.Doctor {

	class DoctorViewModel : ViewModelBase {

		private ViewModelBase _currentViewModel;
		private string _caption;

		//komendy dla wszystkich widoków w oknie
		public ICommand ShowDashboardViewCommand { get; }

		private void ExecuteShowDashboardViewCommand(object obj) {
			//Jeżeli użytkownik loguje się po raz pierwszy wyświetl widok zminy hasła, jeśli nie - ekran główny
			Console.WriteLine(_currentUser.Surname);
			if (!_currentUser.FirstLogin.GetValueOrDefault())
			{
			CurrentViewModel = new FirstLoginViewModel();			
			}
			else
			{
				CurrentViewModel = new DashboardViewModel();
			}	
			Caption2 = "Ekran główny";
		}

		public ICommand ShowPatientListViewCommand { get; }

		private void ExecuteShowPatientListViewCommand(object obj) {
			if (_currentUser.FirstLogin.GetValueOrDefault())
			{
				//Ustawiamy viewmodel dla widoku listy użytkowników
				CurrentViewModel = new PatientListViewModel();
				Caption2 = "Lista pacjentów";
				Console.WriteLine("dasdas");
			}
		}

		public ICommand ShowScheduleViewCommand { get; }

		private void ExecuteShowScheduleViewCommand(object obj) {
			if (_currentUser.FirstLogin.GetValueOrDefault())
			{
				//Ustawiamy viewmodel dla widoku listy użytkowników
				CurrentViewModel = new ScheduleViewModel();
				Caption2 = "Terminarz";
			}
		}



		private User _currentUser;
		private IUserRepository _userRepository;

		public User CurrentUser {
			get {
				return _currentUser;
			}
			set {
				_currentUser = value;
				OnPropertyChanged(nameof(CurrentUser));
			}
		}

		public DoctorViewModel() {

			
			_userRepository = new UserRepository();
			CurrentUser = new User();
			loadCurrentUser();
			ExecuteShowDashboardViewCommand(null);
			ShowDashboardViewCommand = new BasicCommand(ExecuteShowDashboardViewCommand);
			ShowPatientListViewCommand = new BasicCommand(ExecuteShowPatientListViewCommand);
			ShowScheduleViewCommand = new BasicCommand(ExecuteShowScheduleViewCommand);
		}

		//Znajdź w bazie użytkownika o danych podanych w polu logowania i ustaw jako właściwość CurrentUser
		private void loadCurrentUser() {
			User? user = _userRepository.findByUsername(Thread.CurrentPrincipal.Identity.Name);
			if (user != null)
			{
				CurrentUser.Name = user.Name;
				CurrentUser.Surname = user.Surname;
				CurrentUser.Login = user.Login;
				CurrentUser.Type = user.Type;
				CurrentUser.Password = user.Password;
			}
		}

		public ViewModelBase CurrentViewModel {
			get => _currentViewModel;
			set {
				_currentViewModel = value;
				OnPropertyChanged(nameof(CurrentViewModel));
			}
		}

		public string Caption2 {
			get => _caption;

			set {
				_caption = value;
				OnPropertyChanged(nameof(Caption2));
			}
		}

	}
}
