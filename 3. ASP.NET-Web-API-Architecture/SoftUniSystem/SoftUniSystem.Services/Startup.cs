[assembly: Microsoft.Owin.OwinStartup(typeof(SoftUniSystem.Services.Startup))]

namespace SoftUniSystem.Services
{
    using System.Data.Entity;
    using System.Reflection;
    using System.Web.Http;

    using Ninject;
    using Ninject.Web.Common.OwinHost;
    using Ninject.Web.WebApi.OwinHost;

    using Owin;

    using SoftUniSystem.Data;

    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            
            // GlobalConfiguration.Configuration.DependencyResolver = 
            app.UseNinjectMiddleware(this.CreateKernel).UseNinjectWebApi(GlobalConfiguration.Configuration);
        }

        private IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            kernel.Load(Assembly.GetExecutingAssembly());

            this.RegisterMappings(kernel);

            return kernel;
        }

        private void RegisterMappings(IKernel kernel)
        {
            kernel.Bind<DbContext>().To<ApplicationDbContext>();
            kernel.Bind<ISoftUniSystemData>().To<SoftUniSystemData>();
                // .WithConstructorArgument("context", c => new ApplicationDbContext());
        }
    }
}
