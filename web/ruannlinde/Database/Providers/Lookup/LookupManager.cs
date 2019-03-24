using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using log4net;
using RL.Database.Models;
using RL.Database.Providers.Quickbooks;

namespace RL.Database.Providers.Lookup {

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