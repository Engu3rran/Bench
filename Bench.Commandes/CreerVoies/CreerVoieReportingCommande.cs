using System;
using System.Collections.Generic;
using System.Linq;

namespace Bench.Commandes
{
    public class CreerVoieReportingCommande : IInstructionBus<ICreerVoieReportingMessage, ReponseCommande>, IInstructionBus
    {
        public CreerVoieReportingCommande()
        {
            FabriqueGenerique.constuire<BusCommande>().sAbonner<EvenementCreerVoie>(écouterLaCréationDeVoie);
        }

        public ReponseCommande exécuter(ICreerVoieReportingMessage message)
        {
            Commune[] communes = FabriqueGenerique.constuire<IEntrepotPersistance>().donnerLaCollection<Commune>().ToArray();
            Voie voie = new Voie();
            voie.initialiserAléatoirement(communes);
            return enregistrerLaNouvelleVoie(voie);
        }

        private static ReponseCommande enregistrerLaNouvelleVoie(Voie voie)
        {
            voie.définirUnEntrepotDePersistance(FabriqueGenerique.constuire<IEntrepotReporting>());
            voie.enregistrer();
            return ReponseCommande.générerUnSuccès();
        }

        public Type TypeDuMessage
        {
            get { return typeof(ICreerVoieReportingMessage); }
        }

        public Func<Evenement, ReponseCommande> écouterLaCréationDeVoie = x =>
        {
            Voie voie = x.Agrégat as Voie;
            return enregistrerLaNouvelleVoie(voie);
        };
    }
}
