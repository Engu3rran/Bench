using System;

namespace Bench
{
    public abstract class Entite<T> : IEntite where T : Entite<T>
    {
        protected IEntrepotPersistance _entrepot = FabriqueGenerique.constuire<IEntrepotPersistance>();

        public Guid Id { get; set; }

        public Entite() 
        {
            Id = Guid.NewGuid();
        }

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
