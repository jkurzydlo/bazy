using bazy1.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Windows.Input;

namespace bazy1.ViewModels.Receptionist.Pages {
	public class EditPatientViewModel : ViewModelBase {
		private Patient _selectedPatient;
		private Address _selectedAddress;
		//private readonly Przychodnia9Context DbContext;
		private PatientListViewModel  _adminPatientListViewModel;

		private List<string> _addressType = new List<string> { "Zamieszkania", "Zameldowania" };
		public List<string> AddressType {
			get => _addressType;
			set {
				_addressType = value;
				OnPropertyChanged(nameof(AddressType));
			}
		}

		public EditPatientViewModel(PatientListViewModel adminPatientListViewModel, Patient patient) {
			_adminPatientListViewModel = adminPatientListViewModel;
			//DbContext = new Przychodnia9Context();
			SelectedPatient = patient;
			Addresses = new ObservableCollection<Address>(DbContext.Addresses.Where(a => a.Patients.Contains(_selectedPatient)).ToList());
			SavePatientCommand = new BasicCommand(SavePatient);
			AddNewAddressCommand = new BasicCommand(AddNewAddress);
			RemoveAddressCommand = new BasicCommand(RemoveAddress, CanRemoveAddress);
		}

		public Patient SelectedPatient {
			get => _selectedPatient;
			set {
				_selectedPatient = value;
				OnPropertyChanged(nameof(SelectedPatient));
			}
		}

		private ObservableCollection<Address> _addresses = new();

		public ObservableCollection<Address> Addresses{
			get => _addresses;
			set {
				_addresses = value;
				OnPropertyChanged(nameof(Addresses));
			}
		}

		public Address SelectedAddress {
			get => _selectedAddress;
			set {
				_selectedAddress = value;
				OnPropertyChanged(nameof(SelectedAddress));
				((BasicCommand)RemoveAddressCommand).RaiseCanExecuteChanged();

			}
		}

		public ICommand SavePatientCommand { get; }
		public ICommand AddNewAddressCommand { get; }
		public ICommand RemoveAddressCommand { get; }

		private void AddNewAddress(object obj) {
			var newAddress = new Address();
			Addresses.Add(newAddress);
			SelectedAddress = newAddress;
		}

		private bool CanRemoveAddress(object obj) {
			return Addresses.Count > 1 && SelectedAddress != null;
		}

		private void RemoveAddress(object obj) {
			if (SelectedAddress != null)
			{
				DbContext.Database.ExecuteSqlRaw($"delete from patient_address where patient_id ={SelectedPatient.Id} && address_id={SelectedAddress.Id}");
				Addresses = new ObservableCollection<Address>(DbContext.Addresses.Where(a => a.Patients.Contains(_selectedPatient)).ToList());

				SelectedAddress = Addresses.FirstOrDefault();
			}
		}

		private void SavePatient(object obj) {
			if (SelectedPatient == null)
			{
				System.Windows.MessageBox.Show("Nie wybrano pacjenta do edycji.");
				return;
			}

			// Walidacja imienia
			if (string.IsNullOrEmpty(SelectedPatient.Name) || string.IsNullOrWhiteSpace(SelectedPatient.Name))
			{
				System.Windows.MessageBox.Show("Imię nie może być puste.");
				return;
			}

			// Walidacja nazwiska
			if (string.IsNullOrEmpty(SelectedPatient.Surname) || string.IsNullOrWhiteSpace(SelectedPatient.Surname))
			{
				System.Windows.MessageBox.Show("Nazwisko nie może być puste.");
				return;
			}

			// Walidacja PESEL (przykładowa walidacja długości, należy dostosować do rzeczywistych wymagań)
			if (string.IsNullOrEmpty(SelectedPatient.Pesel) || !Regex.IsMatch(SelectedPatient.Pesel, @"^\d{11}$"))
			{
				System.Windows.MessageBox.Show("PESEL musi składać się z 11 cyfr.");
				return;
			}

			// Walidacja numeru telefonu (jeśli nie jest pusty)
			if (!string.IsNullOrEmpty(SelectedPatient.PhoneNumber))
			{
				if (!Regex.IsMatch(SelectedPatient.PhoneNumber, @"^\+?\d{9,15}$"))
				{
					System.Windows.MessageBox.Show("Numer telefonu jest nieprawidłowy.");
					return;
				}
			}

			// Walidacja adresu email (jeśli nie jest pusty)
			if (!string.IsNullOrEmpty(SelectedPatient.Email))
			{
				try
				{
					var mail = new MailAddress(SelectedPatient.Email);
				}
				catch (FormatException)
				{
					System.Windows.MessageBox.Show("Podany adres email jest nieprawidłowy.");
					return;
				}
			}

			// Walidacja adresów
			foreach (var address in Addresses)
			{

				if (string.IsNullOrEmpty(address.City) || string.IsNullOrWhiteSpace(address.City))
				{
					System.Windows.MessageBox.Show("Miasto nie może być puste.");
					return;
				}

				if (string.IsNullOrEmpty(address.BuildingNumber) || string.IsNullOrWhiteSpace(address.BuildingNumber))
				{
					System.Windows.MessageBox.Show("Numer budynku nie może być pusty.");
					return;
				}

				if (string.IsNullOrEmpty(address.PostalCode) || !Regex.IsMatch(address.PostalCode, @"^\d{2}-\d{3}$"))
				{
					System.Windows.MessageBox.Show("Kod pocztowy musi być w formacie XX-XXX.");
					return;
				}

				if (string.IsNullOrEmpty(address.Type) || string.IsNullOrWhiteSpace(address.Type))
				{
					System.Windows.MessageBox.Show("Typ adresu nie może być pusty.");
					return;
				}
			}

			var existingPatient = DbContext.Patients.FirstOrDefault(p => p.Id == SelectedPatient.Id);
			if (existingPatient != null)
			{
				existingPatient.Name = SelectedPatient.Name;
				existingPatient.Surname = SelectedPatient.Surname;
				existingPatient.Pesel = SelectedPatient.Pesel;
				existingPatient.PhoneNumber = SelectedPatient.PhoneNumber;
				existingPatient.Email = SelectedPatient.Email;

				// Aktualizacja adresów
				var existingAddresses = DbContext.Addresses.Where(a => a.Patients.Contains(_selectedPatient)).ToList();
				foreach (var address in Addresses)
				{
					if (!existingAddresses.Contains(address))
					{
						DbContext.Addresses.Add(address);
						existingPatient.Addresses.Add(address);
					}
				}

				try
				{
					DbContext.SaveChanges();
					System.Windows.MessageBox.Show("Zmiany zostały zapisane pomyślnie.");
				}
				catch (Exception ex)
				{
					System.Windows.MessageBox.Show($"Wystąpił błąd podczas zapisywania zmian: {ex.Message}");
				}
				// Powrót do listy pacjentów po zapisaniu
				//_adminPatientListViewModel.CurrentViewModel = new AdminPatientListViewModel(_adminPatientListViewModel);
			}
			else
			{
				System.Windows.MessageBox.Show("Nie znaleziono pacjenta do aktualizacji.");
			}
		}
	}
}


