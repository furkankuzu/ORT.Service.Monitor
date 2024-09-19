using Microsoft.EntityFrameworkCore;
using Quartz;
using Service.ORT.Api.Services;
using Service.ORT.Data.Context;

namespace Service.ORT.Api.Extensions;

public static class ServiceCollectionExtensions
{
    public static void ConfigureServices(this IServiceCollection services, IConfiguration configuration)
    {


        services.AddQuartz(options =>
        {
            options.UseMicrosoftDependencyInjectionJobFactory();
            using (var scope = services.BuildServiceProvider().CreateScope())
            {

                var context = scope.ServiceProvider.GetService<WebsiteMonitorContext>()
                    ?? throw new ArgumentNullException(nameof(WebsiteMonitorContext));

                var websites = context.Websites.AsNoTracking().AsEnumerable().ToList();
                foreach (var website in websites)
                {
                    var jobKey = new JobKey($"MonitorJob-{website.Id}");
                    options.AddJob<WebsiteMonitorJob>(opts => opts.WithIdentity(jobKey));

                    options.AddTrigger(opts => opts
                        .ForJob(jobKey)
                        .WithIdentity($"MonitorJobTrigger-{website.Id}")
                        .StartNow()
                        .WithSimpleSchedule(scheduleBuilder => scheduleBuilder
                            .WithIntervalInSeconds(website.Period)
                            .RepeatForever())
                        .UsingJobData(new JobDataMap { { "monitor", website } }));
                }
            }
        });

        services.AddQuartzHostedService(options => options.WaitForJobsToComplete = true);
        services.AddScoped<IWebsiteMonitorService, WebsiteMonitorService>();
    }
    public static async Task ConfigureDatabase(this IServiceCollection services, IConfiguration configuration)
    {

        services.AddDbContext<WebsiteMonitorContext>(
            options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        using (var scope = services.BuildServiceProvider().CreateScope())
        {

            var context = scope.ServiceProvider.GetRequiredService<WebsiteMonitorContext>();

            await context.Database.EnsureDeletedAsync();
            await context.Database.MigrateAsync();
        }
    }
}
