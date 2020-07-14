namespace Taiga.Core.Interfaces.ServicesInterfaces
{
    public interface ITowFactorNotificationService
    {
        void SendNotification(string userName, string userEmail, int code);
    }
}
