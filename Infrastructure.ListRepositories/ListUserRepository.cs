using System.Collections.Generic;
using Core.Domain.Model.Users;
using System.Linq;

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

        public User Get(long id)
        {
            return Users.SingleOrDefault(x => x.Id == id);
        }

        public User GetByEmail(string email)
        {
            return Users.SingleOrDefault(x => x.Email == email);
        }

        public void Store(User user)
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

        protected void Insert(User user)
        {
            lock (_listLock)
            {
                user.Id = ++_id;
                Users.Add(user);
            }
        }
    }
}
