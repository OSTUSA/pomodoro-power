using System.Configuration;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using Infrastructure.Migrations.Runner;
using Infrastructure.NHibernate;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Context;
using Configuration = NHibernate.Cfg.Configuration;

namespace Test.Integration
{
    public class DatabaseTestState
    {
        protected string ConnectionString;
        protected string MigrationProfile;
        protected readonly SessionFactoryBuilder Builder = new SessionFactoryBuilder();

        public DatabaseTestState(string connString)
        {
            ConnectionString = connString;
        }

        public ISessionFactory Configure<TMapping>(string profile = "")
        {
            MigrationProfile = profile;
            return Builder.GetFactory(ConnectionString, () => Fluently.Configure()
                                                               .Database(
                                                                   MsSqlConfiguration.MsSql2008.ConnectionString(
                                                                       c => c.FromConnectionStringWithKey(ConnectionString)))
                                                               .Mappings(cfg => cfg.FluentMappings.AddFromAssemblyOf<TMapping>())
                                                               .ExposeConfiguration(CreateSchema)
                                                               .BuildConfiguration()
                                                               .CurrentSessionContext<ThreadStaticSessionContext>().BuildSessionFactory());
        }


        protected void CreateSchema(Configuration config)
        {
            Runner.ConnectionString = ConfigurationManager.ConnectionStrings[ConnectionString].ToString();
            //wipe the database
            Runner.MigrateDown(0);
            //migrate to latest version
            Runner.MigrateUp(Runner.VersionLatest, MigrationProfile);
        }
    }
}
