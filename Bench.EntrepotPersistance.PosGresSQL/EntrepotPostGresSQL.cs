using System;
using System.Collections.Generic;
using System.Linq;
using NHibernate;
using NHibernate.Linq;


namespace Bench.EntrepotPersistance.PostGresSQL
{
    public class EntrepotPostGresSQL : IEntrepotReporting
    {
        ISession _session;

        public EntrepotPostGresSQL(SessionPostGresSQL PostGres)
        {
            _session = PostGres.donnerLaSession();
        }

        public void enregistrer<T>(T entité) where T : IEntite
        {
            try
            {
                using (ITransaction transaction = _session.BeginTransaction())
                {
                    _session.SaveOrUpdate(entité);
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
                using (ITransaction transaction = _session.BeginTransaction())
                {
                    _session.Delete(entité);
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
            return _session.Query<T>();
        }
    }
}
