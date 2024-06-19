using Kiron.Application.Models;
using OneOf;

namespace Kiron.Application.Services;

public interface IBankHolidaysService
{
    Task<OneOf<List<HolidayDto>, SuccessFailDto>> GetHolidaysByRegionId(int regionId);
    Task<List<RegionDto>> GetRegions();
    Task SaveBankingHolidays();
}