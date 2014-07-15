using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bench.EntrepotPersistance.Mock;
using Bench.Commandes;

namespace Bench.TestsUnitaires
{
    [TestClass]
    public class TestCommandes
    {
        [TestMethod]
        public void TestCommandes_peutInsérer100CommunesEt10000Voies()
        {
            ICreerVoiesMessage message = new CreerVoiesMessageMock();
            BusCommande bus = new BusCommande();
            bus.exécuter(message);
            Assert.AreEqual(100, message.Entrepot.donnerLaCollection<Commune>().Count());
            Assert.AreEqual(10000, message.Entrepot.donnerLaCollection<Voie>().Count());
        }

        [TestMethod]
        public void TestCommandes_peutSupprimerToutesLesCommunesEtVoies()
        {
            ISupprimerVoiesMessage message = new SupprimerVoiesMessageMock();
            Commune commune = new Commune();
            commune.initialiserAléatoirement();
            commune.définirUnEntrepotDePersistance(message.Entrepot);
            commune.enregistrer();
            Voie voie = new Voie();
            voie.initialiserAléatoirement(new Commune[] { commune });
            voie.définirUnEntrepotDePersistance(message.Entrepot);
            voie.enregistrer();
            BusCommande bus = new BusCommande();
            bus.exécuter(message);
            Assert.AreEqual(0, message.Entrepot.donnerLaCollection<Commune>().Count());
            Assert.AreEqual(0, message.Entrepot.donnerLaCollection<Voie>().Count());
        }
    }

    public class CreerVoiesMessageMock : ICreerVoiesMessage
    {
        private EntrepotPersistanceMock _entrepot = new EntrepotPersistanceMock();

        public int NombreDeCommunes
        {
            get { return 100; }
        }

        public int NombreDeVoies
        {
            get { return 10000; }
        }

        public IEntrepotPersistance Entrepot
        {
            get { return _entrepot; }
        }
    }

    public class SupprimerVoiesMessageMock : ISupprimerVoiesMessage
    {
        private EntrepotPersistanceMock _entrepot = new EntrepotPersistanceMock();

        public IEntrepotPersistance Entrepot
        {
            get { return _entrepot; }
        }
    }

}
