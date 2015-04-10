using System.Web.Http;
using API.Filters;
using Data;

[assembly: WebActivator.PreApplicationStartMethod(typeof(Api.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivator.ApplicationShutdownMethodAttribute(typeof(Api.App_Start.NinjectWebCommon), "Stop")]

namespace Api.App_Start
{
    using System;
    using System.Web;
    using System.Web.Http.Filters;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;

    public static class NinjectWebCommon 
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start() 
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }
        
        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }
        
        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
            kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

            RegisterServices(kernel);

            //Support WebApi Injection
            GlobalConfiguration.Configuration.DependencyResolver = new WebApiContrib.IoC.Ninject.NinjectResolver(kernel);
            //Support injection in WebAPI filters (Repo in filter LearningAuthorizeAttribute)
            GlobalConfiguration.Configuration.Services.Add(typeof(IFilterProvider), new NinjectWebApiFilterProvider(kernel));

            return kernel;
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind<DRSContext>().To<DRSContext>().InRequestScope();
            kernel.Bind<IDRSRepository>().To<DRSRepository>().InRequestScope();
        }        
    }
}
