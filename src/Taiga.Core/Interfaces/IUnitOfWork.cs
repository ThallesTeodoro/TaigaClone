namespace Taiga.Core.Interfaces
{
    public interface IUnitOfWork
    {
        IUserRepository UserRepository { get; }
        IEmailConfirmationCodeRepository EmailConfirmationCodeRepository { get; }
        IAttemptsQuantityRepository AttemptsQuantityRepository { get; }
        void Commit();
        void Rollback();
    }
}
