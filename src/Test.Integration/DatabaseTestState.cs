using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using Infrastructure.NHibernate;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Context;
using NHibernate.Tool.hbm2ddl;

namespace Test.Integration
{
    public class DatabaseTestState
    {
        protected string ConnectionString;
        protected string OutputFile;
        protected readonly SessionFactoryBuilder Builder = new SessionFactoryBuilder();

        public DatabaseTestState(string connString, string output)
        {
            ConnectionString = connString;
            OutputFile = output;
        }

        public ISessionFactory Configure<TMapping>()
        {
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
            var schemaExport = new SchemaExport(config);
            schemaExport.Drop(false, true);
            schemaExport.SetOutputFile(OutputFile).Create(false, true);
        }
    }
}
