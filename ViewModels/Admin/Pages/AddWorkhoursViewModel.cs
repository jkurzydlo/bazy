using bazy1.Models;
using Itenso.TimePeriod;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace bazy1.ViewModels.Admin.Pages {
	internal class AddWorkhoursViewModel : ViewModelBase {
		private DateTime _selectedDate = DateTime.Now;
		private Workhour _selectedWorkhour;
		private ObservableCollection<Workhour> _workhours;
		public List<User> Users { get; set; }
		public List<Models.Patient> Patients { get; set; }
		private User _selectedUser = DbContext.Users.First();
		private Models.Patient _selectedPatient = DbContext.Patients.First();
		public Dictionary<Workhour, System.Drawing.Brush> RowColor { get; set; }
		public System.Windows.Media.Brush RowColors { get; set; } = new SolidColorBrush(Colors.Green);
		private string _appointmentGoal;

		public Workhour SelectedWorkhour {
			get => _selectedWorkhour;
			set {
				_selectedWorkhour = value;
				OnPropertyChanged(nameof(SelectedWorkhour));
			}
		}

		public string AppointmentGoal{
			get => _appointmentGoal;
			set {
				_appointmentGoal = value;
				OnPropertyChanged(nameof(AppointmentGoal));
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
				if (_selectedDate.Date >= DateTime.Now.Date)
				{
					
					OnPropertyChanged(nameof(SelectedDate));
					Workhours = new(DbContext.Workhours.Where(wh => wh.UserId == SelectedUser.Id).Where(w => w.Start.Value.DayOfYear == SelectedDate.DayOfYear));
				}else _selectedDate = DateTime.Now;
			}
		}

		public User SelectedUser {
			get => _selectedUser;
			set {
				_selectedUser= value;
				OnPropertyChanged(nameof(SelectedUser));
				Workhours = new(DbContext.Workhours.Where(wh => wh.UserId == SelectedUser.Id).Where(w => w.Start.Value.DayOfYear == SelectedDate.DayOfYear));
			}
		}

		public Models.Patient SelectedPatient {
			get => _selectedPatient;
			set {
				_selectedPatient= value;
				OnPropertyChanged(nameof(SelectedPatient));
			}
		}

		public ICommand AddAppointmentCommand{ get; set; }
		
		public AddWorkhoursViewModel(Patient patient) {
			SelectedPatient = patient;
			AddAppointmentCommand = new BasicCommand((object obj) =>
			{
				if (SelectedWorkhour != null && Workhours.Count != 0 && SelectedPatient != null && SelectedUser != null && (bool)Workhours.Where(w => w.Id == SelectedWorkhour.Id).First().Open)
				{
					if (!DbContext.Doctors.Where(d => !d.User.Deleted).Include("Patients").Where(d => d.UserId == SelectedUser.Id).First().Patients.Contains(SelectedPatient))
						DbContext.Database.ExecuteSql($"insert into doctor_has_Patient values( (select id from doctor where user_id = {SelectedUser.Id}),{SelectedUser.Id},{SelectedPatient.Id})");
					DbContext.Database.ExecuteSqlRaw($"update workhours set open = false where user_id={SelectedUser.Id} && id={SelectedWorkhour.Id}");
					DbContext.Doctors.Where(d => d.UserId == SelectedUser.Id).First().Appointments.Add(new Appointment { Date = SelectedWorkhour.Start, Patient = SelectedPatient, Goal = AppointmentGoal });
					//SelectedSelectedPatient.Appointments.Where(a => a.Date == SelectedWorkhour.Start).First().Notifications.Add(new Notification() { });
					DbContext.Workhours.Where(w => w.UserId == SelectedUser.Id && w.Id == SelectedWorkhour.Id).First().Open = false;
					DbContext.SaveChanges();
					SelectedDate = SelectedDate;
				}
				else System.Windows.MessageBox.Show($"Wystąpił błąd podczas dodawania wizyty", "Błąd", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);


			});



			Users = DbContext.Users.Where(u=> !u.Deleted).ToList();
			Patients = DbContext.Patients.Where(p=>!p.Deleted).ToList();
			Workhours = new(DbContext.Workhours.Where(wh => wh.UserId == SelectedUser.Id).Where(w => w.Start.Value.DayOfYear == SelectedDate.DayOfYear));


			var wh = DbContext.Workhours.Where(wh => wh.UserId == SelectedUser.Id).Where(w => w.Start.Value.DayOfYear == SelectedDate.DayOfYear);
            Console.WriteLine("zs: "+wh.Count());

            var docWorkhours = new List<TimeRange>();
			for (int i = 0; i < 365; i++)
			{
				docWorkhours.Add(new(new DateTime(2024, 5, 9, 8, 0, 0).AddDays(i),
					new DateTime(2024, 5, 9, 12, 0, 0).AddDays(i)));
				docWorkhours.Add(new(new DateTime(2024, 5, 9, 13, 0, 0).AddDays(i),
		new DateTime(2024, 5, 9, 19, 0, 0).AddDays(i)));
			}


			var slots = new HashSet<TimeRange>();

			Models.Doctor d1 = DbContext.Doctors.Where(doc => doc.UserId == SelectedUser.Id).First();
			for(int i = 0; i < 48*365; i++)
			{
				var start = DateTime.Now.Date.AddHours(i / 2);
				var end = start.AddHours(0.5);
               // Console.WriteLine("lp:"+start+" "+end);
                foreach (var d in docWorkhours)
				{
					if (d.HasInside(new TimeRange(start, end)) || d.GetRelation(new TimeRange(start,end)) == PeriodRelation.EndInside || d.GetRelation(new TimeRange(start, end)) == PeriodRelation.StartTouching || d.GetRelation(new TimeRange(start, end)) == PeriodRelation.InsideEndTouching)
					slots.Add(new TimeRange(start,end));

				}

			}


			 
			
			foreach(var a in slots)
			{
                //Console.WriteLine("slots: "+a.Start+"->"+a.End);
                //d1.Workhours.Add(new Workhour { Start = a.Start, End = a.End, Open = true });
            }

			//12:30 - 15;
			//18 - 23;
			//13 - 15;
			//d1.Appointments.Add(new Appointment { Date = })
			
			DbContext.SaveChanges();
		}
	}
}
