using System.ComponentModel.DataAnnotations;

namespace Application.Dtos.Books
{
    public class BookCategoriesDto
    {
        public long Id { get; set; }
        [Required(ErrorMessage = "نام دسته‌بندی الزامی است.")]

        public string Name { get; set; }
        public string Description { get; set; }
        public int ChildNumber { get; set; }
        public long? ParentId { get; set; }               
        public long? BookId { get; set; }
        public List<BookCategoriesDto> Children { get; internal set; }
    }
}
