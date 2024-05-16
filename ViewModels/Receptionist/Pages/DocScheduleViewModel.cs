using bazy1.Models;
using bazy1.Views.Admin.Pages;
using Itenso.TimePeriod;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace bazy1.ViewModels.Receptionist.Pages
{
    public class DocScheduleViewModel : ViewModelBase
    {
        private Models.Doctor _selectedDoctor;
        public class WorkHourTemp {
			public DateTime end1 { get; set; }

			public DateTime start1{ get; set; }

            public string fullDate { get; set; }
        }
        public ObservableCollection<Models.Doctor> DoctorsList { get; set; } = new(DbContext.Doctors.ToList());
        private DateTime _selectedDate { get; set; } = DateTime.Now;
        private Dictionary<DateOnly, WorkHourTemp> _workhoursTemp = new();
		private ObservableCollection<WorkHourTemp> _hours = new();
		public ObservableCollection<WorkHourTemp>Hours {
			get => _hours;
			set { 
			_hours = value;
				OnPropertyChanged(nameof(Hours));
			}
		}
        private List<Workhour> workhours = new();

        public Models.Doctor SelectedDoctor {
            get => _selectedDoctor;
            set {
                _selectedDoctor = value;
                OnPropertyChanged(nameof(SelectedDoctor));

                Hours.Clear();

				var tempHours = DbContext.Workhours.Where(w => w.DoctorId == SelectedDoctor.Id && w.Start.Value.DayOfYear == SelectedDate.DayOfYear).ToList();
				for(int i = 0;i < tempHours.Count() -1;i++)
				{
                    Hours.Add(new WorkHourTemp { start1 = (DateTime)tempHours[i].BlockStart, end1 = (DateTime)tempHours[i].BlockEnd });
				}

			}
        }


		public ICommand Test { get; }
        public DateTime SelectedDate {
            get => _selectedDate;
            set {
                _selectedDate = value;
                OnPropertyChanged(nameof(SelectedDate));

				Hours.Clear();

				var tempHours = DbContext.Workhours.Where(w => w.DoctorId == SelectedDoctor.Id && w.Start.Value.DayOfYear == SelectedDate.DayOfYear).ToList();
				int index = 0;
				for (int i = 0; i < tempHours.Count(); i++)
				{
					//Hours.Add(new WorkHourTemp { s})
				}
			}
        }

		public Dictionary<DateOnly,WorkHourTemp> HoursList {
            get => _workhoursTemp;
			set {
                _workhoursTemp = value;
				OnPropertyChanged(nameof(HoursList));
			}
		}


		public DocScheduleViewModel() {
			SelectedDoctor = DoctorsList[0];
			Test = new BasicCommand((object obj) =>
			{
				var tempHours = DbContext.Workhours.Where(w => w.DoctorId == SelectedDoctor.Id && w.Start.Value.DayOfYear == SelectedDate.DayOfYear).ToList();
				for (int i = 0; i < tempHours.Count() - 1; i++)
				{


				}
			});
            //foreach (var item in tempW)
            // {
            //    Hours.Add(new WorkHourTemp { start1 = (DateTime)item.Start, end1 = (DateTime)item.End });
            /*
			if (!HoursList.ContainsKey(DateOnly.FromDateTime(SelectedDate))) {
				HoursList.Add(DateOnly.FromDateTime(SelectedDate), new WorkHourTemp { fullDate = item.Start + "-" + item.End });
					}
			else
			{
				HoursList[DateOnly.FromDateTime(SelectedDate)].fullDate += item.Start + "-" + item.End;
			}*/
            // }


        }

	}
}