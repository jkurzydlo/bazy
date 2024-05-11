using bazy1.Models;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.ComponentModel;

namespace bazy1.ViewModels
{
    public abstract class ViewModelBase : INotifyPropertyChanged //Klasa bazowa, wszystkie viewmodele b�d� z niej dziedziczy�	
    {
        static ViewModelBase()
        {
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
