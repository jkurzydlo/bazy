using bazy1.Models;
using System.Windows.Input;

namespace bazy1.ViewModels.Admin.Pages
{
    public class AdminEditPatientViewModel : ViewModelBase
    {
        private Patient _selectedPatient;
        private readonly przychodnia9Context DbContext;
        private readonly AdminPatientListViewModel _adminPatientListViewModel;

        public AdminEditPatientViewModel(AdminPatientListViewModel adminPatientListViewModel, Patient patient)
        {
            _adminPatientListViewModel = adminPatientListViewModel;
            DbContext = new przychodnia9Context();
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

                // Powrót do listy pacjentów po zapisaniu
                _adminPatientListViewModel.CurrentViewModel = _adminPatientListViewModel;
            }
        }
    }
}

