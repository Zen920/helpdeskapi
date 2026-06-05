using Asp.Versioning;
using HelpDeskAPI.Api.Auth;
using HelpDeskAPI.Api.Extensions;
using HelpDeskAPI.Api.Services;
using HelpDeskAPI.Application.Interfaces;
using HelpDeskAPI.Core.DTOs;
using HelpDeskAPI.Core.Models;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.AddApplicationServices();

var app = builder.Build();
var versionSet = app.NewApiVersionSet()
    .HasApiVersion(new ApiVersion(1, 0))
    .ReportApiVersions()
    .Build();

var debugGroup = app.MapGroup("api/v{version:apiVersion}/debug")
    .WithApiVersionSet(versionSet);
var authGroup = app.MapGroup("api/v{version:apiVersion}/auth")
    .WithApiVersionSet(versionSet);

var ticketsGroup = app.MapGroup("api/v{version:apiVersion}/tickets")
    .WithApiVersionSet(versionSet);

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

debugGroup.MapGet("/health", () => Results.Ok()).WithDescription("Test if the API is up.");
// --- Auth Endpoints ---
authGroup.MapPost("/login", async (LoginRequest request, TokenService tokenService, IAuthService service) =>
{
    var user = await service.Login(request);
    var token = tokenService.GenerateToken(user.Id.ToString(), user.Email, user.Role);

    return Results.Ok(token);
}).AllowAnonymous();

// --- Ticket Endpoints ---

ticketsGroup.MapGet("", async (ITicketService service) =>
{
    var tickets = await service.GetActiveTicketsAsync();
    return Results.Ok(tickets);
}).WithDescription("Get all the active tickets.");

ticketsGroup.MapGet("/{ticketId:int}", async (int ticketId, ITicketService service) =>
{
    var ticket = await service.GetTicketByIdAsync(ticketId);
    return ticket is not null ? Results.Ok(ticket) : Results.NotFound();
}).WithDescription("Get infos abobut a given ticket.");

ticketsGroup.MapPost("", async ([FromBody] CreateTicketRequest request, ITicketService service) =>
{

    var id = await service.CreateTicketAsync(request);
    return Results.Created($"/api/v1/tickets/{id}", id);
}).WithDescription("Create a new ticket.");

ticketsGroup.MapPatch("/{ticketId:int}/status", async ([FromBody] UpdateTicketRequest request, int ticketId, ITicketService service) =>
{
    if (request.TicketId < 1) throw new Exception("Id cannot be lower than 1.");

    await service.UpdateTicketStatusAsync(request);
    return Results.NoContent();
}).WithDescription("Update the status of a given ticket.");

ticketsGroup.MapPost("/{ticketId:int}/comments", async ([FromBody] AddCommentRequest request, int ticketId, ITicketService service) =>
{
    if (request.TicketId < 1) throw new Exception("Id cannot be lower than 1.");
    await service.AddCommentToTicketAsync(request);
    return Results.Created();
}).WithDescription("Create a new comment for a specific ticket id");

ticketsGroup.MapGet("/{ticketId:int}/comments", async (int ticketId, ITicketService service, AppUser appUser) =>
{
    if (!appUser.IsInRole(Ruolo.ADMIN.ToString()))
        if((!appUser.IsInRole(Ruolo.OPERATOR.ToString()) || await service.IsUserAssignedToTicket(Int32.Parse(appUser.Id), ticketId))) return Results.Unauthorized();
    if (ticketId < 1) throw new Exception("Id cannot be lower than 1.");
    var comments = await service.GetCommentsOfTicket(ticketId);
    return Results.Ok(comments);
}).WithDescription("Get all the comments for a given ticket id")
.RequireAuthorization("ResourceAccess");
ticketsGroup.MapPost("/{ticketId:int}/assign", async ([FromBody] AssignTicketToUserRequest request, int ticketId,  ITicketService service) =>
{
    if (request.TicketId < 1) throw new Exception("Id cannot be lower than 1.");

    await service.AssignTicketToUserAsync(request);
    return Results.Ok();
}).WithDescription("Assign a given ticket a given operator");

ticketsGroup.MapPost("/{ticketId:int}/reopen", async (int ticketId, ITicketService service) =>
{
    if (ticketId < 1) throw new Exception("Id cannot be lower than 1.");
    await service.ReopenTicketAsync(ticketId);
    return Results.Ok();
}).WithDescription("Reopen and closed ticket.");

app.UseHttpsRedirection();


app.UseAuthentication();
app.UseAuthorization();
app.Run();
