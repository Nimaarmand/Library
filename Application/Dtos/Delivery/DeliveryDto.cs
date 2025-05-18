using Application.Dtos.Identity.UserProfile;

namespace Application.Dtos.Delivery
{
    public class DeliveryDto
    {
        public long Id { get; set; }
        public long BookId { get; set; }
        public long ReservationId { get; set; }
        public string UserId { get; set; }
        /// <summary>
        /// تاریخ تحویل کتاب
        /// </summary>
        public DateTime DeliveryTime { get; set; }
        public string UserName { get; set; }
        public bool DeliveryState { get; set; }
        public string? BookName { get; internal set; }
       
    }
}
