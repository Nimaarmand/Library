using Application.Dtos.Identity;
using Application.Dtos.Identity.UserProfile;
using Application.Exceptions.BusinessExceptions;
using Application.Features.Definitions.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Library.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAuthService _authService;
        private readonly IUserService _userService;
        public AccountController(IAuthService authService, IUserService userService)
        {
            _authService = authService;
            _userService = userService;
        }
        public async Task<IActionResult> ListUser()
        {
            var users = await _userService.GetAllUsers();
            return View(users); 
        }
        [HttpGet]
        public IActionResult UserProfile()
        {
            return View();
        }
        [HttpPost]
        public IActionResult UserProfile(UserProfileDto userProfileDto)
        {
            return View();
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] AuthRequest request)
        {
            try
            {
                if (string.IsNullOrEmpty(request.PhoneNumber))
                {
                    return BadRequest(new { error = "شماره تلفن وارد نشده است." });
                }

                var response = await _authService.Login(request);
                return Ok(response); 
            }
            catch (BusinessException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
            catch (Exception)
            {
                return StatusCode(500, new { error = "خطای داخلی سرور" });
            }
        }

        public IActionResult Register()
        {
            return View();
        }

        
        [HttpPost]
        public async Task< IActionResult> Register(RegisterationRequest request)
        {
            try
            {
                await _authService.Register(request);
                return Json(new { success = true });
            }
            catch
            {
                return Json(new { success = false });
            }
        }


        [HttpGet]
        public async Task<IActionResult> Edit(string phoneNumber)
        {
            var user = await _userService.GetUserByPhoneNumber(phoneNumber);
            if (user == null)
            {
                return NotFound();
            }

            var model = new RegisterationRequest
            {
                PhoneNumber = user.PhoneNumber,
               
               
            };

            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> Edit(string phoneNumber, RegisterationRequest request)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { success = false, message = "اطلاعات وارد شده نامعتبر است." });
            }

            try
            {
                await _userService.UpdateUser(phoneNumber, request);
                return Json(new { success = true, message = "اطلاعات با موفقیت به‌روزرسانی شد." });
            }
            catch (BusinessException ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
            catch (Exception)
            {
                return Json(new { success = false, message = "خطایی در سرور رخ داد." });
            }
        }




        public IActionResult Delete(int id)
        {
            return View();
        }

      
        [HttpPost]
        
        public async Task<IActionResult> Delete(long id)
        {
            try
            {
               
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public async Task<IActionResult> Logout()
        {
           await _authService.Logout();
           return RedirectToAction("Index", "Home");

        }
    }
}
