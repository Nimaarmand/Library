using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.Repositories
{
    public interface IGenericRepository
    {
        //Task<bool> Any<TEntity>() where TEntity : class;
        Task<long> Count<TEntity>() where TEntity : class;
        Task<bool> Any<TEntity>(Expression<Func<TEntity, bool>> predicate = null, CancellationToken cancellationToken = default) where TEntity : class;

        Task<TEntity> GetFirstRecord<TEntity>(params Expression<Func<TEntity, object>>[] includeProperties) where TEntity : class;
        Task<TEntity> GetLastRecord<TEntity>(params Expression<Func<TEntity, object>>[] includeProperties) where TEntity : class;

        Task<TEntity> Find<TEntity>(Expression<Func<TEntity, bool>> predicate,
                                                     CancellationToken cancellationToken = default,
                                                     params Expression<Func<TEntity, object>>[] includeProperties)
                                                     where TEntity : class;
        Task<TEntity> GetById<TEntity>(object id, params Expression<Func<TEntity, object>>[] includeProperties) where TEntity : class;
        Task<TEntity> GetByIdSingle<TEntity>(object id, params Expression<Func<TEntity, object>>[] includeProperties) where TEntity : class;
        Task<TEntity> GetByIdLast<TEntity>(object id, params Expression<Func<TEntity, object>>[] includeProperties) where TEntity : class;

        Task<IEnumerable<TEntity>> GetAll<TEntity>(Expression<Func<TEntity, bool>> predicate = null,
                                                   Expression<Func<TEntity, object>> orderBy = null,
                                                   bool ascending = true,
                                                   CancellationToken cancellationToken = default,
                                                   params Expression<Func<TEntity, object>>[] includeProperties)
                                                   where TEntity : class;

        IQueryable<TEntity> GetAllQueryable<TEntity>(Expression<Func<TEntity, bool>> predicate = null,
                                                     Expression<Func<TEntity, object>> orderBy = null,
                                                     bool ascending = true,
                                                     params Expression<Func<TEntity, object>>[] includeProperties)
                                                     where TEntity : class;

        Task<IEnumerable<TEntity>> GetPaged<TEntity>(Expression<Func<TEntity, bool>> predicate = null,
                                                     int pageNumber = 1,
                                                     int pageSize = 20,
                                                     Expression<Func<TEntity, object>> orderBy = null,
                                                     bool ascending = true,
                                                     CancellationToken cancellationToken = default,
                                                     params Expression<Func<TEntity, object>>[] includeProperties)
                                                     where TEntity : class;

        Task<TEntity> Add<TEntity>(TEntity entity) where TEntity : class;
        Task<IEnumerable<TEntity>> AddRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class;

        void Remove<TEntity>(TEntity entity) where TEntity : class;
        void RemoveRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class;

        void Update<TEntity>(TEntity entity) where TEntity : class;
        void UpdateRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class;

        Task Save(CancellationToken cancellationToken = default);

    }
}
