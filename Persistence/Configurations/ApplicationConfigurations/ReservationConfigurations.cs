using Domain.Entities.Reservations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations.ApplicationConfigurations
{
    public class ReservationConfigurations : IEntityTypeConfiguration<Reservation>
    {
        public void Configure(EntityTypeBuilder<Reservation> builder)
        {
            // فیلتر کردن رکوردهای حذف‌شده
            builder.HasQueryFilter(m => EF.Property<int>(m, "IsDeleted") == 0);
        }
    }
}
