using FluentNHibernate;
using FluentNHibernate.Mapping;

namespace Bench.EntrepotPersistance.PostGresSQL
{
    public class NumeroVoieMapping : ClassMap<NumeroVoie>
    {
        public NumeroVoieMapping()
        {
            Table("NUMERO_VOIE");
            Id(x => x.Id, "ID").GeneratedBy.Assigned();
            Map(x => x.Numéro, "NUMERO");
            Map(x => x.Répétition, "REPETITION").CustomType<int>();
        }
    }
}
