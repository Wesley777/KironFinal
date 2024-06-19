using Kiron.Application.Interfaces;
using Kiron.Application.Models.ThirdPartyModels;
using Kiron.Infrastructure.ThirdPartyHoliday.Settings;
using Newtonsoft.Json;

namespace Kiron.Infrastructure.ThirdPartyHoliday.Service;

public class ThirdPartyHolidayService : IThirdPartyHolidayService
{
    private readonly ThirdPartySettings _thirdPartySettings;

    public ThirdPartyHolidayService(ThirdPartySettings thirdPartySettings)
    {
        _thirdPartySettings = thirdPartySettings;
    }

    public async Task<RootObject> GetBankHolidays()
    {
        using HttpClient client = new HttpClient();

        HttpResponseMessage response = await client.GetAsync(_thirdPartySettings.Url);
        response.EnsureSuccessStatusCode();

        string json = await response.Content.ReadAsStringAsync();

        return JsonConvert.DeserializeObject<RootObject>(json);
    }
}
