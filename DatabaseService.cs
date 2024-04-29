using bazy1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bazy1
{
    internal class DatabaseService
    {

        private static PrzychodniaContext _dbContext;

        public DatabaseService(PrzychodniaContext dbContext)
        {
            _dbContext = dbContext;
        }

        public static PrzychodniaContext getDbContext()
        {
            return _dbContext;
        }

    }
}
