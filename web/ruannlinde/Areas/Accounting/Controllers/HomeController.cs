using System.Web;
using log4net;
using RL.Areas.Accounting.Providers;
using System.Web.Mvc;

namespace RL.Areas.Accounting.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILog _log;
        private readonly AccountingManager _accountingManager;

        public HomeController(ILog log, AccountingManager accountingManager)
        {
            _log = log;
            _accountingManager = accountingManager;
            _accountingManager.WorkingFolder = HttpRuntime.AppDomainAppPath + @"uploads\";
        }

        public ActionResult Index()
        {
            //var banks = _accountingManager.BankList();
            //banks.ForEach(x => Response.Write(x.Name));

            //_log.Info("index");
            //_log.Debug("Test");
            return View();
        }

        [HttpPost]
        public ActionResult Upload(HttpPostedFileBase file, int bankId)
        {
            var fileProperties = _accountingManager.ExtractFileAndData(file, bankId);

            return Json(new { fileProperties, bankId }, "text/html", JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult BankList()
        {
            var banks = _accountingManager.BankList();
            //var transactionTypes = _accountingManager.TransactionTypes();
            //var transactionTypesAndIdentifiers = _accountingManager.TransactionTypesAndIdentifiers();

            return Json(banks, JsonRequestBehavior.AllowGet);
        }
    }
}