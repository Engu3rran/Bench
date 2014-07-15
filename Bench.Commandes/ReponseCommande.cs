
namespace Bench.Commandes
{
    public class ReponseCommande
    {
        private ReponseCommande(bool réussite)
        {
            ARéussi = réussite;
        }

        public bool ARéussi { get; private set; }

        public static ReponseCommande générerUnSuccès()
        {
            return new ReponseCommande(true);
        }

        public static ReponseCommande générerUnEchec()
        {
            return new ReponseCommande(false);
        }

    }
}
