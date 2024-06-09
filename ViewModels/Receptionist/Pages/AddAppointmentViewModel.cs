using bazy1.Models;
using Itenso.TimePeriod;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace bazy1.ViewModels.Receptionist.Pages {
	internal class AddAppointmentViewModel : ViewModelBase {
		private DateTime _selectedDate = DateTime.Now;
		private Workhour _selectedWorkhour;
		private ObservableCollection<Workhour> _workhours;
		public List<Models.Doctor> Doctors { get; set; }
		public List<Models.Patient> Patients { get; set; }
		private Models.Doctor _selectedDoctor = DbContext.Doctors.Where(doc => !doc.User.Deleted).First();
		private Models.Patient _selectedPatient = DbContext.Patients.First();
		public Dictionary<Workhour, System.Windows.Media.Brush> RowColor { get; set; }
		public System.Windows.Media.Brush RowColors { get; set; } = new SolidColorBrush(Colors.Green);
		private string _appointmentGoal;

		public string AppointmentGoal {
			get => _appointmentGoal;
			set {
				_appointmentGoal = value;
				OnPropertyChanged(nameof(AppointmentGoal));
			}
		}

		public Workhour SelectedWorkhour {
			get => _selectedWorkhour;
			set {
				_selectedWorkhour = value;
				OnPropertyChanged(nameof(SelectedWorkhour));
			}
		}

		public ObservableCollection<Workhour> Workhours {
			get => _workhours;
			set {
				_workhours = value;
				OnPropertyChanged(nameof(Workhours));
			}
		}

		public DateTime SelectedDate {
			get => _selectedDate;
			set {
				_selectedDate = value;
				if (_selectedDate.Date >= DateTime.Now.Date && SelectedDoctor != null)
				{
					var doc_id = new MySqlParameter("doc_id", SelectedDoctor.UserId);
					var day_of_year = new MySqlParameter("day_of_year", SelectedDate.DayOfYear);


					var test = DbContext.Workhours.FromSqlRaw($"select * from przychodnia9.workhours where user_id = @doc_id", doc_id).ToList();
					test = test.Where(w => w.Start.Value.DayOfYear == SelectedDate.DayOfYear).ToList();
					Workhours = new(test);

					foreach (var i in test)
					{
						Console.WriteLine(i.Start + " " + i.Open + " " + i.Start.Value.DayOfYear);
					}

				}
				else _selectedDate = DateTime.Now;
				OnPropertyChanged(nameof(SelectedDate));

			}
		}

		public Models.Doctor SelectedDoctor {
			get => _selectedDoctor;
			set {
				if (value != null)
				{
					_selectedDoctor = value;
					OnPropertyChanged(nameof(SelectedDoctor));
					Workhours = new(DbContext.Workhours.Where(wh => wh.UserId == SelectedDoctor.UserId).Where(w => w.Start.Value.DayOfYear == SelectedDate.DayOfYear));

					foreach (var item in DbContext.Workhours.Where(wh => wh.UserId == SelectedDoctor.UserId).ToList())
					{
						if ((bool)item.Open) { SelectedDate = item.BlockStart.Value.Date; break; }

					}

				}
			}
		}

		public Models.Patient SelectedPatient {
			get => _selectedPatient;
			set {
				_selectedPatient = value;
				OnPropertyChanged(nameof(SelectedPatient));
			}
		}

		public ICommand AddAppointmentCommand { get; set; }

		public AddAppointmentViewModel(ViewModelBase parentViewModel, Patient patient) {
			SelectedDoctor = DbContext.Doctors.Where(d=>!d.User.Deleted).ElementAt(0);
            Console.WriteLine("ldsgsd");
            AddAppointmentCommand = new BasicCommand((object obj) =>
			{
			if (SelectedWorkhour != null && Workhours.Count != 0 && SelectedPatient != null && SelectedDoctor != null && (bool)Workhours.Where(w=> w.Id == SelectedWorkhour.Id).First().Open)
				{
					if (!DbContext.Doctors.Where(d=>!d.User.Deleted).Include("Patients").Where(d => d.Id == SelectedDoctor.Id).First().Patients.Contains(patient))
						DbContext.Database.ExecuteSql($"insert into doctor_has_patient values({SelectedDoctor.Id},{SelectedDoctor.UserId},{patient.Id})");
					DbContext.Database.ExecuteSqlRaw($"update workhours set open = false where user_id={SelectedDoctor.UserId} && id={SelectedWorkhour.Id}");
					SelectedDoctor.Appointments.Add(new Appointment { Date = SelectedWorkhour.Start, Patient = patient , Goal = AppointmentGoal });
					//SelectedPatient.Appointments.Where(a => a.Date == SelectedWorkhour.Start).First().Notifications.Add(new Notification() { });
					DbContext.Workhours.Where(w => w.UserId== SelectedDoctor.UserId && w.Id == SelectedWorkhour.Id).First().Open = false;
					DbContext.SaveChanges();
					SelectedDate = SelectedDate;
				}
				else System.Windows.MessageBox.Show($"Wystąpił błąd podczas dodawania wizyty", "Błąd", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);


			});
			Doctors = DbContext.Doctors.Where(d=>!d.User.Deleted).ToList();
			Patients = DbContext.Patients.ToList();

			var doc_id = new MySqlParameter("doc_id", SelectedDoctor.UserId);
			var day_of_year = new MySqlParameter("day_of_year", SelectedDate.DayOfYear);


			var test = DbContext.Workhours.FromSqlRaw($"select * from przychodnia9.workhours where user_id = @doc_id", doc_id).ToList();
			test = test.Where(w => w.Start.Value.DayOfYear == SelectedDate.DayOfYear).ToList();
			Workhours = new(test);
            foreach (var item in test)
            {
				Console.WriteLine(item.Start + " " + item.End + " " + item.Open);
            }
            SelectedDate = SelectedDate;
			System.Windows.Input.CommandManager.InvalidateRequerySuggested();


			/*
			var wh = DbContext.Workhours.Where(wh => wh.DoctorId == SelectedDoctor.Id).Where(w => w.Start.Value.DayOfYear == SelectedDate.DayOfYear);
			Console.WriteLine("zs: " + wh.Count());

			var docWorkhours = new List<TimeRange>();
			for (int i = 0; i < 365; i++)
			{
				docWorkhours.Add(new(new DateTime(2024, 5, 9, 8, 0, 0).AddDays(i),
					new DateTime(2024, 5, 9, 12, 0, 0).AddDays(i)));
				docWorkhours.Add(new(new DateTime(2024, 5, 9, 13, 0, 0).AddDays(i),
		new DateTime(2024, 5, 9, 19, 0, 0).AddDays(i)));
			}


			var slots = new HashSet<TimeRange>();

			Models.Doctor d1 = DbContext.Doctors.Where(doc => doc.Id == SelectedDoctor.Id).First();
			for (int i = 0; i < 48 * 365; i++)
			{
				var start = DateTime.Now.Date.AddHours(i / 2);
				var end = start.AddHours(0.5);
				// Console.WriteLine("lp:"+start+" "+end);
				foreach (var d in docWorkhours)
				{
					if (d.HasInside(new TimeRange(start, end)) || d.GetRelation(new TimeRange(start, end)) == PeriodRelation.EndInside || d.GetRelation(new TimeRange(start, end)) == PeriodRelation.StartTouching || d.GetRelation(new TimeRange(start, end)) == PeriodRelation.InsideEndTouching)
						slots.Add(new TimeRange(start, end));

				}

			}




			foreach (var a in slots)
			{
				//Console.WriteLine("slots: "+a.Start+"->"+a.End);
				//d1.Workhours.Add(new Workhour { Start = a.Start, End = a.End, Open = true });
			}

			//12:30 - 15;
			//18 - 23;
			//13 - 15;
			//d1.Appointments.Add(new Appointment { Date = })
			*/
		}
	}
}
