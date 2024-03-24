using bazy1.Models;
using bazy1.Models.Repositories;
using bazy1.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bazy1.ViewModels {
	
	class DoctorViewModel : ViewModelBase{
		private User _currentUser;
		private IUserRepository _userRepository;

		public User CurrentUser {
			get {
				return _currentUser;
			}
			set {
				_currentUser = value;
				OnPropertyChanged(nameof(_currentUser));
			}
		}

		public DoctorViewModel() {
			_userRepository = new UserRepository();
			CurrentUser = new User();
			loadCurrentUser();
		}

		//Znajdź w bazie użytkownika o danych podanych w polu logowania i ustaw jako właściwość CurrentUser
		private void loadCurrentUser() {
			User? user = _userRepository.findByUsername(Thread.CurrentPrincipal.Identity.Name); 
			if(user!= null) {
			CurrentUser.Name = user.Name;
			CurrentUser.Surname = user.Surname;
			CurrentUser.Login = user.Login;
			CurrentUser.Type = user.Type;
			CurrentUser.Password = user.Password;
			}
		}

	}
}
