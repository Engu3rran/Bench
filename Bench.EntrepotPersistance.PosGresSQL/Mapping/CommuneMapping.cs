using FluentNHibernate;
using FluentNHibernate.Mapping;

namespace Bench.EntrepotPersistance.PostGresSQL
{
    public class CommuneMapping : ClassMap<Commune>
    {
        public CommuneMapping()
        {
            Table("COMMUNE");
            Id(x => x.Id, "ID").GeneratedBy.Assigned();
            Map(x => x.Nom, "NOM");
            Component(x => x.Code, code =>
            {
                code.Map(x => x.Valeur, "CODE");
            });
        }
    }
}
