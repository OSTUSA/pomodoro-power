using Core.Domain.Model.Users;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using Infrastructure.NHibernate;
using Infrastructure.NHibernate.Mapping.Users;
using Infrastructure.NHibernate.Repositories;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Context;
using Ninject.Modules;
using Ninject.Web.Common;

namespace Infrastructure.IoC
{
    public class NHibernateModule : NinjectModule
    {
        protected SessionFactoryBuilder Builder;

        public NHibernateModule()
        {
            Builder = new SessionFactoryBuilder();
        }

        public override void Load()
        {
            var factory = Builder.GetFactory("MainFactory", ConfigureDefaultConnection);
            Bind<ISession>().ToMethod(x => factory.OpenSession()).InRequestScope();
            Bind<IUserRepository>().To<UserRepository>();
        }

        protected static ISessionFactory ConfigureDefaultConnection()
        {
            return Fluently.Configure()
                    .Database(MsSqlConfiguration.MsSql2008.ConnectionString(c => c.FromConnectionStringWithKey("DefaultConnection")))
                    .Mappings(cfg => cfg.FluentMappings.AddFromAssemblyOf<UserMap>())
                    .BuildConfiguration()
                    .CurrentSessionContext<WebSessionContext>().BuildSessionFactory();
        }
    }
}
