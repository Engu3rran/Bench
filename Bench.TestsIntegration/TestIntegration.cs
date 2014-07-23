using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject.Modules;
using Bench.EntrepotPersistance.MongoDB;
using Bench.EntrepotPersistance.PostGresSQL;
using Bench.Commandes;

namespace Bench.TestsIntegration
{
    public class TestIntegration
    {
        [TestInitialize]
        public void initialiserLaFabrique()
        {
            FabriqueGenerique.configurer(new ConfigurationFabriqueScenario());
            BusCommande bus = FabriqueGenerique.constuire<BusCommande>();
            bus.initialiser();
        }
    }

    public class ConfigurationFabriqueScenario : NinjectModule
    {
        public override void Load()
        {
            Bind<SessionMongoDB>().ToSelf().InSingletonScope();
            Bind<SessionPostGresSQL>().ToSelf().InSingletonScope();
            Bind<IEntrepotPersistance>().To<EntrepotMongoDB>();
            Bind<IEntrepotReporting>().To<EntrepotPostGresSQL>();
            Bind<BusCommande>().ToSelf().InSingletonScope();
        }
    }
}
