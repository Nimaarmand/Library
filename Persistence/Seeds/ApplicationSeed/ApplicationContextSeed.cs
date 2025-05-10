using Microsoft.EntityFrameworkCore;

namespace Persistence.Seeds.ApplicationSeed
{
    public class ApplicationContextSeed
    {
        public static void SeedData(ModelBuilder builder)
        {
            //builder.Entity<Book>().HasData(BookSeed.GetAll());
        }
    }
}
