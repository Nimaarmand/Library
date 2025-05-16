using Application.Dtos.Identity.UserProfile;
using Application.Features.Definitions.Userprofile;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class UserProfileController : ControllerBase
{
    private readonly IUserProfileService _userProfileService;

    public UserProfileController(IUserProfileService userProfileService)
    {
        _userProfileService = userProfileService;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<UserProfileDto>> GetUserById(string id)
    {
        var user = await _userProfileService.GetUserByIdAsync(id);
        if (user == null) return NotFound("کاربر یافت نشد.");
        return Ok(user);
    }

    [HttpGet("all")]
    public async Task<ActionResult<IEnumerable<UserProfileDto>>> GetAllUsers()
    {
        var users = await _userProfileService.GetAllUsersAsync();
        return Ok(users);
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateUser([FromBody] UserProfileDto userProfileDto)
    {
        await _userProfileService.CreateUserAsync(userProfileDto);
        return Ok("✅ کاربر با موفقیت ایجاد شد.");
    }

    [HttpPut("update/{id}")]
    public async Task<IActionResult> UpdateUser(string id, [FromBody] UserProfileDto userProfileDto)
    {
        await _userProfileService.UpdateUserAsync(id, userProfileDto);
        return Ok("✅ اطلاعات کاربر به‌روزرسانی شد.");
    }

    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> DeleteUser(string id)
    {
        await _userProfileService.DeleteUserAsync(id);
        return Ok("✅ کاربر با موفقیت حذف شد.");
    }
}