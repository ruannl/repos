using System.Reflection;
using System.Web;
using System.Web.Http;
using log4net;
using Microsoft.Owin.Security.OAuth;

using Newtonsoft.Json.Serialization;
using Ninject.Extensions.Logging;
using Ninject.Extensions.Logging.Log4net.Infrastructure;
using Ninject.Extensions.NamedScope;
using Ninject.Web.Common;
using Ninject.Web.WebApi.OwinHost;


namespace RL {
    using System;
    using Ninject;
    using Owin;

    public partial class Startup {

        public void ConfigureNinject(IAppBuilder app) {


            //DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            //DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));

            log4net.Config.XmlConfigurator.Configure();

            var config = new HttpConfiguration();
            config.SuppressDefaultHostAuthentication();
            config.MapHttpAttributeRoutes();

            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));
            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            config.Routes.MapHttpRoute("DefaultApi", "api/{controller}/{id}", new { id = RouteParameter.Optional, controller = "values" });

            //config.DependencyResolver = new NinjectDependencyResolver();
            //config.DependencyResolver = new OwinNinjectDependencyResolver(CreateKernel());
            //app.UseNinjectWebApi(config);
        }

        private static bool HasHttpContext() {
            if (HttpContext.Current != null) return true;

            return false;
        }

        private static IKernel CreateKernel() {
            var kernel = new StandardKernel(new NinjectSettings {
                InjectNonPublic = true,
                InjectParentPrivateProperties = true
            });

            try {
                RegisterServices(kernel);
                return kernel;
            }
            catch (Exception exception) {
                Console.WriteLine(exception);
                kernel.Dispose();
                throw;
            }
        }

        private static void RegisterServices(IKernel kernel) {
            kernel.Load(Assembly.GetExecutingAssembly());

            //kernel.Bind<ServiceHost>().To<NinjectServiceHost>();
            //log4net
            kernel.Bind<ILogger>().To<Log4NetLogger>().InSingletonScope();
            kernel.Bind<ILog>().ToMethod(context => LogManager.GetLogger(context.Request.ParentContext?.Request.Service.FullName)).InSingletonScope();

         
        }
    }
}