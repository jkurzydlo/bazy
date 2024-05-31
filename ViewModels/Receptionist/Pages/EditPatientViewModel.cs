﻿using bazy1.Models;
using System.Windows.Input;

namespace bazy1.ViewModels.Receptionist.Pages
{
    public class EditPatientViewModel : ViewModelBase
    {
        private Patient _selectedPatient;
        private readonly przychodnia9Context DbContext;
        private readonly ReceptionistViewModel _receptionistViewModel;

        public EditPatientViewModel(ReceptionistViewModel receptionistViewModel, Patient patient)
        {
            _receptionistViewModel = receptionistViewModel;
            DbContext = new przychodnia9Context();
            SelectedPatient = patient;
            SavePatientCommand = new BasicCommand(SavePatient);
        }

        public EditPatientViewModel(Patient selectedPatient)
        {
            SelectedPatient = selectedPatient;
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

                // Powrót do listy pacjentów po zapisaniu
                _receptionistViewModel.CurrentViewModel = new PatientListViewModel(_receptionistViewModel);
            }
        }
    }
}


