using Domain.Entities.Books;
using Domain.Entities.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Reservations
{
    public class Reservation: BaseEntity
    {
        public DateTime ReservationDate { get; set; }
        public DateTime ExpirationDate { get; set; }    
        public string UserId { get; set; }
        public long BookId { get; set; }
         public Book Book { get; set; }  
    }

  

    
}
