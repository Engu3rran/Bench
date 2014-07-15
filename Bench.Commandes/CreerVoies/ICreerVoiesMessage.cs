
namespace Bench.Commandes
{
    public interface ICreerVoiesMessage : IMessageCommande
    {
        int NombreDeCommunes { get; }
        int NombreDeVoies { get; }
        IEntrepotPersistance Entrepot { get; }
    }
}
