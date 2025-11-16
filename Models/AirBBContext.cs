// File: Models/AirBBContext.cs
using Microsoft.EntityFrameworkCore;
using System;

namespace AirBB.Models
{
    public class AirBBContext : DbContext
    {
        public AirBBContext(DbContextOptions<AirBBContext> options) : base(options) { }

        public DbSet<Location> Locations { get; set; }
        public DbSet<Residence> Residences { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Client> Clients { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed an Owner (for remote validation testing)
            modelBuilder.Entity<Client>().HasData(
                new Client
                {
                    ClientId = 1,
                    Name = "John Owner",
                    PhoneNumber = "111-111-1111",
                    Email = "owner@example.com",
                    DOB = new DateTime(1980, 1, 1),
                    UserType = UserType.Owner
                }
            );

            // Seed Locations
            modelBuilder.Entity<Location>().HasData(
                new Location { LocationId = 1, Name = "Boston" },
                new Location { LocationId = 2, Name = "Chicago" },
                new Location { LocationId = 3, Name = "New York" }
            );

            // Seed Residences (ensure decimals have m suffix)
            modelBuilder.Entity<Residence>().HasData(
                new Residence
                {
                    ResidenceId = 101,
                    Name = "Boston Back Bay Condo",
                    LocationId = 1,
                    OwnerId = 1,
                    Accommodation = 4,
                    Bedrooms = 2,
                    Bathrooms = 1,
                    BuiltYear = 2005,
                    Price = 179m,
                    Image = "boston.jpg"
                },
                new Residence
                {
                    ResidenceId = 102,
                    Name = "Chicago Loop Apartment",
                    LocationId = 2,
                    OwnerId = 1,
                    Accommodation = 3,
                    Bedrooms = 1,
                    Bathrooms = 1,
                    BuiltYear = 2010,
                    Price = 129m,
                    Image = "chicago.jpg"
                },
                new Residence
                {
                    ResidenceId = 103,
                    Name = "NYC Midtown Studio",
                    LocationId = 3,
                    OwnerId = 1,
                    Accommodation = 2,
                    Bedrooms = 1,
                    Bathrooms = 1,
                    BuiltYear = 2015,
                    Price = 149m,
                    Image = "nyc.jpg"
                }
            );
        }
    }
}
