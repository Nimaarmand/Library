using Domain.Entities.Commons;

namespace Domain.Entities.Books
{
    public class BookCategories 
    {

        public long Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public long? ParentId { get; set; }
        public BookCategories? Parent { get; set; }
        public ICollection<BookCategories> Children { get; set; } = new List<BookCategories>();

        public ICollection<Book> Book { get; set; } = new List<Book>();
        public long bookId { get; set; }
    }
}

