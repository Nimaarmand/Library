using Domain.Entities.Books;
using Domain.Entities.Commons;
using Domain.Entities.Users;

namespace Domain.Entities.Reservations
{
    public class Reservation : BaseEntity
    {
        public DateTime ReservationDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string UserId { get; set; }    
        public ApplicationUser User { get; set; }     
        public long BookId { get; set; }
        public virtual Book Book { get; set; }    
        public virtual ICollection<DeliveryStatus> DeliveryStatuses { get; set; } = new List<DeliveryStatus>();
    }



}
