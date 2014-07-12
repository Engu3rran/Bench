using System;
using System.Collections.Generic;

namespace Bench
{
    public class Rue : Agregat<Rue>
    {
        public IList<NumeroRue> Numéros { get; set; }
        public NomRue Nom { get; set; }
        public Guid IdCommune { get; set; }
    }
}
