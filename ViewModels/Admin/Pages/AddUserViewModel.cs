using bazy1.Models;
using bazy1.Utils;
using Google.Protobuf.Compiler;
using Microsoft.AspNetCore.CookiePolicy;
using Org.BouncyCastle.Crypto.Generators;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;

namespace bazy1.ViewModels.Admin.Pages
{
    public class AddUserViewModel : ViewModelBase
    {
        private User _user;
        private List<string> userTypes = new()
        {
            "Lekarz",
            "Recepcjonista",
            "Admin"
        };
        private AdminViewModel parentModel;
        private ObservableCollection<Specialization> _specializations;
        private Enum _userTypes;
        private Visibility _doctorOptionsVisible = Visibility.Hidden;

        public ICommand AddUserCommand { get; set; }

        public AddUserViewModel()
        {
        }

        public ObservableCollection<Specialization> Specializations
        {
            get => _specializations;
            set
            {
                _specializations = value;
                OnPropertyChanged(nameof(Specializations));
            }
        }

        public List<string> UserTypes
        {
            get => userTypes;
            set
            {
                userTypes = value;
                OnPropertyChanged(nameof(UserTypes));
            }
        }

        private string _chosenType = "Administrator";
        public string ChosenUserType
        {
            get => _chosenType;
            set
            {
                _chosenType = value;
                if (!_chosenType.Equals("Lekarz")) DoctorOptionsVisible = Visibility.Hidden;
                else DoctorOptionsVisible = Visibility.Visible;
                OnPropertyChanged(nameof(ChosenUserType));
            }
        }

        private Specialization _chosenSpecialization;
        public Specialization ChosenSpecialization
        {
            get => _chosenSpecialization;
            set
            {
                _chosenSpecialization = value;
                OnPropertyChanged(nameof(ChosenSpecialization));
            }
        }

        private string _userSurname;
        private string _userName;
        private string _userEmail;
        private string _userPhoneNumber;

		public string UserName
        {
            get => _userName;
            set
            {
                _userName = value;
                OnPropertyChanged(nameof(UserName));
            }
        }

        public string UserSurname
        {
            get => _userSurname;
            set
            {
                _userSurname = value;
                OnPropertyChanged(nameof(UserSurname));
            }
        }


		public string UserPhone {
			get => _userPhoneNumber;
			set {
				_userPhoneNumber = value;
				OnPropertyChanged(nameof(UserPhone));
			}
		}

		public string Email {
			get => _userEmail;
			set {
				_userEmail = value;
				OnPropertyChanged(nameof(Email));
			}
		}

		internal AdminViewModel ParentModel { get => parentModel; set => parentModel = value; }
        public Visibility DoctorOptionsVisible
        {
            get => _doctorOptionsVisible;
            set
            {
                _doctorOptionsVisible = value;
                OnPropertyChanged(nameof(DoctorOptionsVisible));
            }
        }

        public AddUserViewModel(User user, AdminViewModel parentModel)
        {

            _user = user;
            ParentModel = parentModel;
            var tempUser = new User();
            AddUserCommand = new BasicCommand((object obj) =>
            {
                // Sprawdzenie, czy imię i nazwisko nie są puste
                if (string.IsNullOrWhiteSpace(UserName) || string.IsNullOrWhiteSpace(UserSurname))
                {
                    System.Windows.MessageBox.Show("Imię i nazwisko nie mogą być puste!", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

				MailAddress mail;

				if (!string.IsNullOrEmpty(Email) && (!MailAddress.TryCreate(Email, out mail)))
                {
                    System.Windows.MessageBox.Show("Niepoprawny adres email!", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
					return;

                }

                // Automatyczna poprawa pierwszej litery imienia i nazwiska na wielką literę
                UserName = char.ToUpper(UserName[0]) + UserName.Substring(1).ToLower();
                UserSurname = char.ToUpper(UserSurname[0]) + UserSurname.Substring(1).ToLower();

                UserCredentialsGenerator generator = new();


                Console.WriteLine("ds");
                tempUser.Name = UserName;
                tempUser.Surname = UserSurname;
                tempUser.Type = ChosenUserType;
                tempUser.Email = Email;
                tempUser.FirstLogin = true;
                tempUser.Login = generator.generateLogin(new User() { Name = UserName, Surname = UserSurname });
                tempUser.Hash = "0";
                string Password = generator.generatePassword();
                string passHash = BCrypt.Net.BCrypt.HashPassword(Password);
                tempUser.Hash = passHash;

                switch (tempUser.Type)
                {
                    case "Lekarz":
                        {
                            Models.Doctor tempDoctor = new Models.Doctor();
                            tempDoctor.User = tempUser;
                            tempDoctor.Name = UserName;
                            tempDoctor.Surname = UserSurname;
                            tempDoctor.Specializations.Add(ChosenSpecialization);
                            DbContext.Doctors.Add(tempDoctor);
                        }
                        break;
                    case "Recepcjonista":
                        {
                            Models.Receptionist tempReceptionist = new Models.Receptionist();
                            tempReceptionist.User = tempUser;
                            tempReceptionist.Name = UserName;
                            tempReceptionist.Surname = UserSurname;
                            DbContext.Receptionists.Add(tempReceptionist);
                        }
                        break;
                    case "Admin":
                        {
                            Models.Administrator tempAdmin = new Models.Administrator();
                            tempAdmin.User = tempUser;
                            DbContext.Administrators.Add(tempAdmin);
                        }
                        break;
                }

                DbContext.SaveChanges();

                // Wyświetlenie loginu i hasła w MessageBoxie
                System.Windows.MessageBox.Show($"Login: {tempUser.Login}\nHasło: {Password}", "Nowy użytkownik utworzony", MessageBoxButton.OK, MessageBoxImage.Information);

            });

            Console.WriteLine(DbContext.Specializations.Count());
            Specializations = new ObservableCollection<Specialization>(DbContext.Specializations);
        }
    }
}