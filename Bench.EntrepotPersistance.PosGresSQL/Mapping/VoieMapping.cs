﻿using FluentNHibernate;
using FluentNHibernate.Mapping;

namespace Bench.EntrepotPersistance.PostGresSQL
{
    public class VoieMapping : ClassMap<Voie>
    {
        public VoieMapping()
        {
            Table("VOIE");
            Id(x => x.Id, "ID").GeneratedBy.GuidComb();
            Component(x => x.Nom, nom =>
            {
                nom.Map(x => x.Type, "TYPE");
                nom.Map(x => x.Libellé, "LIBELLE");
            });
            HasMany(x => x.Numéros)
                .KeyColumn("ID_VOIE")
                .Cascade.AllDeleteOrphan();
            Map(x => x.IdCommune, "ID_COMMUNE");
        }
    }
}
