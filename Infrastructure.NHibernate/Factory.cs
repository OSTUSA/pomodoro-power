using System;

namespace Infrastructure.NHibernate
{
    [AttributeUsage(AttributeTargets.Class, Inherited = true)]
    public class Factory : Attribute
    {
        public string FactoryName { get; private set; }

        public Factory(string factory)
        {
            FactoryName = factory;
        }
    }
}
