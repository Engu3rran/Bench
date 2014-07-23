using FluentNHibernate;
using FluentNHibernate.Mapping;

namespace Bench.EntrepotPersistance.PostGresSQL
{
    public class VoieMapping : ClassMap<Voie>
    {
        public VoieMapping()
        {
            Table("VOIE");
            Id(x => x.Id, "ID").GeneratedBy.Assigned();
            Component(x => x.Nom, nom =>
            {
                nom.Map(x => x.Type, "TYPE");
                nom.Map(x => x.Libellé, "LIBELLE");
            });
            HasManyToMany(x => x.Numéros)
                .Table("VOIE_NUMERO_VOIE")
                .ParentKeyColumns
                    .Add("ID_VOIE")
                .ChildKeyColumns
                    .Add("ID_NUMERO_VOIE")
                .Cascade.AllDeleteOrphan();
            Map(x => x.IdCommune, "ID_COMMUNE");
        }
    }
}
