using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using bazy1.ViewModels.Admin.Pages;

namespace bazy1.ViewModels.Admin
{
    internal class AdminViewModel : ViewModelBase
    {
        private ViewModelBase _currentViewModel;
        private string _caption;

        //komendy dla wszystkich widoków w oknie
        public ICommand ShowAddUserViewCommand { get; }

        private void ExecuteShowAddUserViewCommand(object obj)
        {
            //Ustawiamy viewmodel dla widoku dodawania użytkownika
            CurrentViewModel = new AddUserViewModel();
            Caption2 = "Dodaj użytkownika";
        }

        public ICommand ShowUserListViewCommand { get; }

        private void ExecuteShowUserListViewCommand(object obj)
        {
            //Ustawiamy viewmodel dla widoku listy użytkowników
            CurrentViewModel = new ListUserViewModel();
            Caption2 = "Lista użytkowników";
            Console.WriteLine("dasdas");
        }


        public AdminViewModel()
        {
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


    }
}
