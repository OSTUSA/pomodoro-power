using Core.Domain.Model.Users;
using NHibernate;
using NHibernate.Linq;
using System.Linq;

namespace Infrastructure.NHibernate.Repositories
{
    [Factory("MainFactory")]
    public class UserRepository : IUserRepository
    {
        protected ISession Session;

        public UserRepository(ISession session)
        {
            Session = session;
        }

        public void Store(User user)
        {
            using (ITransaction trans = Session.BeginTransaction())
            {
                Session.SaveOrUpdate(user);
                trans.Commit();
            }
        }

        public User Get(long id)
        {
            throw new System.NotImplementedException();
        }

        public User GetByEmail(string email)
        {
            return Session.Query<User>().SingleOrDefault(u => u.Email == email);
        }
    }
}
