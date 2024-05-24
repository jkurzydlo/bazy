using bazy1.Models;
using bazy1.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace bazy1.ViewModels.Admin.Pages {

	public class FirstLoginViewModel : ViewModelBase {
		private string _password;
		private string _passwordRepeat;
		private User _currentUser = new User();
		public User CurrentUser { get;set; }
		private string _errorMessage;
		private string temp;
		private AdminViewModel parentModel;
		private UserRepository userRepository = new();

		public ICommand PasswordChangeCommand { get; }

		public FirstLoginViewModel() {
			PasswordChangeCommand = new BasicCommand(ExecutePasswordChangeCommand);
		}

		private void ExecutePasswordChangeCommand(object obj) {

			if (!string.IsNullOrEmpty(Password) && !string.IsNullOrEmpty(PasswordRepeat))
			{
				if (Password.Equals(PasswordRepeat))
				{
					if (Password.Length >= 8 && Password.Any(char.IsUpper) && Password.Any(char.IsLower) && Password.Any(char.IsDigit) && Password.Any(ch => !char.IsLetterOrDigit(ch)))
					{
						int id = userRepository.findById(_currentUser.Id).Id;
						var hash = BCrypt.Net.BCrypt.HashPassword(Password);
						userRepository.findById(_currentUser.Id);
						DbContext.Database.ExecuteSqlRaw($"update user set hash='{hash}' where id={_currentUser.Id}");
						//DbContext.Database.ExecuteSqlRaw($"update user set password='{Password}' where id={_currentUser.Id}");
						DbContext.Database.ExecuteSqlRaw($"update user set firstLogin=0 where id={_currentUser.Id}");

						//DbContext.Update(_currentUser);
						//DbContext.SaveChanges();

                        Console.WriteLine("sds:"+_currentUser.FirstLogin,_currentUser.Id, "asjd: "+DbContext.Users.Where(u => u.Id == _currentUser.Id).First().Id);
						parentModel.CurrentUser.FirstLogin = false;
						parentModel.CurrentViewModel = new AdminViewModel(false);

						
					}
					else ErrorMessage = "Hasło nie spełnia wymagań";
				}
				else ErrorMessage = "Hasła nie są identyczne";
				
			}
			else ErrorMessage = "Hasło nie może być puste";

		}
		public FirstLoginViewModel(User user, AdminViewModel viewModel) {
			_currentUser = user;
            Console.WriteLine("cj?:"+_currentUser.Id);
            parentModel = viewModel;
			PasswordChangeCommand = new BasicCommand(ExecutePasswordChangeCommand);
		}

		public string Password {
			get => _password;
			set {
				_password = value;
				OnPropertyChanged(nameof(Password));
			}
		}
		public string PasswordRepeat {
			get => _passwordRepeat;
			set {
				_passwordRepeat = value;
				OnPropertyChanged(nameof(PasswordRepeat));
			}
		}

		public string ErrorMessage {
			get => _errorMessage;
			set {
				_errorMessage = value;
				OnPropertyChanged(nameof(ErrorMessage));
			}
		}

	}
}
