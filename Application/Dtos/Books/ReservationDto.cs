namespace Application.Dtos.Books
{
    public class ReservationDto
    {
        public DateTime ReservationDate { get; set; }
        public ReservationStatusDto ReservationStatusDto { get; set; }
        public string UserId { get; set; }
        public long BookId { get; set; }

    }
    public enum ReservationStatusDto
    {
        Reserved,
        Canceled,
        Completed
    }
}
