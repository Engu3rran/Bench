using System;
using System.Collections.Generic;
using System.Linq;

namespace Bench.Commandes
{
    public class SupprimerVoiesCommande : IInstructionBus, IInstructionBus<ISupprimerVoiesMessage,ReponseCommande>
    {
        private IEntrepotPersistance _entrepot;

        public ReponseCommande exécuter(ISupprimerVoiesMessage message)
        {
            _entrepot = message.Entrepot;
            effacerLesEnregistrements<Voie>();
            effacerLesEnregistrements<Commune>();
            return ReponseCommande.générerUnSuccès();
        }

        private void effacerLesEnregistrements<T>() where T : IEntite
        {
            T enregistrement = chargerLePremierEnregistrement<T>();
            while(enregistrement != null)
            {
                _entrepot.effacer<T>(enregistrement);
                enregistrement = chargerLePremierEnregistrement<T>();
            }
        }

        private T chargerLePremierEnregistrement<T>() where T : IEntite
        {
            return _entrepot
                .donnerLaCollection<T>()
                .FirstOrDefault();
        }

        public Type TypeDuMessage
        {
            get { return typeof(ISupprimerVoiesMessage); }
        }
    }
}
