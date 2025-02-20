using Microsoft.EntityFrameworkCore;
using Nate.Core.Models.Entities;
using Nate.Core.Services.CurrentUser;
using Nate.Data.Core.Models;
using Nate.Data.EntityFrameworkCore.Interfaces;
using System.Linq.Expressions;

namespace Nate.Data.EntityFrameworkCore.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class, IEntity
    {
        protected readonly DbContext _context;
        protected readonly DbSet<TEntity> _dbSet;
        private readonly ICurrentUserService _currentUserService;

        public Repository(DbContext context, ICurrentUserService currentUserService)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
            _currentUserService = currentUserService;
        }

        public async Task<TEntity?> GetByIdAsync(Guid id, bool includeDeleted = false)
        {
            var query = _dbSet.AsQueryable();

            if (includeDeleted)
            {
                query = query.IgnoreQueryFilters();
            }

            return await query.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(bool includeDeleted = false)
        {
            if (includeDeleted)
            {
                return await _dbSet.IgnoreQueryFilters().ToListAsync();
            }

            return await _dbSet.ToListAsync();
        }

        public async Task AddAsync(TEntity entity)
        {
            if (typeof(IAuditEntity).IsAssignableFrom(typeof(TEntity)))
            {
                ((IAuditEntity)entity).CreatedTime = DateTime.UtcNow;
                ((IAuditEntity)entity).CreatedBy = _currentUserService.UserId;
            }

            await _dbSet.AddAsync(entity);
        }

        public Task UpdateAsync(TEntity entity)
        {
            if (typeof(IAuditEntity).IsAssignableFrom(typeof(TEntity)))
            {
                ((IAuditEntity)entity).LastModifiedTime = DateTime.UtcNow;
                ((IAuditEntity)entity).LastModifiedBy = _currentUserService.UserId;
            }
            _dbSet.Update(entity);
            return Task.CompletedTask;
        }

        public Task DeleteAsync(TEntity entity, bool softDelete = true)
        {
            if (softDelete && typeof(ISoftDelete).IsAssignableFrom(typeof(TEntity)))
            {
                ((ISoftDelete)entity).DeletedTime = DateTime.UtcNow;
                ((ISoftDelete)entity).IsDeleted = true;
                ((ISoftDelete)entity).DeletedBy = _currentUserService.UserId;
                return UpdateAsync(entity);
            }
            else
            {
                _dbSet.Remove(entity);
                return Task.CompletedTask;
            }
        }

        public IQueryable<TEntity> Query()
        {
            return _dbSet;
        }

        public async Task<PagedList<TEntity>> GetPagedAsync(int pageNumber, int pageSize, Expression<Func<TEntity, bool>>? filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null)
        {
            IQueryable<TEntity> query = _dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            var totalCount = await query.CountAsync();

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            var items = await query
            .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
            .ToListAsync();

            return new PagedList<TEntity>(items, totalCount, pageNumber, pageSize);
        }
    }
}
