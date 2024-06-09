using bazy1.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bazy1.Repositories
{
    class WorkhoursRepository : RepositoryBase
    {
		public List<Workhour> GetByDoctorId(int id) {
			List<Workhour> workhours = new();
			try
			{
				using (MySqlConnection conn = GetConnection())
				{
					conn.Open();

					var cmd = new MySqlCommand();
					cmd.Connection = conn;
					cmd.CommandText = "select * from workhours where doctor_id=@id";
					cmd.Parameters.Add("@id", MySqlDbType.Int32).Value = id;


					using (MySqlDataReader reader = cmd.ExecuteReader())
					{
						while (reader.Read())
						{
							Workhour tempWorkhour = new ()
							{
								Id = reader.GetInt32("id"),
								UserId = reader.GetInt32("user_id"),
								BlockStart = reader.GetDateTime("blockStart"),
								BlockEnd = reader.GetDateTime("blockEnd"),
								Open = reader.GetBoolean("open"),
								Start = reader.GetDateTime("start"),
								End = reader.GetDateTime("end"),
							};
							workhours.Add(tempWorkhour);
						}
					}
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine("Error sciaganie doctors z bazy: " + ex.Message);
			}
			return workhours;
		}

	}
}

