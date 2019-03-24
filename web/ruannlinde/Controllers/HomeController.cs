namespace RL.Controllers {
    using System;
    using System.Threading.Tasks;
    using System.Web;
    using System.Web.Mvc;

    using log4net;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.Owin;

    using StackifyLib;

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

		[HttpPost]
        public JsonResult SubmitMessage(string name, string surname, string email, string message) {
            return Json(new {
                name
              , surname
              , email
              , message
            });
        }
    }
}