using Taiga.Core.Entities;

namespace Taiga.Core.Interfaces
{
    public interface IEmailConfirmationCodeRepository : IRepository<EmailConfirmationCode>
    {
        EmailConfirmationCode FindUniqueByEmail(string email);
    }
}
