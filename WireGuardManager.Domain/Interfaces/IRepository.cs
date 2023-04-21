using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace WireGuardManager.Domain.Interfaces;

public interface IRepository<TEntity> : IDisposable where TEntity : class
{
    Task<TEntity> GetByIdAsync(Guid id);
    Task<IList<TEntity>> GetAllAsync();
    Task<IList<TEntity>> GetAsync(Expression<Func<TEntity?, bool>> condition);
    Task<TEntity?> GetFirstOrDefaultAsync(Expression<Func<TEntity?, bool>> condition);

    Task<HashSet<TEntity>> GetAllAsync(Expression<Func<TEntity?, bool>> condition,
        Expression<Func<TEntity, object>> orderBy,
        Expression<Func<TEntity, object>>? thenBy = null, bool isDescending = false);

    Task<TEntity> AddAsync(TEntity entity);
    Task UpdateAsync(TEntity entity);
    Task DeleteAsync(TEntity entity);
    Task DeleteAsync(Expression<Func<TEntity, bool>> condition);
    Task UpdateAsync(Expression<Func<TEntity, bool>> condition, TEntity entity);
}