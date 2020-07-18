using Taiga.Core.Entities;
using Taiga.Core.Interfaces;
using Taiga.Infrastructure.Data;
using System.Linq;

namespace Taiga.Infrastructure.Repositories
{
    public class AttemptsQuantityRepository : Repository<AttemptsQuantity>, IAttemptsQuantityRepository
    {
        public AttemptsQuantityRepository(TaigaContext context) : base(context){}

        public AttemptsQuantity FindUniqueByEmailAndType(string email, EndpointType type)
        {
            return dbSet.Where(p => p.Email == email && p.Type == type)
                        .FirstOrDefault();
        }
    }
}
