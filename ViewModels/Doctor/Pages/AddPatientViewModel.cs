using bazy1.Models;
using bazy1.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using dbm = bazy1.Models;

namespace bazy1.ViewModels.Doctor.Pages {
	public class AddPatientViewModel : ViewModelBase, IDataErrorInfo {
		private DoctorViewModel parentViewModel;
		private string _name, _surname, _pesel, _secondName, _sex,_email, _phone;
		private List<string> _sexes = ["K", "M"];
		private DateOnly _birthDate;
		private dbm.Doctor doctor;
		private Brush _nameTextBoxColor = SystemColors.ActiveBorderBrush;
		private Brush _peselTextBoxColor = SystemColors.ActiveBorderBrush;
		private Brush _surnameTextBoxColor = SystemColors.ActiveBorderBrush;
		private Brush _sexTextBoxColor = SystemColors.ActiveBorderBrush;
		private Brush _birthDateTextBoxColor = SystemColors.ActiveBorderBrush;




		public ICommand ShowPatientListCommand { get; set; }
		public ICommand AddNewPatientCommand { get; set; }
		public string Name {
			get => _name;
			set {
				NameTextBoxColor = SystemColors.ActiveBorderBrush;
				_name = value;
				OnPropertyChanged(nameof(Name));
			}
		}
		public Brush NameTextBoxColor {
			get => _nameTextBoxColor;
			set {
				_nameTextBoxColor = value;
				OnPropertyChanged(nameof(NameTextBoxColor));
			}
		}

		public string Surname {
			get => _surname;
			set {
				SurnameTextBoxColor = SystemColors.ActiveBorderBrush;

				_surname = value;
				OnPropertyChanged(nameof(Surname)); 
			}
		}
		public string Pesel {
			get => _pesel;
			set {
				PeselTextBoxColor = SystemColors.ActiveBorderBrush;
				_pesel = value;
				OnPropertyChanged(nameof(Pesel));
			}
		}
		public string SecondName {
			get => _secondName;
			set {
				_secondName = value;
				OnPropertyChanged(nameof(SecondName));
			}
		}
		
		public string Sex {
			get => _sex;
			set {
				SexTextBoxColor = SystemColors.ActiveBorderBrush;
				_sex = value;
				OnPropertyChanged(nameof(Sex));
			}
		}
		public List<string> Sexes { get => _sexes;
			set {
				_sexes = value;
				OnPropertyChanged(nameof(Sexes)); 
			}
		}
		public DateOnly BirthDate {
			get => _birthDate;
			set {
				BirthDateTextBoxColor = SystemColors.ActiveBorderBrush;
				_birthDate = value;
				OnPropertyChanged(nameof(BirthDate));
			}
		}
		public string Email {
			get => _email;
			set {
				_email = value;
				OnPropertyChanged(nameof(Email));
			}
		}
		public string PhoneNumber{
			get => _phone;
			set {
				_phone = value;
				OnPropertyChanged(nameof(PhoneNumber));
			}
		}

		public Brush PeselTextBoxColor {
			get => _peselTextBoxColor;
			set {
				_peselTextBoxColor = value;
				OnPropertyChanged(nameof(PeselTextBoxColor));
			}
		}
		public Brush SurnameTextBoxColor {
			get => _surnameTextBoxColor;
			set {
				_surnameTextBoxColor = value;
				OnPropertyChanged(nameof(SurnameTextBoxColor));
			}
		}
		public Brush SexTextBoxColor {
			get => _sexTextBoxColor;
			set {
				_sexTextBoxColor = value;
				OnPropertyChanged(nameof(SexTextBoxColor));
			}
		}
		public Brush BirthDateTextBoxColor {
			get => _birthDateTextBoxColor;
			set {
				_peselTextBoxColor = value;
				OnPropertyChanged(nameof(BirthDateTextBoxColor));
			}
		}

		//Walidacja pól
		public string Error => null;

		public string this[string columnName] {
			get {
				string result = null;
				if(columnName == "Name")
				if (!validate(Name)) result = "To pole nie może być puste";
				return result;
			}

		}

		private bool validate(string data) {
			return !string.IsNullOrEmpty(data) && !string.IsNullOrWhiteSpace(data);
		}
		private bool validateList(string data,List<string> list) {
			return !string.IsNullOrEmpty(data) && !string.IsNullOrWhiteSpace(data)
				&& list.Contains(data);
		}
		public AddPatientViewModel(dbm.Doctor doctor, DoctorViewModel parentViewModel) {	
			this.doctor = doctor;
			ShowPatientListCommand = new BasicCommand(obj => this.parentViewModel = parentViewModel);
			AddNewPatientCommand = new BasicCommand((object obj) =>
			{
				Console.WriteLine(BirthDate);
				var pv = PESELValidator.isValid("00000000000");
				Console.WriteLine(pv.ToString());
				if (!validate(Name)) NameTextBoxColor = Brushes.Red;
				if (!validate(Surname)) SurnameTextBoxColor = Brushes.Red;
				//if (!validate(Pesel)) NameTextBoxColor = Brushes.Red;
				if (!validateList(Sex,Sexes)) SexTextBoxColor = Brushes.Red;
				if (BirthDate.ToString() == "01.01.0001" ) BirthDateTextBoxColor = Brushes.Red;


				var doc = DbContext.Doctors.Where(doc => this.doctor.Id == doc.Id).First();
				if (validate(Name) && validate(Surname) && validateList(Sex, Sexes) && BirthDate != null)
				{
					Patient patient = new Patient();

					if (validate(Email)) {
						MailAddress tempMail;
						if (MailAddress.TryCreate(Email, out tempMail)) patient.Email = Email;
					}
					patient.Name = Name;
					patient.Surname = Surname;
					patient.SecondName = SecondName;
					patient.BirthDate = DateTime.Parse(BirthDate.ToString());
					patient.Sex = Sex;
					//patient.Pesel = Pesel;
					doctor.Patients.Add(patient);
					DbContext.SaveChanges();
				}

				
				//doc.Patients.Add(patient);
			});
		}
	}
}
