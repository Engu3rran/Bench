
namespace Bench.Commandes
{
    public interface ISupprimerVoiesMessage : IMessageCommande
    {
        IEntrepotPersistance Entrepot { get; }
    }
}
