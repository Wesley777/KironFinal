using Kiron.Application.Interfaces;
using Kiron.Infrastructure.ThirdPartyHoliday.Service;
using Kiron.Infrastructure.ThirdPartyHoliday.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Kiron.Infrastructure.ThirdPartyHoliday;

public static class DependencyInjection
{
    public static IServiceCollection AddThirdPartyHolidayServices(this IServiceCollection services, IConfiguration configuration)
    {
        var thirdPartySettings = new ThirdPartySettings();
        configuration.GetSection("ThirdPartySettings").Bind(thirdPartySettings);
        services.AddSingleton(thirdPartySettings);

        services.AddScoped<IThirdPartyHolidayService,ThirdPartyHolidayService>();

        return services;
    }
}
