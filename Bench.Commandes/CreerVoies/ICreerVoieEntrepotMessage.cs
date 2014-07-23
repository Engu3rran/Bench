
namespace Bench.Commandes
{
    public interface ICreerVoieEntrepotMessage : IMessageCommande
    {
        IEntrepotPersistance Entrepot { get; }
    }
}
