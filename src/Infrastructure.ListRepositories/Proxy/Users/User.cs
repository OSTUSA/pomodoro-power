using Core.Domain.Model;
using Core.Domain.Model.Users;

namespace Infrastructure.ListRepositories.Proxy.Users
{
    public class User : Core.Domain.Model.Users.User, IEntity<User>
    {
        public void SetId(long id)
        {
            Id = id;
        }
    }
}
