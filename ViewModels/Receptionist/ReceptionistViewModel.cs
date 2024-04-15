using bazy1.ViewModels.Doctor;
using System;
using System.Threading;
using System.Windows.Input;
using bazy1.ViewModels;
using bazy1.Views;
using bazy1.Views.Receptionist.Pages;

namespace bazy1.ViewModels.Receptionist
{
    public class ReceptionistViewModel : DoctorViewModel
    {
        public ICommand ShowPatientsCommand { get; }

        public ReceptionistViewModel()
        {
            Console.WriteLine("New ReceptionistViewModel instance created.");
            // Tutaj możesz dodać logikę specyficzną dla recepcjonisty

            // Inicjalizacja polecenia ShowPatientsCommand
            ShowPatientsCommand = new RelayCommand(ExecuteShowPatientsCommand);
        }

        private void ExecuteShowPatientsCommand(object obj)
        {
            // Otwieranie widoku pacjentów (PatientsView.xaml)
            PatientsView patientsView = new PatientsView();
            patientsView.Show();
        }

        // Możesz również nadpisać lub rozszerzyć metody istniejące w DoctorViewModel
    }
}
