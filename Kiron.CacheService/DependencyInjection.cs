using Kiron.Application.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Kiron.CacheService;

public static class DependencyInjection
{
    public static IServiceCollection AddCacheServices(this IServiceCollection services)
    {
        services.AddSingleton<ICacheService, MemoryCacheService>();
        return services;
    }
}
