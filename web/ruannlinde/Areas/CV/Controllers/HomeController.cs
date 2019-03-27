using System;
using System.Web.Mvc;
using log4net;
using Ruann.Linde.Database.Models;
using Ruann.Linde.Database.Providers.CurriculumVitae;

namespace Ruann.Linde.Areas.CV.Controllers {
	public class HomeController : Controller {

		internal CurriculumVitaeManager CurriculumVitaeManager;
		internal ILog Log = LogManager.GetLogger(typeof(HomeController).Name);

		public HomeController(CurriculumVitaeManager cvManager) {
			CurriculumVitaeManager = cvManager;
		}

		public ActionResult Index() {
			return View();
		}

		[HttpPost]
		public JsonResult SubmitMessage(string name, string surname, string email, string message) {
			try {
				Log.Info("CV Home Controller SubmitMessage");
				var contactMessage = CurriculumVitaeManager.AddContactMessage(new ContactMessage {FirstName = name, LastName = surname, Email = email, Message = message});
				Log.Info(contactMessage);

				return Json(contactMessage);
			}
			catch (Exception e) {
				Log.Error(e);
				throw;
			}
		}
	}
}