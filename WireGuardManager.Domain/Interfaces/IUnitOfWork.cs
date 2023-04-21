using System;
using System.Threading.Tasks;

namespace WireGuardManager.Domain.Interfaces;

public interface IUnitOfWork<TContext> : IDisposable
{
    IRepository<TEntity> GetRepository<TEntity>() where TEntity : class;
    Task SaveChangesAsync();
}