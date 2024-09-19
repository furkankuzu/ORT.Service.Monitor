using Microsoft.AspNetCore.Mvc;
using Service.ORT.Api.Models;
using Service.ORT.Api.Services;

[ApiController]
[Route("api/[controller]")]
public class MonitorController : ControllerBase
{
    private readonly IWebsiteMonitorService _websiteMonitorService;

    public MonitorController(IWebsiteMonitorService websiteMonitorService)
    {
        _websiteMonitorService = websiteMonitorService;
    }

    [HttpPost("add")]
    public async Task<IActionResult> AddWebsite([FromBody] AddWebsiteRequest request)
    {
        await _websiteMonitorService.AddWebsite(request.Url, request.Email, request.PeriodInMinutes);

        return Ok("Website added for monitoring.");
    }
}

