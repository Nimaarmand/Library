using Application.Exceptions.BusinessExceptions;
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
    //public class RoleService : IRoleService
    //{
    //    private readonly UserManager<ApplicationUser> _userManager;
    //    private readonly RoleManager<ApplicationRole> _roleManager;
    //    public RoleService(RoleManager<ApplicationRole> roleManager, UserManager<ApplicationUser> userManager)
    //    {
    //        _roleManager = roleManager;
    //        _userManager = userManager;
    //    }

    //    public async Task AssignRoleToUser(ApplicationUser user, string roleName)
    //    {
    //        var roleExists = await _roleManager.RoleExistsAsync(roleName);
    //        if (!roleExists)
    //        {
    //            throw new BusinessException(ErrorType.RoleNotFound);
    //        }

    //        var result = await _userManager.AddToRoleAsync(user, roleName);
    //        if (!result.Succeeded)
    //        {
    //            throw new Exception($"Error assigning role: {string.Join(", ", result.Errors.Select(e => e.Description))}");
    //        }
    //    }
    //    public async Task RemoveRoleFromUser(ApplicationUser user, string roleName)
    //    {
    //        var result = await _userManager.RemoveFromRoleAsync(user, roleName);
    //        if (!result.Succeeded)
    //        {
    //            throw new Exception($"Error removing role: {string.Join(", ", result.Errors.Select(e => e.Description))}");
    //        }
    //    }
    //    public async Task RemoveAllRolesFromUser(ApplicationUser user)
    //    {
    //        var userRoles = await _userManager.GetRolesAsync(user);
    //        if (!userRoles.Any())
    //        {
    //            return;
    //        }

    //        var result = await _userManager.RemoveFromRolesAsync(user, userRoles);

    //        if (!result.Succeeded)
    //        {
    //            throw new Exception($"Error removing roles: {string.Join(", ", result.Errors.Select(e => e.Description))}");
    //        }
    //    }

    //    public async Task<IList<string>> GetRolesForUser(ApplicationUser user)
    //    {
    //        return await _userManager.GetRolesAsync(user);
    //    }
    //    public async Task<IList<ApplicationUser>> GetUsersInRole(string roleName)
    //    {
    //        var role = await _roleManager.FindByNameAsync(roleName);
    //        if (role == null)
    //        {
    //            throw new BusinessException(ErrorType.RoleNotFound);
    //        }

    //        var users = await _userManager.GetUsersInRoleAsync(roleName);
    //        return users.ToList();
    //    }
    //    public async Task<bool> UserHasRole(ApplicationUser user, string roleName)
    //    {
    //        var roles = await _userManager.GetRolesAsync(user);
    //        return roles.Contains(roleName);
    //    }
    //    public async Task<bool> IsUserInAnyRoleAsync(ApplicationUser user)
    //    {
    //        var roles = await _userManager.GetRolesAsync(user);
    //        return roles.Any();
    //    }
    //    public async Task UpdateUserRoles(ApplicationUser user, List<string> newRoles)
    //    {
    //        var currentRoles = await _userManager.GetRolesAsync(user);

    //        var removeRoles = currentRoles.Except(newRoles);
    //        await _userManager.RemoveFromRolesAsync(user, removeRoles);

    //        var addRoles = newRoles.Except(currentRoles);
    //        await _userManager.AddToRolesAsync(user, addRoles);
    //    }
    //    public async Task BulkAssignRolesToUsers(List<ApplicationUser> users, List<string> roleNames)
    //    {
    //        foreach (var user in users)
    //        {
    //            foreach (var roleName in roleNames)
    //            {
    //                if (await _roleManager.RoleExistsAsync(roleName))
    //                {
    //                    if (!await _userManager.IsInRoleAsync(user, roleName))
    //                    {
    //                        await _userManager.AddToRoleAsync(user, roleName);
    //                    }
    //                }
    //            }
    //        }
    //    }
    //    public async Task BulkRemoveRolesFromUsers(List<ApplicationUser> users, List<string> roleNames)
    //    {
    //        foreach (var user in users)
    //        {
    //            foreach (var roleName in roleNames)
    //            {
    //                if (await _roleManager.RoleExistsAsync(roleName))
    //                {
    //                    if (await _userManager.IsInRoleAsync(user, roleName))
    //                    {
    //                        await _userManager.RemoveFromRoleAsync(user, roleName);
    //                    }
    //                }
    //            }
    //        }
    //    }

    //    public async Task<bool> Exists(string roleName)
    //    {
    //        var role = await _roleManager.FindByNameAsync(roleName);
    //        return role != null;
    //    }
    //    public async Task<ApplicationRole> GetRoleDetails(string roleName)
    //    {
    //        var role = await _roleManager.FindByNameAsync(roleName);
    //        if (role == null)
    //        {
    //            throw new BusinessException(ErrorType.RoleNotFound);
    //        }
    //        return role;
    //    }
    //    public async Task<ApplicationRole> GetRoleById(string roleId)
    //    {
    //        var role = await _roleManager.FindByIdAsync(roleId);
    //        if (role == null)
    //        {
    //            throw new BusinessException(ErrorType.RoleNotFound);
    //        }
    //        return role;
    //    }
    //    public async Task<IEnumerable<RoleDto>> GetAllRoles()
    //    {
    //        return await Task.Run(() => _roleManager.Roles.ToList().Select(row => new RoleDto
    //        {
    //            Name = row.Name ?? "",
    //            Description = row.Description ?? "",
    //        }).ToList());
    //    }
    //    public async Task<IList<ApplicationRole>> GetRoles()
    //    {
    //        return await Task.Run(() => _roleManager.Roles.ToList());
    //    }
    //    public async Task CreateRole(RoleDto request)
    //    {
    //        var roleExists = await _roleManager.RoleExistsAsync(request.Name);
    //        if (roleExists)
    //        {
    //            throw new BusinessException(ErrorType.RoleAlreadyExists);
    //        }

    //        var role = new ApplicationRole { Name = request.Name, Description = request.Description };
    //        var result = await _roleManager.CreateAsync(role);

    //        if (!result.Succeeded)
    //        {
    //            throw new Exception($"Error creating role: {string.Join(", ", result.Errors.Select(e => e.Description))}");
    //        }
    //    }
    //    public async Task UpdateRole(string roleName, RoleDto request)
    //    {
    //        var role = await _roleManager.FindByNameAsync(roleName);
    //        if (role == null)
    //        {
    //            throw new BusinessException(ErrorType.RoleNotFound);
    //        }

    //        role.Name = request.Name;
    //        role.Description = request.Description;
    //        var result = await _roleManager.UpdateAsync(role);
    //        if (!result.Succeeded)
    //        {
    //            throw new Exception($"Error updating role: {string.Join(", ", result.Errors.Select(e => e.Description))}");
    //        }
    //    }
    //    public async Task DeleteRole(string roleName)
    //    {
    //        var role = await _roleManager.FindByNameAsync(roleName);
    //        if (role == null)
    //        {
    //            throw new BusinessException(ErrorType.RoleNotFound);
    //        }

    //        var usersInRole = await _userManager.GetUsersInRoleAsync(roleName);
    //        if (usersInRole.Any())
    //        {
    //            throw new BusinessException(ErrorType.RoleHasAssociatedUsers);
    //        }

    //        var result = await _roleManager.DeleteAsync(role);
    //        if (!result.Succeeded)
    //        {
    //            throw new Exception($"Error deleting role: {string.Join(", ", result.Errors.Select(e => e.Description))}");
    //        }
    //    }

    //    #region Permissions Role

    //    //public async Task AddPermissionsToRoleAsync(string roleName, List<string> permissions)
    //    //{
    //    //    // 11. تعریف تنظیمات نقش (برای نقش‌ها، مانند مجوزها و تنظیمات خاص)
    //    //    var role = await _roleManager.FindByNameAsync(roleName);
    //    //    if (role == null)
    //    //    {
    //    //        throw new BusinessException(ErrorType.NotFoundRole);
    //    //    }

    //    //    // فرض کنید اینجا تنظیمات مجوز به نقش اضافه می‌شود
    //    //    foreach (var permission in permissions)
    //    //    {
    //    //        // افزودن مجوز به نقش (در اینجا فرض کردیم که شما از یک سیستم مدیریت مجوز استفاده می‌کنید)
    //    //        role.Claims.Add(new IdentityRoleClaim<string>
    //    //        {
    //    //            RoleId = role.Id,
    //    //            ClaimType = "Permission",
    //    //            ClaimValue = permission
    //    //        });
    //    //    }

    //    //    var result = await _roleManager.UpdateAsync(role);
    //    //    if (!result.Succeeded)
    //    //    {
    //    //        throw new Exception($"Error adding permissions to role: {string.Join(", ", result.Errors.Select(e => e.Description))}");
    //    //    }
    //    //}
    //    //public async Task<IList<string>> GetPermissionsForRoleAsync(string roleName)
    //    //{
    //    //    // 12. بررسی مجوزها و محدودیت‌ها برای نقش خاص
    //    //    var role = await _roleManager.FindByNameAsync(roleName);
    //    //    if (role == null)
    //    //    {
    //    //        throw new BusinessException(ErrorType.NotFoundRole);
    //    //    }

    //    //    var permissions = role.Claims.Where(c => c.ClaimType == "Permission").Select(c => c.ClaimValue).ToList();
    //    //    return permissions;
    //    //}
    //    //public async Task<bool> RoleHasAccessToResourceAsync(string roleName, string resourceName)
    //    //{
    //    //    var role = await _roleManager.FindByNameAsync(roleName);
    //    //    if (role == null)
    //    //    {
    //    //        throw new Exception($"Role '{roleName}' does not exist.");
    //    //    }

    //    //    // فرض کنید هر نقش یک مجموعه دسترسی به منابع دارد
    //    //    var permissions = await GetPermissionsForRoleAsync(roleName);
    //    //    return permissions.Contains(resourceName);
    //    //}
    //    //public async Task CreateRoleWithPermissionsAsync(string roleName, List<string> permissions)
    //    //{
    //    //    var roleExists = await _roleManager.RoleExistsAsync(roleName);
    //    //    if (roleExists)
    //    //    {
    //    //        throw new Exception($"Role '{roleName}' already exists.");
    //    //    }

    //    //    var role = new ApplicationRole { Name = roleName };
    //    //    var result = await _roleManager.CreateAsync(role);
    //    //    if (!result.Succeeded)
    //    //    {
    //    //        throw new Exception($"Error creating role: {string.Join(", ", result.Errors.Select(e => e.Description))}");
    //    //    }

    //    //    // افزودن مجوزها به نقش
    //    //    await AddPermissionsToRoleAsync(roleName, permissions);
    //    //}
    //    //public async Task RemovePermissionFromRoleAsync(string roleName, string permission)
    //    //{
    //    //    var role = await _roleManager.FindByNameAsync(roleName);
    //    //    if (role == null)
    //    //    {
    //    //        throw new Exception($"Role '{roleName}' does not exist.");
    //    //    }

    //    //    var claim = role.Claims.FirstOrDefault(c => c.ClaimType == "Permission" && c.ClaimValue == permission);
    //    //    if (claim != null)
    //    //    {
    //    //        role.Claims.Remove(claim);
    //    //        var result = await _roleManager.UpdateAsync(role);
    //    //        if (!result.Succeeded)
    //    //        {
    //    //            throw new Exception($"Error removing permission from role: {string.Join(", ", result.Errors.Select(e => e.Description))}");
    //    //        }
    //    //    }
    //    //    else
    //    //    {
    //    //        throw new Exception($"Permission '{permission}' not found for role '{roleName}'.");
    //    //    }
    //    //}

    //    #endregion

    //}
}
