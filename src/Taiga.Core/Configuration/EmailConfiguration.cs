using System;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace Taiga.Core.Configuration
{
    public class EmailConfiguration
    {
        public readonly string Driver;
        public readonly string Host;
        public readonly int Port;
        public readonly string UserName;
        public readonly string Password;
        public readonly string Encryption;
        public readonly string FromAddres;
        public readonly string FromName;

        public EmailConfiguration()
        {
            IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            Driver = configuration.GetSection("EmailConfiguration").GetSection("Driver").Value;
            Host = configuration.GetSection("EmailConfiguration").GetSection("Host").Value;
            Port = Int32.Parse(configuration.GetSection("EmailConfiguration").GetSection("Port").Value);
            UserName = configuration.GetSection("EmailConfiguration").GetSection("UserName").Value;
            Password = configuration.GetSection("EmailConfiguration").GetSection("Password").Value;
            Encryption = configuration.GetSection("EmailConfiguration").GetSection("Encryption").Value;
            FromAddres = configuration.GetSection("EmailConfiguration").GetSection("FromAddres").Value;
            FromName = configuration.GetSection("EmailConfiguration").GetSection("FromName").Value;
        }
    }
}
