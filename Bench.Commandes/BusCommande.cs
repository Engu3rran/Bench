using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;

namespace Bench.Commandes
{
    public class BusCommande : Bus<IMessageCommande,ReponseCommande>
    {
        public BusCommande()
        {
        }

        public void initialiser()
        {
            chargerLaListeDesInstruction(Assembly.GetExecutingAssembly());
        }

        private Dictionary<Type, List<Func<Evenement, ReponseCommande>>> _évènements = new Dictionary<Type, List<Func<Evenement, ReponseCommande>>>();

        public void sAbonner<T>(Func<Evenement, ReponseCommande> fonction) where T : Evenement
        {
            Type typeDEvènement = typeof(T);
            if (!_évènements.ContainsKey(typeDEvènement))
                _évènements.Add(typeDEvènement, new List<Func<Evenement, ReponseCommande>>());
            _évènements[typeDEvènement].Add(fonction);
        }

        public void déclencher<T>(T évènement) where T : Evenement
        {
            Type typeDEvènement = typeof(T);
            if (_évènements.ContainsKey(typeDEvènement))
                //Parallel.ForEach(_évènements[typeDEvènement], fonction =>
                //{
                //    fonction(évènement);
                //});
                foreach (Func<Evenement, ReponseCommande> fonction in _évènements[typeDEvènement])
                    #pragma warning disable 4014
                    Task.Run(() =>
                    {
                        fonction(évènement);
                    });
        }
    }
}
