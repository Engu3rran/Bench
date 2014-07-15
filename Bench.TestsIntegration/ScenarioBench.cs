using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bench.Commandes;
using Bench.EntrepotPersistance.PostGresSQL;
using Bench.EntrepotPersistance.MongoDB;

namespace Bench.TestsIntegration.Commandes
{
    [TestClass]
    public class ScenarioBench
    {
        [TestMethod]
        public void ScenarioBench_1000VoiesPostGresSQL()
        {
            lancerLeBench(new CreerVoiesPostGresSQL(100, 1000), new SupprimerVoiesPostGresSQL());
        }

        [TestMethod]
        public void ScenarioBench_5000VoiesPostGresSQL()
        {
            lancerLeBench(new CreerVoiesPostGresSQL(500, 5000), new SupprimerVoiesPostGresSQL());
        }

        [TestMethod]
        public void ScenarioBench_25000VoiesPostGresSQL()
        {
            lancerLeBench(new CreerVoiesPostGresSQL(2500, 25000), new SupprimerVoiesPostGresSQL());
        }

        [TestMethod]
        public void ScenarioBench_1000VoiesMongoDB()
        {
            lancerLeBenchSansLaProjection(new CreerVoiesMongoDB(100, 1000), new SupprimerVoiesMongoDB());
        }

        [TestMethod]
        public void ScenarioBench_5000VoiesMongoDB()
        {
            lancerLeBenchSansLaProjection(new CreerVoiesMongoDB(500, 5000), new SupprimerVoiesMongoDB());
        }

        [TestMethod]
        public void ScenarioBench_25000VoiesMongoDB()
        {
            lancerLeBenchSansLaProjection(new CreerVoiesMongoDB(2500, 25000), new SupprimerVoiesMongoDB());
        }

        private static void lancerLeBench(ICreerVoiesMessage messageCréation, ISupprimerVoiesMessage messageSuppression)
        {
            string début = DateTime.Now.ToString("HH:mm:ss.fff");
            BusCommande bus = new BusCommande();
            bus.exécuter(messageCréation);
            string finEcriture = DateTime.Now.ToString("HH:mm:ss.fff");
            IList<Voie> voieRecherché = messageCréation.Entrepot.donnerLaCollection<Voie>().Where(x => x.Numéros.Any(y => y.Numéro == 20)).ToList();
            string finRecherche = DateTime.Now.ToString("HH:mm:ss.fff");
            IList<IGrouping<Guid, Voie>> projection = messageCréation.Entrepot.donnerLaCollection<Voie>().GroupBy(x => x.IdCommune).ToList();
            string finProjection = DateTime.Now.ToString("HH:mm:ss.fff");
            bus.exécuter(messageSuppression);
        }

        private static void lancerLeBenchSansLaProjection(ICreerVoiesMessage messageCréation, ISupprimerVoiesMessage messageSuppression)
        {
            string début = DateTime.Now.ToString("mm:ss.fff");
            BusCommande bus = new BusCommande();
            bus.exécuter(messageCréation);
            string finEcriture = DateTime.Now.ToString("mm:ss.fff");
            IList<Voie> voieRecherché = messageCréation.Entrepot.donnerLaCollection<Voie>().Where(x => x.Numéros.Any(y => y.Numéro == 20)).ToList();
            string finRecherche = DateTime.Now.ToString("mm:ss.fff");
            bus.exécuter(messageSuppression);
        }
    }

    public class CreerVoiesPostGresSQL : ICreerVoiesMessage
    {
        private IEntrepotPersistance _entrepot = new EntrepotPostGresSQL(new SessionPostGresSQL());
        
        public CreerVoiesPostGresSQL(int nombreDeCommunes, int nombreDeVoies)
        {
            NombreDeCommunes = nombreDeCommunes;
            NombreDeVoies = nombreDeVoies;
        }

        public int NombreDeCommunes { get; private set; }

        public int NombreDeVoies { get; private set; }

        public IEntrepotPersistance Entrepot
        {
            get { return _entrepot; }
        }
    }

    public class SupprimerVoiesPostGresSQL : ISupprimerVoiesMessage
    {
        private IEntrepotPersistance _entrepot = new EntrepotPostGresSQL(new SessionPostGresSQL());

        public IEntrepotPersistance Entrepot
        {
            get { return _entrepot; }
        }
    }

    public class CreerVoiesMongoDB : ICreerVoiesMessage
    {
        private IEntrepotPersistance _entrepot = new EntrepotMongoDB(new SessionMongoDB());

        public CreerVoiesMongoDB(int nombreDeCommunes, int nombreDeVoies)
        {
            NombreDeCommunes = nombreDeCommunes;
            NombreDeVoies = nombreDeVoies;
        }

        public int NombreDeCommunes { get; private set; }

        public int NombreDeVoies { get; private set; }

        public IEntrepotPersistance Entrepot
        {
            get { return _entrepot; }
        }
    }

    public class SupprimerVoiesMongoDB : ISupprimerVoiesMessage
    {
        private IEntrepotPersistance _entrepot = new EntrepotMongoDB(new SessionMongoDB());

        public IEntrepotPersistance Entrepot
        {
            get { return _entrepot; }
        }
    }
}
