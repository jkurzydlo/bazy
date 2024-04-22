using bazy1.ViewModels;
using System;

namespace bazy1.Views.Receptionist.Pages
{
    public class AppointmentViewModel : ViewModelBase
    {
        private string _dateTime;
        private string _goal;
        private int _notificationId;
        private int _patientId;

        public string DateTime
        {
            get => _dateTime;
            set
            {
                _dateTime = value;
                OnPropertyChanged(nameof(DateTime));
            }
        }

        public string Goal
        {
            get => _goal;
            set
            {
                _goal = value;
                OnPropertyChanged(nameof(Goal));
            }
        }

        public int NotificationId
        {
            get => _notificationId;
            set
            {
                _notificationId = value;
                OnPropertyChanged(nameof(NotificationId));
            }
        }

        public int PatientId
        {
            get => _patientId;
            set
            {
                _patientId = value;
                OnPropertyChanged(nameof(PatientId));
            }
        }
    }
}

