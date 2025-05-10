using Domain.Entities.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Persistence.Configurations.IdentityConfigurations;
using Persistence.Seeds.IdentitySeed;

namespace Persistence.Contexts
{
    public class IdentityContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
    {
        public IdentityContext(DbContextOptions<IdentityContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            // base.OnModelCreating(builder);

            #region Schema Sql

            builder.Entity<IdentityUser<string>>().ToTable("Users", "identity");
            builder.Entity<IdentityRole<string>>().ToTable("Roles", "identity");
            builder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaims", "identity");
            builder.Entity<IdentityUserClaim<string>>().ToTable("UserClaims", "identity");
            builder.Entity<IdentityUserLogin<string>>().ToTable("UserLogins", "identity");
            builder.Entity<IdentityUserRole<string>>().ToTable("UserRoles", "identity");
            builder.Entity<IdentityUserToken<string>>().ToTable("UserTokens", "identity");

            #endregion

            builder.Entity<IdentityUserLogin<string>>().HasKey(i => new { i.ProviderKey, i.LoginProvider });
            builder.Entity<IdentityUserRole<string>>().HasKey(i => new { i.UserId, i.RoleId });
            builder.Entity<IdentityUserToken<string>>().HasKey(i => new { i.UserId, i.LoginProvider });

            #region Configuration

            builder.ApplyConfiguration(new RoleConfiguration());
            builder.ApplyConfiguration(new UserConfiguration());
            builder.ApplyConfiguration(new UserRoleConfiguration());

            #endregion

            //Seed Data
            IdentityDbContextSeed.SeedData(builder);

        }
    }
}
