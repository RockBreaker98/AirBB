using AirBB.Models.DomainModels;
using AirBB.Models.DataLayer.Configuration;
using Microsoft.EntityFrameworkCore;

namespace AirBB.Models.DataLayer
{
    public class AirBBContext : DbContext
    {
        public AirBBContext(DbContextOptions<AirBBContext> options)
            : base(options) { }

        public DbSet<Location> Locations { get; set; } = default!;
        public DbSet<Residence> Residences { get; set; } = default!;
        public DbSet<Reservation> Reservations { get; set; } = default!;
        public DbSet<Client> Clients { get; set; } = default!;
        public DbSet<Category> Categories { get; set; } = default!;
        public DbSet<Experience> Experiences { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ClientConfig());
            modelBuilder.ApplyConfiguration(new LocationConfig());
            modelBuilder.ApplyConfiguration(new ResidenceConfig());
            modelBuilder.ApplyConfiguration(new ReservationConfig());
            modelBuilder.ApplyConfiguration(new CategoryConfig());
            modelBuilder.ApplyConfiguration(new ExperienceConfig());
        }
    }
}