using Infrastructure.NHibernate;

namespace Test.Unit.Infrastructure.IoC.NHibernate
{
    [Factory("IDontHaveASessionFactoryAtAll")]
    public class ClassWithInvalidAttribute
    {
    }
}
