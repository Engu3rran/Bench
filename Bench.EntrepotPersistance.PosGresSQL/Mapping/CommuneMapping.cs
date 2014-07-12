using FluentNHibernate;
using FluentNHibernate.Mapping;

namespace Bench.EntrepotPersistance.PostGresSQL
{
    public class CommuneMapping : ClassMap<Commune>
    {
        public CommuneMapping()
        {
            Table("COMMUNE");
            Id(x => x.Id, "ID").GeneratedBy.GuidComb();
            Map(x => x.Nom, "NOM");
            Map(x => x.Code._valeur, "CODE");
        }
    }
}
