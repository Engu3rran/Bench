using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bench
{
    public class NumeroRue : Entite<NumeroRue>
    {
        public NumeroRue()
        {
            Numéro = 0;
            Répétition = RepetitionRue.Aucune;
        }

        public NumeroRue(int numéro)
        {
            Numéro = numéro;
            Répétition = RepetitionRue.Aucune;
        }

        public NumeroRue(int numéro, RepetitionRue répétition)
        {
            Numéro = numéro;
            Répétition = répétition;
        }

        public int Numéro { get; set; }
        public RepetitionRue Répétition {get; set;}

        public override string ToString()
        {
            return string.Concat(Numéro, ' ', donnerLeLibelléDeLaRépétition(Répétition)).Trim();
        }

        private string donnerLeLibelléDeLaRépétition(RepetitionRue répétition)
        {
            return _dictionnaireDesRépétitions[répétition];
        }

        private static Dictionary<RepetitionRue, string> _dictionnaireDesRépétitions = new Dictionary<RepetitionRue, string>()
        {
            {RepetitionRue.Aucune, string.Empty},
            {RepetitionRue.Bis, "Bis"},
            {RepetitionRue.Ter, "Ter"},
            {RepetitionRue.Quar, "Quater"},
            {RepetitionRue.Quin, "Quinquies"}
        };
    }
}
