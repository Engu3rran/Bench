using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bench
{
    public class NumeroVoie : Entite<NumeroVoie>
    {
        private const int NUMERO_RUE_MIN = 1;
        private const int NUMERO_RUE_MAX = 200;

        public NumeroVoie()
        {
            Numéro = 0;
            Répétition = RepetitionVoie.Aucune;
        }

        public int Numéro { get; set; }
        public RepetitionVoie Répétition {get; set;}

        public void initialiserAléatoirement(Random hasard)
        {
            Numéro = hasard.Next(NUMERO_RUE_MIN, NUMERO_RUE_MAX);
            int tirageDeLaRépétition = hasard.Next(1, 25);
            if (tirageDeLaRépétition <= 5)
                Répétition = (RepetitionVoie)tirageDeLaRépétition;
        }

        public override string ToString()
        {
            return string.Concat(Numéro, ' ', donnerLeLibelléDeLaRépétition(Répétition)).Trim();
        }

        private string donnerLeLibelléDeLaRépétition(RepetitionVoie répétition)
        {
            return _dictionnaireDesRépétitions[répétition];
        }

        private static Dictionary<RepetitionVoie, string> _dictionnaireDesRépétitions = new Dictionary<RepetitionVoie, string>()
        {
            {RepetitionVoie.Aucune, string.Empty},
            {RepetitionVoie.Bis, "Bis"},
            {RepetitionVoie.Ter, "Ter"},
            {RepetitionVoie.Quar, "Quater"},
            {RepetitionVoie.Quin, "Quinquies"}
        };
    }
}
