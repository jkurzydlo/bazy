using bazy1.Models;
using bazy1.Models.Repositories;
using bazy1.Utils;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace bazy1.Repositories
{
	public class UserRepository : RepositoryBase, IUserRepository {

		public void add(User user) {
			throw new NotImplementedException();
		}

		public void adminGenerate() {
			using (var connection = GetConnection())
			{
				using (var command = new MySqlCommand())
				{
					connection.Open();
					command.Connection = connection;

					string login = "admin", password = "admin"; //Pierwsze dane generowane i dostarczane przy dostarczaniu programu klientowi
					
					//Dodane Ignore, żeby dodało konkretnego admina tylko raz 
					command.CommandText = "insert ignore into user(type,login,name,surname,hash,firstLogin) values(@type,@login,@name,@surname, @hash, @firstLogin)";
					command.Parameters.Add(new MySqlParameter("@type", MySqlDbType.Enum) { Value = login});
					command.Parameters.Add(new MySqlParameter("@login", MySqlDbType.VarChar) { Value = password });
					command.Parameters.Add(new MySqlParameter("@name", MySqlDbType.VarChar) { Value = "admin"});
					command.Parameters.Add(new MySqlParameter("@surname", MySqlDbType.VarChar) { Value = "admin" });
					command.Parameters.Add(new MySqlParameter("@firstLogin", MySqlDbType.Byte) { Value = 1 });
					command.Parameters.Add(new MySqlParameter("@hash", MySqlDbType.VarChar) { Value = BCrypt.Net.BCrypt.HashPassword(password) }) ;
					//command.Parameters.Add(new MySqlParameter("@password", MySqlDbType.VarChar) { Value = "admin" });
					command.ExecuteScalar();

					var specs = new List<string> { "Chirurg", "Dermatolog", "Kardiolog", "Laryngolog", "Neurolog", "Okulista", "Ortopeda", "Pediatra", "Psychiatra", "Psycholog", "Stomatolog", "Urolog" };

                    command.Connection = connection;

					for (int i = 0;i < specs.Count;i++)
                    {
                       command.CommandText = $"insert ignore into specialization(name) values(@{i})";
						command.Parameters.Add(new MySqlParameter($"@{i}", MySqlDbType.VarChar) { Value = specs[i] });
						command.ExecuteScalar();
                    }

				}
			}
		}

        public bool authenticate(NetworkCredential credential)
        {
            bool valid = false;
            using (var connection = GetConnection())
            {
                using (var command = new MySqlCommand())
                {
                    connection.Open();
                    command.Connection = connection;

                    // Sprawdź, czy konto jest zablokowane
                    command.CommandText = "select lockoutEnd from User where @login = binary login";
                    command.Parameters.AddWithValue("@login", credential.UserName);
                    var lockoutEnd = command.ExecuteScalar() as DateTime?;
                    if (lockoutEnd.HasValue && lockoutEnd.Value > DateTime.Now)
                    {
                        return false; // Konto jest zablokowane
                    }

                    //Dodane binary żeby zwracał uwagę na wielkość znaków
                    command.CommandText = "select hash from User where @login = binary login";
                    var hash = (string)command.ExecuteScalar();
                    command.CommandText = "select deleted from User where @login2 = binary login";
                    command.Parameters.Add("@login2", MySqlDbType.VarChar).Value = credential.UserName;


					Console.WriteLine("hash: "+hash);
					if (hash != null){
						var deleted = (bool)command.ExecuteScalar();

						if (BCrypt.Net.BCrypt.Verify(credential.Password, hash) && !deleted)
						{
							//Jeśli dane ok - ustaw datę ostatniego logowania
							command.CommandText = "update user set lastLogin=@date where login=@login";
							command.Parameters.Add("@date", MySqlDbType.DateTime).Value = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
							command.ExecuteScalar();
							valid = true;

                            command.CommandText = "select MaxFailedLoginAttempts, LockoutDurationMinutes from login_settings limit 1";
                            using (var settingsReader = command.ExecuteReader())
                            {
                                if (settingsReader.Read())
                                {
                                    maxFailedLoginAttempts = settingsReader.GetInt32(0);
                                    lockoutDurationMinutes = settingsReader.GetInt32(1);
                                }
                            }

                            command.CommandText = "select FailedLoginAttempts from User where @login = binary login";
                            command.Parameters.Clear();
                            command.Parameters.AddWithValue("@login", credential.UserName);
                            var failedLoginAttempts = (int?)command.ExecuteScalar() ?? 0;
                            failedLoginAttempts++;

                            DateTime? lockoutEndValue = null;
                            if (failedLoginAttempts >= maxFailedLoginAttempts)
                            {
                                // Zablokuj konto
                                lockoutEndValue = DateTime.Now.AddMinutes(lockoutDurationMinutes);
                                command.CommandText = "update user set LockoutEnd=@lockoutEnd where login=@login";
                                command.Parameters.Clear();
                                command.Parameters.AddWithValue("@lockoutEnd", lockoutEndValue);
                                command.Parameters.AddWithValue("@login", credential.UserName);
                                command.ExecuteScalar();
                            }

                            command.CommandText = "update user set FailedLoginAttempts=@failedLoginAttempts where login=@login";
                            command.Parameters.Clear();
                            command.Parameters.AddWithValue("@failedLoginAttempts", failedLoginAttempts);
                            command.Parameters.AddWithValue("@login", credential.UserName);
                            command.ExecuteScalar();
                        }
                    }
                    Console.WriteLine(credential.UserName + credential.Password);
                }
            }
            return valid;
        }


        public IEnumerable<User> findAll() {
			throw new NotImplementedException();
		}

		public User findById(int id) {
			User? user = null;

			using (var connection = GetConnection())
			{
				using (var command = new MySqlCommand())
				{
					connection.Open();
					command.Connection = connection;
					command.CommandText = "select * from User where id=@id";
					command.Parameters.Add("@id", MySqlDbType.Int32).Value = id;

					using (var reader = command.ExecuteReader())
					{
						while (reader.Read())
						{
							user = new User()
							{
								Id = reader.GetInt32(0),
								Type = reader.GetString(1),
								Name = reader.GetString(4),
								Surname = reader.GetString(5)
							};
						}
					}
				}
			}
			return user;

		}

		public User findByUsername(string username) {
					User? user = null;

			using (var connection = GetConnection())
			{
				using (var command = new MySqlCommand())
				{
					connection.Open();
					command.Connection = connection;
					command.CommandText = "select * from User where login=@login";
					command.Parameters.Add("@login", MySqlDbType.VarChar).Value = username;

					using (var reader = command.ExecuteReader())
					{
						while (reader.Read())
						{
							user = new User()
							{
								Id = reader.GetInt32(0),
								Type = reader.GetString(1),
								Login = reader.GetString(2),
								Name = reader.GetString(3),
								Surname = reader.GetString(4),
                                Hash = reader.GetString(5),
                                FirstLogin = reader.GetBoolean(6),
                                FailedLoginAttempts = reader.GetInt32(12),
                                LockoutEnd = reader.GetDateTime(13)
                            };
						}
					}
				}
			}
			Console.WriteLine("dasda");
			//Console.WriteLine($"user.Id {user.FirstLogin}");
			return user;
		}

		public void remove(User user) {
			throw new NotImplementedException();
		}

		public void update(User user) {
			throw new NotImplementedException();
		}
	}
}
