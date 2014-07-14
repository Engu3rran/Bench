using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bench.TestsUnitaires.Domaine
{
    [TestClass]
    public class TestVoie
    {
        [TestMethod]
        public void TestRue_peutGénérerDesRuesAléatoirement()
        {
            IList<Commune> communes = new List<Commune>();
            for(var i = 0; i < 10; i++)
            {
                Commune commune = new Commune();
                commune.initialiserAléatoirement();
                communes.Add(commune);
            }
            Commune[] communesPourRues = communes.ToArray();
            Voie rue1 = new Voie();
            rue1.initialiserAléatoirement(communesPourRues);
            Voie rue2 = new Voie();
            rue2.initialiserAléatoirement(communesPourRues);
            Assert.AreNotEqual(0, rue1.Numéros.Count);
            Assert.IsFalse(string.IsNullOrEmpty(rue1.Nom.Type));
            Assert.IsFalse(string.IsNullOrEmpty(rue1.Nom.Libellé));
            Assert.IsNotNull(rue1.IdCommune);
            Assert.IsFalse(rue1.Nom.Libellé == rue2.Nom.Libellé
                && rue1.Nom.Type == rue2.Nom.Type
                && rue1.Numéros.Count == rue2.Numéros.Count
                && rue1.IdCommune == rue2.IdCommune);
        }
    }
}
