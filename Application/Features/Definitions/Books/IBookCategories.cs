using Application.Dtos.Books;
using Domain.Entities.Books;

namespace Application.Features.Definitions.Books
{
    /// <summary>
    /// سرویس مدیریت دسته یندی ها
    /// </summary>
    public interface IBookCategories
    {


        Task<string> AddAsync(BookCategoriesDto  categoriesDto);
        public Task<List<BookCategoriesDto>> GetAllCategoriesAsync( );
        Task<string> UpdateAsync(BookCategoriesDto  categoriesDto);
        Task<string> RemoveAsync(long bookId);
    }
}
