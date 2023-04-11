using Shared.Persistence.Base;

namespace Shared.Persistence.Repositories;

public interface IWriteRepository<TEntity, TEntityId> : IQuery<TEntity>
    where TEntity : BaseEntity<TEntityId>
{
    Task<TEntity> AddAsync(TEntity entity);

    Task<IList<TEntity>> AddRangeAsync(IList<TEntity> entity);

    Task<TEntity> UpdateAsync(TEntity entity);

    Task<IList<TEntity>> UpdateRangeAsync(IList<TEntity> entity);

    Task<TEntity> DeleteAsync(TEntity entity);

    Task<IList<TEntity>> DeleteRangeAsync(IList<TEntity> entity);
}

