using Application.Dtos.Commons;
using Application.Features.Definitions.Identity;
using Domain.Entities.Users;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Implementations.Identity
{
    //public class UserService : IUserService
    //{
    //    private readonly IRoleService _roleService;
    //    //private readonly IDisposableCodeService _disposableCodeService;
    //    private readonly UserManager<ApplicationUser> _userManager;
    //    public UserService(UserManager<ApplicationUser> userManager, /*IDisposableCodeService disposableCodeService*/, IRoleService roleService)
    //    {
    //        _userManager = userManager;
    //        //_disposableCodeService = disposableCodeService;
    //        _roleService = roleService;
    //    }

    //    public async Task<string> GetFullNameUser(string userId)
    //    {
    //        var user = await _userManager.FindByIdAsync(userId) ?? new ApplicationUser();
    //        return $"{user.FirstName} {user.LastName}";
    //    }
    //    public async Task<ApplicationUser> GetUserByUserId(string userId)
    //    {
    //        return await _userManager.FindByIdAsync(userId) ?? new ApplicationUser();
    //    }
    //    public async Task<ApplicationUser> GetUserByPhoneNumber(string phoneNumber)
    //    {
    //        return await _userManager.FindByNameAsync(phoneNumber) ?? new ApplicationUser();
    //    }
    //    public async Task<PasswordResponse> ChangePassword(ChangePasswordRequest request)
    //    {
    //        var user = await _userManager.FindByNameAsync(request.PhoneNumber);
    //        if (user == null)
    //        {
    //            throw new BusinessException(ErrorType.PhoneNumberNotFound);
    //        }

    //        await _disposableCodeService.UpdateDisposableCodeAsUsed(new Dtos.Messengers.Sms.DisposableCodeDto
    //        {
    //            PhoneNumber = request.PhoneNumber,
    //            DisposableCode = request.DisposableCode,
    //        }, true);

    //        var result = await _userManager.RemovePasswordAsync(user);
    //        if (!result.Succeeded)
    //        {
    //            throw new BusinessException(ErrorType.ErrorChangeNewPassword);
    //        }

    //        result = await _userManager.AddPasswordAsync(user, request.NewPassword);
    //        if (!result.Succeeded)
    //        {
    //            throw new BusinessException(ErrorType.ErrorChangeNewPassword);
    //        }

    //        return new PasswordResponse { Success = true, Message = "Password changed successfully." };
    //    }
    //    public async Task<PasswordResponse> ForgotPassword(ForgotsPasswordRequest request)
    //    {
    //        var user = await _userManager.FindByNameAsync(request.PhoneNumber);
    //        if (user == null)
    //        {
    //            throw new BusinessException(ErrorType.PhoneNumberNotFound);
    //        }

    //        var resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);

    //        await _disposableCodeService.UpdateDisposableCodeAsUsed(new Dtos.Messengers.Sms.DisposableCodeDto
    //        {
    //            PhoneNumber = request.PhoneNumber,
    //            DisposableCode = request.DisposableCode,
    //        }, true);

    //        if (!string.IsNullOrEmpty(request.NewPassword))
    //        {
    //            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
    //            var result = await _userManager.ResetPasswordAsync(user, token, request.NewPassword);
    //            if (!result.Succeeded)
    //            {
    //                throw new Exception($"Error updating password: {string.Join(", ", result.Errors.Select(e => e.Description))}");
    //            }
    //        }
    //        return new PasswordResponse { Success = true, Message = "Password has been updated successfully." };
    //    }
    //    public async Task<RoleResponse> ToggleUserStatus(string userName)
    //    {
    //        var user = await _userManager.FindByNameAsync(userName);
    //        if (user == null)
    //        {
    //            throw new BusinessException(ErrorType.PhoneNumberNotFound);
    //        }
    //        var currentRoles = await _userManager.GetRolesAsync(user);
    //        string AdminRole = "Administrator";
    //        var equal = currentRoles.Contains(AdminRole);
    //        if (equal)
    //        {
    //            throw new BusinessException(ErrorType.AdminUserCannotBeDeactivated);
    //        }

    //        user.IsActive = user.IsActive ? false : true;
    //        var result = await _userManager.UpdateAsync(user);
    //        return new RoleResponse { Success = result.Succeeded, Message = "switch Status successfully." };
    //    }
    //    public async Task<RegistrationResponse> UpdateUser(string phone, RegisterationRequest request)
    //    {
    //        var user = await _userManager.FindByNameAsync(phone);
    //        if (user == null)
    //        {
    //            throw new BusinessException(ErrorType.PhoneNumberNotFound);
    //        }

    //        if (!string.IsNullOrEmpty(request.PhoneNumber) && request.PhoneNumber != user.UserName)
    //        {
    //            var existingUser = await _userManager.FindByNameAsync(request.PhoneNumber);
    //            if (existingUser != null)
    //            {
    //                throw new BusinessException(ErrorType.PhoneNumberAlreadyExists);
    //            }

    //            await _disposableCodeService.UpdateDisposableCodeAsUsed(new Dtos.Messengers.Sms.DisposableCodeDto
    //            {
    //                PhoneNumber = request.PhoneNumber,
    //                DisposableCode = request.DisposableCode,
    //            }, true);

    //            user.UserName = request.PhoneNumber;
    //            user.PhoneNumber = request.PhoneNumber;
    //        }

    //        user.FirstName = request.FirstName ?? user.FirstName;
    //        user.LastName = request.LastName ?? user.LastName;
    //        user.ConcurrencyStamp = Guid.NewGuid().ToString();

    //        var updateResult = await _userManager.UpdateAsync(user);
    //        if (updateResult.Succeeded)
    //        {
    //            await _roleService.RemoveAllRolesFromUser(user);
    //            await _roleService.AssignRoleToUser(user, request.roleName);
    //            return new RegistrationResponse() { UserId = user.Id };
    //        }
    //        else
    //        {
    //            throw new Exception($"Error updating user: {string.Join(", ", updateResult.Errors.Select(e => e.Description))}");
    //        }
    //    }
    //    public async Task<RoleResponse> ChangeLockoutEnabled(string userId)
    //    {
    //        var user = await _userManager.FindByIdAsync(userId);
    //        if (user == null)
    //        {
    //            return new RoleResponse { Success = false, Message = "User not found." };
    //        }
    //        var currentRoles = await _userManager.GetRolesAsync(user);
    //        string AdminRole = "SuperAdministrator";
    //        var equal = currentRoles.Contains(AdminRole);
    //        if (equal)
    //        {
    //            return new RoleResponse { Success = false, Message = "Super Administrator role cannot be disabled." };
    //        }
    //        user.LockoutEnabled = user.LockoutEnabled ? false : true;
    //        var result = await _userManager.UpdateAsync(user);

    //        return new RoleResponse { Success = result.Succeeded, Message = "switch Status successfully." };
    //    }
    //    public async Task DisableUserAccess(ApplicationUser user)
    //    {
    //        user.LockoutEnd = DateTimeOffset.UtcNow.AddYears(100); // قفل کردن حساب کاربر به طور دائم
    //        var result = await _userManager.UpdateAsync(user);
    //        if (!result.Succeeded)
    //        {
    //            throw new Exception($"Error disabling user access: {string.Join(", ", result.Errors.Select(e => e.Description))}");
    //        }
    //    }
    //    public async Task<PaginatedItemsDto<UserResponse>> GetAllUsers(int page = 1, int pageSize = 20, bool isActive = false)
    //    {
    //        var users = _userManager.Users.ToList();
    //        var userResponses = new List<UserResponse>();
    //        var roleOrder = new Dictionary<string, int> { { "SuperAdministrator", 1 }, { "Administrator", 2 }, { "User", 3 } };

    //        foreach (var user in users)
    //        {
    //            var roles = await _userManager.GetRolesAsync(user);
    //            var userResponse = new UserResponse
    //            {
    //                Id = user.Id,
    //                FirstName = user.FirstName,
    //                LastName = user.LastName,
    //                PhoneNumber = user.UserName ?? "",
    //                IsActive = user.IsActive,
    //                Roles = roles.ToList(),
    //            };

    //            if (isActive)
    //            {
    //                if (userResponse.IsActive == true)
    //                {
    //                    userResponses.Add(userResponse);
    //                }
    //            }
    //            else
    //            {
    //                userResponses.Add(userResponse);
    //            }
    //        }

    //        var sortedUserResponses = userResponses.OrderBy(u => u.Roles.Count > 0 ? roleOrder.GetValueOrDefault(u.Roles[0], int.MaxValue) : int.MaxValue).ToList();

    //        int rowCount = 0;
    //        var data = sortedUserResponses.AsQueryable().PagedResult(page, pageSize, out rowCount).ToList();

    //        return new PaginatedItemsDto<UserResponse>(page, pageSize, rowCount, data);
    //    }
    //}
}
