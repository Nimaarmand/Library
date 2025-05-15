namespace Application.Dtos.Books
{
    public class ReservationDto
    {
        public DateTime ReservationDate { get; set; }=DateTime.Now;    
        public string UserId { get; set; }
        public long BookId { get; set; }
    }
   
}
