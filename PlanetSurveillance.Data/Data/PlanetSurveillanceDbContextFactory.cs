using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace PlanetSurveillance.Data
{
    public class PlanetSurveillanceDbContextFactory : IDesignTimeDbContextFactory<PlanetSurveillanceDbContext>
    {
        public PlanetSurveillanceDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())  
                .AddJsonFile("appsettings.json")               
                .Build();

            var builder = new DbContextOptionsBuilder<PlanetSurveillanceDbContext>();
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            builder.UseSqlServer(connectionString);

            return new PlanetSurveillanceDbContext(builder.Options);
        }
    }
}
