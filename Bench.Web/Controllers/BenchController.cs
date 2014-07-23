using Bench.Commandes;
using Bench.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Bench.Web.Controllers
{
    public class BenchController : ApiController
    {
        private BusCommande _bus = FabriqueGenerique.constuire<BusCommande>();

        [HttpGet]
        public string CompterVoirie()
        {
            IEntrepotPersistance entrepotPersistance = FabriqueGenerique.constuire<IEntrepotPersistance>();
            IEntrepotReporting entrepotReporting = FabriqueGenerique.constuire<IEntrepotReporting>();
            object réponse = new
            {
                Persistance = new
                {
                    Communes = entrepotPersistance.donnerLaCollection<Commune>().Count(),
                    Voies = entrepotPersistance.donnerLaCollection<Voie>().Count()
                },
                Reporting = new
                {
                    Communes = entrepotReporting.donnerLaCollection<Commune>().Count(),
                    Voies = entrepotReporting.donnerLaCollection<Voie>().Count()
                }
            };
            return Newtonsoft.Json.JsonConvert.SerializeObject(réponse);
        }

        [HttpGet]
        public string RechercherVoiePersistance()
        {
            DateTime début = DateTime.Now;
            IEntrepotPersistance entrepotPersistance = FabriqueGenerique.constuire<IEntrepotPersistance>();
            var résultat = entrepotPersistance.donnerLaCollection<Voie>().Where(x => x.Numéros.Any(y => y.Numéro == 10) && x.Nom.Libellé.Contains("Couvent")).ToList();
            DateTime fin = DateTime.Now;
            return calculerLaDurée(début, fin);
        }

        [HttpGet]
        public string RechercherVoieReporting()
        {
            DateTime début = DateTime.Now;
            IEntrepotReporting entrepotReporting = FabriqueGenerique.constuire<IEntrepotReporting>();
            var résultat = entrepotReporting.donnerLaCollection<Voie>().Where(x => x.Numéros.Any(y => y.Numéro == 10) && x.Nom.Libellé.Contains("Couvent")).ToList();
            DateTime fin = DateTime.Now;
            return calculerLaDurée(début, fin);
        }

        [HttpPost]
        public string InsererCommunes(int nombre)
        {
            DateTime début = DateTime.Now;
            for (var i = 0; i < nombre; i++)
                _bus.exécuter(new CreerCommuneMessage());
            DateTime fin = DateTime.Now;
            return calculerLaDurée(début, fin);
        }

        [HttpPost]
        public string InsererVoie(int nombre)
        {
            DateTime début = DateTime.Now;
            for (var i = 0; i < nombre; i++)
                _bus.exécuter(new CreerVoieMessage());
            DateTime fin = DateTime.Now;
            return calculerLaDurée(début, fin);
        }

        [HttpPost]
        public string InsererVoiePersistance(int nombre)
        {
            DateTime début = DateTime.Now;
            for (var i = 0; i < nombre; i++)
                _bus.exécuter(new CreerVoieEntrepotPersistanceMessage());
            DateTime fin = DateTime.Now;
            return calculerLaDurée(début, fin);
        }

        [HttpPost]
        public string InsererVoieReporting(int nombre)
        {
            DateTime début = DateTime.Now;
            for (var i = 0; i < nombre; i++)
                _bus.exécuter(new CreerVoieEntrepotReportingMessage());
            DateTime fin = DateTime.Now;
            return calculerLaDurée(début, fin);
        }

        [HttpPost]
        public string SupprimerVoirie()
        {
            _bus.exécuter(new SupprimerVoirieMessage());
            return "Bases réinitialisées";
        }

        private string calculerLaDurée(DateTime début, DateTime fin)
        {
            return (fin - début).ToString(@"hh\:mm\:ss\.fff");
        }
    }
}
