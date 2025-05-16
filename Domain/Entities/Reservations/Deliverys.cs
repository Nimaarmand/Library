using Domain.Entities.Books;

namespace Domain.Entities.Reservations
{
    public class Deliverys
    {
        public long Id { get; set; }
        public string UserId { get; set; }
        /// <summary>
        /// تاریخ تحویل کتاب
        /// </summary>
        public DateTime DeliveryTime { get; set; }
        public long ReservationId { get; set; }
        public Reservation Reservation { get; set; }
        public long BookId { get; set; }
        public virtual Book Book { get; set; }
        public virtual ICollection<DeliveryStatus> DeliveryStatuses { get; set; } = new List<DeliveryStatus>();
       

    }
}
