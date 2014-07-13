using System;

namespace Bench
{
    public abstract class Entite<T> : IEntite where T : Entite<T>
    {
        protected IEntrepotPersistance _entrepot;

        public Guid Id { get; private set; }

        public Entite() { }

        public void définirUnEntrepotDePersistance(IEntrepotPersistance nouvelEntrepot)
        {
            _entrepot = nouvelEntrepot;
        }

        public void enregistrer()
        {
            _entrepot.enregistrer<T>((T)this);
        }

        public void effacer()
        {
            _entrepot.effacer<T>((T)this);
        }
    }
}
