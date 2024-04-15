using bazy1.ViewModels.Doctor;
using System;
using System.Threading;

namespace bazy1.ViewModels.Receptionist
{
    public class ReceptionistViewModel : DoctorViewModel
    {
        public ReceptionistViewModel()
        {
            Console.WriteLine("New ReceptionistViewModel instance created.");
            // Tutaj możesz dodać logikę specyficzną dla recepcjonisty
        }

        // Możesz również nadpisać lub rozszerzyć metody istniejące w DoctorViewModel
    }
}



