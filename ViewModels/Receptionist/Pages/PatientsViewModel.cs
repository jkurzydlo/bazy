using bazy1.Models;
using bazy1.Repositories;
using System;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace bazy1.ViewModels.Receptionist.Pages
{
    public class PatientListViewModel : ViewModelBase
    {
        private ObservableCollection<Patient> _patientsList;
        private Patient _selectedPatient;
        private PatientRepository _patientRepository;

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
            // Sprawdź, czy został wybrany pacjent
            if (SelectedPatient != null)
            {
                // Pobierz listę chorób dla wybranego pacjenta z bazy danych
                var patientDiseases = _patientRepository.GetPatientDiseases(SelectedPatient.Id);

                // Sprawdź, czy pacjent ma przypisane choroby
                if (patientDiseases != null && patientDiseases.Any())
                {
                    // Przygotuj tekst historii medycznej pacjenta
                    StringBuilder medicalHistoryBuilder = new StringBuilder();
                    medicalHistoryBuilder.AppendLine($"Historia medyczna pacjenta {SelectedPatient.Name} {SelectedPatient.Surname}:\n");

                    // Dodaj każdą chorobę pacjenta do historii medycznej
                    foreach (var disease in patientDiseases)
                    {
                        string dateFrom = disease.DateFrom?.ToShortDateString() ?? "Unknown";
                        string dateTo = disease.DateTo?.ToShortDateString() ?? "Unknown";
                        medicalHistoryBuilder.AppendLine($"- {disease.Name}, rozpoczęta dnia {dateFrom}, zakończona dnia {dateTo}");
                    }

                    // Wyświetl historię medyczną w MessageBox
                    MessageBox.Show(medicalHistoryBuilder.ToString(), "Historia medyczna", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Brak dostępnej historii medycznej dla tego pacjenta.", "Historia medyczna", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }

        private void ExecuteShowAddDiseaseCommand(object obj)
        {
            // Tutaj dodawanie schorzenia dla wybranego pacjenta
        }
    }
}

