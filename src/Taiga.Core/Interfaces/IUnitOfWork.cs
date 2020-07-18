namespace Taiga.Core.Interfaces
{
    public interface IUnitOfWork
    {
        IUserRepository UserRepository { get; }
        IEmailConfirmationCodeRepository EmailConfirmationCodeRepository { get; }
        IAttemptsQuantityRepository AttemptsQuantityRepository { get; }
        IProjectRepository ProjectRepository { get; }
        void Commit();
        void Rollback();
    }
}
