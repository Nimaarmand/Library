using Domain.Entities.Reservations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations.ApplicationConfigurations
{
    public class DeliveryStatusConfiguration : IEntityTypeConfiguration<DeliveryStatus>
    {
        public void Configure(EntityTypeBuilder<DeliveryStatus> builder)
        {
            // فیلتر کردن رکوردهای حذف‌شده
            builder.HasQueryFilter(m => EF.Property<int>(m, "IsDeleted") == 0);


            builder.HasOne(ds => ds.Reservation)
             .WithMany()
             .HasForeignKey(ds => ds.ReservationId)
             .OnDelete(DeleteBehavior.Restrict); // یا NO ACTION


        }
    }
}
