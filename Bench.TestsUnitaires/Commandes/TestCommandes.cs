using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bench.EntrepotPersistance.Mock;
using Bench.Commandes;
using System.Threading;

namespace Bench.TestsUnitaires
{
    [TestClass]
    public class TestCommandes : TestUnitaire
    {
        [TestMethod]
        public void TestCommandes_créerUneCommune()
        {
            FabriqueGenerique.constuire<BusCommande>().exécuter(new CreerCommuneMessageTest());
            Thread.Sleep(200);
            IEntrepotPersistance entrepotPersistance = FabriqueGenerique.constuire<IEntrepotPersistance>();
            Assert.AreEqual(1, entrepotPersistance.donnerLaCollection<Commune>().Count());
            IEntrepotReporting entrepotReporting = FabriqueGenerique.constuire<IEntrepotReporting>();
            Assert.AreEqual(1, entrepotReporting.donnerLaCollection<Commune>().Count());
        }

        [TestMethod]
        public void TestCommandes_créerUneCommunePourReporting()
        {
            FabriqueGenerique.constuire<BusCommande>().exécuter(new CreerCommuneReportingMessageTest());
            IEntrepotPersistance entrepotPersistance = FabriqueGenerique.constuire<IEntrepotPersistance>();
            Assert.AreEqual(0, entrepotPersistance.donnerLaCollection<Commune>().Count());
            IEntrepotReporting entrepotReporting = FabriqueGenerique.constuire<IEntrepotReporting>();
            Assert.AreEqual(1, entrepotReporting.donnerLaCollection<Commune>().Count());
        }

        [TestMethod]
        public void TestCommandes_créerUneVoie()
        {
            Thread.Sleep(200);
            FabriqueGenerique.constuire<BusCommande>().exécuter(new CreerCommuneMessageTest());
            FabriqueGenerique.constuire<BusCommande>().exécuter(new CreerVoieMessageTest());
            IEntrepotPersistance entrepotPersistance = FabriqueGenerique.constuire<IEntrepotPersistance>();
            Assert.AreEqual(1, entrepotPersistance.donnerLaCollection<Voie>().Count());
            IEntrepotReporting entrepotReporting = FabriqueGenerique.constuire<IEntrepotReporting>();
            Assert.AreEqual(1, entrepotReporting.donnerLaCollection<Voie>().Count());
        }

        [TestMethod]
        public void TestCommandes_créerUneVoiePourReporting()
        {
            FabriqueGenerique.constuire<BusCommande>().exécuter(new CreerCommuneMessageTest());
            FabriqueGenerique.constuire<BusCommande>().exécuter(new CreerVoieReportingMessageTest());
            IEntrepotPersistance entrepotPersistance = FabriqueGenerique.constuire<IEntrepotPersistance>();
            Assert.AreEqual(0, entrepotPersistance.donnerLaCollection<Voie>().Count());
            IEntrepotReporting entrepotReporting = FabriqueGenerique.constuire<IEntrepotReporting>();
            Assert.AreEqual(1, entrepotReporting.donnerLaCollection<Voie>().Count());
        }

        [TestMethod]
        public void TestCommandes_créerUneVoieParEntrepotPersistance()
        {
            FabriqueGenerique.constuire<BusCommande>().exécuter(new CreerCommuneMessageTest());
            FabriqueGenerique.constuire<BusCommande>().exécuter(new CreerVoieEntrepotPersistanceMessageTest());
            IEntrepotPersistance entrepotPersistance = FabriqueGenerique.constuire<IEntrepotPersistance>();
            Assert.AreEqual(1, entrepotPersistance.donnerLaCollection<Voie>().Count());
            IEntrepotReporting entrepotReporting = FabriqueGenerique.constuire<IEntrepotReporting>();
            Assert.AreEqual(0, entrepotReporting.donnerLaCollection<Voie>().Count());
        }

        [TestMethod]
        public void TestCommandes_créerUneVoieParEntrepotReporting()
        {
            FabriqueGenerique.constuire<BusCommande>().exécuter(new CreerCommuneMessageTest());
            FabriqueGenerique.constuire<BusCommande>().exécuter(new CreerVoieEntrepotReportingMessageTest());
            IEntrepotPersistance entrepotPersistance = FabriqueGenerique.constuire<IEntrepotPersistance>();
            Assert.AreEqual(0, entrepotPersistance.donnerLaCollection<Voie>().Count());
            IEntrepotReporting entrepotReporting = FabriqueGenerique.constuire<IEntrepotReporting>();
            Assert.AreEqual(1, entrepotReporting.donnerLaCollection<Voie>().Count());
        }

        [TestMethod]
        public void TestCommandes_supprimerLaBase()
        {
            FabriqueGenerique.constuire<BusCommande>().exécuter(new CreerCommuneMessageTest());
            FabriqueGenerique.constuire<BusCommande>().exécuter(new CreerVoieMessageTest());
            FabriqueGenerique.constuire<BusCommande>().exécuter(new SupprimerVoirieMessageTest());
            IEntrepotPersistance entrepotPersistance = FabriqueGenerique.constuire<IEntrepotPersistance>();
            Assert.AreEqual(0, entrepotPersistance.donnerLaCollection<Commune>().Count());
            Assert.AreEqual(0, entrepotPersistance.donnerLaCollection<Voie>().Count());
            IEntrepotReporting entrepotReporting = FabriqueGenerique.constuire<IEntrepotReporting>();
            Assert.AreEqual(0, entrepotReporting.donnerLaCollection<Commune>().Count());
            Assert.AreEqual(0, entrepotReporting.donnerLaCollection<Voie>().Count());
        }
    }

    public class CreerCommuneMessageTest : ICreerCommuneMessage
    {

    }

    public class CreerCommuneReportingMessageTest : ICreerCommuneReportingMessage
    {

    }

    public class CreerVoieMessageTest : ICreerVoieMessage
    {

    }

    public class CreerVoieReportingMessageTest: ICreerVoieReportingMessage
    {

    }

    public class SupprimerVoirieMessageTest : ISupprimerVoirieMessage
    {

    }


    public class CreerVoieEntrepotPersistanceMessageTest : ICreerVoieEntrepotMessage
    {
        public IEntrepotPersistance Entrepot
        {
            get
            {
                return FabriqueGenerique.constuire<IEntrepotPersistance>();
            }
        }
    }

    public class CreerVoieEntrepotReportingMessageTest : ICreerVoieEntrepotMessage
    {
        public IEntrepotPersistance Entrepot
        {
            get
            {
                return FabriqueGenerique.constuire<IEntrepotReporting>();
            }
        }
    }

}
