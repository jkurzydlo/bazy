using bazy1.Models;
using Google.Protobuf.Compiler;
using Microsoft.AspNetCore.CookiePolicy;
using Org.BouncyCastle.Crypto.Generators;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;

namespace bazy1.ViewModels.Admin.Pages {


    public class AddUserViewModel : ViewModelBase {
        private User _user;
        private List<string> userTypes = new()
        {
            "Lekarz",
            "Recepcjonista",
            "Administrator"
        };
        private AdminViewModel parentModel;
        private ObservableCollection<Specialization> _specializations; //ObservableCollection aktualizuje widok przy każdej zmianie kolekcji (usunięciu elementu, dodaniu itd.)
        private Enum _userTypes;
        private Visibility _doctorOptionsVisible = Visibility.Hidden;

        public ICommand AddUserCommand { get; set; }

        public AddUserViewModel() {
		}

		public ObservableCollection<Specialization> Specializations {
			get { Console.WriteLine("kel");  return _specializations; }
			set { _specializations = value;
                OnPropertyChanged(nameof(Specializations));
            }
		}
        
        public List<string> UserTypes {
            get => userTypes;
            set {
                userTypes = value;
                OnPropertyChanged(nameof(UserTypes));
            }
        }

        private string _chosenType = "Administrator";
		public string ChosenUserType{
			get { Console.WriteLine("kek"); return _chosenType; }
            set{
                _chosenType = value;
                if (!_chosenType.Equals("Lekarz")) DoctorOptionsVisible = Visibility.Hidden;
                else DoctorOptionsVisible = Visibility.Visible;                
                OnPropertyChanged(nameof(ChosenUserType));
            }

		}

		private Specialization _chosenSpecialization;

		public Specialization ChosenSpecialization {
			get { Console.WriteLine("kek"); return _chosenSpecialization; }
			set { _chosenSpecialization = value;
                OnPropertyChanged(nameof(ChosenSpecialization));
            }
		}

        private string _userSurname;
        private string _userName;
        public string UserName {
            get => _userName;
            set {
                _userName = value;
                OnPropertyChanged(UserName);
            }
        }

		public string UserSurname {
            get => _userSurname; 
            set{
                _userSurname = value;
                OnPropertyChanged(nameof(UserSurname));
            } 
        }

		internal AdminViewModel ParentModel { get => parentModel; set => parentModel = value; }
		public Visibility DoctorOptionsVisible { 
            get => _doctorOptionsVisible; 
            set {
                _doctorOptionsVisible = value;
                OnPropertyChanged(nameof(DoctorOptionsVisible));
            }
        }

		public AddUserViewModel(User user, AdminViewModel parentModel) {
            
            _user = user;
			ParentModel = parentModel;
            var tempUser = new User();
            var tempDoctor = new Models.Doctor();
            AddUserCommand = new BasicCommand((object obj) =>
            {

                Console.WriteLine("ds");
                tempUser.Name = UserName;
                tempUser.Surname = UserSurname;
                tempUser.Type = ChosenUserType;
                tempUser.FirstLogin = true;
                tempUser.Login = tempUser.Name.First().ToString().ToLower() + tempUser.Surname.First().ToString().ToLower() + tempUser.Id +
                new Random().Next(0, 9) + new Random().Next(0, 9) + new Random().Next(0, 9) + new Random().Next(0, 9);
                tempUser.Hash = "0";

                string tempPass = "";
                var randomGenerator = new Random();

				//Pierwsze hasło w formacie [0-9][0-9][0-9][0-9][Znak ASCII od !-~][Znak ASCII od !-~][Znak ASCII od !-~]
				for (int i = 0; i < 7; i++)
                {
                    if (i < 4) tempPass += randomGenerator.Next(0, 9);
                    else tempPass += Convert.ToChar(randomGenerator.Next(33, 126));
                }
                Console.WriteLine(tempPass);
                tempUser.Password = tempPass;

                //string passHash = BCrypt.Net.BCrypt.HashPassword(tempPass);
               // tempUser.Hash = passHash;
                 
                switch (tempUser.Type)
                {
                    case "Lekarz":
                        {
                            tempDoctor.User = tempUser;
                            tempDoctor.Name = UserName;
                            tempDoctor.Surname = UserSurname;
                            tempDoctor.Specializations.Add(ChosenSpecialization);
                            DbContext.Doctors.Add(tempDoctor);
                        }break;
                    case "Recepcjonista":
                        {
                            
                        }break;
                }

                DbContext.SaveChanges();




            });

            Console.WriteLine(DbContext.Specializations.Count()); 
            Specializations = new ObservableCollection<Specialization>(DbContext.Specializations); //wczytaj specjalizacje z bazy

		}
    }
}
