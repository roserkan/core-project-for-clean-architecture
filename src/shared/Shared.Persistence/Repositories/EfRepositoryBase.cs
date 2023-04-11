using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Shared.Persistence.Base;
using Shared.Persistence.Paging;
using Shared.Persistence.Dynamic;

namespace Shared.Persistence.Repositories;

public class EfRepositoryBase<TEntity, TEntityId, TContext> : IReadRepository<TEntity, TEntityId>, IWriteRepository<TEntity, TEntityId>
    where TEntity : class, IAggregateRoot, new()
    where TContext : DbContext
{
    protected readonly TContext Context;

    public EfRepositoryBase(TContext context)
    {
        Context = context;
    }

    public IQueryable<TEntity> Query() => Context.Set<TEntity>();


    #region Add
    public async Task<TEntity> AddAsync(TEntity entity)
    {
        await Context.AddAsync(entity);
        return entity;
    }
    public async Task<IList<TEntity>> AddRangeAsync(IList<TEntity> entities)
    {
        await Context.AddRangeAsync(entities);
        return entities;
    }
    #endregion

    #region Update
    public async Task<TEntity> UpdateAsync(TEntity entity)
    {
        await Task.Run(() => Context.Update(entity));
        return entity;
    }

    public async Task<IList<TEntity>> UpdateRangeAsync(IList<TEntity> entities)
    {
        await Task.Run(() => Context.UpdateRange(entities));
        return entities;
    }
    #endregion

    #region Delete
    public async Task<TEntity> DeleteAsync(TEntity entity)
    {
        await Task.Run(() => Context.Remove(entity));
        return entity;
    }

    public async Task<IList<TEntity>> DeleteRangeAsync(IList<TEntity> entities)
    {
        await Task.Run(() => Context.RemoveRange(entities));
        return entities;
    }
    #endregion


    #region GetList
    public async Task<IPaginate<TEntity>> GetListAsync(
        Expression<Func<TEntity, bool>>? predicate = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
        int index = 0,
        int size = 10,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        IQueryable<TEntity> queryable = Query();
        if (!enableTracking)
            queryable = queryable.AsNoTracking();
        if (include != null)
            queryable = include(queryable);
        if (predicate != null)
            queryable = queryable.Where(predicate);
        if (orderBy != null)
            return await orderBy(queryable).ToPaginateAsync(index, size, from: 0, cancellationToken);
        return await queryable.ToPaginateAsync(index, size, from: 0, cancellationToken);
    }

    public async Task<TEntity?> GetAsync(
        Expression<Func<TEntity, bool>> predicate,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        IQueryable<TEntity> queryable = Query();
        if (!enableTracking)
            queryable = queryable.AsNoTracking();
        if (include != null)
            queryable = include(queryable);
        return await queryable.FirstOrDefaultAsync(predicate, cancellationToken);
    }
    #endregion


    #region Dynamic
    public async Task<IPaginate<TEntity>> GetListByDynamicAsync(
        DynamicQuery dynamic,
        Expression<Func<TEntity, bool>>? predicate = null,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
        int index = 0,
        int size = 10,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        IQueryable<TEntity> queryable = Query().ToDynamic(dynamic);
        if (!enableTracking)
            queryable = queryable.AsNoTracking();
        if (include != null)
            queryable = include(queryable);
        if (predicate != null)
            queryable = queryable.Where(predicate);
        return await queryable.ToPaginateAsync(index, size, from: 0, cancellationToken);
    }
    #endregion



    public async Task<bool> AnyAsync(
        Expression<Func<TEntity, bool>>? predicate = null,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        IQueryable<TEntity> queryable = Query();
        if (predicate is not null)
            queryable = queryable.Where(predicate);
        if (!enableTracking)
            queryable = queryable.AsNoTracking();
        return await queryable.AnyAsync(cancellationToken);
    }
}
