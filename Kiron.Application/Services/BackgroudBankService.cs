using Kiron.Application.Interfaces;
using Kiron.Application.Settings;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Kiron.Application.Services;

internal class BackgroudBankService : BackgroundService
{
    private readonly ILogger<BackgroudBankService> _logger;
    private readonly IServiceProvider _serviceProvider;
    private readonly ThirdPartySettings _thirdPartySettings;


    public BackgroudBankService(ILogger<BackgroudBankService> logger,
                                IServiceProvider serviceProvider,
                                ThirdPartySettings thirdPartySettings)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
        _thirdPartySettings = thirdPartySettings;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            _logger.LogInformation("Background holiday update service is running at: {time}", DateTimeOffset.Now);

            using (var scope = _serviceProvider.CreateScope())
            {
                var thirdPartyService = scope.ServiceProvider.GetRequiredService<IThirdPartyHolidayService>();

                var bankingHolidays = await thirdPartyService.GetBankHolidays();

                var bankingRepository = scope.ServiceProvider.GetRequiredService<IBankingRepository>();

                await bankingRepository.SaveBankingHolidays(bankingHolidays);

            }

            await Task.Delay(TimeSpan.FromHours(_thirdPartySettings.HostedServicesDurationHourDelay), stoppingToken); 
        }
    }

}
