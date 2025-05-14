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
        base.OnModelCreating(builder); // این را فراموش نکن

       

        #region کلیدهای ترکیبی برای جدول‌های میانی
        builder.Entity<IdentityUserLogin<string>>().HasKey(l => new { l.LoginProvider, l.ProviderKey });
        builder.Entity<IdentityUserRole<string>>().HasKey(r => new { r.UserId, r.RoleId });
        builder.Entity<IdentityUserToken<string>>().HasKey(t => new { t.UserId, t.LoginProvider, t.Name });
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
