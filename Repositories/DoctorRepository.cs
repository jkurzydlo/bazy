using bazy1.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace bazy1.Repositories
{
    public class DoctorRepository : RepositoryBase
    {
        public List<Doctor> GetAll() {
			var doctors = new List<Doctor>();
			try
			{
				using (MySqlConnection conn = GetConnection())
				{
					conn.Open();
					string query = "SELECT * from doctor";
					MySqlCommand cmd = new MySqlCommand(query, conn);
					using (MySqlDataReader reader = cmd.ExecuteReader())
					{
						while (reader.Read())
						{
							Doctor doctor = new Doctor()
							{
								Id = Convert.ToInt32(reader["Id"]),
								 Name = reader.GetString("Name"),
								  Surname = reader.GetString("Surname"),

							};
							doctors.Add(doctor);
						}
					}
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine("Error sciaganie doctors z bazy: " + ex.Message);
			}
			return doctors;
		}

		public Doctor GetById(int id) {
			Doctor doctor = new();
			try
			{
				using (MySqlConnection conn = GetConnection())
				{
					conn.Open();

					var cmd = new MySqlCommand();
					cmd.Connection = conn;
					cmd.CommandText = "select * from doctor where id=@id";
					cmd.Parameters.Add("@id", MySqlDbType.Int32).Value = id;


					using (MySqlDataReader reader = cmd.ExecuteReader())
					{
						while (reader.Read())
						{
							Doctor tempDoctor = new Doctor()
							{
								Id = Convert.ToInt32(reader["Id"]),
								Name = reader.GetString("Name"),
								Surname = reader.GetString("Surname"),
								PhoneNumber = (reader.IsDBNull(3) ? "" : reader.GetString("phoneNumber")),
							};
							doctor = tempDoctor;
						}
					}
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine("Error sciaganie doctors z bazy: " + ex.Message);
			}
			return doctor;
		}

	}
}
