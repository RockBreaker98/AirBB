using AirBB.Models.DomainModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AirBB.Models.DataLayer.Configuration
{
    public class ReservationConfig : IEntityTypeConfiguration<Reservation>
    {
        public void Configure(EntityTypeBuilder<Reservation> entity)
        {
            entity.HasKey(r => r.ReservationId);

            entity.HasOne(r => r.Residence)
                  .WithMany(res => res.Reservations)
                  .HasForeignKey(r => r.ResidenceId);
        }
    }
}