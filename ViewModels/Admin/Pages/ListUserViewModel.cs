using bazy1.Models;
using bazy1.Models.Part;
using bazy1.Utils;
using bazy1.Views.Receptionist.Pages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Mail;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace bazy1.ViewModels.Admin.Pages {
    public class ListUserViewModel : ViewModelBase {
        private readonly AdminViewModel _adminViewModel;
        private ObservableCollection<User> _users;
        private User _selectedUser;
        private Visibility _editFormVisible = Visibility.Hidden;
        public ICollectionView _usersView;
        private string _filterText;
        private Visibility _loading = Visibility.Visible;
		private Visibility _loaded = Visibility.Hidden;
        private float _loading1, _loading2;

        public float Loading1 {
            get=>_loading1;
            set {
				_loading1 = value;
				OnPropertyChanged(nameof(Loading1));
                if (100f - _loading1 < 0.5 && 100f - Loading2 < 0.5)
                {
                    Loaded = Visibility.Visible;
                    Loading = Visibility.Hidden;
                }
			}
        }

		public float Loading2 {
			get => _loading2;
			set {
				_loading1 = value;
				OnPropertyChanged(nameof(Loading2));
				if (100f - Loading1 < 0.5 && 100f - _loading2 < 0.5)
				{
					Loaded = Visibility.Visible;
					Loading = Visibility.Hidden;
				}
			}
		}

		public Visibility Loading {
            get => _loading;
            set {
                _loading = value;
            }
        }


		public Visibility Loaded {
			get => _loaded;
			set {
				_loaded = value;
			}
		}
		public ICommand ShowModifyPanel { get; set; }
        public ICommand ModifyUserCommand { get; set; }
        public ICommand DeleteUserCommand { get; }
        public ICommand SendAccountVerificationEmail { get; set;}
        public ICommand VisitsListShowCommand { get; set; }

		public ICollectionView UsersView {
			get => _usersView;
			set => _usersView = value;
		}

		public string FilterText {
			get => _filterText;
			set {
				_filterText = value;
				UsersView.Filter = (object user) =>
				{
					var tempUser = user as User;
                    bool found = tempUser.Email != null ?
                           (tempUser.Name.ToLower().Contains(FilterText.ToLower().Trim()) ||
                           tempUser.Surname.ToLower().Contains(FilterText.ToLower().Trim()) ||
                           tempUser.Login.ToString().ToLower().Contains(FilterText.ToLower().Trim()) ||
                           (tempUser.Name +" "+tempUser.Surname).ToLower().Contains(FilterText.ToLower().Trim()) ||
                           tempUser.Email.ToLower().Contains(FilterText.ToLower().Trim())) :



                           (tempUser.Name.ToLower().Contains(FilterText.ToLower().Trim()) ||
                           tempUser.Surname.ToLower().Contains(FilterText.ToLower().Trim()) ||
                           tempUser.Login.ToString().ToLower().Contains(FilterText.ToLower().Trim()) ||
                           (tempUser.Name + " "+ tempUser.Surname).ToLower().Contains(FilterText.ToLower().Trim()));
                           
						   return found;
                };
				OnPropertyChanged(nameof(FilterText));
			}
		}


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
                if (_selectedUser != null)
                {
                    Name = _selectedUser.Name;
                    Surname = _selectedUser.Surname;
                    Login = _selectedUser.Login;
                    Email = _selectedUser.Email;
                }
            }
        }

        private string _name;
        public string Name {
            get => _name;
            set {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        private string _surname;
        public string Surname {
            get => _surname;
            set {
                _surname = value;
                OnPropertyChanged(nameof(Surname));
            }
        }

		private string _email;
		public string Email {
			get => _email;
			set {
				_email = value;
				OnPropertyChanged(nameof(Email));
			}
		}

		private string _login;
        public string Login {
            get => _login;
            set {
                _login = value;
                OnPropertyChanged(nameof(Login));
            }
        }

        public Visibility EditFormVisible {
            get => _editFormVisible;
            set {
                _editFormVisible = value;
                OnPropertyChanged(nameof(EditFormVisible));
            }
        }


		private async void downloadXML() {
				var url = "https://rejestry.ezdrowie.gov.pl/api/rpl/medicinal-products/public-pl-report/4.0.0/overall.xml";
				var filePath = "rpl.xml";
				var httpClient = new HttpClientDownloadWithProgress(url, filePath);

				httpClient.ProgressChanged += HttpClient_ProgressChanged;
				await httpClient.StartDownload();

				void HttpClient_ProgressChanged(long? totalFileSize, long totalBytesDownloaded, double? progressPercentage) {
                Loading2 = (float)progressPercentage;
				}
			
		}


		private async void downloadCSV() {

				var httpClient = new HttpClientDownloadWithProgress("https://rpwdl.ezdrowie.gov.pl/Registry/Pobieranie?typ=Csv&rodzajRejestru=Rpm", "rpm.zip");

				httpClient.ProgressChanged += HttpClient_ProgressChanged;
				await httpClient.StartDownload();

				void HttpClient_ProgressChanged(long? totalFileSize, long totalBytesDownloaded, double? progressPercentage) {
                Loading1 = (float)progressPercentage;
				}		
		}

		public ListUserViewModel(AdminViewModel adminViewModel) {

            _users = new ObservableCollection<User>(DbContext.Users);

            UsersView= CollectionViewSource.GetDefaultView(_users);
            _adminViewModel = adminViewModel;

            ShowModifyPanel = new BasicCommand((object obj) => EditFormVisible = Visibility.Visible);
            

            SendAccountVerificationEmail = new BasicCommand((object obj) =>
            {   
                SelectedUser.Token = Guid.NewGuid().ToString();
                SelectedUser.Tokendate = DateTime.Now;

                EmailSender emailSender = new EmailSender();
                emailSender.send(SelectedUser);

                DbContext.SaveChanges();
            });


			ModifyUserCommand = new BasicCommand((object obj) =>
            {
                if (SelectedUser != null)
                {
                    var userToUpdate = DbContext.Users.FirstOrDefault(u => u.Id == SelectedUser.Id);
                    MailAddress mail;
                    if ((string.IsNullOrEmpty(Email) || string.IsNullOrWhiteSpace(Email)) || (MailAddress.TryCreate(Email, out mail)))
                    {
                        if (userToUpdate != null && !string.IsNullOrEmpty(Name) && !string.IsNullOrEmpty(Surname))
                        {
                            userToUpdate.Name = Name;
                            userToUpdate.Surname = Surname;
                            userToUpdate.Login = Login;
                            userToUpdate.Email = Email;
                            try
                            {
                                // Zapisujemy zmiany do bazy danych
                                DbContext.SaveChanges();
                                System.Windows.MessageBox.Show("Zmiany zostały zapisane pomyślnie.");
                            }
                            catch (Exception ex)
                            {
                                System.Windows.MessageBox.Show($"Wystąpił błąd podczas zapisywania zmian");
                            }
                            // Refresh 
                            _adminViewModel.CurrentViewModel = new ListUserViewModel(adminViewModel);
                        }
                    }
                    else
                    {
                        System.Windows.MessageBox.Show($"Wystąpił błąd podczas zapisywania zmian");
						_adminViewModel.CurrentViewModel = new ListUserViewModel(adminViewModel);
					}
				}
            });


            DeleteUserCommand = new BasicCommand((object obj) =>
            {
                var selected = DbContext.Users.Where(user => SelectedUser.Id == user.Id).First();

                Console.WriteLine(selected.Id);

                DbContext.Database.ExecuteSql($"update user set deleted = 1 where id = {SelectedUser.Id}");

              //  DbContext.Database.ExecuteSql($"update appointment set doctor_id =NULL, doctor_user_id =NULL where doctor_user_id={SelectedUser.Id}");
                //    DbContext.Database.ExecuteSqlRaw($"delete from doctor_specialization where doctor_id = (select id from doctor where user_id = {SelectedUser.Id}); ");
					//DbContext.Database.ExecuteSqlRaw($"delete from doctor where user_id = {SelectedUser.Id}; ");
				//DbContext.Database.ExecuteSqlRaw($"delete from administrator where user_id = {SelectedUser.Id}; ");
				//DbContext.Database.ExecuteSqlRaw($"delete from receptionist where user_id = {SelectedUser.Id}; ");


				//DbContext.Database.ExecuteSqlRaw($"delete from user where id = {SelectedUser.Id}; ");

                 //   DbContext.SaveChanges();
                    _adminViewModel.CurrentViewModel = new ListUserViewModel(adminViewModel);
                

                // Refresh 

            }
            );
        }
    }
}
    

