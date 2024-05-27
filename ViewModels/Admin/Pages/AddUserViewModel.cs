using bazy1.Models;
using bazy1.Utils;
using Google.Protobuf.Compiler;
using Microsoft.AspNetCore.CookiePolicy;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Crypto.Generators;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.IO;
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
        private string _pdfPath;

        public string PdfPath {
			get => _pdfPath;
			set {
				_pdfPath = value;
				OnPropertyChanged(nameof(PdfPath));
			}
		}

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
                string login = generator.generateLogin(new User() { Name = UserName, Surname = UserSurname });
                string password = generator.generatePassword();
                string passHash = BCrypt.Net.BCrypt.HashPassword(password);

                // Wykorzystanie procedury składowanej do dodania użytkownika
                try
                {
                    using (var connection = new MySqlConnection("Server=localhost;Database=przychodnia9;Uid=root;Pwd=;"))
                    {
                        connection.Open();
                        using (var command = new MySqlCommand("AddUser", connection))
                        {
                            command.CommandType = CommandType.StoredProcedure;
                            command.Parameters.AddWithValue("@p_type", ChosenUserType.ToLower());
                            command.Parameters.AddWithValue("@p_login", login);
                            command.Parameters.AddWithValue("@p_name", UserName);
                            command.Parameters.AddWithValue("@p_surname", UserSurname);
                            command.Parameters.AddWithValue("@p_hash", passHash);
                            command.Parameters.AddWithValue("@p_email", Email ?? (object)DBNull.Value);

                            command.ExecuteNonQuery();
                        }
                    }

                    // Wyświetlenie loginu i hasła w MessageBoxie
                    System.Windows.MessageBox.Show($"Login: {tempUser.Login}\nHasło: {password}", "Nowy użytkownik utworzony", MessageBoxButton.OK, MessageBoxImage.Information);

                    QuestPDF.Settings.License = QuestPDF.Infrastructure.LicenseType.Community;

                    Document.Create(doc =>
                    {
                        doc.Page(page =>
                        {
                            page.Size(PageSizes.A5);
                            page.Margin(5F);
                            page.Content().Table(tab =>
                            {
                                tab.ColumnsDefinition(cl => cl.RelativeColumn(1));
                                tab.Cell().Text("Dane do pierwszego logowania").AlignCenter().Bold();
                                tab.Cell().Text("Dane użytkownika").AlignCenter().Bold();
                                tab.Cell().Text("Użytkownik: " + tempUser.Name + " " + tempUser.Surname);
                                tab.Cell().Text("Login: " + tempUser.Login);
                                tab.Cell().Text("Hasło: " + password);
                                tab.Cell().Text("Wiadomość wygenerowana przez system Medikat").FontSize(8);


                            });
                        });

                    }).GeneratePdf(Directory.GetCurrentDirectory() + "\\" + "haslologin" + tempUser.Login);
                    PdfPath = Directory.GetCurrentDirectory() + "\\" + "haslologin" + tempUser.Login;

                }
                catch (Exception ex)
                {
                    System.Windows.MessageBox.Show($"Wystąpił błąd podczas dodawania użytkownika: {ex.Message}", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            });

            Console.WriteLine(DbContext.Specializations.Count());
			Specializations = new ObservableCollection<Specialization>();

			foreach (var sp in DbContext.Specializations)
            {
                if (!Specializations.ToList().Exists(s => s.Name == sp.Name)) Specializations.Add(sp);
            }
        }
    }
}