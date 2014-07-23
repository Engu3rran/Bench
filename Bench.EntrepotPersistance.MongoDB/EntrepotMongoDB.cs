using System.Linq;
using System.Reflection;
using MongoDB.Driver;
using MongoDB.Driver.Linq;


namespace Bench.EntrepotPersistance.MongoDB
{
    public class EntrepotMongoDB : IEntrepotPersistance
    {
        private MongoDatabase _session;

        public EntrepotMongoDB(SessionMongoDB session)
        {
            _session = session.donnerLaSession();
        }

        public void enregistrer<T>(T entité) where T : IEntite
        {
            _session
                .GetCollection(trouverLeNomDeLaCollectionCorrespondante<T>())
                .Save(entité);
        }

        public void effacer<T>(T entité) where T : IEntite
        {
            MongoCollection<T> collection = _session.GetCollection<T>(trouverLeNomDeLaCollectionCorrespondante<T>());
            IQueryable<T> requêteSuppression = collection.AsQueryable<T>().Where(x => x.Id == entité.Id);
            IMongoQuery requêteSuppressionMongo = ((MongoQueryable<T>)requêteSuppression).GetMongoQuery();
            collection.Remove(requêteSuppressionMongo);
        }

        public IQueryable<T> donnerLaCollection<T>() where T : IEntite
        {
            return _session
                .GetCollection(trouverLeNomDeLaCollectionCorrespondante<T>())
                .AsQueryable<T>();
        }

        private string trouverLeNomDeLaCollectionCorrespondante<T>()
        {
            return typeof(T).GetTypeInfo().Name;
        }
    }
}
