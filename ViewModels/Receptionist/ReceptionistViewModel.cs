using bazy1.Models;
using bazy1.Repositories;
using bazy1.ViewModels.Doctor;
using bazy1.Views.Receptionist.Pages;
using System.Collections.Generic;
using System.Windows.Input;

namespace bazy1.ViewModels.Receptionist
{
    public class ReceptionistViewModel : DoctorViewModel
    {
        private readonly PatientRepository _patientRepository;

        public ICommand ShowPatientsCommand { get; }

        private List<Patient> _patients;
        public List<Patient> Patients
        {
            get { return _patients; }
            set
            {
                _patients = value;
                OnPropertyChanged(nameof(Patients));
            }
        }

        public ReceptionistViewModel()
        {
            _patientRepository = new PatientRepository();
            Patients = _patientRepository.GetPatients();

            ShowPatientsCommand = new RelayCommand(ExecuteShowPatientsCommand);
        }

        private void ExecuteShowPatientsCommand(object obj)
        {
            PatientsView patientsView = new PatientsView();
            patientsView.DataContext = this; // Przekazujemy bieżący widok modelu do PatientsView
            patientsView.Show();
        }
        public void UpdatePatients()
        {
            Patients = _patientRepository.GetPatients();
        }
    }
}




