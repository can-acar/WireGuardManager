using System.Collections.Concurrent;
using Microsoft.EntityFrameworkCore;
using WireGuardManager.Domain.Interfaces;

namespace WireGuardManager.Infrastructure.Repositories;

public class UnitOfWork<TContext> : IUnitOfWork<TContext> where TContext : DbContext
{
    private readonly DbContext _dbContext;
    private readonly ConcurrentDictionary<Type, object> _repositories;

    public UnitOfWork(DbContext dbContext)
    {
        _dbContext = dbContext;
        _repositories = new ConcurrentDictionary<Type, object>();
    }

    public void Dispose()
    {
        _dbContext.Dispose();
    }

    public IRepository<TEntity> GetRepository<TEntity>() where TEntity : class
    {
        if (_repositories.TryGetValue(typeof(TEntity), out var repository))
        {
            return (IRepository<TEntity>) repository;
        }

        var newRepository = new Repository<TEntity>(_dbContext);
        _repositories.TryAdd(typeof(TEntity), newRepository);
        return newRepository;
    }

    public async Task SaveChangesAsync()
    {
        await _dbContext.SaveChangesAsync();
    }
}