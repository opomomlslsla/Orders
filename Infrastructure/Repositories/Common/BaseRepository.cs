using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Domain.Entities.Common;
using Domain.Interfaces;

namespace Infrastructure.Repositories.Common;

public abstract class BaseRepository<TEntity>(Context context) : IRepository<TEntity> where TEntity : BaseEntity
{
    public virtual async Task<Guid> AddAsync(TEntity entity)
    {
        var entityEntry = await context.Set<TEntity>().AddAsync(entity);
        return entityEntry.Entity.Id;
    }
    public virtual async Task<ICollection<TEntity>> GetAsync(
        Expression<Func<TEntity, bool>>? predicate = null,
        params Expression<Func<TEntity, BaseEntity?>>[]? includes)
    {
        var  query = context.Set<TEntity>().AsQueryable();
        if(includes != null)
            query = includes.Aggregate(query, (current, inc) => current.Include(inc));
        if (predicate != null)
            return await query.Where(predicate).ToListAsync();
        return await query.ToListAsync();
    }
    public virtual async Task<TEntity?> FirstAsync(Expression<Func<TEntity, bool>> predicate,
        params Expression<Func<TEntity, BaseEntity?>>[]? includes)
    {
        var  query = context.Set<TEntity>().AsQueryable();
        if(includes != null)
            query = includes.Aggregate(query, (current, inc) => current.Include(inc));
        return await query.FirstOrDefaultAsync(predicate);
    }
    public virtual void Update(TEntity entity)
    {
        context.Set<TEntity>().Update(entity);
    }
    public virtual void Delete(TEntity entity)
    {
        context.Set<TEntity>().Remove(entity);
    }

    public IQueryable<TEntity> GetQuery()
    {
        return context.Set<TEntity>();
    }
}