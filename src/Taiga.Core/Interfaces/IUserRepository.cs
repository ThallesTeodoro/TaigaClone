using Taiga.Core.Entities;

namespace Taiga.Core.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        User FindUniqueByEmailOrUserName(string emailOrUserName);
        User FindByEmail(string email);
        int CountByUserName(string userName);
    }
}
