using Kiron.Application.Interfaces;
using Kiron.Infrastructure.Managers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Kiron.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddRepositoryServices(this IServiceCollection services,IConfiguration configuration)
    {

        services.AddScoped<IDBConnectionManager>(provider => new DBConnectionManager(configuration.GetConnectionString("DefaultConnection")));

        return services;
    }
}
