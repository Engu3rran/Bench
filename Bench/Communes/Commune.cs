using System;
using System.Collections.Generic;
using System.Linq;

namespace Bench
{
    public class Commune : Agregat<Commune>
    {
        private const int NOMBRE_SYLLABES_ALEATOIRES_MIN = 2;
        private const int NOMBRE_SYLLABES_ALEATOIRES_MAX = 5;
        private const int TAILLE_CODE_COMMUNE = 5;
        private static readonly Random _hasard = new Random();

        public CodeCommune Code {get; set;}
        public string Nom { get; set; }

        public void initialiserAléatoirement()
        {
            générerUnNomAléatoire();
            générerUnCodeCommuneAléatoire();
        }

        private void générerUnNomAléatoire()
        {
            int nombreDeSyllabes = _hasard.Next(NOMBRE_SYLLABES_ALEATOIRES_MIN, NOMBRE_SYLLABES_ALEATOIRES_MAX);
            string nom = string.Empty;
            for (int i = 0; i < nombreDeSyllabes; i++)
                nom = string.Concat(nom, _syllabesPossibles[_hasard.Next(0, _syllabesPossibles.Length - 1)]);
            Nom = mettreLaPremièreLettreEnMajuscule(nom);
        }

        private static string mettreLaPremièreLettreEnMajuscule(string nom)
        {
            return string.Concat(nom[0].ToString().ToUpper(), nom.Substring(1));
        }

        private void générerUnCodeCommuneAléatoire()
        {
            string codeCommuneAléatoire = string.Empty;
            for (int i = 0; i < TAILLE_CODE_COMMUNE; i++)
                codeCommuneAléatoire = string.Concat(codeCommuneAléatoire, _hasard.Next(0, 9));
            Code = new CodeCommune(codeCommuneAléatoire);
        }

        private static string[] _syllabesPossibles = new string[]
        {
            "ma", "lo", "ni", "ce", "su", "for", "mi", "do", "gue", "ra", "ro", "re", "la", "mo", "de", "lé", "vo"
        };
    }
}
