using Bench.Commandes;
using Bench.EntrepotPersistance.MongoDB;
using Bench.EntrepotPersistance.PostGresSQL;
using Ninject.Modules;
using Ninject.Web.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Bench.Web
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            FabriqueGenerique.configurer(new ConfigurationFabrique());
            FabriqueGenerique.constuire<BusCommande>().initialiser();
        }
    }

    public class ConfigurationFabrique : NinjectModule
    {
        public override void Load()
        {
            Bind<SessionMongoDB>().ToSelf().InRequestScope();
            Bind<SessionPostGresSQL>().ToSelf().InRequestScope();
            Bind<IEntrepotPersistance>().To<EntrepotMongoDB>();
            Bind<IEntrepotReporting>().To<EntrepotPostGresSQL>();
            Bind<BusCommande>().ToSelf().InSingletonScope();
        }
    }
}
