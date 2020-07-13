using Taiga.Core.Entities;

namespace Taiga.Core.Interfaces
{
    public interface IAttemptsQuantityRepository : IRepository<AttemptsQuantity>
    {
        AttemptsQuantity FindUniqueByEmailAndType(string email, EndpointType type);
    }
}
