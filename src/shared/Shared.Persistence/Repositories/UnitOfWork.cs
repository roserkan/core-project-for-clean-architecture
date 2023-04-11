using Microsoft.EntityFrameworkCore;

namespace Shared.Persistence.Repositories;

public class UnitOfWork<TContext> : IUnitOfWork
    where TContext : DbContext
{
    private readonly TContext _dbContext;
    private bool disposed = false;

    public UnitOfWork(TContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
    {
        return await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!disposed)
        {
            if (disposing)
            {
                _dbContext.Dispose();
            }
        }
        disposed = true;
    }
}