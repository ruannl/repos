namespace Ruann.Linde.Controllers
{
	using System;
	using System.Data;
	using System.Diagnostics;
	using System.Web.Mvc;
	using log4net;
	using Ruann.Linde.Database;
    using Ruann.Linde.Database.Models;
    using Ruann.Linde.Database.Providers;
    using Ruann.Linde.Database.Providers.CurriculumVitae;
    using Ruann.Linde.Database.Providers.Lookup;
	using Ruann.Linde.Extensions;

	[Authorize]
	public class HomeController : Controller
	{
		private static ApplicationDatabaseContext _applicationDatabaseContext;

		private static ILogProvider _logProvider;
		private static LookupProvider _lookupProvider;

		internal CurriculumVitaeManager _curriculumVitaeManager;
		internal ILog Log = LogManager.GetLogger(typeof(HomeController).Name);

		/// <summary>
		///     Home Controller
		/// </summary>
		/// <param name = "applicationDatabaseContext" ></param>
		/// <param name = "logProvider" ></param>
		/// <param name = "lookupProvider" ></param>
		public HomeController(ApplicationDatabaseContext applicationDatabaseContext, ILogProvider logProvider, LookupProvider lookupProvider, CurriculumVitaeManager cvManager)
		{
			_logProvider = logProvider;
			_lookupProvider = lookupProvider;
			_applicationDatabaseContext = applicationDatabaseContext;
			_curriculumVitaeManager = cvManager;
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

		public JsonResult GetLogContent()
		{
			try
			{
				//var content = _logProvider.GetLog(null,null);
				var content = new string('*', 1);
				return Json(new
				{
					content
				}
						  , "json"
						  , JsonRequestBehavior.AllowGet
				);
			}
			catch (Exception e)
			{
				throw new DomainException(e.Message);
			}
		}

		public JsonResult GetSettings()
		{
			var connection = _applicationDatabaseContext.Database.Connection;

			try
			{
				if (_applicationDatabaseContext.Database.Exists())
				{
					if (connection.State != ConnectionState.Open) connection.Open();

					var connectionObject = new {
						connection.ConnectionString,
						connection.ConnectionTimeout,
						connection.DataSource,
						connection.ServerVersion,
						connection.State
					};

					Debug.WriteLine($"connection established to {_applicationDatabaseContext.Database.Connection}");
					connection.Close();
					return Json(new
					{
						connectionObject
					}
							  , "json"
							  , JsonRequestBehavior.AllowGet
					);
				}
				else
				{
					_applicationDatabaseContext.Database.Initialize(true);

					throw new Exception($"{_applicationDatabaseContext.Database.Connection.Database} does not exist");
				}
			}
			catch (Exception e)
			{
				throw new DomainException(e.Message, e);
			}
			finally
			{
				if (connection.State != ConnectionState.Closed)
				{
					connection.Close();
					connection.Dispose();
				}
			}
		}

		//private ApplicationSignInManager SignInManager { get => _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>(); set => _signInManager = value; }
		// private ApplicationUserManager UserManager { get => _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>(); set => _userManager = value; }

		public ActionResult Index()
		{
			return View();
		}

		public JsonResult ThrowDomainException()
		{
			Log.Info("logging info from home controller");
			Log.Error("logging error from home controller");
			Log.Fatal("logging fatal error from home controller");
			return Json(new { }
					  , "json"
					  , JsonRequestBehavior.AllowGet
			);
		}

		[HttpPost]
		public JsonResult SubmitMessage(string name, string surname, string email, string message)
		{
			try
			{
				Log.Info("CV Home Controller SubmitMessage");
				var contactMessage = _curriculumVitaeManager.AddContactMessage(new ContactMessage { FirstName = name, LastName = surname, Email = email, Message = message });
				Log.Info(contactMessage);

				return Json(contactMessage);
			}
			catch (Exception e)
			{
				Log.Error(e);
				throw;
			}
		}
	}
}