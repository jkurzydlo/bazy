using bazy1.Models;
using bazy1.Repositories;
using Google.Protobuf.WellKnownTypes;
using Itenso.TimePeriod;
using Microsoft.EntityFrameworkCore;
using Microsoft.Xaml.Behaviors.Media;
using Org.BouncyCastle.Asn1.Cmp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

namespace bazy1.ViewModels.Admin.Pages
{
    public class UpdateScheduleViewModel : ViewModelBase
    {
		public class WorkHour {
			public DateTime start1 { get; set; }
			public DateTime end1 { get; set; }
		}
        private Models.User _user;

        private AppointmentRepository appointmentRepository = new();

		private User _selectedUser;
        private ObservableCollection<Models.User> _users = new(DbContext.Users.Where(d => !d.Deleted));
        public ObservableCollection<Models.User> Users {
            get => _users;
            set {
                _users = value;
                OnPropertyChanged(nameof(Users));
            }
        }

		public User SelectedUser {
			get => _selectedUser;
			set {
				_selectedUser = value;
				OnPropertyChanged(nameof(SelectedUser));
                SelectedDate = DateTime.Now;
			}
		}


		private string _msgBoxMsg = "";
        public string MsgBoxMessage {
            get => _msgBoxMsg;
            set {
                _msgBoxMsg = value;
                OnPropertyChanged(nameof(MsgBoxMessage));
            }
        }

		private ObservableCollection<WorkHour> _workHours = new();
        private List<WorkHour> oldWorkhours = new();

        private void LoadWorkhours() {
			WorkHours.Clear();
			var whStarts = DbContext.Workhours.Where(ws => ws.BlockStart.Value.Date == SelectedDate.Date && SelectedUser.Id == ws.UserId).GroupBy(ws => ws.BlockStart).Select(g => g.First());
			var whEnds = DbContext.Workhours.Where(ws => ws.BlockStart.Value.Date == SelectedDate.Date && SelectedUser.Id == ws.UserId).GroupBy(ws => ws.BlockEnd).Select(g => g.First());
            Console.WriteLine(whStarts.Count()+"--"+SelectedDate.Date);
            foreach (var item in whEnds)
			{
				WorkHours.Add(new WorkHour() { start1 = (DateTime)item.BlockStart, end1 = (DateTime)item.BlockEnd });
			}
		}

        public ObservableCollection<WorkHour> WorkHours {
            get => _workHours;
            set {
                _workHours = value;
                OnPropertyChanged(nameof(WorkHours));

			}

        }
        public ICommand AddNewHours {  get; set; }
        public ICommand RemoveHour { get; set; }
        public ICommand Save { get; set; }

        private DateTime _selectedDate = DateTime.Now;
        public DateTime SelectedDate {
            get => _selectedDate;
            set {
                if (DbContext.Workhours.Where(w=>w.UserId == SelectedUser.Id).Count() >0&& SelectedUser != null && value.Date.Date <= DbContext.Workhours.Where(w => w.UserId == SelectedUser.Id).OrderBy(w => w.BlockEnd).Last().End.Value.Date &&
                    value.Date.Date >= DbContext.Workhours.Where(w => w.UserId == SelectedUser.Id).OrderBy(w => w.BlockStart).First().Start.Value.Date)
                {
                    _selectedDate = value;
                    OnPropertyChanged(nameof(SelectedDate));
                    LoadWorkhours();
                }
                else _selectedDate = DateTime.Now.Date;

			}
		}

        public ICommand RestartView { get; }

        public UpdateScheduleViewModel(AdminViewModel viewModel, string msgBoxText) {
            //RestartView = new BasicCommand((object obj) => viewModel.CurrentViewModel = new UpdateScheduleViewModel(viewModel));
            MsgBoxMessage = msgBoxText;
            Save = new BasicCommand((object obj) =>
            {
                bool error = false;
                foreach (var item in WorkHours)
                {
                    Console.WriteLine(item.start1+"->"+item.end1);
                    foreach (var item2 in WorkHours)
                    {
                        if (item != item2)
                        {

                            if (((item2.start1 <= item.start1 && item2.end1 >= item.end1) || //np. 7-12 i 6-18
                                (item2.start1 >= item.start1 && item2.end1 <= item.end1) || // np. 7-15 i 12-14
                                (item2.start1 >= item.start1 && item2.start1 <= item.end1) ||
                                (item2.end1 <= item2.start1)))

                            {
                                error = true;
                                if (!MsgBoxMessage.Contains("Niepoprawne godziny pracy"))
                                {                                   
                                    MsgBoxMessage += ("Niepoprawne godziny pracy");
                                }
                            }else error = false;
                        }
                        
                    }
                    if (error == false) MsgBoxMessage = "";
                }
                if (MsgBoxMessage.Length == 0)
                {
                    MsgBoxMessage = "";
                    foreach (var item in WorkHours)
                    {
                        var apps = DbContext.Appointments.Include("Patient").Include("Doctor").Where(ap => !ap.Patient.Deleted && ap.DoctorUserId == SelectedUser.Id && ap.Date.Value.Date == SelectedDate.Date).ToList();

						foreach (var app in apps)
                        {
                            
                            if (WorkHours.Any(wh => { return new TimeRange(wh.start1, wh.end1).HasInside(app.Date.Value); })) { }
                            else
                            {
                                var msg = "\n" + DbContext.Patients.Where(p => p.Id == app.PatientId).First().Name + " " +
								DbContext.Patients.Where(p => p.Id == app.PatientId).First().Surname + " " + app.Date.Value.ToString("yyyy-MM-dd HH:mm:ss");
								if (!MsgBoxMessage.Contains("Zmiana harmonogramu koliduje z następującymi wizytami: \n"))
                                    MsgBoxMessage += "Zmiana harmonogramu koliduje z następującymi wizytami: \n";
                                if(!MsgBoxMessage.Contains(msg))MsgBoxMessage += msg;
                            }
                        }
                        
                    }
                    if (MsgBoxMessage.Length == 0)
                    {

                        DbContext.Database.ExecuteSqlRaw($"delete from workhours where user_id={SelectedUser.Id} && dayofyear(start) = {SelectedDate.Date.DayOfYear} && year(start) = {SelectedDate.Date.Year}");
                        GenerateNewWorkhours();

                        MsgBoxMessage = "Zmieniono harmonogram";


                    }
                    else viewModel.CurrentViewModel = new UpdateScheduleViewModel(viewModel,MsgBoxMessage);
				}

			});



            AddNewHours = new BasicCommand((object obj) =>
            {
                WorkHours.Add(new(){ start1 = SelectedDate.Date, end1 = SelectedDate.Date});
            });

            RemoveHour = new BasicCommand((object obj) =>
            {
                WorkHours.Remove(WorkHours.Where(w => w.start1 == (DateTime)obj).First());

            });
        }
        private void GenerateNewWorkhours() {

			var slots = new HashSet<KeyValuePair<TimeRange, TimeRange>>();

			for (int i = 0; i < 48; i++)
			{
				var start = SelectedDate.Date.AddHours((double)i / 2);
				var end = start.AddHours(0.5);
				Console.WriteLine("i->" + i + " " + start + " " + end);
				foreach (var d in WorkHours)
				{
					var blockStart = d.start1;
					var blockEnd = d.end1;
					var block = new TimeRange(blockStart, blockEnd);
					if (new TimeRange() { Start = d.start1, End = d.end1 }.HasInside(new TimeRange(start, end)) && block.HasInside(new TimeRange(start, end)))
						slots.Add(new(new(start, end), new(blockStart, blockEnd)));

				}

			}

			foreach (var slot in slots)
			{
                DbContext.Database.ExecuteSqlRaw($"insert into workhours(start,end,user_id,blockStart,open,blockEnd) values('{slot.Key.Start.ToString("yyyy-MM-dd HH:mm:ss")}','{slot.Key.End.ToString("yyyy-MM-dd HH:mm:ss")}',{SelectedUser.Id},'{slot.Value.Start.ToString("yyyy-MM-dd HH:mm:ss")}',1,'{slot.Value.End.ToString("yyyy-MM-dd HH:mm:ss")}')");
			}
		}
    }
}
