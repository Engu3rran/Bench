using MongoDB.Bson.Serialization;

namespace Bench.EntrepotPersistance.MongoDB
{
    public class VoieMapping : IMappingMongo
    {
        public void mapper()
        {
            if (!BsonClassMap.IsClassMapRegistered(typeof(Voie)))
                BsonClassMap.RegisterClassMap<Voie>();
        }
    }
}
