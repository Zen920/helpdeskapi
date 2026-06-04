using System.Security.Claims;
namespace HelpDeskAPI.Api.Auth;
public class AppUser : ClaimsPrincipal
{
    public AppUser(IHttpContextAccessor contextAccessor) : base(contextAccessor.HttpContext.User) { }

    public string Id => FindFirst(ClaimTypes.NameIdentifier).Value;
    public string Email => FindFirst(ClaimTypes.Email).Value;
}