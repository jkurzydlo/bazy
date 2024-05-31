﻿using bazy1.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using bazy1.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using QuestPDF;
using System.Windows.Input;
using Microsoft.VisualBasic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System.ComponentModel;
using System.Windows.Data;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
namespace bazy1.ViewModels.Receptionist.Pages {


	public class PatientListViewModel : ViewModelBase {

		private Patient _selectedPatient;
		private ObservableCollection<Patient> _patientsList;
		public ICommand ShowMedicalHistoryCommand { get; }
		public ICommand AddPatientCommand { get; set; }
		public ICommand PatientDeleteCommand { get; set; }
		public ICommand ShowAddAppointmentViewCommand { get; set; }
		public ICommand ShowReferralListCommand { get; set; }
		public ICommand ShowPrescriptionListCommand { get; set; }
		public ICommand ShowAppointmentsListCommand { get; set; }
		public ICommand EditPatientCommand { get; set; }
		public ICommand ShowPatientDocumentsList { get; set; }



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
					var names = DbContext.Database.SqlQueryRaw<string>($"select m.name from (prescription p join prescription_medicine ps on p.id=ps.prescription_id) join medicine m on m.id=ps.medicine_id join patient pat on pat.id =p.patient_id where pat.id ={SelectedPatient.Id};").ToList();

					Console.WriteLine("namws: " + names.Count());
					var dosages = DbContext.Database.SqlQueryRaw<string>($"select m.dose from (prescription p join prescription_medicine ps on p.id=ps.prescription_id) join medicine m on m.id=ps.medicine_id join patient pat on pat.id =p.patient_id where pat.id ={SelectedPatient.Id};").ToList();
					string tempMedicines = "";

					for (int i = 0; i < names.Count; i++)
					{
						tempMedicines += $"{names[i]}: {(i<dosages.Count ? dosages[i] : "")}\n";
					}
					info += tempMedicines;


				}
				return info;
			}
			set {

			}
		}
		private ICollectionView patientsView;
		private string _filterText;

		public string FilterText {
			get => _filterText;
			set {
				_filterText = value;

				//Wyszukiwanie po nazwie
				PatientView.Filter += (object patient) =>
				{
					var tempPatient = patient as Patient;
					return tempPatient.Name.ToLower().Contains(FilterText.ToLower().Trim()) ||
					tempPatient.Surname.ToLower().Contains(FilterText.ToLower().Trim()) ||
					tempPatient.Pesel.ToString().ToLower().Contains(FilterText.ToLower().Trim());

				};
				OnPropertyChanged(nameof(FilterText));

			}
		}

		public ICollectionView PatientView { get => patientsView; set => patientsView = value; }


		public PatientListViewModel(ReceptionistViewModel viewModel) {

			EditPatientCommand = new BasicCommand((object obj) =>
			{
				viewModel.CurrentViewModel = new EditPatientViewModel(SelectedPatient);
			});

			ShowAppointmentsListCommand = new BasicCommand((object obj) =>
			{
				viewModel.CurrentViewModel = new PatientAppointmentsViewModel(viewModel, SelectedPatient);
			});

			ShowAddAppointmentViewCommand = new BasicCommand(obj => {
				viewModel.CurrentViewModel = new AddAppointmentViewModel(viewModel, SelectedPatient);
			});

			ShowPatientDocumentsList = new BasicCommand(obj =>
			{
                Console.WriteLine(SelectedPatient.Prescriptions.Count());
                viewModel.CurrentViewModel = new PatientDocumentsListViewModel(DbContext.Prescriptions.Where(pr => pr.PatientId == SelectedPatient.Id).ToList(), DbContext.Referrals.Where(pr => pr.PatientId == SelectedPatient.Id).ToList());
			});

			AddPatientCommand = new BasicCommand(obj => viewModel.CurrentViewModel = new AddPatientViewModel(viewModel));
			PatientDeleteCommand = new BasicCommand(obj =>
			{
				/*
				DbContext.Database.ExecuteSql($"Delete from patient_diesease where patient_id = {SelectedPatient.Id}");
				DbContext.Database.ExecuteSql($"Delete from patient_address where patient_id = {SelectedPatient.Id}");
				DbContext.Database.ExecuteSql($"Delete from doctor_has_patient where patient_id = {SelectedPatient.Id}");
				DbContext.Database.ExecuteSql($"delete from appointment where patient_id={SelectedPatient.Id}");
				DbContext.Database.ExecuteSql($"delete from prescription_medicine where prescription_patient_id ={SelectedPatient.Id};");
				DbContext.Database.ExecuteSql($"delete from prescription where patient_id={SelectedPatient.Id}");
				DbContext.Database.ExecuteSql($"delete from referral where patient_id={SelectedPatient.Id}");
				DbContext.Database.ExecuteSql($"delete from patient where patient_id={SelectedPatient.Id}");
				*/
				DbContext.Database.ExecuteSql($"update patient set deleted = 1 where id={SelectedPatient.Id}");
				DbContext.Database.ExecuteSql($"update workhours set open = true where start in (select date from appointment a join patient p on p.id=a.patient_id where p.deleted = 1)");
				//DbContext.SaveChanges();
				viewModel.CurrentViewModel = new PatientListViewModel(viewModel);
			});

			_patientsList = new(DbContext.Patients.Where(p=>!p.Deleted).ToList());

            foreach (var item in _patientsList)
            {
				DbContext.Entry(item).Reload();
            }

			PatientView = CollectionViewSource.GetDefaultView(_patientsList);

			ShowMedicalHistoryCommand = new BasicCommand((object obj) => {
				if (SelectedPatient != null) viewModel.CurrentViewModel = new MedicalHistoryViewModel(DbContext.Patients.Where(pat => pat.Id == SelectedPatient.Id).First(), null, viewModel);
			});


		}

		public ObservableCollection<Patient> PatientsList {
			get => _patientsList;
			set {
				_patientsList = value;
				OnPropertyChanged(nameof(PatientsList));
			}
		}

		public Patient SelectedPatient {
			get => _selectedPatient;
			set {
				_selectedPatient = value;
				OnPropertyChanged(nameof(SelectedPatient));
			}
		}
	}
}
