
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Taiga.Infrastructure.Data
{
    public class TaigaDbContextFactory : IDesignTimeDbContextFactory<TaigaContext>
    {
        public string ConnectionString { get; set; }

        public TaigaDbContextFactory()
        {
            IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
            this.ConnectionString = configuration.GetConnectionString("Default");
        }

        public TaigaContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<TaigaContext>();
            optionsBuilder.UseMySql(ConnectionString);

            return new TaigaContext(optionsBuilder.Options);
        }
    }
}
