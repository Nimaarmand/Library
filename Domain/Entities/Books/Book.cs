using Domain.Entities.Commons;
using Domain.Entities.Reservations;

namespace Domain.Entities.Books
{


    public class Book : BaseEntity
    {
        public string? Name { get; set; }
        public string? Author { get; set; }
        public string? Subject { get; set; }
        public string? ISBN { get; set; }
        public int Pages { get; set; }
        public string? Publisher { get; set; }
        public string? Language { get; set; }
        public bool IsAvailable { get; set; } = true;
        public string? PublicationDate { get; set; }
      
        public long BookCategoriesId { get; set; }

        public virtual ICollection<BookCategories> BookCategories { get; set; } = new List<BookCategories>();
       
   
        public virtual ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
        
        public virtual ICollection<Deliverys> Deliveries { get; set; } = new List<Deliverys>();
    }

}





