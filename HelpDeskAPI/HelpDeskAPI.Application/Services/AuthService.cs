using HelpDeskAPI.Application.Interfaces;
using HelpDeskAPI.Core.DTOs;
namespace HelpDeskAPI.Application.Services;

public class AuthService(IUtenteRepository _userRepo) : IAuthService
{
    private readonly IUtenteRepository _userRepo = _userRepo;
    public async Task<LoginResponse> Login(LoginRequest loginRequest)
    {
        var user = await _userRepo.GetUtenteByEmail(loginRequest.Email);
        if (user is null) throw new Exception("No account associated with this email.");
        if (user.Password != loginRequest.Password) throw new Exception("Password is invalid.");
        return new LoginResponse(loginRequest.Email, user.Id, user.Role);
    }
}
