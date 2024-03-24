using bazy1.Models;
using bazy1.Models.Repositories;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace bazy1.Repositories
{
	public class UserRepository : RepositoryBase, IUserRepository {
		public void add(User user) {
			throw new NotImplementedException();
		}

		public bool authenticate(NetworkCredential credential) {
			bool valid = false;
			using (var connection = GetConnection()) {
				using (var command = new MySqlCommand()) {
					connection.Open();
					command.Connection = connection;
					command.CommandText = "select * from User where login=@login and password=@password";
					command.Parameters.Add("@login",MySqlDbType.VarChar).Value = credential.UserName;
					command.Parameters.Add("@password", MySqlDbType.VarChar).Value = credential.Password;
					valid = command.ExecuteScalar() == null ? false : true;

				}
			}
			return valid;
		}

		public IEnumerable<User> findAll() {
			throw new NotImplementedException();
		}

		public User findById(int id) {
			throw new NotImplementedException();
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
								Name = reader.GetString(4),
								Surname = reader.GetString(5)
							};
						}
					}
				}
			}
			Console.WriteLine("dasda");
			Console.WriteLine(user.Name);
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
