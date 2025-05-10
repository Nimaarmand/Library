using Domain.Entities.Books;
using Domain.Entities.Reservations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Domain.Entities.Reservations;

namespace Application.Features.Definitions.Contexts
{
    public interface IApplicationDbContext
    {
        DbContext Context { get; }
        DatabaseFacade Database { get; }
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
         DbSet<BookCategories> BookCategories { get; set; }
        public DbSet<Book> Books { get; set; }
        DbSet<Deliverys> Deliveries { get; set; }
         DbSet<DeliveryStatus> DeliveryStatus { get; set; }
         DbSet<Reservation> Reservations { get; set; }





        int SaveChanges();
        int SaveChanges(bool acceptAllChangesOnSuccess);
        Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default);
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken());
        EntityEntry Entry(object entity);
        void Dispose();

    }
}
