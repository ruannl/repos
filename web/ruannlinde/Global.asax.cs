using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

using log4net;

namespace RL {
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
            LogicalThreadContext.Properties["Request"] = new {
                HostAddress = Request.UserHostAddress
              , Request.RawUrl
              , Request.QueryString
            };

            LogicalThreadContext.Properties["RequestInfo"] = new WebRequestInfo();
            //    string url = Request.Url.LocalPath;
            //    if (!System.IO.File.Exists(Context.Server.MapPath(url)))
            //        Context.RewritePath(RootDocument);
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