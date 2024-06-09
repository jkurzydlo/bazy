using bazy1.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Data;
using System.Windows.Input;

namespace bazy1.ViewModels.Admin.Pages
{
    public class AdminMedicalHistoryViewModel : ViewModelBase
    {
        private ObservableCollection<Disease> _diseasesList;
        private Disease _selectedDisease;
        private Patient _patient;
        private string _filterText;
        private ICollectionView _diseasesView;

        public Patient SelectedPatient { get; set; }
        public AdminMedicalHistoryViewModel(Patient selectedPatient)
        {
            SelectedPatient = selectedPatient;

            using (var DbContext = new Przychodnia9Context())
            {
                _diseasesList = new ObservableCollection<Disease>(DbContext.Diseases.Where(d => d.Patients.Contains(SelectedPatient)).ToList());
            }

            DiseasesView = CollectionViewSource.GetDefaultView(_diseasesList);
        }

        public ObservableCollection<Disease> DiseasesList
        {
            get => _diseasesList;
            set
            {
                _diseasesList = value;
                OnPropertyChanged(nameof(DiseasesList));
            }
        }

        public Disease SelectedDisease
        {
            get {
				return _selectedDisease;
            }
            set
            {

                if (value != null)
                {
                    _selectedDisease = value;
                    OnPropertyChanged(nameof(SelectedDisease));

                    DbContext.Diseases.Where(d => d.Id == SelectedDisease.Id).First().Comments = SelectedDisease.Comments;

                    Console.WriteLine(SelectedDisease.Comments);
                    DbContext.SaveChanges();
                }
			}
        }

        public string FilterText
        {
            get => _filterText;
            set
            {
                _filterText = value;
                DiseasesView.Filter = (object disease) =>
                {
                    var tempDisease = disease as Disease;
                    return tempDisease.Name.ToLower().Contains(FilterText.ToLower().Trim());
                };
                OnPropertyChanged(nameof(FilterText));
                foreach (var item in DiseasesList)
                {
                    Console.WriteLine(item.Comments);
                }
            }
        }

        public ICollectionView DiseasesView
        {
            get => _diseasesView;
            set => _diseasesView = value;
        }
    }
}

