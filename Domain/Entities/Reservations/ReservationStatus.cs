namespace Domain.Entities.Reservations
{
    public class ReservationStatus 
    {
        public long Id { get; set; }
        public DateTime StatusDate { get; set; }
        public long ReservationId { get; set; }
        public Reservation Reservation { get; set; }
    }
}
