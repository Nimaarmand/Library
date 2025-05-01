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
        /// <summary>
        /// برگرداندن تعداد
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        Task<long> Count<TEntity>() where TEntity : class;
        /// <summary>
        /// بررسی مجودیت در دیتا بیس
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="predicate"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<bool> Any<TEntity>(Expression<Func<TEntity, bool>> predicate = null, CancellationToken cancellationToken = default) where TEntity : class;
        /// <summary>
        /// دریافت اولین رکورد د
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="includeProperties"></param>
        /// <returns></returns>
        Task<TEntity> GetFirstRecord<TEntity>(params Expression<Func<TEntity, object>>[] includeProperties) where TEntity : class;
        /// <summary>
        /// دریافت اخرین رکورد
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="includeProperties"></param>
        /// <returns></returns>
        Task<TEntity> GetLastRecord<TEntity>(params Expression<Func<TEntity, object>>[] includeProperties) where TEntity : class;
        /// <summary>
        /// دریافت اطلاعات با استفاده از شناسه 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="predicate"></param>
        /// <param name="cancellationToken"></param>
        /// <param name="includeProperties"></param>
        /// <returns></returns>
        Task<TEntity> Find<TEntity>(Expression<Func<TEntity, bool>> predicate,
                                                     CancellationToken cancellationToken = default,
                                                     params Expression<Func<TEntity, object>>[] includeProperties)
                                                     where TEntity : class;
        /// <summary>
        /// دریافت اطلاعات با استفاده از ایدی
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="id"></param>
        /// <param name="includeProperties"></param>
        /// <returns></returns>
        Task<TEntity> GetById<TEntity>(object id, params Expression<Func<TEntity, object>>[] includeProperties) where TEntity : class;
        Task<TEntity> GetByIdSingle<TEntity>(object id, params Expression<Func<TEntity, object>>[] includeProperties) where TEntity : class;
        Task<TEntity> GetByIdLast<TEntity>(object id, params Expression<Func<TEntity, object>>[] includeProperties) where TEntity : class;
        /// <summary>
        /// نمایش لیست اطلاعات بصورت کامل
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="predicate"></param>
        /// <param name="orderBy"></param>
        /// <param name="ascending"></param>
        /// <param name="cancellationToken"></param>
        /// <param name="includeProperties"></param>
        /// <returns></returns>
        Task<IEnumerable<TEntity>> GetAll<TEntity>(Expression<Func<TEntity, bool>> predicate = null,
                                                   Expression<Func<TEntity, object>> orderBy = null,
                                                   bool ascending = true,
                                                   CancellationToken cancellationToken = default,
                                                   params Expression<Func<TEntity, object>>[] includeProperties)
                                                   where TEntity : class;
        /// <summary>
        /// نمایش لیست اطلاعات با استفاده از شرایط خاص
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="predicate"></param>
        /// <param name="orderBy"></param>
        /// <param name="ascending"></param>
        /// <param name="includeProperties"></param>
        /// <returns></returns>
        IQueryable<TEntity> GetAllQueryable<TEntity>(Expression<Func<TEntity, bool>> predicate = null,
                                                     Expression<Func<TEntity, object>> orderBy = null,
                                                     bool ascending = true,
                                                     params Expression<Func<TEntity, object>>[] includeProperties)
                                                     where TEntity : class;
        /// <summary>
        /// صفحه بندی
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="predicate"></param>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <param name="orderBy"></param>
        /// <param name="ascending"></param>
        /// <param name="cancellationToken"></param>
        /// <param name="includeProperties"></param>
        /// <returns></returns>
        Task<IEnumerable<TEntity>> GetPaged<TEntity>(Expression<Func<TEntity, bool>> predicate = null,
                                                     int pageNumber = 1,
                                                     int pageSize = 20,
                                                     Expression<Func<TEntity, object>> orderBy = null,
                                                     bool ascending = true,
                                                     CancellationToken cancellationToken = default,
                                                     params Expression<Func<TEntity, object>>[] includeProperties)
                                                     where TEntity : class;
        /// <summary>
        /// افزودن اطلاعات به دیتا بیس
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<TEntity> AddAsync<TEntity>(TEntity entity) where TEntity : class;
        /// <summary>
        /// افزودن گروهی اطلاعات
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entities"></param>
        /// <returns></returns>
        Task<IEnumerable<TEntity>> AddRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class;
        /// <summary>
        /// حذف اطلاعات
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity"></param>
        Task RemoveAsync<TEntity>(TEntity entity) where TEntity : class;
        /// <summary>
        /// حذف گروهی اطلاعات
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entities"></param>
        void RemoveRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class;
        /// <summary>
        /// ویرایش اطلاعات
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity"></param>
        Task UpdateAsync<TEntity>(TEntity entity) where TEntity : class;
        /// <summary>
        /// ویرایش گروهی اطلاعات
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entities"></param>
        void UpdateRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class;
        /// <summary>
        /// ذخیره تغیرات در دیتا بیس
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task SaveChangesAsync(CancellationToken cancellationToken = default);

    }
}
