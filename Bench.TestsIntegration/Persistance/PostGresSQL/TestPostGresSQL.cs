using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bench.EntrepotPersistance.PostGresSQL;

namespace Bench.TestsIntegration.Persistance.PostGresSQL
{
    [TestClass]
    public class TestPostGresSQL
    {
        private EntrepotPostGresSQL _entrepot;

        [TestInitialize]
        public void initialiserLEntrepot()
        {
            _entrepot = new EntrepotPostGresSQL(
                new SessionPostGresSQL()
                );
        }

        [TestMethod]
        public void TestPostGresSQL_peutFaireDuCRUDSurLesCommunes()
        {
            Commune commune = new Commune();
            commune.définirUnEntrepotDePersistance(_entrepot);
            commune.initialiserAléatoirement();
            string nomInitial = commune.Nom;
            commune.enregistrer();
            Commune communeEnregistrée = _entrepot.donnerLaCollection<Commune>().Single(x => x.Id == commune.Id);
            Assert.AreEqual(commune.Nom, communeEnregistrée.Nom);
            commune.initialiserAléatoirement();
            commune.enregistrer();
            Commune communeRechargée = _entrepot.donnerLaCollection<Commune>().Single(x => x.Id == commune.Id);
            Assert.AreEqual(commune.Nom, communeRechargée.Nom);
            Assert.AreNotEqual(nomInitial, communeRechargée.Nom);
            commune.effacer();
            Assert.IsFalse(_entrepot.donnerLaCollection<Commune>().Any(x => x.Id == commune.Id));
        }

        [TestMethod]
        public void TestPostGresSQL_peutFaireDuCRUDSurlesVoies()
        {
            Commune commune = new Commune();
            commune.initialiserAléatoirement();
            commune.définirUnEntrepotDePersistance(_entrepot);
            commune.enregistrer();
            Commune[] communes = new Commune[] { commune };
            Voie voie = new Voie();
            voie.initialiserAléatoirement(communes);
            voie.définirUnEntrepotDePersistance(_entrepot);
            int nombreDeNumérosInitial = voie.Numéros.Count;
            string libelléInitial = voie.Nom.Libellé;
            voie.enregistrer();
            Voie voieEnregistrée = _entrepot.donnerLaCollection<Voie>().Single(x => x.Id == voie.Id);
            Assert.AreEqual(voie.Numéros.Count, voieEnregistrée.Numéros.Count);
            Assert.AreEqual(voie.Nom.Libellé, voieEnregistrée.Nom.Libellé);
            voie.initialiserAléatoirement(communes);
            voie.enregistrer();
            Voie voieRechargée = _entrepot.donnerLaCollection<Voie>().Single(x => x.Id == voie.Id);
            Assert.AreEqual(voie.Numéros.Count, voieRechargée.Numéros.Count);
            Assert.AreEqual(voie.Nom.Libellé, voieRechargée.Nom.Libellé);
            Assert.AreNotEqual(nombreDeNumérosInitial, voieRechargée.Numéros.Count);
            Assert.AreNotEqual(libelléInitial, voieRechargée.Nom.Libellé);
            voie.effacer();
            Assert.IsFalse(_entrepot.donnerLaCollection<Voie>().Any(x => x.Id == voie.Id));
            commune.effacer();
        }
    }
}
