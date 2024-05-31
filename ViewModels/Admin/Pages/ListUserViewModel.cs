using bazy1.Models;
using bazy1.Models.Part;
using bazy1.Utils;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Mail;
using System.Windows;
using System.Windows.Input;

namespace bazy1.ViewModels.Admin.Pages {
    public class ListUserViewModel : ViewModelBase {
        private readonly AdminViewModel _adminViewModel;
        private ObservableCollection<User> _users;
        private User _selectedUser;
        private Visibility _editFormVisible = Visibility.Hidden;

        public ICommand ShowModifyPanel { get; set; }
        public ICommand ModifyUserCommand { get; set; }
        public ICommand DeleteUserCommand { get; }
        public ICommand SendAccountVerificationEmail { get; set;}


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

        public ListUserViewModel(AdminViewModel adminViewModel) {

            _adminViewModel = adminViewModel;
            _users = new ObservableCollection<User>(DbContext.Users);

            ShowModifyPanel = new BasicCommand((object obj) => EditFormVisible = Visibility.Visible);


            SendAccountVerificationEmail = new BasicCommand((object obj) =>
            {
                EmailSender emailSender = new EmailSender();
                emailSender.send(SelectedUser);
                SelectedUser.Token = Guid.NewGuid().ToString();
                SelectedUser.Tokendate = DateTime.Now;
                DbContext.SaveChanges();
            });

            ModifyUserCommand = new BasicCommand((object obj) =>
            {
                if (SelectedUser != null)
                {
                    var userToUpdate = DbContext.Users.FirstOrDefault(u => u.Id == SelectedUser.Id);
                    MailAddress mail;
                    if((string.IsNullOrEmpty(Email) || string.IsNullOrWhiteSpace(Email)) || (MailAddress.TryCreate(Email, out mail)))
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
                            System.Windows.MessageBox.Show($"Wystąpił błąd podczas zapisywania zmian: {ex.Message}");
                        }
                        // Refresh 
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
    

