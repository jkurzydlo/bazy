﻿using Microsoft.EntityFrameworkCore;
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
		private string _name = "";
		private string _address = "";
		private string _phone = "";



		public string Name {
			get => _name;
			set {
				_name = value;
				OnPropertyChanged(nameof(Name));
			}
		}

		public string Phone {
			get => _phone;
			set {
				_phone = value;
				OnPropertyChanged(nameof(Phone));
			}
		}

		public string Address {
			get => _address;
			set {
				_address = value;
				OnPropertyChanged(nameof(Address));
			}
		}
		public ICommand SaveName { get; set; }
		public ICommand SaveAddress { get; set; }
		public ICommand SavePhone { get; set; }



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
			if (DbContext.Settings.Count() > 0)
			{
				Name = DbContext.Settings.First().Name;
				Phone = DbContext.Settings.First().Phone;
				Address = DbContext.Settings.First().Address;
			}
			else
			{
				Name = "";
				Phone = "";
				Address = "";
			}

				DownloadRPL = new BasicCommand(downloadXML);
			DownloadRPM = new BasicCommand(downloadCSV);

			if(File.Exists("rpl.xml")) RPLFound = "Znaleziono plik zasobów";
			if (File.Exists("rpm.zip")) RPMFound = "Znaleziono plik zasobów";
			SaveName = new BasicCommand((object obj) => {
				if (Name != "" && Phone != "" && Address != "")
				{
					if (DbContext.Settings.Count() > 0)
					{
						DbContext.Settings.First().Name = Name;
						DbContext.Settings.First().Address = Address;
						DbContext.Settings.First().Phone = Phone;
					}
					else DbContext.Settings.Add(new Models.Setting() { Name = Name, Address = Address, Phone = Phone });
					DbContext.SaveChanges();
					System.Windows.MessageBox.Show("Zapisano", "Powodzenie", MessageBoxButton.OK, MessageBoxImage.Information);

				}
				else
				{
					System.Windows.MessageBox.Show("Wszystkie pola muszą być wypełnione","Błąd",MessageBoxButton.OK, MessageBoxImage.Error);
				}
			});

			SaveAddress = new BasicCommand((object obj) =>
			{
				if (DbContext.Settings.Count() > 0 || DbContext.Settings.First().Address == "")
					DbContext.Settings.First().Address = Address;
				else DbContext.Settings.Add(new Models.Setting() { Address = Address });

				DbContext.SaveChanges();
			});

			SavePhone = new BasicCommand((object obj) =>
			{
				if (DbContext.Settings.Count() > 0 || DbContext.Settings.First().Phone == "")
					DbContext.Settings.First().Phone = Phone;
				else DbContext.Settings.Add(new Models.Setting() { Phone = Phone});

				DbContext.SaveChanges();
			});

		}
	}
}