using bazy1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace bazy1.ViewModels.Doctor.Pages
{
    using dbm = Models;
    public class AddDiseaseViewModel : ViewModelBase
    {
<<<<<<< HEAD
        public ICommand AddDiseaseCommand { get; }
        public string Name
        {
            get => _name;
            set
            {
=======
        private Patient _selectedPatient;
        public ICommand AddDiseaseCommand { get; }
		public string Name { 
            get => _name;
            set {
>>>>>>> master
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }
<<<<<<< HEAD
        public string Description
        {
            get => _description;
            set
            {
=======
		public string Description {
            get => _description;
            set {
>>>>>>> master
                _description = value;
                OnPropertyChanged(nameof(Description));
            }
        }
<<<<<<< HEAD
        public DateOnly Date
        {
            get => _date;
            set
            {
                _date = value;
                OnPropertyChanged(nameof(Date));
            }
        }

        private dbm.Doctor doctor;
        private string _name, _description;
        private DateOnly _date;
        public AddDiseaseViewModel(dbm.Patient patient)
        {
=======
		public DateTime DateFrom {
            get => _date;
            set{
                _date = value;
                OnPropertyChanged(nameof(DateFrom));
            }
        }

		public Patient SelectedPatient { 
            get => _selectedPatient;
            set {
                _selectedPatient = value;
                OnPropertyChanged(nameof(SelectedPatient));
            }
        }

		private dbm.Doctor doctor;
        private string _name, _description;
        private DateTime _date = DateTime.Now;
        public AddDiseaseViewModel(dbm.Patient patient) {
            SelectedPatient = patient;
>>>>>>> master
            AddDiseaseCommand = new BasicCommand((object obj) =>
            {
                Console.WriteLine(patient.Name);
                Disease disease = new Disease();
                disease.Comments = Description;
<<<<<<< HEAD
                disease.DateFrom = Date.ToDateTime(new TimeOnly(0, 0, 0));
                if (!string.IsNullOrEmpty(Name) && !string.IsNullOrWhiteSpace(Name))
                {
                    disease.Name = Name;
                    patient.Diseases.Add(disease);
                    DbContext.SaveChanges();
                    Console.WriteLine("choroby:" + patient.Diseases.Count());
                }

            });
=======
                disease.DateFrom = DateTime.Parse(DateFrom.ToString());
                if (!string.IsNullOrEmpty(Name) && !string.IsNullOrWhiteSpace(Name))
                {
                    Patient tempPatient;  
                    disease.Name = Name;
                    DbContext.Patients.Where(pat => pat.Id == patient.Id).First().Diseases.Add(disease);
                    Console.WriteLine("imie: " + DbContext.Patients.Where(pat => pat.Id == patient.Id).First());
                    DbContext.SaveChanges();
                    Console.WriteLine("choroby:" + patient.Diseases.Count());
				}

			});
>>>>>>> master
        }
    }
}
