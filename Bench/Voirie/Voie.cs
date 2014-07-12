using System;
using System.Collections.Generic;

namespace Bench
{
    public class Voie : Agregat<Voie>
    {
        private const int NOMBRE_NUMEROS_RUE_MIN = 5;
        private const int NOMBRE_NUMEROS_RUE_MAX = 20;
        private static readonly Random _hasard = new Random();

        public IList<NumeroVoie> Numéros { get; set; }
        public NomVoie Nom { get; set; }
        public Guid IdCommune { get; set; }

        public void initialiserAléatoirement(Commune[] communes)
        {
            int nombreDeNumérosDeRue = _hasard.Next(NOMBRE_NUMEROS_RUE_MIN, NOMBRE_NUMEROS_RUE_MAX);
            Numéros = new List<NumeroVoie>();
            for (var i = 0; i < nombreDeNumérosDeRue; i++)
            {
                NumeroVoie numéro = new NumeroVoie();
                numéro.initialiserAléatoirement(_hasard);
                Numéros.Add(numéro);
            }
            Nom = new NomVoie();
            Nom.initaliserAléatoirement(_hasard);
            int indiceCommune = _hasard.Next(0, communes.Length - 1);
            IdCommune = communes[indiceCommune].Id;
        }
    }
}
