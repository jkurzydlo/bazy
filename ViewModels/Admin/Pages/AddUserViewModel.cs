using Microsoft.AspNetCore.CookiePolicy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace bazy1.ViewModels.Admin.Pages {
    public class AddUserViewModel : ViewModelBase {
        private string _username;
        public string UserName {
            get => _username; set {
                _username = value;
                OnPropertyChanged(UserName);
            }
        }
        public AddUserViewModel() { UserName = "dsds"; }
    }
}
