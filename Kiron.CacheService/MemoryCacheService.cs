using Kiron.Application.Interfaces;
using Microsoft.Extensions.Caching.Memory;

namespace Kiron.CacheService;

public class MemoryCacheService : ICacheService
{
    private readonly IMemoryCache _memoryCache;

    public MemoryCacheService()
    {
        _memoryCache = new MemoryCache(new MemoryCacheOptions());
    }

    public T Get<T>(string key)
    {
        return _memoryCache.TryGetValue(key, out T value) ? value : default(T);
    }

    public void Set<T>(string key, T value, TimeSpan expiration)
    {
        var cacheEntryOptions = new MemoryCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = expiration
        };
        _memoryCache.Set(key, value, cacheEntryOptions);
    }

    public void Remove(string key)
    {
        _memoryCache.Remove(key);
    }
}