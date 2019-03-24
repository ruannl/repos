namespace RL.Controllers {
	using System.Web.Mvc;

	//[Authorize]
	public class HomeController : Controller {
		//private static readonly ILog Log = LogManager.GetLogger(typeof(HomeController).Name);

		//private ApplicationUserManager _userManager;

		public HomeController() {

			//if (User.Identity.IsAuthenticated) {
			//    Log.Debug("User is authenticated");

			//}
		}

		//private ApplicationSignInManager SignInManager { get => _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>(); set => _signInManager = value; }
		// private ApplicationUserManager UserManager { get => _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>(); set => _userManager = value; }

		public ActionResult Index() {
			return View();
		}

		
	}
}