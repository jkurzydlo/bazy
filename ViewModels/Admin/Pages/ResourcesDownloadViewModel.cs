using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace bazy1.ViewModels.Admin.Pages {
	class ResourcesDownloadViewModel : ViewModelBase {

		public ICommand DownloadRPL { get; set; }
		private double _rplfraction;
		private bool _RPLButtonActive = true;
		private Visibility _RPLVisible = Visibility.Hidden;

		private string rplFound = "Nie znaleziono pliku";
		public string RPLFound {
			get => rplFound;
			set {
				rplFound = value;
				OnPropertyChanged(nameof(RPLFound));
			}
		}

		public Visibility RPLVisible {
			get => _RPLVisible;
			set {
				_RPLVisible = value;
				OnPropertyChanged(nameof(RPLVisible));
			}
		}

		public bool RPLButtonActive {
			get => _RPLButtonActive;
			set {
				_RPLButtonActive = value;
				OnPropertyChanged(nameof(RPLButtonActive));
			}
		}

		public double RPLFraction {
			get => _rplfraction;
			set {
				_rplfraction = value;
				OnPropertyChanged(nameof(RPLFraction));
			}
		}

		public ICommand DownloadRPM { get; set; }
		private double _rpmfraction;
		private bool _RPMButtonActive = true;
		private Visibility _RPMVisible = Visibility.Hidden;

		private string rpmFound = "Nie znaleziono pliku";
		public string RPMFound {
			get => rpmFound;
			set {
				rpmFound = value;
				OnPropertyChanged(nameof(RPMFound));
			}
		}

		public Visibility RPMVisible {
			get => _RPMVisible;
			set {
				_RPMVisible = value;
				OnPropertyChanged(nameof(RPMVisible));
			}
		}

		public bool RPMButtonActive {
			get => _RPMButtonActive;
			set {
				_RPMButtonActive = value;
				OnPropertyChanged(nameof(RPMButtonActive));
			}
		}

		public double RPMFraction {
			get => _rpmfraction;
			set {
				_rpmfraction = value;
				OnPropertyChanged(nameof(RPMFraction));
			}
		}




		private async void downloadCSV(object obj) {
			RPMFraction = 0;
			try
			{
				RPMButtonActive = false;
				RPMVisible = Visibility.Visible;

				var httpClient = new HttpClientDownloadWithProgress("https://rpwdl.ezdrowie.gov.pl/Registry/Pobieranie?typ=Csv&rodzajRejestru=Rpm", "rpm.zip");
				httpClient.ProgressChanged += HttpClient_ProgressChanged;
				await httpClient.StartDownload();
			}
			catch (Exception e)
			{
				System.Windows.MessageBox.Show("Nie można pobrać pliku");
				RPMButtonActive = true;
				RPMVisible = Visibility.Hidden;
			}
			void HttpClient_ProgressChanged(long? totalFileSize, long totalBytesDownloaded, double? progressPercentage) {
					Console.WriteLine(progressPercentage);
				RPMFraction = (float)progressPercentage;
				if (100F - progressPercentage < 0.5)
				{
					RPMButtonActive = true;
					RPMVisible = Visibility.Hidden;
					RPMFound = "Znaleziono plik zasobów";
				}
			}
			
		}

		private async void downloadXML(object obj) {
			RPLFraction = 0;
			var url = "https://rejestry.ezdrowie.gov.pl/api/rpl/medicinal-products/public-pl-report/4.0.0/overall.xml";
			var filePath = "rpl.xml";
			try
			{
				RPLButtonActive = false;
				RPLVisible = Visibility.Visible;
				var httpClient = new HttpClientDownloadWithProgress(url, filePath);
				httpClient.ProgressChanged += HttpClient_ProgressChanged;
				await httpClient.StartDownload();
			}catch(Exception e)
			{
				System.Windows.MessageBox.Show("Nie można pobrać pliku");
				RPLButtonActive = true;
				RPLVisible = Visibility.Hidden;
			}
			void HttpClient_ProgressChanged(long? totalFileSize, long totalBytesDownloaded, double? progressPercentage) {
				RPLFraction = (float)progressPercentage;
				if(100F - progressPercentage < 0.5)
				{
					RPLButtonActive = true;
					RPLVisible = Visibility.Hidden;
					RPLFound = "Znaleziono plik zasobów";
				}
                Console.WriteLine((float)progressPercentage);
            }

		}

		public ResourcesDownloadViewModel(AdminViewModel viewModel) {
			DownloadRPL = new BasicCommand(downloadXML);
			DownloadRPM = new BasicCommand(downloadCSV);

			if(File.Exists("rpl.xml")) RPLFound = "Znaleziono plik zasobów";
			if (File.Exists("rpm.zip")) RPMFound = "Znaleziono plik zasobów";

		}
	}
}
