using MongoDB.Bson.Serialization;

namespace Bench.EntrepotPersistance.MongoDB
{
    public class CommuneMapping : IMappingMongo
    {
        public void mapper()
        {
            if (!BsonClassMap.IsClassMapRegistered(typeof(Commune)))
                BsonClassMap.RegisterClassMap<Commune>();
        }
    }
}
