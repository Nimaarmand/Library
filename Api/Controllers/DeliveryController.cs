using Application.Dtos.Delivery;
using Application.Features.Definitions.Delivery;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DeliveryController : ControllerBase
    {
        private readonly IDeliveryService _deliveryService;

        public DeliveryController(IDeliveryService deliveryService)
        {
            _deliveryService = deliveryService;
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddDelivery([FromBody] DeliveryDto deliveryDto)
        {
            var result = await _deliveryService.Add(deliveryDto);
            return Ok(new { message = result });
        }

        [HttpPost("return/{deliveryId}")]
        public async Task<IActionResult> ReturnBook(long deliveryId)
        {
            var result = await _deliveryService.ReturnBook(deliveryId);
            return Ok(new { message = result });
        }

        [HttpPost("reservation-delivery/{reservId}")]
        public async Task<IActionResult> ReservationDelivery(long reservId)
        {
            var result = await _deliveryService.ReservationDelivery(reservId);
            return Ok(new { message = result });
        }

        [HttpGet("all-reservations")]
        public async Task<IActionResult> GetAllResrvDelivery()
        {
            var deliveries = await _deliveryService.GetAllResrvDelivery();
            return Ok(deliveries);
        }
    }
}
