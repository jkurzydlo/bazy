using bazy1.Models;
using bazy1.Repositories;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace bazy1.ViewModels.Receptionist.Pages
{
    public class PatientListViewModel : ViewModelBase
    {
        private ObservableCollection<Patient> _patientsList;
        private Patient _selectedPatient;

        public PatientListViewModel()
        {
            LoadPatients();
            ShowMedicalHistoryCommand = new BasicCommand(ExecuteShowMedicalHistoryCommand);
            ShowAddDiseaseCommand = new BasicCommand(ExecuteShowAddDiseaseCommand);
        }

        public ObservableCollection<Patient> PatientsList
        {
            get => _patientsList;
            set
            {
                _patientsList = value;
                OnPropertyChanged(nameof(PatientsList));
            }
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

        public ICommand ShowMedicalHistoryCommand { get; }
        public ICommand ShowAddDiseaseCommand { get; }

        private void LoadPatients()
        {
            PatientRepository patientRepository = new PatientRepository();
            _patientsList = new ObservableCollection<Patient>(patientRepository.GetPatients());
        }

        private void ExecuteShowMedicalHistoryCommand(object obj)
        {
            // Tutaj przeglądanie historii medycznej wybranego pacjenta
        }

        private void ExecuteShowAddDiseaseCommand(object obj)
        {
            // Tutaj dodawanie schorzenia dla wybranego pacjenta
        }
    }
}

