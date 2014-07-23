using System;
using System.Collections.Generic;
using System.Linq;

namespace Bench.Commandes
{
    public class CreerCommuneReportingCommande : IInstructionBus<ICreerCommuneReportingMessage, ReponseCommande>, IInstructionBus
    {
        public CreerCommuneReportingCommande()
        {
            FabriqueGenerique.constuire<BusCommande>().sAbonner<EvenementCreerCommune>(écouterLaCréationDeCommune);
        }

        public ReponseCommande exécuter(ICreerCommuneReportingMessage message)
        {
            Commune commune = new Commune();
            commune.initialiserAléatoirement();
            return enregistrerLaCommune(commune);
        }

        private static ReponseCommande enregistrerLaCommune(Commune commune)
        {
            commune.définirUnEntrepotDePersistance(FabriqueGenerique.constuire<IEntrepotReporting>());
            commune.enregistrer();
            return ReponseCommande.générerUnSuccès();
        }

        public Type TypeDuMessage
        {
            get { return typeof(ICreerCommuneReportingMessage); }
        }

        public Func<Evenement, ReponseCommande> écouterLaCréationDeCommune = x =>
        {
            Commune commune = x.Agrégat as Commune;
            return enregistrerLaCommune(commune);
        };
    }
}
