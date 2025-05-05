using Domain.Entities.Reservations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
