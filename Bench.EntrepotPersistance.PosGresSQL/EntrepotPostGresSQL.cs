using System;
using System.Collections.Generic;
using System.Linq;
using NHibernate;
using NHibernate.Linq;


namespace Bench.EntrepotPersistance.PostGresSQL
{
    public class EntrepotPostGresSQL : IEntrepotPersistance
    {
        ISession _sessionNhibernate;

        public EntrepotPostGresSQL(SessionPostGresSQL PostGres)
        {
            _sessionNhibernate = PostGres.donnerLaSession();
        }

        public void enregistrer<T>(T entité) where T : IEntite
        {
            try
            {
                using (ITransaction transaction = _sessionNhibernate.BeginTransaction())
                {
                    _sessionNhibernate.SaveOrUpdate(entité);
                    transaction.Commit();
                }
            }
            catch(Exception e)
            {
                throw new PersistanceException(e);
            }
        }

        public void effacer<T>(T entité) where T : IEntite
        {
            try
            {
                using (ITransaction transaction = _sessionNhibernate.BeginTransaction())
                {
                    _sessionNhibernate.Delete(entité);
                    transaction.Commit();
                }
            }
            catch (Exception e)
            {
                throw new PersistanceException(e);
            }
            
        }

        public IQueryable<T> donnerLaCollection<T>() where T : IEntite
        {
            return _sessionNhibernate.Query<T>();
        }
    }
}
