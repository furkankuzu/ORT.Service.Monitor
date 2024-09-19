using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.ORT.Data.Context
{
    public class WebsiteMonitorDbContextFactory : IDesignTimeDbContextFactory<WebsiteMonitorContext>
    {
        public WebsiteMonitorContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<WebsiteMonitorContext>();
            optionsBuilder.UseSqlServer("Server=localhost,1433;Database=WebsiteMonitor;User Id=sa;Password=Friday01!;TrustServerCertificate=True;");

            return new WebsiteMonitorContext(optionsBuilder.Options);
        }
    }
}
