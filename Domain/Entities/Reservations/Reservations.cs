using Domain.Entities.Books;
using Domain.Entities.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Reservations
{
    public class Reservation:BaseEntity
    {
        public DateTime DeliveryDate { get; set; }
        public string UserId { get; set; }
        public long BookId { get; set; }
        public Book Book { get; set; }
        public ICollection<ReservationStatus> ReservationStatus { get; set; } = new List<ReservationStatus>();

    }
}
