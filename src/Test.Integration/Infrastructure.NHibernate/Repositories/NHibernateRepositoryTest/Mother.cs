using Core.Domain.Model.Users;

namespace Test.Integration.Infrastructure.NHibernate.Repositories.NHibernateRepositoryTest
{
    public static class Mother
    {
        public static User GetUser(string email = "b@s.com", string name = "brian", string pass = "pass")
        {
            var user = new User()
                {
                    Email = email,
                    Name = name,
                    Password = pass
                };
            user.HashPassword();
            return user;
        }
    }
}