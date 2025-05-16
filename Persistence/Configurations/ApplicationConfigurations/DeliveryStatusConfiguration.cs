using Domain.Entities.Reservations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace Persistence.Configurations.ApplicationConfigurations
{
   
        public class DeliverysConfiguration : IEntityTypeConfiguration<Deliverys>
        {
            public void Configure(EntityTypeBuilder<Deliverys> builder)
            {
                // فعال‌سازی حذف زنجیره‌ای برای ReservationId
                builder.HasOne(d => d.Reservation)
                    .WithMany()
                    .HasForeignKey(d => d.ReservationId)
                    .OnDelete(DeleteBehavior.Cascade);
            }
        }
    
}
