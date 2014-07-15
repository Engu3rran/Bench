using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Bench.EntrepotPersistance.Mock
{
    public class EntrepotPersistanceMock : IEntrepotPersistance
    {
        private IDictionary<string, IList<IEntite>> _collections;

        public EntrepotPersistanceMock()
        {
            _collections = new Dictionary<string, IList<IEntite>>();
        }

        public IQueryable<T> donnerLaCollection<T>() where T : IEntite
        {
            string nomDeLaCollection = trouverLeNomDeLaCollectionCorrespondante<T>();
            if (_collections.ContainsKey(nomDeLaCollection))
                return convertirLesEléments<T>(nomDeLaCollection);
            return new List<T>().AsQueryable();
        }

        private IQueryable<T> convertirLesEléments<T>(string nomDeLaCollection) where T : IEntite
        {
            IList<T> éléments = new List<T>();
            foreach (IEntite agrégat in _collections[nomDeLaCollection])
                éléments.Add((T)agrégat);
            return éléments.AsQueryable<T>();
        }

        public void enregistrer<T>(T entité) where T : IEntite
        {
            try
            {
                if (entité.Id == new Guid())
                    entité.Id = Guid.NewGuid();
                string nomDeLaCollection = trouverLeNomDeLaCollectionCorrespondante<T>();
                if (!_collections.ContainsKey(nomDeLaCollection))
                    _collections.Add(nomDeLaCollection, new List<IEntite>());
                modifierLaCollection(entité, nomDeLaCollection);
            }
            catch (Exception e)
            {
                throw new PersistanceException(e);
            }
        }


        private void modifierLaCollection(IEntite entité, string nomDeLaCollection)
        {
            IList<IEntite> collection = _collections[nomDeLaCollection];
            IEntite entitéAModifier = collection.SingleOrDefault(x => x.Id == entité.Id);
            if (entitéAModifier == null)
                _collections[nomDeLaCollection].Add(entité);
            else
            {
                collection.Remove(entitéAModifier);
                collection.Add(entité);
            }
        }

        public void effacer<T>(T entité) where T : IEntite
        {
            try
            {
                string nomDeLaCollection = trouverLeNomDeLaCollectionCorrespondante<T>();
                if (_collections.ContainsKey(nomDeLaCollection))
                    supprimerDeLaCollection(entité, nomDeLaCollection);
            }
            catch (Exception e)
            {
                throw new PersistanceException(e);
            }
        }

        private void supprimerDeLaCollection(IEntite entité, string nomDeLaCollection)
        {
            IList<IEntite> collection = _collections[nomDeLaCollection];
            IEntite entitéASupprimer = collection.SingleOrDefault(x => x.Id == entité.Id);
            if (entitéASupprimer != null)
            {
                collection.Remove(entitéASupprimer);
            }
        }

        private string trouverLeNomDeLaCollectionCorrespondante<T>()
        {
            return typeof(T).GetTypeInfo().Name;
        }
    }
}
