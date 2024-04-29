using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using bazy1.ViewModels.Admin.Pages;
using bazy1.Views.Admin.Pages;
using CommunityToolkit.Mvvm.Input;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using MvvmDialogs;

namespace bazy1.ViewModels.Admin
{
    public class AdminViewModel : ViewModelBase
    {

<<<<<<< HEAD
        private ViewModelBase _currentViewModel;
=======
		private ViewModelBase _currentViewModel;
>>>>>>> master
        private string _caption;
        private DatabaseService _databaseService;
        private User _currentUser;
        private List<UserPart> _users;
<<<<<<< HEAD
        public ICommand RefreshCommand { get; set; }
=======
        public ICommand RefreshCommand{ get; set; }
>>>>>>> master

        public List<UserPart> Users
        {
            get => _users;
            set
            {
                _users = value;
                OnPropertyChanged(nameof(Users));
            }
        }

        //komendy dla wszystkich widoków w oknie
        public ICommand ShowAddUserViewCommand { get; }

        private void ExecuteShowAddUserViewCommand(object obj)
        {
            //Ustawiamy viewmodel dla widoku dodawania użytkownika
            CurrentViewModel = new AddUserViewModel(_currentUser, this);
            Caption2 = "Dodaj użytkownika";

        }

        public ICommand ShowUserListViewCommand { get; }

        private void ExecuteShowUserListViewCommand(object obj)
        {
            //Ustawiamy viewmodel dla widoku listy użytkowników
            CurrentViewModel = new ListUserViewModel(this);
            Caption2 = "Lista użytkowników";
            Console.WriteLine("dasdas");
        }
        

        public AdminViewModel()
        {
<<<<<<< HEAD
            // RefreshCommand = new BasicCommand((object obj) => CurrentViewModel);

            _databaseService = new DatabaseService(new PrzychodniaContext());
            Users =
                DatabaseService.getDbContext().Users.Select(u => new UserPart()
                {
                    Name = u.Name,
                    Surname = u.Surname,
                    Type = u.Type
                }).ToList();
=======
           // RefreshCommand = new BasicCommand((object obj) => CurrentViewModel);

			_databaseService = new DatabaseService(new Przychodnia9Context());
            Users = 
                DatabaseService.getDbContext().Users.Select(u => new UserPart(){
                Name = u.Name,
               Surname = u.Surname,
                Type = u.Type
            }).ToList();
>>>>>>> master

            ShowAddUserViewCommand = new BasicCommand(ExecuteShowAddUserViewCommand);
            ShowUserListViewCommand = new BasicCommand(ExecuteShowUserListViewCommand);
            ExecuteShowUserListViewCommand(null);

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
<<<<<<< HEAD
=======





>>>>>>> master
    }
}