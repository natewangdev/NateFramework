using Nate.Data.Core.Models;
using System.Linq.Expressions;

namespace Nate.Data.EntityFrameworkCore.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task<TEntity?> GetByIdAsync(Guid id, bool includeDeleted = false);
        Task<IEnumerable<TEntity>> GetAllAsync(bool includeDeleted = false);
        Task AddAsync(TEntity entity);
        Task UpdateAsync(TEntity entity);
        Task DeleteAsync(TEntity entity, bool softDelete = true);
        IQueryable<TEntity> Query();
        Task<PagedList<TEntity>> GetPagedAsync(int pageNumber, int pageSize, Expression<Func<TEntity, bool>>? filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null);
    }
}
