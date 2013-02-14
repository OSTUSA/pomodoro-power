using Core.Domain.Model.Users;

namespace Infrastructure.ListRepositories.Proxy.Users
{
    public class User : Core.Domain.Model.Users.User
    {
        public void SetId(long id)
        {
            Id = id;
        }
    }
}
