﻿using bazy1.Models;
using bazy1.Models.Part;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.ObjectModel;
using System.Linq;
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

            ModifyUserCommand = new BasicCommand((object obj) =>
            {
                if (SelectedUser != null)
                {
                    var userToUpdate = DbContext.Users.FirstOrDefault(u => u.Id == SelectedUser.Id);
                    if (userToUpdate != null)
                    {
                        userToUpdate.Name = Name;
                        userToUpdate.Surname = Surname;
                        userToUpdate.Login = Login;
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
                var doctors = DbContext.Doctors.Where(doctor => doctor.UserId == selected.Id).First();
                doctors.User = null;
                DbContext.Doctors.Remove(doctors);
                doctors.Specializations = null;
                doctors.Offices = null;
                doctors.Workhours = null;
                doctors.Patients = null;

                DbContext.Users.Remove(selected);

                DbContext.SaveChanges();
                _adminViewModel.CurrentViewModel = new ListUserViewModel(adminViewModel);

                // Refresh 
                Users.Remove(SelectedUser);
            }
            );
        }
    }
}
    

