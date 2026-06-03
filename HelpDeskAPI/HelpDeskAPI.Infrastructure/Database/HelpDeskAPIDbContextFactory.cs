using HelpDeskAPI.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace HelpDeskAPI.Infrastructure.Database;
public class HelpDeskAPIDbContextFactory : IDesignTimeDbContextFactory<HelpDeskAPIDbContext>
{
    public HelpDeskAPIDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<HelpDeskAPIDbContext>();
        optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=HelpDeskAPIDB;Trusted_Connection=True;MultipleActiveResultSets=true");

        return new HelpDeskAPIDbContext(optionsBuilder.Options);
    }
}