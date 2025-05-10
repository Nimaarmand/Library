namespace Application.Dtos.Books
{
    public class BookCategoriesDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string ChildName { get; set; } // نام فرزند مرتبط
        public int ChildNumber { get; set; }
        public string Description { get; set; }
    }
}
