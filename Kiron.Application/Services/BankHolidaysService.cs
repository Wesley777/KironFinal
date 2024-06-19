using AutoMapper;
using FluentValidation;
using Kiron.Application.Interfaces;
using Kiron.Application.Models;
using Kiron.Application.Settings;
using Kiron.Domain.Entities;
using OneOf;
using System.Data;

namespace Kiron.Application.Services;

public class BankHolidaysService : IBankHolidaysService
{
    private const string CacheRegionKey = "CacheRegionKey";
    private const string CacheHolidayRegionKey = "CacheHolidayRegionKey";
    private readonly IRepository _repository;
    private readonly IBankingRepository _bankingRepository;
    private readonly IThirdPartyHolidayService _thirdPartyHolidayService;
    private readonly IValidator<RegionRequest> _validator;
    private readonly ICacheService _cacheService;
    private readonly IMapper _mapper;
    private readonly AppSettings _appSettings;

    public BankHolidaysService(IRepository repository,
                               IBankingRepository bankingRepository,
                               IThirdPartyHolidayService thirdPartyHolidayService,
                               IValidator<RegionRequest> validator,
                               ICacheService cacheService,
                               IMapper mapper,
                               AppSettings appSettings)
    {
        _repository = repository;
        _bankingRepository = bankingRepository;
        _thirdPartyHolidayService = thirdPartyHolidayService;
        _validator = validator;
        _cacheService = cacheService;
        _mapper = mapper;
        _appSettings = appSettings;
    }

    public async Task SaveBankingHolidays()
    {
        var bankingHolidays = await _thirdPartyHolidayService.GetBankHolidays();

        await _bankingRepository.SaveBankingHolidays(bankingHolidays);
    }

    public async Task<List<RegionDto>> GetRegions()
    {

        var cachedResult = _cacheService.Get<IEnumerable<Region>>(CacheRegionKey);

        if (cachedResult != null)
        {
            return cachedResult.Select(item => _mapper.Map<RegionDto>(item)).ToList();
        }

        var result = await _repository.ExecuteStoredProcedureManyAsync<Region>("GetAllRegions");

        _cacheService.Set(CacheRegionKey, result, TimeSpan.FromMinutes(_appSettings.ThirdPartyCacheDurationMin));

        return result.Select(item => _mapper.Map<RegionDto>(item)).ToList();
    }

    public async Task<OneOf<List<HolidayDto>,SuccessFailDto>> GetHolidaysByRegionId(int regionId)
    {
        var validationResult = await _validator.ValidateAsync(
          new RegionRequest
          {
             RegionId = regionId
          });

        if (!validationResult.IsValid)
        {
            var errors = string.Join(Environment.NewLine, validationResult.Errors.Select(e => e.ErrorMessage));

            return new SuccessFailDto
            {
                IsSuccess = false,
                Messsage = errors
            };
        }
        var regionIdCacheKey = $"{CacheHolidayRegionKey}_{regionId}";

        var cachedResult = _cacheService.Get<IEnumerable<Holiday>>(regionIdCacheKey);

        if (cachedResult != null)
        {
            return cachedResult.Select(item => _mapper.Map<HolidayDto>(item)).ToList();
        }

        var result = await _repository.ExecuteStoredProcedureManyAsync<Holiday>("GetBankHolidaysByRegion", new {RegionId = regionId});

        _cacheService.Set(regionIdCacheKey, result, TimeSpan.FromMinutes(_appSettings.ThirdPartyCacheDurationMin));

        return result.Select(item => _mapper.Map<HolidayDto>(item)).ToList();
    }

}
