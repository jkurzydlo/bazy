using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bazy1 {
	//klasa do przesłania typu użytkownika
	public class UserEventArgs : EventArgs {

		private readonly string _userType;
		private readonly bool _firstLogin;
		public string UserType { get { return _userType; } }

		public UserEventArgs(string? userType, bool? firstLogin) {
			_userType = userType;
			_firstLogin = firstLogin.GetValueOrDefault();
		}
	}
}
