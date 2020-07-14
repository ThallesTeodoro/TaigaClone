namespace Taiga.Core.Interfaces.ServicesInterfaces
{
    public interface IUnitServiceOfWork
    {
        IConfirmationCodeNotificationService ConfirmationCodeNotificationService { get; }
        ITowFactorNotificationService TowFactorNotificationService { get; }
        IJwtService JwtService { get; }
    }
}
