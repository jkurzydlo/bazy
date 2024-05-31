using bazy1.Models;
using bazy1.Views.Admin.Pages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace bazy1.ViewModels.Doctor.Pages {

	public class FirstLoginViewModel : ViewModelBase {
		private string _password;
		private string _passwordRepeat;
		private User _currentUser;
		private string _errorMessage;
		private string temp;
		public ViewModelBase ParentModel { get; set; }

		public ICommand PasswordChangeCommand{ get; }

		public FirstLoginViewModel() {
			PasswordChangeCommand = new BasicCommand(ExecutePasswordChangeCommand);
		}

		private void ExecutePasswordChangeCommand(object obj) {

			
			var watch = new Stopwatch();
			watch.Start();
			var db = new przychodnia9Context();
			watch.Stop();
			Console.WriteLine("czas"+ watch.ElapsedMilliseconds);
			_currentUser.Surname = "sds";
			Console.WriteLine($"id2: {_currentUser.Name}");
			Console.WriteLine("hasło: "+Password + PasswordRepeat);
			//Console.WriteLine($"Rozmiar: {db.Users.Where(e => e.Id == _currentUser.Id).Count()}");
			//Console.WriteLine(db.Users.Count());
			if (!string.IsNullOrEmpty(Password) && !string.IsNullOrEmpty(PasswordRepeat))
			{
				if (Password.Equals(PasswordRepeat))
				{
					if (Password.Length >= 8 && Password.Any(char.IsUpper) && Password.Any(char.IsLower) && Password.Any(char.IsDigit) && Password.Any(ch => !char.IsLetterOrDigit(ch)))
					{
						var hash = BCrypt.Net.BCrypt.HashPassword(Password);
						DbContext.Database.ExecuteSqlRaw($"update user set hash='{hash}' where id={_currentUser.Id}");
						//DbContext.Database.ExecuteSqlRaw($"update user set password='{Password}' where id={_currentUser.Id}");
						DbContext.Database.ExecuteSqlRaw($"update user set firstLogin=0 where id={_currentUser.Id}");

					}
					else ErrorMessage = "Hasło nie spełnia wymagań";
				}
				else ErrorMessage = "Hasła nie są identyczne";

			}
			else ErrorMessage = "Hasło nie może być puste";
			Console.WriteLine(ErrorMessage);
			
		}
		public FirstLoginViewModel(User user) {
			PasswordChangeCommand = new BasicCommand(ExecutePasswordChangeCommand);
			_currentUser = user;
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
