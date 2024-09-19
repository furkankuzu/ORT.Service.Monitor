using Microsoft.EntityFrameworkCore;
using Quartz;
using Quartz.Impl.Matchers;
using Service.ORT.Data.Context;
using Service.ORT.Data.Entities;

namespace Service.ORT.Api.Services;
public interface IWebsiteMonitorService
{
    Task AddWebsite(string url, string userEmail, int periodInMinutes);
    List<Website> GetWebsites();
    Task ScheduleNewJob(Website newWebsite);
}

public class WebsiteMonitorService : IWebsiteMonitorService
{
    private readonly WebsiteMonitorContext _context;
    private readonly ISchedulerFactory _schedulerFactory;
    public WebsiteMonitorService(WebsiteMonitorContext context, ISchedulerFactory schedulerFactory)
    {
        _context = context;
        _schedulerFactory = schedulerFactory;
    }

    public async Task AddWebsite(string url, string userEmail, int periodInMinutes)
    {
        var website = new Website
        {
            WebsiteUrl = url,
            IsUp = true,
            UserEmail = userEmail,
            Period = periodInMinutes
        };
        await _context.Websites.AddAsync(website);
        await _context.SaveChangesAsync();
        await ScheduleNewJob(website);
    }

    public List<Website> GetWebsites()
    {
        return _context.Websites.AsNoTracking().ToList();
    }

    public async Task ScheduleNewJob(Website website)
    {
        // Get the Quartz scheduler
        var scheduler = await _schedulerFactory.GetScheduler();

        // Create the job
        var jobKey = new JobKey($"MonitorJob-{website.Id}");
        var job = JobBuilder.Create<WebsiteMonitorJob>()
            .WithIdentity(jobKey)
            .Build();

        // Create the trigger for the job
        var trigger = TriggerBuilder.Create()
            .WithIdentity($"MonitorJobTrigger-{website.Id}")
            .StartNow()
            .WithSimpleSchedule(x => x
                .WithIntervalInSeconds(website.Period)
                .RepeatForever())
            .UsingJobData(new JobDataMap { { "monitor", website } })
            .Build();

        // Schedule the job with the scheduler
        await scheduler.ScheduleJob(job, trigger);
        Console.WriteLine($"Scheduled job for website {website.WebsiteUrl} with interval {website.Period} seconds");

        var jobs = await scheduler.GetJobKeys(GroupMatcher<JobKey>.AnyGroup());

        Console.WriteLine("Scheduled jobs:");
        foreach (var jKey in jobs)
        {
            Console.WriteLine($"Job: {jobKey.Name}");
        }
    }
}

