using Asp.Versioning;
using HelpDeskAPI.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.AddApplicationServices();
builder.Services.AddOpenApi();

var app = builder.Build();
var versionSet = app.NewApiVersionSet()
    .HasApiVersion(new ApiVersion(1, 0))
    .ReportApiVersions()
    .Build();
var versionedGroup = app.MapGroup("api/v{version:apiVersion}/debug")
    .WithApiVersionSet(versionSet);
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}
versionedGroup.MapGet("/health", () => { Results.Ok(); });
app.UseHttpsRedirection();

app.Run();
