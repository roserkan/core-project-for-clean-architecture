namespace Shared.Persistence.Repositories;

public interface IQuery<T>
{
    IQueryable<T> Query();
}
