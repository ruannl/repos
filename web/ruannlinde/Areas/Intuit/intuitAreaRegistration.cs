using System.Web.Mvc;

namespace Ruann.Linde.Areas.Intuit {
	public class IntuitAreaRegistration : AreaRegistration {
		public override string AreaName => "Intuit";

		public override void RegisterArea(AreaRegistrationContext context) {
			context.MapRoute(
				"intuit_default"
				, "Intuit/{controller}/{action}/{id}"
				, new { controller = "Home", action = "Index", id = UrlParameter.Optional }
				, new[] { "Ruann.Linde.Areas.Intuit.Controllers" });
		}
	}
}