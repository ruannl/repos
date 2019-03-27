using System;
using System.Collections.Generic;
using System.Linq;
using log4net;
using Ruann.Linde.Database.Models;

namespace Ruann.Linde.Database.Providers.CurriculumVitae {
	public interface ICurriculumVitaeManager {
		ContactMessage AddContactMessage(ContactMessage message);
		IList<ContactMessage> GetContactMessages();
	}

	public class CurriculumVitaeManager : ICurriculumVitaeManager {

		internal ApplicationDatabaseContext ApplicationDatabaseContext;
		internal ILog Log = LogManager.GetLogger(typeof(CurriculumVitaeManager).Name);

		public CurriculumVitaeManager(ApplicationDatabaseContext context) {
			ApplicationDatabaseContext = context;
		}

		public ContactMessage AddContactMessage(ContactMessage message) {
			try {
				ApplicationDatabaseContext.ContactMessages.Add(message);
				ApplicationDatabaseContext.SaveChanges();
				return message;
			}
			catch (Exception e) {
				Log.Error(e.ToString());
				return null;
			}
		}

		public IList<ContactMessage> GetContactMessages() {
			return ApplicationDatabaseContext.ContactMessages.ToList();
		}
	}
}