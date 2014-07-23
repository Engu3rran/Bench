using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bench.TestsUnitaires.Domaine
{
    [TestClass]
    public class TestCommune : TestUnitaire
    {
        [TestMethod]
        public void TestCommune_peutGénérerUneCommuneAvecUnCodeAléatoire()
        {
            Commune commune1 = new Commune();
            commune1.initialiserAléatoirement();
            Commune commune2 = new Commune();
            commune2.initialiserAléatoirement();
            Assert.IsFalse(string.IsNullOrEmpty(commune1.Nom));
            Assert.IsFalse(string.IsNullOrEmpty(commune1.Code.ToString()));
            Assert.AreEqual(5, commune1.Code.ToString().Length);
            Assert.IsFalse(commune1.Nom == commune2.Nom
                && commune1.Code == commune2.Code);
        }
    }
}
