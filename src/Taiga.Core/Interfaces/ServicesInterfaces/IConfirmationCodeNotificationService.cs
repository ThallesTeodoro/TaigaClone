namespace Taiga.Core.Interfaces.ServicesInterfaces
{
    public interface IConfirmationCodeNotificationService
    {
        void SendNotification(string userName, string userEmail, int code);
    }
}
