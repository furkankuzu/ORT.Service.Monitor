using Flurl.Http;
using Microsoft.Extensions.Logging;
using Quartz;
using Service.ORT.Api.Services;
using Service.ORT.Data.Entities;

public class WebsiteMonitorJob : IJob
{
    //private readonly IEmailService _emailService;
    private readonly IWebsiteMonitorService _websiteMonitorService;
    private readonly ILogger<WebsiteMonitorJob> _logger;

    public WebsiteMonitorJob(
                             IWebsiteMonitorService websiteMonitorService,
                             ILogger<WebsiteMonitorJob> logger
        //IEmailService emailService,
        )
    {
        _websiteMonitorService = websiteMonitorService;
        _logger = logger;
        //_emailService = emailService;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        var website = (Website)context.MergedJobDataMap["monitor"]; // Retrieve the monitor object
        Console.WriteLine($"Checking website: {website.WebsiteUrl}");
        var isWebsiteUp = await CheckWebsiteAsync(website.WebsiteUrl);

        if (!isWebsiteUp)
        {
            _logger.LogWarning($"website is down, website url: {website.WebsiteUrl}");
        }
        else
        {
            _logger.LogWarning($"website is up, website url: {website.WebsiteUrl}");
        }
    }

    private async Task<bool> CheckWebsiteAsync(string url)
    {
        try
        {
            var response = await url
                .GetAsync();

            return response.StatusCode >= 200 && response.StatusCode <= 299;
        }
        catch (FlurlHttpException e)
        {
            _logger.LogWarning($"FlurlHttpException when requesting to website: {e.Message}");
            return false;
        }
        catch (Exception e)
        {
            _logger.LogWarning($"Exception when requesting to website: {e.Message}");
            return false;
        }
    }
}
