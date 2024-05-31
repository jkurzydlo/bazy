using bazy1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bazy1 {
	internal class DatabaseService {
		
		private static przychodnia9Context _dbContext;

		public DatabaseService(przychodnia9Context dbContext) {
			_dbContext = dbContext;
		}

		public static przychodnia9Context getDbContext() {
			return _dbContext;
		}
		
	}
}
