namespace Domain.Entities.Reservations
{
    public class DeliveryStatus 
    {
        public long Id { get; set; }
        public string UserId { get; set; }
        /// <summary>
        /// تاریخ تحویل کتاب
        /// </summary>
        public DateTime Delivery { get; set; }
        public long ReservationId { get; set; }
        public Reservation Reservation { get; set; }
    }
}
