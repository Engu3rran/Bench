using System;

namespace Bench
{
    public class FabriqueException : CustomException
    {
        public FabriqueException(Exception e) : base(e) { }

        protected override string donnerLeTitreDeLException()
        {
            return "Erreur de configuration de la fabrique";
        }
    }
}
