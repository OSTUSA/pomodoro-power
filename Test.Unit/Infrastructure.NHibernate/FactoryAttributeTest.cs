using Infrastructure.NHibernate;
using NUnit.Framework;

namespace Test.Unit.Infrastructure.NHibernate
{
    [TestFixture]
    public class FactoryAttributeTest
    {
        [Test]
        public void Constructor_should_set_factory_name()
        {
            var attr = new Factory("MyFactory");
            Assert.AreEqual("MyFactory", attr.FactoryName);
        }
    }
}
