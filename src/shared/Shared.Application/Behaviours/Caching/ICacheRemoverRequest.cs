namespace Shared.Application.Behaviours.Caching;

public interface ICacheRemoverRequest
{
    bool BypassCache { get; }
    string? CacheKey { get; }
    string? CacheGroupKey { get; }
}


