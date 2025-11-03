    using Microsoft.EntityFrameworkCore;

    namespace AirBB.Models
    {
        public class AirBBContext : DbContext
        {
            public AirBBContext(DbContextOptions<AirBBContext> options)
                : base(options)
            {
            }

            public DbSet<Location> Locations { get; set; }
            public DbSet<Residence> Residences { get; set; }
            public DbSet<Reservation> Reservations { get; set; }
            public DbSet<Client> Clients { get; set; }

            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                // --- Seed Locations ---
                modelBuilder.Entity<Location>().HasData(
                    new Location { LocationId = 1, Name = "Boston" },
                    new Location { LocationId = 2, Name = "Chicago" },
                    new Location { LocationId = 3, Name = "New York" }
                );

                // --- Seed Residences ---
                modelBuilder.Entity<Residence>().HasData(
                    new Residence
                    {
                        ResidenceId = 101,
                        Name = "Boston Back Bay Condo",
                        ResidencePicture = "boston.jpg",
                        LocationId = 1,
                        GuestNumber = 4,
                        BedroomNumber = 2,
                        BathroomNumber = 1,
                        PricePerNight = 179
                    },
                    new Residence
                    {
                        ResidenceId = 102,
                        Name = "Chicago Loop Apartment",
                        ResidencePicture = "chicago.jpg",
                        LocationId = 2,
                        GuestNumber = 3,
                        BedroomNumber = 1,
                        BathroomNumber = 1,
                        PricePerNight = 129
                    },
                    new Residence
                    {
                        ResidenceId = 103,
                        Name = "NYC Midtown Studio",
                        ResidencePicture = "nyc.jpg",
                        LocationId = 3,
                        GuestNumber = 2,
                        BedroomNumber = 1,
                        BathroomNumber = 1,
                        PricePerNight = 149
                    }
                );
            }
        }
    }

