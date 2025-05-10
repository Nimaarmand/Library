using Application.Dtos.Identity;
using Application.Dtos.Identity.Authentications;

namespace Application.Features.Definitions.Identity
{
    public interface IAuthService
    {
        Task<AuthResponse> Login(AuthRequest request);
        Task<RegistrationResponse> Register(RegisterationRequest request);
        Task Logout();
        Task<string> GetCurrentUserId();
    }
}
