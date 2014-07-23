using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bench.Commandes
{
    public class SupprimerVoirieCommande : IInstructionBus, IInstructionBus<ISupprimerVoirieMessage, ReponseCommande>
    {
        public ReponseCommande exécuter(ISupprimerVoirieMessage message)
        {
            IList<IEntrepotPersistance> entrepots = new List<IEntrepotPersistance>();
            entrepots.Add(FabriqueGenerique.constuire<IEntrepotPersistance>());
            entrepots.Add(FabriqueGenerique.constuire<IEntrepotReporting>());
            Parallel.ForEach(entrepots, entrepot =>
            {
                effacerLesEnregistrements<Voie>(entrepot);
                effacerLesEnregistrements<Commune>(entrepot);
            });
            return ReponseCommande.générerUnSuccès();
        }

        private void effacerLesEnregistrements<T>(IEntrepotPersistance entrepot) where T : IEntite
        {
            T enregistrement = chargerLePremierEnregistrement<T>(entrepot);
            while(enregistrement != null)
            {
                entrepot.effacer<T>(enregistrement);
                enregistrement = chargerLePremierEnregistrement<T>(entrepot);
            }
        }

        private T chargerLePremierEnregistrement<T>(IEntrepotPersistance entrepot) where T : IEntite
        {
            return entrepot
                .donnerLaCollection<T>()
                .FirstOrDefault();
        }

        public Type TypeDuMessage
        {
            get { return typeof(ISupprimerVoirieMessage); }
        }
    }
}
