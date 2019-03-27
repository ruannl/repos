using System.Web.Mvc;

namespace Ruann.Linde.Areas.Intuit.Controllers {
	// [Authorize]
    public class HomeController : Controller {
        
        public ActionResult Index() {
            return this.View();
        }
    }
}