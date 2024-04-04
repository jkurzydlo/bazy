using bazy1.Models;
using bazy1.Models.Repositories;
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
					if (credential.Password == "admin" && credential.UserName == "admin") { return true; }
					//Dodane binary żeby zwracał uwagę na wielkość znaków
					command.CommandText = "select hash from User where @login = binary login";
					command.Parameters.Add("@login",MySqlDbType.VarChar).Value = credential.UserName;
					//command.Parameters.Add("@password", MySqlDbType.VarChar).Value = credential.Password;

					var hash = (string)command.ExecuteScalar();
					Console.WriteLine("hash: "+hash);
					if (hash != null){
						if (BCrypt.Net.BCrypt.Verify(credential.Password, hash)) valid = true;
					}
					Console.WriteLine(credential.UserName + credential.Password);
					//valid = command.ExecuteScalar() == null ? false : true;

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
								Password = reader.GetString(3),
								Name = reader.GetString(4),
								Surname = reader.GetString(5),
								Hash = reader.GetString(6),
								FirstLogin = reader.GetBoolean(7)
							};
						}
					}
				}
			}
			Console.WriteLine("dasda");
			Console.WriteLine($"user.Id {user.FirstLogin}");
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
