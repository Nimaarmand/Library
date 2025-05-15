using Application.Dtos.Identity.UserProfile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Definitions.Userprofile
{
    public interface IUserProfileService
    {
        Task<UserProfileDto> GetUserByIdAsync(string id);
        Task<IEnumerable<UserProfileDto>> GetAllUsersAsync();
        Task CreateUserAsync(UserProfileDto userProfileDto);
        Task UpdateUserAsync(string id, UserProfileDto userProfileDto);
        Task DeleteUserAsync(string id);
    }
}
