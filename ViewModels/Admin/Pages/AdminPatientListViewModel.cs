using bazy1.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Data;
using System.Windows.Input;
using bazy1.Models;
using bazy1.ViewModels.Receptionist.Pages;
using CommunityToolkit.Mvvm.Input;
using bazy1.Views.Admin.Pages;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;

namespace bazy1.ViewModels.Admin.Pages
{
    public class AdminPatientListViewModel : ViewModelBase
    {
        private Patient _selectedPatient;
        private ObservableCollection<Patient> _patientsList;
        private ViewModelBase _currentViewModel;
        public ICommand ShowMedicalHistoryCommand { get; }
        public ICommand ShowAddDiseaseCommand { get; }
        public ICommand AddPatientCommand { get; set; }
        public ICommand PatientDeleteCommand { get; set; }
        public ICommand ShowAddMedicationCommand { get; set; }
        public ICommand ShowAddReferralCommand { get; set; }
        public ICommand ShowAddAppointmentCommand { get; set; }
        public ICommand AdminEditPatientCommand { get; set; }
        public ICommand ShowPatientListCommand { get; set;}

        private ICollectionView patientsView;
        private string _filterText;


		public string PatientDetails {
			get {
				string adressess = "", info = "";
				if (SelectedPatient != null)
				{
					Console.WriteLine(DbContext.Addresses.Count());
					var tempPatient = DbContext.Patients.Where(pat => pat.Id == SelectedPatient.Id).First();
					if (tempPatient.SecondName != null) info += "Drugie imię: " + tempPatient.SecondName + "\n";
					Console.WriteLine("ile: " + DbContext.Addresses.Where(adr => adr.Patients.Contains(tempPatient)).Count());
					DbContext.Addresses.Where(adr => adr.Patients.Contains(tempPatient)).ToList().
						ForEach(adr => adressess += adr.City + " " + adr.PostalCode + " ul." + adr.Street + " " + adr.BuildingNumber + "\n");
					info = $"Data urodzenia: {tempPatient.BirthDate.Value.ToShortDateString()}\n";
					if (tempPatient.PhoneNumber != null) info += "Telefon: " + tempPatient.PhoneNumber + "\n";
					if (tempPatient.Email != null) info += "Email: " + tempPatient.Email + "\n";
					info += "Adresy:" + adressess;
					info += "Przyjmowane leki:\n";
					string tempDoses = "";
					var names = DbContext.Database.SqlQueryRaw<string>("select med.name from patient_diesease pd join prescription pr on pr.patient_id=pd.patient_id" +
				" join prescription_medicine pm on pm.prescription_id = pr.id" +
				$" join medicine med on med.id = pm.medicine_id where pd.patient_id={SelectedPatient.Id}").ToList();

					var dosages = DbContext.Database.SqlQueryRaw<string>("select med.dose from patient_diesease pd join prescription pr on pr.patient_id=pd.patient_id" +
" join prescription_medicine pm on pm.prescription_id = pr.id" +
$" join medicine med on med.id = pm.medicine_id where pd.patient_id={SelectedPatient.Id} ").ToList();
					string tempMedicines = "";

					for (int i = 0; i < names.Count; i++)
					{
						tempMedicines += $"{names[i]}: {dosages[i]}\n";
					}
					info += tempMedicines;


				}
				return info;
			}
			set {

			}
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

        public ICollectionView PatientView
        {
            get => patientsView;
            set => patientsView = value;
        }

        public ViewModelBase CurrentViewModel
        {
            get => _currentViewModel;
            set
            {
                _currentViewModel = value;
                OnPropertyChanged(nameof(CurrentViewModel));
            }
        }

        public AdminPatientListViewModel(AdminViewModel viewModel)
        {
            CurrentViewModel = viewModel;
            using (var DbContext = new Przychodnia9Context())
            {
                _patientsList = new ObservableCollection<Patient>(DbContext.Patients.ToList());
            }
            PatientView = CollectionViewSource.GetDefaultView(_patientsList);

            ShowPatientListCommand = new BasicCommand((object obj) =>
            { 
                				viewModel.CurrentViewModel = new AdminPatientListViewModel(viewModel);
            });
            ShowAddAppointmentCommand = new BasicCommand((object obj) => { /* Implementacja */ });
            ShowAddMedicationCommand = new BasicCommand(obj => { /* Implementacja */ });
            ShowAddReferralCommand = new BasicCommand(obj => { /* Implementacja */ });
            AddPatientCommand = new BasicCommand(obj => viewModel.CurrentViewModel = new  AddPatientAdminControl(this));
            PatientDeleteCommand = new BasicCommand(obj =>
            {
                if (SelectedPatient != null)
                {
                    using (var DbContext = new Przychodnia9Context())
                    {
						//patient_diesease itd...
						//DbContext.Patients.Remove(SelectedPatient);
						DbContext.Database.ExecuteSql($"Delete from patient_diesease where patient_id = {SelectedPatient.Id}");
						DbContext.Database.ExecuteSql($"Delete from patient_address where patient_id = {SelectedPatient.Id}");
						DbContext.Database.ExecuteSql($"Delete from doctor_has_patient where patient_id = {SelectedPatient.Id}");
                        DbContext.Database.ExecuteSql($"delete from appointment where patient_id={SelectedPatient.Id}");
						DbContext.Database.ExecuteSql($"delete from prescription_medicine where prescription_patient_id ={SelectedPatient.Id};");
						DbContext.Database.ExecuteSql($"delete from prescription where patient_id={SelectedPatient.Id}");
						DbContext.Database.ExecuteSql($"delete from referral where patient_id={SelectedPatient.Id}");

						DbContext.Database.ExecuteSql($"delete from patient where id={SelectedPatient.Id}");
                        foreach(var p in PatientsList) DbContext.Update(p);
                        //DbContext.SaveChanges();
                    }
                    _patientsList.Remove(SelectedPatient);
                    PatientView.Refresh();
                }
            });

            ShowMedicalHistoryCommand = new RelayCommand((object obj) =>
            {
				if (SelectedPatient != null)
				{
					viewModel.CurrentViewModel = new AdminMedicalHistoryViewModel(SelectedPatient);
				}
			});
            ShowAddDiseaseCommand = new BasicCommand((object obj) => { /* Implementacja */ });
            AdminEditPatientCommand = new BasicCommand((object obj)=> 
            viewModel.CurrentViewModel = new AdminEditPatientViewModel(this,SelectedPatient));

           // CurrentViewModel = this;
        }
        private void ShowMedicalHistory(object obj)
        {
            Console.WriteLine("ShowMedicalHistory called");
            if (SelectedPatient != null)
            {
                CurrentViewModel = new AdminMedicalHistoryViewModel(SelectedPatient);
                Console.WriteLine("CurrentViewModel set to AdminMedicalHistoryViewModel");
            }
        }

        private void AdminEditPatient(object obj)
        {
            Console.WriteLine("AdminEditPatient called");
            try
            {
                if (SelectedPatient != null)
                {
                    CurrentViewModel = new AdminEditPatientViewModel(this, SelectedPatient);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
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


    }
}



