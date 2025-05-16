using Application.Dtos.Delivery;
using Application.Features.Definitions.Delivery;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
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
        return Ok(result);
    }

    [HttpPost("return/{deliveryId}")]
    public async Task<IActionResult> ReturnBook(long deliveryId)
    {
        var result = await _deliveryService.ReturnBook(deliveryId);
        return Ok(result);
    }

    [HttpPost("reservation/{reservId}")]
    public async Task<IActionResult> DeliverReservedBook(long reservId)
    {
        var result = await _deliveryService.ReservationDelivery(reservId);
        return Ok(result);
    }

    [HttpGet("all-reservations")]
    public async Task<ActionResult<List<DeliveryDto>>> GetAllReservedDeliveries()
    {
        var deliveries = await _deliveryService.GetAllResrvDelivery();
        return Ok(deliveries);
    }
}