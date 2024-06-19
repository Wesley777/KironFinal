using Kiron.Application.Models;
using Kiron.Application.Services;
using Kiron.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Kiron.Presentation.Controllers;

[ApiController]
public class NavigationItemController : Controller
{
    private readonly INavigationService _navigationService;

    public NavigationItemController(INavigationService navigationService)
    {
        _navigationService = navigationService;
    }

    [Authorize]
    [HttpGet("getnavigationitems")]
    public async Task<ActionResult<IEnumerable<NavigationItem>>> GetNavigationItems()
    {
        return Ok(await _navigationService.GetNavigationItems());
    }
}
