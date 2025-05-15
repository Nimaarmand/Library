using Application.Dtos.Books;
using Domain.Entities.Books;

namespace Application.Features.Definitions.Books
{
    /// <summary>
    /// سرویس مدیریت کتاب ها 
    /// </summary>
    public interface IBookService
    {
        Task<string> AddAsync(BookDto book);
        Task<BookDto> FindAsync(long id);
        public Task<List<BookDto>> GetAllBooksAsync();
        public Task<List<BookDto>> GetAllReservation();
        public Task<List<BookDto>> GetAllNotReservation();
        Task<string> UpdateAsync(BookDto bookDto);
        Task<string> RemoveAsync(long bookId);
    }
}
