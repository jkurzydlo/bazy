using bazy1.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.ComponentModel;

namespace bazy1.ViewModels
{
	public abstract class ViewModelBase : INotifyPropertyChanged //Klasa bazowa, wszystkie viewmodele bêd¹ z niej dziedziczyæ	
	{
		static ViewModelBase() {
			if (DbContext == null) DbContext = new Przychodnia9Context();
		}

		public static Przychodnia9Context DbContext { get; set; }
	
		public event PropertyChangedEventHandler? PropertyChanged;
		public void OnPropertyChanged(string propertyName)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

	}
}
