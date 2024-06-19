using Kiron.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Kiron.Presentation.Controllers;

[ApiController]
[Authorize]
public class HolidayEventsController : Controller
{
    private readonly IBankHolidaysService _bankHolidaysService;

    public HolidayEventsController(IBankHolidaysService bankHolidaysService)
    {
        _bankHolidaysService = bankHolidaysService;
    }

    [HttpGet("regions")]
    public async Task<IActionResult> GetRegions()
    {
        return Ok(await _bankHolidaysService.GetRegions());
    }

    [HttpGet("getholidaybyregionid")]
    public async Task<IActionResult> GetHolidayByRegionId(int regionId)
    {
        var result = await _bankHolidaysService.GetHolidaysByRegionId(regionId);

        return result.Match<IActionResult>(
           holiday => Ok(result.Value),
           error => NotFound(new { message = error.Messsage })
       );

    }
}
