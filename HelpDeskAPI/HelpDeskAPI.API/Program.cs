using HelpDeskAPI.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.AddApplicationServices();
builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.Run();
