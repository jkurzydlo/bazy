﻿using System;
using System.IO;
using bazy1.Models;
using bazy1.Repositories;
using Microsoft.EntityFrameworkCore;
using QuestPDF.Drawing;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;


namespace bazy1 {
	public class PrescriptionGenerator {
		private SettingsRepository repository = new();

		public string generate(Prescription prescription, Doctor doctor) {
            Console.WriteLine("genruje sie;");
			//ViewModels.ViewModelBase.DbContext.Doctors.Where(doc => doc.Patients.Contains(prescription.Patient)).Where(doc => )

			FontManager.RegisterFont(File.OpenRead("LibreBarcode39Text-Regular.ttf"));
            QuestPDF.Settings.License = LicenseType.Community;
			string fileTitle = Directory.GetCurrentDirectory() + "\\" + 
				prescription.Id + 
				DateTime.Now.Day.ToString() +
				DateTime.Now.Month + 
				DateTime.Now.Year.ToString() +
				".pdf";

			Document.Create(container =>
			{
				container.Page(page =>
				{
					page.Size(PageSizes.EnvDL);

					page.Content().Table(table =>
					{
						table.ExtendLastCellsToTableBottom();
						table.ColumnsDefinition(columns =>
						{
							columns.RelativeColumn(2);
							columns.RelativeColumn(1);
						});

						table.Cell().Row(1).ColumnSpan(2).Border(1F).Text("Recepta").AlignLeft();
						table.Cell().Row(1).ColumnSpan(2).Text("\n"+repository.GetSettings().Name+"\n" +repository.GetSettings().Address+ "," +
							"tel.:"+repository.GetSettings().Phone+"\n").AlignCenter();
						table.Cell().Row(1).ColumnSpan(2).Border(1F).Text("\n\n\n\nŚwiadczeniodawca").AlignLeft();


						table.Cell().Row(2).ColumnSpan(2).BorderLeft(1F).Text("Pacjent\n" +
							prescription.Patient.Name + " " + prescription.Patient.Surname + "\n" + prescription.Patient.Addresses.ElementAt(0).City +
							 prescription.Patient.Addresses.ElementAt(0).Street +" " +prescription.Patient.Addresses.ElementAt(0).BuildingNumber).AlignLeft();
						table.Cell().Row(3).ColumnSpan(2).BorderBottom(1F).BorderLeft(1F).Text("\n\n\n\nPESEL: " + prescription.Patient.Pesel).AlignLeft();

						table.Cell().Row(2).RowSpan(2).Column(2).Border(1F).Text("Uprawnienia dodatkowe\n").AlignLeft();
						table.Cell().Row(4).Column(1).BorderLeft(1F).Text("Rp");
						table.Cell().Row(4).Column(2).BorderRight(1F).Text("Odpłatność");

						for (int i = 0; i < 11; i++)
						{
							Medicine med = new();
							if (i < prescription.Medicines.Count) med = prescription.Medicines.ElementAt(i);
							table.Cell().Row((uint)i + 5).Column(1).BorderLeft(1F).Text(
								i < prescription.Medicines.Count ?
								(med.Amount > 1 ? med.Amount + "x" : "") + med.Name + " "+ med.Dose+ " "+ med.Comments : "⠀\n");
							table.Cell().Column(2).Row((uint)i + 5).BorderRight(1F).Text(i < prescription.Medicines.Count ? med.Fraction * 100 + "%" : "⠀");
						}
						table.Cell().Row(16).ColumnSpan(2).BorderRight(1F).BorderLeft(1F).Text(prescription.Code).FontSize(25).AlignCenter().FontFamily("Libre Barcode 39 Text");

						table.Cell().Row(17).Column(1).Border(1F).Text("Data wystawienia\n\n" + prescription.DateOfPrescription.Value.ToShortDateString());
						table.Cell().Row(18).Column(1).Border(1F).Text("Data realizacji od dnia:\n\n");

						table.Cell().Row(17).RowSpan(2).Column(2).Border(1F).Text("Dane i podpis\n" + doctor.Name + " " + doctor.Surname + "\n" + "\nDane podmiotu drukującego");


					});
				});



			}).GeneratePdf(fileTitle);
            Console.WriteLine("skonczul");
            prescription.Pdf = fileTitle;
			//prescription.Pdf = doc.GenerateImages((int index) => { return DateTime.Now.ToString(); });
			//ViewModels.ViewModelBase.DbContext.Update(prescription.Medicines);

			//Console.WriteLine("pdff:"+prescription.Pdf);
			ViewModels.ViewModelBase.DbContext.SaveChanges();
			return fileTitle;
        }
	}
}

