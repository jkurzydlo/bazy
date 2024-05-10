using bazy1.Models;
using Itenso.TimePeriod;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace bazy1.ViewModels.Admin.Pages
{
    public class WorkhoursViewModel : ViewModelBase {

        public class WorkHour {
            public string weekday { get; set; }
            public DateTime start1 { get; set; }
            public DateTime end1 { get; set; }

			public int start2 { get; set; }
			public int end2 { get; set; }
		}

        private ObservableCollection<Models.Doctor> _doctors;
        private Models.Doctor _selectedDoctor;
        private List<string> _weekdays = ["Poniedziałek", "Wtorek", "Środa", "Czwartek", "Piątek", "Sobota", "Niedziela"];
        private ObservableCollection<WorkHour> _HoursListMonday = [];
		private ObservableCollection<WorkHour> _HoursListTuesday = [];
		private ObservableCollection<WorkHour> _HoursListWednesday = [];
		private ObservableCollection<WorkHour> _HoursListThursday = [];
		private ObservableCollection<WorkHour> _HoursListFriday = [];
		private ObservableCollection<WorkHour> _HoursListSaturday = [];
		private ObservableCollection<WorkHour> _HoursListSunday = [];



		private Dictionary<string, List<WorkHour>> ws = [];
        private string _selectedWeekday;

        public ICommand RemoveHourMonday { get; set; }
		public ICommand RemoveHourTuesday { get; set; }

		public ICommand RemoveHourWednesday { get; set; }

		public ICommand RemoveHourThursday { get; set; }
		public ICommand RemoveHourFriday { get; set; }
		public ICommand RemoveHourSaturday { get; set; }
		public ICommand RemoveHourSunday { get; set; }

		public ObservableCollection<WorkHour> HoursListMonday {
            get => _HoursListMonday;
            set {
                _HoursListMonday = value;
                OnPropertyChanged(nameof(HoursListMonday));
            }
        }
		public ObservableCollection<WorkHour> HoursListTuesday {
			get => _HoursListTuesday;
			set {
				_HoursListTuesday = value;
				OnPropertyChanged(nameof(HoursListTuesday));
			}
		}
		public ObservableCollection<WorkHour> HoursListWednesday {
			get => _HoursListWednesday;
			set {
				_HoursListWednesday = value;
				OnPropertyChanged(nameof(HoursListWednesday));
			}
		}
		public ObservableCollection<WorkHour> HoursListThursday {
			get => _HoursListThursday;
			set {
				_HoursListThursday = value;
				OnPropertyChanged(nameof(HoursListThursday));
			}
		}

		public ObservableCollection<WorkHour> HoursListFriday {
			get => _HoursListFriday;
			set {
				_HoursListFriday = value;
				OnPropertyChanged(nameof(HoursListFriday));
			}
		}
		public ObservableCollection<WorkHour> HoursListSaturday {
			get => _HoursListSaturday;
			set {
				_HoursListSaturday= value;
				OnPropertyChanged(nameof(HoursListSaturday));
			}
		}
		public ObservableCollection<WorkHour> HoursListSunday {
			get => _HoursListSunday;
			set {
				_HoursListSunday= value;
				OnPropertyChanged(nameof(HoursListSunday));
			}
		}

		public ObservableCollection<Models.Doctor> Doctors {
            get => _doctors;
            set {
                _doctors = value;
                OnPropertyChanged(nameof(Doctors));
            }
        }

		public List<string>Weekdays {
            get => _weekdays;
			set {
				_weekdays = value;
				OnPropertyChanged(nameof(Weekdays));
			}
		}

		public string SelectedWeekday {
            get => _selectedWeekday;
			set {
				_selectedWeekday = value;
				OnPropertyChanged(nameof(SelectedWeekday));

			}
		}

		public Models.Doctor SelectedDoctor {
            get => _selectedDoctor;
            set {
                _selectedDoctor = value;
                OnPropertyChanged(nameof(SelectedDoctor));
            }
        }
		public ICommand Save { get; set; }
        public ICommand AddHourMonday { get; set; }
		public ICommand AddHourTuesday { get; set; }
		public ICommand AddHourWednesday { get; set; }
		public ICommand AddHourThursday { get; set; }
		public ICommand AddHourFriday { get; set; }
		public ICommand AddHourSaturday { get; set; }
		public ICommand AddHourSunday { get; set; }

		public WorkhoursViewModel() {
			Doctors = new(DbContext.Doctors);

			Save = new BasicCommand((object obj) =>
			{
				DbContext.Database.ExecuteSqlRaw($"delete from workhours where doctor_id={SelectedDoctor.Id}");
				DbContext.SaveChanges();

				var docWorkhours = new List<TimeRange>();
				for (int i = 0; i < 365; i++)
				{
                    /*	
                        docWorkhours.Add(new(new DateTime(2024, 5, 9, 8, 0, 0).AddDays(7*i),
                            new DateTime(2024, 5, 9, 16, 0, 0).AddDays(7*i)));
                        docWorkhours.Add(new(new DateTime(2024, 5, 9, 17, 0, 0).AddDays(7*i),
                new DateTime(2024, 5, 9, 19, 0, 0).AddDays(7*i)));*/

                    Console.WriteLine(HoursListMonday.Count);
                    foreach (var a in HoursListMonday)
					{
						Console.WriteLine(a.start1.Hour + "->" + a.end1.Hour);

						docWorkhours.Add(new(new DateTime(2024, 5, 6, a.start1.Hour, a.start1.Minute, 0).AddDays(7*i),
							new DateTime(2024, 5, 6,a.end1.Hour, a.end1.Minute, 0).AddDays(7*i)));
					}
					foreach (var a in HoursListTuesday)
					{
						Console.WriteLine(a.start1.Hour + "->" + a.end1.Hour);

						docWorkhours.Add(new(new DateTime(2024, 5, 7, a.start1.Hour, a.start1.Minute, 0).AddDays(7 * i),
							new DateTime(2024, 5, 7, a.end1.Hour, a.end1.Minute, 0).AddDays(7 * i)));
					}
					
					foreach (var a in HoursListWednesday)
					{
						Console.WriteLine(a.start1.Hour + "->" + a.end1.Hour);

						docWorkhours.Add(new(new DateTime(2024, 5, 8, a.start1.Hour, a.start1.Minute, 0).AddDays(7 * i),
							new DateTime(2024, 5, 8, a.end1.Hour, a.end1.Minute, 0).AddDays(7 * i)));
					}
					
					foreach (var a in HoursListThursday)
					{
						Console.WriteLine(a.start1.Hour + "->" + a.end1.Hour);

						docWorkhours.Add(new(new DateTime(2024, 5, 9, a.start1.Hour, a.start1.Minute, 0).AddDays(7 * i),
							new DateTime(2024, 5, 9, a.end1.Hour, a.end1.Minute, 0).AddDays(7 * i)));
					}
					
					
					foreach (var a in HoursListFriday)
					{
						Console.WriteLine(a.start1.Hour + "->" + a.end1.Hour);

						docWorkhours.Add(new(new DateTime(2024, 5, 10, a.start1.Hour, a.start1.Minute, 0).AddDays(7 * i),
							new DateTime(2024, 5, 10, a.end1.Hour, a.end1.Minute, 0).AddDays(7 * i)));
					}
					foreach (var a in HoursListSaturday)
					{
						Console.WriteLine(a.start1.Hour + "->" + a.end1.Hour);

						docWorkhours.Add(new(new DateTime(2024, 5, 11, a.start1.Hour, a.start1.Minute, 0).AddDays(7 * i),
							new DateTime(2024, 5, 11, a.end1.Hour, a.end1.Minute, 0).AddDays(7 * i)));
					}
					foreach (var a in HoursListSunday)
					{
						Console.WriteLine(a.start1.Hour + "->" + a.end1.Hour);

						docWorkhours.Add(new(new DateTime(2024, 5, 12, a.start1.Hour, a.start1.Minute, 0).AddDays(7 * i),
							new DateTime(2024, 5, 12, a.end1.Hour, a.end1.Minute, 0).AddDays(7 * i)));
					}

				}


				var slots = new HashSet<TimeRange>();

				Models.Doctor d1 = DbContext.Doctors.Where(doc => doc.Id == 22).First();
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
					d1.Workhours.Add(new Workhour { Start = a.Start, End = a.End, Open = true });
				}

				//12:30 - 15;
				//18 - 23;
				//13 - 15;
				//d1.Appointments.Add(new Appointment { Date = })

				DbContext.SaveChanges();
			});
            RemoveHourMonday = new BasicCommand((object obj) =>
            {
				HoursListMonday.Remove(HoursListMonday.Where(w => w.start1 == (DateTime)obj).First());
			});
			RemoveHourTuesday = new BasicCommand((object obj) =>
			{
				HoursListTuesday.Remove(HoursListTuesday.Where(w => w.start1 == (DateTime)obj).First());
			});
			RemoveHourWednesday = new BasicCommand((object obj) =>
			{
				HoursListWednesday.Remove(HoursListWednesday.Where(w => w.start1 == (DateTime)obj).First());
			});
			RemoveHourThursday = new BasicCommand((object obj) =>
			{
				HoursListThursday.Remove(HoursListThursday.Where(w => w.start1 == (DateTime)obj).First());
			});
			RemoveHourFriday = new BasicCommand((object obj) =>
			{
				HoursListFriday.Remove(HoursListFriday.Where(w => w.start1 == (DateTime)obj).First());
			});
			RemoveHourSaturday = new BasicCommand((object obj) =>
			{
				HoursListSaturday.Remove(HoursListSaturday.Where(w => w.start1 == (DateTime)obj).First());
			});
			RemoveHourSunday = new BasicCommand((object obj) =>
			{
				HoursListSunday.Remove(HoursListSunday.Where(w => w.start1 == (DateTime)obj).First());
			});

			AddHourMonday = new BasicCommand((object obj) =>
            {
					HoursListMonday.Add(new WorkHour {  weekday = "Poniedziałek" });
				Console.WriteLine("---");
				foreach (var hour in HoursListMonday)
				{
                    
                    //Console.WriteLine(hour.start1.Hour+"->"+hour.end1.Hour);
                }
				Console.WriteLine("---");
			});
			AddHourTuesday = new BasicCommand((object obj) =>
			{
				HoursListTuesday.Add(new WorkHour { weekday = "Wtorek" });
			});
			AddHourWednesday = new BasicCommand((object obj) =>
			{
				HoursListWednesday.Add(new WorkHour { weekday = "Środa" });
			});
			AddHourThursday = new BasicCommand((object obj) =>
			{
				HoursListThursday.Add(new WorkHour { weekday = "Czwartek" });
			});
			AddHourFriday= new BasicCommand((object obj) =>
			{
				HoursListFriday.Add(new WorkHour { weekday = "Piątek" });
			});
			AddHourSaturday = new BasicCommand((object obj) =>
			{
				HoursListSaturday.Add(new WorkHour { weekday = "Sobota" });
			});
			AddHourSunday = new BasicCommand((object obj) =>
			{
				HoursListSunday.Add(new WorkHour { weekday = "Niedziela" });
			});
		}
    }
}
