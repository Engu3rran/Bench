using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject.Modules;
using Bench.EntrepotPersistance.Mock;
using Bench.Commandes;

namespace Bench.TestsUnitaires
{
    public class TestUnitaire
    {
        [TestInitialize]
        public void initialiserLaFabrique()
        {
            FabriqueGenerique.configurer(new ConfigurationFabrique());
            BusCommande bus = FabriqueGenerique.constuire<BusCommande>();
            bus.initialiser();
        }
    }

    public class ConfigurationFabrique : NinjectModule
    {
        public override void Load()
        {
            Bind<IEntrepotPersistance>().To<EntrepotPersistanceMock>().InSingletonScope();
            Bind<IEntrepotReporting>().To<EntrepotPersistanceMock>().InSingletonScope();
            Bind<BusCommande>().ToSelf().InSingletonScope();
        }
    }
}
