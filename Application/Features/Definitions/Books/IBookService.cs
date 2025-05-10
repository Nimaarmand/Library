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
        public Task<List<Book>> GetAllBooksAsync(CancellationToken cancellationToken = default);
        Task<string> UpdateAsync(BookDto bookDto);
        Task<string> RemoveAsync(long bookId);
    }
}
