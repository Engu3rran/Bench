using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bench.EntrepotPersistance.PostGresSQL;
using Bench.EntrepotPersistance.MongoDB;

namespace Bench.TestsIntegration
{
    [TestClass]
    public class TestPersistance
    {
        [TestMethod]
        public void TestPersistance_CRUDCommunePostGresSQL()
        {
            EntrepotPostGresSQL entrepot = new EntrepotPostGresSQL(new SessionPostGresSQL());
            testerLeCRUDPourLesCommunes(entrepot);
        }

        [TestMethod]
        public void TestPersistance_CRUDVoiePostGresSQL()
        {
            EntrepotPostGresSQL entrepot = new EntrepotPostGresSQL(new SessionPostGresSQL());
            testerLeCRUDPourLesVoies(entrepot);
        }

        [TestMethod]
        public void TestPersistance_CRUDCommuneMongoDB()
        {
            EntrepotMongoDB entrepot = new EntrepotMongoDB(new SessionMongoDB());
            testerLeCRUDPourLesCommunes(entrepot);
        }

        [TestMethod]
        public void TestPersistance_CRUDVoieMongoDB()
        {
            EntrepotMongoDB entrepot = new EntrepotMongoDB(new SessionMongoDB());
            testerLeCRUDPourLesVoies(entrepot);
        }

        private void testerLeCRUDPourLesCommunes(IEntrepotPersistance entrepot)
        {
            Commune commune = new Commune();
            commune.définirUnEntrepotDePersistance(entrepot);
            commune.initialiserAléatoirement();
            string nomInitial = commune.Nom;
            commune.enregistrer();
            Commune communeEnregistrée = entrepot.donnerLaCollection<Commune>().Single(x => x.Id == commune.Id);
            Assert.AreEqual(commune.Nom, communeEnregistrée.Nom);
            commune.initialiserAléatoirement();
            commune.enregistrer();
            Commune communeRechargée = entrepot.donnerLaCollection<Commune>().Single(x => x.Id == commune.Id);
            Assert.AreEqual(commune.Nom, communeRechargée.Nom);
            Assert.AreNotEqual(nomInitial, communeRechargée.Nom);
            commune.effacer();
            Assert.IsFalse(entrepot.donnerLaCollection<Commune>().Any(x => x.Id == commune.Id));
        }

        private void testerLeCRUDPourLesVoies(IEntrepotPersistance entrepot)
        {
            Commune commune = new Commune();
            commune.initialiserAléatoirement();
            commune.définirUnEntrepotDePersistance(entrepot);
            commune.enregistrer();
            Commune[] communes = new Commune[] { commune };
            Voie voie = new Voie();
            voie.initialiserAléatoirement(communes);
            voie.définirUnEntrepotDePersistance(entrepot);
            int nombreDeNumérosInitial = voie.Numéros.Count;
            string libelléInitial = voie.Nom.Libellé;
            voie.enregistrer();
            Voie voieEnregistrée = entrepot.donnerLaCollection<Voie>().Single(x => x.Id == voie.Id);
            Assert.AreEqual(voie.Numéros.Count, voieEnregistrée.Numéros.Count);
            Assert.AreEqual(voie.Nom.Libellé, voieEnregistrée.Nom.Libellé);
            voie.initialiserAléatoirement(communes);
            voie.enregistrer();
            Voie voieRechargée = entrepot.donnerLaCollection<Voie>().Single(x => x.Id == voie.Id);
            Assert.AreEqual(voie.Numéros.Count, voieRechargée.Numéros.Count);
            Assert.AreEqual(voie.Nom.Libellé, voieRechargée.Nom.Libellé);
            Assert.AreNotEqual(nombreDeNumérosInitial, voieRechargée.Numéros.Count);
            Assert.AreNotEqual(libelléInitial, voieRechargée.Nom.Libellé);
            voie.effacer();
            Assert.IsFalse(entrepot.donnerLaCollection<Voie>().Any(x => x.Id == voie.Id));
            commune.effacer();
        }
    }
}
