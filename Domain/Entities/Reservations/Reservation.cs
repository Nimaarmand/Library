using Domain.Entities.Books;
using Domain.Entities.Commons;

namespace Domain.Entities.Reservations
{
    public class Reservation : BaseEntity
    {
        public DateTime ReservationDate { get; set; }
        public DateTime ExpirationDate { get; set; }

        // ارتباط مستقیم با کاربر
        public string UserId { get; set; }

        // ارتباط مستقیم با کتاب
        public long BookId { get; set; }
        public virtual Book Book { get; set; }

        // ارتباط مستقیم با وضعیت تحویل
        public virtual ICollection<DeliveryStatus> DeliveryStatuses { get; set; } = new List<DeliveryStatus>();
    }



}
