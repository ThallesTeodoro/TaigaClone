namespace Taiga.Core.Interfaces.ServicesInterfaces
{
    public interface ILoginNotificationService
    {
        void SendNotification(string userName, string userEmail);
    }
}
