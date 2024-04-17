using bazy1.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;

namespace bazy1.ViewModels.Doctor.Pages {
	public class MedicalHistoryViewModel : ViewModelBase {
		private ObservableCollection<Disease> diesasesList;
		private Patient patient;
		private List<Medicine> _medicines;
		private string _medicineDetails;
		private string? _filterText;

		public MedicalHistoryViewModel(Patient patient) {
			this.patient = patient;
			Console.Write("pacjent:"+patient.Name);
			_medicines = patient.Medicines.ToList();
			//DbContext.Patients.ForEachAsync(pat => Console.WriteLine("wsz: "+pat.Diseases.Count));
			//diesasesList = new(DbContext.Patients.Where(patient => patient.Id == this.patient.Id).First().Diseases);
			diesasesList = new(DbContext.Diseases.Where(dis => dis.Patients.Contains(patient)));

			//Console.WriteLine("chr: "+diesasesList.Count());
			DiseasesView = CollectionViewSource.GetDefaultView(diesasesList);
		}

		public ObservableCollection<Disease> DiseasesList {
			get => diesasesList;
			set {
				diesasesList = value;
				OnPropertyChanged(nameof(DiseasesList));
			}
		}

		private ICollectionView diseasesView;

		public string FilterText { 
			get => _filterText;
			set{
				_filterText = value;

				//Wyszukiwanie po nazwie
				DiseasesView.Filter += (object disease) =>
				{
					var tempDisease = disease as Disease;
					return tempDisease.Name.ToLower().Contains(FilterText.ToLower().Trim());
				};
				OnPropertyChanged(nameof(FilterText));
				
			} 
		}

		public ICollectionView DiseasesView { get => diseasesView; set => diseasesView = value; }
		public string Medicines {
			get {
				string tempMedicines = "";
                for(int i=0;i<_medicines.Count;i++)
                {
					tempMedicines += $"{_medicines[i].Name}{(i!= _medicines.Count -1 ? "," : "")}";
                }
				return tempMedicines;
            }
		}

		public string MedicineDetails {
			get{
				string tempMedicines = "";
				for (int i = 0; i < _medicines.Count; i++)
				{
					tempMedicines += $"{_medicines[i].Name}:{_medicines[i].Dose} {(i != _medicines.Count - 1 ? "\n" : "")}";
				}
				return tempMedicines;

			}
			set => _medicineDetails = value; 
		}
	}
}
