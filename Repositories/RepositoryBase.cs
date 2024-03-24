using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bazy1.Repositories
{
    public abstract class RepositoryBase
    {
        private readonly string _connectionStrings;
        public RepositoryBase() {
            _connectionStrings = "Server=localhost;Database=m4;Uid=root;Pwd=12345;";

		}
        protected MySqlConnection GetConnection() {
            return new MySqlConnection(_connectionStrings);
        }
    }
}
