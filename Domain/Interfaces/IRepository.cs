using System.Linq.Expressions;
using Domain.Entities.Common;
using Domain.Interfaces.Queries;

namespace Domain.Interfaces;

public interface IRepository<TEntity> where TEntity : BaseEntity
{
    Task<Guid> AddAsync(TEntity entity);
    Task<ICollection<TEntity>> GetAsync(
        Expression<Func<TEntity, bool>>? predicate = null,
        params Expression<Func<TEntity, BaseEntity?>>[]? includes); 
    Task<TEntity?> FirstAsync(Expression<Func<TEntity, bool>> predicate,
        params Expression<Func<TEntity, BaseEntity?>>[]? includes);
    void Update(TEntity entity);
    void Delete(TEntity entity);
    IQueryable<TEntity> GetQuery();
}