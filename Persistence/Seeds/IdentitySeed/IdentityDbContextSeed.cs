using Microsoft.EntityFrameworkCore;

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
