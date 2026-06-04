using HelpDeskAPI.Application.Interfaces;
using HelpDeskAPI.Application.Services;
using HelpDeskAPI.Infrastructure.Database;
using HelpDeskAPI.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
namespace HelpDeskAPI.Api.Extensions;
public static class WiringExtension
{
    public static WebApplicationBuilder AddApplicationServices(this WebApplicationBuilder builder)
    {
        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

        builder.Services.AddDbContext<HelpDeskAPIDbContext>(options =>
            options.UseSqlServer(connectionString));

        builder.Services.AddScoped<IDBContext>(sp =>
            sp.GetRequiredService<HelpDeskAPIDbContext>());

        builder.Services.AddScoped<IUtenteRepository, UtenteRepository>();
        builder.Services.AddScoped<ITicketRepository, TicketRepository>();
        builder.Services.AddScoped<ICommentoRepository, CommentoRepository>();
        builder.Services.AddScoped<IAssegnazioneRepository, AssegnazioneRepository>();
        builder.Services.AddScoped<ITicketReadRepo, TicketReadRepository>();
        builder.Services.AddScoped<ITicketService, TicketService>();

        return builder;
    }
}
