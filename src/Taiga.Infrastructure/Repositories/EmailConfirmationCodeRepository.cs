using Taiga.Core.Entities;
using Taiga.Core.Interfaces;
using Taiga.Infrastructure.Data;
using System.Linq;

namespace Taiga.Infrastructure.Repositories
{
    public class EmailConfirmationCodeRepository : Repository<EmailConfirmationCode>, IEmailConfirmationCodeRepository
    {
        public EmailConfirmationCodeRepository(TaigaContext context) : base(context){}

        public EmailConfirmationCode FindUniqueByEmail(string email)
        {
            return dbSet.Where(p => p.Email == email && p.Type == CodeType.Register)
                .FirstOrDefault();
        }
    }
}
