using bazy1.Models;
using System.Windows.Input;
using System.Linq;

namespace bazy1.ViewModels.Receptionist.Pages
{
    public class EditPatientViewModel : ViewModelBase
    {
        private Patient _patient;
        private Przychodnia9Context DbContext;

        public EditPatientViewModel(Patient patient)
        {
            _patient = patient;
            DbContext = new Przychodnia9Context();
            SaveCommand = new BasicCommand(Save);
        }

        public Patient Patient
        {
            get => _patient;
            set
            {
                _patient = value;
                OnPropertyChanged(nameof(Patient));
            }
        }

        public ICommand SaveCommand { get; }

        private void Save(object obj)
        {
            var patientInDb = DbContext.Patients.First(p => p.Id == Patient.Id);
            patientInDb.Name = Patient.Name;
            patientInDb.Surname = Patient.Surname;
            patientInDb.Pesel = Patient.Pesel;
            patientInDb.PhoneNumber = Patient.PhoneNumber;
            patientInDb.Email = Patient.Email;
            // Dodaj inne pola do edycji tutaj

            DbContext.SaveChanges();
        }
    }
}
