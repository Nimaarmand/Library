using Application.Dtos.Identity.UserProfile;
using Application.Features.Definitions.Userprofile;
using Application.MappingProfile;
using Domain.Entities.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RestSharp;
using System.Text.Json;

namespace Library.Controllers
{
    public class UserProfileController : Controller
    {
        private readonly IUserProfileService _userProfileService;
       
        private readonly UserManager<ApplicationUser> _userManager;

        public UserProfileController(IUserProfileService userProfileService, UserManager<ApplicationUser> userManager)
        {
            _userProfileService = userProfileService;
         
           _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var id = _userManager.GetUserId(User);
            if (id != null)
            {
                // دریافت اطلاعات کاربر
                var user = await _userProfileService.GetUserByIdAsync(id);

                // دریافت اطلاعات پروفایل کاربر
                var profile = await _userProfileService.GetUserByIdAsync(id);

                if (profile != null && !string.IsNullOrEmpty(profile.ImagePath))
                {
                    user.ImagePath = $"/uploads/{profile.ImagePath}"; // مقداردهی مسیر تصویر
                }

                return View(user);
            }
            return NotFound();
        }
        //public async Task<IActionResult> Index()
        //{
        //    var id = _userManager.GetUserId(User);
        //    if (id != null)
        //    {
        //        var user = await _userProfileService.GetUserByIdAsync(id);
        //        return View(user); // مدل از نوع UserProfileDto است
        //    }
        //    return NotFound();
        //}



        public async Task<IActionResult> Details()
        {
            var id= _userManager.GetUserId(User);
            var user = await _userProfileService.GetUserByIdAsync(id);
            if (user == null)
                return NotFound();

            return View(user);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] UserProfileDto userProfileDto, [FromForm] IFormFile ProfileImage)
        {
            var userId = _userManager.GetUserId(User);
            userProfileDto.UserId = userId;

            if (string.IsNullOrEmpty(userProfileDto.UserId))
            {
                return Json(new
                {
                    success = false,
                    message = "ابتدا باید وارد شوید."
                });
            }
            if (ProfileImage != null && ProfileImage.Length > 0)
            {
                var client = new RestClient("https://localhost:44376");
                var request = new RestRequest("/api/FileUpload/upload", Method.Post); 

                using (var memoryStream = new MemoryStream())
                {
                    await ProfileImage.CopyToAsync(memoryStream);
                    request.AddFile("imageFile", memoryStream.ToArray(), ProfileImage.FileName, "multipart/form-data");
                }

                var response = await client.ExecuteAsync<UploadResponseDto>(request);

                if (response.IsSuccessful && response.Data != null)
                {
                    userProfileDto.ImagePath = response.Data.FileUrl;
                }
                else
                {
                    return BadRequest(new { success = false, message = "خطا در آپلود تصویر." });
                }
            }
            await _userProfileService.CreateUserAsync(userProfileDto);

            return Json(new
            {
                success = true,
                message = "اطلاعات کاربر با موفقیت ثبت شد!",
                imageUrl = userProfileDto.ImagePath
            });
        }


        public async Task<IActionResult> Edit(string id)
        {
            var user = await _userProfileService.GetUserByIdAsync(id);
            if (user == null)
                return NotFound();

            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string id, UserProfileDto userProfileDto)
        {
            if (!ModelState.IsValid || userProfileDto.Id != id)
                return View(userProfileDto);

            await _userProfileService.UpdateUserAsync(id, userProfileDto);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(string id)
        {
            var user = await _userProfileService.GetUserByIdAsync(id);
            if (user == null)
                return NotFound();

            return View(user);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            await _userProfileService.DeleteUserAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
    public class UploadResponseDto
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public string FileUrl { get; set; }
    }

}
