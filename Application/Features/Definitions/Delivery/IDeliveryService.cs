using Application.Dtos.Delivery;

namespace Application.Features.Definitions.Delivery
{
    public interface IDeliveryService
    {
       
        Task<string> Add(DeliveryDto deliveryDto);

       
        Task<string> ReturnBook(long deliveryId);

       
        Task<string> ReservationDelivery(long reservId);
        Task<List<DeliveryDto>> GetAllResrvDelivery();
    }
}
