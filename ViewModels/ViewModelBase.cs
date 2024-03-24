using System.ComponentModel;

namespace bazy1.ViewModels
{
	public abstract class ViewModelBase : INotifyPropertyChanged //Klasa bazowa, wszystkie viewmodele bêd¹ z niej dziedziczyæ	
	{
		public event PropertyChangedEventHandler? PropertyChanged;
		public void OnPropertyChanged(string propertyName)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}
