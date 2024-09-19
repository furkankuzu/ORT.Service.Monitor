using Microsoft.EntityFrameworkCore;
using Service.ORT.Data.Context;
using Service.ORT.Data.Entities;

namespace Service.ORT.Data.Repositories;

public interface IWebsiteRepository
{
    void AddWebsiteMonitor(Website website);
    List<Website> GetWebsites();
}
public class WebsiteRepository : IWebsiteRepository
{
    private readonly WebsiteMonitorContext _context;
    public WebsiteRepository(WebsiteMonitorContext context)
    {
        _context = context;
    }

    public void AddWebsiteMonitor(Website website)
    {
        _context.Websites.AddAsync(website);
    }

    public List<Website> GetWebsites()
    {
        return _context.Websites.AsNoTracking().ToList();
    }
}
