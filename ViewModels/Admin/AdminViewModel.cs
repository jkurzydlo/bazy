using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Navigation;
using bazy1.Models;
using bazy1.Models.Part;
using bazy1.Repositories;
using bazy1.ViewModels.Admin.Pages;
using bazy1.Views.Admin.Pages;
using CommunityToolkit.Mvvm.Input;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using MvvmDialogs;

namespace bazy1.ViewModels.Admin
{
    public class AdminViewModel : ViewModelBase
    {
        //public DoctorViewModel DoctorViewModel { get; set; }

        public ICommand ShowUpdateScheduleCommand { get; set; }
        private ViewModelBase _currentViewModel;
        private AdminViewModel _adminViewModel;
        private string _caption;
        private DatabaseService _databaseService;
        private User _currentUser = new();
        private UserRepository userRepository = new();
        private bool firstLogin = true;
        public User CurrentUser {
            get => _currentUser;
            set {
                _currentUser = value;
                OnPropertyChanged(nameof(CurrentUser));
            }
        }
        private List<UserPart> _users;
        public ICommand RefreshCommand{ get; set; }
        private Visibility _leftPaneVisible = Visibility.Visible;

        public Visibility LeftPaneVisible {
            get => _leftPaneVisible;
            set {
                _leftPaneVisible = value;
                OnPropertyChanged(nameof(LeftPaneVisible));
            }
        }


        public List<UserPart> Users {
            get => _users;
            set {
                _users = value;
                OnPropertyChanged(nameof(Users));
            }
        }

        //komendy dla wszystkich widoków w oknie
        public ICommand ShowAddUserViewCommand { get; }

        private void ExecuteShowAddUserViewCommand(object obj)
        {
            //Ustawiamy viewmodel dla widoku dodawania użytkownika
            if (!CurrentUser.FirstLogin || !firstLogin)
            {
                CurrentViewModel = new AddUserViewModel(_currentUser, this);
                Caption2 = "Dodaj użytkownika";
            }

        }

        public ICommand ShowUserListViewCommand { get; }
        public ICommand ShowAddWorkhoursCommand { get; set; }
        public ICommand ShowPatientListViewCommand { get; }
        public ICommand ShowAdminPatientListViewCommand { get; }
        private void ExecuteShowAdminPatientListViewCommand(object obj)
        {
            if (!_currentUser.FirstLogin || !firstLogin)
            {
                CurrentViewModel = new AdminPatientListViewModel(this);
            }
            Caption2 = "Lista pacjentów";
        }


        private void ExecuteShowUserListViewCommand(object obj)
        {
            //Ustawiamy viewmodel dla widoku listy użytkowników
            if (_currentUser.FirstLogin && firstLogin) { CurrentViewModel = new Pages.FirstLoginViewModel(CurrentUser, this); }
            else CurrentViewModel = new ListUserViewModel(this);
            Caption2 = "Lista użytkowników";
            Console.WriteLine("dasdas");
        }
		private void loadCurrentUser() {
			User? user = userRepository.findByUsername(Thread.CurrentPrincipal.Identity.Name);
			if (user != null)
			{
				CurrentUser.Id = user.Id;
				CurrentUser.Name = user.Name;
				CurrentUser.Surname = user.Surname;
				CurrentUser.Login = user.Login;
				CurrentUser.Type = user.Type;
				//CurrentUser.Password = user.Password;
				CurrentUser.FirstLogin = user.FirstLogin;
			}
		}

		public AdminViewModel()
        {
            // RefreshCommand = new BasicCommand((object obj) => CurrentViewModel);
            loadCurrentUser();
            Console.WriteLine("ab: "+CurrentUser.Name+CurrentUser.Id+ CurrentUser.FirstLogin);
            _databaseService = new DatabaseService(new Przychodnia9Context());
            Users = 
                DatabaseService.getDbContext().Users.Select(u => new UserPart(){
                Name = u.Name,
               Surname = u.Surname,
                Type = u.Type
            }).ToList();

            ShowAddUserViewCommand = new BasicCommand(ExecuteShowAddUserViewCommand);
            ShowUserListViewCommand = new BasicCommand(ExecuteShowUserListViewCommand);
            ShowAddWorkhoursCommand = new BasicCommand((object obj) => { if(!CurrentUser.FirstLogin || !firstLogin)CurrentViewModel = new WorkhoursViewModel(); });

            ShowUpdateScheduleCommand = new BasicCommand((object obj) => {if(!CurrentUser.FirstLogin || !firstLogin)  CurrentViewModel = new  UpdateScheduleViewModel(this,""); });
            ExecuteShowUserListViewCommand(null);
            // Inicjalizacja DoctorViewModel w AdminViewModel
            //DoctorViewModel = new DoctorViewModel();
            ShowAdminPatientListViewCommand = new BasicCommand(ExecuteShowAdminPatientListViewCommand);
        }


		public AdminViewModel(bool firstLogin) {


			this.firstLogin = firstLogin;
			// RefreshCommand = new BasicCommand((object obj) => CurrentViewModel);
			loadCurrentUser();
			Console.WriteLine("abdzia: " + CurrentUser.Name + CurrentUser.Id+ CurrentUser.FirstLogin);
			_databaseService = new DatabaseService(new Przychodnia9Context());
			Users =
				DatabaseService.getDbContext().Users.Select(u => new UserPart()
				{
					Name = u.Name,
					Surname = u.Surname,
					Type = u.Type
				}).ToList();

			ShowAddUserViewCommand = new BasicCommand(ExecuteShowAddUserViewCommand);
			ShowUserListViewCommand = new BasicCommand(ExecuteShowUserListViewCommand);
            ExecuteShowUserListViewCommand(false);
			ShowAddWorkhoursCommand = new BasicCommand((object obj) => { if (!CurrentUser.FirstLogin || !firstLogin) CurrentViewModel = new WorkhoursViewModel(); });

			ShowUpdateScheduleCommand = new BasicCommand((object obj) => { if (!CurrentUser.FirstLogin || !firstLogin) CurrentViewModel = new UpdateScheduleViewModel(this, ""); });

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

        public string Caption2
        {
            get => _caption;

            set
            {
                _caption = value;
                OnPropertyChanged(nameof(CurrentViewModel));
            }
        }

    }
}
