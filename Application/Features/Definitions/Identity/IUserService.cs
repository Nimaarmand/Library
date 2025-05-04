using Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Definitions.Identity
{
    public interface IUserService
    {
        Task<string> GetFullNameUser(string userId);
        Task<ApplicationUser> GetUserByUserId(string userId);
        Task<ApplicationUser> GetUserByPhoneNumber(string phoneNumber);
        //Task<RegistrationResponse> UpdateUser(string phone, RegisterationRequest request);
        //Task<PasswordResponse> ChangePassword(ChangePasswordRequest request);
        //Task<PasswordResponse> ForgotPassword(ForgotsPasswordRequest request);
        //Task<RoleResponse> ChangeLockoutEnabled(string userId);
        //Task<RoleResponse> ToggleUserStatus(string userName);
        //Task<PaginatedItemsDto<UserResponse>> GetAllUsers(int page = 1, int pageSize = 20, bool isActive = false);
    }
}
