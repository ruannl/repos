namespace RL.Areas.Accounting {
    using System.Web.Mvc;

    public class AccountingAreaRegistration : AreaRegistration {
        public override string AreaName => "Accounting";

        public override void RegisterArea(AreaRegistrationContext context) {
            context.MapRoute(
                "Accounting_default"
                , "Accounting/{controller}/{action}/{id}"
                , new { action = "Index", id = UrlParameter.Optional });
        }
    }
}