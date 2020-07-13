using Taiga.Core.Entities;
using Taiga.Core.Interfaces;
using Taiga.Infrastructure.Data;
using System.Linq;

namespace Taiga.Infrastructure.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(TaigaContext context) : base(context){}

        public User FindUniqueByEmailOrUserName(string emailOrUserName)
        {
            return dbSet.Where(p => p.Email == emailOrUserName || p.UserName == emailOrUserName)
                .FirstOrDefault();
        }

        public User FindByEmail(string email)
        {
            return dbSet.Where(p => p.Email == email)
                .FirstOrDefault();
        }

        public int CountByUserName(string userName)
        {
            return dbSet.Where(p => p.UserName == userName).Count();
        }
    }
}
