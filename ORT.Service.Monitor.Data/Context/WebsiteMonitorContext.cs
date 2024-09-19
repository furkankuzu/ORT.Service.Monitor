
using Microsoft.EntityFrameworkCore;
using Service.ORT.Data.Entities;

namespace Service.ORT.Data.Context;

public class WebsiteMonitorContext : DbContext
{
    public WebsiteMonitorContext(DbContextOptions<WebsiteMonitorContext> options)
        : base(options)
    {
    }
    public DbSet<Website> Websites { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Website>().ToTable("Website");
    }
}
