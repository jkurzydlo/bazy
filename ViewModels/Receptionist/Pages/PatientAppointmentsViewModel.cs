﻿using bazy1.Models;
using bazy1.Repositories;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace bazy1.ViewModels.Receptionist.Pages {
	internal class PatientAppointmentsViewModel : ViewModelBase {
		private ObservableCollection<Patient> _patients = new();
		private Patient _selectedPatient;
		private ObservableCollection<Appointment> _appointments = new();
		private Appointment _selectedAppointment;
		private Workhour _selectedNewDate;

		public Workhour SelectedNewDate {
			get => _selectedNewDate;
			set {
				_selectedNewDate = value;
				OnPropertyChanged(nameof(SelectedNewDate));
			}
		}

		private ObservableCollection<Workhour> _appointmentSchedule = new();
		public ObservableCollection<Workhour> AppointmentsSchedule {
			get => _appointmentSchedule;
			set {
				_appointmentSchedule = value;
				OnPropertyChanged(nameof(AppointmentsSchedule));
			}
		}

		private Visibility _appointmentsScheduleVisible = Visibility.Hidden;
		private DateTime _selectedDate = DateTime.Now;
		public DateTime SelectedDate {
			get => _selectedDate;
			set {
				_selectedDate = value;
				OnPropertyChanged(nameof(SelectedDate));

				var doc_id = new MySqlParameter("doc_id", SelectedAppointment.Doctor.Id);
				var test = DbContext.Workhours.FromSqlRaw($"select * from przychodnia9.workhours where doctor_id = @doc_id", doc_id).ToList();
				test = test.Where(w => w.Start.Value.DayOfYear == SelectedDate.DayOfYear).ToList();
				AppointmentsSchedule = new(test);
			}
		}

		public Visibility AppointmentScheduleVisible {
			get => _appointmentsScheduleVisible;
			set {
				_appointmentsScheduleVisible = value;
				OnPropertyChanged(nameof(AppointmentScheduleVisible));
			}
		}

		AppointmentRepository appointmentRepository = new();
		PatientRepository patientRepository = new();

		public Appointment SelectedAppointment {
			get => _selectedAppointment;
			set {
				_selectedAppointment = value;
				OnPropertyChanged(nameof(SelectedAppointment));

				var doc_id = new MySqlParameter("doc_id", SelectedAppointment.Doctor.Id);
				var test = DbContext.Workhours.Where(w => w.Id == SelectedAppointment.DoctorId);
				test = test.Where(w => w.Start.Value.DayOfYear == SelectedDate.DayOfYear);
				AppointmentsSchedule = new(test);
                foreach (var item in test)
                {
                    Console.WriteLine(item.Start+" "+item.Open);
                }
				DbContext.SaveChanges();

            }
		}

		public Patient SelectedPatient {
			get => _selectedPatient;
			set {
				_selectedPatient = value;
				OnPropertyChanged(nameof(SelectedPatient));
				AppointmentsList = new(appointmentRepository.GetAppointmentsByPatientId(SelectedPatient.Id));

			}

		}
		public ObservableCollection<Patient> PatientList {
			get => _patients;
			set {
				_patients = value;
				OnPropertyChanged(nameof(PatientList));

			}
		}

		public ObservableCollection<Appointment> AppointmentsList {
			get => _appointments;
			set {
				_appointments = value;
				OnPropertyChanged(nameof(AppointmentsList));
			}
		}

		public ICommand RemoveAppointmentCommand { get; set; }
		public ICommand UpdateAppointmentCommand { get; set; }
		public ICommand ShowAppointmentsSchedule { get;set; }
		public ICommand SaveChangesCommand { get; set; }

		public PatientAppointmentsViewModel(ReceptionistViewModel viewModel, Patient patient) {
			
			SaveChangesCommand = new BasicCommand((object obj) =>
			{

				DbContext.Database.ExecuteSqlRaw($"update workhours set open = true where doctor_id={SelectedAppointment.DoctorId} && start='{SelectedAppointment.Date.Value.ToString("yyyy-MM-dd HH:mm:ss")}'");
				DbContext.Database.ExecuteSqlRaw($"update appointment set date='{SelectedNewDate.Start.Value.ToString("yyyy-MM-dd HH:mm:ss")}' where id={SelectedAppointment.Id}");
				DbContext.Database.ExecuteSqlRaw($"update workhours set open = false where doctor_id={SelectedAppointment.DoctorId} && start='{SelectedNewDate.Start.Value.ToString("yyyy-MM-dd HH:mm:ss")}'");
                
				foreach (var item in AppointmentsSchedule)
                {
					DbContext.Entry(item).Reload();
                }
                viewModel.CurrentViewModel = new PatientAppointmentsViewModel(viewModel,patient);
			});
			PatientList = new(patientRepository.GetAll());
			AppointmentsList = new(appointmentRepository.GetAppointmentsByPatientId(patient.Id));

			UpdateAppointmentCommand = new BasicCommand((object obj) =>
			{
				AppointmentScheduleVisible = Visibility.Visible;
			});
			RemoveAppointmentCommand = new BasicCommand((object obj) =>
			{
				if (SelectedAppointment != null)
				{
					Console.WriteLine(SelectedAppointment.Date.Value.ToString("yyyy-MM-dd HH:mm:ss"));
					var ok = DbContext.Database.ExecuteSqlRaw($"update workhours set open = true where doctor_id={SelectedAppointment.DoctorId} && start='{SelectedAppointment.Date.Value.ToString("yyyy-MM-dd HH:mm:ss") }'");
					appointmentRepository.RemoveAppointmentById(SelectedAppointment.Id);

					foreach (var item in AppointmentsSchedule)
					{
						DbContext.Entry(item).Reload();
					}
					viewModel.CurrentViewModel = new PatientAppointmentsViewModel(viewModel, patient);

				}
			});
        }
	}
}
