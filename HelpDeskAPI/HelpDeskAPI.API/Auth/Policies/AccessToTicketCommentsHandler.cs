using HelpDeskAPI.Application.Interfaces;
using HelpDeskAPI.Core.Models;
using HelpDeskAPI.Infrastructure.Database;
using Microsoft.AspNetCore.Authorization;
using System.Runtime.ConstrainedExecution;
using System.Security.Claims;

namespace HelpDeskAPI.API.Auth.Policies;

public class AccessToTicketCommentsHandler(IAssegnazioneRepository _repo, IHttpContextAccessor _httpContextAccessor) : AuthorizationHandler<AccessToTicketCommentsRequirement>
{
    private readonly IHttpContextAccessor _httpContextAccessor = _httpContextAccessor;
    readonly private IAssegnazioneRepository _repo = _repo;
    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, AccessToTicketCommentsRequirement requirement)
    {
        var user = context.User;
        var role = user.FindFirst(ClaimTypes.Role)!.Value;
        if (role == Ruolo.ADMIN.ToString()) context.Succeed(requirement);
        var id = user.FindFirst(ClaimTypes.NameIdentifier)!.Value;
        var ticketId = _httpContextAccessor.HttpContext?.GetRouteValue("ticketId")?.ToString();
        if (role == Ruolo.OPERATOR.ToString() && await _repo.IsUserAssignedToTicket(Int32.Parse(id), Int32.Parse(ticketId))) context.Succeed(requirement);
    }
}
