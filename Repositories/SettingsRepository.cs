using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using bazy1.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bazy1.Repositories {
	class SettingsRepository : RepositoryBase {

		public Setting GetSettings() {
			Setting tempSetting = new();
			try
			{
				using (MySqlConnection conn = GetConnection())
				{
					conn.Open();
					string query = "SELECT * FROM settings";
					MySqlCommand cmd = new MySqlCommand(query, conn);
					using (MySqlDataReader reader = cmd.ExecuteReader())
					{
						while (reader.Read())
						{
							Setting setting = new Setting()
							{
								Id = reader.GetInt32("id"),
								Name = reader.GetString("name"),
								Address = reader.GetString("address"),
								Phone = reader.GetString("phone")
							};
							tempSetting = setting;

						}

					}
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine("Błąd pobierania z bazy danych: " + ex.Message);
			}
			return tempSetting;
		}
	}
}

