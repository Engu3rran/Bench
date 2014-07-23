using System;
using System.Collections.Generic;
using System.Linq;

namespace Bench.Commandes
{
    public class CreerVoieCommande : IInstructionBus<ICreerVoieMessage, ReponseCommande>, IInstructionBus
    {
        public ReponseCommande exécuter(ICreerVoieMessage message)
        {
            Commune[] communes = FabriqueGenerique.constuire<IEntrepotPersistance>().donnerLaCollection<Commune>().ToArray();
            Voie voie = new Voie();
            voie.initialiserAléatoirement(communes);
            voie.enregistrer();
            FabriqueGenerique.constuire<BusCommande>().déclencher<EvenementCreerVoie>(new EvenementCreerVoie(voie));
            return ReponseCommande.générerUnSuccès();
        }

        public Type TypeDuMessage
        {
            get { return typeof(ICreerVoieMessage); }
        }
    }

    public class EvenementCreerVoie : Evenement
    {
        public EvenementCreerVoie(Voie voie)
        {
            Agrégat = voie;
        }
    }
}
