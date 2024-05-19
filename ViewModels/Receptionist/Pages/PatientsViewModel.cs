using bazy1.Models;
using bazy1.Repositories;
using System;
using System.Collections.ObjectModel;
using System.Linq; // Dodane
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace bazy1.ViewModels.Receptionist.Pages // Zmienione
{
    public class PatientListViewModel : ViewModelBase
    {
        private ObservableCollection<Patient> _patientsList;
        private Patient _selectedPatient;
        private PatientRepository _patientRepository;

        public PatientListViewModel()
        {
            _patientRepository = new PatientRepository(); // Dodane
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
            _patientsList = new ObservableCollection<Patient>(_patientRepository.GetPatients());
        }

        private void ExecuteShowMedicalHistoryCommand(object obj)
        {
            if (SelectedPatient != null)
            {
                Console.WriteLine($"Wybrany pacjent: {SelectedPatient.Name} {SelectedPatient.Surname}");
                var patientDiseases = _patientRepository.GetPatientDiseases(SelectedPatient.Id);
                if (patientDiseases != null && patientDiseases.Any())
                {
                    StringBuilder medicalHistoryBuilder = new StringBuilder();
                    medicalHistoryBuilder.AppendLine($"Historia medyczna pacjenta {SelectedPatient.Name} {SelectedPatient.Surname}:\n");
                    foreach (var disease in patientDiseases)
                    {
                        string dateFrom = disease.DateFrom?.ToShortDateString() ?? "Unknown";
                        string dateTo = disease.DateTo?.ToShortDateString() ?? "Unknown";
                        medicalHistoryBuilder.AppendLine($"- {disease.Name}, rozpoczęta dnia {dateFrom}, zakończona dnia {dateTo}");
                    }
                    MessageBox.Show(medicalHistoryBuilder.ToString(), "Historia medyczna", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Brak dostępnej historii medycznej dla tego pacjenta.", "Historia medyczna", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            else
            {
                Console.WriteLine("Nie wybrano pacjenta.");
            }
        }


        private void ExecuteShowAddDiseaseCommand(object obj)
        {
            // Tutaj dodawanie schorzenia dla wybranego pacjenta
        }
    }
}

