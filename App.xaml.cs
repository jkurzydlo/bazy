using bazy1.ViewModels;
using bazy1.Views;
using System.Configuration;
using System.Data;
using System.Windows;

namespace bazy1
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application
	{
		public App()
		{
		}

		protected void ApplicationStartup(object sender, StartupEventArgs e) {
			var mainView = new MainView();
			var vm = new LoginViewModel();
			var loginView = new DoctorLogin
			{
				DataContext = vm
			};
			vm.LoginCompleted += (s, e) =>
			{
				loginView.Close();
				mainView.Show();
			};
			loginView.Show();
			
		}

		private void Vm_LoginCompleted(object? sender, EventArgs e) {
			throw new NotImplementedException();
		}
	}

}
