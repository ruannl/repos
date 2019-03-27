using System.Collections.Generic;
using System.Linq;
using log4net;
using Ruann.Linde.Database.Models;
using Ruann.Linde.Database.Providers.Quickbooks;

namespace Ruann.Linde.Database.Providers.Lookup {

	public class LookupManager {

		internal ILog Log = LogManager.GetLogger(typeof(QuickbooksRepository).Name);
		internal ApplicationDatabaseContext ApplicationDatabaseContext;

		public LookupManager(ApplicationDatabaseContext context) {
			ApplicationDatabaseContext = context;
		}

		public List<PaymentType> PaymentTypes() {
			using (var context = new ApplicationDatabaseContext()) {
				return context.PaymentTypes.ToList();
			}
		}
	}
}