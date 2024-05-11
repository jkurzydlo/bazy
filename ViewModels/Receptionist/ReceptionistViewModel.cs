﻿using bazy1.Models;
using bazy1.Models.Repositories;
using bazy1.Repositories;
using bazy1.Utils;
using bazy1.ViewModels.Admin.Pages;
using bazy1.ViewModels.Doctor.Pages;
using bazy1.Views.Doctor.Pages;
using bazy1.Views.Receptionist.Pages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Collections.Generic;
using bazy1.Views.Receptionist.Pages;
using System.Threading;
using bazy1.ViewModels.Receptionist.Pages;
using bazy1.ViewModels;

namespace bazy1.ViewModels.Receptionist {

	public class ReceptionistViewModel : ViewModelBase {

		private ViewModelBase _currentViewModel;
		private string _caption;
		private bool _firstLogin = true;
		private string errorMessage;
		private Pages.FirstLoginViewModel _firstLoginViewModel;
		private string _tag;
		public ICommand ShowDashboardLoggedInCommand { get; }

		public ICommand ShowPatientRegistrationCommand { get; }
		public ICommand ShowAppointmentManagementCommand { get; }

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

		public ICommand ShowPatientListViewCommand { get; }

		private void ExecuteShowPatientListViewCommand(object obj) {

			if (!_currentUser.FirstLogin || !_firstLogin) //Jeśli użytkownik nie loguje się pierwszy raz -> zmienił hasło -> daj dostęp do przycisków
			{
				Console.WriteLine("nazwisko:" + CurrentUser.Surname);
				//Ustawiamy adres osadzonej przeglądarki wyświetlającej recepty, tak żeby nie był nullem
				if (CurrentViewModel is PrescriptionsViewModel viewModel)
				{

				}
				//Ustawiamy viewmodel dla widoku listy użytkowników
				//CurrentViewModel = new PatientListViewModel(_currentUser, this);
				Caption2 = "Lista pacjentów";
				Console.WriteLine("dasdas");
			}

		}

		private void ViewModel_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e) {
			throw new NotImplementedException();
		}

		public ICommand ShowReferralViewCommand { get; }
		private void ExecuteShowReferralViewCommand(object obj) {
			if (!_currentUser.FirstLogin || !_firstLogin)
			{
				//CurrentViewModel = new ReferralViewViewModel(DbContext.Referrals.Include("Doctor").Include("Patient").Where(r => r.DoctorUserId == _currentUser.Id).ToList(), DbContext.Doctors.Where(doc => doc.UserId == _currentUser.Id).First());
			}
		}

		public ICommand ShowScheduleViewCommand { get; }
		public ICommand AddAppointmentCommand { get; }

		private void ExecuteShowScheduleViewCommand(object obj) {

			if (!_currentUser.FirstLogin || !_firstLogin)
			{
				//Ustawiamy viewmodel dla widoku listy użytkowników
				CurrentViewModel = new ScheduleViewModel();
				Caption2 = "Terminarz";
			}
		}

		public ICommand ShowPrescriptionViewCommand { get; }
		private void ExecuteShowPrescriptionViewCommand(object obj) {
			if (!_currentUser.FirstLogin || !_firstLogin)
			{
				foreach (var item in DbContext.Prescriptions)
				{
					Console.WriteLine(item.DoctorId);
				}

				var sz = DbContext.Prescriptions.Where(
				pr => pr.DoctorUserId == CurrentUser.Id);

				var ab = DbContext.Prescriptions.Include("Patient").
					Where(pr => pr.DoctorUserId == CurrentUser.Id).ToList();

				//var pat = DbContext.Patients.Where(pat => pat.Prescriptions.Contains())
				//Console.WriteLine("roz: "+ab.First().Patient.Id);
				//Ustawiamy viewmodel dla widoku listy recept
				CurrentViewModel = new PrescriptionsViewModel(ab, DbContext.Doctors.Where(doc => doc.UserId == CurrentUser.Id).First());
				Caption2 = "Lista recept";
			}
		}

			private void ExecuteShowPatientRegistrationCommand(object obj) {
				// Ustawiamy widok rejestracji pacjenta
				//CurrentViewModel = new AddPatientViewModel();
				//Caption = "Rejestracja pacjenta";
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

		public ReceptionistViewModel() {
			AddAppointmentCommand = new BasicCommand((object obj) => { CurrentViewModel = new AddAppointmentViewModel(this); });
			ShowReferralViewCommand = new BasicCommand(ExecuteShowReferralViewCommand);
			ShowPrescriptionViewCommand = new BasicCommand(ExecuteShowPrescriptionViewCommand);

			_userRepository = new UserRepository();
			CurrentUser = new User();
			loadCurrentUser();
			ExecuteShowDashboardViewCommand(null);
			ShowDashboardViewCommand = new BasicCommand(ExecuteShowDashboardViewCommand);
			ShowPatientListViewCommand = new BasicCommand(ExecuteShowPatientListViewCommand);
			ShowScheduleViewCommand = new BasicCommand(ExecuteShowScheduleViewCommand);

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

				//Patients = _patientRepository.GetPatients();
			}
		}

		public ViewModelBase CurrentViewModel {
			get => _currentViewModel;
			set {
				_currentViewModel = value;
				OnPropertyChanged(nameof(CurrentViewModel));
				Console.WriteLine("model: " + CurrentViewModel.ToString());
			}
		}

		public string Caption2 {
			get => _caption;

			set {
				_caption = value;
				OnPropertyChanged(nameof(Caption2));
			}
		}

		public string Tag {
			get => _tag; set {
				_tag = value;
				OnPropertyChanged(nameof(Tag));
			}
		}

		public string ErrorMessage {
			get => errorMessage; set {
				errorMessage = value;
				OnPropertyChanged(nameof(ErrorMessage));
			}
		}
		public List<Medicine> Medicines { get; set; } = [];
		//public Dictionary<Medicine, MedicinePart> MedicineDataGrid { get; set; } = [];

		public Pages.FirstLoginViewModel FirstLoginViewModel {
			get => _firstLoginViewModel; set {
				_firstLoginViewModel = value;
				OnPropertyChanged(nameof(FirstLoginViewModel));
			}
		}
	}
}


