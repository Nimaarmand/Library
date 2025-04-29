using Domain.Entities.Books;
using Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
