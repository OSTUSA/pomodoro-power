using System.Collections.Generic;
using System.Linq;
using Core.Domain.Model;
using Core.Domain.Model.Users;
using User = Infrastructure.ListRepositories.Proxy.Users.User;

namespace Infrastructure.ListRepositories
{
    /// <summary>
    /// A UserRepository backed by a List of type User
    /// </summary>
    public class ListUserRepository : IUserRepository
    {
        protected static List<User> Users { get; set; }

        private static long _id;

        private readonly object _listLock = new object();

        static ListUserRepository()
        {
            Users = new List<User>();
            _id = 0;
        }

        public Core.Domain.Model.Users.User Get(long id)
        {
            return Users.SingleOrDefault(x => x.Id == id);
        }

        public Core.Domain.Model.Users.User GetByEmail(string email)
        {
            return Users.SingleOrDefault(x => x.Email == email);
        }

        public void Store(Core.Domain.Model.Users.User user)
        {
            var fetched = Get(user.Id);
            if (fetched == null)
                Insert(user);
        }

        public void Clear()
        {
            lock (_listLock)
            {
                Users = new List<User>();
                _id = 0;
            }
        }

        protected void Insert(Core.Domain.Model.Users.User user)
        {
            lock (_listLock)
            {
                var cast = user as User;
                cast.SetId(++_id);
                Users.Add(cast);
            }
        }
    }
}
