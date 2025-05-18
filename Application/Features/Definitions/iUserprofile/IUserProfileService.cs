using Application.Dtos.Identity.UserProfile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Application.Features.Definitions.Userprofile
{
    public interface IUserProfileService
    {
        Task<UserProfileDto> GetUserByIdAsync(string id);
        Task<UserProfileDto> GetByIdAsync(string id);
        Task<List<UserProfileDto>> GetAllUsersAsync();
        Task CreateUserAsync(UserProfileDto userProfileDto);
        Task UpdateUserAsync(string id, UserProfileDto userProfileDto);
        Task DeleteUserAsync(string id);
    }
}
