using Application.Dtos.Books;
using Application.Features.Definitions.Books;
using Domain.Entities.Reservations;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class ReservationBookController : ControllerBase
{
    private readonly IReservationBookService _reservationBookService;

    public ReservationBookController(IReservationBookService reservationBookService)
    {
        _reservationBookService = reservationBookService;
    }

    [HttpPost("add")]
    public async Task<IActionResult> AddReservation([FromBody] ReservationDto reservationDto)
    {
        var result = await _reservationBookService.AddReservationAsync(reservationDto);
        return Ok(result);
    }

    [HttpDelete("remove/{id}")]
    public async Task<IActionResult> RemoveReservation(long id)
    {
        var result = await _reservationBookService.Remove(id);
        return Ok(result);
    }

    [HttpGet("all-books")]
    public async Task<ActionResult<IEnumerable<Reservation>>> GetAllReservedBooks()
    {
        var reservations = await _reservationBookService.GetAllBooksAsync();
        return Ok(reservations);
    }

    [HttpGet("all")]
    public async Task<ActionResult<List<ReservationDto>>> GetAllReservations()
    {
        var reservations = await _reservationBookService.GetAllReservations();
        return Ok(reservations);
    }

    [HttpPost("auto-release-expired")]
    public async Task<IActionResult> AutoReleaseExpiredReservations()
    {
        await _reservationBookService.AutoReleaseExpiredReservationsAsync();
        return Ok("رزروهای منقضی شده آزاد شدند.");
    }
}