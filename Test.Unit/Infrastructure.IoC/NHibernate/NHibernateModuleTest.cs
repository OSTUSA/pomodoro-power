using System;
using System.Collections.Generic;
using System.Reflection;
using Infrastructure.IoC.NHibernate;
using Infrastructure.NHibernate;
using Moq;
using NHibernate;
using NUnit.Framework;
using Ninject;
using System.Linq;
using Ninject.Activation;
using Ninject.Planning.Targets;

namespace Test.Unit.Infrastructure.IoC.NHibernate
{
    [TestFixture]
    public class NHibernateModuleTest
    {
        protected NHibernateModule Module;

        [SetUp]
        public void Init()
        {
            NHibernateModule.DefaultFactory = () => new Mock<ISessionFactory>().Object;
            Module = new NHibernateModule();
        }

        [Test]
        public void Constructor_has_default_empty_dictionary_of_ISessionFactory()
        {
            Assert.IsInstanceOf<Dictionary<string, ISessionFactory>>(Module.Factories);
        }

        [Test]
        public void Add_does_thread_safe_add_to_dictionary()
        {
            Module.Add("OTHER", () => new Mock<ISessionFactory>().Object);
            Assert.True(Module.Factories.ContainsKey("OTHER"));
        }

        [Test]
        public void OnLoad_should_perform_binding_to_ISession()
        {
            var std = new StandardKernel();
            Module.OnLoad(std);
            var bound = Module.Bindings.SingleOrDefault(b => b.Service == typeof (ISession));
            Assert.IsNotNull(bound);
        }

        [Test]
        public void Constructor_should_set_default_factory()
        {
            Assert.IsInstanceOf<ISessionFactory>(Module.Factories["Default"]);
        }

        [Test]
        public void Overriding_default_factory_function_should_return_provided_factory()
        {
            ClearDefaultFactory();

            var mocked = new Mock<ISessionFactory>().Object;
            NHibernateModule.DefaultFactory = () => mocked;
            var module = new NHibernateModule();
            Assert.AreSame(mocked, module.Factories["Default"]);
        }

        [Test]
        public void Test_GetSession_returns_factory_for_attribute()
        {
            ClearDefaultFactory();
            var context = GetMockContext<ClassWithAttribute>();
            var module = new NHibernateModule();
            var result = InvokeGetSession(module, context);
            Assert.IsNotNull(result);
        }

        [Test]
        [ExpectedException(typeof(InvalidFactoryException))]
        public void Test_Invalid_Factory_name_in_attribute_throws_exception()
        {
            var context = GetMockContext<ClassWithInvalidAttribute>();
            var module = new NHibernateModule();
            //test the inner exception, not the one from reflection
            try
            {
                InvokeGetSession(module, context);
            }
            catch (TargetInvocationException e)
            {
                throw e.InnerException;
            }
        }

        [Test]
        public void Test_no_attribute_should_use_default_factory()
        {
            var context = GetMockContext<ClassWithNoAttribute>();
            var module = new NHibernateModule();
            var result = InvokeGetSession(module, context);
            Assert.IsNotNull(result);
        }

        protected Mock<IContext> GetMockContext<T>()
        {
            var factory = new Mock<ISessionFactory>();
            factory.Setup(f => f.OpenSession()).Returns(new Mock<ISession>().Object);
            NHibernateModule.DefaultFactory = () => factory.Object;
            var context = new Mock<IContext>();
            var request = new Mock<IRequest>();
            var target = new Mock<ITarget>();
            var info = new Mock<MemberInfo>();
            info.SetupGet(i => i.ReflectedType).Returns(typeof(T));
            target.SetupGet(t => t.Member).Returns(info.Object);
            request.SetupGet(r => r.Target).Returns(target.Object);
            context.SetupGet(c => c.Request).Returns(request.Object);
            return context;
        }

        protected static ISession InvokeGetSession(NHibernateModule module, Mock<IContext> context)
        {
            MethodInfo method = typeof(NHibernateModule).GetMethod("GetSession", BindingFlags.NonPublic | BindingFlags.Instance);
            var result = method.Invoke(module, new object[] { context.Object }) as ISession;
            return result;
        }

        protected void ClearDefaultFactory()
        {
            var builder =
                typeof(NHibernateModule).GetField("Builder", BindingFlags.NonPublic | BindingFlags.Static).GetValue(Module) as
                SessionFactoryBuilder;
            var scoper =
                typeof(SessionFactoryBuilder).GetField("_factorySingleton", BindingFlags.NonPublic | BindingFlags.Instance)
                                              .GetValue(builder) as SingletonInstanceScoper<ISessionFactory>;
            scoper.ClearInstance("Default");
        }
    }
}
