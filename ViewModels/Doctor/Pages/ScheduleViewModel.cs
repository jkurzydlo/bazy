﻿using bazy1.Models;
using bazy1.Repositories;
using Itenso.TimePeriod;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Asn1.Mozilla;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace bazy1.ViewModels.Doctor.Pages {
	public class ScheduleViewModel : ViewModelBase {
		private WorkhoursRepository workhoursRepository = new();
		private ObservableCollection<Workhour> _workhourList = new();
		private Dictionary<DateTime, List<TimeRange>> _hoursList = new();
		private ObservableCollection<Appointment> _appointments = new();
		private string _selectedAppointments;
		private Workhour _selectedWorkhour;
		private Models.Doctor doctor;

		public Workhour SelectedWorkhour {
			get => _selectedWorkhour;
			set {
				_selectedWorkhour = value;
				OnPropertyChanged(nameof(SelectedWorkhour));

				SelectedAppointments = "";
				if (SelectedWorkhour != null)
				{
					foreach (var app in AppointmentsList.Where(a => a.DoctorId == doctor.Id && SelectedWorkhour.BlockStart <= a.Date && SelectedWorkhour.BlockEnd >= a.Date))
						SelectedAppointments += "Termin: " + app.Date.Value.ToString("HH:mm") + "\nPacjent:" + app.Patient.Name + " " + app.Patient.Surname + "\n" + "Cel wizyty: " + app.Goal + "\n\n";
				}
			}
		}


		public string SelectedAppointments {
			get => _selectedAppointments;
			set {
				_selectedAppointments = value;
				OnPropertyChanged(nameof(SelectedAppointments));
			}
		}

		private void LoadWorkhours() {
			HoursList.Clear();
			var whStarts = DbContext.Workhours.Where(ws => ws.DoctorId == doctor.Id && ws.BlockStart.Value.Date >= SelectedDateStart.Date && ws.BlockEnd.Value.Date <= SelectedDateEnd).GroupBy(ws => ws.BlockStart).Select(g => g.First());
			var whEnds = DbContext.Workhours.Where(ws => ws.DoctorId == doctor.Id && ws.BlockStart.Value.Date >= SelectedDateStart.Date && ws.BlockEnd.Value.Date <= SelectedDateEnd).GroupBy(ws => ws.BlockEnd).Select(g => g.First());
			

			for (int i = 0; i < whStarts.Count(); i++)
			{
                //Console.WriteLine(whStarts.ElementAt(i));
				//Console.WriteLine(whStarts.ElementAt(i).BlockStart + "-" + whEnds.ElementAt(i).BlockEnd +"\n");
				if (HoursList.ContainsKey(whStarts.ElementAt(i).BlockStart.Value.Date))
				{
					HoursList[whStarts.ElementAt(i).BlockStart.Value.Date].Add(new() { Start = (DateTime)whStarts.ElementAt(i).BlockStart, End = (DateTime)whEnds.ElementAt(i).BlockEnd });
				}
				else
				{
					HoursList.Add(whEnds.ElementAt(i).BlockStart.Value.Date, new List<TimeRange>() { new() { Start = (DateTime)whStarts.ElementAt(i).BlockStart, End = (DateTime)whEnds.ElementAt(i).BlockEnd } });
					//Console.WriteLine(whStarts.Count() + " " + whEnds.Count());
				}
			}
			WorkhoursList.Clear();

			var cultureInfo = new System.Globalization.CultureInfo("pl-PL"); //polskie nazwy dni tygodnia

			foreach (var item in HoursList)
			{
				foreach (var item2 in item.Value)
				{
					WorkhoursList.Add(new Workhour() { BlockStart = item2.Start, BlockEnd = item2.End });
				}
			}
			Console.WriteLine("whs:" + HoursList.Count());
			WorkhoursList = new(WorkhoursList.OrderBy(w => w.BlockStart.Value));
        }

		private void LoadAppointments() {
			Console.WriteLine(SelectedDateEnd + "" + SelectedDateStart);
			AppointmentsList = new(DbContext.Appointments.FromSqlRaw($"select a.id, a.doctor_id,a.doctor_user_id, a.dateTo, a.goal, a.date, a.patient_id, p.name, p.surname from appointment a join patient p on a.patient_id=p.id where !p.deleted && a.doctor_id ={doctor.Id} && a.date between '{SelectedDateStart.ToString("yyyy-MM-dd HH:mm:ss")}' and '{SelectedDateEnd.ToString("yyyy-MM-dd HH:mm:ss")}'").Include("Patient").Include("Doctor"));
			Console.WriteLine("apl: " + AppointmentsList.Count());
        }

		public ObservableCollection<Appointment> AppointmentsList {
			get => _appointments;
			set {
				_appointments = value;
				OnPropertyChanged(nameof(AppointmentsList));
			}
		}

		public Dictionary<DateTime, List<TimeRange>> HoursList {
			get => _hoursList;
			set {
				_hoursList = value;
				OnPropertyChanged(nameof(HoursList));
			}
		}
		private DateTime _selectedDateStart = new DateTime( DateTime.Now.Year,DateTime.Now.Month,DateTime.Now.Day,0,0,0);
		private DateTime _selectedDateEnd = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 23, 59, 59);

		public DateTime SelectedDateEnd {
			get => _selectedDateEnd;
			set {
				Console.WriteLine(DbContext.Workhours.Where(w => w.DoctorId == doctor.Id).OrderBy(w => w.BlockEnd).Count());

				if (DbContext.Workhours.Where(w => w.DoctorId == doctor.Id).Count() > 0)
				{
                    if (value.Date <= DbContext.Workhours.Where(w => w.DoctorId == doctor.Id).OrderBy(w => w.BlockEnd).Last().End && value.Date >= SelectedDateStart.Date)
					{
						_selectedDateEnd = value;
						OnPropertyChanged(nameof(SelectedDateEnd));
						LoadWorkhours();
						LoadAppointments();
					}
					else _selectedDateEnd = DateTime.Now;
				}
			}
		}

		public DateTime SelectedDateStart {
			get => _selectedDateStart;
			set {
				if (DbContext.Workhours.Where(w => w.DoctorId == doctor.Id).Count() > 0)
				{
					if (value.Date >= DbContext.Workhours.Where(w => w.DoctorId == doctor.Id).OrderBy(w => w.BlockStart).First().Start && value.Date <= SelectedDateEnd.Date)
					{
						_selectedDateStart = value;
						OnPropertyChanged(nameof(SelectedDateStart));
						LoadWorkhours();
						LoadAppointments();
					}
					else _selectedDateStart = DateTime.Now;
				}
				

            }
		}

		public ObservableCollection<Workhour> WorkhoursList {
			get => _workhourList;
			set {
				_workhourList = value;
				OnPropertyChanged(nameof(WorkhoursList));
			}
		}

		public ScheduleViewModel(Models.Doctor doctor) {
			this.doctor = doctor;

        }
	}
}
