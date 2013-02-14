using Infrastructure.NHibernate;
using Moq;
using NHibernate;
using NUnit.Framework;

namespace Test.Unit.Infrastructure.NHibernate
{
    [TestFixture]
    public class SessionFactoryBuilderTest
    {
        [Test]
        public void GetFactory_with_key_should_return_factory_by_function()
        {
            var builder = new SessionFactoryBuilder();
            var factory = builder.GetFactory("factory", () => new Mock<ISessionFactory>().Object);
            Assert.IsInstanceOf<ISessionFactory>(factory);
        }
    }
}
