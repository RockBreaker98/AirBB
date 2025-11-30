using AirBB.Models.DomainModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AirBB.Models.DataLayer.Configuration
{
    public class ResidenceConfig : IEntityTypeConfiguration<Residence>
    {
        public void Configure(EntityTypeBuilder<Residence> entity)
        {
            entity.HasKey(r => r.ResidenceId);

            entity.Property(r => r.Name)
                  .IsRequired()
                  .HasMaxLength(50);

            entity.Property(r => r.Bathrooms)
                  .HasColumnType("decimal(4,1)");

            entity.Property(r => r.Price)
                  .HasColumnType("decimal(10,2)");

            entity.Property(r => r.PricePerNight)
                  .HasColumnType("decimal(10,2)");

            entity.HasOne(r => r.Location)
                  .WithMany(l => l.Residences)
                  .HasForeignKey(r => r.LocationId);

            entity.HasOne(r => r.Owner)
                  .WithMany()
                  .HasForeignKey(r => r.OwnerId);

            // Seed sample residences
            entity.HasData(
                new Residence
                {
                    ResidenceId = 101,
                    LocationId = 1,
                    OwnerId = 1,
                    Name = "Boston Back Bay Condo",
                    Accommodation = 4,
                    Bedrooms = 2,
                    Bathrooms = 1.0m,
                    BuiltYear = 2005,
                    Image = "boston.jpg",
                    ResidencePicture = "boston.jpg",
                    GuestNumber = 4,
                    BedroomNumber = 2,
                    BathroomNumber = 1.0m,
                    Price = 179m,
                    PricePerNight = 179m
                },
                new Residence
                {
                    ResidenceId = 102,
                    LocationId = 2,
                    OwnerId = 1,
                    Name = "Chicago Loop Apartment",
                    Accommodation = 3,
                    Bedrooms = 1,
                    Bathrooms = 1.0m,
                    BuiltYear = 2010,
                    Image = "chicago.jpg",
                    ResidencePicture = "chicago.jpg",
                    GuestNumber = 3,
                    BedroomNumber = 1,
                    BathroomNumber = 1.0m,
                    Price = 129m,
                    PricePerNight = 129m
                },
                new Residence
                {
                    ResidenceId = 103,
                    LocationId = 3,
                    OwnerId = 1,
                    Name = "NYC Midtown Studio",
                    Accommodation = 2,
                    Bedrooms = 1,
                    Bathrooms = 1.0m,
                    BuiltYear = 2015,
                    Image = "nyc.jpg",
                    ResidencePicture = "nyc.jpg",
                    GuestNumber = 2,
                    BedroomNumber = 1,
                    BathroomNumber = 1.0m,
                    Price = 149m,
                    PricePerNight = 149m
                }
            );
        }
    }
}