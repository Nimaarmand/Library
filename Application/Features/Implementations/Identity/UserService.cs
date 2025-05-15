using Application.Dtos.Identity;
using Application.Dtos.Identity.Authentications;
using Application.Exceptions.BusinessExceptions;
using Application.Features.Definitions.Identity;
using Domain.Entities.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Implementations.Identity
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        public UserService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<string> GetFullNameUser(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId) ?? new ApplicationUser();
            return $"{user.FirstName} {user.LastName}";
        }
        public async Task<ApplicationUser> GetUserByUserId(string userId)
        {
            return await _userManager.FindByIdAsync(userId) ?? new ApplicationUser();
        }
        public async Task<ApplicationUser> GetUserByPhoneNumber(string phoneNumber)
        {
            return await _userManager.FindByNameAsync(phoneNumber) ?? new ApplicationUser();
        }
        public async Task<PasswordResponse> ChangePassword(ChangePasswordRequest request)
        {
            var user = await _userManager.FindByNameAsync(request.PhoneNumber);
            if (user == null)
            {
                throw new BusinessException(ErrorType.PhoneNumberNotFound);
            }

            var result = await _userManager.RemovePasswordAsync(user);
            if (!result.Succeeded)
            {
                throw new BusinessException(ErrorType.ErrorChangeNewPassword);
            }

            result = await _userManager.AddPasswordAsync(user, request.NewPassword);
            if (!result.Succeeded)
            {
                throw new BusinessException(ErrorType.ErrorChangeNewPassword);
            }

            return new PasswordResponse { Success = true, Message = "Password changed successfully." };
        }
        public async Task<PasswordResponse> ForgotPassword(ForgotsPasswordRequest request)
        {
            var user = await _userManager.FindByNameAsync(request.PhoneNumber);
            if (user == null)
            {
               // throw new BusinessException(ErrorType.PhoneNumberNotFound);
            }

            var resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);

            if (!string.IsNullOrEmpty(request.NewPassword))
            {
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                var result = await _userManager.ResetPasswordAsync(user, token, request.NewPassword);
                if (!result.Succeeded)
                {
                    throw new Exception($"Error updating password: {string.Join(", ", result.Errors.Select(e => e.Description))}");
                }
            }
            return new PasswordResponse { Success = true, Message = "Password has been updated successfully." };
        }
        public async Task<RoleResponse> ToggleUserStatus(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            if (user == null)
            {
                //throw new BusinessException(ErrorType.PhoneNumberNotFound);
            }
            var currentRoles = await _userManager.GetRolesAsync(user);
            string AdminRole = "Administrator";
            var equal = currentRoles.Contains(AdminRole);
            if (equal)
            {
                //throw new BusinessException(ErrorType.AdminUserCannotBeDeactivated);
            }

            user.IsActive = user.IsActive ? false : true;
            var result = await _userManager.UpdateAsync(user);
            return new RoleResponse { Success = result.Succeeded, Message = "switch Status successfully." };
        }
        public async Task<RegistrationResponse> UpdateUser(string phone, RegisterationRequest request)
        {
            var user = await _userManager.FindByNameAsync(phone);
            if (user == null)
            {                           
                throw new BusinessException(ErrorType.PhoneNumberNotFound);
            }

            if (!string.IsNullOrEmpty(request.PhoneNumber) && request.PhoneNumber != user.UserName)
            {
                var existingUser = await _userManager.FindByNameAsync(request.PhoneNumber);
                if (existingUser != null)
                {
                    throw new BusinessException(ErrorType.PhoneNumberAlreadyExists);
                }

                user.UserName = request.PhoneNumber;
                user.PhoneNumber = request.PhoneNumber;
            }

            
            var updateResult = await _userManager.UpdateAsync(user);
            if (updateResult.Succeeded)
            {
                //await _roleService.RemoveAllRolesFromUser(user);
               // await _roleService.AssignRoleToUser(user, request.roleName);
                return new RegistrationResponse() { UserId = user.Id };
            }
            else
            {
                throw new Exception($"Error updating user: {string.Join(", ", updateResult.Errors.Select(e => e.Description))}");
            }
        }

        public async Task<List<ApplicationUser>> GetAllUsers()
        {
            return await _userManager.Users.ToListAsync();
        }

    }
}
