using Application.Features.Definitions.Contexts;
using Application.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class GenericRepository : IGenericRepository
    {
        private readonly IApplicationDbContext _context;

        public GenericRepository(IApplicationDbContext context)
        {
            _context = context;
        }
        //public async Task<bool> Any<TEntity>() where TEntity : class
        //{
        //    return await _context.Set<TEntity>().AnyAsync();
        //}
        public async Task<long> Count<TEntity>() where TEntity : class
        {
            return await _context.Set<TEntity>().LongCountAsync();
        }
        public async Task<bool> Any<TEntity>(Expression<Func<TEntity, bool>> predicate = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            if (predicate == null)
            {
                return await _context.Set<TEntity>().AnyAsync(cancellationToken);
            }
            return await _context.Set<TEntity>().AnyAsync(predicate, cancellationToken);
        }

        public async Task<TEntity> GetFirstRecord<TEntity>(params Expression<Func<TEntity, object>>[] includeProperties) where TEntity : class
        {
            IQueryable<TEntity> query = _context.Set<TEntity>();

            foreach (var includeProperty in includeProperties ?? Array.Empty<Expression<Func<TEntity, object>>>())
            {
                query = query.Include(includeProperty);
            }

            return await query.FirstOrDefaultAsync();
        }
        public async Task<TEntity> GetLastRecord<TEntity>(params Expression<Func<TEntity, object>>[] includeProperties) where TEntity : class
        {
            IQueryable<TEntity> query = _context.Set<TEntity>();

            foreach (var includeProperty in includeProperties ?? Array.Empty<Expression<Func<TEntity, object>>>())
            {
                query = query.Include(includeProperty);
            }

            return await query.OrderByDescending(e => EF.Property<object>(e, "Id")).FirstOrDefaultAsync();
        }

        public async Task<TEntity> Find<TEntity>(Expression<Func<TEntity, bool>> predicate,
                                                     CancellationToken cancellationToken = default,
                                                     params Expression<Func<TEntity, object>>[] includeProperties)
                                                     where TEntity : class
        {
            var query = _context.Set<TEntity>().AsQueryable();

            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            return await query.FirstOrDefaultAsync(cancellationToken);
        }
        public async Task<TEntity> GetById<TEntity>(object id, params Expression<Func<TEntity, object>>[] includeProperties) where TEntity : class
        {
            IQueryable<TEntity> query = _context.Set<TEntity>();

            foreach (var includeProperty in includeProperties ?? Array.Empty<Expression<Func<TEntity, object>>>())
            {
                query = query.Include(includeProperty);
            }

            var entityType = _context.Context.Model.FindEntityType(typeof(TEntity));
            var idProperty = entityType.FindPrimaryKey().Properties.FirstOrDefault();

            var parameter = Expression.Parameter(typeof(TEntity), "entity");
            var property = Expression.Property(parameter, idProperty.Name);
            var equals = Expression.Equal(property, Expression.Constant(id));
            var lambda = Expression.Lambda<Func<TEntity, bool>>(equals, parameter);

            return await query.FirstOrDefaultAsync(lambda);
        }
        public async Task<TEntity> GetByIdSingle<TEntity>(object id, params Expression<Func<TEntity, object>>[] includeProperties) where TEntity : class
        {
            IQueryable<TEntity> query = _context.Set<TEntity>();
            foreach (var includeProperty in includeProperties ?? Array.Empty<Expression<Func<TEntity, object>>>())
            {
                query = query.Include(includeProperty);
            }
            var entity = await query.SingleOrDefaultAsync(e => EF.Property<object>(e, "Id").Equals(id));
            return entity ?? Activator.CreateInstance<TEntity>();
        }
        public async Task<TEntity> GetByIdLast<TEntity>(object id, params Expression<Func<TEntity, object>>[] includeProperties) where TEntity : class
        {
            IQueryable<TEntity> query = _context.Set<TEntity>();

            // Include properties if any
            foreach (var includeProperty in includeProperties ?? Array.Empty<Expression<Func<TEntity, object>>>())
            {
                query = query.Include(includeProperty);
            }

            // Get the entity type and its primary key property
            var entityType = _context.Context.Model.FindEntityType(typeof(TEntity));
            var idProperty = entityType.FindPrimaryKey().Properties.FirstOrDefault();

            if (idProperty == null)
            {
                throw new InvalidOperationException("Entity does not have a primary key.");
            }

            // Build the predicate to filter by ID
            var parameter = Expression.Parameter(typeof(TEntity), "entity");
            var property = Expression.Property(parameter, idProperty.Name);
            var equals = Expression.Equal(property, Expression.Constant(id));
            var lambda = Expression.Lambda<Func<TEntity, bool>>(equals, parameter);

            // Apply ordering to the query (order by primary key or another relevant field)
            query = query.OrderBy(e => EF.Property<object>(e, idProperty.Name)); // You can adjust the ordering as needed

            // Execute the query and get the last entity based on the ordering
            return await query.LastOrDefaultAsync(lambda);
        }

        public async Task<IEnumerable<TEntity>> GetAll<TEntity>(Expression<Func<TEntity, bool>> predicate = null,
                                                                Expression<Func<TEntity, object>> orderBy = null,
                                                                bool ascending = true,
                                                                CancellationToken cancellationToken = default,
                                                                params Expression<Func<TEntity, object>>[] includeProperties)
                                                                where TEntity : class
        {
            var query = _context.Set<TEntity>().AsQueryable();

            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            if (orderBy != null)
            {
                query = ascending ? query.OrderBy(orderBy) : query.OrderByDescending(orderBy);
            }

            return await query.ToListAsync(cancellationToken);
        }

        public IQueryable<TEntity> GetAllQueryable<TEntity>(Expression<Func<TEntity, bool>> predicate = null,
                                                            Expression<Func<TEntity, object>> orderBy = null,
                                                            bool ascending = true,
                                                            params Expression<Func<TEntity, object>>[] includeProperties)
                                                            where TEntity : class
        {
            var query = _context.Set<TEntity>().AsQueryable();
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            if (orderBy != null)
            {
                query = ascending ? query.OrderBy(orderBy) : query.OrderByDescending(orderBy);
            }

            return query;
        }

        public async Task<IEnumerable<TEntity>> GetPaged<TEntity>(Expression<Func<TEntity, bool>> predicate = null,
                                                                  int pageNumber = 1,
                                                                  int pageSize = 20,
                                                                  Expression<Func<TEntity, object>> orderBy = null,
                                                                  bool ascending = true,
                                                                  CancellationToken cancellationToken = default,
                                                                  params Expression<Func<TEntity, object>>[] includeProperties)
                                                                  where TEntity : class
        {
            var query = _context.Set<TEntity>().AsQueryable();

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                query = ascending ? query.OrderBy(orderBy) : query.OrderByDescending(orderBy);
            }

            return await query.Skip((pageNumber - 1) * pageSize)
                              .Take(pageSize)
                              .ToListAsync(cancellationToken);
        }

        public async Task<TEntity> Add<TEntity>(TEntity entity) where TEntity : class
        {
            await _context.Set<TEntity>().AddAsync(entity);
            return entity;
        }
        public async Task<IEnumerable<TEntity>> AddRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class
        {
            await _context.Set<TEntity>().AddRangeAsync(entities);
            return entities;
        }

        public void Remove<TEntity>(TEntity entity) where TEntity : class
        {
            _context.Set<TEntity>().Remove(entity);
        }
        public void RemoveRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class
        {
            _context.Set<TEntity>().RemoveRange(entities);
        }

        public void Update<TEntity>(TEntity entity) where TEntity : class
        {
            _context.Entry(entity).State = EntityState.Modified;
        }
        public void UpdateRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class
        {
            _context.Set<TEntity>().UpdateRange(entities);
        }

        public async Task Save(CancellationToken cancellationToken = default)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync(cancellationToken))
            {
                try
                {
                    await _context.SaveChangesAsync(cancellationToken);
                    await transaction.CommitAsync(cancellationToken);
                }
                catch
                {
                    await transaction.RollbackAsync(cancellationToken);
                    throw;
                }
            }
        }

    }
}
