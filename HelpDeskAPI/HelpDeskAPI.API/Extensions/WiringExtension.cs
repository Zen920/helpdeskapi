using Asp.Versioning;
using HelpDeskAPI.Application.Interfaces;
using HelpDeskAPI.Application.Services;
using HelpDeskAPI.Infrastructure.Database;
using HelpDeskAPI.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
namespace HelpDeskAPI.Api.Extensions;
public static class WiringExtension
{
    public static void AddApplicationServices(this WebApplicationBuilder builder)
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
        builder.Services.AddApiVersioning(opt =>
        {
            opt.DefaultApiVersion = new ApiVersion(1, 0);
            opt.AssumeDefaultVersionWhenUnspecified = true;
            opt.ReportApiVersions = true;

            opt.ApiVersionReader = ApiVersionReader.Combine(
                new HeaderApiVersionReader("X-Api-Version"),
                new QueryStringApiVersionReader("api-version")
            );
        })
 .AddApiExplorer(opt =>
 {
     opt.GroupNameFormat = "'v'VVV";
     opt.SubstituteApiVersionInUrl = true;
 });

        builder.Services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new Microsoft.OpenApi.OpenApiInfo
            {
                Title = "HelpDesk API",
                Version = "1",
                Description = "HelpDesk Management API"
            });

            options.DocumentFilter<ReplaceVersionInPathDocumentFilter>();
        });
    }
}
