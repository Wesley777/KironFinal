using AutoMapper;
using Kiron.Application.Interfaces;
using Kiron.Application.Models;
using Kiron.Application.Settings;
using Kiron.Domain.Entities;
using System.Data;

namespace Kiron.Application.Services;
public class NavigationService : INavigationService
{
    private const int ParentId = -1;
    private const string CacheNavigationItemsKey = $"GetNavigationItems";
    private readonly IRepository _repository;
    private readonly ICacheService _cacheService;
    private readonly IMapper _mapper;
    private readonly AppSettings _appSettings;
    public NavigationService(IRepository repository,
                             ICacheService cacheService,
                             IMapper mapper,
                             AppSettings appSettings)
    {
        _repository = repository;
        _cacheService = cacheService;
        _mapper = mapper;
        _appSettings = appSettings;
    }

    public async Task<IEnumerable<NavigationItemDto>> GetNavigationItems()
    {

        var cachedResult = _cacheService.Get<IEnumerable<NavigationItem>>(CacheNavigationItemsKey);

        if (cachedResult != null)
        {
            var formatedItems = await FormatNavigationItems(cachedResult.ToList(), ParentId);

            return formatedItems.Select(item => _mapper.Map<NavigationItemDto>(item)).ToList();
        }

        var allItems = await _repository.ExecuteStoredProcedureManyAsync<NavigationItem>("GetAllNavigationItems");

        _cacheService.Set(CacheNavigationItemsKey, allItems, TimeSpan.FromMinutes(_appSettings.NavigationCacheDurationMin));

       var result = await FormatNavigationItems(allItems.ToList(), ParentId);

       return result.Select(item => _mapper.Map<NavigationItemDto>(item)).ToList();
    }

    private async Task<List<NavigationItem>> FormatNavigationItems(List<NavigationItem> allItems, int parentId)
    {
        var formattedItems = new List<NavigationItem>();

        var children = allItems.Where(item => item.ParentID == parentId).ToList();

        foreach (var child in children)
        {
            formattedItems.Add(new NavigationItem
            {
                ID = child.ID,
                Children = await FormatNavigationItems(allItems, child.ID),
                ParentID = parentId , 
                Text = child.Text,
            });
        }

        return formattedItems;
    }
}
