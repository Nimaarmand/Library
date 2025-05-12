using Application.Dtos.Books;
using Application.Dtos.Commons;
using Domain.Entities.Books;

namespace Application.Features.Definitions.Books
{
    /// <summary>
    /// سرویس مدیریت دسته یندی ها
    /// </summary>
    public interface IBookCategories
    {

      
       

        Task<string> AddAsync(BookCategoriesDto  categoriesDto);
        Task<string> AddChildAsync(BookCategoriesDto bookCategoriesDto);
        Task<BookCategoriesDto> FindAsync(long id);
        public Task<List<BookCategoriesDto>> GetAllCategoriesAsync(long? parentId );
        Task<string> UpdateAsync(BookCategoriesDto  categoriesDto);
        Task<string> RemoveAsync(long bookId);
    }
}
