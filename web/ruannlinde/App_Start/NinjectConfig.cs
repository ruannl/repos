﻿using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Web;
using log4net;
using Microsoft.Web.Infrastructure.DynamicModuleHelper;
using Ninject;
using Ninject.Extensions.Logging;
using Ninject.Extensions.Logging.Log4net.Infrastructure;
using Ninject.Extensions.NamedScope;
using Ninject.Web.Common;
using Ninject.Web.Common.WebHost;
using RL;
using RL.Areas.Accounting.Providers;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(NinjectConfig), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(NinjectConfig), "Stop")]

namespace RL {
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
            var kernel = new StandardKernel(new NinjectSettings {InjectNonPublic = true, InjectParentPrivateProperties = true});
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

            kernel.Bind<AccountingContext>().To<AccountingContext>().InSingletonScope();
            kernel.Bind<AccountingManager>().ToSelf().InSingletonScope();
        }
    }
}