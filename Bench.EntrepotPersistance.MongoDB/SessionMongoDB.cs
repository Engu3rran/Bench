using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Configuration;
using MongoDB.Driver;

namespace Bench.EntrepotPersistance.MongoDB
{
    public class SessionMongoDB
    {
        private const string CLE_CHAINE_CONNEXION_MONGODB = "connectionStringMG";
        private const string NOM_BASE = "nomBDDMongo";
        private static readonly MongoDatabase _baseDeDonnéesMongo = seConnecterALaBaseDeDonnées();

        public static MongoDatabase seConnecterALaBaseDeDonnées()
        {
            initialiserLeMapping();
            string chaîneDeConnexion = récupérerLaChaîneDeConnexion();
            return new MongoClient(chaîneDeConnexion)
                .GetServer()
                .GetDatabase(
                    MongoUrl
                        .Create(chaîneDeConnexion)
                        .DatabaseName
                    );
        }

        private static void initialiserLeMapping()
        {
            IEnumerable<IMappingMongo> maps = Assembly
                .GetExecutingAssembly()
                .GetTypes()
                .Where(x => x
                    .GetInterfaces()
                    .Contains(typeof(IMappingMongo)))
                .Select(x => (IMappingMongo)Activator.CreateInstance(x));
            foreach (IMappingMongo map in maps)
                map.mapper();
        }

        private static string récupérerLaChaîneDeConnexion()
        {
            return ConfigurationManager.AppSettings[CLE_CHAINE_CONNEXION_MONGODB];
        }

        public SessionMongoDB() { }

        public MongoDatabase donnerLaSession()
        {
            return _baseDeDonnéesMongo;
        }
    }
}
