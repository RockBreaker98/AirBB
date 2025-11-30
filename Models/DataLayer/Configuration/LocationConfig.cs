using AirBB.Models.DomainModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AirBB.Models.DataLayer.Configuration
{
    public class LocationConfig : IEntityTypeConfiguration<Location>
    {
        public void Configure(EntityTypeBuilder<Location> entity)
        {
            entity.HasKey(l => l.LocationId);

            entity.Property(l => l.City)
                  .IsRequired()
                  .HasMaxLength(50);

            entity.Property(l => l.State)
                  .HasMaxLength(50);

            entity.Property(l => l.Country)
                  .HasMaxLength(50);

            // Seed locations
            entity.HasData(
                new Location { LocationId = 1, City = "Boston" },
                new Location { LocationId = 2, City = "Chicago" },
                new Location { LocationId = 3, City = "New York" }
            );
        }
    }
}