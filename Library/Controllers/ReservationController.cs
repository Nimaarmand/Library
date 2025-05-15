using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Threading;
using Application.Dtos.Books;
using Application.Exceptions.ValidationExceptions;
using Application.Features.Definitions.Books;
using Microsoft.AspNetCore.Identity;
using Domain.Entities.Users;

namespace Library.Controllers
{
    
    public class ReservationController : Controller
    {
        private readonly IReservationBookService _reservationBookService;
       private readonly IBookService _bookService;
        private readonly UserManager<ApplicationUser> _userManager;

        public ReservationController(IReservationBookService reservationBookService,
            UserManager<ApplicationUser> userManager
,
            IBookService bookService)
        {
            _reservationBookService = reservationBookService;
            _userManager = userManager;
            _bookService = bookService;
        }

        public async Task<IActionResult> GetAllBookNotReservation()
        {
            var list =await _bookService.GetAllNotReservation();

            if (list == null)
            {
                ViewBag.Message = "هیچ کتابی بدون رزرو یافت نشد.";
                return View();
            }

            return View(list);
        }
        public async Task<IActionResult> GetAllBookReservation()
        {
            var list = await _bookService.GetAllReservation();

            if (list == null)
            {
                ViewBag.Message = "هیچ کتابی بدون رزرو یافت نشد.";
                return View();
            }

            return View(list);
        }
        [HttpPost]
        public IActionResult TestReceiveId(long bookId)
        {
            if (bookId == 0)
            {
                return Json(new { success = false, message = "آیدی دریافت نشد یا صفر بود." });
            }

            return Json(new { success = true, message = $"آیدی دریافت شد: {bookId}" });
        }

        [HttpPost]
        public async Task<IActionResult> AddReservation([FromBody] ReservationDto reservationDto)
        {
            Console.WriteLine("🔥 اکشن فراخوانی شد");
            Console.WriteLine($"📘 BookId: {reservationDto?.BookId}");

            if (reservationDto == null || reservationDto.BookId == 0)
                return BadRequest(new { error = "شناسه کتاب نامعتبر است." });

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return Unauthorized(new { error = "کاربر وارد نشده است." });

            reservationDto.UserId = user.Id;

            try
            {
                var result = await _reservationBookService.AddReservationAsync(reservationDto);
                return Ok(new { message = "رزرو با موفقیت انجام شد." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "خطای سرور: " + ex.Message });
            }
        }


        [HttpDelete]
        public async Task<IActionResult> RemoveReservationAsync(long id)
        {
            if (id <= 0)
                return BadRequest("شناسه رزرو معتبر نیست.");

            try
            {
                var result = await _reservationBookService.Remove(id);
                return Ok(result);
            }
            catch (MyArgumentNullException ex)
            {
                return BadRequest(ex.Message); 
            }
            catch (Exception ex)
            {
                return StatusCode(500, "خطای داخلی سرور: " + ex.Message);
            }
        }

        // اکشن برای دریافت همه رزروها
        [HttpGet]
        public async Task<IActionResult> GetAllReservationsAsync(CancellationToken cancellationToken)
        {
            try
            {
                var reservations = await _reservationBookService.GetAllBooksAsync(cancellationToken);
                return Ok(reservations);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "خطای داخلی سرور: " + ex.Message);
            }
        }
    }
}
