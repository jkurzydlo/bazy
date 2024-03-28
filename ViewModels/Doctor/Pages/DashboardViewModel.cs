using bazy1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bazy1.ViewModels.Doctor.Pages {
	public class DashboardViewModel : ViewModelBase {
		private User _currentUser;

		public DashboardViewModel(User user) {
			CurrentUser = user;
		}

		public User CurrentUser { get => _currentUser; set => _currentUser = value; }
	}
}
