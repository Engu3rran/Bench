using System;
using System.Collections.Generic;
using System.Linq;

namespace Bench.Commandes
{
    public class CreerVoieEntrepotCommande : IInstructionBus<ICreerVoieEntrepotMessage, ReponseCommande>, IInstructionBus
    {
        public ReponseCommande exécuter(ICreerVoieEntrepotMessage message)
        {
            Commune[] communes = FabriqueGenerique.constuire<IEntrepotPersistance>().donnerLaCollection<Commune>().ToArray();
            Voie voie = new Voie();
            voie.initialiserAléatoirement(communes);
            voie.définirUnEntrepotDePersistance(message.Entrepot);
            voie.enregistrer();
            return ReponseCommande.générerUnSuccès();
        }

        public Type TypeDuMessage
        {
            get { return typeof(ICreerVoieEntrepotMessage); }
        }
    }
}
