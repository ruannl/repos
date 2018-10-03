namespace RL.Areas.intuit {
    using System.Web.Mvc;

    public class IntuitAreaRegistration : AreaRegistration {
        public override string AreaName => "Intuit";

        public override void RegisterArea(AreaRegistrationContext context) {
            context.MapRoute(
                "intuit_default"
                , "Intuit/{controller}/{action}/{id}"
                , new { controller = "Home", action = "Index", id = UrlParameter.Optional }
                , new[] { "RL.Areas.Intuit.Controllers" });
        }
    }
}