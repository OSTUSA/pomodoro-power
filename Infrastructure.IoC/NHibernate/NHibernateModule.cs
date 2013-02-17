using System;
using System.Collections.Generic;
using System.Linq;
using Core.Domain.Model.Users;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using Infrastructure.NHibernate;
using Infrastructure.NHibernate.Mapping.Users;
using Infrastructure.NHibernate.Repositories;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Context;
using Ninject.Activation;
using Ninject.Modules;
using Ninject.Web.Common;

namespace Infrastructure.IoC.NHibernate
{
    public class NHibernateModule : NinjectModule
    {
        public static Func<ISessionFactory> DefaultFactory = ConfigureDefaultFactory;

        public Dictionary<string, ISessionFactory> Factories { get; private set; }

        protected SessionFactoryBuilder Builder = new SessionFactoryBuilder();

        public NHibernateModule(Dictionary<string, ISessionFactory> factories)
        {
            Builder = new SessionFactoryBuilder();
            Factories = factories;
            Factories["Default"] = DefaultFactory.Invoke();
        }

        public NHibernateModule() : this(new Dictionary<string, ISessionFactory>())
        {
            
        }

        public override void Load()
        {
            Bind<ISession>().ToMethod(GetSession).InRequestScope();
            Bind<IUserRepository>().To<UserRepository>();
        }

        protected static ISessionFactory ConfigureDefaultFactory()
        {
            return Fluently.Configure()
                    .Database(MsSqlConfiguration.MsSql2008.ConnectionString(c => c.FromConnectionStringWithKey("DefaultConnection")))
                    .Mappings(cfg => cfg.FluentMappings.AddFromAssemblyOf<UserMap>())
                    .BuildConfiguration()
                    .CurrentSessionContext<WebSessionContext>().BuildSessionFactory();
        }

        protected ISession GetSession(IContext context)
        {
            Type requestType = context.Request.Target.Member.ReflectedType;
            var attrs = requestType.GetCustomAttributes(true);
            var factoryAttr = attrs.FirstOrDefault(a => a.GetType() == typeof(Factory)) as Factory ?? null;
            var factory = (factoryAttr == null) ? "Default" : factoryAttr.FactoryName;

            if(!Factories.ContainsKey(factory))
                throw new InvalidFactoryException(string.Format("Invalid factory \"{0}\" provided", factory));

            return Factories[factory].OpenSession();
        }
    }
}
