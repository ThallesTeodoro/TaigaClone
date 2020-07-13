namespace Taiga.Core.Interfaces.ServicesInterfaces
{
    public interface IEmailNotificationService
    {
        void SendNotification(string userName, string userEemail, int code);
    }
}
