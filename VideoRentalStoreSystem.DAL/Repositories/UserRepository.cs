using System.Linq;
using VideoRentalStoreSystem.DAL.DBContextEF;
using VideoRentalStoreSystem.DAL.Interfaces;

namespace VideoRentalStoreSystem.DAL.Repositories
{
    public class UserRepository : GenericRepository<DBVRContext, User>, IUserRepository<User>
    {
        public UserRepository(DBVRContext context) : base(context)
        {
        }

        public User Get(string userName, string password)
        {
            return _context.Users.Where(x => x.UserName.Equals(userName) && x.Password.Equals(password)).FirstOrDefault();
        }
    }
}
