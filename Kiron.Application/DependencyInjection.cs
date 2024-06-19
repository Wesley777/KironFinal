using FluentValidation;
using Kiron.Application.Automapper;
using Kiron.Application.Models;
using Kiron.Application.Services;
using Kiron.Application.Settings;
using Kiron.Application.Validation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Kiron.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services,IConfiguration configuration)
    {
        var appSettings = new AppSettings();
        configuration.GetSection("AppSettings").Bind(appSettings);
        services.AddSingleton(appSettings);

        var thirdPartySettings = new ThirdPartySettings();
        configuration.GetSection("ThirdPartySettings").Bind(thirdPartySettings);
        services.AddSingleton(thirdPartySettings);

        services.AddScoped<IUserService, UserService>();
        services.AddScoped<INavigationService, NavigationService>();
        services.AddScoped<IBankHolidaysService, BankHolidaysService>();
        services.AddScoped<IValidator<UserCredentials>, UserCredentialsValidator>();
        services.AddScoped<IValidator<RegionRequest>, RegionValidator>();
        services.AddAutoMapper(typeof(NavigationItemProfile));
        services.AddAutoMapper(typeof(RegionProfile));
        services.AddAutoMapper(typeof(HolidayProfile));

        services.AddHostedService<BackgroudBankService>();

        return services;
    }
}
