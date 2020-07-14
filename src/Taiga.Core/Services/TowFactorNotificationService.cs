using System;
using System.IO;
using MimeKit;
using MimeKit.Text;
using Taiga.Core.Configuration;
using Taiga.Core.Interfaces.ServicesInterfaces;

namespace Taiga.Core.Services
{
    public class TowFactorNotificationService : ITowFactorNotificationService
    {
        private readonly EmailConfiguration configuration;

        public TowFactorNotificationService()
        {
            configuration = new EmailConfiguration();
        }

        public void SendNotification(string userName, string userEmail, int code)
        {
            var message = new MimeMessage();
            message.To.Add(new MailboxAddress(userName, userEmail));
            message.From.Add(new MailboxAddress(configuration.FromName, configuration.FromAddres));
            message.Subject = "Taiga - Email Confirmation";

            string emailBody = string.Empty;
            using (StreamReader reader = new StreamReader(Path.Combine(Directory.GetCurrentDirectory(), "Features/Login/EmailTowFactor.cshtml")))
            {
                emailBody = reader.ReadToEnd();
            }

            emailBody = emailBody.Replace("{UserName}", userName);
            emailBody = emailBody.Replace("{Code}", code.ToString());

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
