using HelpDeskAPI.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace HelpDeskAPI.Infrastructure.Database;
public class HelpDeskAPIDbContextFactory : IDesignTimeDbContextFactory<HelpDeskAPIDbContext>
{
    public HelpDeskAPIDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<HelpDeskAPIDbContext>();

        // Hardcode your connection string here JUST for design-time migrations,
        // or configure it to read from your appsettings.json
        optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=TicketingDb;Trusted_Connection=True;MultipleActiveResultSets=true");

        return new HelpDeskAPIDbContext(optionsBuilder.Options);
    }
}