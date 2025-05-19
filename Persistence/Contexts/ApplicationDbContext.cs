using Application.Features.Definitions.Contexts;
using Domain.Entities.Books;
using Domain.Entities.Reservations;
using Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Persistence.Configurations.ApplicationConfigurations;
using Persistence.Seeds.ApplicationSeed;
using System.Reflection.Emit;

namespace Persistence.Contexts
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbContext Context => this;
        public DatabaseFacade Database => base.Database;
        public DbSet<ProfileUser> ProfileUser { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<BookCategories> BookCategories { get; set; }
        public DbSet<Deliverys> Deliveries { get; set; }
        public DbSet<DeliveryStatus> DeliveryStatus { get; set; }
        public DbSet<Reservation> Reservations { get; set; }


        
        protected override void OnModelCreating(ModelBuilder builder)
        {
            #region Schema Sql

            builder.HasDefaultSchema("dbo");
            //builder.Entity<Book>().ToTable(nameof(Books), "Gnr");

            #endregion
          

            #region Configuration

            builder.ApplyConfiguration(new BookConfiguration());
            builder.ApplyConfiguration(new ReservationConfigurations());
            builder.ApplyConfiguration(new DeliveryStatusConfiguration());
         


            //builder.ApplyConfiguration(new BookConfiguration());

            #endregion

            //Seed Data
            ApplicationContextSeed.SeedData(builder);

            base.OnModelCreating(builder);
        }

        public override int SaveChanges()
        {
            var modifiedEntries = ChangeTracker.Entries()
                .Where(p => p.State == EntityState.Modified ||
                p.State == EntityState.Added ||
                p.State == EntityState.Deleted
                );
            foreach (var item in modifiedEntries)
            {
                var entityType = item.Context.Model.FindEntityType(item.Entity.GetType());
                if (entityType != null)
                {
                    var inserted = entityType.FindProperty("InsertTime");
                    var updated = entityType.FindProperty("UpdateTime");
                    var removeTime = entityType.FindProperty("RemoveTime");
                    var isRemoved = entityType.FindProperty("IsRemoved");
                    var isActive = entityType.FindProperty("IsActive");

                    if (item.State == EntityState.Added && inserted != null && isActive != null)
                    {
                        item.Property("InsertTime").CurrentValue = DateTime.Now;
                        item.Property("IsActive").CurrentValue = true;
                    }
                    if (item.State == EntityState.Modified && updated != null)
                    {
                        item.Property("UpdateTime").CurrentValue = DateTime.Now;
                    }

                    if (item.State == EntityState.Deleted && removeTime != null && isRemoved != null)
                    {
                        item.Property("RemoveTime").CurrentValue = DateTime.Now;
                        item.Property("IsRemoved").CurrentValue = true;
                        item.State = EntityState.Modified;
                    }
                }
            }
            return base.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var modifiedEntries = ChangeTracker.Entries()
                .Where(p => p.State == EntityState.Modified ||
                            p.State == EntityState.Added ||
                            p.State == EntityState.Deleted);

            foreach (var item in modifiedEntries)
            {
                var entityType = item.Context.Model.FindEntityType(item.Entity.GetType());
                if (entityType != null)
                {
                    var inserted = entityType.FindProperty("InsertTime");
                    var updated = entityType.FindProperty("UpdateTime");
                    var removeTime = entityType.FindProperty("RemoveTime");
                    var isRemoved = entityType.FindProperty("IsRemoved");
                    var isActive = entityType.FindProperty("IsActive");

                    if (item.State == EntityState.Added && inserted != null && isActive != null)
                    {
                        item.Property("InsertTime").CurrentValue = DateTime.Now;
                        item.Property("IsActive").CurrentValue = true;
                    }

                    if (item.State == EntityState.Modified && updated != null)
                    {
                        item.Property("UpdateTime").CurrentValue = DateTime.Now;
                    }

                    if (item.State == EntityState.Deleted && isRemoved != null && removeTime != null)
                    {
                        item.Property("RemoveTime").CurrentValue = DateTime.Now;
                        item.Property("IsRemoved").CurrentValue = true;
                        item.State = EntityState.Modified;
                    }
                }
            }

            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
