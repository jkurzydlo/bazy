using bazy1.Models;
using Google.Protobuf.Compiler;
using Microsoft.AspNetCore.CookiePolicy;
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
        private List<string> userTypes = new()
        {
            "Lekarz",
            "Recepcjonista",
            "Administrator"
        };
        private AdminViewModel parentModel;
        private ObservableCollection<Specialization> _specializations;
        private Enum _userTypes;
        private Visibility _doctorOptionsVisible = Visibility.Hidden;

        public ICommand AddUserCommand;

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

		private string _login;
        private User user;
        private string _username;
        public string UserName {
            get => _username; set {
                _username = value;
                OnPropertyChanged(UserName);
            }
        }

		public string Login { get => _login; set{
                _login = value;
                OnPropertyChanged(nameof(Login));
                Console.WriteLine(_login);
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
            
            this.user = user;
			ParentModel = parentModel;
            AddUserCommand = new BasicCommand((object obj) =>
            {
               // DbContext.Users.Add(new User
               // {
               //     Type = 
               // });
            });

            Console.WriteLine(DbContext.Specializations.Count()); 
            Specializations = new ObservableCollection<Specialization>(DbContext.Specializations); //wczytaj specjalizacje z bazy

		}
    }
}
