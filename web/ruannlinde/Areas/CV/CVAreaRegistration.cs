using System.Web.Mvc;

namespace RL.Areas.CV {
	public class CVAreaRegistration : AreaRegistration {
		public override string AreaName => "CV";

		public override void RegisterArea(AreaRegistrationContext context) {
			context.MapRoute(
				"CV_default",
				"CV/{controller}/{action}/{id}",
				new { controller="Home", action = "Index", id = UrlParameter.Optional },
				new[] { "RL.Areas.CV.Controllers"}
			);
		}
	}
}