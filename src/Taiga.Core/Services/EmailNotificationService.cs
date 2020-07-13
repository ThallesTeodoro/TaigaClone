using System;
using System.IO;

namespace Taiga.Core.Services
{
    public class LoginNotificationService
    {
        public void SendNotification(Object model)
        {
            // var message = new MimeMessage();
            // message.To.Add(new MailboxAddress(userName, userEmail));
            // message.From.Add(new MailboxAddress("Clone Taiga", "clonetaiga@contact.com"));
            // message.Subject = "Clone Taiga - Login Notification";

            // string body = string.Empty;
            // using (StreamReader reader = new StreamReader(Path.Combine("Areas/Dashboard/Emails/LoginNotification.cshtml")))
            // {
            //     body = reader.ReadToEnd();
            // }

            // body = body.Replace("{UserName}", userName);
            // body = body.Replace("{UserEmail}", userEmail);
            // body = body.Replace("{DateTime}", DateTime.Now.ToString());

            // message.Body = new TextPart(TextFormat.Html)
            // {
            //     Text = body
            // };

            // using (var client = new MailKit.Net.Smtp.SmtpClient())
            // {
            //     client.Connect("smtp.mailtrap.io", 587, false);
            //     client.Authenticate("0f98966e2b1101", "0f54b66c3174d7");
            //     client.Send(message);
            //     client.Disconnect(true);
            // }
        }
    }
}
