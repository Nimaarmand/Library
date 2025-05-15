using Application.Features.Definitions.Contexts;
using Domain.Entities.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Persistence.Configurations.IdentityConfigurations;
using Persistence.Seeds.IdentitySeed;


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


        #region کلیدهای ترکیبی برای جدول‌های میانی
        builder.Entity<IdentityUserLogin<string>>().HasKey(i => new { i.ProviderKey, i.LoginProvider });
        builder.Entity<IdentityUserRole<string>>().HasKey(i => new { i.UserId, i.RoleId });
        builder.Entity<IdentityUserToken<string>>().HasKey(i => new { i.UserId, i.LoginProvider });
        #endregion

        #region اعمال تنظیمات اختصاصی (در صورت نیاز)
        builder.ApplyConfiguration(new RoleConfiguration());     // اختیاری
        builder.ApplyConfiguration(new UserConfiguration());     // اختیاری
        builder.ApplyConfiguration(new UserRoleConfiguration()); // اختیاری
        #endregion

        #region Seed اولیه اطلاعات (در صورت نیاز)
        IdentityDbContextSeed.SeedData(builder); // اگر فایل SeedData داری
        #endregion
    }
}
