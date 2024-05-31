using bazy1.Models;
using bazy1.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.EntityFrameworkCore.Infrastructure;
using MySql.Data.MySqlClient;
using System.ComponentModel;
using System.Data;

namespace bazy1.ViewModels
{
	public abstract class ViewModelBase : INotifyPropertyChanged //Klasa bazowa, wszystkie viewmodele bêd¹ z niej dziedziczyæ	
	{
		static ViewModelBase() {
			if (DbContext == null) DbContext = new przychodnia9Context();
		}

		public static przychodnia9Context DbContext { get; set; }

		private readonly string _connectionStrings = "Server=localhost;Database=przychodnia9;Uid=root;Pwd=12345;";

		
		protected MySqlConnection GetConnection() {
			return new MySqlConnection(_connectionStrings);
		}

		public List<object> ExecuteRawQuery(string query) {
			using var connection = GetConnection();
			using var command = new MySqlCommand();
			List<object> results = [];


			connection.Open();
			command.Connection = connection;
			command.CommandText = query;

			using (var reader = command.ExecuteReader())
			{
				//results.
			}
			return results;
		}

		public event PropertyChangedEventHandler? PropertyChanged;
		public void OnPropertyChanged(string propertyName)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

	}
}
