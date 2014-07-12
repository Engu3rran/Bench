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
            return ConfigurationManager.ConnectionStrings[CLE_CHAINE_CONNEXION_POSGRESSQL].ConnectionString;
        }

        private static ISessionFactory initialiserLaFabrique()
        {
            return Fluently.Configure()
              .Database(
                PostgreSQLConfiguration
                    .Standard
                    .ConnectionString(récupérerLaChaîneDeConnexion())
              )
              .Mappings(m =>
                m.FluentMappings.AddFromAssembly(Assembly.GetExecutingAssembly()))
              .BuildSessionFactory();
        }

    }
}
