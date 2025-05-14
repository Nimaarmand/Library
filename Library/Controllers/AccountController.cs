using Application.Dtos.Identity;
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
        public AccountController(IAuthService authService)
        {
            _authService = authService;
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
                return Ok(response); // ارسال داده موفق
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

      
        public ActionResult Edit(int id)
        {
            return View();
        }

        [HttpPost]
      
        public ActionResult Edit(int id, IFormCollection collection)
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

      
        public ActionResult Delete(int id)
        {
            return View();
        }

      
        [HttpPost]
        
        public ActionResult Delete(int id, IFormCollection collection)
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
    }
}
