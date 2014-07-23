using System;
using System.Collections.Generic;
using System.Linq;


namespace Bench.Commandes
{
    public class CreerCommuneCommande : IInstructionBus<ICreerCommuneMessage, ReponseCommande>, IInstructionBus
    {
        public ReponseCommande exécuter(ICreerCommuneMessage message)
        {
            Commune commune = new Commune();
            commune.initialiserAléatoirement();
            commune.enregistrer();
            FabriqueGenerique.constuire<BusCommande>().déclencher<EvenementCreerCommune>(new EvenementCreerCommune(commune));
            return ReponseCommande.générerUnSuccès();
        }

        public Type TypeDuMessage
        {
            get { return typeof(ICreerCommuneMessage); }
        }
    }

    public class EvenementCreerCommune : Evenement
    {
        public EvenementCreerCommune(Commune commune)
        {
            Agrégat = commune;
        }
    }
}
