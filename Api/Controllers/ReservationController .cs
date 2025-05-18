using Application.Dtos.Books;
using Application.Features.Definitions.Books;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReservationController : ControllerBase
    {
        private readonly IReservationBookService _reservationService;

        public ReservationController(IReservationBookService reservationService)
        {
            _reservationService = reservationService;
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddReservation([FromBody] ReservationDto reservationDto)
        {
            var result = await _reservationService.AddReservationAsync(reservationDto);
            return Ok(new { message = result });
        }

        [HttpDelete("remove/{id}")]
        public async Task<IActionResult> RemoveReservation(long id)
        {
            var result = await _reservationService.Remove(id);
            return Ok(new { message = result });
        }

        [HttpGet("all-books")]
        public async Task<IActionResult> GetAllBooks()
        {
            var books = await _reservationService.GetAllBooksAsync();
            return Ok(books);
        }

        [HttpPost("auto-release")]
        public async Task<IActionResult> AutoReleaseExpiredReservations()
        {
            await _reservationService.AutoReleaseExpiredReservationsAsync();
            return Ok(new { message = "رزروهای منقضی شده آزاد شدند." });
        }

        [HttpGet("all-reservations")]
        public async Task<IActionResult> GetAllReservations()
        {
            var reservations = await _reservationService.GetAllReservations();
            return Ok(reservations);
        }
    }
}
