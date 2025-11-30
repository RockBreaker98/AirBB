using AirBB.Models.DomainModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AirBB.Models.DataLayer.Configuration
{
    public class ExperienceConfig : IEntityTypeConfiguration<Experience>
    {
        public void Configure(EntityTypeBuilder<Experience> entity)
        {
            entity.HasKey(e => e.ExperienceId);

            entity.Property(e => e.Title)
                  .IsRequired()
                  .HasMaxLength(100);

            entity.Property(e => e.Price)
                  .HasColumnType("decimal(10,2)");

            entity.Property(e => e.DiscountPercent)
                  .HasColumnType("decimal(5,2)");

            entity.HasOne(e => e.Category)
                  .WithMany(c => c.Experiences)
                  .HasForeignKey(e => e.CategoryId);
        }
    }
}    