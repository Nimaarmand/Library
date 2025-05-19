using Application.Dtos.Delivery;
using Application.Dtos.Identity.UserProfile;
using Application.Features.Definitions.Delivery;
using Application.Features.Definitions.Identity;
using Application.Features.Definitions.Userprofile;
using Library.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Library.Controllers
{
    public class DeliveryController : Controller
    {
        private readonly IDeliveryService _deliveryService;
        private readonly IUserProfileService _userService;
        public DeliveryController(IDeliveryService deliveryService, IUserProfileService userService)
        {
            _deliveryService = deliveryService;
            _userService = userService;
        }
        // GET: Delivery
        public ActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GetAllResrvDelivery()
        {
            try
            {
                var list = await _deliveryService.GetAllResrvDelivery();
                return View(list);
            }
            catch (Exception ex)
            {
                return Content($"❌ خطا در نمایش اطلاعات تحویل‌شده‌ها:\n{ex.Message}\n\n{ex.StackTrace}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> ReturnBook(long id)
        {
            try
            {
                var list = await _deliveryService.ReturnBook(id);
                return View(list);
            }
            catch (Exception ex)
            {
                return Content($" پس گرفتن کتاب:\n{ex.Message}\n\n{ex.StackTrace}");
            }
        }

        public async Task<IActionResult> GetAllDelivery()
        {
            try
            {
                var list = await _deliveryService.GetAllDelivery();
                return View(list);
            }
            catch (Exception ex)
            {
                return Content($"❌ خطا در نمایش اطلاعات تحویل‌شده‌ها:\n{ex.Message}\n\n{ex.StackTrace}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> ReservationDelivery(long id)
        {
            var result = await _deliveryService.ReservationDelivery(id);

            if (result.Contains("خطا"))
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> Create(long bookId)
        {
            var users = await _userService.GetAllUsersAsync();

            var modelList = users.Select(user => new CreateDeliveryViewModel
            {
                BookId = bookId,
                Users = new List<UserProfileDto> { user }
            }).ToList();

            return View(modelList);
        }
      
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] DeliveryDto deliveryDto)
        {
            try
            {
              
                Console.WriteLine($"BookId: {deliveryDto.BookId}, UserId: {deliveryDto.UserId}");

            
                await _deliveryService.Add(deliveryDto);

                return Json(new
                {
                    success = true,
                    message = "تحویل با موفقیت ثبت شد!"
                });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    success = false,
                    message = "خطایی رخ داده است: " + ex.Message
                });
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
    public class TestDeliveryDto
    {
        public long BookId { get; set; }
        public string UserId { get; set; }
    }
}




