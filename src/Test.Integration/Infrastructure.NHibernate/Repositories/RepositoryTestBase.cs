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
        protected DatabaseTestState TestState
        {
            get
            {
                return new DatabaseTestState("TestConnection", "pom-schema.sql");
            }
        }
    }
}
