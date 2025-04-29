using Domain.Entities.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Seeds.IdentitySeed
{
    public class IdentityDbContextSeed
    {
        public static void SeedData(ModelBuilder builder)
        {
            //builder.Entity<ApplicationRole>().HasData(ApplicationRoleSeed.GetIdentityRoles());
            //builder.Entity<ApplicationUser>().HasData(ApplicationUserSeed.GetUsers());
            //builder.Entity<IdentityUserRole<string>>().HasData(IdentityUserRoleSeed.GetUserRoles());

        }
    }
}
