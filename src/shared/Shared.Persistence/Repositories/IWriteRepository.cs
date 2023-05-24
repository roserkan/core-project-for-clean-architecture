using Shared.Persistence.Base;

namespace Shared.Persistence.Repositories;

public interface IWriteRepository<TEntity> : IQuery<TEntity>
    where TEntity : class, IAggregateRoot, new()
{
    Task AddAsync(TEntity entity);

    Task AddRangeAsync(IList<TEntity> entity);

    Task UpdateAsync(TEntity entity);

    Task UpdateRangeAsync(IList<TEntity> entity);

    Task DeleteAsync(TEntity entity);

    Task DeleteRangeAsync(IList<TEntity> entity);
}

