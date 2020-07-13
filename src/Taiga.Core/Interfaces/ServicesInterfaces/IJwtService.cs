using Taiga.Core.Entities;

namespace Taiga.Core.Interfaces.ServicesInterfaces
{
    public interface IJwtService
    {
        string GenerateToken(User user, string secret, string expDate);
    }
}
