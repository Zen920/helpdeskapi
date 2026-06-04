using Asp.Versioning;
using HelpDeskAPI.Api.Extensions;
using HelpDeskAPI.Application.Interfaces;
using HelpDeskAPI.Core.DTOs;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.AddApplicationServices();
builder.Services.AddOpenApi();

var app = builder.Build();
var versionSet = app.NewApiVersionSet()
    .HasApiVersion(new ApiVersion(1, 0))
    .ReportApiVersions()
    .Build();

var debugGroup = app.MapGroup("api/v{version:apiVersion}/debug")
    .WithApiVersionSet(versionSet);

var ticketsGroup = app.MapGroup("api/v{version:apiVersion}/tickets")
    .WithApiVersionSet(versionSet);

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

debugGroup.MapGet("/health", () => Results.Ok());

// --- Ticket Endpoints ---

ticketsGroup.MapGet("", async (ITicketService service) =>
{
    var tickets = await service.GetActiveTicketsAsync();
    return Results.Ok(tickets);
});

ticketsGroup.MapGet("/{ticketId:int}", async (int ticketId, ITicketService service) =>
{
    var ticket = await service.GetTicketByIdAsync(ticketId);
    return ticket is not null ? Results.Ok(ticket) : Results.NotFound();
});

ticketsGroup.MapPost("", async ([FromBody] CreateTicketRequest request, ITicketService service) =>
{

    var id = await service.CreateTicketAsync(request);
    return Results.Created($"/api/v1/tickets/{id}", id);
});

ticketsGroup.MapPatch("/{ticketId:int}/status", async ([FromBody] UpdateTicketRequest request, ITicketService service) =>
{
    if (request.TicketId < 1) throw new Exception("Id cannot be lower than 1.");

    await service.UpdateTicketStatusAsync(request);
    return Results.NoContent();
});

ticketsGroup.MapPost("/{ticketId:int}/comments", async ([FromBody] AddCommentRequest request, ITicketService service) =>
{
    if (request.TicketId < 1) throw new Exception("Id cannot be lower than 1.");
    await service.AddCommentToTicketAsync(request);
    return Results.Created();
});

ticketsGroup.MapGet("/{ticketId:int}/comments", async (int ticketId, ITicketService service) =>
{
    if (ticketId < 1) throw new Exception("Id cannot be lower than 1.");
    var comments = await service.GetCommentsOfTicket(ticketId);
    return Results.Ok(comments);
});

ticketsGroup.MapPost("/{ticketId:int}/assign", async ([FromBody] AssignTicketToUserRequest request, ITicketService service) =>
{
    if (request.TicketId < 1) throw new Exception("Id cannot be lower than 1.");

    await service.AssignTicketToUserAsync(request);
    return Results.Ok();
});

ticketsGroup.MapPost("/{ticketId:int}/reopen", async (int ticketId, ITicketService service) =>
{
    if (ticketId < 1) throw new Exception("Id cannot be lower than 1.");
    await service.ReopenTicketAsync(ticketId);
    return Results.Ok();
});

app.UseHttpsRedirection();

app.Run();
