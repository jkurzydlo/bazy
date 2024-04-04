using bazy1.Models.Repositories;
using bazy1.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Markup;

namespace bazy1.ViewModels {
    class LoginViewModel : ViewModelBase {


        private string _username;
        private string _password;
        private string _errorMessage;
        private string _loginMessage;
        private string _passwordMessage;

        public ICommand LoginCommand { get; }
        private IUserRepository userRepository;

        public event EventHandler LoginCompleted;

        //Jeśli nie udało się zalogować - okno cały czas widoczne
        private bool isVisible = true;

        public LoginViewModel() {
            userRepository = new UserRepository();

            //ustawiamy komendę dla viewmodelu
            LoginCommand = new BasicCommand(ExecuteLoginCommand, CanExecuteLoginCommand);
        }
        private bool CanExecuteLoginCommand(object obj) {
            return !(string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(Password));
        }

        private void ExecuteLoginCommand(object obj) {
            var isValid = userRepository.authenticate(new System.Net.NetworkCredential(Username, Password));
            if(isValid)
            {
                Thread.CurrentPrincipal = new GenericPrincipal(new GenericIdentity(Username),null);
                LoginCompleted(this, new UserEventArgs(userRepository.findByUsername(Username).Type,userRepository.findByUsername(Username).FirstLogin));// Użytkownik zalogowany, wywołaj komendę z LoginCompleted -> ukryj okno
            }
            else
            {
                ErrorMessage = "Niepoprawne dane logowania";
            }
        }

        //Przy zmianie którejś ze składowych wywołać OnPropertyChanged
        public string Username {
            get => _username;
            set {
                _username = value;
                OnPropertyChanged(nameof(Username));
            }
        }

        public string Password {
            get => _password;
            set {
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }
        public string ErrorMessage {
            get => _errorMessage;
            set {
                _errorMessage = value;
                OnPropertyChanged(nameof(ErrorMessage));
            }
        }
        public bool IsVisible {
            get => isVisible;
            set {
                isVisible = value;
            }
        }

		public string LoginMessage {
            get => _loginMessage;
            set {
                _loginMessage = value;
                OnPropertyChanged(nameof(LoginMessage));
            }
        }
		public string PasswordMessage {
            get => _passwordMessage;
            set {
                _passwordMessage = value;
                OnPropertyChanged(nameof(PasswordMessage));
            }
        }
	}

}
