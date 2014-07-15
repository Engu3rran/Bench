using System.Reflection;

namespace Bench.Commandes
{
    public class BusCommande : Bus<IMessageCommande,ReponseCommande>
    {
        public BusCommande()
        {
            chargerLaListeDesInstruction(Assembly.GetExecutingAssembly());
        }
    }
}
