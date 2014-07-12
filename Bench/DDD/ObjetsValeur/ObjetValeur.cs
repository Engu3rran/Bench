using System;

namespace Bench
{
    public abstract class ObjetValeur
    {
        protected internal string _valeur;


        public ObjetValeur(string valeur)
        {
            _valeur = valeur;
        }


        public override bool Equals(object obj)
        {
            return obj is ObjetValeur
                && (obj as ObjetValeur)._valeur.Equals(_valeur);
        }


        public override int GetHashCode()
        {
            return 13 ^ _valeur.GetHashCode();
        }


        public override string ToString()
        {
            return _valeur;
        }


        public static bool operator ==(ObjetValeur obj1, ObjetValeur obj2)
        {
            if (Object.ReferenceEquals(obj1, obj2))
                return true;
            if ((object)obj1 == null || (object)obj2 == null)
                return false;
            return obj1._valeur == obj2._valeur;
        }


        public static bool operator !=(ObjetValeur obj1, ObjetValeur obj2)
        {
            return !(obj1 == obj2);
        }
    }
}
