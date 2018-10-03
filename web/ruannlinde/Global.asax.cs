using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace RL {
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start() {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            //var container = new Container();
            //container.Options.DefaultScopedLifestyle = new WebRequestLifestyle();

            //container.Register<AccountingContext, AccountingContext>(Lifestyle.Scoped);
            //container.Register<ILogger, Log4NetLogger>(Lifestyle.Singleton);

            //container.RegisterMvcControllers(Assembly.GetExecutingAssembly());

            //container.Verify();

            //DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));
        }
    }
}