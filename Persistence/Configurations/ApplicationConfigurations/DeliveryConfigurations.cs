using Domain.Entities.Reservations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations.ApplicationConfigurations
{
    public class DeliveryStatusConfiguration : IEntityTypeConfiguration<Deliverys>
    {
        public void Configure(EntityTypeBuilder<Deliverys> builder)
        {
            // فیلتر کردن رکوردهای حذف‌شده
            builder.HasQueryFilter(m => EF.Property<int>(m, "IsDeleted") == 0);

            // تنظیم ارتباط کلید خارجی با Reservation
            builder.HasOne(d => d.Reservation)
                   .WithMany()
                   .HasForeignKey(d => d.ReservationId)
                   .OnDelete(DeleteBehavior.Restrict); // یا .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
