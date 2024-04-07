using bazy1.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using bazy1.Models;

using dbm = bazy1.Models;

namespace bazy1.ViewModels.Doctor.Pages {
	public class PatientListViewModel : ViewModelBase {
		private readonly M4Context _context;
		private User user;
		private List<Patient> _patients;
		ObservableCollection<Patient> _patientsList;

		public PatientListViewModel(User user) {
		_context = new M4Context();
			this.user = user;
			var db = new M4Context();
			//_context.Doctors.Where((dbm.Doctor doctor) => doctor.User.Id == this.user.Id).First().Patients.Add(new Patient() { Pesel = 12345 });
			db.SaveChanges();
		}

	}
}
