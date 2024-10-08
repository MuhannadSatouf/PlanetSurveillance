using Microsoft.EntityFrameworkCore;
using PlanetSurveillance.Data.Models;

namespace PlanetSurveillance.Data
{
    public class PlanetSurveillanceDbContext : DbContext
    {
        public virtual DbSet<Person> Persons { get; set; }
        public virtual DbSet<Planet> Planets { get; set; }
        public virtual DbSet<Visit> Visits { get; set; }


        public PlanetSurveillanceDbContext(DbContextOptions<PlanetSurveillanceDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
