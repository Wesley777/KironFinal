using Kiron.Application.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Kiron.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services)
    {
        services.AddScoped<IRepository, DapperRepository>();
        services.AddScoped<IBankingRepository, BankingRepository>();
        return services;
    }
}
