using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using Infrastructure.NHibernate;
using Infrastructure.NHibernate.Mapping.Users;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Context;
using NHibernate.Tool.hbm2ddl;
using NUnit.Framework;

namespace Test.Integration.Infrastructure.NHibernate.Repositories
{
    public class RepositoryTestBase
    {
        protected readonly SessionFactoryBuilder Builder = new SessionFactoryBuilder();

        protected static ISessionFactory BuildTestFactory()
        {
            return Fluently.Configure()
                .Database(MsSqlConfiguration.MsSql2008.ConnectionString(c => c.FromConnectionStringWithKey("TestConnection")))
                .Mappings(cfg => cfg.FluentMappings.AddFromAssemblyOf<UserMap>())
                .ExposeConfiguration(CreateSchema)
                .BuildConfiguration()
                .CurrentSessionContext<ThreadStaticSessionContext>().BuildSessionFactory();
        }

        protected static void CreateSchema(Configuration config)
        {
            var schemaExport = new SchemaExport(config);
            schemaExport.Drop(false, true);
            schemaExport.SetOutputFile("pom-schema.sql").Create(false, true);
        }
    }
}
