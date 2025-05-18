using Domain.Entities.Books;

namespace Application.Dtos.Books
{


    public class BookDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
        public string Subject { get; set; }
        public string ISBN { get; set; }
        public int Pages { get; set; }
        public string Publisher { get; set; }
        public string Language { get; set; }
        public bool IsAvailable { get; set; }
        public DateTime PublicationDate { get; set; }
        public long BookCategoriesId { get; set; }    
        public List<BookCategoriesDto> BookCategories { get; set; } = new List<BookCategoriesDto>();
    }
}
