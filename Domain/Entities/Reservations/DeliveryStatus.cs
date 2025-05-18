using Domain.Entities.Commons;

namespace Domain.Entities.Reservations
{
    public class DeliveryStatus : BaseEntity
    {
        public long Id { get; set; }
        public string UserId { get; set; }
        public bool DeliveryState { get; set; } = false;
        public DateTime BookBack { get; set; } = DateTime.Now;    
        public long DeliveryId { get; set; }
        public virtual Deliverys Delivery { get; set; }

    }
}
