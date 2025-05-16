using Application.Dtos.Books;
using Application.Features.Definitions.Books;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class BooksController : ControllerBase
{
    private readonly IBookService _bookService;

    public BooksController(IBookService bookService)
    {
        _bookService = bookService;
    }

    [HttpPost("add")]
    public async Task<IActionResult> AddBook([FromBody] BookDto bookDto)
    {
        var result = await _bookService.AddAsync(bookDto);
        return Ok(result);
    }

    [HttpGet("all")]
    public async Task<ActionResult<List<BookDto>>> GetAllBooks()
    {
        var books = await _bookService.GetAllBooksAsync();
        return Ok(books);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<BookDto>> GetBook(long id)
    {
        var book = await _bookService.FindAsync(id);
        if (book == null) return NotFound("کتاب یافت نشد.");
        return Ok(book);
    }

    [HttpPut("update")]
    public async Task<IActionResult> UpdateBook([FromBody] BookDto bookDto)
    {
        var result = await _bookService.UpdateAsync(bookDto);
        return Ok(result);
    }

    [HttpDelete("remove/{id}")]
    public async Task<IActionResult> RemoveBook(long id)
    {
        var result = await _bookService.RemoveAsync(id);
        return Ok(result);
    }

    [HttpGet("reserved")]
    public async Task<ActionResult<List<BookDto>>> GetReservedBooks()
    {
        var books = await _bookService.GetAllReservation();
        return Ok(books);
    }

    [HttpGet("not-reserved")]
    public async Task<ActionResult<List<BookDto>>> GetNotReservedBooks()
    {
        var books = await _bookService.GetAllNotReservation();
        return Ok(books);
    }
}