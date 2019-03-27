using System;
using System.Web.Mvc;
using log4net;
using Ruann.Linde.Database.Providers;

namespace Ruann.Linde.Controllers
{
	//[Authorize]
    public class HomeController : Controller
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(HomeController).Name);

        private static ILogProvider _logProvider;
        public HomeController(ILogProvider logProvider)
        {
            _logProvider = logProvider;
            Log.Info(_logProvider.LogFileDirectory);
            //if (User.Identity.IsAuthenticated) {
            //    Log.Debug("User is authenticated");
            //}
        }

        //private ApplicationSignInManager SignInManager { get => _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>(); set => _signInManager = value; }
        // private ApplicationUserManager UserManager { get => _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>(); set => _userManager = value; }

        public ActionResult Index()
        {
            return View();
        }

        public bool ClearLogContent()
        {
            try
            {
                return _logProvider.ClearLog();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public string GetLogContent()
        {
            try
            {
                return _logProvider.GetLog();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}