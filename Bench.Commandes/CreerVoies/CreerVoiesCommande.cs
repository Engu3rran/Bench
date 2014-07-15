using System;
using System.Collections.Generic;
using System.Linq;

namespace Bench.Commandes
{
    public class CreerVoiesCommande : IInstructionBus, IInstructionBus<ICreerVoiesMessage, ReponseCommande>
    {
        public ReponseCommande exécuter(ICreerVoiesMessage message)
        {
            Commune[] communesEnregistrées = enregistrerLesCommunes(message);
            enregistrerLesVoies(message, communesEnregistrées);
            return ReponseCommande.générerUnSuccès();
        }

        private Commune[] enregistrerLesCommunes(ICreerVoiesMessage message)
        {
            IList<Commune> communes = new List<Commune>();
            for (int i = 0; i < message.NombreDeCommunes; i++)
            {
                Commune commune = new Commune();
                commune.initialiserAléatoirement();
                commune.définirUnEntrepotDePersistance(message.Entrepot);
                commune.enregistrer();
                communes.Add(commune);
            }
            Commune[] communesEnregistrées = communes.ToArray();
            return communesEnregistrées;
        }

        private void enregistrerLesVoies(ICreerVoiesMessage message, Commune[] communesEnregistrées)
        {
            for (int i = 0; i < message.NombreDeVoies; i++)
            {
                Voie voie = new Voie();
                voie.initialiserAléatoirement(communesEnregistrées);
                voie.définirUnEntrepotDePersistance(message.Entrepot);
                voie.enregistrer();
            }
        }

        public Type TypeDuMessage
        {
            get { return typeof(ICreerVoiesMessage); }
        }
    }
}
