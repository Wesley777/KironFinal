using Kiron.Application.Models.ThirdPartyModels;

namespace Kiron.Application.Interfaces;

public interface IBankingRepository
{
    Task SaveBankingHolidays(RootObject rootObject);
}