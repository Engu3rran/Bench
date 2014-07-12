using System;

namespace Bench
{
    public class BusException : CustomException
    {
        public BusException(Exception e) : base(e) { }

        protected override string donnerLeTitreDeLException()
        {
            return "Erreur lors de l'exécution d'une instruction du bus";
        }
    }
}
