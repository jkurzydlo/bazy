using MySql.Data.MySqlClient;
using Org.BouncyCastle.Crypto.Generators;

namespace EmailAuthorization.Models {
	public class UserRepository {

		private List<User> _users = new List<User>();

		public List<User> getAllUsers() {

			using var connection = new MySqlConnection("server=localhost;database=przychodnia9;user=root;password=12345");
			connection.Open();
			using var cmd = new MySqlCommand("Select email, token, activated from user", connection);
			using var reader = cmd.ExecuteReader();


			while (reader.Read())
			{
				User user = new User();

				user.Activated = reader.GetBoolean("activated");
				user.Email = reader.GetString("email");
				user.Token = reader.GetString("token");
				_users.Add(user);
			}

			return _users;

		}

		public User getUserByEmail(string email) {
			using var connection = new MySqlConnection("server=localhost;database=przychodnia9;user=root;password=12345");
			connection.Open();
			using var cmd = new MySqlCommand($"Select email, token, activated from user where email='{email}'", connection);
			using var reader = cmd.ExecuteReader();

			User user = new User();

			while (reader.Read())
			{

				user.Activated = reader.GetBoolean("activated");
				user.Email = reader.GetString("email");
				user.Token = reader.GetString("token");
			}

			return user;

		}


		public void activateAccount(string token, string newPassword) {
            var hash = BCrypt.Net.BCrypt.HashPassword(newPassword);

            using var connection = new MySqlConnection("server=localhost;database=przychodnia9;user=root;password=12345");
			connection.Open();
			using var cmd = new MySqlCommand($"start transaction; update user set activated = 1,firstLogin=0, hash ='{hash}' where token='{token}'; commit; ", connection);
			cmd.ExecuteNonQuery();


		}
	

		public User getUserByToken(string token) {
			using var connection = new MySqlConnection("server=localhost;database=przychodnia9;user=root;password=12345");
			connection.Open();
			using var cmd = new MySqlCommand($"Select email, token, activated from user where token ='{token}'", connection);
			using var reader = cmd.ExecuteReader();

			User user = new User();

			while (reader.Read())
			{
				user.Activated = reader.GetBoolean("activated");
				user.Email = reader.GetString("email");
				user.Token = reader.GetString("token");
			}

			return user;

		}

	}
}
