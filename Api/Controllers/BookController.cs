using Application.Dtos.Books;
using Application.Features.Definitions.Books;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddBook([FromBody] BookDto bookDto)
        {
            var result = await _bookService.AddAsync(bookDto);
            return Ok(new { message = result });
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllBooks()
        {
            var books = await _bookService.GetAllBooksAsync();
            return Ok(books);
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateBook([FromBody] BookDto bookDto)
        {
            var result = await _bookService.UpdateAsync(bookDto);
            return Ok(new { message = result });
        }

        [HttpDelete("remove/{id}")]
        public async Task<IActionResult> RemoveBook(long id)
        {
            var result = await _bookService.RemoveAsync(id);
            return Ok(new { message = result });
        }

        [HttpGet("find/{id}")]
        public async Task<IActionResult> FindBook(long id)
        {
            var book = await _bookService.FindAsync(id);
            if (book == null)
                return NotFound(new { message = "کتاب یافت نشد." });

            return Ok(book);
        }

        [HttpGet("reserved")]
        public async Task<IActionResult> GetReservedBooks()
        {
            var books = await _bookService.GetAllReservation();
            return Ok(books);
        }

        [HttpGet("not-reserved")]
        public async Task<IActionResult> GetNotReservedBooks()
        {
            var books = await _bookService.GetAllNotReservation();
            return Ok(books);
        }
    }
}
