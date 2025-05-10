using Application.Dtos.Identity;
using Application.Dtos.Identity.Authentications;
using Domain.Entities.Users;

namespace Application.Features.Definitions.Identity
{
    public interface IUserService
    {
        Task<string> GetFullNameUser(string userId);
        Task<ApplicationUser> GetUserByUserId(string userId);
        Task<ApplicationUser> GetUserByPhoneNumber(string phoneNumber);
        Task<RegistrationResponse> UpdateUser(string phone, RegisterationRequest request);
        Task<PasswordResponse> ChangePassword(ChangePasswordRequest request);
        Task<PasswordResponse> ForgotPassword(ForgotsPasswordRequest request);
    }
}
