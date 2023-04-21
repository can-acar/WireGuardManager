using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using WireGuardManager.Domain.Interfaces;

namespace WireGuardManager.Infrastructure.Repositories;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
{
    private readonly DbContext _dbContext;
    private readonly DbSet<TEntity?> _dbSet;

    public Repository(DbContext dbContext)
    {
        _dbContext = dbContext;
        _dbSet = _dbContext.Set<TEntity>();
    }

    public async Task<TEntity> GetByIdAsync(Guid id)
    {
        return await _dbSet.FindAsync(id);
    }

    public async Task<IList<TEntity>> GetAllAsync()
    {
        return await _dbSet.ToListAsync();
    }

    public async Task<IList<TEntity>> GetAsync(Expression<Func<TEntity?, bool>> condition)
    {
        return await _dbSet.Where(condition).ToListAsync();
    }

    public async Task<TEntity?> GetFirstOrDefaultAsync(Expression<Func<TEntity?, bool>> condition)
    {
        return await _dbSet.FirstOrDefaultAsync(condition);
    }

    public async Task<HashSet<TEntity>> GetAllAsync(Expression<Func<TEntity?, bool>> condition,
        Expression<Func<TEntity, object>> orderBy,
        Expression<Func<TEntity, object>>? thenBy = null, bool isDescending = false)
    {
        var query = _dbSet.Where(condition);

        if (isDescending)
            query = thenBy != null
                ? query.OrderByDescending(orderBy).ThenByDescending(thenBy)
                : query.OrderByDescending(orderBy);
        else
            query = thenBy != null
                ? query.OrderBy(orderBy).ThenBy(thenBy)
                : query.OrderBy(orderBy);

        return new HashSet<TEntity>(await query.ToListAsync());
    }

    public async Task<TEntity?> AddAsync(TEntity? entity)
    {
        var result = await _dbSet.AddAsync(entity);
        await _dbContext.SaveChangesAsync();
        return result.Entity;
    }

    public async Task UpdateAsync(TEntity? entity)
    {
        _dbSet.Update(entity);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(TEntity? entity)
    {
        await UpdateAsync(entity);
    }

    public async Task DeleteAsync(Expression<Func<TEntity?, bool>> condition)
    {
        var entity = await GetFirstOrDefaultAsync(condition);
        await DeleteAsync(entity);
    }

    public async Task UpdateAsync(Expression<Func<TEntity?, bool>> condition, TEntity? entity)
    {
        var existingEntity = await GetFirstOrDefaultAsync(condition);
        if (existingEntity == null) return;


        _dbSet.Update(entity);

        await _dbContext.SaveChangesAsync();
    }

    public void Dispose()
    {
        _dbContext.Dispose();
    }
}