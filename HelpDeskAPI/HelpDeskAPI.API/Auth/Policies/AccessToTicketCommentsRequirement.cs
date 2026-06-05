using HelpDeskAPI.Core.Models;
using Microsoft.AspNetCore.Authorization;

namespace HelpDeskAPI.API.Auth.Policies;

public class AccessToTicketCommentsRequirement : IAuthorizationRequirement { }