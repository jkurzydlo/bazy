using bazy1.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace bazy1.ViewModels.Receptionist.Pages {
	public class PatientDocumentsListViewModel : ViewModelBase {
			private string _date, _patientName, _filterText;
			private Prescription _selectedPrescription;
			private Referral _selectedReferral;

		private PrescriptionGenerator generator = new();
			private Models.Doctor doctor;
			private static string _pdfPath;
			private ICollectionView _prescriptionsView;


			public string PdfPath {
				get => _pdfPath;
				set {
					if (value != null)
					{
						_pdfPath = value;
						OnPropertyChanged(nameof(PdfPath));
					}
				}
			}
			public string FilterText {
				get => _filterText;
				set {
					_filterText = value;

					//Wyszukiwanie po nazwie
					PrescriptionsView.Filter += (object prescription) =>
					{
						var tempPrescription = prescription as Prescription;
						return tempPrescription.DateOfPrescription.Value.ToLongDateString().ToLower().Contains(FilterText.ToLower().Trim())
						|| tempPrescription.Patient.Name.ToLower().Contains(FilterText.ToLower().Trim()) ||
						tempPrescription.Patient.Surname.ToLower().Contains(FilterText.ToLower().Trim())
						|| tempPrescription.Medicines.Any(m => m.Name.ToLower().Contains(FilterText.ToLower().Trim()));
					};
				ReferralsView.Filter += (object referral) =>
				{
					var tempReferral = referral as Referral;
					return tempReferral.Date.Value.ToLongDateString().ToLower().Contains(FilterText.ToLower().Trim())
						|| tempReferral.Code.ToLower().Contains(FilterText.ToLower().Trim()) ||
						tempReferral.Information.ToLower().Contains(FilterText.ToLower().Trim());
				};

					OnPropertyChanged(nameof(FilterText));

				}
			}
			public Prescription SelectedPrescription {
				get => _selectedPrescription;
				set {
					_selectedPrescription = value;

					if (SelectedPrescription != null)
					{
						PdfPath = SelectedPrescription.Pdf;
					}

					//_pdfPath = _selectedPrescription != null ? SelectedPrescription.Pdf : "";
					Console.WriteLine("wymm:" + PdfPath);
					OnPropertyChanged(nameof(SelectedPrescription));
					//var filename = generator.generate(DbContext.Prescriptions.Include("Medicines").Include("Patient").Include("Patient.Addresses").Where(pr => pr.Id == SelectedPrescription.Id).First(), doctor);
					//Console.WriteLine("pdf: " + SelectedPrescription.Pdf);

				}
			}

		public Referral SelectedReferral {
			get => _selectedReferral;
			set {
				_selectedReferral= value;

				if (SelectedReferral != null)
				{
					PdfPath = SelectedReferral.Pdf;
				}
				OnPropertyChanged(nameof(SelectedReferral));


			}
		}

		public ICollectionView PrescriptionsView { get; set; }
		public ICollectionView ReferralsView { get; set; }



		public PatientDocumentsListViewModel(List<Prescription> prescriptions, List<Referral> referrals) {
				foreach (var item in prescriptions)
				{
					if (item.Patient.Name != null) Console.WriteLine(item.Patient.Name);
				}
				// Console.WriteLine(prescriptions[0].Patient.Name);
				PrescriptionsView = CollectionViewSource.GetDefaultView(prescriptions);
				ReferralsView = CollectionViewSource.GetDefaultView(referrals);

		}
	}
	}
