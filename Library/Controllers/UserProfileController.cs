using Application.Dtos.Identity.UserProfile;
using Application.Features.Definitions.Userprofile;
using Domain.Entities.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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
                var user = await _userProfileService.GetUserByIdAsync(id);
                return View(user); // مدل از نوع UserProfileDto است
            }
            return NotFound();
        }



        public async Task<IActionResult> Details(string id)
        {
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
        public async Task<IActionResult> Create([FromForm] UserProfileDto userProfileDto, IFormFile imageFile)
        {
            var userId = _userManager.GetUserId(User);
            userProfileDto.Id = userId;

            if (string.IsNullOrEmpty(userProfileDto.Id))
            {
                return BadRequest(new
                {
                    success = false,
                    message = "ابتدا باید وارد شوید."
                });
            }

            if (imageFile == null || imageFile.Length == 0)
            {
                return BadRequest(new
                {
                    success = false,
                    message = "تصویر ارسال نشده است."
                });
            }

            // 📤 ارسال تصویر به API آپلود
            using var httpClient = new HttpClient();
            using var formData = new MultipartFormDataContent();

            var streamContent = new StreamContent(imageFile.OpenReadStream());
            streamContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(imageFile.ContentType);
            formData.Add(streamContent, "imageFile", imageFile.FileName);

            var response = await httpClient.PostAsync("https://localhost:5001/api/UploadImage", formData); // آدرس API خودت رو بزن

            if (!response.IsSuccessStatusCode)
            {
                return StatusCode((int)response.StatusCode, new
                {
                    success = false,
                    message = "آپلود تصویر با خطا مواجه شد."
                });
            }

            var jsonResult = await response.Content.ReadAsStringAsync();
            var uploadResponse = JsonSerializer.Deserialize<UploadResult>(jsonResult, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            // ذخیره نام فایل در DTO
            userProfileDto.ImagePath = uploadResponse.FileName;

            // ذخیره اطلاعات کاربر
            await _userProfileService.CreateUserAsync(userProfileDto);

            return Json(new
            {
                success = true,
                message = "پروفایل با موفقیت ثبت شد."
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

}
