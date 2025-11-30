using AirBB.Models.DomainModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace AirBB.Models.DataLayer.Configuration
{
    public class ClientConfig : IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> entity)
        {
            entity.HasKey(c => c.ClientId);

            entity.Property(c => c.Name)
                  .IsRequired()
                  .HasMaxLength(60);

            entity.Property(c => c.PhoneNumber)
                  .HasMaxLength(10);

            entity.Property(c => c.Email)
                  .HasMaxLength(100);

            entity.Property(c => c.SSN)
                  .IsRequired()
                  .HasMaxLength(9);

            // Seed owner for residences
            entity.HasData(
                new Client
                {
                    ClientId = 1,
                    Name = "John Owner",
                    PhoneNumber = "1111111111",
                    Email = "owner@example.com",
                    SSN = "000000000",
                    DOB = new DateTime(1980, 1, 1),
                    UserType = UserType.Owner
                }
            );
        }
    }
}