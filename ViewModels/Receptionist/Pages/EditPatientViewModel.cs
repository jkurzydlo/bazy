﻿using bazy1.Models;
using System.Windows.Input;

namespace bazy1.ViewModels.Receptionist.Pages
{
    public class EditPatientViewModel : ViewModelBase
    {
        private Patient _selectedPatient;
        private readonly Przychodnia9Context DbContext;

        public EditPatientViewModel(Patient patient)
        {
            DbContext = new Przychodnia9Context();
            SelectedPatient = patient;
            SavePatientCommand = new BasicCommand(SavePatient);
        }

        public Patient SelectedPatient
        {
            get => _selectedPatient;
            set
            {
                _selectedPatient = value;
                OnPropertyChanged(nameof(SelectedPatient));
            }
        }

        public ICommand SavePatientCommand { get; }

        private void SavePatient(object obj)
        {
            var existingPatient = DbContext.Patients.FirstOrDefault(p => p.Id == SelectedPatient.Id);
            if (existingPatient != null)
            {
                existingPatient.Name = SelectedPatient.Name;
                existingPatient.Surname = SelectedPatient.Surname;
                existingPatient.Pesel = SelectedPatient.Pesel;
                existingPatient.PhoneNumber = SelectedPatient.PhoneNumber;
                existingPatient.Email = SelectedPatient.Email;
                DbContext.SaveChanges();
            }
        }
    }
}

