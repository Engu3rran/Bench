using System;

namespace Bench
{
    public class NomVoie
    {
        public NomVoie() { }

        public string Type { get; set; }
        public string Libellé { get; set; }

        public void initaliserAléatoirement(Random hasard)
        {
            Type = _typesDeVoie[hasard.Next(0, _typesDeVoie.Length - 1)];
            Libellé = _libellésDeVoie[hasard.Next(0, _libellésDeVoie.Length - 1)];
        }

        public override string ToString()
        {
            return string.Concat(Type, ' ', Libellé).Trim();
        }

        private static readonly string[] _typesDeVoie = new string[]
        {
            "Rue", "Allée", "Place", "Quai", "Boulevard", "Avenue", "Chemin"
        };

        private static readonly string[] _libellésDeVoie = new string[]
        {
            "de Marseille", "du Couvent", "de la République", "de l'Europe", "Judaïque", "de la Paix", "du Parlement", "Napoléon Bonaparte", "Jeanne d'Arc", "du 14 Juillet", "de la Jallère",
            "de la Victoire", "du 8 Mai", "de l'Armistice", "du Général De Gaulle", "du Maréchal Foch", "des Lilas", "Saint André", "Sainte Catherine", "Wilson", "de l'Hermite"
        };
    }
}
