using System;
using NHibernate;

namespace Infrastructure.NHibernate
{
    public class SessionFactoryBuilder
    {
        private readonly SingletonInstanceScoper<ISessionFactory> _factorySingleton = new SingletonInstanceScoper<ISessionFactory>();
  
        public ISessionFactory GetFactory(string key, Func<ISessionFactory> builder)
        {
            return _factorySingleton.GetScopedInstance(key, builder);
        }
    }
}
