using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Data;
using System.Windows.Input;
using bazy1.Models;
using CommunityToolkit.Mvvm.Input;

namespace bazy1.ViewModels.Admin.Pages
{
    public class AdminPatientListViewModel : ViewModelBase
    {
        private Patient _selectedPatient;
        private ObservableCollection<Patient> _patientsList;
        private ICollectionView patientsView;
        private string _filterText;
        private readonly AdminViewModel _adminViewModel;

        public ICommand ShowMedicalHistoryCommand { get; }
        public ICommand ShowAddDiseaseCommand { get; }
        public ICommand AddPatientCommand { get; }
        public ICommand PatientDeleteCommand { get; }
        public ICommand ShowAddMedicationCommand { get; }
        public ICommand ShowAddReferralCommand { get; }
        public ICommand ShowEditPatientCommand { get; }
        public ICommand ShowAddAppointmentCommand { get; }

        public AdminPatientListViewModel()
        {

            using (var DbContext = new Przychodnia9Context())
            {
                _patientsList = new ObservableCollection<Patient>(DbContext.Patients.ToList());
            }
            PatientView = CollectionViewSource.GetDefaultView(_patientsList);

            ShowAddAppointmentCommand = new BasicCommand((object obj) => { /* Implementacja */ });
            ShowAddMedicationCommand = new BasicCommand(obj => { /* Implementacja */ });
            ShowAddReferralCommand = new BasicCommand(obj => { /* Implementacja */ });
            AddPatientCommand = new BasicCommand(obj => { /* Implementacja */ });
            PatientDeleteCommand = new BasicCommand(obj =>
            {
                if (SelectedPatient != null)
                {
                    using (var DbContext = new Przychodnia9Context())
                    {
                        DbContext.Patients.Remove(SelectedPatient);
                        DbContext.SaveChanges();
                    }
                    _patientsList.Remove(SelectedPatient);
                    PatientView.Refresh();
                }
            });

            ShowMedicalHistoryCommand = new BasicCommand((object obj) => { /* Implementacja */ });
            ShowAddDiseaseCommand = new BasicCommand((object obj) => { /* Implementacja */ });
            ShowEditPatientCommand = new BasicCommand(ShowEditPatient);
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

        public ICollectionView PatientView
        {
            get => patientsView;
            set => patientsView = value;
        }

        public string FilterText
        {
            get => _filterText;
            set
            {
                _filterText = value;
                PatientView.Filter = (object patient) =>
                {
                    var tempPatient = patient as Patient;
                    return tempPatient.Name.ToLower().Contains(FilterText.ToLower().Trim()) ||
                           tempPatient.Surname.ToLower().Contains(FilterText.ToLower().Trim()) ||
                           tempPatient.Pesel.ToString().ToLower().Contains(FilterText.ToLower().Trim());
                };
                OnPropertyChanged(nameof(FilterText));
            }
        }

        private void ShowEditPatient(object obj)
        {
            var selectedPatient = obj as Patient;
            if (selectedPatient != null)
            {
                // Utwórz widok modelu edycji pacjenta i przekaż wybranego pacjenta
                var editPatientViewModel = new AdminEditPatientViewModel(_adminViewModel, selectedPatient);
                
                // Ustaw widok modelu edycji pacjenta jako aktualny widok
                _adminViewModel.CurrentViewModel = editPatientViewModel;
            }
        }
    }
}

