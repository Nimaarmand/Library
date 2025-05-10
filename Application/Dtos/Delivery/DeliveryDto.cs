namespace Application.Dtos.Delivery
{
    public class DeliveryDto
    {
        public long Id { get; set; }
        public long BookId { get; set; }
        public string UserId { get; set; }
        /// <summary>
        /// تاریخ تحویل کتاب
        /// </summary>
        public DateTime DeliveryTime { get; set; }
    }
}
