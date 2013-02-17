using Core.Domain.Model.Users;

namespace Test.Integration.Infrastructure.NHibernate.Repositories.UserRepositoryTest
{
    public static class Mother
    {
        public static User GetUser(string email = "b@s.com")
        {
            var user = new User()
                {
                    Email = email,
                    Name = "brian",
                    Password = "pass"
                };
            user.HashPassword();
            return user;
        }
    }
}