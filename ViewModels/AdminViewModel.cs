using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace bazy1.ViewModels {
	internal class AdminViewModel : ViewModelBase {
		private ViewModelBase _currentViewModel;
		private string caption;

		//komendy dla wszystkich widoków w oknie
		public ICommand ShowAddUserViewCommand { get; }
		
		private void ExecuteShowAddUserViewCommand(object obj) {
			//Ustawiamy viewmodel dla widoku dodawania użytkownika
			_currentViewModel = new AddUserViewModel();
			Caption = "Dodaj użytkownika";
		}
		
		public ICommand ShowUserListViewCommand { get; }

		private void ExecuteShowUserListViewCommand(object obj) {
			//Ustawiamy viewmodel dla widoku listy użytkowników
			_currentViewModel = new ListUserViewModel();
			Caption = "Lista użytkowników";
		}


		public AdminViewModel() {
			ShowAddUserViewCommand = new BasicCommand(ExecuteShowAddUserViewCommand);
			ShowUserListViewCommand = new BasicCommand(ExecuteShowUserListViewCommand);
			ExecuteShowUserListViewCommand(null);
		}



		public ViewModelBase CurrentViewModel {
			get => _currentViewModel;
			set {
				_currentViewModel = value;
				OnPropertyChanged(nameof(CurrentViewModel));
			}
		}
		public string Caption {
			get => caption;

			set {
				caption = value;
				OnPropertyChanged(nameof(CurrentViewModel));
			}
		}
		

	}
}
