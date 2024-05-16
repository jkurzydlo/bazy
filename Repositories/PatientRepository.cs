using bazy1.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace bazy1.Repositories
{
    class PatientRepository : RepositoryBase
    {
		private AppointmentRepository appointmentRepository = new();
        public List<Patient> GetAll() {
			List<Patient> patients = new();
			try
			{
				using (MySqlConnection conn = GetConnection())
				{
					conn.Open();
					string query = "SELECT * FROM patient";
					MySqlCommand cmd = new MySqlCommand(query, conn);
					using (MySqlDataReader reader = cmd.ExecuteReader())
					{
						while (reader.Read())
						{
							Patient patient = new Patient()
							{
								Id = reader.GetInt32("id"),
								Name = reader.GetString("name"),
								Surname = reader.GetString("surname"),
							};
							patients.Add(patient);

						}

					}
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine("Error sciaganie patients z bazy: " + ex.Message);
			}
			return patients;
		}
	}
}



