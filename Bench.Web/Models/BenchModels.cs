using Bench.Commandes;

namespace Bench.Web.Models
{
    public class CreerCommuneMessage : ICreerCommuneMessage
    {

    }

    public class CreerCommuneReportingMessage : ICreerCommuneReportingMessage
    {

    }

    public class CreerVoieMessage : ICreerVoieMessage
    {

    }

    public class CreerVoieReportingMessage : ICreerVoieReportingMessage
    {

    }

    public class SupprimerVoirieMessage : ISupprimerVoirieMessage
    {

    }

    public class CreerVoieEntrepotPersistanceMessage : ICreerVoieEntrepotMessage
    {
        public IEntrepotPersistance Entrepot
        {
            get
            {
                return FabriqueGenerique.constuire<IEntrepotPersistance>();
            }
        }
    }

    public class CreerVoieEntrepotReportingMessage : ICreerVoieEntrepotMessage
    {
        public IEntrepotPersistance Entrepot
        {
            get
            {
                return FabriqueGenerique.constuire<IEntrepotReporting>();
            }
        }
    }

}