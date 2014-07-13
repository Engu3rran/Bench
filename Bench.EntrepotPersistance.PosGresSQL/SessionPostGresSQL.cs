using System.Reflection;
using System.Configuration;
using NHibernate;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;

namespace Bench.EntrepotPersistance.PostGresSQL
{
    public class SessionPostGresSQL
    {
        private const string CLE_CHAINE_CONNEXION_POSGRESSQL = "connectionStringPG";
        private static readonly ISessionFactory _fabriqueSession = initialiserLaFabrique();

        private ISession _sessionNhibernate;

        public SessionPostGresSQL()
        {
            _sessionNhibernate = _fabriqueSession.OpenSession();
        }

        public ISession donnerLaSession()
        {
            return _sessionNhibernate;
        }

        private static string récupérerLaChaîneDeConnexion()
        {
            return ConfigurationManager.AppSettings[CLE_CHAINE_CONNEXION_POSGRESSQL];
        }

        private static ISessionFactory initialiserLaFabrique()
        {
            return Fluently.Configure()
              .Database(
                PostgreSQLConfiguration
                    .PostgreSQL82
                    .ConnectionString(récupérerLaChaîneDeConnexion())
              )
              .Mappings(m =>
                m.FluentMappings
                    .AddFromAssembly(Assembly.GetExecutingAssembly())
                    .Conventions.Add(FluentNHibernate.Conventions.Helpers.DefaultLazy.Never()))
              .BuildSessionFactory();
        }

    }
}
