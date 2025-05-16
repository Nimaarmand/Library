using Application.Dtos.Books;
using Application.Features.Definitions.Books;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class BookCategoriesController : ControllerBase
{
    private readonly IBookCategories _bookCategoriesService;

    public BookCategoriesController(IBookCategories bookCategoriesService)
    {
        _bookCategoriesService = bookCategoriesService;
    }

    [HttpPost("add")]
    public async Task<IActionResult> AddCategory([FromBody] BookCategoriesDto categoryDto)
    {
        var result = await _bookCategoriesService.AddAsync(categoryDto);
        return Ok(result);
    }

    [HttpGet("all")]
    public async Task<ActionResult<List<BookCategoriesDto>>> GetAllCategories([FromQuery] long? parentId)
    {
        var categories = await _bookCategoriesService.GetAllCategoriesAsync(parentId);
        return Ok(categories);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<BookCategoriesDto>> GetCategory(long id)
    {
        var category = await _bookCategoriesService.FindAsync(id);
        if (category == null) return NotFound("دسته‌بندی یافت نشد.");
        return Ok(category);
    }

    [HttpPut("update")]
    public async Task<IActionResult> UpdateCategory([FromBody] BookCategoriesDto categoryDto)
    {
        var result = await _bookCategoriesService.UpdateAsync(categoryDto);
        return Ok(result);
    }

    [HttpDelete("remove/{id}")]
    public async Task<IActionResult> RemoveCategory(long id)
    {
        var result = await _bookCategoriesService.RemoveAsync(id);
        return Ok(result);
    }

    [HttpPost("add-child")]
    public async Task<IActionResult> AddChildCategory([FromBody] BookCategoriesDto categoryDto)
    {
        var result = await _bookCategoriesService.AddChildAsync(categoryDto);
        return Ok(result);
    }

    [HttpGet("children/{parentId}")]
    public async Task<ActionResult<List<BookCategoriesDto>>> GetChildCategories(long parentId)
    {
        
        return Ok();
    }

    [HttpGet("books/{categoryId}")]
    public async Task<ActionResult<List<BookCategoriesDto>>> GetBooksByCategory(long categoryId)
    {
      
        return Ok();
    }
}