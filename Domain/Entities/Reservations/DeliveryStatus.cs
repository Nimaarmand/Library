using Domain.Entities.Commons;

namespace Domain.Entities.Reservations
{
    public class DeliveryStatus : BaseEntity
    {
        public long Id { get; set; }
        public string UserId { get; set; }
        // ارتباط مستقیم با رزرو
        public long ReservationId { get; set; }
        public virtual Reservation Reservation { get; set; }
        // وضعیت تحویل
        public bool DeliveryState { get; set; } = false;
        // تاریخ بازگشت کتاب
        public DateTime BookBack { get; set; } = DateTime.Now;
        /// <summary>
        /// ارتباط با تحویل کتاب
        /// </summary>
        public long DeliveryId { get; set; }
        public virtual Deliverys Delivery { get; set; }

    }
}
