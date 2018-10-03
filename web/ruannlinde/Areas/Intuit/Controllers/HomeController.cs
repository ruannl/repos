namespace RL.Areas.intuit.Controllers {
    using System.Web.Mvc;

    // [Authorize]
    public class HomeController : Controller {
        
        public ActionResult Index() {
            return this.View();
        }
    }
}