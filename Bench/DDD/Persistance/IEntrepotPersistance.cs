using System.Linq;

namespace Bench
{
    public interface IEntrepotPersistance
    {
        void enregistrer<T>(T entité) where T : IEntite;
        void effacer<T>(T entité) where T : IEntite;
        IQueryable<T> donnerLaCollection<T>() where T : IEntite;
    }
}
