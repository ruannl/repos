using System;
using System.IO.Compression;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using log4net;

namespace Ruann.Linde {
	public class MvcApplication : HttpApplication {
		//private const string RootDocument = "/Home/Index";
		public static readonly ILog Log = LogManager.GetLogger(typeof(MvcApplication));

		protected void Application_Start() {

			AreaRegistration.RegisterAllAreas();
			FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
			RouteConfig.RegisterRoutes(RouteTable.Routes);
			BundleConfig.RegisterBundles(BundleTable.Bundles);

			log4net.Config.XmlConfigurator.Configure();
			Log.Debug("Application_Start");
			//var container = new Container();
			//container.Options.DefaultScopedLifestyle = new WebRequestLifestyle();

			//container.Register<AccountingContext, AccountingContext>(Lifestyle.Scoped);
			//container.Register<ILogger, Log4NetLogger>(Lifestyle.Singleton);

			//container.RegisterMvcControllers(Assembly.GetExecutingAssembly());

			//container.Verify();

			//DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));
		}

		protected void Application_BeginRequest(object sender, EventArgs e) {
			LogicalThreadContext.Properties["Request"] = new { HostAddress = Request.UserHostAddress, Request.RawUrl, Request.QueryString };
			LogicalThreadContext.Properties["RequestInfo"] = new WebRequestInfo();

			//var app = (HttpApplication)sender;
			//var encodings = app.Request.Headers.Get("Accept-Encoding");
			//if (encodings != null) {
			//	// Check the browser accepts deflate or gzip (deflate takes preference)
			//	encodings = encodings.ToLower();
			//	if (encodings.Contains("deflate")) {
			//		app.Response.Filter = new DeflateStream(app.Response.Filter, CompressionMode.Compress);
			//		app.Response.AppendHeader("Content-Encoding", "deflate");
			//	}
			//	else if (encodings.Contains("gzip")) {
			//		app.Response.Filter = new GZipStream(app.Response.Filter, CompressionMode.Compress);
			//		app.Response.AppendHeader("Content-Encoding", "gzip");
			//	}
			//}
		}

		protected void MvcApplication_AuthenticateRequest(object sender, EventArgs e) {
			try {
				LogicalThreadContext.Properties["User"] = User;
			}
			catch (Exception ex) {
				Log.Error(ex);
			}
		}

		public class WebRequestInfo {
			public override string ToString() {
				return HttpContext.Current?.Request?.RawUrl + ", " + HttpContext.Current?.Request?.UserAgent;
			}
		}
	}
}