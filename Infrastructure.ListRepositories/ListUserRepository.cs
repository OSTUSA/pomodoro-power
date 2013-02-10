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
        /// <summary>
        /// An internal list backing the this repository
        /// </summary>
        protected static List<User> Users { get; set; }

        private readonly object listLock = new object();

        public ListUserRepository(List<User> users)
        {
            Users = users;
        }

        /// <summary>
        /// Get a User by id
        /// </summary>
        /// <param name="id">The unique id of a User object</param>
        /// <returns>A User object</returns>
        public User Get(long id)
        {
            return Users.SingleOrDefault(x => x.Id == id);
        }

        /// <summary>
        /// Persist a User. If an ID is present it will update,
        /// if not it will insert
        /// </summary>
        /// <param name="user">The User object to persist</param>
        public void Store(User user)
        {
            var fetched = Get(user.Id);
            if (fetched == null)
            {
                lock (listLock)
                {
                    Users.Add(user);
                }
            }
        }
    }
}
