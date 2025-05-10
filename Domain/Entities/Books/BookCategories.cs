using Domain.Entities.Commons;

namespace Domain.Entities.Books
{
    public class BookCategories : BaseEntity
    {
        public string Name { get; set; }
        public string? ChildName { get; set; }// نام فرزند مرتبط
        public long? ChildNumber { get; set; }
        public string Description { get; set; }
        public ICollection<Book> Book { get; set; } = new List<Book>();
        public long bookId { get; set; }
    }
}
