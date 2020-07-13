namespace Taiga.Core.Interfaces.ServicesInterfaces
{
    public interface IUnitServiceOfWork
    {
        IConfirmationCodeNotificationService ConfirmationCodeNotificationService { get; }
        IJwtService JwtService { get; }
    }
}
