using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bench.EntrepotPersistance.Mock;

namespace Bench.TestsUnitaires.Persistance
{
    [TestClass]
    public class TestEntrepotPersistanceMock
    {
        private IEntrepotPersistance _entrepot;

        [TestInitialize]
        public void initialiser()
        {
            _entrepot = new EntrepotPersistanceMock();
        }

        [TestMethod]
        public void TestEntrepotPersistanceMock_peutinsérerUnAgrégat()
        {
            int nombreInitial = _entrepot.donnerLaCollection<IEntite>().Count();
            IEntite entité = new EntiteMock();
            _entrepot.enregistrer<IEntite>(entité);
            int nombreFinal = _entrepot.donnerLaCollection<IEntite>().Count();
            Assert.AreEqual(nombreInitial + 1, nombreFinal);
        }

        [TestMethod]
        public void TestEntrepotPersistanceMock_peutModifierUnAgrégat()
        {
            int nombreInitial = _entrepot.donnerLaCollection<IEntite>().Count();
            IEntite entité = new EntiteMock();
            _entrepot.enregistrer<IEntite>(entité);
            _entrepot.enregistrer<IEntite>(entité);
            int nombreFinal = _entrepot.donnerLaCollection<IEntite>().Count();
            IEntite entitéRécupérée = _entrepot.donnerLaCollection<IEntite>().SingleOrDefault(x => x.Id == entité.Id);
            Assert.IsNotNull(entitéRécupérée);
            Assert.AreEqual(nombreInitial + 1, nombreFinal);
        }

        [TestMethod]
        public void TestEntrepotPersistanceMock_peutSupprimerUnAgrégat()
        {
            IEntite entité = new EntiteMock();
            _entrepot.enregistrer<IEntite>(entité);
            _entrepot.effacer<IEntite>(entité);
            IEntite entitéRécupérée = _entrepot.donnerLaCollection<IEntite>().SingleOrDefault(x => x.Id == entité.Id);
            Assert.IsNull(entitéRécupérée);
        }
    }

    class EntiteMock : IEntite
    {
        public Guid Id { get; set; }
    }
}
