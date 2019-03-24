using System.Web.Mvc;
using System.Web.Routing;
namespace RL {
	public class RouteConfig {
		public static void RegisterRoutes(RouteCollection routes) {
			routes.LowercaseUrls = true;

			routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
			routes.MapMvcAttributeRoutes();

			//routes.MapRoute("Default"
			//				, "{area}/{controller}/{action}/{id}"
			//				, new { area = "CV", controller = "Home", action = "Index", id = UrlParameter.Optional }
			//			  , new[] { "RL.Areas.CV.Controllers" }
			// );

			//routes.MapRoute("CV"
			//              , "{area}/{controller}/{action}/{id}"
			//              , new { area = "CV", controller = "Home", action = "Index", id = UrlParameter.Optional }
			//              , new[] { "RL.Areas.CV.Controllers" }
			//);

			routes.MapRoute("Default"
						  , "{controller}/{action}/{id}"
						  , new { controller = "Home", action = "Index", id = UrlParameter.Optional }
						  , new[] { "RL.Controllers" });

			//routes.MapRoute("Calendar"
			//              , "{controller}/{action}/{id}"
			//              , new { controller = "Calendar", action = "Index", id = UrlParameter.Optional }
			//              , new[] { "RL.Controllers" });


			//routes.MapMvcAttributeRoutes();


			//routes.MapRoute("signin-google", "signin-google", new { controller = "Account", action = "ExternalLoginCallback" });
		}
	}
}