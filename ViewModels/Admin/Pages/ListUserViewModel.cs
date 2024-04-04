using bazy1.Models.Part;
using bazy1.Models;
using System.Collections.ObjectModel;
using System.Xml.Linq;
using System.Windows.Input;
using System.Windows.Navigation;
using MvvmDialogs;
using System.Windows;
using Microsoft.AspNetCore.Components.Forms;

namespace bazy1.ViewModels.Admin.Pages {
	public class ListUserViewModel : ViewModelBase {

		private AdminViewModel _adminViewModel;
		private ObservableCollection<User> _users = new(DbContext.Users);
		private User _selectedUser;
		private string _name, _surname, _login;
		private Visibility _editFormVisible = Visibility.Hidden;
		public ICommand ShowModifyPanel { get; set; }

		public ICommand ModifyUserCommand { get; set; }
		public ObservableCollection<User> Users {
			get => _users;
			set {
				_users = value;
				OnPropertyChanged(nameof(Users));
			}
		}

		public User SelectedUser {
			get => _selectedUser;
			set {
				_selectedUser = value;
				OnPropertyChanged(nameof(SelectedUser));
			}
		}

		public string Name {
			get => _name;
			set{
				_name = value;
				OnPropertyChanged(nameof(Name));
			}
		}
		public string Surname {
			get => _surname;
			set {
				_surname = value;
				OnPropertyChanged(nameof(Surname));
			}
		}
		public string Login {
			get => _login;
			set {
				_login = value;
				OnPropertyChanged(nameof(Login));
			}
		}

		public AdminViewModel AdminViewModel { get => _adminViewModel; set => _adminViewModel = value; }
		public Visibility EditFormVisible {
			get => _editFormVisible; set {
				_editFormVisible = value;
				OnPropertyChanged(nameof(EditFormVisible));
			}
		}

		public ListUserViewModel(AdminViewModel adminViewModel) {
			this._adminViewModel = adminViewModel;
			ShowModifyPanel = new BasicCommand((object obj) => EditFormVisible = Visibility.Visible);
			ModifyUserCommand = new BasicCommand((object obj) =>
			{
				var selected = DbContext.Users.Where(user => SelectedUser.Id == user.Id).First();

				if(!string.IsNullOrEmpty(Name)) selected.Name = Name;
				if (!string.IsNullOrEmpty(Surname)) selected.Surname = Surname;
				if (!string.IsNullOrEmpty(Login)) selected.Login = Login;

				DbContext.SaveChanges();
				_adminViewModel.CurrentViewModel = new ListUserViewModel(adminViewModel);


			});

		}
	}
}