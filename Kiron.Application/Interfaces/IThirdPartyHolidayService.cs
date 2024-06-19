
using Kiron.Application.Models.ThirdPartyModels;

namespace Kiron.Application.Interfaces;

public interface IThirdPartyHolidayService
{
    Task<RootObject> GetBankHolidays();
}