using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using bazy1.Models.Part;
using bazy1.Models;
using bazy1.ViewModels.Admin;
using bazy1.ViewModels.Admin.Pages;
using bazy1.Views.Admin.Pages;

namespace bazy1.ViewModels.Receptionist
{
    public class ReceptionistViewModel : ViewModelBase
    {
        private ViewModelBase _currentViewModel;
        private string _caption;
        private DatabaseService _databaseService;
        private User _currentUser;
        private List<UserPart> _users;

        public List<UserPart> Users
        {
            get => _users;
            set
            {
                _users = value;
                OnPropertyChanged(nameof(Users));
            }
        }

        public ICommand ShowAddUserViewCommand { get; }
        public ICommand ShowUserListViewCommand { get; }

        public ReceptionistViewModel()
        {
            _databaseService = new DatabaseService(new PrzychodniaContext());

            // Pobierz listę użytkowników (to tylko przykład)
            Users = DatabaseService.getDbContext().Users.Select(u => new UserPart()
            {
                Name = u.Name,
                Surname = u.Surname,
                Type = u.Type
            }).ToList();

            // Komendy dla wszystkich widoków w oknie
            ShowAddUserViewCommand = new BasicCommand(ExecuteShowAddUserViewCommand);
            ShowUserListViewCommand = new BasicCommand(ExecuteShowUserListViewCommand);
            ExecuteShowUserListViewCommand(null); // Domyślnie wyświetl widok listy użytkowników
        }

        private void ExecuteShowAddUserViewCommand(object obj)
        {
            // Ustaw viewmodel dla widoku dodawania użytkownika
            CurrentViewModel = new AddUserViewModel(_currentUser, this);
            Caption = "Dodaj użytkownika";
        }

        private void ExecuteShowUserListViewCommand(object obj)
        {
            // Ustaw viewmodel dla widoku listy użytkowników
            CurrentViewModel = new ListUserViewModel();
            Caption = "Lista użytkowników";
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

