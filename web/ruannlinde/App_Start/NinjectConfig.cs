using Ruann.Linde;
using Ruann.Linde.Database;
using Ruann.Linde.Database.Providers;
using Ruann.Linde.Database.Providers.CurriculumVitae;
using Ruann.Linde.Database.Providers.Lookup;
using WebActivatorEx;

[assembly: PreApplicationStartMethod(typeof(NinjectConfig), "Start")]
[assembly: ApplicationShutdownMethod(typeof(NinjectConfig), "Stop")]

namespace Ruann.Linde {
	using System;
	using System.Diagnostics.CodeAnalysis;
	using System.Reflection;
	using System.Web;

	using log4net;

	using Microsoft.Web.Infrastructure.DynamicModuleHelper;

	using Ninject;
	using Ninject.Extensions.Logging;
	using Ninject.Extensions.Logging.Log4net.Infrastructure;
	using Ninject.Web.Common;
	using Ninject.Web.Common.WebHost;

	[ExcludeFromCodeCoverage]
	public static class NinjectConfig {
		private static readonly Bootstrapper bootstrapper = new Bootstrapper();

		public static void Start() {
			DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
			DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));

			bootstrapper.Initialize(CreateKernel);
		}

		public static void Stop() {
			bootstrapper.ShutDown();
		}

		private static bool HasHttpContext() {
			if (HttpContext.Current != null) return true;

			return false;
		}

		private static IKernel CreateKernel() {
			var kernel = new StandardKernel(new NinjectSettings {
				InjectNonPublic = true, InjectParentPrivateProperties = true
			});

			try {
				kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
				kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

				RegisterServices(kernel);

				return kernel;
			}
			catch {
				kernel.Dispose();
				throw;
			}
		}

		private static void RegisterServices(IKernel kernel) {
			kernel.Load(Assembly.GetExecutingAssembly());
			kernel.Bind<ILogger>().To<Log4NetLogger>().InSingletonScope();
			kernel.Bind<ILog>().ToMethod(context => LogManager.GetLogger(context.Request.ParentContext?.Request.Service.FullName)).InSingletonScope();

			kernel.Bind<ApplicationDatabaseContext>().To<ApplicationDatabaseContext>().InSingletonScope();
			kernel.Bind<ICurriculumVitaeManager>().To<CurriculumVitaeManager>().InSingletonScope();
			kernel.Bind<LookupManager>().ToSelf().InSingletonScope();
			kernel.Bind<ILogProvider>().To<LogProvider>().InSingletonScope();
		}
	}
}