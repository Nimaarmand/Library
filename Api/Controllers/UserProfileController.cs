using Application.Dtos.Identity.UserProfile;
using Application.Features.Definitions.Userprofile;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserProfileController : ControllerBase
    {
        private readonly IUserProfileService _userProfileService;

        public UserProfileController(IUserProfileService userProfileService)
        {
            _userProfileService = userProfileService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(string id)
        {
            var user = await _userProfileService.GetUserByIdAsync(id);
            if (user == null)
                return NotFound(new { message = "کاربر یافت نشد." });

            return Ok(user);
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userProfileService.GetAllUsersAsync();
            return Ok(users);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateUser([FromBody] UserProfileDto userProfileDto)
        {
            await _userProfileService.CreateUserAsync(userProfileDto);
            return Ok(new { message = "کاربر با موفقیت ایجاد شد." });
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateUser(string id, [FromBody] UserProfileDto userProfileDto)
        {
            await _userProfileService.UpdateUserAsync(id, userProfileDto);
            return Ok(new { message = "اطلاعات کاربر به‌روزرسانی شد." });
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            await _userProfileService.DeleteUserAsync(id);
            return Ok(new { message = "کاربر با موفقیت حذف شد." });
        }
    }
}
