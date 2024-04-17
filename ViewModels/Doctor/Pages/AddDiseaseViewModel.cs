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
        public ICommand AddDiseaseCommand { get; }
		public string Name { 
            get => _name;
            set {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }
		public string Description {
            get => _description;
            set {
                _description = value;
                OnPropertyChanged(nameof(Description));
            }
        }
		public DateOnly Date {
            get => _date;
            set{
                _date = value;
                OnPropertyChanged(nameof(Date));
            }
        }

		private dbm.Doctor doctor;
        private string _name, _description;
        private DateOnly _date;
        public AddDiseaseViewModel(dbm.Patient patient) {
            AddDiseaseCommand = new BasicCommand((object obj) =>
            {
                Console.WriteLine(patient.Name);
                Disease disease = new Disease();
                disease.Comments = Description;
                disease.DateFrom = Date.ToDateTime(new TimeOnly(0, 0, 0));
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
        }
    }
}
