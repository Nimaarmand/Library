using Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Definitions.Identity
{
    public interface IRoleService
    {
        Task AssignRoleToUser(ApplicationUser user, string roleName);
        Task RemoveRoleFromUser(ApplicationUser user, string roleName);
        Task RemoveAllRolesFromUser(ApplicationUser user);
        //Task<IEnumerable<RoleDto>> GetAllRoles();
    }
}
