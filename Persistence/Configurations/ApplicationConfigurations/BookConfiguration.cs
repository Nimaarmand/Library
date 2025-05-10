using Domain.Entities.Books;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations.ApplicationConfigurations
{
    public class BookConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            //builder.Property(a => a.Pages).HasColumnType("ntext");

            builder.HasQueryFilter(m => EF.Property<int>(m, "IsDeleted") == 0);
        }
    }
}
