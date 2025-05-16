namespace Application.Dtos.Books
{
    public class ReservationDto
    {
        public long Id { get; set; }
        public DateTime ReservationDate { get; set; }=DateTime.Now;
        public DateTime ExpirationDate { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string BookName { get; set; }
        public long BookId { get; set; }
    }
   
}
