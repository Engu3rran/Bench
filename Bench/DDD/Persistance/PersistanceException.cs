using System;

namespace Bench
{
    public class PersistanceException : CustomException
    {
        public PersistanceException(Exception e) : base(e) { }

        protected override string donnerLeTitreDeLException()
        {
            return "Erreur de persistance";
        }
    }
}
