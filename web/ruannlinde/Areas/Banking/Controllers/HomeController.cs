using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ruann.Linde.Areas.Banking.Controllers
{
    public class HomeController : Controller
    {
        // GET: Banking/Home
        public ActionResult Index()
        {
            return View();
        }
    }
}