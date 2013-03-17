namespace Test.Integration.Infrastructure.NHibernate.Repositories
{
    public class RepositoryTestBase
    {
        protected DatabaseTestState TestState
        {
            get
            {
                return new DatabaseTestState("TestConnection");
            }
        }
    }
}
