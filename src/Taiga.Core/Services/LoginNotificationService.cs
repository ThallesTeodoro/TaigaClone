using System;
using System.IO;
using MimeKit;
using MimeKit.Text;
using Taiga.Core.Configuration;
using Taiga.Core.Interfaces.ServicesInterfaces;

namespace Taiga.Core.Services
{
    public class LoginNotificationService : ILoginNotificationService
    {
        private readonly EmailConfiguration configuration;

        public LoginNotificationService()
        {
            configuration = new EmailConfiguration();
        }

        public void SendNotification(string userName, string userEmail)
        {
            var message = new MimeMessage();
            message.To.Add(new MailboxAddress(userName, userEmail));
            message.From.Add(new MailboxAddress(configuration.FromName, configuration.FromAddres));
            message.Subject = "Taiga - Login Notification";

            string emailBody = string.Empty;
            using (StreamReader reader = new StreamReader(Path.Combine(Directory.GetCurrentDirectory(), "Features/Login/LoginNotification.cshtml")))
            {
                emailBody = reader.ReadToEnd();
            }

            emailBody = emailBody.Replace("{UserName}", userName);
            emailBody = emailBody.Replace("{Date}", DateTime.Now.ToString("dddd, dd MMMM yyyy"));

            message.Body = new TextPart(TextFormat.Html)
            {
                Text = emailBody
            };

            using (var client = new MailKit.Net.Smtp.SmtpClient())
            {
                client.Connect(configuration.Host, configuration.Port, false);
                client.Authenticate(configuration.UserName, configuration.Password);
                client.Send(message);
                client.Disconnect(true);
            };
        }
    }
}
